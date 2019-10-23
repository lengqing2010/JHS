
''' <summary>
''' ���ʏ����N���X(�ÓI�C���X�^���X)
''' </summary>
''' <remarks>���̃N���X�ɂ̓A�v���P�[�V�����ŋ��L���鏈���ȊO�������Ȃ���</remarks>
Public Class CommonLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    '�ÓI�ϐ��Ƃ��ăN���X�^�̃C���X�^���X�𐶐�
    Private Shared _instance = New CommonLogic()

    '�ÓI�֐��Ƃ��ăN���X�^�̃C���X�^���X��Ԃ��֐���p��
    Public Shared ReadOnly Property Instance() As CommonLogic
        Get
            '�ÓI�ϐ����������Ă����ꍇ�̂݁A�C���X�^���X�𐶐�����
            If IsDBNull(_instance) Then
                _instance = New CommonLogic()
            End If
            Return _instance
        End Get
    End Property

    Private Const strOpenLinkScript As String = "window.open('about:Blank','_blank').location.href='{0}'"

    ''' <summary>
    ''' ��ʕ\���p�ɃI�u�W�F�N�g�𕶎���ϊ�����
    ''' </summary>
    ''' <param name="obj">�\���Ώۂ̃f�[�^</param>
    ''' <param name="str">���ݒ莞�̏����l</param>
    ''' <returns>�\���`���̕�����</returns>
    ''' <remarks>
    ''' Decimal  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' Integer  : Minvalue���󔒁A����ȊO�͓��͒l��String�^�ŕԋp<br/>
    ''' DateTime : MinDateTime���󔒁A����ȊO�͓��͒l�� YYYY/MM/DD �`����String�^�ŕԋp<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������
    ''' </remarks>
    Public Function GetDisplayString(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDisplayString", _
                                                    obj, _
                                                    str)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDisplayString(obj, str)
    End Function

    ''' <summary>
    ''' ��ʕ\���p�ɃI�u�W�F�N�g�𕶎���ϊ�����i�����j
    ''' </summary>
    ''' <param name="obj">�\���Ώۂ̃f�[�^�i�����j</param>
    ''' <param name="str">���ݒ莞�̏����l</param>
    ''' <returns>�\���`���̕�����</returns>
    ''' <remarks>
    ''' DateTime : MinDateTime���󔒁A����ȊO�͓��͒l�� yyyy/MM/dd HH:mm:ss�`����String�^�ŕԋp<br/>
    ''' </remarks>
    Public Function GetDispStrDateTime(ByVal obj As Object, _
                                     Optional ByVal str As String = "") As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDispStrDateTime", _
                                                    obj, _
                                                    str)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDispStrDateTime(obj, str)
    End Function

    ''' <summary>
    ''' ��ʕ\���p��������w�肵���^�ɕϊ�����
    ''' </summary>
    ''' <param name="strData">�ϊ��Ώۂ̃f�[�^</param>
    ''' <param name="objChangeData">�ϊ���̌^�f�[�^�i�Q�Ɓj</param>
    ''' <returns>�ϊ���̌^�f�[�^</returns>
    ''' <remarks>
    ''' Decimal  : �󔒂�Minvalue�A����ȊO�͓��͒l��Decimal�ɕϊ�<br/>
    ''' Integer  : �󔒂�Minvalue�A����ȊO�͓��͒l��Integer�ɕϊ�<br/>
    ''' DateTime : �󔒂�Minvalue�A����ȊO�͓��͒l��DateTime�ɕϊ�<br/>
    ''' ��L�ȊO : ���̂܂܁B�K�X�ǉ����Ă�������<br/>
    ''' �ϊ��Ɏ��s�����ꍇ��False��Ԃ��A�w��^��MinValue���Z�b�g���܂�
    ''' </remarks>
    Public Function SetDisplayString(ByVal strData As String, _
                                     ByRef objChangeData As Object) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDisplayString", _
                                                    strData, _
                                                    objChangeData)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.SetDisplayString(strData, objChangeData)
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i������n�j
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="strReturn">�߂������l���Z�b�g</param>
    ''' <returns>[String]�������̕����� or �������̒l</returns>
    ''' <remarks>
    ''' �������̒l���擾�ł��Ȃ��ꍇ�A""(��)��߂�<br/>
    ''' �������Ŗ߂������l���w��\<br/>
    ''' </remarks>
    Public Function GetDispStr(ByVal objArg As Object, Optional ByVal strReturn As String = "") As String
        Dim strTmp As String = Me.GetDisplayString(objArg)
        If strTmp.Length = 0 Then
            strTmp = ""
        Else
            If strReturn <> "" Then
                strTmp = strReturn
            End If
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i���l�n�j
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="strReturn">�߂������l���Z�b�g</param>
    ''' <returns>[String]�������̕����� or �������̒l</returns>
    ''' <remarks>
    ''' �������̒l���擾�ł��Ȃ��ꍇ�A""(��)��߂�<br/>
    ''' �������Ŗ߂������l���w��\<br/>
    ''' </remarks>
    Public Function GetDispNum(ByVal objArg As Object, Optional ByVal strReturn As String = "0") As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDispNum", _
                                                    objArg, _
                                                    strReturn)

        Dim cbLogic As New CommonBizLogic
        Return cbLogic.GetDispNum(objArg, strReturn)
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i�L���\���j
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <param name="blnShortType">�Lor��</param>
    ''' <param name="blnDispCode">�R�[�h�l�{"�F" �\���L��</param>
    ''' <returns>[String]�L��A���� ���邢�͗L�A��</returns>
    ''' <remarks>
    ''' �E��ʕ\�����ړ��́u�L��v�u�����v��\������B<br/>
    ''' blnShortType��True�̏ꍇ�A�u�L�v�u���v�ŕ\��<br/>
    ''' blnShortType��False�̏ꍇ�A�u�L��v�u�����v�ŕ\��<br/>
    ''' blnDispCode��True�̏ꍇ�A�u�R�[�h�l�v�{�u�F�v�ŕ\��<br/>
    ''' </remarks>
    Public Function GetDispUmuStr(ByVal objArg As Object, _
                                    Optional ByVal blnShortType As Boolean = False, _
                                    Optional ByVal blnDispCode As Boolean = False _
                                ) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = String.Empty Then
            Return strTmp
        End If

        If objTmp.ToString = "0" Then
            strTmp = EarthConst.NASI
        ElseIf objTmp.ToString = "1" Then
            strTmp = EarthConst.ARI
        End If

        If blnShortType <> False Then
            strTmp = Left(strTmp, 1)
        End If

        If blnDispCode <> False Then
            strTmp = objTmp.ToString & ":" & strTmp
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i���㏈���ϕ\���j
    ''' </summary>
    ''' <param name="objArg">�u����v����v���w��</param>
    ''' <returns>[String]���㏈���� or ""</returns>
    ''' <remarks>
    ''' �u����v����v�̓��͂�����ꍇ�A�u���㏈���ρv��\��<br/>
    ''' �u����v����v�̓��͂��Ȃ��ꍇ�A�u""(��)�v��\��<br/>
    ''' </remarks>
    Public Function GetDispUriageSyoriZumiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString.Length = 0 Then
            strTmp = ""
        Else
            strTmp = EarthConst.URIAGE_ZUMI
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' ��ʕ\���p�����񂩂�t���O�ɕϊ�����t�@���N�V����(���㏈���ϕ\��)
    ''' </summary>
    ''' <param name="strZumiTmp">�u���㏈���ρv������w��</param>
    ''' <returns>[String]"1" or ""</returns>
    ''' <remarks></remarks>
    Public Function GetUriageSyoriZumiFlg(ByVal strZumiTmp As String) As String
        Dim strTmp As String = ""

        If strZumiTmp = EarthConst.URIAGE_ZUMI Then
            strTmp = EarthConst.URIAGE_ZUMI_CODE
        Else
            strTmp = String.Empty
        End If

        Return strTmp
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i�������m��\���j
    ''' </summary>
    ''' <param name="objArg"></param>
    ''' <returns>[String]1:�m�� or 0:���m��</returns>
    ''' <remarks>
    ''' �������m��FLG��1�̏ꍇ�A�u�m��v��\��<br/>
    ''' �������m��FLG��0�̏ꍇ�A�u���m��v��\��<br/>
    ''' </remarks>
    Public Function GetDispHattyuusyoKakuteiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = "0" Then
            strTmp = EarthConst.MIKAKUTEI
        ElseIf objTmp.ToString = "1" Then
            strTmp = EarthConst.KAKUTEI
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' ��ʕ\���p������ɕϊ�����t�@���N�V�����i�����X�ENG��񃁃b�Z�[�W�\���j
    ''' </summary>
    ''' <param name="objArg">�����X�}�X�^.�ۋ敪</param>
    ''' <returns>[String]�������NG or ""</returns>
    ''' <remarks>
    ''' �����X�}�X�^.�ۋ敪=9�̏ꍇ�A�u�������NG�v��\��<br/>
    ''' �����X�}�X�^.�ۋ敪=9�ȊO�̏ꍇ�A�u""(��)�v��\��<br/>
    ''' </remarks>
    Public Function GetDispKahiStr(ByVal objArg As Object) As String

        Dim objTmp As Object = Me.GetDispStr(objArg)
        Dim strTmp As String = ""

        If objTmp.ToString = "9" Then
            strTmp = EarthConst.TYOUSA_KAISYA_NG
        Else
            strTmp = ""
        End If
        Return strTmp
    End Function

    ''' <summary>
    ''' �������Integer�^�ɕϊ�����t�@���N�V�����i�S�ʁj
    ''' </summary>
    ''' <param name="strArg"></param>
    ''' <returns>[Integer]�������l or Integer.MinValue</returns>
    ''' <remarks>
    ''' <br/>
    ''' </remarks>
    Public Function ChgStrToInt(ByVal strArg As String) As Integer
        Dim intTmp As Integer = 0
        If strArg Is Nothing Then
            Return intTmp
        End If

        Dim strTmp As String = strArg.Trim
        strTmp = strTmp.Replace(",", "")
        strTmp = strTmp.Replace("/", "")

        If strTmp.Length = 0 Then
            intTmp = Integer.MinValue
        Else
            intTmp = Integer.Parse(strTmp)
        End If
        Return intTmp
    End Function

    Public Function getTyosaMitumoriFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String
        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                'Web.config���ۏ؏�DB�̃t�@�C���p�X�\�����擾
                strPath = ConfigurationManager.AppSettings("HosyousyoDbFilePath")
                strReturn = strPath
            Else
                strReturn = String.Empty
            End If
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' �������Ϗ��t�@�C�������N�p�X����
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="no">�ԍ�</param>
    ''' <returns>�������Ϗ��t�@�C���p�X</returns>
    ''' <remarks></remarks>
    Public Function getTyousaMitsumorisyoFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                'Web.config��蒲�����Ϗ��̃t�@�C���p�X�\�����擾
                strPath = ConfigurationManager.AppSettings("TyosaMitumoriFilePath")

                '�p�X�ɒu��
                strPath = strPath.Replace("@KUBUN@", kubun)
                strPath = strPath.Replace("@NO@", no)
                strPath = strPath.Replace("@TYOUSAMITSUMORISYO.PDF@", EarthConst.PDF_TYOUSAMITSUMORISYO)
                strReturn = strPath
            Else
                strReturn = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strReturn = String.Empty, String.Empty, String.Format(strOpenLinkScript, strReturn))
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' �ۏ؏�DB�t�@�C�������N�p�X����
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="no">�ԍ�</param>
    ''' <returns>�ۏ؏�DB�t�@�C���p�X</returns>
    ''' <remarks></remarks>
    Public Function getHosyousyoDbFilePath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strReturn As String = String.Empty
        Dim strPath As String = String.Empty
        Dim strConfig As String = String.Empty
        Dim cbLogic As New CommonBizLogic

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strReturn = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                '�ԍ��𕪉�
                Dim server As String = String.Empty
                Dim year As String = no.Substring(0, 4)
                Dim month As String = no.Substring(4, 2)
                Dim ym As String = no.Substring(0, 6)
                Dim num1 As String = no.Substring(6, 2)
                Dim num2 As String = no.Substring(8, 2)
                Dim intNo As Integer = Integer.Parse(no.Substring(6, 4))
                Dim intFrom As Integer
                Dim intTo As Integer
                Dim noFrom As String
                Dim noTo As String
                Dim recResult As New HosyousyoDbRecord

                'Web.config���ۏ؏�DB�̃t�@�C���p�X�\�����擾
                strPath = ConfigurationManager.AppSettings("HosyousyoDbFilePath")

                '��DB����T�[�o�p�X���擾
                recResult = cbLogic.GetHosyousyoDbLinkPath(kubun, ym)
                If Not recResult.KakunousakiFilePass Is Nothing Then
                    server = recResult.KakunousakiFilePass.ToString
                End If

                '�ԍ���4����0000�̏ꍇ�A�z��O�Ȃ̂ŁA�󕶎���Ԃ�
                If intNo <= 0 Then
                    Return String.Empty
                End If

                '�t�H���_���͈̔͐ݒ�
                intFrom = ((intNo - 1) \ 100) * 100 + 1
                intTo = ((intNo - 1) \ 100 + 1) * 100

                noFrom = Format(intFrom, "0000")
                noTo = Format(intTo, "0000")

                '�p�X�ɒu��
                strPath = strPath.Replace("@SERVER@", server)
                strPath = strPath.Replace("@YEAR@", year)
                strPath = strPath.Replace("@KUBUN@", kubun)
                strPath = strPath.Replace("@MONTH@", month)
                strPath = strPath.Replace("@NOFROM@", noFrom)
                strPath = strPath.Replace("@NOTO@", noTo)
                strPath = strPath.Replace("@NUM1@", num1)
                strPath = strPath.Replace("@NUM2@", num2)

                strReturn = strPath

                'DB�l(�t�@�C���p�X)���擾�o���Ȃ��ꍇ�́A�p�X��ݒ肵�Ȃ�
                If String.IsNullOrEmpty(server) Then
                    strReturn = String.Empty
                End If
            Else
                strReturn = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strReturn = String.Empty, String.Empty, String.Format(strOpenLinkScript, strReturn))
        End If

        Return strReturn
    End Function

    ''' <summary>
    ''' ReportJHS�����N�p�X����
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="no">�ԍ�</param>
    ''' <returns>�ۏ؏�DB�t�@�C���p�X</returns>
    ''' <remarks></remarks>
    Public Function getReportJHSPath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strPath = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                Dim path As String = ConfigurationManager.AppSettings("ReportJHSPath")

                '�p�X�ɒu��
                path = path.Replace("@KUBUN@", kubun)
                path = path.Replace("@NO@", no)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' �����X���ӏ�񃊃��N�p�X����
    ''' </summary>
    ''' <param name="kameitenCdClientId">�����X�R�[�h���͍��ڂ�ClientID</param>
    ''' <returns>�����X���ӏ��t�@�C���p�X</returns>
    ''' <remarks></remarks>
    Public Function getKameitenTyuuijouhouPath(ByVal kameitenCdClientId As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(kameitenCdClientId) Then
            strPath = String.Empty
        Else
            If kameitenCdClientId.Length >= 1 Then
                Dim path As String = UrlConst.EARTH2_KAMEITEN_TYUUIJIKOU
                '�p�X�ɒu��
                path += "?strKameitenCd=" & "' + objEBI('" & kameitenCdClientId & "').value + '"
                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath

    End Function

    ''' <summary>
    ''' ������}�X�^�����N�p�X����
    ''' </summary>
    ''' <param name="prmSskCd">������R�[�h.ClientID</param>
    ''' <param name="prmSskBrc">������}��.ClientID</param>
    ''' <param name="prmSskKbn">������敪.ClientID</param>
    ''' <param name="targetCtrl">�Ώ�HTML�R���g���[��</param>
    ''' <returns>������}�X�^�t�@�C���p�X</returns>
    ''' <remarks></remarks>
    Public Function getSeikyuuSakiMasterPath( _
                                            ByVal prmSskCd As String _
                                            , ByVal prmSskBrc As String _
                                            , ByVal prmSskKbn As String _
                                            , Optional ByRef targetCtrl As HtmlControl = Nothing _
                                            ) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(prmSskCd) _
            Or String.IsNullOrEmpty(prmSskBrc) _
                Or String.IsNullOrEmpty(prmSskKbn) Then

            strPath = String.Empty
        Else
            If (prmSskCd & prmSskBrc & prmSskKbn).Length >= 1 Then
                Dim path As String = UrlConst.EARTH2_SEIKYUUSAKI_MASTER '������M

                path += "?sendSearchTerms=" _
                         & prmSskCd & EarthConst.SEP_STRING _
                         & prmSskBrc & EarthConst.SEP_STRING _
                         & prmSskKbn

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If targetCtrl IsNot Nothing Then
            targetCtrl.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
            '�X�^�C���ݒ�
            If targetCtrl.GetType.Name = "HtmlAnchor" Then '�A���J�[�̏ꍇ
                targetCtrl.Attributes("style") = "text-decoration:underline;display:inherit;cursor:pointer;"
            End If
        End If

        Return strPath

    End Function

    ''' <summary>
    ''' �\����񃊃��N�p�X����
    ''' </summary>
    ''' <param name="strMousikomiNo">�\��NO</param>
    ''' <returns>�\����񃊃��N�p�X</returns>
    ''' <remarks></remarks>
    Public Function getMousikomiPath(ByVal strMousikomiNo As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty

        If String.IsNullOrEmpty(strMousikomiNo) Then
            strPath = String.Empty
        Else
            If strMousikomiNo.Length >= 15 Then
                Dim path As String = ConfigurationManager.AppSettings("MousikomiPath")

                '�p�X�ɒu��
                path = path.Replace("@NO@", strMousikomiNo)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' FC�\����񃊃��N�p�X����
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="no">�ԍ�</param>
    ''' <returns>�\����񃊃��N�p�X</returns>
    ''' <remarks></remarks>
    Public Function getFcMousikomiPath(ByVal kubun As String, ByVal no As String, Optional ByRef button As HtmlInputButton = Nothing) As String

        Dim strPath As String = String.Empty


        If String.IsNullOrEmpty(kubun) Or String.IsNullOrEmpty(no) Then
            strPath = String.Empty
        Else
            If (kubun & no).Length >= 11 Then
                Dim path As String = ConfigurationManager.AppSettings("FcMousikomiPath")

                '�p�X�ɒu��
                path = path.Replace("@KUBUN@", kubun)
                path = path.Replace("@NO@", no)

                strPath = path
            Else
                strPath = String.Empty
            End If
        End If

        '�{�^����n����Ă���ꍇ�Aonclick�ݒ�
        If button IsNot Nothing Then
            button.Attributes("onclick") = IIf(strPath = String.Empty, String.Empty, String.Format(strOpenLinkScript, strPath))
        End If

        Return strPath
    End Function

    ''' <summary>
    ''' �R���g���[����\���X�^�C���ɕύX����
    ''' </summary>
    ''' <param name="tmpCtrl">�؂�ւ��ΏۃR���g���[��</param>
    ''' <param name="tmpText">�v���_�E�����̒l��\������Span�R���g���[��</param>
    ''' <param name="blnBorder">�e�L�X�g�{�b�N�X�̉����\���L��</param>
    ''' <remarks></remarks>
    Sub chgVeiwMode(ByRef tmpCtrl As Object, Optional ByRef tmpText As HtmlGenericControl = Nothing, Optional ByVal blnBorder As Boolean = False)

        '�R���g���[���̃^�C�v���Ƃɏ��������s
        Select Case tmpCtrl.GetType.Name

            Case "HtmlInputText", "HtmlInputHidden", "HtmlTextArea"
                tmpCtrl.Attributes("readonly") = True
                Dim classString As String = String.Empty
                If tmpCtrl.Attributes("class") IsNot Nothing Then
                    classString = Replace(tmpCtrl.Attributes("class").ToString, "readOnlyStyle", "")
                    classString = Replace(classString, " readOnlyStyle", "")
                End If
                tmpCtrl.Attributes("class") = classString & " readOnlyStyle"
                If blnBorder = False Then
                    tmpCtrl.Style("border-style") = "none"
                End If
                tmpCtrl.Attributes("tabindex") = -1

            Case "TextBox"
                tmpCtrl.ReadOnly = True
                Dim classString As String = String.Empty
                If tmpCtrl.CssClass IsNot Nothing Then
                    classString = Replace(tmpCtrl.CssClass.ToString, "readOnlyStyle", "")
                    classString = Replace(classString, " readOnlyStyle", "")
                End If
                tmpCtrl.CssClass = classString & " readOnlyStyle"
                If blnBorder = False Then
                    tmpCtrl.Style("border-style") = "none"
                End If
                tmpCtrl.Tabindex = -1

            Case "HtmlInputRadioButton", "HtmlInputCheckBox", "CheckBox"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    If tmpCtrl.Checked = True Then
                        tmpText.InnerHtml = Replace(tmpText.InnerHtml, "&nbsp;�y&nbsp;", String.Empty)
                        tmpText.InnerHtml = Replace(tmpText.InnerHtml, "&nbsp;�z&nbsp;", String.Empty)
                        tmpText.InnerHtml = "&nbsp;�y&nbsp;" & tmpText.InnerHtml & "&nbsp;�z&nbsp;"
                        tmpText.Style("display") = "inline"
                    Else
                        tmpText.Style("display") = "none"
                    End If
                End If

            Case "HtmlSelect"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    tmpText.InnerHtml = "&nbsp;�y&nbsp;" & tmpCtrl.Items(tmpCtrl.SelectedIndex).Text & "&nbsp;�z&nbsp;"
                    tmpText.Style("display") = "inline"
                End If

            Case "DropDownList"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                    tmpText.InnerHtml = "&nbsp;�y&nbsp;" & tmpCtrl.SelectedItem.Text & "&nbsp;�z&nbsp;"
                    tmpText.Style("display") = "inline"
                End If

            Case "HtmlInputButton"
                If tmpText IsNot Nothing Then
                    tmpCtrl.Style("display") = "none"
                End If

            Case Else
                Exit Sub

        End Select

    End Sub

    ''' <summary>
    ''' ���i�s�e�L�X�g�{�b�N�X��\���X�^�C���ɕύX����
    ''' </summary>
    ''' <param name="text"></param>
    ''' <remarks></remarks>
    Sub chgDispSyouhinText(ByRef text As Object)
        Dim strType As String = text.GetType.Name

        If strType = "HtmlInputText" Then
            text.Attributes.Remove("ReadOnly")
            If text.Attributes("Class") IsNot Nothing Then
                text.Attributes("Class") = _
                    text.Attributes("Class").Replace("readOnlyStyle", "")
            End If
            text.Attributes.Remove("tabindex")
        ElseIf strType = "TextBox" Then
            text.ReadOnly = False
            If text.CssClass IsNot Nothing Then
                text.CssClass = _
                    text.CssClass.ToString.Replace("readOnlyStyle", "")
            End If
            text.TabIndex = Nothing
        End If

        text.Style.Remove("border-style")
    End Sub

    ''' <summary>
    ''' ���i�s�v���_�E����\���X�^�C���ɕύX����
    ''' </summary>
    ''' <param name="pull"></param>
    ''' <param name="objSpan"></param>
    ''' <remarks></remarks>
    Sub chgDispSyouhinPull(ByRef pull As Object, ByRef objSpan As Object)
        pull.Style.Remove("display")
        objSpan.InnerHtml = String.Empty
    End Sub

    ''' <summary>
    ''' �`�F�b�N�{�b�N�X��\���X�^�C���ɕύX����
    ''' </summary>
    ''' <param name="pull"></param>
    ''' <param name="objSpan"></param>
    ''' <remarks></remarks>
    Sub chgDispCheckBox(ByRef pull As Object, ByRef objSpan As Object)
        pull.Style.Remove("display")
        objSpan.InnerHtml = String.Empty
    End Sub

    ''' <summary>
    ''' �n��R�[�h������R�n�񂩂̔����Ԃ��܂�<br/>
    ''' ����R�n��
    ''' �E�A�C�t���z�[�� "0001"
    ''' �ETH�F�̉� "THTH"
    ''' �E�����_�[�z�[�� "NF03"
    ''' </summary>
    ''' <param name="keiretu_cd"></param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks> </remarks>
    Public Function getKeiretuFlg(ByVal keiretu_cd As String) As Boolean
        Select Case keiretu_cd
            Case EarthConst.KEIRETU_AIFURU
                Return True
            Case EarthConst.KEIRETU_TH
                Return True
            Case EarthConst.KEIRETU_WANDA
                Return True
        End Select
        Return False

    End Function

    ''' <summary>
    ''' ���t�͈̓`�F�b�N
    ''' </summary>
    ''' <param name="str">���t�Ƀp�[�X�\�ȕ�����</param>
    ''' <returns>�`�F�b�N����</returns>
    ''' <remarks></remarks>
    Public Function checkDateHanni(ByVal str As String) As Boolean
        Try
            If DateTime.Parse(str) > EarthConst.Instance.MAX_DATE Or DateTime.Parse(str) < EarthConst.Instance.MIN_DATE Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' ���t�͈̓`�F�b�N(From,To�w��)
    ''' </summary>
    ''' <param name="str">���t�Ƀp�[�X�\�ȕ�����</param>
    ''' <param name="dtFrom">From</param>
    ''' <param name="dtTo">To</param>
    ''' <returns>�`�F�b�N����</returns>
    ''' <remarks></remarks>
    Public Function checkDateHanniFromTo(ByVal str As String, _
                                         ByVal dtFrom As Date, _
                                         ByVal dtTo As Date) As Boolean
        Try
            If DateTime.Parse(str) >= dtFrom AndAlso DateTime.Parse(str) <= dtTo Then
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

#Region "�n�Ճe�[�u��.�X�V��(kousinsya)"
    ''' <summary>
    ''' ��ʕ\���p(�n�Ճe�[�u��.�X�V�҂�胍�O�C�����[�U���A�X�V���t���擾����)
    ''' </summary>
    ''' <param name="strKousinsya">�n�Ճe�[�u��.�X�V��(kousinsya)</param>
    ''' <param name="strUserName">���O�C�����[�U��</param>
    ''' <param name="strUpdDate">�X�V���t(yy/mm/dd hh:mm)</param>
    ''' <returns>���O�C�����[�UID</returns>
    ''' <remarks>�n�Ճe�[�u��.�X�V��(kousinsya)�����[�U���{�X�V���t(yy/mm/dd hh:mm)</remarks>
    Public Function SetKousinsya(ByVal strKousinsya As String, ByRef strUserName As String, ByRef strUpdDate As String) As String
        Dim strRetUserID As String = "" '�߂�l(���O�C�����[�UID)
        Dim strTmpUpdDate As String = "" '��Ɨp(�X�V���t)
        Dim strTmp() As String = Nothing

        If strKousinsya Is Nothing OrElse strKousinsya.Trim = "" Then
            strUserName = ""
            strUpdDate = ""
            '���[�UID��ԋp����
            Return strRetUserID
            Exit Function
        End If

        '�n�Ճe�[�u��.�X�V�҂��A���[�UID�ƍX�V���t�ɕ�������
        strTmp = strKousinsya.Split("$")
        strRetUserID = strTmp(0)
        strTmpUpdDate = strTmp(1)

        '���[�UID��胆�[�U�����擾����
        Dim userLogic As New LoginUserLogic
        Dim userInfo As New LoginUserInfo

        '���[�U�����Z�b�g
        If userLogic.MakeUserInfo(strRetUserID, userInfo) Then
            strUserName = userInfo.Name '���[�U����ԋp
        Else
            strUserName = strRetUserID '���[�UID��ԋp
        End If

        '�X�V���t���t�H�[�}�b�g���ăZ�b�g
        Dim dtTmp As New DateTime
        Try
            dtTmp = DateTime.ParseExact(strTmpUpdDate, EarthConst.FORMAT_DATE_TIME_4, Nothing)
            strUpdDate = Format(dtTmp, "yyyy/MM/dd HH:mm")
        Catch ex As Exception
            strUpdDate = strTmpUpdDate
        End Try

        '���[�UID��ԋp����
        Return strRetUserID
    End Function

#End Region

#Region "[��ʕ\���p]�o�^�Җ�,�X�V�Җ�"
    ''' <summary>
    ''' ��ʕ\���p(���[�UID���o�^/�X�V���[�U����ԋp����)
    ''' </summary>
    ''' <param name="strUserID">���[�UID</param>
    ''' <remarks>���[�U��(�擾�s���A���[�UID)</remarks>
    Public Function SetDispUserNM(ByVal strUserID As String) As String
        Dim strRetVal As String = String.Empty '�߂�l

        If strUserID Is Nothing OrElse strUserID.Trim = String.Empty Then
            '�󔒂�ԋp����
            Return strRetVal
            Exit Function
        End If

        '���[�UID��胆�[�U�����擾����
        Dim userLogic As New LoginUserLogic
        Dim userInfo As New LoginUserInfo

        '���[�U�����Z�b�g
        If userLogic.MakeUserInfo(strUserID, userInfo) Then
            If userInfo.DisplayName Is Nothing OrElse userInfo.DisplayName = String.Empty Then
                strRetVal = strUserID  '���[�UID��ԋp
            Else
                strRetVal = userInfo.DisplayName  '���[�U����ԋp
            End If
        Else
            strRetVal = strUserID '���[�UID��ԋp
        End If

        Return strRetVal
    End Function

#End Region

#Region "���i�R�[�h����(���H��=B2000�ԑ�)"

    ''' <summary>
    ''' ���H�����i�`�F�b�N
    ''' </summary>
    ''' <param name="strSyouhinCd"></param>
    ''' <returns>True[B2000�ԑ�] or False[B2000�ԑ�ȊO]</returns>
    ''' <remarks>�ΏۂƂ��鏤�i�R�[�h�����H��(=B2000�ԑ�)���ǂ���������s�Ȃ��B</remarks>
    Public Function ChkSyouhinCdB2000(ByVal strSyouhinCd As String) As Boolean
        '���̓`�F�b�N
        If strSyouhinCd Is Nothing OrElse strSyouhinCd.Trim = String.Empty Then
            Return False
        End If

        Dim strLogic As New StringLogic
        '�擪�����񂪁uB2�v�ł��A���i�R�[�h�̒�����5��
        If strSyouhinCd.StartsWith("B2") And strLogic.GetStrByteSJIS(strSyouhinCd) = 5 Then
            Return True
        End If
        Return False
    End Function

#End Region

#Region "������/�d����"

    ''' <summary>
    ''' ������敪�A������(�d����)�R�[�h�A������(�d����)�}�Ԃ̕\�L�������Ԃ�
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">������敪 or ""(�d����)</param>
    ''' <param name="strSakiCd">������(�d����)�R�[�h</param>
    ''' <param name="strSakiBrc">������(�d����)�}��</param>
    ''' <param name="blnBlank">�u�����N�Ƃ��ĕ\�����邩�t���O(True�F�u�����N�\�� False�F"��nbsp;"�\��)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispSeikyuuSakiCd(ByVal strSeikyuuSakiKbn As String, ByVal strSakiCd As String, ByVal strSakiBrc As String, Optional ByVal blnBlank As Boolean = False) As String
        Const KAMEITEN As String = "��:"    '�����X
        Const TYSKAISYA As String = "��:"   '�������
        Const EIGYOUSYO As String = "�c:"   '�c�Ə�
        Const HYPHEN As String = " - "      '�n�C�t��
        Const SPACE As String = EarthConst.HANKAKU_SPACE    '���p�X�y�[�X

        Dim strRet As String = String.Empty

        '������敪
        If Me.GetDisplayString(strSeikyuuSakiKbn) = String.Empty Then
            strRet = String.Empty
        Else
            If strSeikyuuSakiKbn = "0" Then
                strRet = KAMEITEN
            ElseIf strSeikyuuSakiKbn = "1" Then
                strRet = TYSKAISYA
            ElseIf strSeikyuuSakiKbn = "2" Then
                strRet = EIGYOUSYO
            Else
                strRet = String.Empty
            End If
        End If

        '������R�[�h�A������}��
        If Me.GetDisplayString(strSakiCd) = String.Empty Or Me.GetDisplayString(strSakiBrc) = String.Empty Then
            If blnBlank = False Then
                strRet = SPACE
            Else
                strRet = String.Empty
            End If
        Else
            '�A��
            strRet &= strSakiCd & HYPHEN & strSakiBrc
        End If

        Return strRet
    End Function

#End Region

#Region "�x��/�Ȗ�"

    ''' <summary>
    ''' �Ȗڂ̉�ʕ\��������̐��`���s�Ȃ��A�ԋp����
    ''' </summary>
    ''' <param name="intKamoku">�Ȗ�</param>
    ''' <param name="blnBlank">�u�����N�Ƃ��ĕ\�����邩�t���O(True�F�u�����N�\�� False�F"��nbsp;"�\��)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDispKamoku(ByVal intKamoku As Integer, Optional ByVal blnBlank As Boolean = False) As String
        Dim strRet As String = String.Empty

        If intKamoku = 0 Then
            strRet = EarthConst.KAMOKU_KAIKAKE
        ElseIf intKamoku = 1 Then
            strRet = EarthConst.KAMOKU_MIBARAI
        Else
            If blnBlank Then
                strRet = String.Empty
            Else
                strRet = EarthConst.HANKAKU_SPACE
            End If
        End If

        Return strRet
    End Function

#End Region

#Region "�h���b�v�_�E�����X�g���̃f�[�^���݃`�F�b�N"
    ''' <summary>
    ''' �h���b�v�_�E�����X�g���̃f�[�^���݃`�F�b�N
    ''' </summary>
    ''' <param name="drpArg">�`�F�b�N�Ώۃh���b�v�_�E�����X�g</param>
    ''' <param name="strSearchCd">[String]�R�[�h�l</param>
    ''' <returns>[Boolean]True or False</returns>
    ''' <remarks>�������̃h���b�v�_�E�����X�g���ɁA�������̃f�[�^�����݂��邩�ǂ����𔻒f����</remarks>
    Public Function ChkDropDownList(ByVal drpArg As DropDownList, ByVal strSearchCd As String) As Boolean
        Dim intItemCnt As Integer = drpArg.Items.Count '�A�C�e����
        Dim intCnt As Integer '�J�E���^

        For intCnt = 0 To intItemCnt - 1
            If strSearchCd = drpArg.Items(intCnt).Value Then
                Return True
            End If
        Next

        Return False
    End Function
#End Region

    ''' <summary>
    ''' ����ʂ����X�N���v�g�𐶐�����
    ''' </summary>
    ''' <param name="objPage">�y�[�W�N���X�I�u�W�F�N�g</param>
    ''' <remarks></remarks>
    Public Sub CloseWindow(ByVal objPage As Page)
        Dim tmpScript As String = "window.close();" '��ʂ����
        ScriptManager.RegisterStartupScript(objPage, Me.GetType(), "CloseWindow", tmpScript, True)
    End Sub

    ''' <summary>
    ''' ������𐔒l�^[Long]�ɕϊ����܂�
    ''' </summary>
    ''' <param name="strValue">�ϊ�������������</param>
    ''' <returns>�ϊ���̐��l</returns>
    ''' <remarks></remarks>
    Public Function Str2Long(ByVal strValue As String) As Long
        Dim lngRet As Long

        If strValue Is Nothing OrElse strValue.Length = 0 Then
            lngRet = 0
        Else
            strValue = strValue.Replace(",", "")
            If IsNumeric(strValue) Then
                lngRet = Long.Parse(strValue)
            Else
                lngRet = 0
            End If
        End If
        Return lngRet
    End Function

#Region "�f�[�^�t�@�C���o�͊֘A"

#Region "�e�L�X�g���t�@�C���o�͂���"
    ''' <summary>
    ''' �e�L�X�g���t�@�C���o�͂���
    ''' </summary>
    ''' <param name="strFileNm">�o�̓t�@�C����</param>
    ''' <param name="dtTable">�f�[�^�e�[�u�����o�͗p�ɐ��`�ς̂���</param>
    ''' <param name="strQuote">�Z�p���[�^(���蕶��)</param>
    ''' <param name="strDelimiter">�f���~�^(��؂蕶��)</param>
    ''' <param name="strLineFeedCd">���s�R�[�h(1���R�[�h�I�[����)</param>
    ''' <param name="blnHeaderUmu">�w�b�_�[���̗L��</param>
    ''' <returns>True,False</returns>
    ''' <remarks></remarks>
    Public Function OutPutFileFromDtTable( _
                                             ByVal strFileNm As String _
                                            , ByRef dtTable As DataTable _
                                            , Optional ByVal strQuote As String = EarthConst.CSV_QUOTE _
                                            , Optional ByVal strDelimiter As String = EarthConst.CSV_DELIMITER _
                                            , Optional ByVal strLineFeedCd As String = vbCrLf _
                                            , Optional ByVal blnHeaderUmu As Boolean = True _
                                            ) As Boolean

        Dim sbOutput As New StringBuilder '�o�͗p������

        '�t�@�C����
        If strFileNm = String.Empty Then
            '���w�莞�͏o�͂��Ȃ�
            Return False
        Else
            '�t�@�C�����̒u��
            strFileNm = strFileNm.Replace("@yyyyMMddHHmmss@", DateTime.Now.ToString(EarthConst.FORMAT_DATE_TIME_6))
        End If

        '�擾�f�[�^���o�͗p������Ɋi�[
        Dim strErrRet As String = Me.dtTableToStringOutput(sbOutput, dtTable, strQuote, strDelimiter, strLineFeedCd, blnHeaderUmu)
        If strErrRet <> String.Empty Then
            ' �o�͗p�����񂪂Ȃ��̂ŁA�����I��
            Return False
        End If

        Dim httpRes As HttpResponse = HttpContext.Current.Response

        '�t�@�C���̏o�͂��s��
        With httpRes
            .Clear()
            .AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(strFileNm))
            .ContentType = "text/plain"
            .BinaryWrite(System.Text.Encoding.GetEncoding("Shift-JIS").GetBytes(sbOutput.ToString))
            .End()
        End With

        Return True
    End Function

#End Region

#Region "�f�[�^�e�[�u���̓��e���e�L�X�g������"
    ''' <summary>
    ''' �f�[�^�e�[�u���̓��e���e�L�X�g������
    ''' </summary>
    ''' <param name="sbText">�o�͗pStringBuilder</param>
    ''' <param name="dtTable">�o�͑Ώۃf�[�^�e�[�u��</param>
    ''' <param name="strQuote">�Z�p���[�^(���蕶��)</param>
    ''' <param name="strDelimiter">�f���~�^(��؂蕶��)</param>
    ''' <param name="strLineFeedCd">���s�R�[�h(1���R�[�h�I�[����)</param>
    ''' <param name="blnHeaderUmu">�w�b�_�[���̗L��</param>
    ''' <returns>���^�[�����b�Z�[�W</returns>
    ''' <remarks></remarks>
    Public Function dtTableToStringOutput( _
                                        ByRef sbText As StringBuilder _
                                        , ByVal dtTable As DataTable _
                                        , Optional ByVal strQuote As String = EarthConst.CSV_QUOTE _
                                        , Optional ByVal strDelimiter As String = EarthConst.CSV_DELIMITER _
                                        , Optional ByVal strLineFeedCd As String = vbCrLf _
                                        , Optional ByVal blnHeaderUmu As Boolean = True _
                                        ) As String

        Dim strRetMsg As String            '���^�[�����b�Z�[�W
        Dim sbTmp As New StringBuilder()   '�o�͗pString�Z�b�g�pStrinBuilder
        Dim rowSb As New StringBuilder()   '��ƗpString�Z�b�g�pStrinBuilder
        Dim blnSentou As Boolean = True          '�J�E���^

        Dim strTmpVal As String = String.Empty
        '�J�E���^
        Dim intCnt As Integer = 0
        '�o�͍ő匏��
        Dim end_count As Integer = Integer.Parse(EarthConst.MAX_CSV_OUTPUT)

        Try
            '�w�b�_�[��񂠂�̏ꍇ
            If blnHeaderUmu Then

                '********************
                '* �w�b�_�s�쐬
                '********************
                '�w�b�_�i�[�p�f�[�^�e�[�u��
                Dim dtHeadTbl As DataTable
                '�w�b�_�i�[�p�f�[�^�e�[�u���̗�
                Dim dtCol As DataColumn

                '�w�b�_�i�[�p�f�[�^�e�[�u�����C���X�^���X����
                dtHeadTbl = New DataTable

                '�o�͂���f�[�^�e�[�u���̗񕪃��[�v
                For intColCnt As Integer = 0 To dtTable.Columns.Count - 1
                    '�w�b�_�i�[�p�f�[�^�e�[�u���̗���C���X�^���X����
                    dtCol = New DataColumn
                    '�w�b�_�i�[�p�f�[�^�e�[�u����̑�����ݒ�
                    dtCol.DataType = System.Type.GetType("System.String")
                    '�w�b�_�i�[�p�f�[�^�e�[�u������f�[�^�e�[�u���ɒǉ�
                    dtHeadTbl.Columns.Add(dtCol)
                Next

                '�w�b�_�i�[�p�f�[�^�e�[�u���̍s
                Dim dtRow As DataRow
                '�w�b�_�i�[�p�f�[�^�e�[�u���̍s���C���X�^���X����
                dtRow = dtHeadTbl.NewRow

                '�w�b�_�i�[�p�f�[�^�e�[�u���̃J���������[�v
                For intColCnt As Integer = 0 To dtHeadTbl.Columns.Count - 1
                    '�o�͂���f�[�^�e�[�u���̃J���������A�C���X�^���X���������w�b�_�i�[�p�f�[�^�e�[�u���s�ɃZ�b�g���Ă���
                    dtRow(intColCnt) = dtTable.Columns(intColCnt).ColumnName
                Next
                dtHeadTbl.Rows.Add(dtRow)

                '�쐬�����w�b�_�i�[�p�f�[�^�e�[�u���̍s���e�L�X�g�t�@�C����1�s�ڂփZ�b�g
                rowSb = New StringBuilder()
                blnSentou = True
                For Each tmpcol As Object In dtHeadTbl.Rows(0).ItemArray
                    strTmpVal = tmpcol.ToString

                    If blnSentou Then '�擪��
                        rowSb.Append(strQuote & strTmpVal & strQuote)
                        blnSentou = False
                    Else
                        rowSb.Append(strDelimiter & strQuote & strTmpVal & strQuote)
                    End If
                Next
                rowSb.Append(vbCrLf) '���s
                sbTmp.Append(rowSb.ToString)

            End If

            '********************
            '* �f�[�^�s�쐬
            '********************
            '�f�[�^�e�[�u���̓��e��String��
            For Each tmpRow As DataRow In dtTable.Rows
                intCnt += 1
                If intCnt > end_count Then
                    Exit For
                End If
                rowSb = New StringBuilder()
                blnSentou = True
                For Each tmpCol As Object In tmpRow.ItemArray
                    strTmpVal = tmpCol.ToString

                    If blnSentou Then '�擪��
                        rowSb.Append(strQuote & strTmpVal & strQuote)
                        blnSentou = False
                    Else
                        rowSb.Append(strDelimiter & strQuote & strTmpVal & strQuote)
                    End If
                Next
                rowSb.Append(vbCrLf) '���s
                sbTmp.Append(rowSb.ToString)
            Next

            '�o�͗pString�ɃZ�b�g
            sbText = sbTmp

            '�f�[�^�`�F�b�N
            If sbText.Length > 0 Then
                strRetMsg = String.Empty
            Else
                strRetMsg = Messages.MSG020E
            End If

        Catch ex As Exception
            strRetMsg = ex.Message
        End Try

        Return strRetMsg
    End Function
#End Region

#End Region

#Region "ToolTip��ݒ�"
    ''' <summary>
    ''' Html�R���g���[���Ƀc�[���`�b�v��ݒ肷��
    ''' </summary>
    ''' <param name="targetCtrl">�Ώۂ�Html�R���g���[��</param>
    ''' <param name="strDispTip">�\�����镶����</param>
    ''' <remarks></remarks>
    Public Sub SetToolTipForCtrl(ByVal targetCtrl As HtmlControl, ByVal strDispTip As String)
        targetCtrl.Attributes("title") = strDispTip
    End Sub

#Region "ToolTip�ݒ�^�C�v"
    ''' <summary>
    ''' ToolTip�ݒ�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emToolTipSetType
        ''' <summary>
        ''' ���������s��
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoHakDate = 1
        ''' <summary>
        ''' �������ߓ�
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuuSimeDate = 2
    End Enum
#End Region

    ''' <summary>
    ''' (11) �������ʈꗗ�����F���]����/�������ꗗ��ʁA�ߋ��������ꗗ���
    ''' </summary>
    ''' <param name="dtRec">�����f�[�^���R�[�h�N���X</param>
    ''' <param name="targetCtrl">�ΏۃR���g���[��</param>
    ''' <param name="emType">�Z�b�g�^�C�v</param>
    ''' <remarks></remarks>
    Public Sub setSeikyuusyoToolTip(ByVal dtRec As SeikyuuDataRecord _
                                        , ByVal targetCtrl As HtmlControl _
                                        , ByVal emType As emToolTipSetType _
                                        )

        Const TOOLTIP1 As String = "������}�X�^�̐������ߓ��Ɛ����f�[�^�̐������ߓ����قȂ�܂��B" & vbCrLf
        Const TOOLTIP2 As String = "������}�X�^�̐������ߓ��Ɛ����f�[�^�̐��������s�����قȂ�܂��B" & vbCrLf
        Const TOOLTIP3 As String = "�����f�[�^�̐��������s���ƈقȂ鐿���N������������f�[�^���܂܂�Ă��܂��B" & vbCrLf

        '������T.�������ߓ�(���ɂ��̂�)
        Dim strTmpRecSimeDate As String = IIf(dtRec.SeikyuuSimeDate Is Nothing, String.Empty, dtRec.SeikyuuSimeDate)
        Dim intTmpRecSimeDate As Integer
        If strTmpRecSimeDate = String.Empty Then
            intTmpRecSimeDate = Integer.MinValue
        Else
            intTmpRecSimeDate = Integer.Parse(strTmpRecSimeDate)
        End If

        '������T.���������s��(���ɂ��̂�)
        Dim intTmpRecHakDate As Integer
        Dim dtSeikyuusyoHakDate As Date = dtRec.SeikyuusyoHakDate
        If dtSeikyuusyoHakDate = Date.MinValue Then
            intTmpRecHakDate = Integer.MinValue
        Else
            intTmpRecHakDate = Integer.Parse(dtSeikyuusyoHakDate.Day.ToString)
        End If

        '������M.�������ߓ�(���ɂ��̂�)
        Dim strTmpMstSimeDate As String = IIf(dtRec.SeikyuuSimeDateMst Is Nothing, String.Empty, dtRec.SeikyuuSimeDateMst)
        Dim intTmpMstSimeDate As Integer
        If strTmpMstSimeDate = String.Empty Then
            intTmpMstSimeDate = Integer.MinValue
        Else
            intTmpMstSimeDate = Integer.Parse(strTmpMstSimeDate)
        End If

        '������T.���������s��(�N��)�Ɛ�����M.�������ߓ�(��)����ɐ��������s�����擾
        Dim dtTmpMstSeikyuusyoHakDate As Date
        Dim strTmpMstSeikyuusyoHakDate As String
        Dim cBizLogic As New CommonBizLogic
        If dtSeikyuusyoHakDate = Date.MinValue Or strTmpMstSimeDate = String.Empty Then
            dtTmpMstSeikyuusyoHakDate = Date.MinValue
        Else
            strTmpMstSeikyuusyoHakDate = cBizLogic.GetDateStrReplaceDay(dtSeikyuusyoHakDate, strTmpMstSimeDate)
            If Not IsDate(strTmpMstSeikyuusyoHakDate) Then
                strTmpMstSeikyuusyoHakDate = cBizLogic.GetEndOfMonth(dtSeikyuusyoHakDate)
            End If
            dtTmpMstSeikyuusyoHakDate = Date.Parse(strTmpMstSeikyuusyoHakDate)
        End If

        '���������s���̍��كt���O(1:���ق���A0:���قȂ��ANULL:�����ꂩNULL�l�̂��߁A���ق���Ƃ݂Ȃ�)
        Dim intSeikyuuDateFlg As Integer = IIf(dtRec.SeikyuuDateSaiFlg = Integer.MinValue, 1, dtRec.SeikyuuDateSaiFlg)

        Dim strTip1 As String = String.Empty
        Dim strTip2 As String = String.Empty
        Dim strTip3 As String = String.Empty

        Dim strSetTip As String = String.Empty

        '�ΏۃR���g���[���ʂɐݒ�
        Select Case emType
            Case emToolTipSetType.SeikyuuSimeDate '�y�P�z�������ߓ�����
                '1. ������}�X�^.�������ߓ��Ɛ�����.�������ߓ��̔�r
                If intTmpMstSimeDate <> intTmpRecSimeDate Then
                    strSetTip &= TOOLTIP1
                End If

            Case emToolTipSetType.SeikyuusyoHakDate '�y�Q�z���������s������
                '1.������}�X�^.�������ߓ��Ɛ�����T.���������s���i�f�[�^�j�̔�r
                If dtTmpMstSeikyuusyoHakDate <> dtSeikyuusyoHakDate Then
                    strSetTip &= TOOLTIP2
                End If
                '2.������T.���������s���Ɛ����Ӗ���T.������NO�ɕR�t������`�[�̐����N�����̔�r
                If intSeikyuuDateFlg = 1 Then
                    strSetTip &= TOOLTIP3
                End If
            Case Else
                Exit Sub
        End Select

        '���c�[���`�b�v�ݒ�
        If strSetTip <> String.Empty Then
            Me.SetToolTipForCtrl(targetCtrl, strSetTip)
            '�X�^�C���ݒ�
            setStyleRedBold(targetCtrl.Style, True)
        End If

    End Sub

#End Region

#Region "�����挟���|�b�v�A�b�v"
    ''' <summary>
    ''' �����挟����ʌďo����
    ''' </summary>
    ''' <param name="SelectKbn">������敪�h���b�v�_�E�����X�g</param>
    ''' <param name="TextCd">������R�[�h�e�L�X�g�{�b�N�X</param>
    ''' <param name="TextBrc">������}�ԃe�L�X�g�{�b�N�X</param>
    ''' <param name="TextMei">�����於�e�L�X�g�{�b�N�X</param>
    ''' <param name="HiddenCall">�E�B���h�E�ďo�����fHidden</param>
    ''' <param name="HiddenOld">�ύX�O������i�[Hidden�z��(�敪,�ԍ�,�}�Ԃ̏��ŃZ�b�g)</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="objPage">�y�[�W�N���X�I�u�W�F�N�g</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks>�e�L�X�g�{�b�N�XWebControl�o�[�W����</remarks>
    Public Function CallSeikyuuSakiSearchWindow(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal objPage As Page _
                                            , ByVal SelectKbn As DropDownList _
                                            , ByVal TextCd As TextBox _
                                            , ByVal TextBrc As TextBox _
                                            , ByVal TextMei As TextBox _
                                            , ByVal ButtonSearch As HtmlInputButton _
                                            , Optional ByVal HiddenOld() As Object = Nothing _
                                            , Optional ByVal HiddenCall As Object = Nothing _
                                            ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallSeikyuuSakiSearch", _
                                                    SelectKbn, _
                                                    TextCd, _
                                                    TextBrc, _
                                                    TextMei, _
                                                    objPage)
        Dim strSkKbn As String      '������敪
        Dim strSkCd As String       '������R�[�h
        Dim strSkBrc As String      '������}��
        Dim strSkKbnClId As String      '������敪ID
        Dim strSkCdClId As String       '������R�[�hID
        Dim strSkBrcClId As String      '������}��ID
        Dim strSkMeiClId As String      '�����於ID

        Dim tmpScript As String = String.Empty

        '��ʂ��琿��������擾
        strSkKbn = IIf(SelectKbn.SelectedValue <> String.Empty, SelectKbn.SelectedValue, String.Empty)
        strSkCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        strSkBrc = IIf(TextBrc.Text <> String.Empty, TextBrc.Text, String.Empty)
        strSkKbnClId = SelectKbn.ClientID
        strSkCdClId = TextCd.ClientID
        strSkBrcClId = TextBrc.ClientID
        strSkMeiClId = TextMei.ClientID

        Dim dicSeikyuu As New Dictionary(Of String, String)
        dicSeikyuu.Add(strSkKbnClId, strSkKbn)
        dicSeikyuu.Add(strSkCdClId, strSkCd)
        dicSeikyuu.Add(strSkBrcClId, strSkBrc)
        dicSeikyuu.Add(strSkMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallSeikyuuSakiSearchWindowCmn(sender _
                                                    , strSkKbn _
                                                    , strSkCd _
                                                    , strSkBrc _
                                                    , strSkKbnClId _
                                                    , strSkCdClId _
                                                    , strSkBrcClId _
                                                    , strSkMeiClId _
                                                    , ButtonSearch _
                                                    , dicSeikyuu _
                                                    , HiddenOld _
                                                    , HiddenCall)

        SelectKbn.SelectedValue = dicSeikyuu(strSkKbnClId)
        TextCd.Text = dicSeikyuu(strSkCdClId)
        TextBrc.Text = dicSeikyuu(strSkBrcClId)
        TextMei.Text = dicSeikyuu(strSkMeiClId)

        Return blnResult

    End Function

    ''' <summary>
    ''' �����挟����ʌďo����
    ''' </summary>
    ''' <param name="SelectKbn">������敪�h���b�v�_�E�����X�g</param>
    ''' <param name="TextCd">������R�[�h�e�L�X�g�{�b�N�X</param>
    ''' <param name="TextBrc">������}�ԃe�L�X�g�{�b�N�X</param>
    ''' <param name="TextMei">�����於�e�L�X�g�{�b�N�X</param>
    ''' <param name="HiddenCall">�E�B���h�E�ďo�����fHidden</param>
    ''' <param name="HiddenOld">�ύX�O������i�[Hidden</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="objPage">�y�[�W�N���X�I�u�W�F�N�g</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks>�e�L�X�g�{�b�N�XHtmlControl�o�[�W����</remarks>
    Public Function CallSeikyuuSakiSearchWindow(ByVal sender As System.Object _
                                            , ByVal e As System.EventArgs _
                                            , ByVal objPage As Page _
                                            , ByVal SelectKbn As DropDownList _
                                            , ByVal TextCd As HtmlInputText _
                                            , ByVal TextBrc As HtmlInputText _
                                            , ByVal TextMei As HtmlInputText _
                                            , ByVal ButtonSearch As HtmlInputButton _
                                            , Optional ByVal HiddenOld() As Object = Nothing _
                                            , Optional ByVal HiddenCall As Object = Nothing _
                                            ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallSeikyuuSakiSearchWindow", _
                                                    SelectKbn, _
                                                    TextCd, _
                                                    TextBrc, _
                                                    TextMei, _
                                                    objPage)
        Dim strSkKbn As String      '������敪
        Dim strSkCd As String       '������R�[�h
        Dim strSkBrc As String      '������}��
        Dim strSkKbnClId As String      '������敪ID
        Dim strSkCdClId As String       '������R�[�hID
        Dim strSkBrcClId As String      '������}��ID
        Dim strSkMeiClId As String      '�����於ID

        Dim tmpScript As String = String.Empty

        '��ʂ��琿��������擾
        strSkKbn = IIf(SelectKbn.SelectedValue <> String.Empty, SelectKbn.SelectedValue, String.Empty)
        strSkCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        strSkBrc = IIf(TextBrc.Value <> String.Empty, TextBrc.Value, String.Empty)
        strSkKbnClId = SelectKbn.ClientID
        strSkCdClId = TextCd.ClientID
        strSkBrcClId = TextBrc.ClientID
        strSkMeiClId = TextMei.ClientID

        Dim dicSeikyuu As New Dictionary(Of String, String)
        dicSeikyuu.Add(strSkKbnClId, strSkKbn)
        dicSeikyuu.Add(strSkCdClId, strSkCd)
        dicSeikyuu.Add(strSkBrcClId, strSkBrc)
        dicSeikyuu.Add(strSkMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallSeikyuuSakiSearchWindowCmn(sender _
                                                    , strSkKbn _
                                                    , strSkCd _
                                                    , strSkBrc _
                                                    , strSkKbnClId _
                                                    , strSkCdClId _
                                                    , strSkBrcClId _
                                                    , strSkMeiClId _
                                                    , ButtonSearch _
                                                    , dicSeikyuu _
                                                    , HiddenOld _
                                                    , HiddenCall)

        SelectKbn.SelectedValue = dicSeikyuu(strSkKbnClId)
        TextCd.Value = dicSeikyuu(strSkCdClId)
        TextBrc.Value = dicSeikyuu(strSkBrcClId)
        TextMei.Value = dicSeikyuu(strSkMeiClId)

        Return blnResult

    End Function

    ''' <summary>
    ''' �����挟����ʌďo�����m���ʁn
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strSkKbn">������敪</param>
    ''' <param name="strSkCd">������R�[�h</param>
    ''' <param name="strSkBrc">������}��</param>
    ''' <param name="strSkKbnClId">������敪�h���b�v�_�E�����X�gID</param>
    ''' <param name="strSkCdClId">������R�[�h�e�L�X�g�{�b�N�XID</param>
    ''' <param name="strSkBrcClId">������}�ԃe�L�X�g�{�b�N�XID</param>
    ''' <param name="strSkMeiClId">�����於�e�L�X�g�{�b�N�XID</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="dicRet">��������i�[Dictionary</param>
    ''' <param name="HiddenOld">�ύX�O������i�[Hidden</param>
    ''' <param name="HiddenCall">�E�B���h�E�ďo�����fHidden</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks></remarks>
    Private Function CallSeikyuuSakiSearchWindowCmn(ByVal sender As System.Object _
                                                    , ByVal strSkKbn As String _
                                                    , ByVal strSkCd As String _
                                                    , ByVal strSkBrc As String _
                                                    , ByVal strSkKbnClId As String _
                                                    , ByVal strSkCdClId As String _
                                                    , ByVal strSkBrcClId As String _
                                                    , ByVal strSkMeiClId As String _
                                                    , ByVal ButtonSearch As HtmlInputButton _
                                                    , ByRef dicRet As Dictionary(Of String, String) _
                                                    , Optional ByRef HiddenOld() As Object = Nothing _
                                                    , Optional ByRef HiddenCall As Object = Nothing _
                                                    ) As Boolean
        Dim intAllCnt As Integer
        Dim list As New List(Of SeikyuuSakiInfoRecord)
        Dim uriageLogic As New UriageDataSearchLogic

        If strSkKbn <> String.Empty Or strSkCd <> String.Empty Or strSkBrc <> String.Empty Then
            intAllCnt = uriageLogic.GetSeikyuuSakiCnt(strSkCd, strSkBrc, strSkKbn, String.Empty, String.Empty, True)
            If intAllCnt = 1 Then
                list = uriageLogic.GetSeikyuuSakiInfo(strSkCd, strSkBrc, strSkKbn, String.Empty, String.Empty, intAllCnt, 1, 10, True)
            End If
        End If

        If intAllCnt = 1 Then
            Dim recData As SeikyuuSakiInfoRecord = list(0)
            dicRet(strSkKbnClId) = GetDisplayString(recData.SeikyuuSakiKbn)
            dicRet(strSkCdClId) = GetDisplayString(recData.SeikyuuSakiCd)
            dicRet(strSkBrcClId) = GetDisplayString(recData.SeikyuuSakiBrc)
            dicRet(strSkMeiClId) = GetDisplayString(recData.SeikyuuSakiMei)

            If HiddenOld IsNot Nothing Then
                If dicRet(strSkKbnClId) = String.Empty _
                        And dicRet(strSkCdClId) = String.Empty _
                        And dicRet(strSkBrcClId) = String.Empty Then
                    For intCnt As Integer = LBound(HiddenOld) To UBound(HiddenOld)
                        HiddenOld(intCnt).value = String.Empty
                    Next
                Else
                    If HiddenOld.Length = 1 Then
                        HiddenOld(0).value = dicRet(strSkKbnClId) & EarthConst.SEP_STRING _
                                            & dicRet(strSkCdClId) & EarthConst.SEP_STRING _
                                            & dicRet(strSkBrcClId)
                    Else
                        HiddenOld(0).value = dicRet(strSkKbnClId)
                        HiddenOld(1).value = dicRet(strSkCdClId)
                        HiddenOld(2).value = dicRet(strSkBrcClId)
                    End If
                End If
            End If
            Return True
        Else
            '�����於���N���A
            dicRet(strSkMeiClId) = String.Empty

            '�t�H�[�J�X�Z�b�g
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & strSkCdClId & EarthConst.SEP_STRING & _
                                            strSkBrcClId & EarthConst.SEP_STRING & _
                                            strSkKbnClId & "','" _
                                        & UrlConst.SEARCH_SEIKYUU_SAKI & "','" _
                                        & strSkCdClId & EarthConst.SEP_STRING & _
                                            strSkBrcClId & EarthConst.SEP_STRING & _
                                            strSkKbnClId & EarthConst.SEP_STRING & _
                                            strSkMeiClId & "','" _
                                        & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript

            If HiddenCall IsNot Nothing Then
                If HiddenCall.Value <> String.Empty Then
                    ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
                End If
            Else
                ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            End If
            Return False
        End If

    End Function
#End Region

#Region "�����X�����|�b�v�A�b�v"
    ''' <summary>
    ''' �����X������ʌďo����
    ''' </summary>
    ''' <param name="objPage">�y�[�W�N���X�I�u�W�F�N�g</param>
    ''' <param name="strKbnId">�敪ClientID</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="TextCd">�����X�R�[�h�e�L�X�g�{�b�N�X</param>
    ''' <param name="TextMei">�����X���e�L�X�g�{�b�N�X</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <param name="TextTorikesi">�����X������R�e�L�X�g�{�b�N�X</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallKameitenSearchWindow(ByVal sender As System.Object _
                                             , ByVal e As System.EventArgs _
                                             , ByVal objPage As Page _
                                             , ByVal strKbnId As String _
                                             , ByVal strKbn As String _
                                             , ByVal TextCd As Object _
                                             , ByVal TextMei As Object _
                                             , ByVal ButtonSearch As Object _
                                             , Optional ByVal blnTorikesi As Boolean = False _
                                             , Optional ByVal TextTorikesi As Object = Nothing _
                                             ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallKameitenSearchWindow" _
                                                    , objPage _
                                                    , strKbnId _
                                                    , strKbn _
                                                    , TextCd _
                                                    , TextMei _
                                                    , TextTorikesi _
                                                    , ButtonSearch _
                                                    , blnTorikesi _
                                                    , TextTorikesi)

        Dim strKameitenCd As String = String.Empty  '�����X�R�[�h
        Dim strKameitenCdClId As String             '�����X�R�[�hID
        Dim strKameitenMeiClId As String            '�����X��ID
        Dim strTorikeiClId As String                '�����������RID
        Dim tmpScript As String = String.Empty

        '��ʂ�������X�����擾
        If TextCd.GetType.Name = "HtmlInputText" Then
            strKameitenCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            strKameitenCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        End If
        strKameitenCdClId = TextCd.ClientID
        strKameitenMeiClId = TextMei.ClientID

        Dim dicKameiten As New Dictionary(Of String, String)
        dicKameiten.Add(strKbnId, strKbn)
        dicKameiten.Add(strKameitenCdClId, strKameitenCd)
        dicKameiten.Add(strKameitenMeiClId, String.Empty)

        '������R
        If TextTorikesi Is Nothing Then
            strTorikeiClId = ""
        Else
            strTorikeiClId = TextTorikesi.ClientID
            dicKameiten.Add(strTorikeiClId, String.Empty)
            dicKameiten.Add(EarthConst.STYLE_FONT_COLOR, String.Empty)
        End If

        Dim blnResult As Boolean
        blnResult = CallKameitenSearchWindowCmn(sender _
                                                , strKbn _
                                                , strKameitenCd _
                                                , strKbnId _
                                                , strKameitenCdClId _
                                                , strKameitenMeiClId _
                                                , strTorikeiClId _
                                                , ButtonSearch _
                                                , dicKameiten _
                                                , blnTorikesi)

        '�����X�R�[�h
        If TextCd.GetType.Name = "HtmlInputText" Then
            TextCd.Value = dicKameiten(strKameitenCdClId)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            TextCd.Text = dicKameiten(strKameitenCdClId)
        End If
        '�����X��
        If TextMei.GetType.Name = "HtmlInputText" Then
            TextMei.Value = dicKameiten(strKameitenMeiClId)
        ElseIf TextMei.GetType.Name = "TextBox" Then
            TextMei.Text = dicKameiten(strKameitenMeiClId)
        End If

        If strTorikeiClId <> "" Then
            If dicKameiten.ContainsKey(strTorikeiClId) Then
                '������R
                If TextTorikesi.GetType.Name = "HtmlInputText" Then
                    TextTorikesi.Value = dicKameiten(strTorikeiClId)
                ElseIf TextTorikesi.GetType.Name = "TextBox" Then
                    TextTorikesi.Text = dicKameiten(strTorikeiClId)
                End If
                '�F�ւ�����
                setStyleFontColor(TextCd.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
                setStyleFontColor(TextMei.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
                setStyleFontColor(TextTorikesi.Style, dicKameiten(EarthConst.STYLE_FONT_COLOR))
            End If
        End If

        Return blnResult

    End Function

    ''' <summary>
    ''' �����X������ʌďo�����m���ʁn
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKbnClId">�敪�e�L�X�g�{�b�N�XID</param>
    ''' <param name="strKameitenCdClId">�����X�R�[�h�e�L�X�g�{�b�N�XID</param>
    ''' <param name="strKameitenMeiClId">�����X���e�L�X�g�{�b�N�XID</param>
    ''' <param name="strTorikesiRiyuuClId">�����X������R�e�L�X�g�{�b�N�XID</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="dicRet">�����X���i�[Dictionary</param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallKameitenSearchWindowCmn(ByVal sender As System.Object _
                                                 , ByVal strKbn As String _
                                                 , ByVal strKameitenCd As String _
                                                 , ByVal strKbnClId As String _
                                                 , ByVal strKameitenCdClId As String _
                                                 , ByVal strKameitenMeiClId As String _
                                                 , ByVal strTorikesiRiyuuClId As String _
                                                 , ByVal ButtonSearch As HtmlInputButton _
                                                 , ByRef dicRet As Dictionary(Of String, String) _
                                                 , Optional ByVal blnTorikesi As Boolean = False _
                                                 ) As Boolean

        Dim list As New List(Of KameitenSearchRecord)
        Dim kLogic As New KameitenSearchLogic
        Dim allRowCount As Integer = 0

        If strKameitenCd <> String.Empty Then
            list = kLogic.GetKameitenSearchResult(strKbn, _
                                                  strKameitenCd, _
                                                  blnTorikesi, _
                                                  allRowCount)

        End If

        If allRowCount = 1 Then
            Dim recData As KameitenSearchRecord = list(0)
            dicRet(strKameitenCdClId) = GetDisplayString(recData.KameitenCd)
            dicRet(strKameitenMeiClId) = GetDisplayString(recData.KameitenMei1)
            If strTorikesiRiyuuClId = "" Then
                dicRet(EarthConst.STYLE_FONT_COLOR) = Me.getKameitenFontColor(0)
            Else
                dicRet(strTorikesiRiyuuClId) = Me.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu)
                dicRet(EarthConst.STYLE_FONT_COLOR) = Me.getKameitenFontColor(recData.Torikesi)
            End If
            Return True
        Else
            '�����X���A������R���N���A
            dicRet(strKameitenMeiClId) = String.Empty
            If strTorikesiRiyuuClId <> "" Then
                dicRet(strTorikesiRiyuuClId) = String.Empty
            End If

            '�t�H�[�J�X�Z�b�g
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & strKbnClId & EarthConst.SEP_STRING _
                                       & strKameitenCdClId & "','" _
                                       & UrlConst.SEARCH_KAMEITEN & "','" _
                                       & strKameitenCdClId & EarthConst.SEP_STRING _
                                       & strKameitenMeiClId & "','" _
                                       & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Return False
        End If

    End Function
#End Region

#Region "�c�Ə������|�b�v�A�b�v"
    ''' <summary>
    ''' �c�Ə�������ʌďo����
    ''' </summary>
    ''' <param name="objPage">�y�[�W�N���X�I�u�W�F�N�g</param>
    ''' <param name="TextCd">�c�Ə��R�[�h�e�L�X�g�{�b�N�X</param>
    ''' <param name="TextMei">�c�Ə����e�L�X�g�{�b�N�X</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallEigyousyoSearchWindow(ByVal sender As System.Object _
                                             , ByVal e As System.EventArgs _
                                             , ByVal objPage As Page _
                                             , ByVal TextCd As Object _
                                             , ByVal TextMei As Object _
                                             , ByVal ButtonSearch As Object _
                                             , Optional ByVal blnTorikesi As Boolean = False _
                                             ) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CallKameitenSearchWindow" _
                                                    , objPage _
                                                    , TextCd _
                                                    , TextMei _
                                                    , ButtonSearch _
                                                    , blnTorikesi)

        Dim strEigyousyoCd As String = String.Empty  '�c�Ə��R�[�h
        Dim strEigyousyoCdClId As String             '�c�Ə��R�[�hID
        Dim strEigyousyoMeiClId As String            '�c�Ə���ID
        Dim tmpScript As String = String.Empty

        '��ʂ�������X�����擾
        If TextCd.GetType.Name = "HtmlInputText" Then
            strEigyousyoCd = IIf(TextCd.Value <> String.Empty, TextCd.Value, String.Empty)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            strEigyousyoCd = IIf(TextCd.Text <> String.Empty, TextCd.Text, String.Empty)
        End If
        strEigyousyoCdClId = TextCd.ClientID
        strEigyousyoMeiClId = TextMei.ClientID

        Dim dicEigyousyo As New Dictionary(Of String, String)
        dicEigyousyo.Add(strEigyousyoCdClId, strEigyousyoCd)
        dicEigyousyo.Add(strEigyousyoMeiClId, String.Empty)

        Dim blnResult As Boolean
        blnResult = CallEigyousyoSearchWindowCmn(sender _
                                                , strEigyousyoCd _
                                                , strEigyousyoCdClId _
                                                , strEigyousyoMeiClId _
                                                , ButtonSearch _
                                                , dicEigyousyo _
                                                , blnTorikesi)

        '�c�Ə��R�[�h
        If TextCd.GetType.Name = "HtmlInputText" Then
            TextCd.Value = dicEigyousyo(strEigyousyoCdClId)
        ElseIf TextCd.GetType.Name = "TextBox" Then
            TextCd.Text = dicEigyousyo(strEigyousyoCdClId)
        End If
        '�c�Ə���
        If TextMei.GetType.Name = "HtmlInputText" Then
            TextMei.Value = dicEigyousyo(strEigyousyoMeiClId)
        ElseIf TextMei.GetType.Name = "TextBox" Then
            TextMei.Text = dicEigyousyo(strEigyousyoMeiClId)
        End If

        Return blnResult

    End Function

    ''' <summary>
    ''' �c�Ə�������ʌďo�����m���ʁn
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strEigyousyoCd">�c�Ə��R�[�h</param>
    ''' <param name="strEigyousyoCdClId">�c�Ə��R�[�h�e�L�X�g�{�b�N�XID</param>
    ''' <param name="strEigyousyoMeiClId">�c�Ə����e�L�X�g�{�b�N�XID</param>
    ''' <param name="ButtonSearch">�����{�^��</param>
    ''' <param name="dicRet">�c�Ə����i�[Dictionary</param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <returns>��������(True/False)</returns>
    ''' <remarks></remarks>
    Public Function CallEigyousyoSearchWindowCmn(ByVal sender As System.Object _
                                                 , ByVal strEigyousyoCd As String _
                                                 , ByVal strEigyousyoCdClId As String _
                                                 , ByVal strEigyousyoMeiClId As String _
                                                 , ByVal ButtonSearch As HtmlInputButton _
                                                 , ByRef dicRet As Dictionary(Of String, String) _
                                                 , Optional ByVal blnTorikesi As Boolean = False _
                                                 ) As Boolean

        Dim list As New List(Of EigyousyoSearchRecord)
        Dim eLogic As New EigyousyoSearchLogic
        Dim allRowCount As Integer = 0

        If strEigyousyoCd <> String.Empty Then
            list = eLogic.GetEigyousyoSearchResult(strEigyousyoCd _
                                                   , String.Empty _
                                                   , blnTorikesi _
                                                   , allRowCount)

        End If

        If allRowCount = 1 Then
            Dim recData As EigyousyoSearchRecord = list(0)
            dicRet(strEigyousyoCdClId) = GetDisplayString(recData.EigyousyoCd)
            dicRet(strEigyousyoMeiClId) = GetDisplayString(recData.EigyousyoMei)

            Return True
        Else
            '�c�Ə������N���A
            dicRet(strEigyousyoMeiClId) = String.Empty

            '�t�H�[�J�X�Z�b�g
            Dim tmpFocusScript = "objEBI('" & ButtonSearch.ClientID & "').focus();"
            Dim tmpScript As String = String.Empty

            '������ʕ\���pJavaScript�wcallSearch�x�����s
            tmpScript = "callSearch('" & strEigyousyoCdClId & "','" _
                                       & UrlConst.SEARCH_EIGYOUSYO & "','" _
                                       & strEigyousyoCdClId & EarthConst.SEP_STRING _
                                       & strEigyousyoMeiClId & "','" _
                                       & ButtonSearch.ClientID & "');"
            tmpScript = tmpFocusScript + tmpScript
            ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "callSaerch", tmpScript, True)
            Return False
        End If

    End Function
#End Region

    ''' <summary>
    ''' �Q�ƃ��[�h�擾���\�b�h
    ''' </summary>
    ''' <param name="strMode">���[�h�ݒ�i"1" or "1"�ȊO)</param>
    ''' <param name="intKeiriKengen">�o������</param>
    ''' <returns>strMode��"1"�̏ꍇ�A�Q�ƃ��[�h������^strMode��"1"�ȊO�̏ꍇ�A�u�����N</returns>
    ''' <remarks></remarks>
    Public Function GetViewMode(ByVal strMode As String, Optional ByVal intKeiriKengen As Integer = 0) As String
        Dim strViewMode As String

        If strMode = EarthConst.URIAGE_ZUMI_CODE Then
            strViewMode = EarthConst.MODE_VIEW
        Else
            strViewMode = String.Empty
        End If

        '�o������������ꍇ�͎Q�ƃ��[�h�𖢐ݒ�
        If intKeiriKengen = -1 Then
            strViewMode = String.Empty
        End If

        Return strViewMode

    End Function

    ''' <summary>
    ''' �������(���ڐ����E������)���擾
    ''' </summary>
    ''' <param name="ctrlLink">������E�d���惊���N</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKojKaisyaCd">�H����ЃR�[�h</param>
    ''' <returns>������ʁi"���ڐ���"/"������"�j</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuTypeStr(ByVal ctrlLink As SeikyuuSiireLinkCtrl _
                                    , ByVal strKameitenCd As String _
                                    , ByVal strSyouhinCd As String _
                                    , Optional ByVal strKojKaisyaCd As String = "") As String
        Dim strSeikyuuSakiCd As String = ctrlLink.AccSeikyuuSakiCd.Value
        Dim strSeikyuuSakiBrc As String = ctrlLink.AccSeikyuuSakiBrc.Value
        Dim strSeikyuuSakiKbn As String = ctrlLink.AccSeikyuuSakiKbn.Value
        Dim strChkSeikyuuSaki As String
        Dim strSeikyuuType As String
        Dim dicSeikyuu As Dictionary(Of String, String)
        Dim cBizLogic As New CommonBizLogic

        If strSeikyuuSakiCd <> String.Empty And strSeikyuuSakiBrc <> String.Empty And strSeikyuuSakiKbn <> String.Empty Then
            If strKameitenCd = strSeikyuuSakiCd Then
                strSeikyuuType = EarthConst.SEIKYU_TYOKUSETU
            Else
                strSeikyuuType = EarthConst.SEIKYU_TASETU
            End If
            Return strSeikyuuType
            Exit Function
        End If

        '������̎擾
        If strKojKaisyaCd = String.Empty Then
            dicSeikyuu = cBizLogic.getDefaultSeikyuuSaki(strKameitenCd, strSyouhinCd)
        Else
            dicSeikyuu = cBizLogic.getDefaultSeikyuuSaki(strKojKaisyaCd)
        End If
        strChkSeikyuuSaki = dicSeikyuu(cBizLogic.dicKeySeikyuuSakiCd)

        If strKameitenCd = strChkSeikyuuSaki Then
            strSeikyuuType = EarthConst.SEIKYU_TYOKUSETU
        Else
            strSeikyuuType = EarthConst.SEIKYU_TASETU
        End If

        Return strSeikyuuType

    End Function

#Region "�|�b�v�A�b�v�N������"
    ''' <summary>
    ''' ���i4�|�b�v�A�b�v��ʃ����N�p�X����
    ''' </summary>
    ''' <param name="btnPopup">���i4�{�^��</param>
    ''' <param name="infLoginUser">���O�C�����[�U���</param>
    ''' <param name="strKbnId">�敪(�R���g���[��ID)</param>
    ''' <param name="strBangouId">�ۏ؏�No(�R���g���[��ID)</param>
    ''' <param name="strKameiCdId">�����X�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strTysKaisyaCdId">������ЃR�[�h(�R���g���[��ID)</param>
    ''' <remarks></remarks>
    Public Sub getSyouhin4MasterPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysKaisyaCdId As String)

        btnPopup.Attributes("onclick") = "callModalSyouhin4('" & UrlConst.POPUP_SYOUHIN4 & "','" _
                                                               & strKbnId & "','" _
                                                               & strBangouId & "','" _
                                                               & strKameiCdId & "','" _
                                                               & strTysKaisyaCdId & "');"


    End Sub

    ''' <summary>
    ''' ���ʑΉ��|�b�v�A�b�v��ʃ����N�p�X����
    ''' </summary>
    ''' <param name="btnPopup">���ʑΉ��{�^��</param>
    ''' <param name="infLoginUser">���O�C�����[�U���</param>
    ''' <param name="strKbnId">�敪(�R���g���[��ID)</param>
    ''' <param name="strBangouId">�ۏ؏�No(�R���g���[��ID)</param>
    ''' <param name="strKameiCdId">�����X�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strTysHouhouNoId">�������@No�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strTysSyouhinCdId">���i�R�[�h(�R���g���[��ID)</param>
    ''' <remarks></remarks>
    Public Sub getTokubetuTaiouLinkPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysHouhouNoId As String _
                                    , ByVal strTysSyouhinCdId As String)

        btnPopup.Attributes("onclick") = "callModalTokubetuTaiou('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                   & strKbnId & "','" _
                                                                   & strBangouId & "','" _
                                                                   & strKameiCdId & "','" _
                                                                   & strTysHouhouNoId & "','" _
                                                                   & strTysSyouhinCdId & "');"

    End Sub

    ''' <summary>
    ''' ���ʑΉ��|�b�v�A�b�v��ʃ����N�p�X����(�󒍁E�@�ʏC����ʗp)
    ''' </summary>
    ''' <param name="btnPopup">���ʑΉ��{�^��</param>
    ''' <param name="infLoginUser">���O�C�����[�U���</param>
    ''' <param name="strKbnId">�敪(�R���g���[��ID)</param>
    ''' <param name="strBangouId">�ۏ؏�No(�R���g���[��ID)</param>
    ''' <param name="strKameiCdId">�����X�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strTysHouhouNoId">�������@No�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strTysSyouhinCdId">���i�R�[�h(�R���g���[��ID)</param>
    ''' <param name="strHdnKakuteiValue"></param>
    ''' <param name="strBtnTokubetu"></param>
    ''' <param name="emType">���ʑΉ������^�C�v</param>
    ''' <param name="strKkkHaneiFlg">���ʑΉ����i���f�p�t���O</param>
    ''' <param name="strChgTokuCd">���ʑΉ��X�V�ΏۃR�[�h</param>
    ''' <param name="strRentouBukkenSuu">�A��������</param>
    ''' <param name="strBtnReloadId">��ʍĕ`��pHidden�{�^��ID</param>
    ''' <remarks></remarks>
    Public Sub getTokubetuTaiouLinkPathJT(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String _
                                    , ByVal strKameiCdId As String _
                                    , ByVal strTysHouhouNoId As String _
                                    , ByVal strTysSyouhinCdId As String _
                                    , ByVal strHdnKakuteiValue As String _
                                    , ByVal strBtnTokubetu As String _
                                    , ByVal emType As EarthEnum.emTokubetuTaiouSearchType _
                                    , Optional ByVal strKkkHaneiFlg As String = "" _
                                    , Optional ByVal strChgTokuCd As String = "" _
                                    , Optional ByVal strRentouBukkenSuu As String = "" _
                                    , Optional ByVal strBtnReloadId As String = "")

        If emType = EarthEnum.emTokubetuTaiouSearchType.TeibetuSyuusei Then

            btnPopup.Attributes("onclick") = "callModalTokubetuTaiouJT('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                       & strKbnId & "','" _
                                                                       & strBangouId & "','" _
                                                                       & strKameiCdId & "','" _
                                                                       & strTysHouhouNoId & "','" _
                                                                       & strTysSyouhinCdId & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & "" & "','" _
                                                                       & strChgTokuCd & "','" _
                                                                       & emType & "','" _
                                                                       & strKkkHaneiFlg & "','" _
                                                                       & strRentouBukkenSuu & "','" _
                                                                       & strHdnKakuteiValue & "','" _
                                                                       & strBtnTokubetu & "','" _
                                                                       & strBtnReloadId & "');"
        Else
            btnPopup.Attributes("onclick") = "callModalTokubetuTaiouJT('" & UrlConst.POPUP_TOKUBETU_TAIOU & "','" _
                                                                & strKbnId & "','" _
                                                                & strBangouId & "','" _
                                                                & strKameiCdId & "','" _
                                                                & strTysHouhouNoId & "','" _
                                                                & strTysSyouhinCdId & "'," _
                                                                & "createPrm(1)," _
                                                                & "createPrm(2)," _
                                                                & "createPrm(3)," _
                                                                & "createPrm(4),'" _
                                                                & strChgTokuCd & "','" _
                                                                & emType & "','" _
                                                                & strKkkHaneiFlg & "','" _
                                                                & strRentouBukkenSuu & "','" _
                                                                & strHdnKakuteiValue & "','" _
                                                                & strBtnTokubetu & "','" _
                                                                & strBtnReloadId & "');"
        End If

    End Sub

#End Region

    ''' <summary>
    ''' ���i�R�[�h�A���i���ރ^�C�v�A�����X�R�[�h�Ɛ�����������ɐ�����^�C�v���擾����
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="emType">���i���ރ^�C�v</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSeikyuuSakiCd">������R�[�h</param>
    ''' <param name="strSeikyuuSakibrc">������}��</param>
    ''' <param name="strSeikyuuSakiKbn">������敪</param>
    ''' <returns>���ڐ���,������,""(��)</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuSakiTypeStr( _
                                            ByVal strSyouhinCd As String, _
                                            ByVal emType As EarthEnum.EnumSyouhinKubun, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strSeikyuuSakiCd As String, _
                                            ByVal strSeikyuuSakibrc As String, _
                                            ByVal strSeikyuuSakiKbn As String _
                                        ) As String

        Dim JibanLogic As New JibanLogic
        '������^�C�v
        Dim strSeikyuuSakiType As String = EarthConst.SEIKYU_TASETU '���i�����Ȃ��ꍇ�ɁA"������"���f�t�H���g�Ƃ���

        '������^�C�v�̎����ݒ�
        Dim syouhinRec As Syouhin23Record

        If strSyouhinCd = String.Empty Or strKameitenCd = String.Empty Then
            Return strSeikyuuSakiType
            Exit Function
        End If

        syouhinRec = JibanLogic.GetSyouhinInfo(strSyouhinCd, emType, strKameitenCd)
        If Not syouhinRec Is Nothing Then
            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If strSeikyuuSakiCd <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhinRec.SeikyuuSakiCd = strSeikyuuSakiCd
                syouhinRec.SeikyuuSakiBrc = strSeikyuuSakibrc
                syouhinRec.SeikyuuSakiKbn = strSeikyuuSakiKbn
            End If
            strSeikyuuSakiType = syouhinRec.SeikyuuSakiType '��������
        End If

        Return strSeikyuuSakiType
    End Function

    ''' <summary>
    ''' ������~�t���O�`�F�b�N
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strOrderStopFlg">������~�t���O</param>
    ''' <remarks></remarks>
    Public Sub chkOrderStopFlg(ByVal sender As System.Object, ByVal strOrderStopFlg As String, ByRef strKameitenCd As String, ByRef strSaveCdOrderStop As String)
        Dim mLogic As New MessageLogic

        '������~�`�F�b�N
        If EarthConst.Instance.HATTYUU_TEISI_FLGS.ContainsKey(strOrderStopFlg) Then
            mLogic.AlertMessage(sender, Messages.MSG166E)
            '�ޔ����Ă��������X�R�[�h����꒼��
            strKameitenCd = strSaveCdOrderStop
            strSaveCdOrderStop = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' ��ʂ�Hidden���ڂɔr���p�̍X�V����(�X�V�������Ȃ��ꍇ�͓o�^����)���Z�b�g
    ''' </summary>
    ''' <param name="dtAddDateTime">�o�^����</param>
    ''' <param name="dtUpdDateTime">�X�V����</param>
    ''' <param name="objHdnUpdDateTime">�r������p�X�V�����i�[Hidden�R���g���[��</param>
    ''' <param name="strDateFormat">���t�t�H�[�}�b�g������</param>
    ''' <remarks></remarks>
    Public Sub setDispHaitaUpdTime(ByVal dtAddDateTime As DateTime, ByVal dtUpdDateTime As DateTime, ByVal objHdnUpdDateTime As Object, Optional ByVal strDateFormat As String = EarthConst.FORMAT_DATE_TIME_1)
        If dtUpdDateTime = DateTime.MinValue Then
            If dtAddDateTime = DateTime.MinValue Then
                objHdnUpdDateTime.Value = String.Empty
            Else
                objHdnUpdDateTime.Value = dtAddDateTime.ToString(strDateFormat)
            End If
        Else
            objHdnUpdDateTime.Value = dtUpdDateTime.ToString(strDateFormat)
        End If
    End Sub

    ''' <summary>
    ''' ��ʂ�Hidden���ڂ���r���p�̍X�V������DateTime�^�Ŏ擾
    ''' </summary>
    ''' <param name="objHdnDateTime">�r������p�X�V�����i�[Hidden�R���g���[��</param>
    ''' <param name="strDateFormat">���t�t�H�[�}�b�g������</param>
    ''' <returns>�X�V����</returns>
    ''' <remarks></remarks>
    Public Function getDispHaitaUpdTime(ByVal objHdnDateTime As Object, Optional ByVal strDateFormat As String = EarthConst.FORMAT_DATE_TIME_1) As DateTime
        Dim dtUpdDateTime As DateTime

        If objHdnDateTime.value = String.Empty Then
            dtUpdDateTime = DateTime.MinValue
        Else
            dtUpdDateTime = DateTime.ParseExact(objHdnDateTime.value, strDateFormat, Nothing)
        End If

        Return dtUpdDateTime
    End Function

#Region "�����m��\��Ǘ��e�[�u���̏����󋵂��擾"
    ''' <summary>
    ''' �����m��\��Ǘ��e�[�u���̏����󋵂��擾
    ''' </summary>
    ''' <returns>True:�����̂݉�,False:�����ȑO����</returns>
    ''' <remarks></remarks>
    Public Function GetGetujiYoyakuJyky() As Boolean
        '�����m��\��Ǘ��e�[�u���̏����󋵂��擾(�`�[����N�����`�F�b�N�p)
        Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
        Dim targetYM As New DateTime
        Dim flgGetujiKakuteiYoyakuzumi As Boolean = False
        targetYM = Today.Year.ToString() & "/" & Today.Month.ToString("00") & "/01" '��������
        targetYM = targetYM.AddDays(-1)    '�挎����
        Dim syoriJoukyou As Object = clsUpdLogic.GetGetujiKakuteiYoyakuData(targetYM)
        If syoriJoukyou Is Nothing OrElse syoriJoukyou = 0 Then
            flgGetujiKakuteiYoyakuzumi = False
        Else
            flgGetujiKakuteiYoyakuzumi = True
        End If

        Return flgGetujiKakuteiYoyakuzumi
    End Function
#End Region

    ''' <summary>
    ''' �`�[����N�����̌����m��`�F�b�N
    ''' </summary>
    ''' <param name="strTextDenUriDate">�`�[����N����</param>
    ''' <returns>�`�F�b�N����(True�FOK�^False�FNG)</returns>
    ''' <remarks>�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[����N������ݒ肷��̂̓G���[</remarks>
    Public Function chkGetujiKakuteiYoyakuzumi(ByVal strTextDenUriDate As String, _
                                               ByRef dtGetujiKakuteiLastSyoriDate As DateTime) As Boolean
        Dim clsUpdLogic As New GetujiIkkatuUpdateLogic
        dtGetujiKakuteiLastSyoriDate = clsUpdLogic.getGetujiKakuteiLastSyoriDate()

        '�����m��\��ς݂̏����N���u�ȑO�v�̓��t�œ`�[����N������ݒ肷��̂̓G���[
        If dtGetujiKakuteiLastSyoriDate <> Date.MinValue AndAlso strTextDenUriDate <> String.Empty Then
            Try
                Dim dtDenUriDate As Date = Date.Parse(strTextDenUriDate)
                If dtDenUriDate.Year.ToString & dtDenUriDate.Month.ToString("00") <= dtGetujiKakuteiLastSyoriDate.Year.ToString & dtGetujiKakuteiLastSyoriDate.Month.ToString("00") Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End If

        Return True
    End Function

    ''' <summary>
    ''' �Ԏ�������ݒ肷��
    ''' </summary>
    ''' <param name="objStyle">�e�R���g���[����Style</param>
    ''' <param name="blnSet">True�F�Ԏ��E�����^False:�ʏ�t�H���g</param>
    ''' <remarks></remarks>
    Public Sub setStyleRedBold(ByRef objStyle As CssStyleCollection, ByVal blnSet As Boolean)
        If blnSet Then
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        Else
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLACK
        End If
    End Sub

    ''' <summary>
    ''' ��������ݒ肷��
    ''' </summary>
    ''' <param name="objStyle">�e�R���g���[����Style</param>
    ''' <param name="blnSet">True�F���E�����^False:�ʏ�t�H���g</param>
    ''' <remarks></remarks>
    Public Sub setStyleBlueBold(ByRef objStyle As CssStyleCollection, ByVal blnSet As Boolean)
        If blnSet Then
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            objStyle.Item(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_NORMAL
            objStyle.Item(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLACK
        End If
    End Sub

    ''' <summary>
    ''' �����F��ύX����
    ''' </summary>
    ''' <param name="objStyle">�e�R���g���[����Style</param>
    ''' <param name="strFontColor">�ݒ�F</param>
    ''' <remarks></remarks>
    Public Sub setStyleFontColor(ByRef objStyle As CssStyleCollection, ByVal strFontColor As String)

        '�����F��ύX
        objStyle.Item(EarthConst.STYLE_FONT_COLOR) = strFontColor

    End Sub

#Region "�ύX�ӏ����ڑΉ�(���@�ʃf�[�^�C�����)"
    ''' <summary>
    ''' �f�B�N�V���i����KEY�l�����ɁA�Y���ӏ��̔w�i�F��ԐF�ɕύX����
    ''' ��Hidden�ȊO
    ''' </summary>
    ''' <param name="dic">�f�B�N�V���i��</param>
    ''' <param name="strKey">KEY�l</param>
    ''' <remarks></remarks>
    Public Sub ChgHenkouCtrlBgColor(ByVal dic As Dictionary(Of String, Object), ByVal strKey As String)
        Dim objRet As New Object

        If Not dic Is Nothing Then
            '�w�i�F�ύX����
            If dic.ContainsKey(strKey) Then
                objRet = dic(strKey)
                If Not objRet Is Nothing Then
                    Dim strTmpId As String = objRet.GetType.Name
                    If strTmpId = "HtmlInputHidden" _
                        OrElse strTmpId = "HiddenField" Then 'Hidden�͑ΏۊO
                    Else
                        objRet.Style("background-color") = "red"
                    End If
                End If
            End If
        End If

    End Sub
#End Region

#Region "�����i����"
    ''' <summary>
    ''' �����i���󋵃}�X�^�����N�p�X����
    ''' </summary>
    ''' <param name="btnPopup">�����i���󋵃{�^��</param>
    ''' <param name="infLoginUser">���O�C�����[�U���</param>
    ''' <param name="strKbnId">�敪(�R���g���[��ID)</param>
    ''' <param name="strBangouId">�ۏ؏�No(�R���g���[��ID)</param>
    ''' <remarks></remarks>
    Public Sub getBukkenJykyMasterPath(ByVal btnPopup As HtmlInputButton _
                                    , ByVal infLoginUser As LoginUserInfo _
                                    , ByVal strKbnId As String _
                                    , ByVal strBangouId As String)

        btnPopup.Attributes("onclick") = "callSearch('" & strKbnId & EarthConst.SEP_STRING & strBangouId & "','" _
                                                        & UrlConst.POPUP_Bukken_SINTYOKU_JYKY & "','" _
                                                        & "','');"


    End Sub
#End Region

#Region "�ۏ؏��֘A�Ή�"
    ''' <summary>
    ''' �������ɕۏ؏��Ǘ��󋵂𔻒f���āA�ۏ؏��Ǘ�T.�����󋵂ɐݒ肷��l��Ԃ�
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h�N���X(�l�Z�b�g�ς݂̂���)</param>
    ''' <remarks></remarks>
    Public Function ChkHosyousyoBukkenJyky(ByVal jibanRec As JibanRecordBase) As Integer

        Dim intRet As Integer = Integer.MinValue

        Dim cbLogic As New CommonBizLogic
        Dim jLogic As New JibanLogic
        Dim BJykyLogic As New BukkenSintyokuJykyLogic

        '�ۏؗL��(�����s���X�g�󎚗L��)
        Dim strHosyouUmu As String = cbLogic.GetHosyousyoHakJykyHosyouUmu(Me.GetDispStr(jibanRec.HosyousyoHakJyky))
        '�n�Ճ��R�[�hOld
        Dim jibanRecOld As JibanRecordBase = jLogic.GetJibanData(jibanRec.Kbn, jibanRec.HosyousyoNo)

        Dim HosyouRec As New HosyousyoKanriRecord
        Dim sender As New Object
        HosyouRec = BJykyLogic.getSearchKeyDataRec(sender, jibanRecOld.Kbn, jibanRec.HosyousyoNo)

        '�ۏ؏��Ǘ�T.������Old�����ݒ�̏ꍇ
        If HosyouRec.BukkenJyky = Integer.MinValue Then

            '�n��T.�ۏ؏����s��Old
            If jibanRecOld.HosyousyoHakDate <> DateTime.MinValue Then
                intRet = 3

            Else
                '�n��T.�f�[�^�j�����(���ݒ莞:0)
                If jibanRec.DataHakiSyubetu <> 0 Then
                    intRet = 0

                ElseIf strHosyouUmu = "0" Then '�ۏ؏����s��.�ۏؗL��
                    intRet = 0

                ElseIf Not jibanRecOld.KaiyakuHaraimodosiRecord Is Nothing Then '��񕥖߂̓@�ʐ������R�[�h������ꍇ
                    intRet = 0

                Else
                    '�ۏ؏��Ǘ��f�[�^�𔻒�
                    If cbLogic.GetHosyousyoKanriJyky(jibanRec.Kbn, jibanRec.HosyousyoNo) = 1 Then
                        intRet = 1
                    Else
                        intRet = 2
                    End If
                End If
            End If

        Else
            intRet = HosyouRec.BukkenJyky
        End If

        Return intRet
    End Function
#End Region

#Region "�ۏ؏��i�L��"

    ''' <summary>
    ''' �ۏ؏��i�̕\���ؑ�
    ''' ���ۏ؂���F"����"(������)
    ''' ���ۏ؂Ȃ��F"�Ȃ�"(�Ԏ�����)
    ''' </summary>
    ''' <param name="strHosyouSyouhinUmu">�ۏ؏��i�L��(1:����A�ȊO:�Ȃ�)</param>
    ''' <param name="txtTarget">�Ώۃe�L�X�g�{�b�N�X</param>
    ''' <remarks></remarks>
    Public Sub ChgDispHosyouSyouhin(ByVal strHosyouSyouhinUmu As String, ByRef txtTarget As HtmlInputText)
        If strHosyouSyouhinUmu = "1" Then '�L
            txtTarget.Value = EarthConst.ARI_HIRAGANA '����
            '��
            txtTarget.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_BLUE
        Else
            txtTarget.Value = EarthConst.NASI_HIRAGANA '�Ȃ�
            '�Ԏ�
            txtTarget.Style(EarthConst.STYLE_FONT_COLOR) = EarthConst.STYLE_COLOR_RED
        End If
        '����
        txtTarget.Style(EarthConst.STYLE_FONT_WEIGHT) = EarthConst.STYLE_WEIGHT_BOLD

    End Sub

#End Region

#Region "��ʕ\���ؑ�[����]"

    ''' <summary>
    ''' �w��l�I�����̓���(��ʕ\�����p)
    ''' ���\���A�\���b�A����ҁ@��
    ''' ���h���b�v�_�E�����X�g�A�`�F�b�N�{�b�N�X�A���W�I�{�^��
    ''' </summary>
    ''' <param name="checkVal">�`�F�b�N���R���g���[���̗L���l</param>
    ''' <param name="pull">�`�F�b�N���R���g���[���i�v���_�E���j</param>
    ''' <param name="arrSonota">�`�F�b�N���R���g���[�����L���l�������ꍇ�ɕ\��/��\�����s���R���g���[���S</param>
    ''' <remarks></remarks>
    Public Sub CheckVisible(ByVal checkVal As String, _
                             ByVal pull As Object, _
                             ByVal arrSonota As ArrayList)

        Dim arrChkVal() As String = Split(checkVal, EarthConst.SEP_STRING)
        Dim intCnt As Integer = 0
        Dim strObjTmpVal As String = String.Empty

        Select Case pull.GetType.Name
            Case "DropDownList" '�h���b�v�_�E�����X�g
                strObjTmpVal = pull.SelectedValue

            Case "HtmlInputCheckBox", "CheckBox", "HtmlInputRadioButton" '�`�F�b�N�{�b�N�X,INPUT���W�I�{�^��
                strObjTmpVal = pull.checked

            Case Else
                Exit Sub
        End Select

        '�R���g���[�������[�v
        For Each sonota As Object In arrSonota
            '�w��l����������ꍇ�̃��[�v
            For intCnt = 0 To arrChkVal.Length - 1
                '�\���Ώۂ����������ꍇ�A�����𔲂���
                If strObjTmpVal = arrChkVal(intCnt) Then
                    sonota.Style("visibility") = "visible"
                    Exit For
                Else
                    sonota.Style("visibility") = "hidden"
                End If
            Next
        Next

    End Sub

#Region "��������҃R�[�h"

    ''' <summary>
    ''' ����҃R�[�h�����ʍ��ڂւ̔��f����
    ''' </summary>
    ''' <param name="tatiaiCd">����҃R�[�h</param>
    ''' <param name="CheckTTSesyuSama">�{��l</param>
    ''' <param name="CheckTTTantousya">�S����</param>
    ''' <param name="CheckTTSonota">���̑�</param>
    ''' <remarks></remarks>
    Public Sub SetTatiaiCd( _
                            ByVal tatiaiCd As Integer _
                            , ByRef CheckTTSesyuSama As HtmlInputCheckBox _
                            , ByRef CheckTTTantousya As HtmlInputCheckBox _
                            , ByRef CheckTTSonota As HtmlInputCheckBox _
                            )

        Dim tSesyu As Integer = CheckTTSesyuSama.Value
        Dim tTant As Integer = CheckTTTantousya.Value
        Dim tOther As Integer = CheckTTSonota.Value
        Dim tSesyuTant As Integer = tSesyu + tTant
        Dim tSesyuOther As Integer = tSesyu + tOther
        Dim tTantOther As Integer = tTant + tOther
        Dim zAll As Integer = tSesyu + tTant + tOther

        CheckTTSesyuSama.Checked = False
        CheckTTTantousya.Checked = False
        CheckTTSonota.Checked = False

        Select Case tatiaiCd
            Case tSesyu
                CheckTTSesyuSama.Checked = True
            Case tTant
                CheckTTTantousya.Checked = True
            Case tOther
                CheckTTSonota.Checked = True
            Case tSesyuTant
                CheckTTSesyuSama.Checked = True
                CheckTTTantousya.Checked = True
            Case tTantOther
                CheckTTTantousya.Checked = True
                CheckTTSonota.Checked = True
            Case tSesyuOther
                CheckTTSesyuSama.Checked = True
                CheckTTSonota.Checked = True
            Case zAll
                CheckTTSesyuSama.Checked = True
                CheckTTTantousya.Checked = True
                CheckTTSonota.Checked = True

        End Select

    End Sub

    ''' <summary>
    ''' ��ʍ��ڂ��痧��҃R�[�h�ւ̕ϊ�����
    ''' </summary>
    ''' <param name="CheckTTSesyuSama">�{��l</param>
    ''' <param name="CheckTTTantousya">�S����</param>
    ''' <param name="CheckTTSonota">���̑�</param>
    ''' <returns>����҃R�[�h</returns>
    ''' <remarks></remarks>
    Public Function GetTatiaiCd( _
                            ByRef CheckTTSesyuSama As HtmlInputCheckBox _
                            , ByRef CheckTTTantousya As HtmlInputCheckBox _
                            , ByRef CheckTTSonota As HtmlInputCheckBox _
                            ) As Integer

        Dim tmpCd As Integer = 0

        If CheckTTSesyuSama.Checked Then
            tmpCd = CheckTTSesyuSama.Value
        End If
        If CheckTTTantousya.Checked Then
            tmpCd = tmpCd + CheckTTTantousya.Value
        End If
        If CheckTTSonota.Checked Then
            tmpCd = tmpCd + CheckTTSonota.Value
        End If

        Return tmpCd
    End Function

#End Region

#End Region

#Region "�o���ǉ��Ή�"
    ''' <summary>
    ''' �V�X�e�����t�����ɁA�N�x�n�ߓ��t���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTermFirstDate(ByVal dteNow As DateTime) As DateTime

        Dim strTermFirstDate As String
        Dim dteTermFirstDate As DateTime
        Dim intMonth As Integer = dteNow.Month

        If intMonth <= 3 Then   '3���ȑO
            strTermFirstDate = CStr(dteNow.Year - 1) & EarthConst.TERM_FIRST_DATE
        Else                    '4���ȍ~
            strTermFirstDate = CStr(dteNow.Year) & EarthConst.TERM_FIRST_DATE
        End If

        dteTermFirstDate = CDate(strTermFirstDate)

        Return dteTermFirstDate
    End Function

#End Region

#Region "�������@�̃h���b�v�_�E�����X�g�\������"

    ''' <summary>
    ''' �������@�̃h���b�v�_�E�����X�g�\������
    ''' </summary>
    ''' <param name="intTysHouhou">��ʂɃZ�b�g���钲�����@NO</param>
    ''' <param name="objSelectTysHouhou">�Z�b�g�Ώۂ̃h���b�v�_�E�����X�g</param>
    ''' <param name="objTextTysHouhou">�Z�b�g�Ώۂ̃e�L�X�g�{�b�N�X</param>
    ''' <remarks></remarks>
    Public Sub ps_SetSelectTextBoxTysHouhou(ByVal intTysHouhou As Integer, ByRef objSelectTysHouhou As DropDownList, ByVal withCode As Boolean, Optional ByRef objTextTysHouhou As TextBox = Nothing)
        Dim JLogic As New JibanLogic
        Dim strTysHouhou As String = String.Empty

        strTysHouhou = Me.GetDisplayString(intTysHouhou)

        '�������@
        If Me.ChkDropDownList(objSelectTysHouhou, strTysHouhou) Then
        ElseIf strTysHouhou <> String.Empty Then
            'DDL�ɂȂ���΁A�A�C�e����ǉ�
            Dim recTysHouhou As New TyousahouhouRecord
            recTysHouhou = JLogic.getTyousahouhouRecord(CInt(strTysHouhou))
            If withCode Then
                objSelectTysHouhou.Items.Add(New ListItem(recTysHouhou.TysHouhouNo & ":" & recTysHouhou.TysHouhouMei, recTysHouhou.TysHouhouNo))
            Else
                objSelectTysHouhou.Items.Add(New ListItem(recTysHouhou.TysHouhouMei, recTysHouhou.TysHouhouNo))
            End If
        End If

        '�I�����
        objSelectTysHouhou.SelectedValue = strTysHouhou

        If Not objTextTysHouhou Is Nothing Then
            objTextTysHouhou.Text = objSelectTysHouhou.SelectedItem.Text
        End If

    End Sub

#End Region

#Region "�����X���\���ؑ�"
    ''' <summary>
    ''' ��ʕ\���p.������R���擾
    ''' </summary>
    ''' <param name="intTorikesi">���(�R�[�h)</param>
    ''' <param name="strMeisyou">�g�����̃}�X�^.����</param>
    ''' <param name="withCode">�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="blnTorikesiRow">"0:�ݒ�Ȃ�"��\������ꍇ:true</param>
    ''' <returns>������R</returns>
    ''' <remarks></remarks>
    Public Function getTorikesiRiyuu(ByVal intTorikesi As Integer _
                                    , ByVal strMeisyou As String _
                                    , Optional ByVal withCode As Boolean = True _
                                    , Optional ByVal blnTorikesiRow As Boolean = False) As String

        Dim strTorikesiRiyuu As String                  '������R
        Dim strTorikesi As String                       '���

        strTorikesi = Me.GetDisplayString(intTorikesi)

        If intTorikesi = 0 AndAlso blnTorikesiRow = False Then
            Return String.Empty
        End If

        If withCode Then
            strTorikesiRiyuu = strTorikesi & "�F" & strMeisyou
        Else
            strTorikesiRiyuu = strMeisyou
        End If

        Return strTorikesiRiyuu
    End Function

    ''' <summary>
    ''' ��ʕ\���p.�����F�𔻒�
    ''' </summary>
    ''' <param name="intTorikesi">���</param>
    ''' <returns>�����F</returns>
    ''' <remarks></remarks>
    Public Function getKameitenFontColor(ByVal intTorikesi As Integer) As String
        Dim strFontColor As String

        If intTorikesi > 0 Then
            strFontColor = EarthConst.STYLE_COLOR_RED
        Else
            strFontColor = EarthConst.STYLE_COLOR_BLACK
        End If

        Return strFontColor
    End Function

    ''' <summary>
    ''' �����X������R�̔��肨��ѐݒ菈��
    ''' </summary>
    ''' <param name="strSeikyuuSakiKbn">������敪(Value�l)</param>
    ''' <param name="strKameitenCd">�����X�R�[�h(Value�l)</param>
    ''' <param name="objTorikesi">������R(�I�u�W�F�N�g)</param>
    ''' <param name="withCode">�R�[�h�\���̗L��</param>
    ''' <param name="blnTorikesi">������R�[�h�̌�����</param>
    ''' <param name="objArray"></param>
    ''' <remarks></remarks>
    Public Sub GetKameitenTorikesiRiyuuMain(ByVal strSeikyuuSakiKbn As String _
                                        , ByVal strKameitenCd As String _
                                        , ByVal objTorikesi As Object _
                                        , Optional ByVal withCode As Boolean = True _
                                        , Optional ByVal blnTorikesi As Boolean = False _
                                        , Optional ByVal objArray() As Object = Nothing)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenTorikesiRiyuuMain" _
                                                    , strSeikyuuSakiKbn _
                                                    , strKameitenCd _
                                                    , objTorikesi _
                                                    , withCode _
                                                    , blnTorikesi _
                                                    , objArray)

        '�R���g���[���i�[�p�I�u�W�F�N�g
        Dim objTmpCtrl As New Object

        '������敪�������X�̏ꍇ�A������R���擾�ݒ�
        If Not String.IsNullOrEmpty(strSeikyuuSakiKbn) AndAlso strSeikyuuSakiKbn = EarthConst.SEIKYUU_SAKI_KAMEI Then
            '�����X������R�擾�ݒ菈��
            GetKameitenTorikesiRiyuu(String.Empty, strKameitenCd, objTorikesi, withCode, blnTorikesi, objArray)
        Else
            '�F�t�ΏۃI�u�W�F�N�g��W���F(��)�ɖ߂�
            If Not objArray Is Nothing Then
                For Each objTmpCtrl In objArray
                    setStyleFontColor(objTmpCtrl.Style, EarthConst.STYLE_COLOR_BLACK)
                Next
            End If
            '������R���N���A
            If objTorikesi.GetType.Name = "HtmlInputText" Then
                objTorikesi.Value = String.Empty
            ElseIf objTorikesi.GetType.Name = "TextBox" Then
                objTorikesi.Text = String.Empty
            End If
        End If

    End Sub

    ''' <summary>
    ''' �����X������R�ݒ菈��
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="TextTorikesi">������R�e�L�X�g�{�b�N�X</param>
    ''' <param name="withCode">�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="blnTorikesi">����Ώۃt���O</param>
    ''' <param name="objArray">�F�ւ��ΏۃR���g���[���̔z��</param>
    ''' <remarks></remarks>
    Public Sub GetKameitenTorikesiRiyuu(ByVal strKbn As String _
                                        , ByVal strKameitenCd As String _
                                        , ByVal TextTorikesi As Object _
                                        , Optional ByVal withCode As Boolean = True _
                                        , Optional ByVal blnTorikesi As Boolean = False _
                                        , Optional ByVal objArray() As Object = Nothing)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenTorikesiRiyuu" _
                                                    , strKbn _
                                                    , strKameitenCd _
                                                    , TextTorikesi _
                                                    , withCode _
                                                    , blnTorikesi _
                                                    , objArray)

        Dim recData As New KameitenSearchRecord
        Dim kLogic As New KameitenSearchLogic
        Dim allRowCount As Integer = 0
        Dim strTorikesiRiyuu As String
        Dim strFontColor As String
        Dim tmpCtrl As Object
        Dim intCnt As Integer = 0

        recData = kLogic.GetKameitenRecord(strKbn _
                                           , strKameitenCd _
                                           , blnTorikesi)

        If Not recData Is Nothing AndAlso recData.Torikesi > 0 Then
            strTorikesiRiyuu = Me.getTorikesiRiyuu(recData.Torikesi, recData.KtTorikesiRiyuu, withCode, False)  '������R
            strFontColor = Me.getKameitenFontColor(recData.Torikesi)                                    '�����F
        Else
            strTorikesiRiyuu = String.Empty             '������R
            strFontColor = Me.getKameitenFontColor(0)   '�����F
        End If

        '������R��ݒ�
        If TextTorikesi.GetType.Name = "HtmlInputText" Then
            TextTorikesi.Value = strTorikesiRiyuu
        ElseIf TextTorikesi.GetType.Name = "TextBox" Then
            TextTorikesi.Text = strTorikesiRiyuu
        End If

        '������R�̐F�ւ�����
        setStyleFontColor(TextTorikesi.Style, strFontColor)

        '���̑����ڂ̐F�ւ�����
        If Not objArray Is Nothing Then
            For Each tmpCtrl In objArray
                setStyleFontColor(tmpCtrl.Style, strFontColor)
            Next
        End If

    End Sub



#End Region
End Class
