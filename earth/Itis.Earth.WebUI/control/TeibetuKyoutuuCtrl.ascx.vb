
Partial Public Class TeibetuKyoutuuCtrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    Dim cLogic As New CommonLogic

#Region "�v���p�e�B"
#Region "���ʏ��"
    ''' <summary>
    ''' ���ʏ��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���ʏ�񃊃��NID</returns>
    ''' <remarks></remarks>
    Public Property KyoutuuInfo() As HtmlGenericControl
        Get
            Return TBodyKyotuInfo
        End Get
        Set(ByVal value As HtmlGenericControl)
            TBodyKyotuInfo = value
        End Set
    End Property
#End Region

    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return TextKubun.Value
        End Get
        Set(ByVal value As String)
            TextKubun.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �ԍ��i���ۏ؏�NO�j
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return TextBangou.Value
        End Get
        Set(ByVal value As String)
            TextBangou.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ���l�P
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou1() As String
        Get
            Return TextBikou1.Value
        End Get
        Set(ByVal value As String)
            TextBikou1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ���l�Q
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bikou2() As String
        Get
            Return TextBikou2.Value
        End Get
        Set(ByVal value As String)
            TextBikou2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sesyumei() As String
        Get
            Return TextSesyuMei.Value
        End Get
        Set(ByVal value As String)
            TextSesyuMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���P
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo1() As String
        Get
            Return TextJyuusyo1.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo1.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���Q
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo2() As String
        Get
            Return TextJyuusyo2.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo2.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �����Z���R
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Jyuusyo3() As String
        Get
            Return TextJyuusyo3.Value
        End Get
        Set(ByVal value As String)
            TextJyuusyo3.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �������{��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TyousaJissibi() As String
        Get
            Return TextTyousaJissibi.Value
        End Get
        Set(ByVal value As String)

            If Not value Is Nothing Then
                If value.Trim() <> "" Then
                    value = IIf(DateTime.Parse(value) = DateTime.MinValue, "", DateTime.Parse(value).ToString("yyyy/MM/dd"))
                End If
            End If

            TextTyousaJissibi.Value = value
        End Set
    End Property

    ''' <summary>
    ''' ���ǍH�����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KairyouKoujiJissibi() As Date
        Get
            Return Date.Parse(TextKairyouKoujiJissibi.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextKairyouKoujiJissibi.Value = ""
            Else
                TextKairyouKoujiJissibi.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' ���ǍH�����H���񒅓�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KairyouKoujiKankou() As Date
        Get
            Return Date.Parse(TextKairyouKoujiKankou.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextKairyouKoujiKankou.Value = ""
            Else
                TextKairyouKoujiKankou.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' �ǉ��H�����{��
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuikaKoujiJissibi() As Date
        Get
            Return Date.Parse(TextTuikaKoujiJissibi.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextTuikaKoujiJissibi.Value = ""
            Else
                TextTuikaKoujiJissibi.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' �ǉ��H�����H���񒅓�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuikaKoujiKankou() As Date
        Get
            Return Date.Parse(TextTuikaKoujiKankou.Value)
        End Get
        Set(ByVal value As Date)

            If value = Date.MinValue Then
                TextTuikaKoujiKankou.Value = ""
            Else
                TextTuikaKoujiKankou.Value = value.ToString("yyyy/MM/dd")
            End If

        End Set
    End Property

    ''' <summary>
    ''' ��͒S���҃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KaisekiTantouCd() As String
        Get
            Return TextKaisekiTantouCd.Value
        End Get
        Set(ByVal value As String)
            TextKaisekiTantouCd.Value = IIf(value = 0, "", value)
        End Set
    End Property

    ''' <summary>
    ''' ��͒S���Җ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KaisekiTantouMei() As String
        Get
            Return TextKaisekiTantouMei.Value
        End Get
        Set(ByVal value As String)
            TextKaisekiTantouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �H���S���҃R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiTantouCd() As String
        Get
            Return TextKoujiTantouCd.Value
        End Get
        Set(ByVal value As String)
            TextKoujiTantouCd.Value = IIf(value = 0, "", value)
        End Set
    End Property

    ''' <summary>
    ''' �H���S���Җ�
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KoujiTantouMei() As String
        Get
            Return TextKoujiTantouMei.Value
        End Get
        Set(ByVal value As String)
            TextKoujiTantouMei.Value = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextKubun
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccKubun() As HtmlInputText
        Get
            Return TextKubun
        End Get
        Set(ByVal value As HtmlInputText)
            TextKubun = value
        End Set
    End Property

    ''' <summary>
    ''' �O������̃A�N�Z�X�p for TextBangou
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccBangou() As HtmlInputText
        Get
            Return TextBangou
        End Get
        Set(ByVal value As HtmlInputText)
            TextBangou = value
        End Set
    End Property


#Region "����P"
    ''' <summary>
    ''' ����P
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiCd1 As Integer
    ''' <summary>
    ''' ����P
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����P</returns>
    ''' <remarks></remarks>
    Public Property HanteiCd1() As Integer
        Get
            Return _hanteiCd1
        End Get
        Set(ByVal value As Integer)
            _hanteiCd1 = value
        End Set
    End Property
#End Region

#Region "����Q"
    ''' <summary>
    ''' ����Q
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiCd2 As Integer
    ''' <summary>
    ''' ����Q
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����Q</returns>
    ''' <remarks></remarks>
    Public Property HanteiCd2() As Integer
        Get
            Return _hanteiCd2
        End Get
        Set(ByVal value As Integer)
            _hanteiCd2 = value
        End Set
    End Property
#End Region

#Region "����ڑ���"
    ''' <summary>
    ''' ����ڑ���
    ''' </summary>
    ''' <remarks></remarks>
    Private _hanteiSetuzokuMoji As Integer
    ''' <summary>
    ''' ����ڑ���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����ڑ���</returns>
    ''' <remarks></remarks>
    Public Property HanteiSetuzokuMoji() As Integer
        Get
            Return _hanteiSetuzokuMoji
        End Get
        Set(ByVal value As Integer)
            _hanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "�o���Ɩ�����"
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return HiddenKeiriGyoumuKengen.Value
        End Get
        Set(ByVal value As Integer)
            HiddenKeiriGyoumuKengen.Value = value
        End Set
    End Property
#End Region

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            SetHanteiData()

            ' ���ʏ��^�C�g�����A���ݒ�
            SetKyoutuuTitleInfo()

            KyoutuuDispLink.HRef = "javascript:changeDisplay('" & _
                                 TBodyKyotuInfo.ClientID & _
                                 "');changeDisplay('" & _
                                 KyoutuuTitleInfobar.ClientID & _
                                 "');"

            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            ' ���V�X�e���ւ̃����N�{�^���ݒ�
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            '�ۏ؏�DB
            cLogic.getHosyousyoDbFilePath(TextKubun.Value, TextBangou.Value, ButtonHosyousyoDB)

            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            ' �C�x���g�n���h���ݒ�
            '+++++++++++++++++++++++++++++++++++++++++++++++++++
            '��������\���{�^��
            ButtonBukkenRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.POPUP_BUKKEN_RIREKI & "','','');"

            '�X�V����\���{�^��
            ButtonKousinRireki.Attributes("onclick") = "callSearch('" & TextKubun.ClientID & EarthConst.SEP_STRING & TextBangou.ClientID & "','" & UrlConst.SEARCH_KOUSIN_RIREKI & "','','');"

            '��ʕ\�����_�̒l���AHidden�ɕێ�(�d�� �ύX�`�F�b�N�p)
            If Me.HiddenOpenValue.Value = String.Empty Then
                Me.HiddenOpenValue.Value = Me.getCtrlValuesStringAll()
            End If

        End If

    End Sub
#End Region

#Region "�v���C�x�[�g���\�b�h"
    ''' <summary>
    ''' ������e�̐ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHanteiData()

        ' ��b�d�l���W�b�N�N���X
        Dim kisoSiyouLogic As New KisoSiyouLogic()



        ' ����P
        If _hanteiCd1 > 0 Then
            Dim record As KisoSiyouRecord = kisoSiyouLogic.GetKisoSiyouRec(_hanteiCd1)

            If record.KahiKbn = 9 Then
                SpanHantei1.Style("color") = "red"
            Else
                SpanHantei1.Style("color") = "blue"
            End If

            SpanHantei1.InnerHtml = record.KsSiyou ' ����P
        End If


        ' ����P
        If _hanteiCd1 > 0 Then
            SpanHantei1.InnerHtml = kisoSiyouLogic.GetKisoSiyouMei(_hanteiCd1)
        End If

        ' ����Q
        If _hanteiCd2 > 0 Then

            Dim record As KisoSiyouRecord = kisoSiyouLogic.GetKisoSiyouRec(_hanteiCd2)

            If record.KahiKbn = 9 Then
                SpanHantei2.Style("color") = "red"
            Else
                SpanHantei2.Style("color") = "blue"
            End If

            SpanHantei2.InnerHtml = record.KsSiyou ' ����Q
        End If

        ' ����ڑ���
        If _hanteiSetuzokuMoji > 0 Then
            SpanHanteiSetuzoku.InnerHtml = kisoSiyouLogic.GetKisoSiyouSetuzokusiMei(_hanteiSetuzokuMoji)
        End If
    End Sub

    ''' <summary>
    ''' ���ʏ��^�C�g�����̏��ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKyoutuuTitleInfo()

        KyoutuuTitleInfobar.InnerHtml = "&nbsp;&nbsp;�y" & _
                                     TextKubun.Value & "�z �y" & _
                                     TextBangou.Value & "�z �y" & _
                                     TextSesyuMei.Value & "�z �y" & _
                                     TextJyuusyo1.Value & "�z"

    End Sub

    ''' <summary>
    ''' �e�L�X�g�G���A�P�̂̊�������
    ''' </summary>
    ''' <param name="enabled"></param>
    ''' <remarks></remarks>
    Private Sub EnableTextArea(ByRef ctrl As HtmlTextArea, _
                               ByVal enabled As Boolean)

        If enabled Then
            ctrl.Attributes("ReadOnly") = ""
            ctrl.Attributes("class") = "codeNumber"
        Else
            ctrl.Attributes("ReadOnly") = "readonly"
            ctrl.Attributes("class") = "readOnlyStyle"
        End If

    End Sub

#Region "��ʃR���g���[���̕ύX�ӏ��Ή�"

    ''' <summary>
    ''' ��ʃR���g���[���̒l���������A�����񉻂���(�S����)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAll() As String

        Dim sb As New StringBuilder

        '���\������
        sb.Append(Me.TextBikou1.Value & EarthConst.SEP_STRING) '���l1

        'KEY���̎擾
        Me.getCtrlValuesStringAllKey()

        Return (sb.ToString)
    End Function

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����������A�����񉻂���
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKey()
        Dim dic As New Dictionary(Of String, String)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        '��ʕ\������DB�l�̘A���l���擾
        If Me.HiddenKeyValue.Value = String.Empty Then

            With dic
                .Add("0", "���l1")
            End With

            strRecString = iLogic.getJoinString(dic.Values.GetEnumerator)
            Me.HiddenKeyValue.Value = strRecString
        End If

    End Sub

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����Ǘ����A�Ώۂ̍��ڂ����݂����ꍇ�A�w�i�F��ԐF�ɕύX����
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCtrlValuesStringAllKeyCtrlId(ByVal strKey As String)
        Dim objRet As New Object
        Dim dic As New Dictionary(Of String, Object)
        Dim strRecString As String = String.Empty
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic

        'Key���ɃI�u�W�F�N�g���Z�b�g
        With dic
            .Add("0", Me.TextBikou1)
        End With

        '�w�i�F�ύX����
        Call cLogic.ChgHenkouCtrlBgColor(dic, strKey)

    End Sub

    ''' <summary>
    ''' �@�ʐ����e�[�u���̑S���ڏ����������A�����񉻂���B
    ''' �ύX�ӏ��̔w�i�F��ύX����
    ''' </summary>
    ''' <param name="strKey">KEY�l</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getCtrlValuesStringAllKeyName(ByVal strKey As String, ByVal strCtrlNameKey As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim MyLogic As New TeibetuSyuuseiLogic

        Dim strKeyValues() As String
        Dim strHiddenKeyValues() As String
        Dim strRet As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty
        Dim dicItem1 As Dictionary(Of String, String)
        Dim strColorId As String = String.Empty

        If strKey = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB�l
        strKeyValues = iLogic.getArrayFromDollarSep(strKey)

        '���ږ����擾
        strHiddenKeyValues = iLogic.getArrayFromDollarSep(strCtrlNameKey)
        dicItem1 = MyLogic.getDicItem(strHiddenKeyValues)

        For intCnt = 0 To strHiddenKeyValues.Length - 1

            If strKeyValues.Length <= intCnt Then Exit For

            strTmp1 = strKeyValues(intCnt)
            If strTmp1 <> String.Empty Then
                If dicItem1.ContainsKey(strTmp1) Then
                    If intCnt <> 0 Then '�ŏ��̍��ڂ�","�͕t���Ȃ�
                        strRet &= ","
                    End If
                    '�ύX�ӏ��̍��ږ��̂��擾
                    strRet &= dicItem1(strTmp1)
                    '�w�i�F�̕ύX
                    Me.getCtrlValuesStringAllKeyCtrlId(strTmp1)
                End If
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' �ύX�̂������R���g���[�����̂𕶎��񌋍����A�ԋp����
    ''' </summary>
    ''' <param name="strDbVal">DB�l</param>
    ''' <param name="strChgVal">�ύX�l</param>
    ''' <param name="strCtrlNm">�R���g���[������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkChgCtrlName(ByVal strDbVal As String, ByVal strChgVal As String, ByVal strCtrlNm As String) As String
        Dim iLogic As New IkkatuHenkouTysSyouhinLogic
        Dim strDbValues() As String
        Dim strChgValues() As String
        Dim strRet As String = String.Empty
        Dim strKey As String = String.Empty

        Dim intCnt As Integer = 0
        Dim strTmp1 As String = String.Empty
        Dim strTmp2 As String = String.Empty

        'DB�l���邢�͕ύX�l�������͂̏ꍇ
        If strDbVal = String.Empty OrElse strChgVal = String.Empty Then
            Return strRet
            Exit Function
        End If

        'DB�l
        strDbValues = iLogic.getArrayFromDollarSep(strDbVal)
        '��ʂ̒l
        strChgValues = iLogic.getArrayFromDollarSep(strChgVal)

        '���ڐ��������ꍇ
        If strDbValues.Length = strChgValues.Length Then
            For intCnt = 0 To strDbValues.Length - 1
                strTmp1 = strDbValues(intCnt)
                strTmp2 = strChgValues(intCnt)
                '�ύX�ӏ��������index��ޔ�
                If strTmp1 <> strTmp2 Then
                    strKey &= CStr(intCnt) & EarthConst.SEP_STRING
                End If
            Next
        End If

        'index�����ɁA�ύX�ӏ��̖��̂Ɣw�i�F�ύX���s�Ȃ�
        strRet = Me.getCtrlValuesStringAllKeyName(strKey, strCtrlNm)

        Return strRet
    End Function

#End Region

#End Region

#Region "�p�u���b�N���\�b�h"
    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByRef strChgPartMess As String _
                          )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckInput", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    strChgPartMess)

        '�n�Չ�ʋ��ʃN���X
        Dim jBn As New Jiban

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "�F"
        End If

        '�֑��������`�F�b�N
        If jBn.KinsiStrCheck(TextBikou1.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", setuzoku & "���l1")
            arrFocusTargetCtrl.Add(TextBikou1)
        End If

        If jBn.KinsiStrCheck(TextBikou2.Value) = False Then
            errMess += Messages.MSG015E.Replace("@PARAM1", setuzoku & "���l2")
            arrFocusTargetCtrl.Add(TextBikou2)
        End If

        '���s�ϊ�
        If TextBikou1.Value <> "" Then
            TextBikou1.Value = TextBikou1.Value.Replace(vbCrLf, " ")
        End If
        If TextBikou2.Value <> "" Then
            TextBikou2.Value = TextBikou2.Value.Replace(vbCrLf, " ")
        End If

        '�o�C�g���`�F�b�N
        If jBn.ByteCheckSJIS(TextBikou1.Value, 256) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", setuzoku & "���l1")
            arrFocusTargetCtrl.Add(TextBikou1)
        End If

        If jBn.ByteCheckSJIS(TextBikou2.Value, 512) = False Then
            errMess += Messages.MSG016E.Replace("@PARAM1", setuzoku & "���l2")
            arrFocusTargetCtrl.Add(TextBikou2)
        End If

        '��r���{(�ύX�`�F�b�N)
        Dim strChgVal As String = Me.getCtrlValuesStringAll()
        If Me.HiddenOpenValue.Value <> strChgVal Then
            Dim strCtrlNm As String = String.Empty
            strCtrlNm = Me.ChkChgCtrlName(Me.HiddenOpenValue.Value, strChgVal, Me.HiddenKeyValue.Value) '�ύX�ӏ����̎擾
            strChgPartMess += "[" & typeWord & "]\r\n" & strCtrlNm & "\r\n"
        End If

    End Sub
#End Region

End Class