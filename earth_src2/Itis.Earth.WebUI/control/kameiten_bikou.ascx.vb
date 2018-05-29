Imports Itis.Earth.DataAccess
Imports Itis.Earth.BizLogic
Partial Public Class kameiten_bikou
    Inherits System.Web.UI.UserControl


#Region "共通変数"
    Private KihonJyouhouInquiryBc As New Itis.Earth.BizLogic.kihonjyouhou.KihonJyouhouInquiryLogic
    Private CommonCheckFuc As New CommonCheck()
    'メッセージとFOCUS
    Public msgAndFocus As New Itis.Earth.BizLogic.kihonjyouhou.MessageAndFocus
#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 更新者
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _upd_login_user_id As String
    Public Property Upd_login_user_id() As String
        Get
            Return _upd_login_user_id
        End Get

        Set(ByVal value As String)
            _upd_login_user_id = value
        End Set

    End Property

    Public _kameiten_cd As String
    ''' <summary>
    ''' 加盟店コード
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

    '権限
    Private _kenngenn As Boolean
    ''' <summary>
    ''' 権限
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

#End Region

#Region "画面"

    ''' <summary>
    ''' PAGE LOAD
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            meisaiTbody.Style.Item("display") = "none"

        Else
        End If
    End Sub

    ''' <summary>
    ''' 登録ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Touroku_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim btn As Button = CType(sender, Button)
        Dim data As String
        Dim maxDate As String
        Dim index As Integer

        '更新日取得
        data = KihonJyouhouInquiryBc.GetkameitenBikouMaxUpdInfo(_kameiten_cd)
        maxDate = data.Split(",")(0)
        index = btn.Attributes("index")

        'チェック
        Dim msg As String
        msg = CommonCheckFuc.CheckHissuNyuuryoku(CType(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckHankaku(CType(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text, "種別", "1")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckByte(CType(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text, 4, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), msg)
            msg = String.Empty
        End If


        'msg = CommonCheckFuc.CheckHissuNyuuryoku(CType(grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou"), TextBox).Text, "内容")
        'If msg <> String.Empty Then
        '    msgAndFocus.AppendMsgAndCtrl((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), msg)
        '    msg = String.Empty
        'End If

        msg = CommonCheckFuc.CheckByte(CType((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), TextBox).Text, 80, "内容", kbn.ZENKAKU)
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), msg)
            msg = String.Empty
        End If



        If CommonCheckFuc.CheckKinsoku(CType((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), TextBox).Text, "内容") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(CType((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), TextBox), CommonCheckFuc.CheckKinsoku(CType((grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou")), TextBox).Text, "内容"))
        End If

        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Exit Sub
        End If



        '他の端末で更新されたチェック
        If Me.hidMaxDate.Value <> String.Empty AndAlso maxDate <> String.Empty Then
            If Convert.ToDateTime(Me.hidMaxDate.Value).ToString("yyyy/MM/dd HH:mm:ss") < Convert.ToDateTime(maxDate).ToString("yyyy/MM/dd HH:mm:ss") Then
                ShowMsg(Messages.Instance.MSG003E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), data.Split(",")(1), Convert.ToDateTime(data.Split(",")(0)).ToString("yyyy/MM/dd HH:mm:ss"))
                Exit Sub
            End If
        Else
            If maxDate = String.Empty Then
                ShowMsg(Messages.Instance.MSG2009E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), )
                Exit Sub
            End If
        End If


        Dim bikousyubetu As String
        Dim naiyou As String
        Dim oldBikousyubetu As String
        Dim oldNaiyou As String
        Dim oldnyuuryokuNo As String

        oldBikousyubetu = btn.Attributes("bikousyubetu")
        oldNaiyou = btn.Attributes("Naiyou")
        oldnyuuryokuNo = btn.Attributes("nyuuryokuNo")
        bikousyubetu = CType(grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text
        naiyou = CType(grdBeikou.Rows(index).Cells(2).FindControl("tbxNaiyou"), TextBox).Text


        '加盟店備考情報取得
        msg = KihonJyouhouInquiryBc.SelkameitenBikouInfo(_kameiten_cd, oldBikousyubetu, oldnyuuryokuNo)
        If msg = String.Empty Then

            ShowMsg(Messages.Instance.MSG2009E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"))
            Exit Sub
        End If


        '登録データ作成
        Dim insdata As New KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable
        Dim dr As KameitenBikouDataSet.m_kameiten_bikou_setteiRow
        dr = insdata.NewRow
        dr.bikou_syubetu = bikousyubetu
        dr.naiyou = naiyou
        dr.upd_login_user_id = _upd_login_user_id
        insdata.Rows.Add(dr)

        '登録
        If KihonJyouhouInquiryBc.SetBikou(_kameiten_cd, oldBikousyubetu, oldnyuuryokuNo, insdata) Then
            GamenInit()
            ShowMsg(Messages.Instance.MSG018S, Me.TextBoxDisplayNone, "備考")
        Else
            ShowMsg(Messages.Instance.MSG019E, Me.TextBoxDisplayNone, "備考")
        End If

    End Sub

    ''' <summary>
    ''' 削除ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Sakujyo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim btn As Button = CType(sender, Button)
        Dim btn2 As Button
        Dim data As String
        Dim maxDate As String
        Dim index As Integer

        '更新日
        data = KihonJyouhouInquiryBc.GetkameitenBikouMaxUpdInfo(_kameiten_cd)
        maxDate = data.Split(",")(0)
        index = btn.Attributes("index")


        If Me.hidMaxDate.Value <> String.Empty AndAlso maxDate <> String.Empty Then
            If Convert.ToDateTime(Me.hidMaxDate.Value).ToString("yyyy/MM/dd HH:mm:ss") < Convert.ToDateTime(maxDate).ToString("yyyy/MM/dd HH:mm:ss") Then
                ShowMsg(Messages.Instance.MSG003E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), data.Split(",")(1), Convert.ToDateTime((data.Split(",")(0))).ToString("yyyy/MM/dd HH:mm:ss"))
                Exit Sub
            End If
        Else
            If Me.hidMaxDate.Value <> String.Empty AndAlso maxDate = String.Empty Then
                ShowMsg(Messages.Instance.MSG2009E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"), )
                Exit Sub
            End If
        End If

        btn2 = CType(grdBeikou.Rows(index).Cells(3).FindControl("btnTouroku"), Button)


        Dim oldBikousyubetu As String
        Dim oldNaiyou As String
        Dim oldnyuuryokuNo As String

        oldBikousyubetu = btn2.Attributes("bikousyubetu")
        oldNaiyou = btn2.Attributes("Naiyou")
        oldnyuuryokuNo = btn2.Attributes("nyuuryokuNo")

        '加盟店備考情報取得
        Dim msg As String = KihonJyouhouInquiryBc.SelkameitenBikouInfo(_kameiten_cd, oldBikousyubetu, oldnyuuryokuNo)
        If msg = String.Empty Then

            ShowMsg(Messages.Instance.MSG2009E, grdBeikou.Rows(index).Cells(0).FindControl("tbxBikousyubetu"))
            Exit Sub
        End If

        '登録料の削除
        If KihonJyouhouInquiryBc.DelBikou(_kameiten_cd, oldBikousyubetu, oldnyuuryokuNo, _upd_login_user_id) Then

            GamenInit()


            ShowMsg(Messages.Instance.MSG018S, Me.TextBoxDisplayNone, "備考")
        Else
            ShowMsg(Messages.Instance.MSG019E, Me.TextBoxDisplayNone, "備考")
        End If

    End Sub

    ''' <summary>
    ''' 新規ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub Sinki_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'チェック
        Dim msg As String
        msg = CommonCheckFuc.CheckHissuNyuuryoku(CType(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckHankaku(CType(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text, "種別", "1")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckByte(CType(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text, 4, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), msg)
            msg = String.Empty
        End If


        'msg = CommonCheckFuc.CheckHissuNyuuryoku(CType(Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou"), TextBox).Text, "内容")
        'If msg <> String.Empty Then
        '    msgAndFocus.AppendMsgAndCtrl((Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou")), msg)
        '    msg = String.Empty
        'End If

        msg = CommonCheckFuc.CheckByte(CType((Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou")), TextBox).Text, 80, "内容", kbn.ZENKAKU)
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl((Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou")), msg)
            msg = String.Empty
        End If

        If CommonCheckFuc.CheckKinsoku(CType((Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou")), TextBox).Text, "内容") <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(CType(Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou"), TextBox), CommonCheckFuc.CheckKinsoku(CType((Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou")), TextBox).Text, "内容"))
        End If

        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Exit Sub
        End If





        Dim btn As Button = CType(sender, Button)
        Dim insdata As New KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable
        Dim data As String
        Dim maxDate As String

        '更新日
        data = KihonJyouhouInquiryBc.GetkameitenBikouMaxUpdInfo(_kameiten_cd)
        maxDate = data.Split(",")(0)

        '他の端末で更新されたチェック
        If Me.hidMaxDate.Value <> String.Empty AndAlso maxDate <> String.Empty Then
            If Convert.ToDateTime(Me.hidMaxDate.Value).ToString("yyyy/MM/dd HH:mm:ss") < Convert.ToDateTime(maxDate).ToString("yyyy/MM/dd HH:mm:ss") Then
                ShowMsg(Messages.Instance.MSG003E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), data.Split(",")(1), Convert.ToDateTime((data.Split(",")(0))).ToString("yyyy/MM/dd HH:mm:ss"))
                Exit Sub
            End If
        ElseIf Me.hidMaxDate.Value <> String.Empty AndAlso maxDate = String.Empty Then
            ShowMsg(Messages.Instance.MSG2009E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"))
            Exit Sub
        Else
            If Me.hidMaxDate.Value = String.Empty AndAlso maxDate <> String.Empty Then
                ShowMsg(Messages.Instance.MSG003E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), data.Split(",")(1), Convert.ToDateTime((data.Split(",")(0))).ToString("yyyy/MM/dd HH:mm:ss"))
                Exit Sub
            End If

        End If

        'データ作成
        Dim dr As KameitenBikouDataSet.m_kameiten_bikou_setteiRow
        dr = insdata.NewRow
        dr.kameiten_cd = _kameiten_cd
        dr.bikou_syubetu = CType(Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text
        dr.naiyou = CType(Me.grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou"), TextBox).Text
        dr.nyuuryoku_no = grdBeikou.Rows.Count + 1
        dr.upd_login_user_id = _upd_login_user_id
        dr.add_login_user_id = _upd_login_user_id
        insdata.Rows.Add(dr)

        '加盟店備考情報取得
        msg = KihonJyouhouInquiryBc.SelkameitenBikouInfo(_kameiten_cd, dr.bikou_syubetu, dr.nyuuryoku_no)
        If msg <> String.Empty Then
            Dim useDate As String = Convert.ToDateTime(msg.Split(",")(1)).ToString("yyyy/MM/dd HH:mm:ss")
            ShowMsg(Messages.Instance.MSG003E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), msg.Split(",")(0), useDate)
            Exit Sub
        End If


        If KihonJyouhouInquiryBc.SelkameitenBikouInfoCount(_kameiten_cd) < Me.grdBeikou.Rows.Count Then
            ShowMsg(Messages.Instance.MSG2009E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"))
        End If

        If KihonJyouhouInquiryBc.InsBikou(insdata) Then
            '登録料の登録
            GamenInit()
            ShowMsg(Messages.Instance.MSG018S, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), "備考")
        Else
            ShowMsg(Messages.Instance.MSG019E, Me.grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), "備考")
        End If

    End Sub

#End Region

#Region "処理"

    ''' <summary>
    ''' 画面表示
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GamenInit()

        Dim data As New KameitenBikouDataSet.m_kameiten_bikou_setteiDataTable
        data = KihonJyouhouInquiryBc.GetkameitenBikouInfo(_kameiten_cd)

        grdBeikou.DataSource = data
        grdBeikou.DataBind()

        Me.hidMaxDate.Value = KihonJyouhouInquiryBc.GetkameitenBikouMaxUpdInfo(_kameiten_cd).Split(",")(0)


        Dim dtKeyWords2 As New DataTable
        Dim drTemp2 As DataRow

        dtKeyWords2.Columns.Add("syubetu", GetType(String))
        dtKeyWords2.Columns.Add("syubetumei", GetType(String))
        dtKeyWords2.Columns.Add("naiyou", GetType(String))

        drTemp2 = dtKeyWords2.NewRow
        With drTemp2
            .Item("syubetu") = "11"
            .Item("syubetumei") = ""
            .Item("naiyou") = ""
        End With

        dtKeyWords2.Rows.Add(drTemp2)
        grdAddBeikou.DataSource = dtKeyWords2
        Me.grdAddBeikou.DataBind()

    End Sub

    ''' <summary>
    ''' 画面クリア　画面SPEED　UP
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GamenInitClear()


        grdBeikou.DataSource = Nothing
        grdBeikou.DataBind()

        grdAddBeikou.DataSource = Nothing
        Me.grdAddBeikou.DataBind()

    End Sub

    ''' <summary>
    ''' 備考種別変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxBikousyubetu_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim i As Integer
        Dim obj As TextBox
        obj = CType(sender, TextBox)
        i = obj.Attributes("index")
        Dim text As String

        Dim bikousyubetu As Integer
        text = CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text



        Dim msg As String
        msg = CommonCheckFuc.CheckHissuNyuuryoku(text, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckHankaku(text, "種別", "1")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckByte(text, 4, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text = String.Empty
            CType(grdBeikou.Rows(i).Cells(1).FindControl("lblSyubetumei"), Label).Text = String.Empty
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Exit Sub
        End If


        bikousyubetu = Convert.ToInt32(text)

        Dim bikousyubetuMei As String

        bikousyubetuMei = KihonJyouhouInquiryBc.Getkameitensyubetu(bikousyubetu)

        If bikousyubetuMei Is Nothing Then
            CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text = String.Empty
            CType(grdBeikou.Rows(i).Cells(1).FindControl("lblSyubetumei"), Label).Text = String.Empty
            ShowMsg(Messages.Instance.MSG2008E, obj, "種別")

            msgAndFocus.setFocus(Me.Page, obj)
            Exit Sub
        Else
            CType(grdBeikou.Rows(i).Cells(1).FindControl("lblSyubetumei"), Label).Text = bikousyubetuMei
        End If

        msgAndFocus.setFocus(Me.Page, CType(grdBeikou.Rows(i).Cells(2).FindControl("tbxNaiyou"), TextBox))




    End Sub

    ''' <summary>
    ''' 備考種別変更時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxBikousyubetu_TextChanged2(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim obj As TextBox
        obj = CType(sender, TextBox)

        Dim text As String

        Dim bikousyubetu As Integer
        text = CType(grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text
     
        Dim msg As String
        msg = CommonCheckFuc.CheckHissuNyuuryoku(text, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckHankaku(text, "種別", "1")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        msg = CommonCheckFuc.CheckByte(text, 4, "種別")
        If msg <> String.Empty Then
            msgAndFocus.AppendMsgAndCtrl(obj, msg)
            msg = String.Empty
        End If

        ''メッセージ表示
        If msgAndFocus.Message <> String.Empty Then
            CType(grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text = String.Empty
            CType(grdAddBeikou.Rows(0).Cells(1).FindControl("lblAddSyubetumei"), Label).Text = String.Empty
            ShowMsg(msgAndFocus.Message, msgAndFocus.focusCtrl)
            Exit Sub
        End If


        bikousyubetu = Convert.ToInt32(text)

        Dim bikousyubetuMei As String

        bikousyubetuMei = KihonJyouhouInquiryBc.Getkameitensyubetu(bikousyubetu)

        If bikousyubetuMei Is Nothing Then
            CType(grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Text = String.Empty
            CType(grdAddBeikou.Rows(0).Cells(1).FindControl("lblAddSyubetumei"), Label).Text = String.Empty
            ShowMsg(Messages.Instance.MSG2008E, obj, "種別")

            msgAndFocus.setFocus(Me.Page, obj)
            Exit Sub
        Else
            CType(grdAddBeikou.Rows(0).Cells(1).FindControl("lblAddSyubetumei"), Label).Text = bikousyubetuMei
        End If

        msgAndFocus.setFocus(Me.Page, CType(grdAddBeikou.Rows(0).Cells(2).FindControl("tbxAddNaiyou"), TextBox))




    End Sub

    ''' <summary>
    ''' MsgBox表示
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
    ''' 備考表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdBeikou_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBeikou.DataBound

        For i As Integer = 0 To grdBeikou.Rows.Count - 1

            Dim btn As Button
            btn = CType(grdBeikou.Rows(i).Cells(3).FindControl("btnTouroku"), Button)

            btn.Attributes.Add("index", i)
            btn.Attributes.Add("bikousyubetu", CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Text)
            btn.Attributes.Add("nyuuryokuNo", CType(grdBeikou.Rows(i).Cells(0).FindControl("hidNyuuryokuNo"), HiddenField).Value)
            btn.Attributes.Add("Naiyou", CType(grdBeikou.Rows(i).Cells(2).FindControl("tbxNaiyou"), TextBox).Text)

            CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Attributes.Add("index", i)
            CType(grdBeikou.Rows(i).Cells(3).FindControl("btnSakujyo"), Button).Enabled = _kenngenn
            CType(grdBeikou.Rows(i).Cells(3).FindControl("btnSakujyo"), Button).Attributes.Add("index", i)
            CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).Attributes.Add("onchange", "ShowModal();")
            '        CType(grdBeikou.Rows(i).Cells(3).FindControl("btnSakujyo"), Button).Attributes.Add("onclick", "ShowModal();")
            btn.Enabled = _kenngenn
            '  btn.Attributes.Add("onclick", "ShowModal();")

            Dim btnKensaku As Button

            btnKensaku = CType(grdBeikou.Rows(i).Cells(0).FindControl("btnKensaku1"), Button)
            '      btnKensaku.Attributes.Add("onclick", "ShowModal();")

            btnKensaku.Attributes.Add("onclick", "fncOpenwindowSyubetu( '" & _
            CType(grdBeikou.Rows(i).Cells(0).FindControl("tbxBikousyubetu"), TextBox).ClientID & "','" & _
            CType(grdBeikou.Rows(i).Cells(1).FindControl("lblSyubetumei"), Label).ClientID & "') ;return false;")

        Next

    End Sub

    ''' <summary>
    ''' 備考（新規）
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdAddBeikou_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAddBeikou.DataBound

        Dim btn As Button
        btn = CType(grdAddBeikou.Rows(0).Cells(3).FindControl("btnSinki"), Button)
        btn.Enabled = _kenngenn


        Dim btnKensaku As Button
        btnKensaku = CType(grdAddBeikou.Rows(0).Cells(0).FindControl("btnKensaku2"), Button)
        CType(grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).Attributes.Add("onchange", "ShowModal();")
        btnKensaku.Attributes.Add("onclick", "fncOpenwindowSyubetu( '" & _
        CType(grdAddBeikou.Rows(0).Cells(0).FindControl("tbxAddBikousyubetu"), TextBox).ClientID & "','" & _
        CType(grdAddBeikou.Rows(0).Cells(1).FindControl("lblAddSyubetumei"), Label).ClientID & "'); return false;")

    End Sub

    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lbtnBikou_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtnBikou.Click
        '明細を表示
        If meisaiTbody.Style.Item("display") = "none" Then
            meisaiTbody.Style.Item("display") = "inline"
            GamenInit()
        Else
            meisaiTbody.Style.Item("display") = "none"
        End If
    End Sub

#End Region

End Class