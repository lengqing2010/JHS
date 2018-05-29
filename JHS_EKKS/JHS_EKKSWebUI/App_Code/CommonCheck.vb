Imports Lixil.JHS_EKKS.BizLogic
Imports Messages = Lixil.JHS_EKKS.Utilities.CommonMessage
Public Enum kbn As Integer
    HANKAKU = 1
    ZENKAKU = 2
End Enum

''' <summary>
''' ����
''' </summary>
''' <remarks></remarks>
Public Enum kegen As Integer
    ''' <summary>
    ''' �c�ƌv��Ǘ�_�Q�ƌ���
    ''' </summary>
    ''' <remarks></remarks>
    EigyouKeikakuKenriSansyou = 1
    ''' <summary>
    ''' ����\��_�Q�ƌ���
    ''' </summary>
    ''' <remarks></remarks>
    UriYojituKanriSansyou = 2
    ''' <summary>
    ''' �S�Ќv��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    ZensyaKeikakuKengen = 3
    ''' <summary>
    ''' �x�X�ʔN�x�v��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuNenKeikakuKengen = 4
    ''' <summary>
    ''' �x�X�ʌ����v��_�捞����
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuTorikomi = 5
    ''' <summary>
    ''' �x�X�ʌ����v��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuKakutei = 6
    ''' <summary>
    ''' �v��l������_����
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuMinaosiKengen = 7
    ''' <summary>
    ''' �v��l�m��_����
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuKakuteiKengen = 8
    ''' <summary>
    ''' �x�X�ʌ��ʌv��_�m�茠��
    ''' </summary>
    ''' <remarks></remarks>
    KeikakuTorikomiKengen = 9
    ''' <summary>
    ''' ���[�U�[�̂�
    ''' </summary>
    ''' <remarks></remarks>
    UserIdOnly = 10
    ''' <summary>
    ''' �x�X�ʌ����v��_����������
    ''' </summary>
    ''' <remarks></remarks>
    SitenbetuGetujiKeikakuMinaosi = 11
End Enum
Public Class CommonCheck

    ''' <summary>
    ''' �ғ��`�F�b�N
    ''' </summary>
    ''' <remarks></remarks>
    Public Function KadouCheck() As Boolean
        Dim Context As HttpContext = HttpContext.Current
        Dim Common As New Common
        Dim dteKadou As String = Format(Common.GetSystemDate, "HH:mm:ss")
        Dim strKadouJikan As String = System.Configuration.ConfigurationManager.AppSettings("KadouJikan")
        Dim blnJikan As Boolean = False
        If strKadouJikan = "" Then
            blnJikan = False
        Else
            Dim strDanJikan() As String = Split(strKadouJikan, ",")
            For intDan As Integer = 0 To strDanJikan.Length - 1
                Dim strJikan() As String = Split(strDanJikan(intDan), "~")
                For intJikan As Integer = 0 To Split(strJikan(intDan), "~").Length - 1
                    If dteKadou >= strJikan(0) AndAlso dteKadou <= strJikan(1) Then
                        blnJikan = True
                    End If
                Next

            Next
        End If
        Return blnJikan

    End Function
    ''' <summary>
    ''' ������̃o�C�g�����擾�iShift-JIS�j
    ''' </summary>
    ''' <param name="str">�Ώە�����</param>
    ''' <returns>Integer�F�o�C�g��</returns>
    ''' <remarks></remarks>
    Public Function getStrByteSJIS(ByVal str As String) As Integer

        'Shift-JIS�ł̃o�C�g�����擾
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

    End Function
    ''' <summary>
    ''' �֑������`�F�b�N
    ''' </summary>
    ''' <param name="target">�`�F�b�N���镶����</param>
    ''' <returns>true:�`�F�b�NOK / false:�`�F�b�NNG</returns>
    ''' <remarks></remarks>
    Public Function kinsiStrCheck(ByVal target As String) As Boolean

        For Each st As String In CommonConstBC.Instance.arrayKinsiStr
            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True

    End Function
    ''' <summary>
    ''' �o�C�g���`�F�b�N(SJIS)
    ''' </summary>
    ''' <param name="target">�`�F�b�N���镶����</param>
    ''' <param name="max">�ő�OK�o�C�g��</param>
    ''' <returns>true:�`�F�b�NOK / false:�`�F�b�NNG</returns>
    ''' <remarks></remarks>
    Public Function byteCheckSJIS(ByVal target As String, ByVal max As Integer) As Boolean

        Return getStrByteSJIS(target) <= max

    End Function
    ''' <summary>
    ''' ���[�U�[�����̃`�F�b�N
    ''' </summary>
    ''' <param name="strUserId">���[�U�[�R�[�h</param>
    ''' <param name="userInfo">���[�U�[infor</param>
    ''' <param name="strNinsyou">���[�U�[����</param>
    ''' <returns></returns>
    Function CommonNinsyou(ByRef strUserId As String, ByRef userInfo As LoginUserInfoList, ByVal strNinsyou As kegen) As Boolean
        Dim Context As HttpContext = HttpContext.Current

        Dim login_logic As New NinsyouBC
        Dim ninsyou As New NinsyouBC()


        ' ���[�U�[�F��
        If (Not ninsyou.IsUserLogon()) Then
            '�F�؎��s
            ninsyou.EndResponseWithAccessDeny()
        End If
        ' ���[�U�[��{�F��
        Context.Items("strFailureMsg") = String.Format(Messages.MSG022E, "���[�U�[") '�i"�Y�����[�U�[������܂���B"�j
        If ninsyou.GetUserID() = "" Then
            Context.Server.Transfer("./CommonErr.aspx")
            Return False
        End If
        userInfo = login_logic.GetUserInfo(ninsyou.GetUserID())

        Context.Items("strFailureMsg") = Messages.MSG023E '�i"����������܂���B"�j
        If userInfo.Items.Count = 0 Then
            Return False
        Else
            If userInfo.Items.Count > 1 Then
                Return False
            Else
                strUserId = userInfo.Items(0).Account
            End If

        End If

        Select Case strNinsyou
            Case kegen.EigyouKeikakuKenriSansyou
                If userInfo.Items(0).EigyouKeikakuKenriSansyou = -1 Then
                    Return True
                End If
            Case kegen.KeikakuKakuteiKengen
                If userInfo.Items(0).KeikakuKakuteiKengen = -1 Then
                    Return True
                End If
            Case kegen.KeikakuMinaosiKengen
                If userInfo.Items(0).KeikakuMinaosiKengen = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuGetujiKeikakuKakutei
                If userInfo.Items(0).SitenbetuGetujiKeikakuKakutei = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuGetujiKeikakuTorikomi
                If userInfo.Items(0).SitenbetuGetujiKeikakuTorikomi = -1 Then
                    Return True
                End If
            Case kegen.SitenbetuNenKeikakuKengen
                If userInfo.Items(0).SitenbetuNenKeikakuKengen = -1 Then
                    Return True
                End If
            Case kegen.KeikakuTorikomiKengen
                If userInfo.Items(0).KeikakuTorikomiKengen = -1 Then
                    Return True
                End If
            Case kegen.UriYojituKanriSansyou
                If userInfo.Items(0).UriYojituKanriSansyou = -1 Then
                    Return True
                End If
            Case kegen.ZensyaKeikakuKengen
                If userInfo.Items(0).ZensyaKeikakuKengen = -1 Then
                    Return True
                End If
            Case kegen.UserIdOnly
                Return True

            Case kegen.SitenbetuGetujiKeikakuMinaosi
                If userInfo.Items(0).SitenbetuGetujiKeikakuMinaosi = -1 Then
                    Return True
                End If
        End Select
        Return False
    End Function
    ''' <summary>
    ''' �K�{���̓`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHissuNyuuryoku(ByVal inTarget As String, ByVal PARAM As String) As String
        If inTarget.Trim = "" Then
            Return String.Format(Messages.MSG001E, PARAM)
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    ''' ���t�͈̓`�F�b�N
    ''' </summary>
    ''' <param name="kaisiHiduke"></param>
    ''' <param name="syuuryouHiduke"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHidukeHani(ByVal kaisiHiduke As Date, ByVal syuuryouHiduke As Date, ByVal PARAM As String) As String
        If Date.Compare(kaisiHiduke, syuuryouHiduke) > 0 Then
            Return String.Format(Messages.MSG024E, PARAM, PARAM).ToString
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' �L�����t�`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckYuukouHiduke(ByVal inTarget As String, ByVal PARAM As String) As String

        If inTarget = String.Empty Then
            Return String.Empty
        End If
        Dim data As Date
        Try
            data = Convert.ToDateTime(inTarget)
            If data > CommonConstBC.Instance.dateMax Or data < CommonConstBC.Instance.dateMin Then

                Return String.Format(Messages.MSG025E, PARAM).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Return String.Format(Messages.MSG025E, PARAM).ToString
        End Try
    End Function
    ''' <summary>
    ''' �o�C�g���`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="intLength"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckByte(ByVal inTarget As String, ByVal intLength As Long, ByVal PARAM As String, Optional ByVal type As kbn = kbn.HANKAKU) As String

        '�l��Check���s�Ȃ�
        If inTarget = String.Empty Then
            Return ""
        End If

        '�������`�F�b�N
        If Not byteCheckSJIS(inTarget, intLength) Then
            Dim csScript As New StringBuilder

            'MsgBox �\��
            If type = kbn.HANKAKU Then
                Return String.Format(Messages.MSG026E, PARAM, intLength)
            Else
                '�S�p�@{0}�ɓo�^�ł��镶�����́A�S�p{1}�����ȓ��ł��B
                Return String.Format(Messages.MSG027E, PARAM, intLength / 2)
            End If
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' �S�p�`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckZenkaku(ByVal inTarget As String, ByVal PARAM As String) As String

        If Not (inTarget.Length * 2 = Encoding.Default.GetByteCount(inTarget)) Then
            Return String.Format(Messages.MSG028E, PARAM).ToString
        Else
            Return ""
        End If
    End Function
    ''' <summary>
    '''  ���p�����`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <param name="kbn">1:�����`�F�b�N</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckHankaku(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal kbn As String = "") As String
        If inTarget.Length = Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            If kbn = "1" Then
                If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
                    Return String.Format(Messages.MSG029E, PARAM).ToString
                Else
                    Return ""
                End If
            Else
                Return ""
            End If

        Else
            If kbn = "1" Then
                Return String.Format(Messages.MSG029E, PARAM).ToString
            Else
                Return String.Format(Messages.MSG030E, PARAM).ToString
            End If

        End If
    End Function

    ''' <summary>
    ''' ���p�p�����`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChkHankakuEisuuji(ByVal inTarget As String, ByVal PARAM As String) As String
        Dim intCount As Long = 0
        Dim strTmp As String = 0
        ChkHankakuEisuuji = ""
        For intCount = 1 To Len(inTarget)
            strTmp = Mid(inTarget, intCount, 1)
            If Not ((Asc(strTmp) >= Asc("a") And Asc(strTmp) <= Asc("z")) _
                Or (Asc(strTmp) >= Asc("A") And Asc(strTmp) <= Asc("Z")) _
                Or (Asc(strTmp) >= Asc("0") And Asc(strTmp) <= Asc("9"))) Then
                Return String.Format(Messages.MSG031E, PARAM).ToString
            End If
        Next

    End Function
    ''' <summary>
    ''' �J�^�J�i�`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKatakana(ByVal inTarget As String, ByVal PARAM As String, Optional ByVal blnKbn As Boolean = False) As String
        Dim chkstr As String
        Dim meisai_next_cnt As Integer
        CheckKatakana = ""
        chkstr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm�������������������������������������������ܦݧ��������~!@#$%^&*()_+`-={}|:<>?[]\;,./ ��޷޸޹޺޻޼޽޾޿���������������������������'"

        For meisai_next_cnt = 1 To Len(inTarget)
            If InStr(chkstr, Mid(inTarget, meisai_next_cnt, 1)) = 0 Then
                If blnKbn Then
                    If Mid(inTarget, meisai_next_cnt, 1) <> "*" Then
                        Return String.Format(Messages.MSG032E, PARAM).ToString
                    End If
                Else
                    Return String.Format(Messages.MSG032E, PARAM).ToString
                End If

            End If
        Next
    End Function
    ''' <summary>
    ''' �֑������`�F�b�N
    ''' </summary>
    ''' <param name="inTarget"></param>
    ''' <param name="PARAM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CheckKinsoku(ByVal inTarget As String, ByVal PARAM As String) As String

        '�֑������`�F�b�N
        If Not kinsiStrCheck(inTarget) Then
            Return String.Format(Messages.MSG033E, PARAM).ToString
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' ���p�������ɕϊ�����B
    ''' </summary>
    ''' <param name="value">value</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SetTokomozi(ByVal value As String) As String
        Dim komoziString As String = "�������������������������������������������ܦݧ��������0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopasdfghjklzxcvbnm�������������������������������������������ܦݧ��������~!@#$%^&*()_+`-={}|:<>?[]\;,./ "
        Dim daiString As String = "�����������������������������������ĂƂȂɂʂ˂̂͂Ђӂւق܂݂ނ߂�������������񂟂�������������O�P�Q�R�S�T�U�V�W�X�`�a�b�c�d�e�f�g�h�i�j�k�l�m�n�o�p�q�r�s�t�u�v�w�x�y�����������������������������������������������������A�C�E�G�I�J�L�N�P�R�T�V�X�Z�\�^�`�c�e�g�i�j�k�l�m�n�q�t�w�z�}�~�����������������������������@�B�D�F�H�b�������`�I���������O�����i�j�Q�{�e�|���o�p�b�F�����H�u�v���G�A�B�E�@"
        Dim i As Integer
        Dim str As Char
        Dim str1 As String = ""
        Dim intIndex As Integer

        For i = 0 To value.Length - 1

            str = value.Chars(i)
            intIndex = daiString.IndexOf(str)
            If intIndex <> -1 Then
                str1 = str1 & komoziString.Chars(intIndex)
            Else
                str1 = str1 & str
            End If

        Next

        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")
        str1 = str1.Replace("��", "��")

        str1 = str1.Replace("�K", "��")

        str1 = str1.Replace("�M", "��")
        str1 = str1.Replace("�O", "��")
        str1 = str1.Replace("�Q", "��")
        str1 = str1.Replace("�S", "��")

        str1 = str1.Replace("�U", "��")
        str1 = str1.Replace("�W", "��")
        str1 = str1.Replace("�Y", "��")
        str1 = str1.Replace("�[", "��")
        str1 = str1.Replace("�]", "��")

        str1 = str1.Replace("�_", "��")
        str1 = str1.Replace("�f", "��")
        str1 = str1.Replace("�h", "��")

        str1 = str1.Replace("�o", "��")
        str1 = str1.Replace("�r", "��")
        str1 = str1.Replace("�u", "��")
        str1 = str1.Replace("�x", "��")
        str1 = str1.Replace("�{", "��")

        str1 = str1.Replace("�p", "��")
        str1 = str1.Replace("�s", "��")
        str1 = str1.Replace("�v", "��")
        str1 = str1.Replace("�y", "��")
        str1 = str1.Replace("�|", "��")

        str1 = str1.Replace("�[", "�")
        Return str1

    End Function

    Public Sub New()

    End Sub
End Class
