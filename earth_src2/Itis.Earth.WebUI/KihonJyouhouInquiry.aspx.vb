Partial Public Class KihonJyouhouInquiry
    Inherits System.Web.UI.Page

#Region "ã§í ïœêî"
    Private commonChk As New CommonCheck
    Private kameitencduse As String
    Private kousin As Integer = "1"
#End Region

#Region "âÊñ "
    ''' <summary>
    ''' âÊñ èâä˙âª
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'JAVASCRIPT
        MakeJavaScript()
        MakeJavaScript1()

        If Not IsPostBack Then

            'å†å¿ämîF
            Dim Ninsyou As New BizLogic.Ninsyou
            Context.Items("strFailureMsg") = BizLogic.Messages.Instance.MSG2024E
            If Ninsyou.GetUserID() = "" Then
                Server.Transfer("CommonErr.aspx")
            End If
            Context.Items("strFailureMsg") = BizLogic.Messages.Instance.MSG2020E
            Dim jBn As New Jiban
            Dim user_info As New BizLogic.LoginUserInfo
            jBn.userAuth(user_info)

            Dim commonCheck As New CommonCheck
            Dim data As DataAccess.CommonSearchDataSet.AccountTableDataTable
            'â¡ñøìXÉRÅ[ÉhéÊìæ
            kameitencduse = Request.QueryString("strKameitenCd")
            If user_info Is Nothing Then
                ' Server.Transfer("CommonErr.aspx")
                commonChk.SetURL(Me, Ninsyou.GetUserID())

            Else
                data = commonCheck.CheckKengen(user_info.AccountNo)
                ViewState("Kengen") = data
                'URLãLì¸
                commonChk.SetURL(Me, user_info.AccountNo)

                'çXêVé“éÊìæ
                hidUpdLoginUserId.Value = user_info.LoginUserId

            End If

            'âÊñ ÇÃå†å¿éÊìæ






            'â¡ñøìXèÓïÒéÊìæ
            Dim kameitenTableDataTable As New Itis.Earth.DataAccess.KameitenDataSet.m_kameitenTableDataTable
            Dim kihonJyouhouInquiryLogic As New BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
            kameitenTableDataTable = kihonJyouhouInquiryLogic.GetkameitenInfo(kameitencduse)

            If kameitenTableDataTable.Rows.Count = 0 Then
                Context.Items("strFailureMsg") = BizLogic.Messages.Instance.MSG020E 'Åi"äYìñÉfÅ[É^Ç™Ç†ÇËÇ‹ÇπÇÒÅB"Åj
                Server.Transfer("CommonErr.aspx")
            End If

            'â¡ñøìXäÓñ{èZèäéÊìæ
            Dim kameitenjyushoDataSet As DataAccess.KameitenjyushoDataSet
            kameitenjyushoDataSet = kihonJyouhouInquiryLogic.GetkameitenJyushoInfo(kameitencduse)


            'â¡ñøìXã§í 
            Me.Kyoutuu_jyouhou1.GetItemValue = kameitenTableDataTable
            'â¡ñøìXÇªÇÃëº
            Me.Kameiten_sonota1.KameitenTableDataTable = kameitenTableDataTable

            'â¡ñøìXäÓñ{èZèä
            Me.Kameitenkihon_jyusho1.jyusho = kameitenjyushoDataSet.m_kameiten_jyuusyo
            'èZèäÇP
            Me.Kameitenkihon_jyushoNoPage1.jyusho = kameitenjyushoDataSet.m_kameiten_jyuusyo
            Me.Kameitenkihon_jyushoNoPage2.jyusho = kameitenjyushoDataSet.m_kameiten_jyuusyo
            Me.Kameitenkihon_jyushoNoPage3.jyusho = kameitenjyushoDataSet.m_kameiten_jyuusyo
            Me.Kameitenkihon_jyushoNoPage4.jyusho = kameitenjyushoDataSet.m_kameiten_jyuusyo


            'â¡ñøìXÉRÅ[Éh
            hidKameitenCd.Value = kameitencduse
            'çXêVì˙
            Me.hidKousinHi.Value = kameitenTableDataTable(0).upd_datetime
            hidKousinFlg.Value = kousin
            'ånóÒCD
            If Not kameitenTableDataTable(0).Iskeiretu_cdNull Then
                Me.hidKeiretuCd.Value = kameitenTableDataTable(0).keiretu_cd
            End If
            'ãÊï™
            If Not kameitenTableDataTable(0).IskbnNull Then
                Me.hidKbn.Value = kameitenTableDataTable(0).kbn
            End If

            Me.titleText5.HRef = "javascript:changeDisplay('" & Me.meisaiTbody5.ClientID & "');changeDisplay('" & titleInfobar5.ClientID & "');"

            '--------------------------FROM 2013.03.26óõâFèCê≥----------------------------
            'OrElse kameitenTableDataTable(0).kbn = "C"í«â¡Ç∑ÇÈ
            'âcã∆èäèÓïÒÉRÉsÅ[
            hidKameitenCopyFlg.Value = "false"
            If kameitenTableDataTable(0).kbn = "A" OrElse kameitenTableDataTable(0).kbn = "C" Then
                hidKameitenCopyFlg.Value = "true"
            End If
            '--------------------------TO   2013.03.26óõâFèCê≥----------------------------



        Else

            closecover()
        End If


        'å†å¿ê›íË
        If ViewState("Kengen") Is Nothing Then

            Kyoutuu_jyouhou1.HansokuSinaKenngenn = False
            'ã§í 
            Kyoutuu_jyouhou1.Kenngenn = False
            'äÓñ{
            Kihon_jyouhou1.Kenngenn = False
            'âøäiêøãÅ
            KakakuseikyuJyouhou1.Kenngenn = False
            'éÊà¯èÓïÒÇP
            TorihikiJyouhou1.Kenngenn = False
            TorihikiJyouhou1.KenngennGM = False
            TorihikiJyouhou1.KenngennKR = False
            Me.Kameitenkihon_jyusho1.Kenngenn = False
            Me.Kameitenkihon_jyushoNoPage1.Kenngenn = False
            Me.Kameitenkihon_jyushoNoPage2.Kenngenn = False
            Me.Kameitenkihon_jyushoNoPage3.Kenngenn = False
            Me.Kameitenkihon_jyushoNoPage4.Kenngenn = False
            Kameiten_tourokuryou1.Kenngenn = False
            Me.Kameiten_bikou1.Kenngenn = False
            Me.Kameiten_sonota1.Kenngenn = False

        Else
            Dim kengen As DataAccess.CommonSearchDataSet.AccountTableDataTable
            kengen = CType(ViewState("Kengen"), DataAccess.CommonSearchDataSet.AccountTableDataTable)
            If kengen.Rows.Count = 0 Then
                Kyoutuu_jyouhou1.HansokuSinaKenngenn = False
                'ã§í 
                Kyoutuu_jyouhou1.Kenngenn = False
                'äÓñ{
                Kihon_jyouhou1.Kenngenn = False
                'âøäiêøãÅ
                KakakuseikyuJyouhou1.Kenngenn = False
                'éÊà¯èÓïÒÇP
                TorihikiJyouhou1.Kenngenn = False
                TorihikiJyouhou1.KenngennGM = False
                TorihikiJyouhou1.KenngennKR = False
                Me.Kameitenkihon_jyusho1.Kenngenn = False
                Me.Kameitenkihon_jyushoNoPage1.Kenngenn = False
                Me.Kameitenkihon_jyushoNoPage2.Kenngenn = False
                Me.Kameitenkihon_jyushoNoPage3.Kenngenn = False
                Me.Kameitenkihon_jyushoNoPage4.Kenngenn = False
                Kameiten_tourokuryou1.Kenngenn = False
                Me.Kameiten_bikou1.Kenngenn = False
                Me.Kameiten_sonota1.Kenngenn = False
            Else
                'îÃë£îÑè„å†å¿
                If kengen(0).hansoku_uri_kengen = -1 Then
                    Kyoutuu_jyouhou1.HansokuSinaKenngenn = True
                Else
                    Kyoutuu_jyouhou1.HansokuSinaKenngenn = False
                End If

                'ã§í èÓïÒ
                If kengen(0).eigyou_master_kanri_kengen = -1 Then
                    'ã§í 
                    Kyoutuu_jyouhou1.Kenngenn = True
                    'äÓñ{
                    Kihon_jyouhou1.Kenngenn = True
                    'âøäiêøãÅ
                    KakakuseikyuJyouhou1.Kenngenn = True
                    'éÊà¯èÓïÒ
                    TorihikiJyouhou1.Kenngenn = True
                Else
                    'ã§í 
                    Kyoutuu_jyouhou1.Kenngenn = False
                    'äÓñ{
                    Kihon_jyouhou1.Kenngenn = False
                    'âøäiêøãÅ
                    KakakuseikyuJyouhou1.Kenngenn = False
                    'éÊà¯èÓïÒÇP
                    TorihikiJyouhou1.Kenngenn = False
                End If
                'éÊà¯èÓïÒ(ã∆ñ±)
                If kengen(0).irai_gyoumu_kengen = -1 _
                        OrElse kengen(0).kekka_gyoumu_kengen = -1 _
                        OrElse kengen(0).hosyou_gyoumu_kengen = -1 _
                        OrElse kengen(0).hkks_gyoumu_kengen = -1 _
                        OrElse kengen(0).koj_gyoumu_kengen = -1 _
                                                                                            Then
                    TorihikiJyouhou1.KenngennGM = True
                Else
                    TorihikiJyouhou1.KenngennGM = False
                End If
                ' éÊà¯èÓïÒ(åoóù)
                If kengen(0).keiri_gyoumu_kengen = -1 Then
                    TorihikiJyouhou1.KenngennKR = True
                Else
                    TorihikiJyouhou1.KenngennKR = False
                End If

                'èZèäèÓïÒ
                If kengen(0).eigyou_master_kanri_kengen = -1 Then
                    Me.Kameitenkihon_jyusho1.Kenngenn = True
                    Me.Kameitenkihon_jyushoNoPage1.Kenngenn = True
                    Me.Kameitenkihon_jyushoNoPage2.Kenngenn = True
                    Me.Kameitenkihon_jyushoNoPage3.Kenngenn = True
                    Me.Kameitenkihon_jyushoNoPage4.Kenngenn = True
                Else
                    Me.Kameitenkihon_jyusho1.Kenngenn = False
                    Me.Kameitenkihon_jyushoNoPage1.Kenngenn = False
                    Me.Kameitenkihon_jyushoNoPage2.Kenngenn = False
                    Me.Kameitenkihon_jyushoNoPage3.Kenngenn = False
                    Me.Kameitenkihon_jyushoNoPage4.Kenngenn = False
                End If

                'ìoò^óøèÓïÒ
                If kengen(0).hansoku_uri_kengen = -1 Then
                    Kameiten_tourokuryou1.Kenngenn = True
                Else
                    Kameiten_tourokuryou1.Kenngenn = False
                End If

                'îıçl
                If kengen(0).irai_gyoumu_kengen = -1 _
                        OrElse kengen(0).kekka_gyoumu_kengen = -1 _
                        OrElse kengen(0).hosyou_gyoumu_kengen = -1 _
                        OrElse kengen(0).hkks_gyoumu_kengen = -1 _
                        OrElse kengen(0).koj_gyoumu_kengen = -1 _
                                                                                Then
                    Me.Kameiten_bikou1.Kenngenn = True
                Else
                    Me.Kameiten_bikou1.Kenngenn = False
                End If

                If kengen(0).eigyou_master_kanri_kengen = -1 Then
                    Me.Kameiten_sonota1.Kenngenn = True
                Else
                    Me.Kameiten_sonota1.Kenngenn = False
                End If
            End If
           

        End If


        'POPUPÉ{É^ÉìÇÃJAVASCRIPT
        CType(Kyoutuu_jyouhou1.FindControl("btnEigyousyoCd"), Button).Attributes.Add("onclick", "return fncOpenwindow(1);")
        CType(Kyoutuu_jyouhou1.FindControl("btnKeiretuCd"), Button).Attributes.Add("onclick", "return fncOpenwindow(2);")
        CType(Kyoutuu_jyouhou1.FindControl("btnBirudaNo"), Button).Attributes.Add("onclick", "return fncOpenwindow(3);")
        CType(Kihon_jyouhou1.FindControl("btnKensaku"), Button).Attributes.Add("onclick", "return fncOpenwindow(4);")
        CType(Kihon_jyouhou1.FindControl("btnKyuuKensaku"), Button).Attributes.Add("onclick", "return fncOpenwindow(5);")

        'èZèäNO
        Me.Kameitenkihon_jyushoNoPage1.jyushoNo = 2
        Me.Kameitenkihon_jyushoNoPage2.jyushoNo = 3
        Me.Kameitenkihon_jyushoNoPage3.jyushoNo = 4
        Me.Kameitenkihon_jyushoNoPage4.jyushoNo = 5

        'â¡ñøìXCD
        Me.SinnseiKbn_jyouhou1.GetKameitenCd = hidKameitenCd.Value
        Me.Kyoutuu_jyouhou1.GetKameitenCd = hidKameitenCd.Value
        Me.Kihon_jyouhou1.GetKameitenCd = hidKameitenCd.Value
        Me.Kameitenkihon_jyusho1.kameiten_cd = hidKameitenCd.Value
        Me.Kameitenkihon_jyushoNoPage1.kameiten_cd = hidKameitenCd.Value
        Me.Kameitenkihon_jyushoNoPage2.kameiten_cd = hidKameitenCd.Value
        Me.Kameitenkihon_jyushoNoPage3.kameiten_cd = hidKameitenCd.Value
        Me.Kameitenkihon_jyushoNoPage4.kameiten_cd = hidKameitenCd.Value
        Me.Kameiten_tourokuryou1.kameiten_cd = hidKameitenCd.Value
        Me.Kameiten_tourokuryou1.MiseCode = hidKameitenCd.Value
        Me.Kameiten_bikou1.kameiten_cd = hidKameitenCd.Value
        Me.Kameiten_bikou1.kameiten_cd = hidKameitenCd.Value
        Me.KakakuseikyuJyouhou1.kameiten_cd = hidKameitenCd.Value
        Me.TorihikiJyouhou1.kameiten_cd = hidKameitenCd.Value
        Me.Kameiten_sonota1.kameiten_cd = hidKameitenCd.Value

        'ìoò^é“
        Me.SinnseiKbn_jyouhou1.GetLoginUserId = hidUpdLoginUserId.Value
        Me.Kyoutuu_jyouhou1.GetLoginUserId = hidUpdLoginUserId.Value
        Me.Kihon_jyouhou1.GetLoginUserId = hidUpdLoginUserId.Value
        Me.Kameitenkihon_jyusho1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameitenkihon_jyushoNoPage1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameitenkihon_jyushoNoPage2.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameitenkihon_jyushoNoPage3.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameitenkihon_jyushoNoPage4.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameiten_tourokuryou1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.KakakuseikyuJyouhou1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.TorihikiJyouhou1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameiten_bikou1.Upd_login_user_id = hidUpdLoginUserId.Value
        Me.Kameiten_sonota1.Upd_login_user_id = hidUpdLoginUserId.Value

        'ãÊï™
        Me.Kameiten_tourokuryou1.Kbn = Me.hidKbn.Value
        'ånóÒCD
        Me.Kameiten_tourokuryou1.keiretuCd = Me.hidKeiretuCd.Value

        If hidKameitenCopyFlg.Value = "true" Then
            Me.Kameitenkihon_jyushoNoPage1.Copy_button_flg = True
        Else
            Me.Kameitenkihon_jyushoNoPage1.Copy_button_flg = False
        End If
        Me.Kameitenkihon_jyushoNoPage2.Copy_button_flg = False
        Me.Kameitenkihon_jyushoNoPage3.Copy_button_flg = False
        Me.Kameitenkihon_jyushoNoPage4.Copy_button_flg = False



        CType(Kihon_jyouhou1.FindControl("btnCopy1"), Button).Attributes.Add("onclick", "fncCopy('" & CType(Kihon_jyouhou1.FindControl("tbxSeisikiMei"), TextBox).ClientID & "','" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKameitenMei1"), TextBox).ClientID & "','â¡ñøìXñºÇP','');return false;")
        CType(Kihon_jyouhou1.FindControl("btnCopy2"), Button).Attributes.Add("onclick", "fncCopy('" & CType(Kihon_jyouhou1.FindControl("tbxSeisikiMei"), TextBox).ClientID & "','" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKameitenMei2"), TextBox).ClientID & "','â¡ñøìXñºÇQ','');return false;")
        CType(Kihon_jyouhou1.FindControl("btnTenCopy1"), Button).Attributes.Add("onclick", "fncCopy('" & CType(Kihon_jyouhou1.FindControl("tbxSeisikiKana"), TextBox).ClientID & "','" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKameitenMei1"), TextBox).ClientID & "','â¡ñøìXñºÇP','ÇQ');return false;")

        CType(Kihon_jyouhou1.FindControl("btnTenCopy2"), Button).Attributes.Add("onclick", "fncCopy('" & CType(Kihon_jyouhou1.FindControl("tbxSeisikiKana"), TextBox).ClientID & "','" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKameitenMei2"), TextBox).ClientID & "','â¡ñøìXñºÇQ','ÇQ');return false;")

        CType(KakakuseikyuJyouhou1.FindControl("btnKihonSet"), Button).Attributes.Add("onclick", "objEBI('" & CType(KakakuseikyuJyouhou1.FindControl("hidSeisikiMei"), HiddenField).ClientID & "').value = objEBI('" & CType(Kihon_jyouhou1.FindControl("tbxSeisikiMei"), TextBox).ClientID & "').value;objEBI('" & CType(KakakuseikyuJyouhou1.FindControl("hidKyoutuKameitenMei1"), HiddenField).ClientID & "').value = objEBI('" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKameitenMei1"), TextBox).ClientID & "').value;objEBI('" & CType(KakakuseikyuJyouhou1.FindControl("hidKyoutuEigyousyoCode"), HiddenField).ClientID & "').value = objEBI('" & CType(Kyoutuu_jyouhou1.FindControl("tbxEigyousyoCd"), TextBox).ClientID & "').value;objEBI('" & CType(KakakuseikyuJyouhou1.FindControl("hidKyoutuKubun"), HiddenField).ClientID & "').value = objEBI('" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKubun"), TextBox).ClientID & "').value;")

    End Sub
#End Region

#Region "èàóù"


    ''' <summary>
    ''' â¡ñøìXèZèäïîÅ@çXêV
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetKousinData()

        Dim KihonJyouhouInquiryLogic As New BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
        Dim list As DataAccess.KameitenjyushoDataSet
        list = KihonJyouhouInquiryLogic.GetkameitenJyushoInfo(hidKameitenCd.Value)

        'â¡ñøìXäÓñ{èZèä
        Me.Kameitenkihon_jyusho1.jyusho = list.m_kameiten_jyuusyo

        'èZèäÇP
        Me.Kameitenkihon_jyushoNoPage1.jyusho = list.m_kameiten_jyuusyo
        Me.Kameitenkihon_jyushoNoPage2.jyusho = list.m_kameiten_jyuusyo
        Me.Kameitenkihon_jyushoNoPage3.jyusho = list.m_kameiten_jyuusyo
        Me.Kameitenkihon_jyushoNoPage4.jyusho = list.m_kameiten_jyuusyo

        Kameitenkihon_jyushoNoPage1.PageInit()
        Kameitenkihon_jyushoNoPage2.PageInit()
        Kameitenkihon_jyushoNoPage3.PageInit()
        Kameitenkihon_jyushoNoPage4.PageInit()

        Dim kameitenTableDataTable As DataAccess.KameitenDataSet.m_kameitenTableDataTable
        kameitenTableDataTable = KihonJyouhouInquiryLogic.GetkameitenInfo(hidKameitenCd.Value)

        'â¡ñøìXäÓñ{èZèä
        Me.Kameitenkihon_jyusho1.jyusho = list.m_kameiten_jyuusyo
        Kameitenkihon_jyusho1.PageInit()

    End Sub

    ''' <summary>
    ''' CLOSE Div
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub closecover()
        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "closecover();").ToString()
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "closecover1", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' ÉÅÉbÉZÅ[ÉWï\é¶Å@Focus
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg(ByVal id As String, ByVal msg As String)

        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "alert('" & msg & "');").ToString()

        'ÉtÉHÅ[ÉJÉX
        csScript.AppendFormat("if(document.getElementById('" & id & "').type != ""submit"") document.getElementById('" & id & "').select();")
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "err", _
                                        csScript.ToString, _
                                        True)
    End Sub
    Public Sub ShowMsgFocus(ByVal id As String, ByVal msg As String)

        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "alert('" & msg & "');").ToString()

        'ÉtÉHÅ[ÉJÉX
        csScript.AppendFormat("if(document.getElementById('" & id & "').type != ""submit"") document.getElementById('" & id & "').focus();")
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "err", _
                                        csScript.ToString, _
                                        True)
    End Sub
    ''' <summary>
    ''' ÉÅÉbÉZÅ[ÉWï\é¶Å@Focus
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Sub ShowMsg2(ByVal id As String, ByVal msg As String)

        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "alert('" & msg & "');").ToString()

        'ÉtÉHÅ[ÉJÉX
        csScript.AppendFormat("if(document.getElementById('" & id & "').type != ""submit"") document.getElementById('" & id & "').select();")
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "err2", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' ÉÅÉbÉZÅ[ÉWï\é¶Å@NOÅ@Focus
    ''' </summary>
    ''' <param name="msg">ÉÅÉbÉZÅ[ÉW</param>
    ''' <remarks></remarks>
    Public Sub ShowMsg3(ByVal msg As String)

        Dim csScript As New StringBuilder
        csScript.AppendFormat( _
                                "alert('" & msg & "');").ToString()

        'ÉtÉHÅ[ÉJÉX
        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "err", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' ÉtÉHÅ[ÉJÉX
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetFocus()

        Dim csScript As New StringBuilder

        'ÉtÉHÅ[ÉJÉX
        csScript.AppendFormat("getFocus();")

        ScriptManager.RegisterStartupScript(Me, _
                                        Me.GetType(), _
                                        "getFocus", _
                                        csScript.ToString, _
                                        True)
    End Sub

    ''' <summary>
    ''' îrëºâ¡ñøìX
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Haitakameiten()
        Dim csScript As New StringBuilder
        Dim strErr As String = ""
        strErr = Kyoutuu_jyouhou1.haitaCheck(Me.hidKameitenCd.Value)
        If strErr <> "" Then
            Kyoutuu_jyouhou1.setKousinSya(strErr)
            ShowMsg3(strErr)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' ã§í çXêV
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetKyoutuuKousin()
        Kyoutuu_jyouhou1.setKousinHi(Me.hidKameitenCd.Value)
    End Sub

    ''' <summary>
    ''' JAVASCRIPT
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MakeJavaScript()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "ScriptOpenwindow"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .AppendLine("function fncCopy(strObj1,strObj2,str,str2)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strValue1=eval('document.all.'+strObj1).value;" & vbCrLf)
            .AppendLine("var strValue2=eval('document.all.'+strObj2).value;" & vbCrLf)
            '.AppendLine("alert(strValue1);alert(strValue2);" & vbCrLf)
            .AppendLine("if (strValue2==''){alert(str+'ÇÕãÛîíÇ≈Ç∑ÅB');return false;" & vbCrLf)
            .AppendLine("}else{" & vbCrLf)
            .AppendLine("if (strValue1!=''){" & vbCrLf)
            .AppendLine("   if (confirm('ê≥éÆñºèÃ'+str2+'Çè„èëÇ´ÇµÇ‹Ç∑Ç©ÅH')==true){" & vbCrLf)
            .AppendLine("       eval('document.all.'+strObj1).value=strValue2;" & vbCrLf)
            .AppendLine("   }" & vbCrLf)
            .AppendLine("}else{ eval('document.all.'+strObj1).value=strValue2;}" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("function fncOpenwindowSyubetu(id1,mei1)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='éÌï ';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
            "'+escape(id1)+'" & _
                    "&objMei=" & "'+mei1+'" & _
                    "&strCd='+escape(eval('document.all.'+" & _
                    " id1 +'" & "').value)+'&strMei='+escape(eval('document.all.'+" & _
                     " mei1 " & ").innerText), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            .AppendLine("function fncOpenwindowYuubin(id1,mei1,mei2,mei3)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='óXï÷';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & _
            Me.Page.Form.Name & _
            "&objCd=" & _
            "'+escape(id1)+'" & _
            "&objMei=" & _
            "'+mei1+'" & _
            "&objMei2=" & _
            "'+mei2+'" & _
            "&objMei3=" & _
            "'+mei3+'" & _
            "&strCd='+escape(eval('document.all.'+" & _
            " id1 +'" & "').value)" & _
            "+'&strMei='+escape(eval('document.all.'+" & _
            " mei1 " & ").innerText)" & _
            "+'&strMei2='+escape(eval('document.all.'+" & _
            " mei2 " & ").innerText)" & _
            "+'&strMei3='+escape(eval('document.all.'+" & _
            " mei3 " & ").value)" & _
            ", 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)


            .AppendLine("var objSrchWin;" & vbCrLf)
            .AppendLine("function fncOpenwindowSyouhin(soukoCd)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn='è§ïi';" & vbCrLf)
            .AppendLine("    if(soukoCd==200) {")

            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&soukoCd='+escape(soukoCd)+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
            Me.Kameiten_tourokuryou1.FindControl("tbxSyouhinCd").ClientID & _
                    "&objMei=" & Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei").ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    Me.Kameiten_tourokuryou1.FindControl("tbxSyouhinCd").ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei").ClientID & "').innerText), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)

            .AppendLine("}else{" & vbCrLf)

            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&soukoCd='+escape(soukoCd)+'&FormName=" & _
            Me.Page.Form.Name & "&objCd=" & _
            Me.Kameiten_tourokuryou1.FindControl("tbxSyouhinCd1").ClientID & _
                    "&objMei=" & Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei1").ClientID & _
                    "&strCd='+escape(eval('document.all.'+'" & _
                    Me.Kameiten_tourokuryou1.FindControl("tbxSyouhinCd1").ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & _
                    Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei1").ClientID & "').innerText), 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)


            .AppendLine("}" & vbCrLf)

            .AppendLine("}" & vbCrLf)

            .AppendLine("var comChkOkFlg = false;" & vbCrLf)
            .AppendLine("var syainCdHenkouKbn = false;" & vbCrLf)

            .AppendLine("function LostFocusPostBack(e,index){")

            .AppendLine("	var obj;")
            .AppendLine("	obj = document.getElementById ('" & Me.Kameiten_tourokuryou1.FindControl("hidLastFocus").ClientID & "'); ")

            .AppendLine("    if(index==1|| index==6 || index==5 || index==9) {")
            .AppendLine("    if(!checkDate(e) && e.value.Trim()!='') {")
            .AppendLine("    e.focus();")
            .AppendLine("    return false;")
            .AppendLine("    }")
            .AppendLine("    }")


            .AppendLine("    if(index==2) {")
            .AppendLine("    if(e.value=='') {")
            .AppendLine(" document.getElementById ('" & Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei").ClientID & "').innerText=''; ")

            .AppendLine("    return false;")
            .AppendLine("    }")
            .AppendLine("    }")



            .AppendLine("    if(index==7) {")
            .AppendLine("    if(e.value=='') {")
            .AppendLine(" document.getElementById ('" & Me.Kameiten_tourokuryou1.FindControl("lblSyouhinMei1").ClientID & "').innerText=''; ")
            .AppendLine("    return false;")
            .AppendLine("    }")
            .AppendLine("    }")


            .AppendLine("    if(index==3|| index==4 || index==8 ) {")
            .AppendLine("    var koumoku;")
            .AppendLine("    if (index==3){koumoku='é¿êøãÅê≈î≤âøäi'}")
            .AppendLine("    if (index==4){koumoku='çHñ±ìXêøãÅê≈î≤ã‡äz'}")
            .AppendLine("    if (index==8){koumoku='é¿êøãÅê≈î≤âøäi'}")
            .AppendLine("    if(!gfIsNumeric(e,koumoku) && e.value.Trim()!='') {")
            .AppendLine("    e.focus();")
            .AppendLine("    return false;")
            .AppendLine("    }")
            .AppendLine("    }")



            .AppendLine("    if(syainCdHenkouKbn) {")
            .AppendLine("	syainCdHenkouKbn = false; ")
            .AppendLine("	obj.value = index; ")
            .AppendLine(" ShowModal();")
            .AppendLine("	setTimeout('__doPostBack(\'" & Me.Kameiten_tourokuryou1.FindControl("Button1").ClientID & "\',\'\')', 0)   ")
            .AppendLine("    return false;")
            .AppendLine("    }")

            .AppendLine("    if(objSrchWin!=null) {")

            .AppendLine("    if(objSrchWin.closed==true) {")
            .AppendLine("    objSrchWin=null;")
            .AppendLine("    }")

            .AppendLine("	syainCdHenkouKbn = false; ")
            .AppendLine("	obj.value = index; ")
            .AppendLine(" ShowModal();")
            .AppendLine("	setTimeout('__doPostBack(\'" & Me.Kameiten_tourokuryou1.FindControl("Button1").ClientID & "\',\'\')', 0)   ")

            .AppendLine("    }")


            .AppendLine("    if(((comObj2==e.value && obj.value=='')|| e.value== comObj1) ) {")
            .AppendLine("         e.value= comObj1;")
            .AppendLine("          return false; ")
            .AppendLine("    }")



            .AppendLine("	if(obj.value=='' || obj.value==index){  ")
            .AppendLine("	obj.value = index; ")
            .AppendLine(" ShowModal();")
            .AppendLine("	setTimeout('__doPostBack(\'" & Me.Kameiten_tourokuryou1.FindControl("Button1").ClientID & "\',\'\')', 0)   ")
            .AppendLine("	} else  ")
            .AppendLine("	{ ")

            .AppendLine("	}   ")

            '.Append("	if(e.value==comObj2 && obj.value==index)")
            '.Append("	{")
            '.Append("		e.value=comObj1;")
            '.Append("		return false;")
            '.Append("	}   ")
            '.Append("	obj.value = index; ")
            '.Append("	setTimeout('__doPostBack(\'" & Me.Kameiten_tourokuryou1.FindControl("Button1").ClientID & "\',\'\')', 0)   ")
            .AppendLine("}   ")


            '.AppendLine("function LostFocusPostBackBikou(e){")
            '.AppendLine("document.getElementById('" & Me.Kameiten_bikou1.FindControl("hidActionFlg").ClientID & "').value = 'true'   ")
            '.AppendLine("	setTimeout('__doPostBack(\'" & Me.Kameiten_bikou1.FindControl("TextBoxDisplayNone").ClientID & "\',\'\')', 0)   ")
            '.AppendLine("}   ")


            .AppendLine("function ShowModal()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")

            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("   if(buyDiv.style.display=='none')")
            .AppendLine("   {")
            .AppendLine("    buyDiv.style.display='';")
            .AppendLine("    disable.style.display='';")
            .AppendLine("    disable.focus();")
            .AppendLine("   }else{")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")
            .AppendLine("   }")
            .AppendLine("}")


            .AppendLine("function closecover()")
            .AppendLine("{")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("   var disable=document.getElementById('" & Me.disableDiv.ClientID & "');")
            .AppendLine("    buyDiv.style.display='none';")
            .AppendLine("    disable.style.display='none';")

            .AppendLine("}")



            .AppendLine("function setOnfocusNengetu(e){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("    if(buyDiv.style.display!='none'){")
            .AppendLine("   try{")
            .AppendLine(" if (oldObjFocus!=null)oldObjFocus.focus();")
            .AppendLine("}catch(e){")
            .AppendLine("}")
            .AppendLine(" return false;")
            .AppendLine("}")
            .AppendLine("    oldObjFocus = e;")
            .AppendLine("    comObj1=e.value;")
            .AppendLine("    e.value = e.value.replace('/','').replace('/','');")
            .AppendLine("    comObj2=e.value;")
            .AppendLine("    event.returnValue = false;")
            .AppendLine("    e.select();")
            .AppendLine("    return false;")
            .AppendLine("}")

            .AppendLine("  function SetKingaku(e){")

            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("    if(buyDiv.style.display!='none'){")
            .AppendLine("   try{")
            .AppendLine(" if (oldObjFocus!=null)oldObjFocus.focus();")
            .AppendLine("}catch(e){")
            .AppendLine("}")
            .AppendLine(" return false;")
            .AppendLine("}")

            .AppendLine("	var obj;")
            '.AppendLine("	obj = document.getElementById ('" & Me.Kameiten_tourokuryou1.FindControl("hidLastFocus").ClientID & "'); ")
            '.AppendLine(" if (obj.value==''){return false;}")
            .AppendLine(" if (e.value==''){return false;}")

            .AppendLine("        var value;")
            .AppendLine("        var arr;")
            .AppendLine("        value = e.value;")
            .AppendLine("    comObj1=e.value;")
            .AppendLine("        arr=value.split(',');")
            .AppendLine("        e.value = arr.join('');  ")
            .AppendLine("    comObj2=e.value;")
            .AppendLine("        e.select();")
            .AppendLine("    }")

            .AppendLine("  function GetFocusOperate(e){")
            .AppendLine("   var buyDiv=document.getElementById('" & Me.buySelName.ClientID & "');")
            .AppendLine("    if(buyDiv.style.display!='none'){")
            .AppendLine("   try{")
            .AppendLine(" if (oldObjFocus!=null)oldObjFocus.focus();")
            .AppendLine("}catch(e){")
            .AppendLine("}")
            .AppendLine(" return false;")
            .AppendLine("}")
            .AppendLine("    comObj1=e.value;")
            .AppendLine("    comObj2=e.value;")
            .AppendLine("    oldObjFocus = e;")
            .AppendLine("    event.returnValue = false;")
            .AppendLine("    e.select();")
            .AppendLine("    return false;")
            .AppendLine("}")

            .AppendLine("</script>" & vbCrLf)
        End With

        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)

    End Sub

    ''' <summary>
    ''' JAVASCRIPT
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub MakeJavaScript1()
        Dim csType As Type = Page.GetType()
        Dim csName As String = "setScript"
        Dim csScript As New StringBuilder
        With csScript
            .AppendLine("<script language='javascript' type='text/javascript'>  " & vbCrLf)
            .AppendLine("function fncOpenwindow(intkbn)" & vbCrLf)
            .AppendLine("{" & vbCrLf)
            .AppendLine("var strkbn;" & vbCrLf)
            .AppendLine("if (intkbn==1){" & vbCrLf)
            .AppendLine("strkbn='âcã∆èä';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & CType(Kyoutuu_jyouhou1.FindControl("tbxEigyousyoCd"), TextBox).ClientID & "&objMei=" & CType(Kyoutuu_jyouhou1.FindControl("lblEigyousyoMei"), TextBox).ClientID & "&strCd='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("tbxEigyousyoCd"), TextBox).ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("lblEigyousyoMei"), TextBox).ClientID & "').value)+'&blnDelete=True'+'&btnFcId=" & CType(Kyoutuu_jyouhou1.FindControl("btnFcTyousaKaisya"), Button).ClientID & "', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("if (intkbn==2){" & vbCrLf)
            .AppendLine("strkbn='ånóÒ';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & CType(Kyoutuu_jyouhou1.FindControl("tbxKeiretuCd"), TextBox).ClientID & "&objMei=" & CType(Kyoutuu_jyouhou1.FindControl("lblKeiretuMei"), TextBox).ClientID & "&strCd='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("tbxKeiretuCd"), TextBox).ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("lblKeiretuMei"), TextBox).ClientID & "').value)+'&KensakuKubun=" & CType(Kyoutuu_jyouhou1.FindControl("tbxKyoutuKubun"), TextBox).Text & "&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("if (intkbn==3){" & vbCrLf)
            .AppendLine("strkbn='ÉrÉãÉ_ﬁÅ|';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & CType(Kyoutuu_jyouhou1.FindControl("tbxBirudaNo"), TextBox).ClientID & "&objMei=" & CType(Kyoutuu_jyouhou1.FindControl("lblBirudaMei"), TextBox).ClientID & "&strCd='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("tbxBirudaNo"), TextBox).ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & CType(Kyoutuu_jyouhou1.FindControl("lblBirudaMei"), TextBox).ClientID & "').value)+'&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("if (intkbn==4){" & vbCrLf)
            .AppendLine("strkbn='ÉÜÅ[ÉUÅ[';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & CType(Kihon_jyouhou1.FindControl("tbxEigyouCd"), TextBox).ClientID & "&objMei=" & CType(Kihon_jyouhou1.FindControl("tbxEigyouMei"), TextBox).ClientID & "&strCd='+escape(eval('document.all.'+'" & CType(Kihon_jyouhou1.FindControl("tbxEigyouCd"), TextBox).ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & CType(Kihon_jyouhou1.FindControl("tbxEigyouMei"), TextBox).ClientID & "').value)+'&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("if (intkbn==5){" & vbCrLf)
            .AppendLine("strkbn='ÉÜÅ[ÉUÅ[';" & vbCrLf)
            .AppendLine("objSrchWin = window.open('search_common.aspx?Kbn='+escape(strkbn)+'&FormName=" & Form.Name & "&objCd=" & CType(Kihon_jyouhou1.FindControl("tbxKyuuEigyouCd"), TextBox).ClientID & "&objMei=" & CType(Kihon_jyouhou1.FindControl("tbxKyuuEigyouMei"), TextBox).ClientID & "&strCd='+escape(eval('document.all.'+'" & CType(Kihon_jyouhou1.FindControl("tbxKyuuEigyouCd"), TextBox).ClientID & "').value)+'&strMei='+escape(eval('document.all.'+'" & CType(Kihon_jyouhou1.FindControl("tbxKyuuEigyouMei"), TextBox).ClientID & "').value)+'&blnDelete=True', 'searchWindow', 'menubar=no,toolbar=no,location=no,status=yes,resizable=yes,scrollbars=yes');" & vbCrLf)
            .AppendLine("}" & vbCrLf)
            .AppendLine("return false;" & vbCrLf)
            .AppendLine("}" & vbCrLf)

            .AppendLine("</script>" & vbCrLf)
        End With
        ClientScript.RegisterClientScriptBlock(csType, csName, csScript.ToString)
    End Sub

    ''' <summary>
    ''' çXêVé“çXêV
    ''' </summary>
    ''' <param name="strErr"></param>
    ''' <remarks></remarks>
    Public Sub setKousinSya(ByVal strErr As String)
        Kyoutuu_jyouhou1.setKousinSya(strErr)
    End Sub

    ''' <summary>
    ''' çXêVì˙çXêV
    ''' </summary>
    ''' <param name="strKameiten"></param>
    ''' <remarks></remarks>
    Public Sub setKousinHi(ByVal strKameiten As String)
        Kyoutuu_jyouhou1.setKousinHi(strKameiten)
    End Sub

    ''' <summary>
    ''' îrëºÉ`ÉFÉbÉN
    ''' </summary>
    ''' <param name="strKameiten"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function haitaCheck(ByVal strKameiten As String) As String
        Return Kyoutuu_jyouhou1.haitaCheck(strKameiten)
    End Function

#End Region

End Class