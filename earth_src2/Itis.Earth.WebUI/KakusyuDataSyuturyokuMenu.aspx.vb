Imports Itis.Earth.BizLogic
Imports Itis.Earth.DataAccess

Partial Public Class KakusyuDataSyuturyokuMenu
    Inherits System.Web.UI.Page

    '�C���X�^���X����
    Private KakusyuDataSyuturyokuMenuBL As New KakusyuDataSyuturyokuMenuLogic
    Private commonCheck As New CommonCheck
    Private SEP_STRING As String = "$$$"

    '�{�^��
    Private blnBtn As Boolean

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '�����`�F�b�N
        Dim user_info As New LoginUserInfo
        Dim ninsyou As New Ninsyou()
        Dim jBn As New Jiban '�n�Չ�ʋ��ʃN���X
        ' ���[�U�[��{�F��
        jBn.userAuth(user_info)
        If ninsyou.GetUserID() = String.Empty Then
            Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
            Server.Transfer("CommonErr.aspx")
        End If

        If user_info Is Nothing Then
            Me.btnSeikyuuSakiSyuturyoku.Enabled = False
            Me.btnTyousaKaisyaSyuturyoku.Enabled = False
            Me.btnSyouhinSyuturyoku.Enabled = False
            Me.btnGinkouSyuturyoku.Enabled = False
        Else
            If user_info.Account Is Nothing Then
                Me.btnSeikyuuSakiSyuturyoku.Enabled = False
                Me.btnTyousaKaisyaSyuturyoku.Enabled = False
                Me.btnSyouhinSyuturyoku.Enabled = False
                Me.btnGinkouSyuturyoku.Enabled = False
            Else
                Me.btnSeikyuuSakiSyuturyoku.Enabled = True
                Me.btnTyousaKaisyaSyuturyoku.Enabled = True
                Me.btnSyouhinSyuturyoku.Enabled = True
                Me.btnGinkouSyuturyoku.Enabled = True
            End If
        End If

        '�����`�F�b�N��
        Dim strNinsyou As String = ""
        Dim commonChk As New CommonCheck
        Dim strUserID As String = ""
        '�����`�F�b�N����ѐݒ�
        blnBtn = commonChk.CommonNinnsyou(strUserID, "keiri_gyoumu_kengen")

        '����
        If Not blnBtn Then
            btnDataSyuturyoku.Enabled = False
            btnHyouji.Enabled = False
        End If

        If Not IsPostBack Then
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
            '�u������w��v�^�C�g���@�����N�ݒ�
            Call titleTextRefSet()
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
            'DDL�̏����ݒ�
            SetDdlListInf()
        End If

        'JavaScript
        MakeScript()

        '�����於
        Me.tbxSeikyuuSakiMei.Attributes.Add("readonly", "true;")
        '�d����
        Me.tbxSiireMei.Attributes.Add("readonly", "true;")
        '�x����
        Me.tbxSiharaiMei.Attributes.Add("readonly", "true;")
        '���o����
        tbxFrom.Attributes.Add("onblur", "checkDate(this);")
        tbxTo.Attributes.Add("onblur", "checkDate(this);")
        '�����於�A�d���於�A�x���於
        Me.ddlSeikyuuSaki.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd.Attributes.Add("onblur", "fncblur(this,'1')")
        Me.tbxSeikyuuSakiBrc.Attributes.Add("onblur", "fncblur(this,'1')")
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        Me.ddlSeikyuuSaki1.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei1.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd1.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc1.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd1.Attributes.Add("onblur", "fncblur(this,'Seikyuu1')")
        Me.tbxSeikyuuSakiBrc1.Attributes.Add("onblur", "fncblur(this,'Seikyuu1')")

        Me.ddlSeikyuuSaki2.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei2.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd2.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc2.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd2.Attributes.Add("onblur", "fncblur(this,'Seikyuu2')")
        Me.tbxSeikyuuSakiBrc2.Attributes.Add("onblur", "fncblur(this,'Seikyuu2')")

        Me.ddlSeikyuuSaki3.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei3.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd3.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc3.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd3.Attributes.Add("onblur", "fncblur(this,'Seikyuu3')")
        Me.tbxSeikyuuSakiBrc3.Attributes.Add("onblur", "fncblur(this,'Seikyuu3')")

        Me.ddlSeikyuuSaki4.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei4.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd4.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc4.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd4.Attributes.Add("onblur", "fncblur(this,'Seikyuu4')")
        Me.tbxSeikyuuSakiBrc4.Attributes.Add("onblur", "fncblur(this,'Seikyuu4')")

        Me.ddlSeikyuuSaki5.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei5.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd5.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc5.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd5.Attributes.Add("onblur", "fncblur(this,'Seikyuu5')")
        Me.tbxSeikyuuSakiBrc5.Attributes.Add("onblur", "fncblur(this,'Seikyuu5')")

        Me.ddlSeikyuuSaki6.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei6.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd6.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc6.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd6.Attributes.Add("onblur", "fncblur(this,'Seikyuu6')")
        Me.tbxSeikyuuSakiBrc6.Attributes.Add("onblur", "fncblur(this,'Seikyuu6')")

        Me.ddlSeikyuuSaki7.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei7.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd7.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc7.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd7.Attributes.Add("onblur", "fncblur(this,'Seikyuu7')")
        Me.tbxSeikyuuSakiBrc7.Attributes.Add("onblur", "fncblur(this,'Seikyuu7')")

        Me.ddlSeikyuuSaki8.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei8.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd8.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc8.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd8.Attributes.Add("onblur", "fncblur(this,'Seikyuu8')")
        Me.tbxSeikyuuSakiBrc8.Attributes.Add("onblur", "fncblur(this,'Seikyuu8')")

        Me.ddlSeikyuuSaki9.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei9.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd9.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc9.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd9.Attributes.Add("onblur", "fncblur(this,'Seikyuu9')")
        Me.tbxSeikyuuSakiBrc9.Attributes.Add("onblur", "fncblur(this,'Seikyuu9')")

        Me.ddlSeikyuuSaki10.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei10.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd10.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc10.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd10.Attributes.Add("onblur", "fncblur(this,'Seikyuu10')")
        Me.tbxSeikyuuSakiBrc10.Attributes.Add("onblur", "fncblur(this,'Seikyuu10')")

        Me.ddlSeikyuuSaki11.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei11.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd11.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc11.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd11.Attributes.Add("onblur", "fncblur(this,'Seikyuu11')")
        Me.tbxSeikyuuSakiBrc11.Attributes.Add("onblur", "fncblur(this,'Seikyuu11')")

        Me.ddlSeikyuuSaki12.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei12.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd12.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc12.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd12.Attributes.Add("onblur", "fncblur(this,'Seikyuu12')")
        Me.tbxSeikyuuSakiBrc12.Attributes.Add("onblur", "fncblur(this,'Seikyuu12')")

        Me.ddlSeikyuuSaki13.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei13.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd13.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc13.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd13.Attributes.Add("onblur", "fncblur(this,'Seikyuu13')")
        Me.tbxSeikyuuSakiBrc13.Attributes.Add("onblur", "fncblur(this,'Seikyuu13')")

        Me.ddlSeikyuuSaki14.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei14.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd14.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc14.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd14.Attributes.Add("onblur", "fncblur(this,'Seikyuu14')")
        Me.tbxSeikyuuSakiBrc14.Attributes.Add("onblur", "fncblur(this,'Seikyuu14')")

        Me.ddlSeikyuuSaki15.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei15.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd15.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc15.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd15.Attributes.Add("onblur", "fncblur(this,'Seikyuu15')")
        Me.tbxSeikyuuSakiBrc15.Attributes.Add("onblur", "fncblur(this,'Seikyuu15')")

        Me.ddlSeikyuuSaki16.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei16.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd16.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc16.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd16.Attributes.Add("onblur", "fncblur(this,'Seikyuu16')")
        Me.tbxSeikyuuSakiBrc16.Attributes.Add("onblur", "fncblur(this,'Seikyuu16')")

        Me.ddlSeikyuuSaki17.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei17.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd17.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc17.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd17.Attributes.Add("onblur", "fncblur(this,'Seikyuu17')")
        Me.tbxSeikyuuSakiBrc17.Attributes.Add("onblur", "fncblur(this,'Seikyuu17')")

        Me.ddlSeikyuuSaki18.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei18.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd18.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc18.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd18.Attributes.Add("onblur", "fncblur(this,'Seikyuu18')")
        Me.tbxSeikyuuSakiBrc18.Attributes.Add("onblur", "fncblur(this,'Seikyuu18')")

        Me.ddlSeikyuuSaki19.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei19.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd19.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc19.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd19.Attributes.Add("onblur", "fncblur(this,'Seikyuu19')")
        Me.tbxSeikyuuSakiBrc19.Attributes.Add("onblur", "fncblur(this,'Seikyuu19')")

        Me.ddlSeikyuuSaki20.Attributes.Add("onchange", "document.getElementById('" & Me.tbxSeikyuuSakiMei20.ClientID & "').value='';")
        Me.tbxSeikyuuSakiCd20.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiBrc20.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSeikyuuSakiCd20.Attributes.Add("onblur", "fncblur(this,'Seikyuu20')")
        Me.tbxSeikyuuSakiBrc20.Attributes.Add("onblur", "fncblur(this,'Seikyuu20')")
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        '�d���於
        Me.tbxSiireCd.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSiireCd.Attributes.Add("onblur", "fncblur(this,'3')")
        '�x���於
        Me.tbxSiharaiSakiCd.Attributes.Add("onfocus", "fncFocus(this)")
        Me.tbxSiharaiSakiCd.Attributes.Add("onblur", "fncblur(this,'2')")

    End Sub

    ''' <summary>Javascript�쐬</summary>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub MakeScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "GetScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>")

            '�t�H�[�J�X
            .AppendLine("function fncFocus(obj)")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            .AppendLine("   hidConfirm.value = obj.value;")
            .AppendLine("}")
            '���X�g�t�H�[�J�X
            .AppendLine("function fncblur(obj,strFlg)")
            .AppendLine("{")
            .AppendLine("   var hidConfirm = document.getElementById('" & Me.hidConfirm.ClientID & "')")
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
            .AppendLine("   var tbxSeikyuuSakiMei1 = document.getElementById('" & Me.tbxSeikyuuSakiMei1.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei2 = document.getElementById('" & Me.tbxSeikyuuSakiMei2.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei3 = document.getElementById('" & Me.tbxSeikyuuSakiMei3.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei4 = document.getElementById('" & Me.tbxSeikyuuSakiMei4.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei5 = document.getElementById('" & Me.tbxSeikyuuSakiMei5.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei6 = document.getElementById('" & Me.tbxSeikyuuSakiMei6.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei7 = document.getElementById('" & Me.tbxSeikyuuSakiMei7.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei8 = document.getElementById('" & Me.tbxSeikyuuSakiMei8.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei9 = document.getElementById('" & Me.tbxSeikyuuSakiMei9.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei10 = document.getElementById('" & Me.tbxSeikyuuSakiMei10.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei11 = document.getElementById('" & Me.tbxSeikyuuSakiMei11.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei12 = document.getElementById('" & Me.tbxSeikyuuSakiMei12.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei13 = document.getElementById('" & Me.tbxSeikyuuSakiMei13.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei14 = document.getElementById('" & Me.tbxSeikyuuSakiMei14.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei15 = document.getElementById('" & Me.tbxSeikyuuSakiMei15.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei16 = document.getElementById('" & Me.tbxSeikyuuSakiMei16.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei17 = document.getElementById('" & Me.tbxSeikyuuSakiMei17.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei18 = document.getElementById('" & Me.tbxSeikyuuSakiMei18.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei19 = document.getElementById('" & Me.tbxSeikyuuSakiMei19.ClientID & "')")
            .AppendLine("   var tbxSeikyuuSakiMei20 = document.getElementById('" & Me.tbxSeikyuuSakiMei20.ClientID & "')")
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
            .AppendLine("   var tbxSeikyuuSakiMei = document.getElementById('" & Me.tbxSeikyuuSakiMei.ClientID & "')")
            .AppendLine("   var tbxSiharaiMei = document.getElementById('" & Me.tbxSiharaiMei.ClientID & "')")
            .AppendLine("   var tbxSiireMei = document.getElementById('" & Me.tbxSiireMei.ClientID & "')")
            .AppendLine("   if(hidConfirm.value != obj.value){")
            .AppendLine("       if(strFlg == '1'){")
            .AppendLine("           tbxSeikyuuSakiMei.value = '';")
            .AppendLine("       }else if(strFlg == '2'){")
            .AppendLine("           tbxSiharaiMei.value = '';")
            .AppendLine("       }else if(strFlg == '3'){")
            .AppendLine("           tbxSiireMei.value = '';")
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
            .AppendLine("       }else if(strFlg == 'Seikyuu1'){")
            .AppendLine("           tbxSeikyuuSakiMei1.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu2'){")
            .AppendLine("           tbxSeikyuuSakiMei2.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu3'){")
            .AppendLine("           tbxSeikyuuSakiMei3.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu4'){")
            .AppendLine("           tbxSeikyuuSakiMei4.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu5'){")
            .AppendLine("           tbxSeikyuuSakiMei5.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu6'){")
            .AppendLine("           tbxSeikyuuSakiMei6.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu7'){")
            .AppendLine("           tbxSeikyuuSakiMei7.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu8'){")
            .AppendLine("           tbxSeikyuuSakiMei8.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu9'){")
            .AppendLine("           tbxSeikyuuSakiMei9.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu10'){")
            .AppendLine("           tbxSeikyuuSakiMei10.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu11'){")
            .AppendLine("           tbxSeikyuuSakiMei11.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu12'){")
            .AppendLine("           tbxSeikyuuSakiMei12.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu13'){")
            .AppendLine("           tbxSeikyuuSakiMei13.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu14'){")
            .AppendLine("           tbxSeikyuuSakiMei14.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu15'){")
            .AppendLine("           tbxSeikyuuSakiMei15.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu16'){")
            .AppendLine("           tbxSeikyuuSakiMei16.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu17'){")
            .AppendLine("           tbxSeikyuuSakiMei17.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu18'){")
            .AppendLine("           tbxSeikyuuSakiMei18.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu19'){")
            .AppendLine("           tbxSeikyuuSakiMei19.value = '';")
            .AppendLine("       }else if(strFlg == 'Seikyuu20'){")
            .AppendLine("           tbxSeikyuuSakiMei20.value = '';")
            .AppendLine("   }")
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
            .AppendLine("       hidConfirm.value = '';")
            .AppendLine("   }")
            .AppendLine("}")

            .AppendLine("</script>")
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
        '2015/03/04 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        If Me.ddlUriageData.SelectedValue = "0" Then
            '������w�薾�ׂ�display��Ԃ�ێ�����
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SeikyuuMeisai", "objEBI('" & Me.tblSeikyuuSakiMeisai.ClientID & "').style.display =objEBI('" & Me.hidSeikyuuStatus.ClientID & "').value;", True)
        End If
        '2015/03/04 ���h�m(��A���V�X�e����)�@�ǉ�  End
    End Sub

    ''' <summary>
    ''' �l��ύX��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ddlUriageData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUriageData.SelectedIndexChanged

        '�I�����ꂽ�o�̓f�[�^��ʂɂ��A�����������͍��ڂ̕\���A��\����؂�ւ���
        Select Case Me.ddlUriageData.SelectedValue
            Case "0"
                '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
                Me.tblSeikyuuSaki.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End
                Me.ddlSeikyuuSaki.SelectedValue = ""
                '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
                'Me.tbxSeikyuuSakiCd.Text = ""
                'Me.tbxSeikyuuSakiBrc.Text = ""
                'Me.tbxSeikyuuSakiMei.Text = ""
                '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End
                Me.tblSiireSaki.Visible = False
                Me.tblSiharaiSaki.Visible = False
                Me.tblH.Visible = True
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.tblSeikyuuSakiSitei.Visible = True
                Me.ddlSeikyuuSaki1.SelectedValue = ""
                Me.tbxSeikyuuSakiCd1.Text = ""
                Me.tbxSeikyuuSakiBrc1.Text = ""
                Me.tbxSeikyuuSakiMei1.Text = ""
                Me.ddlSeikyuuSaki2.SelectedValue = ""
                Me.tbxSeikyuuSakiCd2.Text = ""
                Me.tbxSeikyuuSakiBrc2.Text = ""
                Me.tbxSeikyuuSakiMei2.Text = ""
                Me.ddlSeikyuuSaki3.SelectedValue = ""
                Me.tbxSeikyuuSakiCd3.Text = ""
                Me.tbxSeikyuuSakiBrc3.Text = ""
                Me.tbxSeikyuuSakiMei3.Text = ""
                Me.ddlSeikyuuSaki4.SelectedValue = ""
                Me.tbxSeikyuuSakiCd4.Text = ""
                Me.tbxSeikyuuSakiBrc4.Text = ""
                Me.tbxSeikyuuSakiMei4.Text = ""
                Me.ddlSeikyuuSaki5.SelectedValue = ""
                Me.tbxSeikyuuSakiCd5.Text = ""
                Me.tbxSeikyuuSakiBrc5.Text = ""
                Me.tbxSeikyuuSakiMei5.Text = ""
                Me.ddlSeikyuuSaki6.SelectedValue = ""
                Me.tbxSeikyuuSakiCd6.Text = ""
                Me.tbxSeikyuuSakiBrc6.Text = ""
                Me.tbxSeikyuuSakiMei6.Text = ""
                Me.ddlSeikyuuSaki7.SelectedValue = ""
                Me.tbxSeikyuuSakiCd7.Text = ""
                Me.tbxSeikyuuSakiBrc7.Text = ""
                Me.tbxSeikyuuSakiMei7.Text = ""
                Me.ddlSeikyuuSaki8.SelectedValue = ""
                Me.tbxSeikyuuSakiCd8.Text = ""
                Me.tbxSeikyuuSakiBrc8.Text = ""
                Me.tbxSeikyuuSakiMei8.Text = ""
                Me.ddlSeikyuuSaki9.SelectedValue = ""
                Me.tbxSeikyuuSakiCd9.Text = ""
                Me.tbxSeikyuuSakiBrc9.Text = ""
                Me.tbxSeikyuuSakiMei9.Text = ""
                Me.ddlSeikyuuSaki10.SelectedValue = ""
                Me.tbxSeikyuuSakiCd10.Text = ""
                Me.tbxSeikyuuSakiBrc10.Text = ""
                Me.tbxSeikyuuSakiMei10.Text = ""
                Me.ddlSeikyuuSaki11.SelectedValue = ""
                Me.tbxSeikyuuSakiCd11.Text = ""
                Me.tbxSeikyuuSakiBrc11.Text = ""
                Me.tbxSeikyuuSakiMei11.Text = ""
                Me.ddlSeikyuuSaki12.SelectedValue = ""
                Me.tbxSeikyuuSakiCd12.Text = ""
                Me.tbxSeikyuuSakiBrc12.Text = ""
                Me.tbxSeikyuuSakiMei12.Text = ""
                Me.ddlSeikyuuSaki13.SelectedValue = ""
                Me.tbxSeikyuuSakiCd13.Text = ""
                Me.tbxSeikyuuSakiBrc13.Text = ""
                Me.tbxSeikyuuSakiMei13.Text = ""
                Me.ddlSeikyuuSaki14.SelectedValue = ""
                Me.tbxSeikyuuSakiCd14.Text = ""
                Me.tbxSeikyuuSakiBrc14.Text = ""
                Me.tbxSeikyuuSakiMei14.Text = ""
                Me.ddlSeikyuuSaki15.SelectedValue = ""
                Me.tbxSeikyuuSakiCd15.Text = ""
                Me.tbxSeikyuuSakiBrc15.Text = ""
                Me.tbxSeikyuuSakiMei15.Text = ""
                Me.ddlSeikyuuSaki16.SelectedValue = ""
                Me.tbxSeikyuuSakiCd16.Text = ""
                Me.tbxSeikyuuSakiBrc16.Text = ""
                Me.tbxSeikyuuSakiMei16.Text = ""
                Me.ddlSeikyuuSaki17.SelectedValue = ""
                Me.tbxSeikyuuSakiCd17.Text = ""
                Me.tbxSeikyuuSakiBrc17.Text = ""
                Me.tbxSeikyuuSakiMei17.Text = ""
                Me.ddlSeikyuuSaki18.SelectedValue = ""
                Me.tbxSeikyuuSakiCd18.Text = ""
                Me.tbxSeikyuuSakiBrc18.Text = ""
                Me.tbxSeikyuuSakiMei18.Text = ""
                Me.ddlSeikyuuSaki19.SelectedValue = ""
                Me.tbxSeikyuuSakiCd19.Text = ""
                Me.tbxSeikyuuSakiBrc19.Text = ""
                Me.tbxSeikyuuSakiMei19.Text = ""
                Me.ddlSeikyuuSaki20.SelectedValue = ""
                Me.tbxSeikyuuSakiCd20.Text = ""
                Me.tbxSeikyuuSakiBrc20.Text = ""
                Me.tbxSeikyuuSakiMei20.Text = ""
                Me.titleText_SeikyuuSaki.Visible = True
                '������w�薾�ׂ̏����\���ݒ�
                Me.hidSeikyuuStatus.Value = "none"
            Case "4"
                Me.tblSeikyuuSakiSitei.Visible = False
                Me.tblSeikyuuSaki.Visible = True
                Me.ddlSeikyuuSaki.SelectedValue = ""
                Me.tbxSeikyuuSakiCd.Text = ""
                Me.tbxSeikyuuSakiBrc.Text = ""
                Me.tbxSeikyuuSakiMei.Text = ""
                Me.tblSiireSaki.Visible = False
                Me.tblSiharaiSaki.Visible = False
                Me.tblH.Visible = True
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
            Case "1"
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.tblSeikyuuSakiSitei.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
                Me.tblSeikyuuSaki.Visible = False
                Me.tblSiireSaki.Visible = True
                Me.tbxSiireCd.Text = ""
                Me.tbxSiireMei.Text = ""
                Me.tblSiharaiSaki.Visible = False
                Me.tblH.Visible = True
            Case "2", "3", "8", "6", "7"
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.tblSeikyuuSakiSitei.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
                Me.tblSeikyuuSaki.Visible = False
                Me.tblSiireSaki.Visible = False
                Me.tblSiharaiSaki.Visible = False
                Me.tblH.Visible = False
            Case "5"
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.tblSeikyuuSakiSitei.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
                Me.tblSeikyuuSaki.Visible = False
                Me.tblSiireSaki.Visible = False
                Me.tblSiharaiSaki.Visible = True
                Me.tbxSiharaiSakiCd.Text = ""
                Me.tbxSiharaiMei.Text = ""
                Me.tblH.Visible = True
            Case Else
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.tblSeikyuuSakiSitei.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
                Me.tblSeikyuuSaki.Visible = False
                Me.tblSiireSaki.Visible = False
                Me.tblSiharaiSaki.Visible = False
                Me.tblH.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
                Me.titleText_SeikyuuSaki.Visible = False
                '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        End Select

        '�o�̓f�[�^��ʃv���_�E���ύX������
        Dim strSysTime As String = KakusyuDataSyuturyokuMenuBL.GetSysTime()
        Select Case Me.ddlUriageData.SelectedValue
            Case "6", "7"
                Me.tbxFrom.Text = Left(DateAdd(DateInterval.Month, -1, CDate(strSysTime)).ToString("yyyy/MM/dd"), 8) & "01"
                Me.tbxTo.Text = DateAdd(DateInterval.Day, -1, CDate(CDate(strSysTime).ToString("yyyy/MM/") & "01")).ToString("yyyy/MM/dd")
            Case "8"
                Dim intBi As Integer = CDate(strSysTime).Day
                If intBi >= 1 And intBi <= 10 Then
                    Me.tbxFrom.Text = Left(DateAdd(DateInterval.Month, -1, CDate(strSysTime)).ToString("yyyy/MM/dd"), 8) & "21"
                    Me.tbxTo.Text = DateAdd(DateInterval.Day, -1, CDate(CDate(strSysTime).ToString("yyyy/MM/") & "01")).ToString("yyyy/MM/dd")
                ElseIf intBi >= 11 And intBi <= 20 Then
                    Me.tbxFrom.Text = Left(CDate(strSysTime).ToString("yyyy/MM/dd"), 8) & "01"
                    Me.tbxTo.Text = Left(CDate(strSysTime).ToString("yyyy/MM/dd"), 8) & "10"
                Else
                    Me.tbxFrom.Text = Left(CDate(strSysTime).ToString("yyyy/MM/dd"), 8) & "11"
                    Me.tbxTo.Text = Left(CDate(strSysTime).ToString("yyyy/MM/dd"), 8) & "20"
                End If
            Case Else
                Me.tbxFrom.Text = ""
                Me.tbxTo.Text = ""
        End Select

        '�I�����ꂽ�o�̓f�[�^��ʂɂ��A�f�[�^�o�̓{�^���̃L���v�V������ύX����
        Select Case Me.ddlUriageData.SelectedValue
            Case "4", "5"
                Me.btnHyouji.Visible = True
                Me.btnDataSyuturyoku.Visible = False
            Case Else
                Me.btnHyouji.Visible = False
                Me.btnDataSyuturyoku.Visible = True
        End Select

    End Sub

    ''' <summary>
    ''' �f�[�^�o�̓{�^����������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnDataSyuturyoku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDataSyuturyoku.Click
        '���̓`�F�b�N
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)
        '���b�Z�[�W�\��
        If strErr <> String.Empty Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
        Else
            Select Case Me.ddlUriageData.SelectedValue
                Case "0"
                    '����f�[�^��CSV�o��
                    '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
                    'UriageDataSyuturyokuCSV(tbxFrom.Text, tbxTo.Text, tbxSeikyuuSakiCd.Text, tbxSeikyuuSakiBrc.Text, ddlSeikyuuSaki.SelectedValue)
                    UriageDataSyuturyokuCSV(tbxFrom.Text, tbxTo.Text)
                    '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End
                Case "1"
                    '�d���f�[�^��CSV�o��
                    SiireDataSyuturyokuCSV(tbxFrom.Text, tbxTo.Text, tbxSiireCd.Text)

                Case "2"
                    '���|���c���\
                    UrikakekinZandakaHyou(Me.tbxFrom.Text, Me.tbxTo.Text)
                Case "3"
                    '���|���c���\��CSV�o��
                    KaikakekinZandakaHyouCSV(Me.tbxFrom.Text, Me.tbxTo.Text)
                Case "6"
                    'Excel�d��_����
                    ExcelSiwakeUriageCSV(Me.tbxFrom.Text, Me.tbxTo.Text)
                Case "7"
                    'Excel�d��_�d��
                    ExcelSiwakeSiireCSV(Me.tbxFrom.Text, Me.tbxTo.Text)
                Case "8"
                    'Excel�d��_����
                    ExcelSiwakeNyuukinCSV(Me.tbxFrom.Text, Me.tbxTo.Text)
            End Select
        End If

    End Sub

    ''' <summary>
    ''' ���|���c���\
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub UrikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String)
        '�f�[�^���擾
        Dim UrikakekinZandakaHyouDataTable As New ExcelSiwakeDataSet.UrikakekinZandakaHyouDataTable
        UrikakekinZandakaHyouDataTable = KakusyuDataSyuturyokuMenuBL.GetUrikakekinZandakaHyou(strDateFrom, strDateTo)

        If UrikakekinZandakaHyouDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("UrikakeDataZanCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            writer.WriteLine(EarthConst.conUrikakekinZandakaHyouCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As ExcelSiwakeDataSet.UrikakekinZandakaHyouRow In UrikakekinZandakaHyouDataTable
                writer.WriteLine(row.datakbn, row.tokuisaki_cd, row.tokuisaki_mei1, row.tokuisaki_mei2, _
                                row.kurikosi_zanndaka, row.genkin_furikomi, row.tegata, row.sousaihoka, row.nyuukin_goukei, _
                                row.mikaisyuu_zanndaka, row.uriagedaka, row.syouhizeinado, row.sasihiki_zanndaka, row.tegata_zanndaka, _
                                row.uriage_saiken)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' EXCEL�d��_����
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ExcelSiwakeUriageCSV(ByVal strDateFrom As String, ByVal strDateTo As String)
        '�f�[�^���擾
        Dim ExcelSiwakeUriageDataTable As New ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableDataTable
        ExcelSiwakeUriageDataTable = KakusyuDataSyuturyokuMenuBL.GetExcelSiwakeUriage(strDateFrom, strDateTo)

        If ExcelSiwakeUriageDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = ConfigurationManager.AppSettings("ExcelSiwakeUriageCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), vbTab, vbCrLf)

            ''CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conExcelSiwakeUriageCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableRow In ExcelSiwakeUriageDataTable
                writer.WriteLine(Replace(row.tekiyou, ",", "�A"), row.kari_zei, row.kari_kamoku_mei, row.kari_kamoku, _
                                row.kari_saimoku, row.kari_keisiki, row.kari_youto, row.kari_tukekaesaki, row.kari_line, _
                                row.kari_aitesaki, row.kari_kingaku, row.kasi_zei, row.kasi_kamoku_mei, row.kasi_kamoku, _
                                row.kasi_saimoku, row.kasi_keisiki, row.kasi_youto, row.kasi_tukekaesaki, row.kasi_line, _
                                row.kasi_aitesaki, row.kasi_kingaku)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' EXCEL�d��_�d��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ExcelSiwakeSiireCSV(ByVal strDateFrom As String, ByVal strDateTo As String)
        '�f�[�^���擾
        Dim ExcelSiwakeSiireDataTable As New ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableDataTable
        ExcelSiwakeSiireDataTable = KakusyuDataSyuturyokuMenuBL.GetExcelSiwakeSiire(strDateFrom, strDateTo)

        If ExcelSiwakeSiireDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = ConfigurationManager.AppSettings("ExcelSiwakeSiireCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), vbTab, vbCrLf)

            ''CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conExcelSiwakeSiireCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableRow In ExcelSiwakeSiireDataTable
                writer.WriteLine(Replace(row.tekiyou, ",", "�A"), row.kari_zei, row.kari_kamoku_mei, row.kari_kamoku, _
                                row.kari_saimoku, row.kari_keisiki, row.kari_youto, row.kari_tukekaesaki, row.kari_line, _
                                row.kari_aitesaki, row.kari_kingaku, row.kasi_zei, row.kasi_kamoku_mei, row.kasi_kamoku, _
                                row.kasi_saimoku, row.kasi_keisiki, row.kasi_youto, row.kasi_tukekaesaki, row.kasi_line, _
                                row.kasi_aitesaki, row.kasi_kingaku)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' EXCEL�d��_����
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub ExcelSiwakeNyuukinCSV(ByVal strDateFrom As String, ByVal strDateTo As String)
        '�f�[�^���擾
        Dim ExcelSiwakeNyuukinDataTable As New ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableDataTable
        ExcelSiwakeNyuukinDataTable = KakusyuDataSyuturyokuMenuBL.GetExcelSiwakeNyuukin(strDateFrom, strDateTo)

        If ExcelSiwakeNyuukinDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = ConfigurationManager.AppSettings("ExcelSiwakeNyukinCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), vbTab, vbCrLf)

            ''CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conExcelSiwakeNyuukinCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableRow In ExcelSiwakeNyuukinDataTable
                writer.WriteLine(Replace(row.tekiyou, ",", "�A"), row.kari_zei, row.kari_kamoku_mei, row.kari_kamoku, _
                                row.kari_saimoku, row.kari_keisiki, row.kari_youto, row.kari_tukekaesaki, row.kari_line, _
                                row.kari_aitesaki, row.kari_kingaku, row.kasi_zei, row.kasi_kamoku_mei, row.kasi_kamoku, _
                                row.kasi_saimoku, row.kasi_keisiki, row.kasi_youto, row.kasi_tukekaesaki, row.kasi_line, _
                                row.kasi_aitesaki, row.kasi_kingaku)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' ����f�[�^�o��
    ''' </summary>
    ''' <param name="fromDate">���o����FROM</param>
    ''' <param name="toDate">���o����TO</param>
    ''' <history>
    ''' 2010/07/21 �ԗ�(��A���V�X�e����)�@�V�K�쐬
    ''' 2015/03/03 ���h�m(��A���V�X�e����)�@�C��
    ''' </history>
    Private Sub UriageDataSyuturyokuCSV(ByVal fromDate As String, ByVal toDate As String)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        Dim lstSeikyuuSakiCd As New List(Of String)
        Dim lstSeikyuuSakiBrc As New List(Of String)
        Dim lstSeikyuuSakiKbn As New List(Of String)
        Dim seikyuu_saki_cd As String
        Dim seikyuu_saki_brc As String
        Dim seikyuu_saki_kbn As String
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End

        '�f�[�^���擾
        Dim uriage_data_syuturyokuCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable
        '������1�����󔒎�
        If Me.tbxSeikyuuSakiMei1.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
            seikyuu_saki_cd = tbxSeikyuuSakiCd1.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc1.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki1.SelectedValue
            '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        End If

        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������2�����󔒎�
        If Me.tbxSeikyuuSakiMei2.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd2.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc2.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki2.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������3�����󔒎�
        If Me.tbxSeikyuuSakiMei3.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd3.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc3.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki3.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������4�����󔒎�
        If Me.tbxSeikyuuSakiMei4.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd4.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc4.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki4.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������5�����󔒎�
        If Me.tbxSeikyuuSakiMei5.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd5.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc5.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki5.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������6�����󔒎�
        If Me.tbxSeikyuuSakiMei6.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd6.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc6.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki6.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������7�����󔒎�
        If Me.tbxSeikyuuSakiMei7.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd7.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc7.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki7.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������8�����󔒎�
        If Me.tbxSeikyuuSakiMei8.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd8.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc8.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki8.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������9�����󔒎�
        If Me.tbxSeikyuuSakiMei9.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd9.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc9.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki9.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������10�����󔒎�
        If Me.tbxSeikyuuSakiMei10.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd10.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc10.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki10.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������11�����󔒎�
        If Me.tbxSeikyuuSakiMei11.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd11.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc11.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki11.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������12�����󔒎�
        If Me.tbxSeikyuuSakiMei12.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd12.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc12.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki12.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������13�����󔒎�
        If Me.tbxSeikyuuSakiMei13.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd13.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc13.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki13.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������14�����󔒎�
        If Me.tbxSeikyuuSakiMei14.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd14.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc14.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki14.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������15�����󔒎�
        If Me.tbxSeikyuuSakiMei15.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd15.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc15.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki15.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������16�����󔒎�
        If Me.tbxSeikyuuSakiMei16.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd16.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc16.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki16.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������17�����󔒎�
        If Me.tbxSeikyuuSakiMei17.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd17.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc17.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki17.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������18�����󔒎�
        If Me.tbxSeikyuuSakiMei18.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd18.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc18.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki18.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������19�����󔒎�
        If Me.tbxSeikyuuSakiMei19.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd19.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc19.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki19.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)

        '������20�����󔒎�
        If Me.tbxSeikyuuSakiMei20.Text.Trim = String.Empty Then
            seikyuu_saki_cd = String.Empty
            seikyuu_saki_brc = String.Empty
            seikyuu_saki_kbn = String.Empty
        Else
            seikyuu_saki_cd = tbxSeikyuuSakiCd20.Text
            seikyuu_saki_brc = tbxSeikyuuSakiBrc20.Text
            seikyuu_saki_kbn = ddlSeikyuuSaki20.SelectedValue
        End If
        lstSeikyuuSakiCd.Add(seikyuu_saki_cd)
        lstSeikyuuSakiBrc.Add(seikyuu_saki_brc)
        lstSeikyuuSakiKbn.Add(seikyuu_saki_kbn)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start

        '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  Start
        'uriage_data_syuturyokuCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Geturiage_data_syuturyokuCSV(fromDate, toDate, seikyuu_saki_cd, seikyuu_saki_brc, seikyuu_saki_kbn)
        uriage_data_syuturyokuCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Geturiage_data_syuturyokuCSV(fromDate, toDate, lstSeikyuuSakiCd, lstSeikyuuSakiBrc, lstSeikyuuSakiKbn)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�C��  End

        If uriage_data_syuturyokuCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("UriageDataSyuturyokuCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conUriage_dataCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            Dim strHakokazu As String '--����
            For Each row As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableRow In uriage_data_syuturyokuCSVTableDataTable
                If row.hakokazu.ToString = "0" Then
                    strHakokazu = "0"
                Else
                    strHakokazu = row.hakokazu.Replace("$$$", "$").Split("$")(3)
                End If
                writer.WriteLine(row.denku, SetNengetunitiInt(row.denpyou_uri_date), SetNengetunitiInt(row.seikyuu_date), row.denpyou_no, row.tokuisaki_cd, _
                                 row.seikyuu_saki_mei, row.tyokusousaki_cd, row.senpou_tantou_mei, row.bumon, row.tantou_cd, _
                                 row.tekiyou, row.sesyu_mei, row.bunrui_cd, row.denpyou_kbn, row.syouhin_cd, _
                                 row.master_kbn, row.hinmei, row.ku, row.souko_cd, row.ikazu, _
                                 strHakokazu, row.suu, row.tani, row.tanka, row.uri_gaku, _
                                 row.gentanka, row.genkaga, row.ararieki, row.sotozei_gaku, row.utizei_gaku, _
                                 row.zei_kbn, row.zeikomi_kbn, row.bikou, row.hyoujun_kkk, row.douji_nyuuka_kbn, _
                                 row.baitanka, row.baika_kingaku, CutMaxLength(row.kikaku_kataban, 36), row.iro, row.saizu)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()

        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' �d���f�[�^�o��
    ''' </summary>
    ''' <param name="fromDate">���o����FROM</param>
    ''' <param name="toDate">���o����TO</param>
    ''' <param name="strSiireCd">������R�[�h</param>
    ''' <history>2010/07/21 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub SiireDataSyuturyokuCSV(ByVal fromDate As String, ByVal toDate As String, ByVal strSiireCd As String)
        '2010/07/16 �ԗ�(��A���V�X�e����)�@�V�K�쐬
        '�f�[�^���擾
        Dim siire_data_syuturyokuCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableDataTable
        '�d���於���󔒎�
        If Me.tbxSiireMei.Text.Trim = String.Empty Then
            strSiireCd = String.Empty
        End If
        siire_data_syuturyokuCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Getsiire_data_syuturyokuCSV(tbxFrom.Text, tbxTo.Text, strSiireCd)

        If siire_data_syuturyokuCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("SiireDataSyuturyokuCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conSiire_dataCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableRow In siire_data_syuturyokuCSVTableDataTable
                writer.WriteLine(row.nyuuka_houhou, row.kamoku_kbn, row.denku, SetNengetuniti(row.denpyou_siire_date), row.seisan_date, _
                                 row.denpyou_no, row.siiresaki_cd, row.tys_kaisya_mei, row.senpou_tantou_mei, row.bumon, _
                                 row.tantou_cd, row.tekiyou, row.sesyu_mei, row.syouhin_cd, row.master_kbn, _
                                 row.hinmei, row.ku, row.souko_cd, row.ikazu, row.hakokazu, _
                                 row.suu, row.tani, row.tanka, row.siire_gaku, row.sotozei_gaku, _
                                 row.utizei_gaku, row.zei_kbn, row.zeikomi_kbn, row.bikou, row.kikaku_kataban, _
                                 row.iro, row.saizu)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()

        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' �\���{�^����������
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnHyouji_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHyouji.Click

        'URL�`�F�b�N
        Dim SEARCH_SEIKYUUSAKIMOTOTYOU As String = String.Empty
        Dim SEARCH_SIHARAISAKIMOTOTYOU As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUUSAKIMOTOTYOU = "../jhs_earth_dev/SeikyuuSakiMototyou.aspx"
            SEARCH_SIHARAISAKIMOTOTYOU = "../jhs_earth_dev/SiharaiSakiMototyou.aspx"
        Else
            SEARCH_SEIKYUUSAKIMOTOTYOU = "../jhs_earth/SeikyuuSakiMototyou.aspx"
            SEARCH_SIHARAISAKIMOTOTYOU = "../jhs_earth/SiharaiSakiMototyou.aspx"
        End If

        '���̓`�F�b�N
        Dim strErr As String = ""
        Dim strID As String = ""
        Dim strDisplayName As String = ""
        strID = InputCheck(strErr)
        '���b�Z�[�W�\��
        If strErr <> String.Empty Then
            '2015/03/04 ���h�m(��A���V�X�e����)�@�C��  Start
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');document.getElementById('" & strID & "').focus();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & strErr & "');if(objEBI('" & Me.hidSeikyuuStatus.ClientID & "').value!='none'){document.getElementById('" & strID & "').focus()};", True)
            '2015/03/04 ���h�m(��A���V�X�e����)�@�C��  End
        Else
            Select Case Me.ddlUriageData.SelectedValue
                Case "4"
                    '�����挳��
                    Dim strFlg As String = "window.open('" & SEARCH_SEIKYUUSAKIMOTOTYOU & "?seiKbn=" & Me.ddlSeikyuuSaki.SelectedValue & "&seiCd=" & Me.tbxSeikyuuSakiCd.Text.Trim & "&seiBrc=" & Me.tbxSeikyuuSakiBrc.Text.Trim & "&fromdate=" & Me.tbxFrom.Text.Trim & "&todate=" & Me.tbxTo.Text.Trim & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strFlg, True)
                Case "5"
                    '�x���挳��
                    Dim strFlg As String = "window.open('" & SEARCH_SIHARAISAKIMOTOTYOU & "?shrCd=" & Me.tbxSiharaiSakiCd.Text.Trim & "&fromdate=" & Me.tbxFrom.Text.Trim & "&todate=" & Me.tbxTo.Text.Trim & "');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", strFlg, True)
            End Select
        End If

    End Sub

    ''' <summary>
    ''' ������}�X�^�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2010/07/12 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnSeikyuuSakiSyuturyoku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeikyuuSakiSyuturyoku.Click
        '�f�[�^���擾
        Dim m_seikyuu_sakiCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableDataTable
        m_seikyuu_sakiCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Getm_seikyuu_sakiCSV()

        If m_seikyuu_sakiCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("SeikyuuSakiMstCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conM_seikyuu_sakiCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableRow In m_seikyuu_sakiCSVTableDataTable
                writer.WriteLine(row.tokuisaki_cd, row.seikyuu_saki_mei, row.seikyuu_saki_mei2, row.senpou_tantou_mei, row.seikyuusyo_hittyk_date, _
                                 row.master_kbn, row.seikyuu_saki_cd, row.jiltuseki_kanri, row.skysy_soufu_jyuusyo1, row.skysy_soufu_jyuusyo2, _
                                 row.skysy_soufu_yuubin_no.ToString, row.skysy_soufu_tel_no, row.nyuukin_kouza_no, row.tokuisaki_kbn1, row.tokuisaki_kbn2, _
                                 row.tokuisaki_kbn3, row.baika_no, row.kakeritu, row.zeikanzan, row.syutantou_cd, _
                                 row.seikyuu_sime_date, row.syouhizei_hasuu, row.syouhizei_tuuti, row.kaisyuu1_syubetu1, row.kaisyuu_kyoukaigaku, _
                                 row.kaisyuu2_syubetu1, SetKaisyuuYoteiDate(row.kaisyuu_yotei_gessuu, row.kaisyuu_yotei_date), row.kaisyuuhouhou, row.yusin_gendogaku, row.kurikosi_zandaka, _
                                 row.nohinsyo_yousi, row.nohinsyu_syamei, row.kaisyuu1_seikyuusyo_yousi, row.seikyuusyo_syamei, row.kankoutyou_kbn, _
                                 row.keisyou, row.syaten_cd, row.torihikisaki_cd)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If

    End Sub

    ''' <summary>
    ''' ����\���
    ''' </summary>
    ''' <param name="strGessuu">����\�茎��</param>
    ''' <param name="strDate">����\���</param>
    ''' <returns>����\���</returns>
    ''' <remarks></remarks>
    ''' <history>2010/08/02 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Function SetKaisyuuYoteiDate(ByVal strGessuu As String, ByVal strDate As String) As String
        Dim KaisyuuYoteiDate As String
        If strDate.ToString.Trim.Length = 1 Then
            KaisyuuYoteiDate = strGessuu.ToString & "0" & strDate.ToString
        ElseIf strDate.ToString.Trim = "" Then
            KaisyuuYoteiDate = strGessuu.ToString.Trim
        Else
            KaisyuuYoteiDate = strGessuu.ToString.Trim & strDate.ToString.Trim
        End If
        Return KaisyuuYoteiDate
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnTyousaKaisyaSyuturyoku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTyousaKaisyaSyuturyoku.Click
        '�f�[�^���擾
        Dim m_tyousakaisyaCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableDataTable
        m_tyousakaisyaCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Getm_tyousakaisyaCSV()

        If m_tyousakaisyaCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("TyousakaisyaMstCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conM_tyousakaisyaCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableRow In m_tyousakaisyaCSVTableDataTable

                writer.WriteLine(row.tys_kaisya_cd, row.jigyousyo_cd, row.torikesi, row.tys_kaisya_mei, row.tys_kaisya_mei_kana, _
                                 row.seikyuu_saki_shri_saki_mei, row.seikyuu_saki_shri_saki_kana, row.jyuusyo1, row.jyuusyo2, row.yuubin_no, _
                                 row.tel_no, row.fax_no, row.pca_siiresaki_cd, row.pca_seikyuu_cd, row.seikyuu_saki_cd, _
                                 row.seikyuu_saki_brc, row.seikyuu_saki_kbn, row.seikyuu_sime_date, row.skysy_soufu_jyuusyo1, row.skysy_soufu_jyuusyo2, _
                                 row.skysy_soufu_yuubin_no, row.skysy_soufu_tel_no, row.skk_shri_saki_cd, row.skk_jigyousyo_cd, row.shri_meisai_jigyousyo_cd, _
                                 row.shri_jigyousyo_cd, row.shri_sime_date, row.shri_yotei_gessuu, SetNengetu(row.fctring_kaisi_nengetu), row.shri_you_fax_no, _
                                 row.ss_kijyun_kkk, row.fc_ten_cd, row.kensa_center_cd, row.koj_hkks_tyokusou_flg, row.koj_hkks_tyokusou_upd_login_user_id, _
                                 row.koj_hkks_tyokusou_upd_datetime, row.tys_kaisya_flg, row.koj_kaisya_flg, row.japan_kai_kbn, SetNengetu(row.japan_kai_nyuukai_date), _
                                 SetNengetu(row.japan_kai_taikai_date), row.fc_ten_kbn, SetNengetu(row.fc_nyuukai_date), SetNengetu(row.fc_taikai_date), row.torikesi_riyuu, _
                                 row.report_jhs_token_flg, row.tkt_jbn_tys_syunin_skk_flg)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()

        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If

    End Sub

    ''' <summary>
    ''' ���i�}�X�^�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnSyouhinSyuturyoku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSyouhinSyuturyoku.Click
        '�f�[�^���擾
        Dim m_syouhinCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableDataTable
        m_syouhinCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Getm_syouhinCSV()

        If m_syouhinCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("SyouhinMstCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conM_syouhinCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableRow In m_syouhinCSVTableDataTable
                writer.WriteLine(row.syouhin_cd, Replace(row.syouhin_mei, ",", "�C"), row.sisutemu_kbn, row.master_kbn, row.zaikokanri, _
                                 row.jiltusekikanri, row.tani, row.ikazu, row.shri_you_syouhin_mei, row.iru, _
                                 row.saizu, row.syouhin_kbn1, row.syouhin_kbn2, row.syouhin_kbn3, row.zei_kbn, _
                                 row.zeikomi_kbn, row.tanka, row.suuryou, row.hyoujun_kkk, row.genka, _
                                 row.haika1, row.haika2, row.haika3, row.haika4, row.haika5, _
                                 row.souko_cd, row.syusiresaki_cd, row.zaikotanka, row.siire_kkk)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()

        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' ��s�}�X�^�o��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <history>2010/07/14 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnGinkouSyuturyoku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGinkouSyuturyoku.Click
        '�f�[�^���擾
        Dim m_ginkouCSVTableDataTable As New KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableDataTable
        m_ginkouCSVTableDataTable = KakusyuDataSyuturyokuMenuBL.Getm_ginkouCSV()

        If m_ginkouCSVTableDataTable.Rows.Count > 0 Then
            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("GinkouMstCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            'writer.WriteLine(EarthConst.conM_ginkouCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableRow In m_ginkouCSVTableDataTable
                writer.WriteLine(row.ginkou_cd, row.ginkou_mei, row.siten_cd, row.siten_mei, row.saisin_flg)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()

        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub


    ''' <summary>
    ''' DDL�̏����ݒ�
    ''' </summary>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub SetDdlListInf()
        '���h���b�v�_�E�����X�g�ݒ聫
        Dim ddlist As ListItem

        '����f�[�^ddlUriageData
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "����f�[�^"
        ddlist.Value = "0"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�d���f�[�^"
        ddlist.Value = "1"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "���|���c���\"
        ddlist.Value = "2"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "���|���c���\"
        ddlist.Value = "3"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�����挳��"
        ddlist.Value = "4"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "�x���挳��"
        ddlist.Value = "5"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "Excel�d��_����"
        ddlist.Value = "6"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "Excel�d��_�d��"
        ddlist.Value = "7"
        ddlUriageData.Items.Add(ddlist)
        ddlist = New ListItem
        ddlist.Text = "Excel�d��_����"
        ddlist.Value = "8"
        ddlUriageData.Items.Add(ddlist)

        '������敪ddlSeikyuuSaki
        ddlist = New ListItem
        ddlist.Text = ""
        ddlist.Value = ""
        ddlSeikyuuSaki.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        ddlSeikyuuSaki1.Items.Add(ddlist)
        ddlSeikyuuSaki2.Items.Add(ddlist)
        ddlSeikyuuSaki3.Items.Add(ddlist)
        ddlSeikyuuSaki4.Items.Add(ddlist)
        ddlSeikyuuSaki5.Items.Add(ddlist)
        ddlSeikyuuSaki6.Items.Add(ddlist)
        ddlSeikyuuSaki7.Items.Add(ddlist)
        ddlSeikyuuSaki8.Items.Add(ddlist)
        ddlSeikyuuSaki9.Items.Add(ddlist)
        ddlSeikyuuSaki10.Items.Add(ddlist)
        ddlSeikyuuSaki11.Items.Add(ddlist)
        ddlSeikyuuSaki12.Items.Add(ddlist)
        ddlSeikyuuSaki13.Items.Add(ddlist)
        ddlSeikyuuSaki14.Items.Add(ddlist)
        ddlSeikyuuSaki15.Items.Add(ddlist)
        ddlSeikyuuSaki16.Items.Add(ddlist)
        ddlSeikyuuSaki17.Items.Add(ddlist)
        ddlSeikyuuSaki18.Items.Add(ddlist)
        ddlSeikyuuSaki19.Items.Add(ddlist)
        ddlSeikyuuSaki20.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        ddlist = New ListItem
        ddlist.Text = "�����X"
        ddlist.Value = "0"
        ddlSeikyuuSaki.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        ddlSeikyuuSaki1.Items.Add(ddlist)
        ddlSeikyuuSaki2.Items.Add(ddlist)
        ddlSeikyuuSaki3.Items.Add(ddlist)
        ddlSeikyuuSaki4.Items.Add(ddlist)
        ddlSeikyuuSaki5.Items.Add(ddlist)
        ddlSeikyuuSaki6.Items.Add(ddlist)
        ddlSeikyuuSaki7.Items.Add(ddlist)
        ddlSeikyuuSaki8.Items.Add(ddlist)
        ddlSeikyuuSaki9.Items.Add(ddlist)
        ddlSeikyuuSaki10.Items.Add(ddlist)
        ddlSeikyuuSaki11.Items.Add(ddlist)
        ddlSeikyuuSaki12.Items.Add(ddlist)
        ddlSeikyuuSaki13.Items.Add(ddlist)
        ddlSeikyuuSaki14.Items.Add(ddlist)
        ddlSeikyuuSaki15.Items.Add(ddlist)
        ddlSeikyuuSaki16.Items.Add(ddlist)
        ddlSeikyuuSaki17.Items.Add(ddlist)
        ddlSeikyuuSaki18.Items.Add(ddlist)
        ddlSeikyuuSaki19.Items.Add(ddlist)
        ddlSeikyuuSaki20.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        ddlist = New ListItem
        ddlist.Text = "�������"
        ddlist.Value = "1"
        ddlSeikyuuSaki.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        ddlSeikyuuSaki1.Items.Add(ddlist)
        ddlSeikyuuSaki2.Items.Add(ddlist)
        ddlSeikyuuSaki3.Items.Add(ddlist)
        ddlSeikyuuSaki4.Items.Add(ddlist)
        ddlSeikyuuSaki5.Items.Add(ddlist)
        ddlSeikyuuSaki6.Items.Add(ddlist)
        ddlSeikyuuSaki7.Items.Add(ddlist)
        ddlSeikyuuSaki8.Items.Add(ddlist)
        ddlSeikyuuSaki9.Items.Add(ddlist)
        ddlSeikyuuSaki10.Items.Add(ddlist)
        ddlSeikyuuSaki11.Items.Add(ddlist)
        ddlSeikyuuSaki12.Items.Add(ddlist)
        ddlSeikyuuSaki13.Items.Add(ddlist)
        ddlSeikyuuSaki14.Items.Add(ddlist)
        ddlSeikyuuSaki15.Items.Add(ddlist)
        ddlSeikyuuSaki16.Items.Add(ddlist)
        ddlSeikyuuSaki17.Items.Add(ddlist)
        ddlSeikyuuSaki18.Items.Add(ddlist)
        ddlSeikyuuSaki19.Items.Add(ddlist)
        ddlSeikyuuSaki20.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
        ddlist = New ListItem
        ddlist.Text = "�c�Ə�"
        ddlist.Value = "2"
        ddlSeikyuuSaki.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  Start
        ddlSeikyuuSaki1.Items.Add(ddlist)
        ddlSeikyuuSaki2.Items.Add(ddlist)
        ddlSeikyuuSaki3.Items.Add(ddlist)
        ddlSeikyuuSaki4.Items.Add(ddlist)
        ddlSeikyuuSaki5.Items.Add(ddlist)
        ddlSeikyuuSaki6.Items.Add(ddlist)
        ddlSeikyuuSaki7.Items.Add(ddlist)
        ddlSeikyuuSaki8.Items.Add(ddlist)
        ddlSeikyuuSaki9.Items.Add(ddlist)
        ddlSeikyuuSaki10.Items.Add(ddlist)
        ddlSeikyuuSaki11.Items.Add(ddlist)
        ddlSeikyuuSaki12.Items.Add(ddlist)
        ddlSeikyuuSaki13.Items.Add(ddlist)
        ddlSeikyuuSaki14.Items.Add(ddlist)
        ddlSeikyuuSaki15.Items.Add(ddlist)
        ddlSeikyuuSaki16.Items.Add(ddlist)
        ddlSeikyuuSaki17.Items.Add(ddlist)
        ddlSeikyuuSaki18.Items.Add(ddlist)
        ddlSeikyuuSaki19.Items.Add(ddlist)
        ddlSeikyuuSaki20.Items.Add(ddlist)
        '2015/03/02 ���h�m(��A���V�X�e����)�@�ǉ�  End
    End Sub

    ''' <summary>
    ''' ���̓`�F�b�N
    ''' </summary>
    ''' <param name="strErr">�G���[���b�Z�[�W</param>
    ''' <returns></returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Function InputCheck(ByRef strErr As String) As String
        Dim strID As String = ""
        '�f�[�^�I��K�{�`�F�b�N
        If strErr = "" Then
            If ddlUriageData.SelectedValue = "" Then
                strErr = "�o�͂���f�[�^��I�����Ă��������B"
            End If
            If strErr <> "" Then
                strID = ddlUriageData.ClientID
            End If
        End If
        '���o����FROM�K�{�`�F�b�N
        If strErr = "" Then
            If tbxFrom.Text.Trim = "" Then
                strErr = "�o�͂���f�[�^�̊��Ԃ���͂��Ă��������B"
            End If
            If strErr <> "" Then
                strID = tbxFrom.ClientID
            End If
        End If
        '���o����TO�K�{�`�F�b�N
        If strErr = "" Then
            If tbxTo.Text.Trim = "" Then
                strErr = "�o�͂���f�[�^�̊��Ԃ���͂��Ă��������B"
            End If
            If strErr <> "" Then
                strID = tbxTo.ClientID
            End If
        End If
        '���o����FROM-TO�̑召�`�F�b�N
        If strErr = "" Then
            Dim blnDrop As Boolean = False
            Dim strObjId As String = String.Empty
            strErr = CheckDate(tbxFrom, tbxTo, "���o����", strObjId)
            If strErr <> "" Then
                strID = strObjId
            End If
        End If

        '2010/12/22�@�t���@�d�l�ύX�@��
        '2010/3/31�ȑO�̎��`�F�b�N
        If strErr = "" Then
            Dim dtmNendo As DateTime = Convert.ToDateTime("2010/03/31")
            Dim blnDrop As Boolean = False
            Dim strObjId As String = String.Empty
            If DateDiff(DateInterval.Day, Convert.ToDateTime(Me.tbxFrom.Text), dtmNendo) >= 0 Then
                strErr = "EARTH�Ƀf�[�^������̂�2010�N�x�ȍ~�ł��B2009�N�x�ȑO��PCA���Q�Ƃ��Ă��������B"
            End If
            If strErr <> "" Then
                strID = tbxFrom.ClientID
            End If
        End If
        '2010/12/22�@�t���@�d�l�ύX�@��

        '���`�F�b�N
        If strErr = "" Then
            Select Case ddlUriageData.SelectedValue
                Case "4"
                    If Me.tbxSeikyuuSakiMei.Text.Trim = "" Then
                        strErr = "�����挟���{�^�����������A�����於���擾���Ă��������B"
                        strID = Me.btnKensakuSeikyuuSaki.ClientID
                    End If
                    'Case "1"
                    '    If Me.tbxSiireMei.Text.Trim = "" Then
                    '        strErr = "�d���挟���{�^�����������A�d���於���擾���Ă��������B"
                    '        strID = Me.btnKensakuSiire.ClientID
                    '    End If
                Case "5"
                    If Me.tbxSiharaiMei.Text.Trim = "" Then
                        strErr = "�x���挟���{�^�����������A�x���於���擾���Ă��������B"
                        strID = Me.btnKensakuSiharai.ClientID
                    End If
            End Select
        End If

        Return strID

    End Function

    ''' <summary>
    ''' ���t�`�F�b�N_FROM-TO�̑召�`�F�b�N
    ''' </summary>
    ''' <param name="strObjNengetu1"></param>
    ''' <param name="strObjNengetu2"></param>
    ''' <param name="strNengetu"></param>
    ''' <param name="strObjId"></param>
    ''' <returns></returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Function CheckDate(ByVal strObjNengetu1 As TextBox, ByVal strObjNengetu2 As TextBox, ByVal strNengetu As String, ByRef strObjId As String) As String
        Dim csScript As New StringBuilder
        With csScript
            '�o�^�N��(From)
            If strObjNengetu1.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(strObjNengetu1.Text, strNengetu & "(From)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu1.ClientID
                End If
            End If
            '�o�^�N��(To)
            If strObjNengetu2.Text <> String.Empty Then
                .Append(commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)"))
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu2.ClientID
                End If
            End If
            '�o�^�N���͈�
            If strObjNengetu1.Text <> String.Empty And strObjNengetu2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(strObjNengetu1.Text, strNengetu & "(From)") = String.Empty _
                   And commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)") = String.Empty Then
                    '.Append(commonCheck.CheckHidukeHani(strObjNengetu1.Text, strObjNengetu2.Text, strNengetu))
                    If commonCheck.CheckHidukeHani(strObjNengetu1.Text, strObjNengetu2.Text, strNengetu) <> String.Empty Then
                        .Append("���o���Ԃ̑召���s�K�؂ł��B")
                    End If
                    If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                        strObjId = strObjNengetu1.ClientID
                    End If
                End If
            End If
            If strObjNengetu1.Text = String.Empty And strObjNengetu2.Text <> String.Empty Then
                If commonCheck.CheckYuukouHiduke(strObjNengetu2.Text, strNengetu & "(To)") = String.Empty Then
                    .Append(String.Format(Messages.Instance.MSG2012E, strNengetu, strNengetu).ToString)
                End If
                If csScript.ToString <> String.Empty And strObjId = String.Empty Then
                    strObjId = strObjNengetu1.ClientID
                End If
            End If
        End With
        Return csScript.ToString
    End Function

    ''' <summary>
    ''' �����挟���{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd.Text.Trim, Me.tbxSeikyuuSakiBrc.Text.Trim, Me.ddlSeikyuuSaki.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������1�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki1.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd1.Text.Trim, Me.tbxSeikyuuSakiBrc1.Text.Trim, Me.ddlSeikyuuSaki1.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki1.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd1.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc1.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei1.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei1.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki1.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd1.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc1.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki1.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd1.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc1.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki1.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei1.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki1.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������2�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki2.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd2.Text.Trim, Me.tbxSeikyuuSakiBrc2.Text.Trim, Me.ddlSeikyuuSaki2.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki2.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd2.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc2.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei2.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei2.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki2.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd2.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc2.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki2.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd2.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc2.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki2.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei2.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki2.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������3�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki3.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd3.Text.Trim, Me.tbxSeikyuuSakiBrc3.Text.Trim, Me.ddlSeikyuuSaki3.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki3.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd3.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc3.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei3.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei3.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki3.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd3.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc3.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki3.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd3.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc3.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki3.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei3.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki3.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������4�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki4.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd4.Text.Trim, Me.tbxSeikyuuSakiBrc4.Text.Trim, Me.ddlSeikyuuSaki4.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki4.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd4.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc4.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei4.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei4.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki4.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd4.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc4.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki4.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd4.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc4.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki4.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei4.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki4.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������5�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki5.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd5.Text.Trim, Me.tbxSeikyuuSakiBrc5.Text.Trim, Me.ddlSeikyuuSaki5.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki5.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd5.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc5.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei5.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei5.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki5.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd5.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc5.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki5.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd5.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc5.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki5.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei5.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki5.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������6�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki6.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd6.Text.Trim, Me.tbxSeikyuuSakiBrc6.Text.Trim, Me.ddlSeikyuuSaki6.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki6.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd6.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc6.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei6.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei6.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki6.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd6.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc6.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki6.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd6.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc6.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki6.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei6.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki6.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������7�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki7.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd7.Text.Trim, Me.tbxSeikyuuSakiBrc7.Text.Trim, Me.ddlSeikyuuSaki7.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki7.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd7.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc7.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei7.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei7.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki7.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd7.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc7.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki7.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd7.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc7.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki7.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei7.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki7.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������8�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki8.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd8.Text.Trim, Me.tbxSeikyuuSakiBrc8.Text.Trim, Me.ddlSeikyuuSaki8.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki8.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd8.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc8.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei8.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei8.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki8.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd8.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc8.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki8.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd8.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc8.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki8.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei8.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki8.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������9�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki9.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd9.Text.Trim, Me.tbxSeikyuuSakiBrc9.Text.Trim, Me.ddlSeikyuuSaki9.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki9.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd9.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc9.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei9.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei9.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki9.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd9.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc9.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki9.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd9.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc9.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki9.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei9.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki9.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������10�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki10.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd10.Text.Trim, Me.tbxSeikyuuSakiBrc10.Text.Trim, Me.ddlSeikyuuSaki10.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki10.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd10.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc10.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei10.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei10.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki10.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd10.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc10.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki10.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd10.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc10.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki10.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei10.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki10.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������11�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki11.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd11.Text.Trim, Me.tbxSeikyuuSakiBrc11.Text.Trim, Me.ddlSeikyuuSaki11.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki11.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd11.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc11.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei11.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei11.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki11.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd11.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc11.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki11.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd11.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc11.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki11.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei11.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki11.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������12�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki12.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd12.Text.Trim, Me.tbxSeikyuuSakiBrc12.Text.Trim, Me.ddlSeikyuuSaki12.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki12.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd12.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc12.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei12.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei12.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki12.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd12.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc12.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki12.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd12.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc12.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki12.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei12.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki12.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������13�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki13_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki13.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd13.Text.Trim, Me.tbxSeikyuuSakiBrc13.Text.Trim, Me.ddlSeikyuuSaki13.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki13.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd13.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc13.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei13.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei13.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki13.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd13.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc13.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki13.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd13.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc13.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki13.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei13.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki13.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������14�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki14.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd14.Text.Trim, Me.tbxSeikyuuSakiBrc14.Text.Trim, Me.ddlSeikyuuSaki14.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki14.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd14.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc14.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei14.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei14.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki14.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd14.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc14.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki14.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd14.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc14.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki14.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei14.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki14.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������15�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki15_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki15.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd15.Text.Trim, Me.tbxSeikyuuSakiBrc15.Text.Trim, Me.ddlSeikyuuSaki15.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki15.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd15.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc15.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei15.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei15.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki15.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd15.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc15.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki15.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd15.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc15.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki15.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei15.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki15.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������16�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki16_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki16.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd16.Text.Trim, Me.tbxSeikyuuSakiBrc16.Text.Trim, Me.ddlSeikyuuSaki16.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki16.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd16.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc16.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei16.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei16.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki16.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd16.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc16.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki16.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd16.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc16.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki16.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei16.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki16.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������17�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki17_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki17.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd17.Text.Trim, Me.tbxSeikyuuSakiBrc17.Text.Trim, Me.ddlSeikyuuSaki17.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki17.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd17.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc17.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei17.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei17.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki17.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd17.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc17.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki17.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd17.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc17.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki17.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei17.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki17.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������18�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki18_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki18.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd18.Text.Trim, Me.tbxSeikyuuSakiBrc18.Text.Trim, Me.ddlSeikyuuSaki18.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki18.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd18.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc18.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei18.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei18.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki18.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd18.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc18.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki18.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd18.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc18.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki18.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei18.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki18.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������19�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki19_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki19.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd19.Text.Trim, Me.tbxSeikyuuSakiBrc19.Text.Trim, Me.ddlSeikyuuSaki19.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki19.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd19.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc19.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei19.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei19.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki19.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd19.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc19.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki19.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd19.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc19.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki19.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei19.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki19.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ������20�����{�^�����N���b�N��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2015/03/03 ���h�m(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSeikyuuSaki20_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSeikyuuSaki20.Click
        Dim SEARCH_SEIKYUU_SAKI As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_SEIKYUU_SAKI = "../jhs_earth_dev/SearchSeikyuuSaki.aspx"
        Else
            SEARCH_SEIKYUU_SAKI = "../jhs_earth/SearchSeikyuuSaki.aspx"
        End If

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(Me.tbxSeikyuuSakiCd20.Text.Trim, Me.tbxSeikyuuSakiBrc20.Text.Trim, Me.ddlSeikyuuSaki20.SelectedValue, String.Empty, String.Empty, 1, 1, 10, True)

        If dtTable.Rows.Count = 1 Then
            Me.ddlSeikyuuSaki20.SelectedValue = dtTable.Rows(0).Item("seikyuu_saki_kbn").ToString
            Me.tbxSeikyuuSakiCd20.Text = dtTable.Rows(0).Item("seikyuu_saki_cd").ToString
            Me.tbxSeikyuuSakiBrc20.Text = dtTable.Rows(0).Item("seikyuu_saki_brc").ToString
            Me.tbxSeikyuuSakiMei20.Text = dtTable.Rows(0).Item("seikyuu_saki_mei").ToString
        Else
            Me.tbxSeikyuuSakiMei20.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & Me.btnKensakuSeikyuuSaki20.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSeikyuuSakiCd20.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc20.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki20.ClientID & "','" _
                                        & SEARCH_SEIKYUU_SAKI & "','" _
                                        & Me.tbxSeikyuuSakiCd20.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiBrc20.ClientID & SEP_STRING & _
                                            Me.ddlSeikyuuSaki20.ClientID & SEP_STRING & _
                                            Me.tbxSeikyuuSakiMei20.ClientID & "','" _
                                        & Me.btnKensakuSeikyuuSaki20.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �d����_����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSiire_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSiire.Click

        Dim SEARCH_TYOUSAKAISYA As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_TYOUSAKAISYA = "../jhs_earth_dev/SearchTyousakaisya.aspx"
        Else
            SEARCH_TYOUSAKAISYA = "../jhs_earth/SearchTyousakaisya.aspx"
        End If

        Me.HiddenTysKensakuType.Value = "1"

        Dim tmpScript As String = String.Empty

        Dim dtTable As New DataTable
        dtTable = KakusyuDataSyuturyokuMenuBL.GetTyousakaisyaSearchResult(Me.tbxSiireCd.Text, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        True, _
                                                                        String.Empty, _
                                                                        CInt("1"))
        If dtTable.Rows.Count = 1 Then
            Me.tbxSiireCd.Text = dtTable.Rows(0).Item("tys_kaisya_cd").ToString & dtTable.Rows(0).Item("jigyousyo_cd").ToString
            Me.tbxSiireMei.Text = dtTable.Rows(0).Item("tys_kaisya_mei").ToString

            '�t�H�[�J�X�Z�b�g
            SetFocus(Me.btnKensakuSiire)

        Else
            '������Ж����N���A
            Me.tbxSiireMei.Text = String.Empty
            Dim tmpFocusScript = "objEBI('" & btnKensakuSiire.ClientID & "').focus();"
            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & Me.tbxSiireCd.ClientID & SEP_STRING & _
                                         Me.tbxSiireBrc.ClientID & SEP_STRING & _
                                         Me.HiddenTysKensakuType.ClientID & "','" _
                                       & SEARCH_TYOUSAKAISYA & "','" _
                                       & Me.tbxSiireCd.ClientID & SEP_STRING & _
                                         Me.tbxSiireMei.ClientID & "','" _
                                       & Me.btnKensakuSiire.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' �x���挟��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub btnKensakuSiharai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKensakuSiharai.Click

        Dim SEARCH_TYOUSAKAISYA As String = String.Empty
        If Request.Url.OriginalString.IndexOf("jhs_earth2_dev") >= 0 Then
            SEARCH_TYOUSAKAISYA = "../jhs_earth_dev/SearchTyousakaisya.aspx"
        Else
            SEARCH_TYOUSAKAISYA = "../jhs_earth/SearchTyousakaisya.aspx"
        End If

        Me.HiddenTysKensakuType1.Value = "2"

        Dim tmpScript As String = String.Empty

        Dim dtTable As New DataTable
        '�d���挟�������s
        dtTable = KakusyuDataSyuturyokuMenuBL.GetTyousakaisyaSearchResult(Me.tbxSiharaiSakiCd.Text, _
                                                               String.Empty, _
                                                               String.Empty, _
                                                               String.Empty, _
                                                               True, _
                                                               tbxSiharaiSakiCd.Text, _
                                                               "2")

        If dtTable.Rows.Count = 1 Then
            Me.tbxSiharaiSakiCd.Text = dtTable.Rows(0).Item("tys_kaisya_cd").ToString & dtTable.Rows(0).Item("jigyousyo_cd").ToString
            Me.tbxSiharaiMei.Text = dtTable.Rows(0).Item("seikyuu_saki_shri_saki_mei").ToString

            '�t�H�[�J�X�Z�b�g
            SetFocus(Me.btnKensakuSiharai)

        Else
            '������Ж����N���A
            Me.tbxSiharaiMei.Text = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & tbxSiharaiSakiCd.ClientID & SEP_STRING & _
                                         HiddenKameitenCd.ClientID & SEP_STRING & _
                                         HiddenTysKensakuType1.ClientID & "','" _
                                       & SEARCH_TYOUSAKAISYA & "','" _
                                       & tbxSiharaiSakiCd.ClientID & SEP_STRING & _
                                         tbxSiharaiMei.ClientID & "','" _
                                       & btnKensakuSiharai.ClientID & "');"
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
        End If

    End Sub

    ''' <summary>
    ''' ���|���c���\��CSV�o��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/20 �Ⓦ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Sub KaikakekinZandakaHyouCSV(ByVal strDateFrom As String, ByVal strDateTo As String)

        '�f�[�^���擾
        Dim KaikakekinZandakaHyouDataTable As New ExcelSiwakeDataSet.KaikakekinZandakaHyouDataTable
        KaikakekinZandakaHyouDataTable = KakusyuDataSyuturyokuMenuBL.GetKaikakekinZandakaHyou(strDateFrom, strDateTo)

        If KaikakekinZandakaHyouDataTable.Rows.Count > 0 Then

            'CSV�t�@�C�����ݒ�
            Dim strFileName As String = System.Configuration.ConfigurationManager.AppSettings("KaikakeDataZanCsv").ToString

            Response.Buffer = True
            Dim writer As New CsvWriter(Me.Response.OutputStream, Encoding.GetEncoding(932), ",", vbCrLf)

            'CSV�t�@�C���w�b�_�ݒ�
            writer.WriteLine(EarthConst.conKaikakekinZandakaHyouCsvHeader)

            'CSV�t�@�C�����e�ݒ�
            For Each row As ExcelSiwakeDataSet.KaikakekinZandakaHyouRow In KaikakekinZandakaHyouDataTable
                writer.WriteLine( _
                        row.datakbn, row.tokuisaki_cd, row.tokuisaki_mei1, row.tokuisaki_mei2, _
                        row.kurikosi_zanndaka, row.furikomi, row.sousai, row.goukei, row.gou_zandaka, _
                        row.siire_gaku, row.sotozei_gaku, row.sai_zandaka)
            Next

            'CSV�t�@�C���_�E�����[�h
            Response.Charset = "shift-jis"
            Response.ContentType = "text/plain"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & System.Web.HttpUtility.UrlEncode(strFileName))
            Response.End()
        Else
            '�f�[�^���������A���b�Z�[�W�\��
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "err", "alert('" & Messages.Instance.MSG034E & "');", True)
        End If
    End Sub

    ''' <summary>
    ''' �N��
    ''' </summary>
    ''' <param name="strNengetu">DateTime</param>
    ''' <returns>�N��</returns>
    ''' <remarks></remarks>
    ''' <history>2010/07/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Function SetNengetu(ByVal strNengetu As String)
        Dim retNengetu As String
        If strNengetu.ToString = "" Then
            retNengetu = ""
        Else
            retNengetu = CDate(strNengetu).ToString("yyyyMM")
        End If
        Return retNengetu
    End Function

    ''' <summary>
    ''' �N/��/��
    ''' </summary>
    ''' <param name="strNengetu">DateTime</param>
    ''' <returns>�N/��/��</returns>
    ''' <remarks></remarks>
    ''' <history>2010/07/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Function SetNengetuniti(ByVal strNengetu As String)
        Dim retNengetu As String
        If strNengetu.ToString = "" Then
            retNengetu = ""
        Else
            retNengetu = CDate(strNengetu).ToString("yyyy/MM/dd")
        End If
        Return retNengetu
    End Function

    ''' <summary>
    ''' �N����
    ''' </summary>
    ''' <param name="strNengetu">DateTime</param>
    ''' <returns>�N����</returns>
    ''' <remarks></remarks>
    ''' <history>2010/07/22 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Private Function SetNengetunitiInt(ByVal strNengetu As String)
        Dim retNengetu As String
        If strNengetu.ToString = "" Then
            retNengetu = ""
        Else
            retNengetu = CDate(strNengetu).ToString("yyyyMMdd")
        End If
        Return retNengetu
    End Function

    '=======================2011/06/07 �ԗ� �ǉ� �J�n��=====================================
    ''' <summary>�ő咷��؂���</summary>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function
    '=======================2011/06/07 �ԗ� �ǉ� �I����=====================================

    ''' <summary>
    ''' ������w��^�C�g���@�����N�ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>2015/03/02 ���h�m �ǉ�</history>
    Protected Sub titleTextRefSet()
        '�����N�ݒ�
        Me.titleText_SeikyuuSaki.HRef = "javascript:changeDisplay('" & Me.tblSeikyuuSakiMeisai.ClientID & "','" & Me.hidSeikyuuStatus.ClientID & "');"
    End Sub

End Class