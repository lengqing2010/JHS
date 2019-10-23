Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �n�Ջ��ʏ����N���X
''' </summary>
''' <remarks></remarks>
Public Class JibanLogic

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    'Utilities��MessegeLogic�N���X
    Private mLogic As New MessageLogic

    'Utilities��StringLogic�N���X
    Private sLogic As New StringLogic
    '�����X����Logic�N���X
    Private ksLogic As New KameitenSearchLogic

    ' �f�[�^�A�N�Z�X�N���X
    Private dataAccess As New JibanDataAccess

    Private cbLogic As New CommonBizLogic

#Region "�ۏ؏�No�̍̔Ԓl�擾"
    ''' <summary>
    ''' �ۏ؏�No���̔Ԃ����ʂ��擾���܂�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kubun">�敪</param>
    ''' <param name="intRentouBukkenSuu">�A��������</param>
    ''' <param name="strLoginUserId">���O�C�����[�UID</param>
    ''' <returns>�̔Ԍ�̕ۏ؏�No�@SPACE�F�ۏ؏�NO�N�����ݒ�</returns>
    ''' <remarks>�敪��Key�ɕۏ؏�No�̍̔Ԃ��s���l��ԋp���܂�</remarks>
    Public Function GetNewHosyousyoNo( _
                                        ByVal sender As Object, _
                                        ByRef kubun As String, _
                                        ByRef intRentouBukkenSuu As Integer, _
                                        ByVal strLoginUserId As String _
                                        ) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetNewHosyousyoNo", _
                                            sender, _
                                            kubun, _
                                            intRentouBukkenSuu, _
                                            strLoginUserId _
                                            )

        ' �߂�l�D�ۏ؏�No
        Dim hosyousyoNo As String = ""
        ' �ۏ؏�No�N��(YYYY/MM)
        Dim hosyousyoNoYm As String = ""
        ' �ۏ؏�No�ŏI�ԍ�(DB�o�^�p)
        Dim hosyousyoLastNo As Integer
        ' �ۏ؏��֘A�̃f�[�^�A�N�Z�X�N���X�𐶐�
        Dim dataAccess As HosyousyoDataAccess = New HosyousyoDataAccess
        ' DB�ւ̔��f����
        Dim intResult As Integer

        ' �ۏ؏�No�N�����擾
        hosyousyoNoYm = dataAccess.GetHosyousyoNoYM(kubun)

        ' �擾�Ɏ��s�����ꍇ�A�󔒒l��ԋp
        If hosyousyoNoYm = "" Then
            Return hosyousyoNo
        End If

        If intRentouBukkenSuu <= 0 Then
            intRentouBukkenSuu = 1
        End If

        ' �ŏI�ۏ؏�No���擾
        hosyousyoLastNo = dataAccess.GetHosyousyoLastNo(kubun, hosyousyoNoYm.Replace("/", ""))

        ' �擾�ł��Ȃ������ꍇ�͐V�K�̔�
        If hosyousyoLastNo = -1 Then
            '�ԍ���U�蒼��(�A��������)
            hosyousyoLastNo = intRentouBukkenSuu

            ' TBL_�ۏ؏�NO�̔ԂɐV�K�o�^����
            intResult = dataAccess.UpdateHosyousyoNo(kubun, _
                                                      hosyousyoNoYm.Replace("/", ""), _
                                                      hosyousyoLastNo, _
                                                      "N", _
                                                      strLoginUserId)
        Else

            '�ŏI�ԍ� = �ŏI�ԍ� + �A��������
            hosyousyoLastNo += intRentouBukkenSuu

            ' 9999�𒴂����ꍇ�A�ԍ���U�蒼��(�A��������)
            If hosyousyoLastNo > 9999 Then
                hosyousyoLastNo = intRentouBukkenSuu
            End If

            ' TBL_�ۏ؏�NO�̔Ԃ��X�V����
            intResult = dataAccess.UpdateHosyousyoNo(kubun, _
                                                      hosyousyoNoYm.Replace("/", ""), _
                                                      hosyousyoLastNo, _
                                                      "U", _
                                                      strLoginUserId)

        End If

        ' �X�V�Ɏ��s�����ꍇ
        If intResult <= 0 Then
            hosyousyoNo = ""
            Return hosyousyoNo
        End If

        ' �ۏ؏�NO�̕ҏW [ �ۏ؏�NO�N��+�A��(0001�`9999) ] =>�A�������̓�����ԋp
        hosyousyoNo = hosyousyoNoYm.Replace("/", "") + Format(hosyousyoLastNo - intRentouBukkenSuu + 1, "0000")

        Return hosyousyoNo

    End Function

#End Region

#Region "���i�ݒ�ꏊ���̎擾�i���i�R�[�h�P�j"

    ''' <summary>
    ''' ���i�R�[�h1�̏����擾���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="hin1InfoRec">���i�R�[�h1�ݒ�p�p�����[�^���R�[�h</param>
    ''' <param name="hin1AutoSetRecord">���i�R�[�h1�̎����ݒ�f�[�^���R�[�h</param>
    ''' <param name="intSetSts">�擾�Z�b�g�X�e�[�^�X[0:�f�t�H���g,1:�H���X�z�Z�b�g�G���[,2:�������z�Z�b�g�G���[]</param>
    ''' <returns>�������� True:�擾���� False:�擾���s</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin1Info(ByRef sender As Object, _
                                    ByVal hin1InfoRec As Syouhin1InfoRecord, _
                                    ByRef hin1AutoSetRecord As Syouhin1AutoSetRecord, _
                                    Optional ByRef intSetSts As Integer = 0) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin1Info", _
                                            sender, _
                                            hin1InfoRec, _
                                            hin1AutoSetRecord, _
                                            intSetSts)


        '**********************************************************************
        ' �����T�v���擾���܂��i���i���i�ݒ�}�X�^�j
        '**********************************************************************
        Dim kakakuSetteiRec As New KakakuSetteiRecord
        kakakuSetteiRec.SyouhinKbn = hin1InfoRec.SyouhinKbn          ' ���i�敪
        kakakuSetteiRec.TyousaHouhouNo = hin1InfoRec.TyousaHouhouNo  ' �������@
        kakakuSetteiRec.SyouhinCd = hin1InfoRec.SyouhinCd            ' ���i�R�[�h

        ' ���i�ݒ背�R�[�h���Q�Ɠn�����Ē����T�v�Ɛݒ�ꏊ���擾����
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(kakakuSetteiRec)

        ' �����T�v���擾�ł��Ȃ������ꍇ
        If kakakuSetteiRec.TyousaGaiyou = 0 Then
            intSetSts = EarthEnum.emSyouhin1Error.GetTysGaiyou
            kakakuSetteiRec.TyousaGaiyou = 9
            'Return False
        End If

        '**********************************************************************
        ' ���i�̏ڍ׏����擾���܂�
        '**********************************************************************
        Dim tmpHin1AutoSetRecord As New Syouhin1AutoSetRecord
        Dim drList As List(Of Syouhin1AutoSetRecord)
        Dim dtResult As DataTable

        ' ���i�����擾
        dtResult = syouhinAccess.GetSyouhinInfo(kakakuSetteiRec.SyouhinCd, hin1InfoRec.KameitenCd)
        'List�Ɋi�[
        drList = DataMappingHelper.Instance.getMapArray(Of Syouhin1AutoSetRecord)(GetType(Syouhin1AutoSetRecord), dtResult)

        ' �擾���������`�F�b�N(�擾�ł��Ȃ��ꍇ�A�����I��)
        If drList.Count <= 0 Then
            intSetSts = EarthEnum.emSyouhin1Error.GetSyouhin1
            Return False
        Else
            tmpHin1AutoSetRecord = drList(0)
        End If

        '**********************************************************************
        ' �����}�X�^�̏ڍ׏����擾���܂�
        '**********************************************************************
        Dim blnGenkaSyutoku As Boolean

        blnGenkaSyutoku = GetSyoudakusyoKingaku1(hin1InfoRec.SyouhinCd, _
                                                hin1InfoRec.Kubun, _
                                                hin1InfoRec.TyousaHouhouNo, _
                                                hin1InfoRec.TyousaGaiyou, _
                                                hin1InfoRec.DoujiIraiTousuu, _
                                                hin1InfoRec.TysKaisyaCd, _
                                                hin1InfoRec.KameitenCd, _
                                                0, _
                                                hin1InfoRec.KeiretuCd, _
                                                blnGenkaSyutoku)

        If blnGenkaSyutoku = False Then
            '�����}�X�^����̂ݎ擾�o���Ȃ��ꍇ
            intSetSts = EarthEnum.emSyouhin1Error.GetGenka
        End If

        '**********************************************************************
        ' �̔����i�}�X�^�̏ڍ׏����擾���܂�
        '**********************************************************************
        Dim blnHanbaiSyutoku As Boolean
        blnHanbaiSyutoku = GetHanbaiKingaku1(hin1InfoRec, hin1AutoSetRecord)

        If blnHanbaiSyutoku = False Then
            If blnGenkaSyutoku = True Then
                '�̔����i�}�X�^����̂ݎ擾�s��
                intSetSts = EarthEnum.emSyouhin1Error.GetHanbai
            Else
                '�����E�̔����i�}�X�^�̗����擾�o���Ȃ��ꍇ
                intSetSts = EarthEnum.emSyouhin1Error.GetGenkaHanbai
            End If
        End If

        ' �擾�������i����߂�l�ɃZ�b�g
        hin1AutoSetRecord.SyouhinCd = kakakuSetteiRec.SyouhinCd                                     ' ���i�R�[�h
        hin1AutoSetRecord.TyousaGaiyou = kakakuSetteiRec.TyousaGaiyou                               ' �����T�v
        hin1AutoSetRecord.KakakuSettei = kakakuSetteiRec.KakakuSettei                               ' ���i�ݒ�ꏊ
        hin1AutoSetRecord.SyouhinNm = tmpHin1AutoSetRecord.SyouhinNm                                ' ���i��
        hin1AutoSetRecord.ZeiKbn = tmpHin1AutoSetRecord.ZeiKbn                                      ' �ŋ敪
        hin1AutoSetRecord.Zeiritu = tmpHin1AutoSetRecord.Zeiritu                                    ' �ŗ�
        hin1AutoSetRecord.TaxUri = Fix(hin1AutoSetRecord.JituGaku * tmpHin1AutoSetRecord.Zeiritu)   ' ����Ŋz
        hin1AutoSetRecord.KameitenCd = hin1InfoRec.KameitenCd                                       ' �����X�R�[�h
        '�����悪�ʂɎw�肳��Ă��Ȃ��ꍇ�A�f�t�H���g�̐�������Z�b�g
        If hin1AutoSetRecord.SeikyuuSakiCd = String.Empty Then
            hin1AutoSetRecord.SeikyuuSakiCd = tmpHin1AutoSetRecord.SeikyuuSakiCd                        ' ������R�[�h
            hin1AutoSetRecord.SeikyuuSakiBrc = tmpHin1AutoSetRecord.SeikyuuSakiBrc                      ' ������}��
            hin1AutoSetRecord.SeikyuuSakiKbn = tmpHin1AutoSetRecord.SeikyuuSakiKbn                      ' ������敪
        End If
        '������^�C�v���Z�b�g
        hin1InfoRec.Seikyuusaki = hin1AutoSetRecord.SeikyuuSakiType

        Return True

    End Function

    ''' <summary>
    ''' ���i�ݒ�ꏊ���擾���܂�
    ''' </summary>
    ''' <param name="syouhinKbn">���i�敪</param>
    ''' <param name="tyousaHouhou">�������@</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <returns>���i�ݒ�ꏊ��String�`���i��ʍ��ڂɂ��̂܂܃Z�b�g����j</returns>
    ''' <remarks></remarks>
    Public Function GetKakakuSetteiBasyo(ByVal syouhinKbn As Integer, _
                                         ByVal tyousaHouhou As Integer, _
                                         ByVal strSyouhinCd As String) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKakakuSetteiBasyo", _
                                            syouhinKbn, _
                                            tyousaHouhou, _
                                            strSyouhinCd)

        '**********************************************************************
        ' ���i�ݒ�����擾���܂�
        '**********************************************************************
        Dim kakakuSetteiRec As New KakakuSetteiRecord
        kakakuSetteiRec.SyouhinKbn = syouhinKbn            ' ���i�敪
        kakakuSetteiRec.TyousaHouhouNo = tyousaHouhou      ' �������@
        kakakuSetteiRec.SyouhinCd = strSyouhinCd           '���i�R�[�h

        ' ���i�ݒ背�R�[�h���Q�Ɠn�����ď��i�R�[�h�Ɛݒ�ꏊ���擾����
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(kakakuSetteiRec)

        ' ��ł��l���擾�ł��Ȃ������ꍇ�A�����I��
        If kakakuSetteiRec.SyouhinCd = Nothing Or _
           kakakuSetteiRec.KakakuSettei = Nothing Then
            Return ""
        Else
            Return kakakuSetteiRec.KakakuSettei.ToString()
        End If
    End Function


    ''' <summary>
    ''' ���i���i�����T�v���擾���܂�
    ''' </summary>
    ''' <param name="recKakakuSettei"></param>
    ''' <remarks></remarks>
    Public Function GetTysGaiyou(ByRef recKakakuSettei As KakakuSetteiRecord) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin1Info", _
                                            recKakakuSettei)

        '�����T�v���擾�i���i���i�ݒ�}�X�^�j
        Dim syouhinAccess As New SyouhinDataAccess
        syouhinAccess.GetKakakuSetteiInfo(recKakakuSettei)

        ' �����T�v���擾�ł��Ȃ������ꍇ
        If recKakakuSettei.TyousaGaiyou = 0 Then
            recKakakuSettei.TyousaGaiyou = 9
            Return False
        End If

        Return True
    End Function

#Region "�����XM���i�擾"
    ''' <summary>
    ''' �����X�e�[�u����艿�i���擾���܂�
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intTatemonoYouto">�����p�rNO</param>
    ''' <param name="isYoutoAdd">�����p�r�ɂ����Z�Ώ�(True:���Z����)</param>
    ''' <param name="strItem">�擾�Ώۍ��ږ�</param>
    ''' <param name="blnDelete">True:����f�[�^�����O False:����f�[�^��Ώ�</param>
    ''' <param name="kameitenKakaku">��������</param>
    ''' <returns>True:�擾���� False:�擾���s</returns>
    ''' <remarks></remarks>
    Function GetKameitenInfo(ByVal strKubun As String, _
                             ByVal strKameitenCd As String, _
                             ByVal intTatemonoYouto As Integer, _
                             ByVal isYoutoAdd As Boolean, _
                             ByVal strItem As String, _
                             ByVal blnDelete As Boolean, _
                             ByRef kameitenKakaku As Decimal) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKameitenInfo", _
                                            strKubun, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd, _
                                            strItem, _
                                            blnDelete, _
                                            kameitenKakaku)

        ' �����X�R�[�h���ݒ莞�͏����I��
        If strKameitenCd = "" Then
            Return False
        End If

        Dim kameitenAccess As New KameitenDataAccess

        If kameitenAccess.GetKameitenKakaku(strKubun, _
                                             strKameitenCd, _
                                             intTatemonoYouto, _
                                             isYoutoAdd, _
                                             strItem, _
                                             blnDelete, _
                                             kameitenKakaku) = True Then
            Return True
        End If

        Return False

    End Function
#End Region

#Region "�������z�擾"
    ''' <summary>
    ''' ������/3�n�񎞂̍H���X�������z�A���������z�̎擾
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="intMode">�擾���[�h<BR/>
    ''' 1 (���i�R�[�h1�̉����X�����i)   <BR/>
    ''' 2 (��񕥖߂̉����X�����i)      <BR/>
    ''' 3 (�{��(TH)�����i)              <BR/>
    ''' 4 (���������z / �|��)           <BR/>
    ''' 5 (�H���X�������z * �|��)       <BR/>
    ''' 6 (�����X�����i�����������z/�|��)</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z<BR/>
    ''' �擾���[�h=1 (TH�����p���i�}�X�^��KEY)<BR/>
    ''' �擾���[�h=4 (���������z)<BR/>
    ''' �擾���[�h=5 (�H���X�������z)</param>
    ''' <param name="intReturnKingaku">�擾���z�i�߂�l�j</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intTatemonoYouto">�����p�rNO</param>
    ''' <param name="isYoutoAdd">�����p�r�ɂ����Z���s���� True:�s�� False:�s��Ȃ�</param>
    ''' <returns>True:�擾OK,False:�擾NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuGaku(ByRef sender As Object, _
                                       ByVal intMode As Integer, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal intKingaku As Integer, _
                                       ByRef intReturnKingaku As Integer, _
                                       Optional ByVal strKameitenCd As String = "", _
                                       Optional ByVal intTatemonoYouto As Integer = 1, _
                                       Optional ByVal isYoutoAdd As Boolean = False) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuGaku", _
                                            sender, _
                                            intMode, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd)

        ' �n��R�[�h���͏��i�R�[�h���󔒂̏ꍇ�A�����I��
        If strKeiretuCd.Trim() = "" Or strSyouhinCd.Trim() = "" Then
            Return False
        End If

        Dim keiretu As IKeiretuDataAccess


        Dim dataAccess As New JibanDataAccess
        Dim thKasangaku As Integer = 0

        ' �n��ɂ�鏈������
        Select Case strKeiretuCd
            Case EarthConst.KEIRETU_AIFURU
                ' �n��_�A�C�t���z�[���̃C���X�^���X�𐶐�
                keiretu = New KeiretuAifuruDataAccess
            Case EarthConst.KEIRETU_TH
                ' �n��_TH�F�̉�̃C���X�^���X�𐶐�
                keiretu = New KeiretuThDataAccess

                If isYoutoAdd = True Then
                    ' TH�����̏ꍇ�͌����p�r���Z�z�����O�擾���A�������z���獷������(���i��Key�ƂȂ��)
                    thKasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)
                    intKingaku = intKingaku - thKasangaku
                End If
            Case EarthConst.KEIRETU_WANDA
                ' �n��_�����_�[�z�[���̃C���X�^���X�𐶐�
                keiretu = New KeiretuWandaDataAccess
            Case Else
                Return False
        End Select


        ' �������z���擾����
        Dim retCd As Integer = keiretu.getSeikyuKingaku(intMode, _
                                                         strSyouhinCd.Trim, _
                                                         intKingaku, _
                                                         intReturnKingaku)
        ' �߂�l�𔻒�
        Select Case retCd
            Case 1
                ' �擾�������z���O�ȊO�Ŏ擾���[�h�����i�P�̍H���X�����z�Ō����p�r���Z�ΏۂɌ���
                ' �����p�r���Z�z�}�X�^�̉��Z�z���Z�b�g����
                If intReturnKingaku <> 0 And _
                   intMode = 1 And _
                   isYoutoAdd = True Then

                    Dim kasangaku As Integer = 0
                    ' ���Z�z���擾����
                    kasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)

                    ' �擾�������Z�z�����Z
                    intReturnKingaku = intReturnKingaku + kasangaku
                End If
                Return True
            Case 0
                If TypeOf sender Is String Then
                    sender = Messages.MSG001E
                Else
                    ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "alert", "alert('" & Messages.MSG001E & "')", True)
                End If
                Return False
            Case -1
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' ������/3�n�񎞂̍H���X�������z�A���������z�̎擾(�i���ۏ؏�������ʗp�R�s�[)
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="intMode">�擾���[�h<BR/>
    ''' 1 (���i�R�[�h1�̉����X�����i)   <BR/>
    ''' 2 (��񕥖߂̉����X�����i)      <BR/>
    ''' 3 (�{��(TH)�����i)              <BR/>
    ''' 4 (���������z / �|��)           <BR/>
    ''' 5 (�H���X�������z * �|��)       <BR/>
    ''' 6 (�����X�����i�����������z/�|��)</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z<BR/>
    ''' �擾���[�h=1 (TH�����p���i�}�X�^��KEY)<BR/>
    ''' �擾���[�h=4 (���������z)<BR/>
    ''' �擾���[�h=5 (�H���X�������z)</param>
    ''' <param name="intReturnKingaku">�擾���z�i�߂�l�j</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="intTatemonoYouto">�����p�rNO</param>
    ''' <param name="isYoutoAdd">�����p�r�ɂ����Z���s���� True:�s�� False:�s��Ȃ�</param>
    ''' <returns>True:�擾OK,False:�擾NG</returns>
    ''' <remarks></remarks>
    Public Function GetSeikyuuGakuHinsitu(ByRef sender As Object, _
                                       ByVal intMode As Integer, _
                                       ByVal strKeiretuCd As String, _
                                       ByVal strSyouhinCd As String, _
                                       ByVal intKingaku As Integer, _
                                       ByRef intReturnKingaku As Integer, _
                                       Optional ByVal strKameitenCd As String = "", _
                                       Optional ByVal intTatemonoYouto As Integer = 1, _
                                       Optional ByVal isYoutoAdd As Boolean = False) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSeikyuuGaku", _
                                            sender, _
                                            intMode, _
                                            strKeiretuCd, _
                                            strSyouhinCd, _
                                            intKingaku, _
                                            intReturnKingaku, _
                                            strKameitenCd, _
                                            intTatemonoYouto, _
                                            isYoutoAdd)

        ' �n��R�[�h���͏��i�R�[�h���󔒂̏ꍇ�A�����I��
        If strKeiretuCd.Trim() = "" Or strSyouhinCd.Trim() = "" Then
            Return False
        End If

        Dim keiretu As IKeiretuDataAccess


        Dim dataAccess As New JibanDataAccess
        Dim thKasangaku As Integer = 0

        ' �n��ɂ�鏈������
        Select Case strKeiretuCd
            Case EarthConst.KEIRETU_AIFURU
                ' �n��_�A�C�t���z�[���̃C���X�^���X�𐶐�
                keiretu = New KeiretuAifuruDataAccess
            Case EarthConst.KEIRETU_TH
                ' �n��_TH�F�̉�̃C���X�^���X�𐶐�
                keiretu = New KeiretuThDataAccess

                If isYoutoAdd = True Then
                    ' TH�����̏ꍇ�͌����p�r���Z�z�����O�擾���A�������z���獷������(���i��Key�ƂȂ��)
                    thKasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)
                    intKingaku = intKingaku - thKasangaku
                End If
            Case EarthConst.KEIRETU_WANDA
                ' �n��_�����_�[�z�[���̃C���X�^���X�𐶐�
                keiretu = New KeiretuWandaDataAccess
            Case Else
                Return False
        End Select


        ' �������z���擾����
        Dim retCd As Integer = keiretu.getSeikyuKingaku(intMode, _
                                                         strSyouhinCd.Trim, _
                                                         intKingaku, _
                                                         intReturnKingaku)
        ' �߂�l�𔻒�
        Select Case retCd
            Case 1
                ' �擾�������z���O�ȊO�Ŏ擾���[�h�����i�P�̍H���X�����z�Ō����p�r���Z�ΏۂɌ���
                ' �����p�r���Z�z�}�X�^�̉��Z�z���Z�b�g����
                If intReturnKingaku <> 0 And _
                   intMode = 1 And _
                   isYoutoAdd = True Then

                    Dim kasangaku As Integer = 0
                    ' ���Z�z���擾����
                    kasangaku = dataAccess.GetTatemonoYoutoKasangaku(strKameitenCd, intTatemonoYouto)

                    ' �擾�������Z�z�����Z
                    intReturnKingaku = intReturnKingaku + kasangaku
                End If
                Return True
            Case 0
                If TypeOf sender Is String Then
                    sender = Messages.MSG001E
                Else
                    'ScriptManager.RegisterClientScriptBlock(sender, sender.GetType(), "alert", "alert('" & Messages.MSG001E & "')", True)
                End If
                Return False
            Case -1
                Return False
        End Select

        Return True
    End Function

#End Region

#End Region

#Region "�d�������`�F�b�N"

    ''' <summary>
    ''' �{�喼�̏d�������`�F�b�N
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strSesyu">�{�喼</param>
    ''' <returns>True:�d���f�[�^�L��</returns>
    ''' <remarks>�敪�A�{�喼�̏d���`�F�b�N���s�����ʂ�ԋp����B<br/>
    '''          �ۏ؏�NO�̔N�����������܂މߋ��U�����ȓ��̃f�[�^���<br/>
    '''          �敪�A�{�喼�̏d�����郌�R�[�h����������i�f�[�^�j���܂ށj</remarks>
    Public Function ChkTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal strSesyu As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            strSesyu)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As New TyoufukuDataAccess

        Dim table As TyoufukuDataSet.TyoufukuTableDataTable = GetTyouhuku(strKubun, _
                                                                          strHosyousyoNo, _
                                                                          " z.sesyu_mei COLLATE Japanese_CS_AS_KS_WS ", _
                                                                          strSesyu.Trim(), _
                                                                          "", _
                                                                          "")
        ' �f�[�^�����݂���ꍇ�A�d���L��
        If table.Count <> 0 Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' �Z���̏d�������`�F�b�N
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strJyuusyo1">�Z���P</param>
    ''' <param name="strJyuusyo2">�Z���Q</param>
    ''' <returns>True:�d���f�[�^�L��</returns>
    ''' <remarks>�敪�A�Z���P�C�Q�̏d���`�F�b�N���s�����ʂ�ԋp����B<br/>
    '''          �ۏ؏�NO�̔N�����������܂މߋ��U�����ȓ��̃f�[�^���<br/>
    '''          �敪�A�Z���P�C�Q�̏d�����郌�R�[�h����������i�f�[�^�j���܂ށj</remarks>
    Public Function ChkTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal strJyuusyo1 As String, _
                                ByVal strJyuusyo2 As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            strJyuusyo1, _
                                            strJyuusyo2)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As New TyoufukuDataAccess

        Dim table As TyoufukuDataSet.TyoufukuTableDataTable = GetTyouhuku(strKubun, _
                                                                          strHosyousyoNo, _
                                                                          " ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'') COLLATE Japanese_CS_AS_KS_WS ", _
                                                                          strJyuusyo1.Trim() & strJyuusyo2.Trim(), _
                                                                          "", _
                                                                          "")

        ' �f�[�^�����݂���ꍇ�A�d���L��
        If table.Count <> 0 Then
            Return True
        End If
        Return False

    End Function
#End Region

#Region "�d�������擾"
    ''' <summary>
    ''' �d���f�[�^�擾
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo">�\�����̕ۏ؏�NO</param>
    ''' <param name="searchItem1">�����������ږ��P�i�e�[�u���J�����j</param>
    ''' <param name="condition1">���������P</param>
    ''' <param name="searchItem2">�����������ږ��Q�i�e�[�u���J�����j</param>
    ''' <param name="condition2">���������Q</param>
    ''' <returns>�d���f�[�^�Z�b�g�̏d���f�[�^�e�[�u��</returns>
    ''' <remarks>�d���f�[�^�̌������s���܂�<br/><br/>
    ''' 
    '''     �������@�P�D�{�喼�̂ݎw�肷��<br/><br/>
    ''' 
    '''         �����������ږ��P�� "Z.sesyu_mei" ���������P�� "���~ ���j" <br/>
    '''         �����������ږ��Q�A���������Q��������󔒂�Call�����ꍇ�A<br/>
    '''         �{�喼�̂ݏd������f�[�^���������܂�<br/><br/>
    ''' 
    '''     �������@�Q�D�����Z���̂ݎw�肷��<br/><br/>
    ''' 
    '''         �����������ږ��P�� "Z.bukken_jyuusyo1 �{ Z.bukken_jyuusyo2" ���������P�� �Z��1��2�����������l <br/>
    '''         �����������ږ��Q�A���������Q��������󔒂�Call�����ꍇ�A<br/>
    '''         �����Z���̂ݏd������f�[�^���������܂�
    '''
    '''     �������@�R�D�{�喼�y�ѕ����Z�����w�肷��<br/><br/>
    ''' 
    '''         �����������ږ��P�� "Z.sesyu_mei" ���������P�� "���~ ���j" <br/>
    '''         �����������ږ��Q�� "Z.bukken_jyuusyo1 �{ Z.bukken_jyuusyo2" ���������Q�� �Z��1��2�����������l<br/>
    '''         �{�喼���͕����Z���ŏd������f�[�^���������܂�
    ''' </remarks>
    Public Function GetTyouhuku(ByVal strKubun As String, _
                                ByVal strHosyousyoNo As String, _
                                ByVal searchItem1 As String, _
                                ByVal condition1 As String, _
                                ByVal searchItem2 As String, _
                                ByVal condition2 As String) As TyoufukuDataSet.TyoufukuTableDataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyouhuku", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            searchItem1, _
                                            condition1, _
                                            searchItem2, _
                                            condition2)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As New TyoufukuDataAccess

        Return dataAccess.GetDataBy(strKubun, strHosyousyoNo, searchItem1, condition1, searchItem2, condition2)

    End Function

    ''' <summary>
    ''' �d���f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="strHosyousyoNo">�\�����̕ۏ؏�NO</param>
    ''' <param name="sesyumei">�{�喼</param>
    ''' <param name="juusyo1">�����Z���P</param>
    ''' <param name="juusyo2">�����Z���Q</param>
    ''' <returns>�d���f�[�^�̃��R�[�h���X�g</returns>
    ''' <remarks>�{�喼�܂��͕����Z���ɏd���f�[�^�����݂���ꍇ�A<br/>
    '''          �Y���f�[�^��z��ŕԋp���܂�</remarks>
    Public Function GetTyouhukuRecords(ByVal strKubun As String, _
                                       ByVal strHosyousyoNo As String, _
                                       ByVal sesyumei As String, _
                                       ByVal juusyo1 As String, _
                                       ByVal juusyo2 As String) As List(Of TyoufukuRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTyouhukuRecords", _
                                            strKubun, _
                                            strHosyousyoNo, _
                                            sesyumei, _
                                            juusyo1, _
                                            juusyo2)

        ' �߂�l�ƂȂ郊�X�g
        Dim returnRec As New List(Of TyoufukuRecord)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As New TyoufukuDataAccess

        Dim jParam1 As String = ""
        Dim jParam2 As String = ""
        Dim jParam3 As String = ""
        Dim jParam4 As String = ""

        ' �{�喼�����ݒ�̏ꍇ�͏d���ł͂Ȃ��̂ŏ������珜�O����
        If sesyumei.Trim() <> "" Then
            jParam1 = " z.sesyu_mei COLLATE Japanese_CS_AS_KS_WS  "
            jParam2 = sesyumei.Trim()
        End If

        ' �Z����񂪖��ݒ�̏ꍇ�͏d���ł͂Ȃ��̂ŏ������珜�O����
        If juusyo1.Trim() & juusyo2.Trim() <> "" Then
            If jParam1 = "" Then
                jParam1 = " (ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'')) COLLATE Japanese_CS_AS_KS_WS "
                jParam2 = juusyo1.Trim() & juusyo2.Trim()
            Else
                jParam3 = " (ISNULL(z.bukken_jyuusyo1,'') + ISNULL(z.bukken_jyuusyo2,'')) COLLATE Japanese_CS_AS_KS_WS "
                jParam4 = juusyo1.Trim() & juusyo2.Trim()
            End If
        End If

        ' �l���擾�ł����ꍇ�A�߂�l�ɐݒ肷��
        For Each row As TyoufukuDataSet.TyoufukuTableRow In dataAccess.GetDataBy(strKubun, _
                                                                                  strHosyousyoNo, _
                                                                                  jParam1, _
                                                                                  jParam2, _
                                                                                  jParam3, _
                                                                                  jParam4)
            Dim tyoufukuRec As New TyoufukuRecord

            tyoufukuRec.HakiSyubetu = row.haki_syubetu
            tyoufukuRec.Kubun = row.kbn
            tyoufukuRec.HosyousyoNo = row.hosyousyo_no
            tyoufukuRec.Sesyumei = row.sesyu_mei
            tyoufukuRec.Jyuusyo1 = row.bukken_jyuusyo1
            tyoufukuRec.Jyuusyo2 = row.bukken_jyuusyo2
            tyoufukuRec.KameitenNm = row.kameiten_mei1
            tyoufukuRec.Bikou = row.bikou
            ' ���X�g�ɃZ�b�g
            returnRec.Add(tyoufukuRec)

        Next

        Return returnRec

    End Function

#End Region

#Region "���i�R�[�h�P���z�擾"

    ''' <summary>
    ''' ���i�R�[�h�P�̏��������z�ݒ�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h1�i�K�{�j</param>
    ''' <param name="strKubun">�敪</param>
    ''' <param name="intTyousaHouhou">�������@</param>
    ''' <param name="intTyousaGaiyou">�����T�v</param>
    ''' <param name="intDoujiIraiSuu">�����˗�������</param>
    ''' <param name="strTyousakaisyaCd">������к���+���Ə����ށi�K�{�j</param>
    ''' <param name="strKameitenCd">�����X���ށi�K�{�j</param>
    ''' <param name="intSyoudakuKingaku">�������z�i�擾�p�Q�ƈ����j</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="blnHenkouKahiFlg">���������z�ύX��FLG�i�擾�p�Q�ƈ����j</param>
    ''' <returns>true:�擾���� false:�擾���s</returns>
    ''' <remarks>��L�����̐����ɂ�������𖞂����Ȃ��ꍇ�A�߂�l��False���Ԃ�A<br/>
    '''          ���������z�͐ݒ肳��܂���<br/><br/>
    ''' </remarks>
    Public Function GetSyoudakusyoKingaku1(ByVal strSyouhinCd As String, _
                                           ByVal strKubun As String, _
                                           ByVal intTyousaHouhou As Integer, _
                                           ByVal intTyousaGaiyou As Integer, _
                                           ByVal intDoujiIraiSuu As Integer, _
                                           ByVal strTyousakaisyaCd As String, _
                                           ByVal strKameitenCd As String, _
                                           ByRef intSyoudakuKingaku As Integer, _
                                           ByVal strKeiretuCd As String, _
                                           ByRef blnHenkouKahiFlg As Boolean) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyoudakusyoKingaku1", _
                                            strSyouhinCd, _
                                            strKubun, _
                                            intTyousaHouhou, _
                                            intTyousaGaiyou, _
                                            intDoujiIraiSuu, _
                                            strTyousakaisyaCd, _
                                            strKameitenCd, _
                                            intSyoudakuKingaku, _
                                            strKeiretuCd, _
                                            blnHenkouKahiFlg)

        '****************************************************
        ' �����̃`�F�b�N�i��ł�NG�̏ꍇFalse�ŏ����I���j
        '****************************************************

        '�����˗��������Ȃ�or0��1�̈���
        If intDoujiIraiSuu = Integer.MinValue Or IsDBNull(intDoujiIraiSuu) Or intDoujiIraiSuu < 1 Then
            intDoujiIraiSuu = 1
        End If

        '���i�R�[�h1���݃`�F�b�N�i���݂��Ȃ��ꍇ�͎Z�o���Ȃ��j
        If strSyouhinCd = Nothing OrElse strSyouhinCd = String.Empty Then
            ' ���ݒ�̏ꍇ�A�����I��
            Return False
        End If

        '����O1 ������ЃR�[�h�`�F�b�N
        If strTyousakaisyaCd <> Nothing AndAlso strTyousakaisyaCd <> String.Empty Then
            If strTyousakaisyaCd.Trim() = EarthConst.KARI_TYOSA_KAISYA_CD Then
                '������Ж�����̏ꍇ0�~�E�ύX�s��
                intSyoudakuKingaku = 0
                blnHenkouKahiFlg = False
                Return True
            End If
        End If

        '����O2 �������@�̃`�F�b�N
        '������̏ꍇ0�~�E�ύX�s��
        If intTyousaHouhou = EarthConst.KARI_TYOUSA_HOUHOU_CD Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = False
            Return True
        End If

        '����O4 �敪�}�X�^�E�����}�X�^��Q�ƃt���O�𔻒f���A��Q�Ƃ̏ꍇ0�~�E�ύX�s��
        Dim recKubun As New KubunRecord
        recKubun = getKubunRecord(strKubun)
        If recKubun.GenkaMasterHisansyouFlg = 1 Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = False
            Return True
        End If

        '����O3 �������@�}�X�^�E�����ݒ�s�v�t���O�𔻒f���A�s�v�̏ꍇ0�~�E�ύX��
        Dim recTysHouhou As New TyousahouhouRecord
        recTysHouhou = getTyousahouhouRecord(intTyousaHouhou)
        If recTysHouhou.GenkaSetteiFuyouFlg >= 1 Then
            intSyoudakuKingaku = 0
            blnHenkouKahiFlg = True
            Return True
        End If

        '�������}�X�^�擾�p�̕K�{�`�F�b�N
        '������ЃR�[�h�`�F�b�N
        If strTyousakaisyaCd = Nothing OrElse strTyousakaisyaCd = String.Empty Then
            '���ݒ�̏ꍇ�A�����I��
            Return False
        End If

        '�V ���������z�̎擾
        Dim daGenka As New GenkaDataAccess

        'JIO��FLG�̎擾
        Dim ksRec As KameitenSearchRecord = ksLogic.GetKameitenSearchResult(strKubun, strKameitenCd, strTyousakaisyaCd, False)
        Dim strJioSakiFlg As String = IIf(ksRec.JioSakiFLG = Integer.MinValue, String.Empty, CStr(ksRec.JioSakiFLG))

        '�u�����X�v�̌����Z�o
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                                      EarthConst.AITESAKI_KAMEITEN, _
                                      strKameitenCd, _
                                      strSyouhinCd, _
                                      intTyousaHouhou, _
                                      intDoujiIraiSuu, _
                                      intSyoudakuKingaku, _
                                      blnHenkouKahiFlg, _
                                      True) = True Then
            Return True
        End If

        '�����XM.JIO��FLG = 1�̏ꍇ
        If strJioSakiFlg = EarthConst.ARI_VAL Then
            '�uJIO�v�̌����Z�o
            If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                                          EarthConst.AITESAKI_JIO_SAKI_FLG, _
                                          EarthConst.AITESAKI_JIO_SAKI_CD, _
                                          strSyouhinCd, _
                                          intTyousaHouhou, _
                                          intDoujiIraiSuu, _
                                          intSyoudakuKingaku, _
                                          blnHenkouKahiFlg, _
                                          True) = True Then
                Return True
            End If
        End If

        '�u�n��v�̌����Z�o���@
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                              EarthConst.AITESAKI_KEIRETU, _
                              strKeiretuCd, _
                              strSyouhinCd, _
                              intTyousaHouhou, _
                              intDoujiIraiSuu, _
                              intSyoudakuKingaku, _
                              blnHenkouKahiFlg, _
                              True) = True Then
            Return True
        End If

        '�u�w�薳�v�̌����Z�o���@
        If daGenka.GetSyoudakuKingaku(strTyousakaisyaCd, _
                              EarthConst.AITESAKI_NASI, _
                              EarthConst.AITESAKI_NASI_CD, _
                              strSyouhinCd, _
                              intTyousaHouhou, _
                              intDoujiIraiSuu, _
                              intSyoudakuKingaku, _
                              blnHenkouKahiFlg, _
                              True) = True Then
            Return True
        End If

        ' �����������z�̎擾
        'Dim dataAccess As New SyoudakusyoKingakuDataAccess
        'If dataAccess.GetSyoudakuKingaku(strTyousakaisyaCd, _
        '                                  strKameitenCd, _
        '                                  intDoujiIraiSuu, _
        '                                  intSyoudakuKingaku) = False Then

        '    Return False
        'End If



        ' ���n�Ղł͂��̃^�C�~���O�ŏ���Ŋz�A�ō����z�̉�ʐݒ�A
        ' ���i�R�[�h1,2�̐��������s���A����N�����̘A�����s���Ă���̂�
        ' ��ʏ������ɕK��������������K�v����

        Return False

    End Function

    ''' <summary>
    ''' ���i�R�[�h�P�̔̔����i�ݒ�
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHanbaiKingaku1(ByVal hin1InfoRec As Syouhin1InfoRecord, _
                                      ByRef hin1AutoSetRecord As Syouhin1AutoSetRecord) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetHanbaiKingaku1", _
                                                    hin1InfoRec, _
                                                    hin1AutoSetRecord)

        '****************************************************
        ' ��O�p�^�[���̃`�F�b�N�i��ł�NG�̏ꍇFalse�ŏ����I���j
        '****************************************************

        '���i�R�[�h1���݃`�F�b�N
        If hin1InfoRec Is Nothing OrElse String.IsNullOrEmpty(hin1InfoRec.SyouhinCd) Then
            '���ݒ�̏ꍇ�����I��
            Return False
        End If

        '���O�ɏ��iM.�����L���敪�𒲂ׂ�
        Dim recSyouhin As New SyouhinMeisaiRecord
        Dim blnTysUmKbn As Boolean = True          '�����L���敪�iT�F�L��,F:�����j
        recSyouhin = GetSyouhinRecord(hin1InfoRec.SyouhinCd)
        If recSyouhin.TysUmuKbn <> 1 Then
            blnTysUmKbn = False
        End If
        '���i����ݒ�
        hin1AutoSetRecord.ZeiKbn = recSyouhin.ZeiKbn            ' �ŋ敪
        hin1AutoSetRecord.Zeiritu = recSyouhin.Zeiritu          ' �ŗ�

        '����O1 ������ЃR�[�h�`�F�b�N
        If hin1InfoRec.TysKaisyaCd <> String.Empty Then
            If hin1InfoRec.TysKaisyaCd = EarthConst.KARI_TYOSA_KAISYA_CD Then
                '������Ж�����̏ꍇ0�~�E�ύX�s��
                hin1AutoSetRecord.KoumutenGaku = 0
                hin1AutoSetRecord.KoumutenGakuHenkouFlg = False
                hin1AutoSetRecord.JituGaku = 0
                hin1AutoSetRecord.JituGakuHenkouFlg = False
                Return True
            End If
        End If

        '����O2 �������@�̃`�F�b�N
        If hin1InfoRec.TyousaHouhouNo = EarthConst.KARI_TYOUSA_HOUHOU_CD Then
            '�������@������̏ꍇ0�~�E�ύX�s��
            hin1AutoSetRecord.KoumutenGaku = 0
            hin1AutoSetRecord.KoumutenGakuHenkouFlg = False
            hin1AutoSetRecord.JituGaku = 0
            hin1AutoSetRecord.JituGakuHenkouFlg = False
            Return True
        End If

        '����O3 �������@�}�X�^�E���i�ݒ�s�v�t���O�𔻒f���A�s�v�̏ꍇ0�~�E�ύX�\
        Dim recTysHouhou As New TyousahouhouRecord
        If blnTysUmKbn Then
            '���iM.�����L���������ꍇ�́A���i�ݒ�s�v�t���O�����Ȃ�
            recTysHouhou = getTyousahouhouRecord(hin1InfoRec.TyousaHouhouNo)
            If recTysHouhou.KakakuSetteiFuyouFlg >= 1 Then
                hin1AutoSetRecord.KoumutenGaku = 0
                hin1AutoSetRecord.KoumutenGakuHenkouFlg = True
                hin1AutoSetRecord.JituGaku = 0
                hin1AutoSetRecord.JituGakuHenkouFlg = True
                Return True
            End If
        End If

        '****************************************************
        ' �̔����i�}�X�^����̋��z�E�ύXFLG�擾
        '****************************************************

        '���̔����i�̎擾
        Dim daHanbai As New HanbaiKakakuDataAccess

        '�u�����X�v�̔̔����i�Z�o
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_KAMEITEN, _
                                    hin1InfoRec.KameitenCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '�u�c�Ə��v�̔̔����i�Z�o
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_EIGYOUSYO, _
                                    hin1InfoRec.EigyousyoCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '�u�n��v�̔̔����i�Z�o
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_KEIRETU, _
                                    hin1InfoRec.KeiretuCd, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If

        '�u�w�薳�v�̔̔����i�Z�o
        If daHanbai.GetHanbaiKakaku(EarthConst.AITESAKI_NASI, _
                                    EarthConst.AITESAKI_NASI_CD, _
                                    hin1InfoRec.SyouhinCd, _
                                    hin1InfoRec.TyousaHouhouNo, _
                                    hin1AutoSetRecord.KoumutenGaku, _
                                    hin1AutoSetRecord.KoumutenGakuHenkouFlg, _
                                    hin1AutoSetRecord.JituGaku, _
                                    hin1AutoSetRecord.JituGakuHenkouFlg, _
                                    blnTysUmKbn, _
                                    True) Then
            Return True
        End If


        Return False
    End Function

#End Region

#Region "���i����2,3���擾"
    ''' <summary>
    ''' ���i�R�[�h�Q�܂��͂R�̏���Syouhin23Record�N���X��List(Of Syouhin23Record)�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSyouhinNm">���i��</param>
    ''' <param name="type">���i�R�[�h�Q�A�R�̎w��</param>
    ''' <param name="allCount">�������̎擾�S����</param>
    ''' <param name="TyousaHouhouNo">�������@No</param>
    ''' <param name="kameitenCd">�i�C�Ӂj�����X�R�[�h Default:""</param>
    ''' <param name="startRow">�i�C�Ӂj�f�[�^���o���̊J�n�s(1���ڂ�1���w��)Default:1</param>
    ''' <param name="endRow">�i�C�Ӂj�f�[�^���o���̏I���s Default:99999999</param>
    ''' <returns>Syouhin23Record�N���X��List(Of Syouhin23Record)</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin23(ByVal strSyouhinCd As String, _
                                 ByVal strSyouhinNm As String, _
                                 ByVal type As EarthEnum.EnumSyouhinKubun, _
                                 ByRef allCount As Integer, _
                                 ByVal TyousaHouhouNo As Integer, _
                                 Optional ByVal kameitenCd As String = "", _
                                 Optional ByVal startRow As Integer = 1, _
                                 Optional ByVal endRow As Integer = 99999999) As List(Of Syouhin23Record)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23", _
                                                    strSyouhinCd, _
                                                    strSyouhinNm, _
                                                    type, _
                                                    allCount, _
                                                    TyousaHouhouNo, _
                                                    kameitenCd, _
                                                    startRow, _
                                                    endRow)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of Syouhin23Record)
        Dim total_count As Integer = 0
        Dim kameitenLogic As New KameitenSearchLogic
        Dim kameitenList As New List(Of KameitenSearchRecord)
        Dim recTysHouhou As New TyousahouhouRecord

        ' ���͂��ꂽ���i�R�[�h��啶���ɕϊ�
        If strSyouhinCd <> "" Then
            strSyouhinCd = UCase(strSyouhinCd)
        End If

        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, strSyouhinNm, Type, kameitenCd)

        ' ���i�R�[�h='A2001'�̏ꍇ�A�����XM����擾����
        If (type = EarthEnum.EnumSyouhinKubun.Syouhin3) AndAlso (strSyouhinCd = EarthConst.SYOUHIN3_AUTO_CD) AndAlso (kameitenCd <> "") Then
            kameitenList = kameitenLogic.GetKameitenSearchResult("", _
                                           kameitenCd, _
                                           True, _
                                           total_count)
        End If

        ' ������ݒ�
        allCount = table.Rows.Count

        ' �f�[�^���擾���AList(Of Syouhin23Record)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of Syouhin23Record)(GetType(Syouhin23Record), table, startRow, endRow)

        '�������@NO�̃`�F�b�N
        If TyousaHouhouNo > Integer.MinValue Then
            recTysHouhou = Me.getTyousahouhouRecord(TyousaHouhouNo)
        End If

        ' ���i3�̏ꍇ�A�Y�������X�ŁA���i�R�[�h='A2001' and �������@No='15'�̏ꍇ�A �����XM.SSGR���i�ŁA
        '                            ���i�R�[�h='A2001' and �������@No<>'15'�̏ꍇ�A�����XM.��͕ۏ؉��i�ŁAlist�̕W�����i���X�V
        If (Not recTysHouhou Is Nothing) AndAlso (recTysHouhou.Torikesi = 0) Then
            If (type = EarthEnum.EnumSyouhinKubun.Syouhin3) AndAlso (strSyouhinCd = EarthConst.SYOUHIN3_AUTO_CD) Then
                If kameitenList.Count < 1 Then
                    'list(0).HyoujunKkk = 0
                ElseIf kameitenList.Count = 1 Then
                    If TyousaHouhouNo = EarthConst.TYOUSA_HOUHOU_CD_15 Then
                        list(0).HyoujunKkk = kameitenList(0).SsgrKkk
                    Else
                        list(0).HyoujunKkk = kameitenList(0).KaisekiHosyouKkk
                    End If
                End If
            End If
        End If

        Return list

    End Function

    ''' <summary>
    ''' ���i�R�[�h�Q�A�R�̏��Ɖ�ʂ̐ݒ���e�����ɐ��������쐬���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="syouhin23Info">���i�R�[�h�Q�̏��Ɖ�ʂ̐ݒ���e</param>
    ''' <param name="intMode">���������z�̐ݒ胂�[�h 1:�ݒ�,0:�ݒ薳��</param>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Function GetSyouhin23SeikyuuData(ByRef sender As Object, _
                                           ByVal syouhin23Info As Syouhin23InfoRecord, _
                                           ByVal intMode As Integer, _
                                           Optional ByRef intSetSts As Integer = 0) As TeibetuSeikyuuRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhin23SeikyuuData", _
                                            sender, _
                                            syouhin23Info, _
                                            intMode)

        ' �߂�l�ƂȂ鏤�i�Q�A�R�̐������
        Dim seikyuuRec As New TeibetuSeikyuuRecord

        seikyuuRec.SyouhinCd = syouhin23Info.Syouhin2Rec.SyouhinCd  ' ���i�R�[�h
        seikyuuRec.ZeiKbn = syouhin23Info.Syouhin2Rec.ZeiKbn        ' �ŋ敪
        If syouhin23Info.SeikyuuUmu <> 0 Then                       ' �����L��
            seikyuuRec.SeikyuuUmu = 1
        Else
            seikyuuRec.SeikyuuUmu = syouhin23Info.SeikyuuUmu
        End If
        If syouhin23Info.HattyuusyoKakuteiFlg <> 0 Then            ' �������m��FLG
            seikyuuRec.HattyuusyoKakuteiFlg = 1
        Else
            seikyuuRec.HattyuusyoKakuteiFlg = syouhin23Info.HattyuusyoKakuteiFlg
        End If

        Dim blnSeikyuuYMD As Boolean = False

        If seikyuuRec.SeikyuuUmu = 1 Then

            blnSeikyuuYMD = True

            ' �����悪���ڐ����̏ꍇ
            If syouhin23Info.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU Then
                ' �H���X�����z�A������z�ɕW�����i���Z�b�g
                seikyuuRec.KoumutenSeikyuuGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
                seikyuuRec.UriGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
            ElseIf syouhin23Info.KeiretuFlg = 1 Then
                '��������3�n��
                ' �H���X�����z�ɕW�����i���Z�b�g
                seikyuuRec.KoumutenSeikyuuGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk

                If seikyuuRec.KoumutenSeikyuuGaku = 0 Then
                    ' �H���X�����z���O�̏ꍇ�A������z���O
                    seikyuuRec.UriGaku = 0
                Else
                    ' ������z�̐ݒ�
                    If GetSeikyuuGaku(sender, _
                                   3, _
                                   syouhin23Info.KeiretuCd, _
                                   syouhin23Info.Syouhin2Rec.SyouhinCd, _
                                   0, _
                                   seikyuuRec.UriGaku) = False Then
                        intSetSts = 1
                    End If
                End If
            Else
                '��������3�n��ȊO
                ' �H���X�����z=0�A������z�ɕW�����i���Z�b�g
                seikyuuRec.KoumutenSeikyuuGaku = 0
                seikyuuRec.UriGaku = syouhin23Info.Syouhin2Rec.HyoujunKkk
            End If

            ' ���������z�̐ݒ�
            If intMode = 1 Then
                ' �q�ɃR�[�h"110,115,120"�̏ꍇ
                If syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_110 _
                    OrElse syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 _
                    OrElse syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_3 Then

                    If syouhin23Info.Syouhin2Rec.SiireKkk = Integer.MinValue Then
                        seikyuuRec.SiireGaku = Integer.MinValue
                    Else
                        seikyuuRec.SiireGaku = syouhin23Info.Syouhin2Rec.SiireKkk
                    End If
                End If
            End If

            ' �q�ɃR�[�h��"115"�̏ꍇ�A�������]
            If syouhin23Info.Syouhin2Rec.SoukoCd = EarthConst.SOUKO_CD_SYOUHIN_2_115 Then
                seikyuuRec.KoumutenSeikyuuGaku = Math.Abs(seikyuuRec.KoumutenSeikyuuGaku) * -1
                seikyuuRec.UriGaku = Math.Abs(seikyuuRec.UriGaku) * -1
                seikyuuRec.SiireGaku = Math.Abs(seikyuuRec.SiireGaku) * -1
            End If

        End If

        Return seikyuuRec

    End Function

#End Region

#Region "���i���̎擾(���ڐ���/������)"
    ''' <summary>
    ''' ���i�R�[�h�A���i�^�C�v��Key�ɏ��i�����擾���܂�(���ڐ���/������)
    ''' </summary>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strSyouhinType">���i�^�C�v(SyouhinDataAccess.enumSyouhinKubun)</param>
    ''' <returns>���i��񃌃R�[�h</returns>
    ''' <remarks>�擾�ł��Ȃ��ꍇ��Nothing��ԋp���܂�</remarks>
    Public Function GetSyouhinInfo(ByVal strSyouhinCd As String, _
                                   ByVal strSyouhinType As EarthEnum.EnumSyouhinKubun, _
                                   ByVal strKameitenCd As String) As Syouhin23Record

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetSyouhinInfo", _
                                                    strSyouhinCd, _
                                                    strSyouhinType)

        Dim dataAccess As New SyouhinDataAccess
        Dim list As List(Of Syouhin23Record)
        Dim table As DataTable = dataAccess.GetSyouhinInfo(strSyouhinCd, "", strSyouhinType, strKameitenCd)

        ' �f�[�^���擾���AList(Of SyouhinRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of Syouhin23Record)(GetType(Syouhin23Record), _
                                                      table)

        ' �P���擾��ړI�Ƃ��Ă���̂Ŗ������ɂP���ڂ�ԋp
        If list.Count > 0 Then
            Return list(0)
        End If

        Return Nothing
    End Function
#End Region

#Region "���i�P���������s��,����N�����̐ݒ菈��"

    ''' <summary>
    ''' ���i�R�[�h�P�̐��������s��,����N�����̏������s���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="tyousaJissibi">�������{��</param>
    ''' <param name="rec">�ݒ�ΏۂƂȂ鏤�i�P���R�[�h�̓@�ʐ������R�[�h</param>
    ''' <param name="uriSet">����N�����𖳏����ɍĐݒ肷��ꍇ:true</param>
    ''' <remarks>����Ɏg�p���鍀�ځF�m��敪</remarks>
    Public Sub SubSeikyuuUriageDateSet(ByVal kameitenCd As String, _
                                       ByVal tyousaJissibi As DateTime, _
                                       ByRef rec As TeibetuSeikyuuRecord, _
                                       ByVal uriSet As Boolean)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SubSeikyuuUriageDateSet", _
                                            kameitenCd, _
                                            tyousaJissibi, _
                                            rec, _
                                            uriSet)
        Dim cBizLogic As New CommonBizLogic
        ' �������{�����w�莞�A�����I��
        If tyousaJissibi = DateTime.MinValue Then
            Exit Sub
        End If

        ' �����L�����w�莞�A�����I��
        If rec.SeikyuuUmu = Integer.MinValue Then
            Exit Sub
        End If

        ' �����L���F����̏ꍇ�A���������s����ݒ肷��i���i�P�͒������{���L�肪�O��j
        If rec.SeikyuuUmu = 1 Then
            ' ���������s���͖��w��̏ꍇ�̂ݐݒ肷��
            If rec.SeikyuusyoHakDate = Date.MinValue Then
                Dim strSimeDate As String
                '���������ߓ����擾
                strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(rec.SeikyuuSakiCd _
                                                                        , rec.SeikyuuSakiBrc _
                                                                        , rec.SeikyuuSakiKbn _
                                                                        , kameitenCd _
                                                                        , rec.SyouhinCd)
                ' ���������s�����Z�b�g
                rec.SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
            End If
        End If

        ' ����N�����F�����͂̏ꍇ�ݒ肷��
        If rec.UriDate = Date.MinValue Then
            rec.UriDate = Date.Now
        ElseIf uriSet = True Then
            rec.UriDate = Date.Now
        End If

    End Sub

#End Region

#Region "���i(�@�ʐ���)���R�[�h���I��������[����]"

#Region "���i1"
    ''' <summary>
    ''' ���i�R�[�h�P�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Function Syouhin1Set(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase) As Boolean
        ' �f�[�^�擾�p�p�����[�^�N���X
        Dim param_rec As New Syouhin1InfoRecord
        ' �擾���R�[�h�i�[�N���X
        Dim syouhin_rec As New Syouhin1AutoSetRecord

        '���n��R�[�h�̎擾
        Dim kameitenlogic As New KameitenSearchLogic
        Dim blnTorikesi As Boolean = False
        Dim record As KameitenSearchRecord = kameitenlogic.GetKameitenSearchResult(jibanRec.Kbn, jibanRec.KameitenCd, jibanRec.TysKaisyaCd + jibanRec.TysKaisyaJigyousyoCd, blnTorikesi)

        jibanRec.SyouhinKkk_KeiretuCd = record.KeiretuCd        '�n��R�[�h���Z�b�g
        jibanRec.SyouhinKkk_EigyousyoCd = record.EigyousyoCd    '�c�Ə��R�[�h���Z�b�g

        '��1.���i�ݒ�ɕK�v�ȉ�ʏ��̎擾
        ' �f�[�^�擾�p�̃p�����[�^�Z�b�g 
        '�敪
        param_rec.Kubun = jibanRec.Kbn
        '���i�敪
        param_rec.SyouhinKbn = jibanRec.SyouhinKbn
        '�����p�r
        param_rec.TatemonoYouto = jibanRec.TatemonoYoutoNo
        '�����T�v
        param_rec.TyousaGaiyou = jibanRec.TysGaiyou
        '���i�R�[�h
        param_rec.SyouhinCd = jibanRec.SyouhinCd1
        '������ЃR�[�h+���Ə��R�[�h
        param_rec.TysKaisyaCd = jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd
        '�����˗�����
        param_rec.DoujiIraiTousuu = jibanRec.DoujiIraiTousuu
        '�������@
        param_rec.TyousaHouhouNo = jibanRec.TysHouhou
        '�����X�R�[�h
        param_rec.KameitenCd = jibanRec.KameitenCd
        '�n��R�[�h
        param_rec.KeiretuCd = jibanRec.SyouhinKkk_KeiretuCd
        '�n��t���O
        param_rec.KeiretuFlg = Me.GetKeiretuFlg(jibanRec.SyouhinKkk_KeiretuCd)
        '�c�Ə��R�[�h
        param_rec.EigyousyoCd = jibanRec.SyouhinKkk_EigyousyoCd
        '������
        If param_rec.KameitenCd.Trim() <> "" Then '�����X�R�[�h�w�莞
            ' �����X�R�[�h�ƒ��������悪����̏ꍇ�A���ڐ���
            If param_rec.KameitenCd = record.TysSeikyuuSaki Then
                param_rec.Seikyuusaki = EarthConst.SEIKYU_TYOKUSETU ' ������
                jibanRec.SyouhinKkk_SeikyuuSaki = EarthConst.SEIKYU_TYOKUSETU ' ���ڐ���
            Else
                param_rec.Seikyuusaki = EarthConst.SEIKYU_TASETU ' ������
                jibanRec.SyouhinKkk_SeikyuuSaki = EarthConst.SEIKYU_TASETU ' ������
            End If
        End If

        '���������z(�Ŕ����z)=>�����Z�b�g�Ȃ̂�0
        param_rec.ZeinukiKingaku1 = 0

        '�H���X�����z=>�����Z�b�g�Ȃ̂�0
        param_rec.KoumutenKingaku1 = 0

        '�擾�X�e�[�^�X
        Dim intSetSts As EarthEnum.emSyouhin1Error

        '��2.���i1���̎擾
        ' ���i�P�����擾���A���R�[�h�N���X�ɃZ�b�g
        If Me.GetSyouhin1Info(sender, param_rec, syouhin_rec, intSetSts) = True Then

            '���i1�擾�X�e�[�^�X�G���[
            If intSetSts > 0 Then
                jibanRec.SyouhinKkk_ErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)
                Return False
                Exit Function
            End If

            '���i1���R�[�h�̐���
            If jibanRec.Syouhin1Record Is Nothing Then
                jibanRec.Syouhin1Record = New TeibetuSeikyuuRecord
            End If

            With jibanRec.Syouhin1Record
                '�敪
                .Kbn = jibanRec.Kbn
                '�ۏ؏�NO
                .HosyousyoNo = jibanRec.HosyousyoNo
                '���ރR�[�h
                .BunruiCd = EarthConst.SOUKO_CD_SYOUHIN_1
                '��ʕ\��NO
                .GamenHyoujiNo = 1
                '���i�R�[�h
                .SyouhinCd = syouhin_rec.SyouhinCd
                '������z(���������z)
                .UriGaku = syouhin_rec.JituGaku
                '�d�����z(���������z)(�ʃ��\�b�h�Ŏ擾
                .SiireGaku = 0
                '�ŋ敪
                .ZeiKbn = syouhin_rec.ZeiKbn
                '�ŗ�
                .Zeiritu = syouhin_rec.Zeiritu
                '����Ŋz(���R�[�h�N���X�Ŏ����Z�o�̂��ߏȗ�)
                '���������s��
                .SeikyuusyoHakDate = DateTime.MinValue '�㑱�ɂăZ�b�g
                '����N����
                .UriDate = DateTime.MinValue
                '�`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
                .DenpyouUriDate = DateTime.MinValue
                '�����L��
                .SeikyuuUmu = 1
                '�m��敪
                .KakuteiKbn = Integer.MinValue
                '����v��FLG
                .UriKeijyouFlg = 0
                '����v���
                .UriKeijyouDate = DateTime.MinValue
                '���l
                .Bikou = String.Empty
                '�H���X�������z
                .KoumutenSeikyuuGaku = syouhin_rec.KoumutenGaku
                '���������z
                .HattyuusyoGaku = Integer.MinValue
                '�������m�F��
                .HattyuusyoKakuninDate = DateTime.MinValue
                '�ꊇ����FLG
                .IkkatuNyuukinFlg = Integer.MinValue
                '�������Ϗ��쐬��
                .TysMitsyoSakuseiDate = DateTime.MinValue
                '�������m��FLG
                .HattyuusyoKakuteiFlg = 0
                '�X�V���O�C�����[�U�[ID
                .UpdLoginUserId = jibanRec.UpdLoginUserId
                '***********************************
            End With

            ' ���i�ݒ�ꏊ���Z�b�g
            jibanRec.SyouhinKkk_SetteiBasyo = syouhin_rec.KakakuSettei

            '������^�C�v���Z�b�g
            jibanRec.SyouhinKkk_SeikyuuSakiType = syouhin_rec.SeikyuuSakiType

        Else
            '�X�e�[�^�X�ɂ��A�G���[���b�Z�[�W�擾
            jibanRec.SyouhinKkk_ErrMsg += cbLogic.GetSyouhin1ErrMsg(intSetSts)

            Return False
            Exit Function
        End If

        Return True
    End Function

    ''' <summary>
    ''' ���i�P���������z�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub Syouhin1SyoudakuGakuSet(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase)
        ' �擾���鏳�������i
        Dim syoudaku_kakaku As Integer = 0

        Dim chousa_jouhou As Integer
        Dim chousa_gaiyou As Integer
        Dim kakaku_settei As Integer
        Dim irai_tousuu As Integer
        Dim blnSyoudakuHenkouFlg As Boolean '���������z�ύX�\FLG

        With jibanRec

            ' ���l���ڂ̕ϊ�
            cbLogic.SetDisplayString(.TysHouhou, chousa_jouhou)
            cbLogic.SetDisplayString(.TysGaiyou, chousa_gaiyou)
            cbLogic.SetDisplayString(.SyouhinKkk_SetteiBasyo, kakaku_settei)
            cbLogic.SetDisplayString(.DoujiIraiTousuu, irai_tousuu)

            If Me.GetSyoudakusyoKingaku1(.Syouhin1Record.SyouhinCd, _
                                         .Syouhin1Record.Kbn, _
                                         chousa_jouhou, _
                                         chousa_gaiyou, _
                                         irai_tousuu, _
                                         .TysKaisyaCd & .TysKaisyaJigyousyoCd, _
                                         .KameitenCd, _
                                         syoudaku_kakaku, _
                                         .SyouhinKkk_KeiretuCd, _
                                         blnSyoudakuHenkouFlg) = True Then

                ' ���������z���Z�b�g
                .Syouhin1Record.SiireGaku = syoudaku_kakaku
            Else
                '�������}�X�^�ɊY���̑g�ݍ��킹�����������i�ݒ薳���́A���z����
                If .Syouhin1Record.SyouhinCd = Nothing OrElse .Syouhin1Record.SyouhinCd = "" Then
                    .Syouhin1Record.SiireGaku = Integer.MinValue
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' ���i�P���������s���A����N�����̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="uri_set">����N�����������ݒ莞:true</param>
    ''' <param name="intGamenHyoujiNo">��ʕ\��No�w�莞�͊Y���̓@�ʐ������R�[�h�̂ݏ������s�Ȃ�</param>
    ''' <remarks></remarks>
    Public Sub Syouhin1UriageSeikyuDateSet(ByVal sender As System.Object, ByRef jibanRec As JibanRecordBase, ByVal uri_set As Boolean, Optional ByVal intGamenHyoujiNo As Integer = Integer.MinValue)
        Dim tyousa_jissi_date As DateTime '�������{��
        Dim seikyuu_hakkou_date As DateTime '���������s��
        Dim uriage_date As DateTime '����N����

        seikyuu_hakkou_date = jibanRec.Syouhin1Record.SeikyuusyoHakDate
        uriage_date = jibanRec.Syouhin1Record.UriDate

        ' �@�ʐ������R�[�h�ɕK�v�����Z�b�g���A���������s���A����N�������擾����
        Dim teibetu_rec As New TeibetuSeikyuuRecord

        ' ��ʂ̏����Z�b�g
        teibetu_rec.Kbn = jibanRec.Kbn
        teibetu_rec.SeikyuuUmu = jibanRec.Syouhin1Record.SeikyuuUmu
        teibetu_rec.SeikyuusyoHakDate = seikyuu_hakkou_date
        teibetu_rec.UriDate = uriage_date
        teibetu_rec.SyouhinCd = jibanRec.Syouhin1Record.SyouhinCd

        ' �������{������t�^�ɂ���
        tyousa_jissi_date = jibanRec.TysJissiDate

        ' ���������s���A����N�������擾����
        Me.SubSeikyuuUriageDateSet(jibanRec.KameitenCd, tyousa_jissi_date, teibetu_rec, uri_set)

        ' ���ʂ��Z�b�g����
        '���������s��
        jibanRec.Syouhin1Record.SeikyuusyoHakDate = teibetu_rec.SeikyuusyoHakDate

        '����N����
        jibanRec.Syouhin1Record.UriDate = teibetu_rec.UriDate

        With jibanRec
            ' ���i�P�C�Q�̐��������s���A����N�����̘A��
            If .Syouhin1Record.SeikyuuUmu = 1 Then '���i1���R�[�h.�����L��=�L�̏ꍇ

                '���i2���R�[�h�̐���
                For intCnt As Integer = 1 To EarthConst.SYOUHIN2_COUNT
                    If intGamenHyoujiNo <> Integer.MinValue Then
                        If intGamenHyoujiNo <> intCnt Then
                            Continue For
                        End If
                    End If

                    '���i2���Z�b�g����
                    If .Syouhin2Records.ContainsKey(intCnt) Then

                        '���i2_*
                        With jibanRec.Syouhin2Records(intCnt)
                            If .SyouhinCd <> "" Then
                                '���������s��
                                .SeikyuusyoHakDate = IIf(.SeikyuusyoHakDate <> Date.MinValue, teibetu_rec.SeikyuusyoHakDate, .SeikyuusyoHakDate)
                                '����N����
                                .UriDate = IIf(.UriDate <> Date.MinValue, teibetu_rec.UriDate, .UriDate)
                            End If
                        End With
                    End If

                Next

            End If

        End With

    End Sub

    ''' <summary>
    ''' ���i�P���R�[�h�I�u�W�F�N�g�̐���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub CreateSyouhin1Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        ' ���i�R�[�h�P�̃��X�g
        Dim syouhinHash As New TeibetuSeikyuuRecord
        Dim teibetuRec As New TeibetuSeikyuuRecord

        syouhinHash = jibanRec.Syouhin1Record

        If syouhinHash Is Nothing Then
            syouhinHash = New TeibetuSeikyuuRecord
        End If

        '���i2���R�[�h�̐���
        If Not jibanRec Is Nothing _
                AndAlso Not syouhinHash Is Nothing Then
        Else
            teibetuRec = New TeibetuSeikyuuRecord

            '���i1_*
            With teibetuRec
                '�敪
                .Kbn = jibanRec.Kbn
                '�ۏ؏�NO
                .HosyousyoNo = jibanRec.HosyousyoNo
                '���ރR�[�h
                .BunruiCd = "" '�㑱�ɂăZ�b�g
                '��ʕ\��NO
                .GamenHyoujiNo = 1
                '���i�R�[�h
                .SyouhinCd = "" '�㑱�ɂăZ�b�g
                '������z(���������z)
                .UriGaku = 0              ' �������z
                '�d�����z(���������z)
                .SiireGaku = 0
                '�ŋ敪
                .ZeiKbn = 0
                '����Ŋz(���R�[�h�N���X�Ŏ����Z�o�̂��ߏȗ�)
                '���������s��
                .SeikyuusyoHakDate = DateTime.MinValue '�㑱�ɂăZ�b�g
                '����N����
                .UriDate = DateTime.MinValue
                '�`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
                .DenpyouUriDate = DateTime.MinValue
                '�����L��
                .SeikyuuUmu = 1
                '�m��敪
                .KakuteiKbn = Integer.MinValue
                '����v��FLG
                .UriKeijyouFlg = 0
                '����v���
                .UriKeijyouDate = DateTime.MinValue
                '���l
                .Bikou = String.Empty
                '�H���X�������z
                .KoumutenSeikyuuGaku = 0      ' �H���X�����z
                '���������z
                .HattyuusyoGaku = Integer.MinValue
                '�������m�F��
                .HattyuusyoKakuninDate = DateTime.MinValue
                '�ꊇ����FLG
                .IkkatuNyuukinFlg = Integer.MinValue
                '�������Ϗ��쐬��
                .TysMitsyoSakuseiDate = DateTime.MinValue
                '�������m��FLG
                .HattyuusyoKakuteiFlg = 0
                '�X�V���O�C�����[�U�[ID
                .UpdLoginUserId = jibanRec.UpdLoginUserId
                '***********************************
            End With
        End If

        '���i2���R�[�h���Z�b�g
        If jibanRec.Syouhin1Record Is Nothing Then
            jibanRec.Syouhin1Record = syouhinHash
        End If

    End Sub

#End Region

#Region "���i2/���i3"
    ''' <summary>
    ''' ���i�Q���R�[�h�I�u�W�F�N�g�̐���
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="blnSyouhin3">���i�R��ΏۂƂ��邩</param>
    ''' <remarks></remarks>
    Public Sub CreateSyouhin23Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase, Optional ByVal blnSyouhin3 As Boolean = False)
        ' ���i�R�[�h�Q�̃��X�g
        Dim syouhinHash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim teibetuRec As New TeibetuSeikyuuRecord

        Dim intMax As Integer = 0

        If blnSyouhin3 = False Then
            intMax = EarthConst.SYOUHIN2_COUNT
            syouhinHash = jibanRec.Syouhin2Records
        Else
            intMax = EarthConst.SYOUHIN3_COUNT
            syouhinHash = jibanRec.Syouhin3Records
        End If

        If syouhinHash Is Nothing Then
            syouhinHash = New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        End If

        '���i2���R�[�h�̐���
        For intCnt As Integer = 1 To intMax
            If Not jibanRec Is Nothing _
                AndAlso Not syouhinHash Is Nothing _
                    AndAlso syouhinHash.ContainsKey(intCnt) = True Then
            Else
                teibetuRec = New TeibetuSeikyuuRecord

                '���i2_*,���i3_*
                With teibetuRec
                    '�敪
                    .Kbn = jibanRec.Kbn
                    '�ۏ؏�NO
                    .HosyousyoNo = jibanRec.HosyousyoNo
                    '���ރR�[�h
                    .BunruiCd = "" '�㑱�ɂăZ�b�g
                    '��ʕ\��NO
                    .GamenHyoujiNo = intCnt
                    '���i�R�[�h
                    .SyouhinCd = "" '�㑱�ɂăZ�b�g
                    '������z(���������z)
                    .UriGaku = 0              ' �������z
                    '�d�����z(���������z)
                    .SiireGaku = 0
                    '�ŋ敪
                    .ZeiKbn = 0
                    '����Ŋz(���R�[�h�N���X�Ŏ����Z�o�̂��ߏȗ�)
                    '���������s��
                    .SeikyuusyoHakDate = DateTime.MinValue '�㑱�ɂăZ�b�g
                    '����N����
                    .UriDate = DateTime.MinValue
                    '�`�[����N����(���W�b�N�N���X�Ŏ����Z�b�g)
                    .DenpyouUriDate = DateTime.MinValue
                    '�����L��
                    .SeikyuuUmu = 1
                    '�m��敪
                    .KakuteiKbn = Integer.MinValue
                    '����v��FLG
                    .UriKeijyouFlg = 0
                    '����v���
                    .UriKeijyouDate = DateTime.MinValue
                    '���l
                    .Bikou = String.Empty
                    '�H���X�������z
                    .KoumutenSeikyuuGaku = 0      ' �H���X�����z
                    '���������z
                    .HattyuusyoGaku = Integer.MinValue
                    '�������m�F��
                    .HattyuusyoKakuninDate = DateTime.MinValue
                    '�ꊇ����FLG
                    .IkkatuNyuukinFlg = Integer.MinValue
                    '�������Ϗ��쐬��
                    .TysMitsyoSakuseiDate = DateTime.MinValue
                    '�������m��FLG
                    .HattyuusyoKakuteiFlg = 0
                    '�X�V���O�C�����[�U�[ID
                    .UpdLoginUserId = jibanRec.UpdLoginUserId
                    '***********************************
                End With

                syouhinHash.Add(intCnt, teibetuRec)

            End If
        Next

        If blnSyouhin3 = False Then
            '���i2���R�[�h���Z�b�g
            If jibanRec.Syouhin2Records Is Nothing Then
                jibanRec.Syouhin2Records = syouhinHash
            End If
        Else
            '���i3���R�[�h���Z�b�g
            If jibanRec.Syouhin3Records Is Nothing Then
                jibanRec.Syouhin3Records = syouhinHash
            End If
        End If

    End Sub

    ''' <summary>
    ''' ����X���i�Q�ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub TokuteitenSyouhin2Set(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        Dim kameiten_cd As String = String.Empty

        ' �����X�擾�ς݃`�F�b�N�i��������X�΍�j
        If jibanRec.KameitenCd Is Nothing OrElse jibanRec.KameitenCd = String.Empty Then
            ' �����X������(�������X����)�̏ꍇ�A�����𔲂���
            Exit Sub
        End If

        ' ���i�Q�ݒ�ς݃`�F�b�N
        With jibanRec
            If .Syouhin2Records(1).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(2).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(3).SyouhinCd <> String.Empty OrElse _
                .Syouhin2Records(4).SyouhinCd <> String.Empty Then
                ' ��ł����i�Q���ݒ肳��Ă���ꍇ�A�����𔲂���
                Exit Sub
            End If
        End With

        ' ����X���i�Q�̃R�[�h�擾����
        Dim syouhinCd2List As New List(Of String)
        If Me.GetTokuteitenSyouhin2(jibanRec.KameitenCd, syouhinCd2List) = True Then
            Dim lineCount As Integer = 1
            For Each syouhinCd2 As String In syouhinCd2List
                setSyouhinCd23(sender, "2_" & lineCount.ToString, syouhinCd2, jibanRec)
                lineCount += 1
                If lineCount > 4 Then
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' �������菤�i�Q�ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub TatouwariSet(ByVal sender As Object, ByRef jibanRec As JibanRecordBase)
        Dim syouhin_cd2 As String = ""

        With jibanRec

            ' ��������ݒ莞�̏��i�R�[�h�Q�擾����
            If Me.GetTatouwariSyouhinCd2(.Kbn, _
                                            .KameitenCd, _
                                            .DoujiIraiTousuu, _
                                            syouhin_cd2) = True Then

                ' ���i�R�[�h���擾�ł����ꍇ�A���ɓ��ꏤ�i�R�[�h�����݂��Ȃ�����
                ' ���i�R�[�h�Q�ɋ󂫂�����ꍇ�A�@�ʐ�����薾�ׂ��擾���ĉ�ʂɃZ�b�g����

                ' ����R�[�h�̃`�F�b�N
                If .Syouhin2Records(1).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(2).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(3).SyouhinCd = syouhin_cd2 Or _
                   .Syouhin2Records(4).SyouhinCd = syouhin_cd2 Then
                    ' ����R�[�h����̏ꍇ�͐ݒ肵�Ȃ�
                    Exit Sub
                End If

                ' �󂢂Ă��闓������ΐݒ肷��
                If .Syouhin2Records(1).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_1", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(2).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_2", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(3).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_3", syouhin_cd2, jibanRec)
                ElseIf .Syouhin2Records(4).SyouhinCd = String.Empty Then
                    setSyouhinCd23(sender, "2_4", syouhin_cd2, jibanRec)
                Else
                    ' �󂫂������ꍇ�͐ݒ肵�Ȃ�
                    Exit Sub
                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' ���i�Q/�R���R�[�h�I�u�W�F�N�g�̔j��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="jibanRec">�n�Ճ��R�[�h</param>
    ''' <param name="blnSyouhin3">���i�R��ΏۂƂ��邩</param>
    ''' <remarks>���R�[�h�I�u�W�F�N�g�̂����A���i�R�[�h�����ݒ�̂��͍̂폜����</remarks>
    Public Sub DeleteSyouhin23Rec(ByVal sender As Object, ByRef jibanRec As JibanRecordBase, Optional ByVal blnSyouhin3 As Boolean = False)
        With jibanRec
            If blnSyouhin3 = False Then
                '���i2���R�[�h
                For intCnt As Integer = EarthConst.SYOUHIN2_COUNT To 1 Step -1
                    '���i2_*
                    If Not .Syouhin2Records Is Nothing Then
                        '���i�R�[�h=������
                        If .Syouhin2Records.ContainsKey(intCnt) AndAlso .Syouhin2Records(intCnt).SyouhinCd = String.Empty Then
                            .Syouhin2Records.Remove(intCnt)
                        End If
                    End If
                Next
            Else
                '���i3���R�[�h
                For intCnt As Integer = EarthConst.SYOUHIN3_COUNT To 1 Step -1
                    '���i3_*
                    If Not .Syouhin3Records Is Nothing Then
                        '���i�R�[�h=������
                        If .Syouhin3Records.ContainsKey(intCnt) AndAlso .Syouhin3Records(intCnt).SyouhinCd = String.Empty Then
                            .Syouhin3Records.Remove(intCnt)
                        End If
                    End If
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' ���i�R�[�h ��ʐݒ�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="syouhin_type">�ݒ肵�������i�s�^�C�v</param>
    ''' <param name="syouhin_cd">���i�R�[�h</param>
    ''' <remarks></remarks>
    Public Function setSyouhinCd23(ByVal sender As Object, _
                               ByVal syouhin_type As String, _
                               ByVal syouhin_cd As String, _
                               ByRef jibanRec As JibanRecordBase _
                               ) As Boolean

        If syouhin_cd = String.Empty Then
            Return False
            Exit Function
        End If

        ' ���擾�p�̃p�����[�^�N���X
        Dim syouhin23_info As New Syouhin23InfoRecord

        ' ���i�̊�{�����擾
        Dim syouhin23_rec As Syouhin23Record = GetSyouhinInfo(syouhin_type.Split("_")(0), syouhin_cd, jibanRec.KameitenCd)

        ' �擾�ł��Ȃ��ꍇ�A�G���[
        If syouhin23_rec Is Nothing Then
            Return False
            Exit Function
        End If

        '���i2,3�̑ΏۃC���f�b�N�X
        Dim strSyouhin23Type As String = syouhin_type.Split("_")(0) '���i2or3
        Dim strSyouhin23Index As String = syouhin_type.Split("_")(1) '�C���f�b�N�X
        Dim intIndex As Integer = CInt(strSyouhin23Index)
        Dim TeibetuTmpRecords As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Dim TeibetuTmpRecord As New TeibetuSeikyuuRecord

        '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
        With jibanRec
            TeibetuTmpRecords = New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            Select Case strSyouhin23Type
                Case "1" '���i1
                    TeibetuTmpRecord = .Syouhin1Record
                Case "2"
                    TeibetuTmpRecords = .Syouhin2Records
                    TeibetuTmpRecord = .Syouhin2Records(intIndex)
                Case "3"
                    TeibetuTmpRecords = .Syouhin3Records
                    TeibetuTmpRecord = .Syouhin3Records(intIndex)
                Case Else
                    TeibetuTmpRecord = Nothing
            End Select

            '��ʏ�Ő����悪�w�肳��Ă���ꍇ�A���R�[�h�̐�������㏑��
            If Not TeibetuTmpRecord Is Nothing AndAlso TeibetuTmpRecord.SeikyuuSakiCd <> String.Empty Then
                '����������R�[�h�ɃZ�b�g
                syouhin23_rec.SeikyuuSakiCd = TeibetuTmpRecord.SeikyuuSakiCd
                syouhin23_rec.SeikyuuSakiBrc = TeibetuTmpRecord.SeikyuuSakiBrc
                syouhin23_rec.SeikyuuSakiKbn = TeibetuTmpRecord.SeikyuuSakiKbn
            End If
        End With

        ' ���i�R�[�h�y�щ�ʂ̏����Z�b�g
        syouhin23_info.Syouhin2Rec = syouhin23_rec                            ' ���i�̊�{���
        syouhin23_info.SeikyuuUmu = 1                                         ' �����L��
        syouhin23_info.HattyuusyoKakuteiFlg = 0                               ' �������m��t���O
        syouhin23_info.Seikyuusaki = syouhin23_rec.SeikyuuSakiType            ' ������^�C�v
        syouhin23_info.KeiretuCd = jibanRec.SyouhinKkk_KeiretuCd                              ' �n��R�[�h
        syouhin23_info.KeiretuFlg = Me.GetKeiretuFlg(jibanRec.SyouhinKkk_KeiretuCd)        ' �n��t���O

        ' �������R�[�h�̎擾(�m���Ɍ��ʂ��L��)
        Dim teibetu_seikyuu_rec As TeibetuSeikyuuRecord = getSyouhin23SeikyuuInfo(sender, syouhin23_info)
        If teibetu_seikyuu_rec Is Nothing Then
            Return False
            Exit Function
        End If

        Select Case syouhin23_rec.SoukoCd
            Case EarthConst.SOUKO_CD_SYOUHIN_2_110, EarthConst.SOUKO_CD_SYOUHIN_2_115 '���i2

                With jibanRec.Syouhin2Records(intIndex)
                    ' �R�[�h�A���̂��Z�b�g�i��������̏ꍇ�A�ݒ�K�{�ׁ̈j
                    '�q�ɃR�[�h(���ރR�[�h)
                    .BunruiCd = syouhin23_rec.SoukoCd
                    '��ʕ\��No
                    .GamenHyoujiNo = intIndex
                    '���i�R�[�h
                    .SyouhinCd = syouhin23_rec.SyouhinCd

                    ' ���i�����Z�b�g
                    '�H���X�������z
                    .KoumutenSeikyuuGaku = teibetu_seikyuu_rec.KoumutenSeikyuuGaku
                    '���������z
                    .UriGaku = teibetu_seikyuu_rec.UriGaku
                    '���������z
                    .SiireGaku = teibetu_seikyuu_rec.SiireGaku
                    '�ŋ敪
                    .ZeiKbn = teibetu_seikyuu_rec.ZeiKbn
                    '�ŗ�
                    .Zeiritu = syouhin23_rec.Zeiritu
                    '������Ŋz
                    .UriageSyouhiZeiGaku = Integer.MinValue
                    '�������m��FLG
                    .HattyuusyoKakuteiFlg = 0

                    ' ���i�Q�̏ꍇ�A���i�P�Ɠ��t�A��
                    If strSyouhin23Type = "2" Then
                        ' ����͖�����
                        '����N����
                        .UriDate = jibanRec.Syouhin1Record.UriDate

                        '�����L��
                        If jibanRec.Syouhin1Record.SeikyuuUmu = 1 Then '�L
                            ' ���i�P�C�Q�̐��������s���A����N�����̘A��
                            .SeikyuusyoHakDate = jibanRec.Syouhin1Record.SeikyuusyoHakDate
                        Else '��
                            Dim cBizLogic As New CommonBizLogic
                            Dim strSimeDate As String
                            '�������ߓ��̎擾
                            strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(.SeikyuuSakiCd _
                                                                                        , .SeikyuuSakiBrc _
                                                                                        , .SeikyuuSakiKbn _
                                                                                        , jibanRec.KameitenCd _
                                                                                        , .SyouhinCd)
                            ' ���i�P�̐����L�������̏ꍇ�̂ݎZ�o
                            .SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
                        End If
                    End If

                End With

            Case EarthConst.SOUKO_CD_SYOUHIN_3 '���i3

                With jibanRec.Syouhin3Records(intIndex)
                    ' �R�[�h�A���̂��Z�b�g�i��������̏ꍇ�A�ݒ�K�{�ׁ̈j
                    '�q�ɃR�[�h(���ރR�[�h)
                    .BunruiCd = syouhin23_rec.SoukoCd
                    '��ʕ\��No
                    .GamenHyoujiNo = intIndex
                    '���i�R�[�h
                    .SyouhinCd = syouhin23_rec.SyouhinCd

                    ' ���i�����Z�b�g
                    '�H���X�������z
                    .KoumutenSeikyuuGaku = teibetu_seikyuu_rec.KoumutenSeikyuuGaku
                    '���������z
                    .UriGaku = teibetu_seikyuu_rec.UriGaku
                    '���������z
                    .SiireGaku = teibetu_seikyuu_rec.SiireGaku
                    '�ŋ敪
                    .ZeiKbn = teibetu_seikyuu_rec.ZeiKbn
                    '�ŗ�
                    .Zeiritu = syouhin23_rec.Zeiritu
                    '������Ŋz
                    .UriageSyouhiZeiGaku = Integer.MinValue
                    '�������m��FLG
                    .HattyuusyoKakuteiFlg = 0

                    ' ���i�R�͊m��敪���Z�b�g
                    If strSyouhin23Type = "3" Then
                        .KakuteiKbn = IIf(.KakuteiKbn = Integer.MinValue, "0", teibetu_seikyuu_rec.KakuteiKbn.ToString())
                        ' �m��󋵂ɂ��A���������s���E����N�����ݒ�
                        Me.SubChangeKakutei(jibanRec.KameitenCd, teibetu_seikyuu_rec)
                        cbLogic.SetDisplayString(.SeikyuusyoHakDate, teibetu_seikyuu_rec.SeikyuusyoHakDate)
                        cbLogic.SetDisplayString(.UriDate, teibetu_seikyuu_rec.UriDate)
                        cbLogic.SetDisplayString(.UriKeijyouFlg, teibetu_seikyuu_rec.UriKeijyouFlg)
                    End If
                End With

            Case Else
                Return False
        End Select

        Return True
    End Function

    ''' <summary>
    ''' ���i�Q�A�R��ʕ\���p�̏��i�����擾���܂�
    ''' </summary>
    ''' <param name="syouhin_type">���i�Qor�R</param>
    ''' <param name="syouhin_cd">���i�R�[�h</param>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Function getSyouhinInfo(ByVal syouhin_type As String, _
                                    ByVal syouhin_cd As String, _
                                    ByVal strKamentenCd As String _
                                    ) As Syouhin23Record

        Dim syouhin23_rec As Syouhin23Record = Nothing
        Dim count As Integer = 0

        ' ���i�����擾����i�R�[�h�w��Ȃ̂łP���̂ݎ擾�����j
        Dim list As List(Of Syouhin23Record) = Me.GetSyouhin23(syouhin_cd, _
                                                                  "", _
                                                                  IIf(syouhin_type = "2", EarthEnum.EnumSyouhinKubun.Syouhin2_110, EarthEnum.EnumSyouhinKubun.Syouhin3), _
                                                                  count, _
                                                                  Integer.MinValue, _
                                                                  strKamentenCd)

        ' �擾�ł��Ȃ��ꍇ
        If list.Count < 1 Then
            Return syouhin23_rec
        End If

        ' �擾�ł����ꍇ�̂݃Z�b�g
        syouhin23_rec = list(0)

        Return syouhin23_rec

    End Function

    ''' <summary>
    ''' ���i�Q�A�R��ʕ\���p�̓@�ʐ����f�[�^���擾���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="syouhin23_info">���i�Q�C�R���擾�p�̃p�����[�^�N���X</param>
    ''' <returns>�@�ʐ������R�[�h</returns>
    ''' <remarks></remarks>
    Public Function getSyouhin23SeikyuuInfo(ByVal sender As Object, _
                                             ByVal syouhin23_info As Syouhin23InfoRecord _
                                             ) As TeibetuSeikyuuRecord

        Dim teibetu_rec As TeibetuSeikyuuRecord = Nothing
        Dim intSetSts As Integer = 0

        ' �����f�[�^�̎擾
        teibetu_rec = Me.GetSyouhin23SeikyuuData(sender, syouhin23_info, 1, intSetSts)

        If intSetSts <> 0 Then
            teibetu_rec = Nothing
        End If

        Return teibetu_rec

    End Function

#End Region

#End Region

#Region "���i�R�m��敪�ύX����"

    ''' <summary>
    ''' ���i�R�[�h�R�̊m��敪�ύX���̏������s���܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="rec">�敪�ύX�ΏۂƂȂ鏤�i�R���R�[�h�̓@�ʐ������R�[�h</param>
    ''' <remarks>����Ɏg�p���鍀�ځF�m��敪</remarks>
    Public Sub SubChangeKakutei(ByVal kameitenCd As String, _
                                ByRef rec As TeibetuSeikyuuRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SubChangeKakutei", _
                                            kameitenCd, _
                                            rec)
        Dim cBizLogic As New CommonBizLogic

        ' �m��敪���ݒ莞�͖����F0�Ƃ���
        If rec.KakuteiKbn = Integer.MinValue Then
            rec.KakuteiKbn = 0
        End If

        ' �����̏ꍇ�A���������s���A����N�������N���A���ďI��
        If rec.KakuteiKbn = 0 Then
            rec.SeikyuusyoHakDate = Date.MinValue
            rec.UriDate = Date.MinValue
            Exit Sub
        End If

        ' �����L�����w�莞�A�����I��
        If rec.SeikyuuUmu = Integer.MinValue Then
            Exit Sub
        End If

        ' �����L���F����̏ꍇ�A���������s����ݒ肷��
        If rec.SeikyuuUmu = 1 Then
            ' ���������s���͖��w��̏ꍇ�̂ݐݒ肷��
            If rec.SeikyuusyoHakDate = Date.MinValue Then
                Dim strSimeDate As String
                '���������ߓ����擾
                strSimeDate = cBizLogic.GetSeikyuuSimeDateFromKameiten(rec.SeikyuuSakiCd _
                                                                        , rec.SeikyuuSakiBrc _
                                                                        , rec.SeikyuuSakiKbn _
                                                                        , kameitenCd _
                                                                        , rec.SyouhinCd)
                ' ���������s�����Z�b�g
                rec.SeikyuusyoHakDate = cBizLogic.CalcSeikyuusyoHakkouDate(strSimeDate)
            End If
        End If

        ' ����N�����F�����͂̏ꍇ�ݒ肷��
        If rec.UriDate = Date.MinValue Then
            rec.UriDate = Date.Now
        End If

    End Sub

#End Region

#Region "����X���i�R�[�h2�擾"
    ''' <summary>
    ''' ����̉����X�Ɏ����ݒ肳��鏤�i�R�[�h�Q���擾���܂�
    ''' �擾�����𖞂��Ȃ��ꍇ�A���i�R�[�h���擾�ł��Ȃ��ꍇ�AFalse��Ԃ��܂�
    ''' </summary>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="syouhinCd2List">���i�R�[�h�Q���X�g</param>
    ''' <returns></returns>
    ''' <remarks>�敪�FA�̏ꍇ�͎����ݒ肳��܂���</remarks>
    Public Function GetTokuteitenSyouhin2(ByVal kameitenCd As String, _
                                          ByRef syouhinCd2List As List(Of String)) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTokuteitenSyouhin2", _
                                            kameitenCd, _
                                            syouhinCd2List)

        '****************************************************
        ' �����̃`�F�b�N�i��ł�NG�̏ꍇFalse�ŏ����I���j
        '****************************************************
        ' �����X�R�[�h�̃`�F�b�N
        If kameitenCd.Trim() = "" Then
            ' �����X�R�[�h��""�̏ꍇ�����I��
            Return False
        End If

        Dim data_access As New SyouhinDataAccess
        Dim syouhinDataTable As New SyouhinDataSet.SyouhinTableDataTable

        ' ����X���i�Q���擾����
        syouhinDataTable = data_access.GetTokuteitenSyouhin2(kameitenCd)

        If syouhinDataTable.Rows.Count <= 0 Then
            Return False
        Else
            For Each syouhinRow As SyouhinDataSet.SyouhinTableRow In syouhinDataTable.Rows
                syouhinCd2List.Add(syouhinRow.syouhin_cd)
            Next
        End If

        Return True

    End Function
#End Region

#Region "���������i�R�[�h2�擾"
    ''' <summary>
    ''' �����˗������ɂ�鑽�������i�R�[�h���擾���܂�
    ''' �擾�����𖞂��Ȃ��ꍇ�A���i�R�[�h���擾�ł��Ȃ��ꍇ�AFalse��Ԃ��܂�
    ''' </summary>
    ''' <param name="kubun">�敪</param>
    ''' <param name="kameitenCd">�����X�R�[�h</param>
    ''' <param name="doujiIraiTousuu">�����˗������i4�ȏ�j</param>
    ''' <param name="syouhinCd2"></param>
    ''' <returns></returns>
    ''' <remarks>�敪�FA�̏ꍇ�͎����ݒ肳��܂���</remarks>
    Public Function GetTatouwariSyouhinCd2(ByVal kubun As String, _
                                           ByVal kameitenCd As String, _
                                           ByVal doujiIraiTousuu As Integer, _
                                           ByRef syouhinCd2 As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatouwariSyouhinCd2", _
                                            kubun, _
                                            kameitenCd, _
                                            doujiIraiTousuu, _
                                            syouhinCd2)

        '****************************************************
        ' �����̃`�F�b�N�i��ł�NG�̏ꍇFalse�ŏ����I���j
        '****************************************************
        ' �敪�̃`�F�b�N
        If kubun.Trim() = "A" Then
            ' �敪��"A"�̏ꍇ�����I��
            Return False
        End If

        ' �����X�R�[�h�̃`�F�b�N
        If kameitenCd.Trim() = "" Then
            ' �����X�R�[�h��""�̏ꍇ�����I��
            Return False
        End If

        ' �����˗������̃`�F�b�N
        If doujiIraiTousuu < 4 Then
            ' �����˗�������4�����̏ꍇ�����I��
            Return False
        End If

        ' ���敪
        Dim touKbn As Integer

        '�����˗����������Ƃɓ��敪(KEY)��ݒ�
        If doujiIraiTousuu >= 4 And doujiIraiTousuu <= 9 Then
            touKbn = 1
        ElseIf doujiIraiTousuu >= 10 And doujiIraiTousuu <= 19 Then
            touKbn = 2
        Else
            touKbn = 3
        End If

        Dim data_access As New SyouhinDataAccess

        ' �������̏��i�R�[�h�Q���擾����
        syouhinCd2 = data_access.GetTatouwariSyouhinCd(kameitenCd, touKbn)

        ' �擾�ł��Ȃ��ꍇ�A������ύX���Č�������
        If syouhinCd2 = "" Then
            syouhinCd2 = data_access.GetTatouwariSyouhinCd(EarthConst.SH_CD_TATOUWARI, touKbn)
        End If

        ' �擾�ł��Ȃ��ꍇ�A���͎擾�����R�[�h��"00000"�̏ꍇ�AFalse�ŏ����I��
        If syouhinCd2 = "" Or syouhinCd2 = EarthConst.SH_CD_TATOUWARI_ER Then
            Return False
        End If

        Return True

    End Function
#End Region

#Region "���m�点���̎擾"
    ''' <summary>
    ''' ���m�点�f�[�^���擾���܂�
    ''' </summary>
    ''' <returns>���m�点�f�[�^�̃��R�[�h���X�g</returns>
    ''' <remarks></remarks>
    Public Function GetOsiraseRecords() As List(Of OsiraseRecord)

        ' �߂�l�ƂȂ郊�X�g
        Dim returnRec As New List(Of OsiraseRecord)

        ' �d���f�[�^�擾�N���X
        Dim dataAccess As New OsiraseDataAccess

        ' �l���擾�ł����ꍇ�A�߂�l�ɐݒ肷��
        For Each row As OsiraseDataSet.OsiraseTableRow In dataAccess.GetOsiraseData()
            Dim osiraseRec As New OsiraseRecord

            osiraseRec.Nengappi = row.nengappi
            osiraseRec.NyuuryokuBusyo = row.nyuuryoku_busyo
            osiraseRec.NyuuryokuMei = row.nyuuryoku_mei
            osiraseRec.HyoujiNaiyou = row.hyouji_naiyou
            osiraseRec.LinkSaki = row.link_saki
            ' ���X�g�ɃZ�b�g
            returnRec.Add(osiraseRec)

        Next

        Return returnRec

    End Function
#End Region

#Region "�n�Ճf�[�^�擾"
    ''' <summary>
    ''' �n�Ճf�[�^���擾���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="isNotDataHaki">�f�[�^�j����ʔ��f�t���O</param>
    ''' <returns>�n�Ճ��R�[�h</returns>
    ''' <remarks>�n�Ճ��R�[�h�ɂ͕R�t���@�ʐ����f�[�^���i�[����Ă���܂�</remarks>
    Public Function GetJibanData(ByVal kbn, _
                                 ByVal hosyousyoNo, _
                        Optional ByVal isNotDataHaki = False) As JibanRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            isNotDataHaki)

        ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �n�Ճf�[�^�̎擾
        Dim jibanList As List(Of JibanRecord) = DataMappingHelper.Instance.getMapArray(Of JibanRecord)(GetType(JibanRecord), _
        dataAccess.GetJibanData(kbn, hosyousyoNo, isNotDataHaki))

        ' �n�Ճf�[�^�ێ��p�̃��R�[�h�N���X
        Dim jibanRec As New JibanRecord

        ' �f�[�^���擾�ł����ꍇ�A���R�[�h�N���X�ɐݒ�
        If jibanList.Count > 0 Then
            jibanRec = jibanList(0)

            '*****************************************************************
            ' �@�ʐ����f�[�^���擾����
            '*****************************************************************
            Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
            dataAccess.GetTeibetuSeikyuuData(kbn, hosyousyoNo))

            ' ���i�R�[�h�Q�̃��X�g
            Dim syouhin2Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' ���i�R�[�h�R�̃��X�g
            Dim syouhin3Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' ���i�R�[�h�S�̃��X�g
            Dim syouhin4Hash As New Dictionary(Of Integer, TeibetuSeikyuuRecord)
            ' ���i�ȊO�̓@�ʐ������X�g
            Dim otherList As New List(Of TeibetuSeikyuuRecord)
            ' �擾���R�[�h���ݒ���s��
            For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList

                ' ���ނɂ��i�[�ꏊ��ύX
                Select Case rec.BunruiCd
                    Case EarthConst.SOUKO_CD_SYOUHIN_1
                        ' ���i�R�[�h�P�͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.Syouhin1Record = rec
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_110
                        ' ���i�R�[�h�Q(110)�͕������Ȃ̂�Dictionary�ɃZ�b�g
                        syouhin2Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_2_115
                        '' ���z�̓}�C�i�X�ɂ���i��Βl*-1�j
                        'rec.KoumutenSeikyuuGaku = Math.Abs(rec.KoumutenSeikyuuGaku) * -1
                        'rec.UriGaku = Math.Abs(rec.UriGaku) * -1
                        'rec.SiireGaku = Math.Abs(rec.SiireGaku) * -1
                        ' ���i�R�[�h�Q(115)�͕������Ȃ̂�Dictionary�ɃZ�b�g
                        syouhin2Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_3
                        ' ���i�R�[�h�R�͕������Ȃ̂�Dictionary�ɃZ�b�g
                        syouhin3Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_SYOUHIN_4
                        ' ���i�R�[�h�S�͕������Ȃ̂�Dictionary�ɃZ�b�g
                        syouhin4Hash.Add(rec.GamenHyoujiNo, rec)
                    Case EarthConst.SOUKO_CD_KAIRYOU_KOUJI
                        ' ���ǍH���͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.KairyouKoujiRecord = rec
                    Case EarthConst.SOUKO_CD_TUIKA_KOUJI
                        ' �ǉ��H���͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.TuikaKoujiRecord = rec
                    Case EarthConst.SOUKO_CD_TYOUSA_HOUKOKUSYO
                        ' �����񍐏��͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.TyousaHoukokusyoRecord = rec
                    Case EarthConst.SOUKO_CD_KOUJI_HOUKOKUSYO
                        ' �H���񍐏��͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.KoujiHoukokusyoRecord = rec
                    Case EarthConst.SOUKO_CD_HOSYOUSYO
                        ' �ۏ؏��͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.HosyousyoRecord = rec
                    Case EarthConst.SOUKO_CD_KAIYAKU_HARAIMODOSI
                        ' ���z�̓}�C�i�X�ɂ���i��Βl*-1�j
                        rec.KoumutenSeikyuuGaku = Math.Abs(rec.KoumutenSeikyuuGaku) * -1
                        rec.UriGaku = Math.Abs(rec.UriGaku) * -1
                        rec.SiireGaku = Math.Abs(rec.SiireGaku) * -1
                        ' ��񕥖߂͂P���Ȃ̂Ń��R�[�h���Z�b�g
                        jibanRec.KaiyakuHaraimodosiRecord = rec
                    Case Else
                        ' ��L�ȊO�͂��̑��p���X�g�ɃZ�b�g
                        otherList.Add(rec)
                End Select

            Next

            ' ���i�R�[�h�Q�̃��X�g�Ƀf�[�^�����݂���ꍇ�A�n�Ճ��R�[�h�ɃZ�b�g
            If syouhin2Hash.Count > 0 Then
                jibanRec.Syouhin2Records = syouhin2Hash
            End If

            ' ���i�R�[�h�R�̃��X�g�Ƀf�[�^�����݂���ꍇ�A�n�Ճ��R�[�h�ɃZ�b�g
            If syouhin3Hash.Count > 0 Then
                jibanRec.Syouhin3Records = syouhin3Hash
            End If

            ' ���i�R�[�h�S�̃��X�g�Ƀf�[�^�����݂���ꍇ�A�n�Ճ��R�[�h�ɃZ�b�g
            If syouhin4Hash.Count > 0 Then
                jibanRec.Syouhin4Records = syouhin4Hash
            End If

            ' ���i�ȊO�̓@�ʐ������X�g�Ƀf�[�^�����݂���ꍇ�A�n�Ճ��R�[�h�ɃZ�b�g
            If otherList.Count > 0 Then
                jibanRec.OtherTeibetuSeikyuuRecords = otherList
            End If


            '*****************************************************************
            ' �@�ʓ����f�[�^���擾����(���ރR�[�h��Key�Ƃ���Dictionary)
            '*****************************************************************
            Dim listTeibetuNyuukin As List(Of TeibetuNyuukinRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord), _
            dataAccess.GetTeibetuNyuukinData(kbn, hosyousyoNo))

            ' �@�ʓ������X�g
            Dim dicTeibetuNyuukin As New Dictionary(Of String, TeibetuNyuukinRecord)

            ' �擾���R�[�h���ݒ���s��
            For Each rec As TeibetuNyuukinRecord In listTeibetuNyuukin
                dicTeibetuNyuukin.Add(rec.BunruiCd, rec)
            Next

            ' �@�ʓ������X�g�Ƀf�[�^�����݂���ꍇ�A�n�Ճ��R�[�h�ɃZ�b�g
            If dicTeibetuNyuukin.Count > 0 Then
                jibanRec.TeibetuNyuukinRecords = dicTeibetuNyuukin
            End If

            '*****************************************************************
            ' �@�ʓ����f�[�^���擾����
            '*****************************************************************
            Dim listTeibetuNyuukinMeisai As List(Of TeibetuNyuukinRecord) = _
            DataMappingHelper.Instance.getMapArray(Of TeibetuNyuukinRecord)(GetType(TeibetuNyuukinRecord), _
            dataAccess.GetTeibetuNyuukinDataKey(kbn, hosyousyoNo))
            Dim listTeibetuNyuukinUpdateMeisai As New List(Of TeibetuNyuukinUpdateRecord)
            Dim updRec As TeibetuNyuukinUpdateRecord

            For Each rec As TeibetuNyuukinRecord In listTeibetuNyuukinMeisai
                updRec = New TeibetuNyuukinUpdateRecord
                updRec.TeibetuNyuukinrecord = rec
                updRec.BunruiCd = rec.BunruiCd
                updRec.GamenHyoujiNo = rec.GamenHyoujiNo
                listTeibetuNyuukinUpdateMeisai.Add(updRec)
            Next
            If listTeibetuNyuukinUpdateMeisai.Count > 0 Then
                jibanRec.TeibetuNyuukinLists = listTeibetuNyuukinUpdateMeisai
            End If

        End If

        Return jibanRec

    End Function

    ''' <summary>
    ''' ������ʗp�n�Ճf�[�^���擾���܂�
    ''' </summary>
    ''' <param name="keyRec">�n��Key���R�[�h</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>�n�Ռ����p���R�[�h��List(Of JibanSearchRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchData(ByVal sender As Object, _
                                       ByVal keyRec As JibanKeyRecord, _
                                       ByVal startRow As Integer, _
                                       ByVal endRow As Integer, _
                                       ByRef allCount As Integer) As List(Of JibanSearchRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanSearchData", _
                                            keyRec, _
                                            startRow, _
                                            endRow, _
                                            allCount)

        ' �������������N���X
        Dim keyRecAcc As New JibanSearchKeyRecord
        ' �������s�N���X
        Dim dataAccess As New JibanDataAccess
        ' �擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of JibanSearchRecord)

        Try
            ' �������������N���X�̃v���p�e�B�ƈ����̒n��Key���R�[�h�f�[�^���}�b�s���O
            RecordMappingHelper.Instance.CopyRecordData(keyRec, keyRecAcc)

            ' �����������s
            Dim table As DataTable = dataAccess.GetJibanSearchData(keyRecAcc)

            ' ���������Z�b�g
            allCount = table.Rows.Count

            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            list = DataMappingHelper.Instance.getMapArray(Of JibanSearchRecord)(GetType(JibanSearchRecord), table, startRow, endRow)

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return list

    End Function

#Region "�i���ۏ؏��󋵌�����ʗp���R�[�h�擾"

    ''' <summary>
    ''' �n�Ճf�[�^���A���������ɍ��v���郌�R�[�h���擾����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyRec">�i��Key���R�[�h</param>
    '''  <param name="startRow">�J�n�s</param>
    '''  <param name="endRow">�ŏI�s</param>
    '''  <param name="allCount">�S����</param>
    ''' <returns>�i�������p���R�[�h��List(Of HinsituHosyousyoJyoukyouRecord)</returns>
    ''' <remarks></remarks>
    Public Function getJibanSearchHinsituRecord(ByVal sender As Object, _
                                                ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                ByVal startRow As Integer, _
                                                ByVal endRow As Integer, _
                                                ByRef allCount As Integer) As List(Of HinsituHosyousyoJyoukyouSearchRecord)
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJibanSearchHinsituRecord", _
                                                    keyRec, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New JibanDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New List(Of HinsituHosyousyoJyoukyouSearchRecord)

        Try

            ' �����������s
            dTblResult = clsDataAcc.GetJibanDataHinsitu(keyRec)

            ' ���������Z�b�g
            allCount = dTblResult.Rows.Count

            If dTblResult.Rows.Count > 0 Then
                ' �������ʂ��i�[�p���X�g�ɃZ�b�g
                recResult = DataMappingHelper.Instance.getMapArray(Of HinsituHosyousyoJyoukyouSearchRecord)(GetType(HinsituHosyousyoJyoukyouSearchRecord), dTblResult, startRow, endRow)
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return recResult

    End Function

    ''' <summary>
    ''' �n�Ճf�[�^���ACSV�o�͗p�f�[�^���擾����
    ''' </summary>
    ''' <param name="keyRec">Key���R�[�h</param>
    ''' <param name="allCount">�S����</param>
    ''' <returns>CSV�o�͗p�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchHinsituRecordCsv(ByVal sender As Object, _
                                       ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                       ByRef allCount As Integer) As DataTable
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetJibanSearchHinsituRecordCsv", _
                                            keyRec, _
                                            allCount)

        '�������s�N���X
        Dim dataAccess As New JibanDataAccess
        '�擾�f�[�^�i�[�p���X�g
        Dim list As New List(Of HinsituHosyousyoJyoukyouRecord)

        Dim dtRet As New DataTable

        Try
            '���������̎��s
            dtRet = dataAccess.GetJibanDataHinsituCsv(keyRec)

            ' ���������Z�b�g
            allCount = dtRet.Rows.Count

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return dtRet
    End Function

    ''' <summary>
    ''' �n�Ճf�[�^���A���������ɍ��v���郌�R�[�h���擾����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyRec">�i��Key���R�[�h</param>
    ''' <param name="startRow">�J�n�s</param>
    ''' <param name="endRow">�ŏI�s</param>
    '''  <param name="allCount">�S����</param>
    ''' <returns>�i�������p���R�[�h��List(Of HinsituHosyousyoJyoukyouRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetJibanSearchIkkatuHinsituRecord(ByVal sender As Object, _
                                                ByVal keyRec As HinsituHosyousyoJyoukyouRecord, _
                                                ByVal startRow As Integer, _
                                                ByVal endRow As Integer, _
                                                ByRef allCount As Integer) As HinsituHosyousyoJyoukyouSearchRecord
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getJibanSearchHinsituRecord", _
                                                    keyRec, _
                                                    startRow, _
                                                    endRow, _
                                                    allCount)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New JibanDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As HinsituHosyousyoJyoukyouSearchRecord = Nothing

        Try
            ' �����������s
            dTblResult = clsDataAcc.GetJibanDataHinsitu(keyRec)

            ' ���������Z�b�g
            allCount = dTblResult.Rows.Count

            '�Z�b�g��f�[�^�e�[�u���̃J�������ꗗ�̃n�b�V����ݒ�
            Dim hashColumnNames As Hashtable = DataMappingHelper.Instance.getColumnNamesHashtable(dTblResult)

            For Each row As DataRow In dTblResult.Rows

                If dTblResult.Rows.Count > 0 Then
                    ' �������ʂ��i�[�p���X�g�ɃZ�b�g
                    recResult = DataMappingHelper.Instance.propertyMap((GetType(HinsituHosyousyoJyoukyouSearchRecord)), row, hashColumnNames)
                End If

            Next

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, Messages.MSG086E, 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            ' �������J�E���g��-1���Z�b�g
            allCount = -1
        End Try

        Return recResult

    End Function

#End Region
#End Region

#Region "�n�Ճf�[�^�ǉ�"
    ''' <summary>
    ''' �n�Ճf�[�^��ǉ����܂��B
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="intRentouBukkenSuu">�A��������</param>
    ''' <returns>True:�o�^���� False:�o�^���s</returns>
    ''' <remarks></remarks>
    Public Function InsertJibanData(ByRef sender As Object, _
                                    ByRef kbn As String, _
                                    ByRef hosyousyoNo As String, _
                                    ByRef strLoginUserId As String, _
                                    ByRef intRentouBukkenSuu As Integer, _
                                    ByVal sinkiTourokuMotoKbnType As EarthEnum.EnumSinkiTourokuMotoKbnType _
                                    ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".InsertJibanData", _
                                            sender, _
                                            kbn, _
                                            hosyousyoNo, _
                                            strLoginUserId, _
                                            intRentouBukkenSuu, _
                                            sinkiTourokuMotoKbnType _
                                            )


        Dim intTmpBangou As Integer = 0 '�ԍ�(��Ɨp)
        Dim intCnt As Integer '�J�E���^
        Dim strTmpBangou As String = "" '�ԍ�(��Ɨp)

        If intRentouBukkenSuu <= 0 Then '�A��������
            intRentouBukkenSuu = 1
        End If

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
                Dim dataAccess As JibanDataAccess = New JibanDataAccess

                Select Case sinkiTourokuMotoKbnType
                    Case EarthEnum.EnumSinkiTourokuMotoKbnType.ReportJHS_FC
                        'FC�\���̏ꍇ�A���ɍ̔ԍ�
                    Case Else
                        If kbn <> "" Then
                            ' �ۏ؏�NO���̔Ԏ擾����
                            hosyousyoNo = GetNewHosyousyoNo(sender, kbn, intRentouBukkenSuu, strLoginUserId)
                        End If
                End Select

                ' �󔒂̏ꍇ�A�ۏ؏�NO�N�������o�^�i�n�ՃV�X�e���œo�^����K�v����j
                If hosyousyoNo = "" Then
                    mLogic.AlertMessage(sender, Messages.MSG002E)
                    Return False
                End If

                '�ԍ�(�ۏ؏�NO)����Ɨp�ɃR�s�[
                strTmpBangou = hosyousyoNo

                '�A�������������[�v
                For intCnt = 0 To intRentouBukkenSuu - 1

                    '�n�Ճf�[�^�ɓo�^
                    If dataAccess.InsertJibanData(kbn, strTmpBangou, strLoginUserId, sinkiTourokuMotoKbnType) <= 0 Then
                        '�o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�o�^�j
                    Dim renkeiJibanRec As New JibanRenkeiRecord
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = strTmpBangou
                    renkeiJibanRec.RenkeiSijiCd = 1
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = strLoginUserId

                    If dataAccess.InsertJibanRenkeiData(renkeiJibanRec) <> 1 Then
                        ' �o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                    strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

                Next

                ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = 2627 Then  '�G���[�L���b�`�F��L�[�̏d��
                mLogic.AlertMessage(sender, String.Format(Messages.MSG110E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function
#End Region

#Region "�n�Ճf�[�^�X�V"
    ''' <summary>
    ''' �n�Ճf�[�^���X�V���܂��B�֘A����@�ʐ����f�[�^�̍X�V���s���܂�
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="jibanRec">�X�V�Ώۂ̒n�Ճ��R�[�h</param>
    ''' <param name="jibanRecAfterExe">�X�V�Ώۂ̒n�Ճ��R�[�h</param>
    ''' <param name="brRec">�����������R�[�h</param>
    ''' <param name="listRec">���ʑΉ����R�[�h�S</param>
    ''' <returns>True:�X�V���� False:�G���[�ɂ��X�V���s</returns>
    ''' <remarks></remarks>
    Public Function UpdateJibanData(ByVal sender As Object, _
                                    ByVal jibanRec As JibanRecord, _
                                    ByRef jibanRecAfterExe As JibanRecord, _
                                    ByVal brRec As BukkenRirekiRecord, _
                                    ByVal listRec As List(Of TokubetuTaiouRecordBase) _
                                    ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".UpdateJibanData", _
                                            sender, _
                                            jibanRec, _
                                            jibanRecAfterExe, _
                                            brRec, _
                                            listRec _
                                            )

        ' Update�ɕK�v��SQL����������������N���X
        Dim upadteMake As New UpdateStringHelper
        ' �r������pSQL��
        Dim sqlString As String = ""
        ' Update��
        Dim updateString As String = ""
        ' �r���`�F�b�N�p�p�����[�^�̏����i�[����List(Of ParamRecord)
        Dim haitaList As New List(Of ParamRecord)
        ' �X�V�p�p�����[�^�̏����i�[����List(Of ParamRecord)
        Dim list As New List(Of ParamRecord)

        ' �r���`�F�b�N�p���R�[�h�쐬
        Dim jibanHaitaRec As New JibanHaitaRecord

        ' �n�Ճf�[�^�p�f�[�^�A�N�Z�X�N���X
        Dim dataAccess As JibanDataAccess = New JibanDataAccess

        ' �V�K�o�^�A�X�V�̔���i�ۏ؏�NO�̔Ԏ��̐V�K�o�^�������j
        Dim isNew As Boolean
        isNew = dataAccess.ChkJibanNew(jibanRec.Kbn, jibanRec.HosyousyoNo)

        ' �A�g�e�[�u���f�[�^�o�^�p�f�[�^�̊i�[�p
        Dim renkeiJibanRec As New JibanRenkeiRecord
        Dim renkeiTeibetuList As New List(Of TeibetuSeikyuuRenkeiRecord)

        ' �X�V���t�擾
        Dim updDateTime As DateTime = DateTime.Now

        ' ReportJHS�A�W�Ώۃt���O(�f�t�H���g�FFalse)
        Dim flgEditReportIf As Boolean

        ' �A���������`�F�b�N
        If jibanRec.RentouBukkenSuu = Nothing OrElse jibanRec.RentouBukkenSuu <= 0 Then
            jibanRec.RentouBukkenSuu = 1
        End If
        ' ���������`�F�b�N
        If jibanRec.SyoriKensuu = Nothing OrElse jibanRec.SyoriKensuu <= 0 Then
            jibanRec.SyoriKensuu = 0
        End If

        ' �ԍ�(�ۏ؏�NO) ��Ɨp
        Dim intTmpBangou As Integer = CInt(jibanRec.HosyousyoNo) + jibanRec.SyoriKensuu '�ԍ�+��������
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

        Dim strRetBangou As String = jibanRec.HosyousyoNo '��ʍĕ`��p

        ' �敪�A�ۏ؏�NO
        Dim kbn As String = jibanRec.Kbn
        Dim hosyousyoNo As String = jibanRec.HosyousyoNo

        ' �o�R�ޔ�p
        Dim intInitKeiyu As Integer = jibanRec.Keiyu

        Dim intCnt As Integer  '�����J�E���^
        Dim intMax As Integer = 20 '�����ő吔

        Dim pUpdDateTime As DateTime
        '�X�V�����擾�i�V�X�e�������j
        pUpdDateTime = DateTime.Now

        Dim htTtUpdFlg As New Dictionary(Of Integer, Integer)
        Dim htTtDateTime As New Dictionary(Of Integer, DateTime)

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                '20��������
                For intCnt = 1 To intMax

                    '*************************
                    ' �A�������Ή�
                    '*************************
                    '�������� >= �A�������� �̏ꍇ�A�S�����I��
                    If jibanRec.SyoriKensuu >= jibanRec.RentouBukkenSuu Then
                        jibanRec.SyoriKensuu = jibanRec.RentouBukkenSuu '�������� = �A��������
                        Exit For
                    End If

                    '�X�V�Ώۂ̔ԍ����w��
                    jibanRec.HosyousyoNo = strTmpBangou '�n�Ճe�[�u��

                    ' �n�Ճf�[�^���敪�A�ۏ؏�NO���擾�i�󃌃R�[�h�m�F�p�j
                    kbn = jibanRec.Kbn
                    hosyousyoNo = jibanRec.HosyousyoNo

                    '����������R�[�h�̎����ݒ�
                    If jibanRec.BukkenNayoseCdFlg Then
                        jibanRec.BukkenNayoseCd = kbn & hosyousyoNo '������NO���Z�b�g
                    End If

                    If jibanRec.SyoriKensuu <= 0 Then
                        If Not listRec Is Nothing Then
                            For intTokuCnt As Integer = 0 To listRec.Count - 1
                                '�X�V�t���O
                                htTtUpdFlg.Add(listRec(intTokuCnt).TokubetuTaiouCd, listRec(intTokuCnt).UpdFlg)
                                '�X�V����
                                If listRec(intTokuCnt).UpdDatetime <> DateTime.MinValue Then
                                    htTtDateTime.Add(listRec(intTokuCnt).TokubetuTaiouCd, listRec(intTokuCnt).UpdDatetime)
                                End If
                            Next
                        End If
                    End If

                    '*************************
                    ' �r���`�F�b�N����
                    '*************************
                    ' �n�Ճ��R�[�h�̓��ꍀ�ڂ𕡐�
                    RecordMappingHelper.Instance.CopyRecordData(jibanRec, jibanHaitaRec)

                    ' �r���`�F�b�N�pSQL����������
                    sqlString = upadteMake.MakeHaitaSQLInfo(GetType(JibanHaitaRecord), jibanHaitaRec, haitaList)

                    ' �r���`�F�b�N���{
                    Dim haitaKekkaList As List(Of JibanHaitaRecord) = _
                                DataMappingHelper.Instance.getMapArray(Of JibanHaitaRecord)(GetType(JibanHaitaRecord), _
                                dataAccess.CheckHaita(sqlString, haitaList))

                    If haitaKekkaList.Count > 0 Then
                        Dim haitaErrorData As JibanHaitaRecord = haitaKekkaList(0)

                        ' �r���`�F�b�N�G���[
                        mLogic.CallHaitaErrorMessage(sender, haitaErrorData.UpdLoginUserId, haitaErrorData.UpdDatetime, "�n�Ճe�[�u��")
                        Return False

                    End If

                    '*************************
                    ' �^�M�`�F�b�N����
                    '*************************
                    Dim jibanRecOld As New JibanRecord
                    jibanRecOld = GetJibanData(kbn, hosyousyoNo)

                    Dim YosinLogic As New YosinLogic
                    Dim intYosinResult As Integer = YosinLogic.YosinCheck(1, jibanRecOld, jibanRec)
                    Select Case intYosinResult
                        Case EarthConst.YOSIN_ERROR_YOSIN_OK
                            '�G���[�Ȃ�
                        Case EarthConst.YOSIN_ERROR_YOSIN_NG
                            '�^�M���x�z����
                            mLogic.AlertMessage(sender, Messages.MSG089E, 1)
                            Return False
                        Case Else
                            '6:�^�M�Ǘ����擾�G���[
                            '7:�^�M�Ǘ��e�[�u���X�V�G���[
                            '8:���[�����M�����G���[
                            '9:���̑��G���[
                            mLogic.AlertMessage(sender, String.Format(Messages.MSG090E, intYosinResult.ToString()), 1)
                            Return False
                    End Select

                    '*************************
                    ' �X�V�����e�[�u���̓o�^
                    '*************************
                    ' �{�喼
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.SesyuMei, _
                                                   EarthConst.RIREKI_SESYU_MEI, _
                                                   jibanRec.SesyuMei, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �����Z��
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Juusyo, _
                                                   EarthConst.RIREKI_BUKKEN_JYUUSYO, _
                                                   jibanRec.BukkenJyuusyo1 & jibanRec.BukkenJyuusyo2, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �����X�R�[�h
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.KameitenCd, _
                                                   EarthConst.RIREKI_BUKKEN_KAMEITEN_CD, _
                                                   jibanRec.KameitenCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �������
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.TyousaKaisya, _
                                                   EarthConst.RIREKI_TYOUSA_KAISYA, _
                                                   jibanRec.TysKaisyaCd & jibanRec.TysKaisyaJigyousyoCd, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' ���l
                    If dataAccess.AddKoushinRireki(kbn, _
                                                   hosyousyoNo, _
                                                   JibanDataAccess.enumItemName.Bikou, _
                                                   EarthConst.RIREKI_BIKOU, _
                                                   jibanRec.Bikou, _
                                                   jibanRec.UpdLoginUserId) = False Then
                        ' �G���[�����������I��
                        Return False
                    End If

                    ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�X�V�j
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 2
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = jibanRec.UpdLoginUserId
                    If dataAccess.EditJibanRenkeiData(renkeiJibanRec, True) <> 1 Then
                        ' �o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    '*************************
                    ' R-JHS�A�g�`�F�b�N
                    '*************************
                    ' ReportJHS�A�W�Ώۃ`�F�b�N���s��(�o�R�F0,1,5�̏ꍇ�̂�)
                    flgEditReportIf = False '������

                    '�o�R=0or1or5(�擪���������f�ΏۊO(�o�R=9)�Ɣ��肳�ꂽ�ꍇ�A�Ȍ�ȉ��̏����̓X���[)
                    If jibanRec.Keiyu = 0 Or _
                        jibanRec.Keiyu = 1 Or _
                        jibanRec.Keiyu = 5 Then

                        'R-JHS�A�g�`�F�b�N
                        If Me.ChkRJhsRenkei(jibanRec.TysKaisyaCd, jibanRec.TysKaisyaJigyousyoCd) Then
                            ' �Ώۂ̏ꍇ�A�t���O�𗧂Ă�
                            flgEditReportIf = True
                            ' ���f�ΏۂȂ̂Ōo�R��5�ɕύX
                            jibanRec.Keiyu = 5
                        Else
                            ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N��NG�ɂȂ����ꍇ�A�o�R��9�ɕύX
                            jibanRec.Keiyu = 9
                        End If
                    End If

                    '�A�������̏ꍇ
                    If jibanRec.RentouBukkenSuu > 1 Then
                        'R-JHS�A�g�Ώۂ̏ꍇ
                        If flgEditReportIf Then
                            If intInitKeiyu = 0 AndAlso jibanRec.SyoriKensuu > 0 Then '���.�o�R=0�ł��A2���ڈȍ~�̏ꍇ
                                '�A�����̘A�g�Ōo�R=0�̏ꍇ�A1���ڂ̂ݘA�g
                                '1���ځF�A�g���A�o�R��5or9�ɂȂ�
                                '2���ڈȍ~�F�A�g�����A�o�R��0�̂܂�
                                jibanRec.Keiyu = intInitKeiyu
                                flgEditReportIf = False
                            End If
                        End If
                    End If

                    '*************************
                    ' �n�Ճe�[�u���̍X�V
                    '*************************
                    ' �X�V�pUPDATE����������
                    updateString = upadteMake.MakeUpdateInfo(GetType(JibanRecord), jibanRec, list)

                    ' �n�Ճe�[�u���̍X�V���s���i�V�K�͍̔Ԏ��A�폜�͕ʋ@�\�Ȃ̂Œn�Ֆ{�͍̂X�V�̂݁j
                    If dataAccess.UpdateJibanData(updateString, list) = True Then

                        '**************************************************************************
                        ' �@�ʐ����f�[�^�̒ǉ��E�X�V�E�폜
                        '**************************************************************************
                        '�@�ʐ����e�[�u���f�[�^����p
                        Dim TeibetuSyuuseiLogic As New TeibetuSyuuseiLogic
                        Dim tempTeibetuRec As New TeibetuSeikyuuRecord

                        '***************************
                        ' ���i�R�[�h�P
                        '***************************
                        '���i�P���R�[�h���e���|�����ɃZ�b�g
                        tempTeibetuRec = jibanRec.Syouhin1Record

                        If tempTeibetuRec IsNot Nothing Then
                            '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                            If jibanRec.RentouBukkenSuu > 1 Then
                                tempTeibetuRec.HosyousyoNo = strTmpBangou
                            End If
                        End If

                        '���i�P�f�[�^����
                        If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                 tempTeibetuRec, _
                                                                 EarthConst.SOUKO_CD_SYOUHIN_1, _
                                                                 1, _
                                                                 jibanRec, _
                                                                 renkeiTeibetuList, _
                                                                 GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                            Return False
                        End If

                        '***************************
                        ' ���i�R�[�h�Q
                        '***************************
                        Dim i As Integer
                        For i = 1 To EarthConst.SYOUHIN2_COUNT
                            If jibanRec.Syouhin2Records.ContainsKey(i) = True Then

                                '���i�Q���R�[�h���e���|�����ɃZ�b�g
                                tempTeibetuRec = jibanRec.Syouhin2Records.Item(i)

                                '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' ���i�Q�̓@�ʐ����f�[�^���X�V���܂�
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         tempTeibetuRec, _
                                                                         tempTeibetuRec.BunruiCd, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList, _
                                                                         GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                                    Return False
                                End If
                            Else
                                ' �폜�����p(�폜�m�F�͏��i�Q�̕��ރR�[�h���ꂩ�Ŗ��Ȃ�)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_2_110, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
                                                                 ) = False Then
                                    Return False
                                End If
                            End If
                        Next

                        '***************************
                        ' ���i�R�[�h�R
                        '***************************
                        i = 1
                        For i = 1 To EarthConst.SYOUHIN3_COUNT
                            If jibanRec.Syouhin3Records.ContainsKey(i) = True Then

                                '���i�R���R�[�h���e���|�����ɃZ�b�g
                                tempTeibetuRec = jibanRec.Syouhin3Records.Item(i)

                                '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                                If jibanRec.RentouBukkenSuu > 1 Then
                                    tempTeibetuRec.HosyousyoNo = strTmpBangou
                                End If

                                ' ���i�R�̓@�ʐ����f�[�^���X�V���܂�
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         tempTeibetuRec, _
                                                                         tempTeibetuRec.BunruiCd, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList, _
                                                                         GetType(TeibetuSeikyuuRecord) _
                                                                 ) = False Then
                                    Return False
                                End If
                            Else
                                ' �폜�����p(�폜�m�F�͏��i�R�̕��ރR�[�h���ꂩ�Ŗ��Ȃ�)
                                If TeibetuSyuuseiLogic.EditTeibetuRecord(sender, _
                                                                         Nothing, _
                                                                         EarthConst.SOUKO_CD_SYOUHIN_3, _
                                                                         i, _
                                                                         jibanRec, _
                                                                         renkeiTeibetuList _
                                                                 ) = False Then
                                    Return False
                                End If
                            End If
                        Next

                        ' �@�ʐ����A�g���f�Ώۂ����݂���ꍇ�A���f���s��
                        For Each renkeiTeibetuRec As TeibetuSeikyuuRenkeiRecord In renkeiTeibetuList
                            ' �A�g�p�e�[�u���ɔ��f����i�@�ʐ����j
                            If dataAccess.EditTeibetuRenkeiData(renkeiTeibetuRec) <> 1 Then
                                ' �o�^�Ɏ��s�����̂ŏ������f
                                Return False
                            End If
                        Next

                        '*************************
                        ' R-JHS�A�g�X�V
                        '*************************
                        '��Ƀ`�F�b�N���Ă�����ReportJHS�A�W�Ώۃ`�F�b�N�����ɁA�i���f�[�^�e�[�u���X�V�������s��
                        If flgEditReportIf Then
                            ' �i���f�[�^�e�[�u���X�V�����ďo
                            If EditReportIfData(jibanRec) = False Then
                                ' �G���[�����������I��
                                Return False
                            End If
                        End If

                        ' �����������e�[�u���ǉ�(�ۏؗL���ύX���̂݁A��������T�ɐV�K�ǉ�����)
                        If Not brRec Is Nothing Then
                            Dim brLogic As New BukkenRirekiLogic

                            '�V�K�ǉ��p�X�N���v�g����ю��s
                            brRec.Kbn = jibanRec.Kbn
                            brRec.HosyousyoNo = jibanRec.HosyousyoNo
                            If brLogic.InsertBukkenRireki(brRec) = False Then
                                Return False
                            End If
                        End If

                        '**********************************************
                        ' ���ʑΉ��f�[�^�̍X�V(�@�ʐ����f�[�^�Ƃ̕R�t)
                        '**********************************************
                        '�A����������2�ȏ�̏ꍇ�A��L�R�s�[��`�F�b�N�Ώۂ̔ԍ����J�E���g�A�b�v
                        If jibanRec.RentouBukkenSuu > 1 Then
                            If Not listRec Is Nothing Then
                                '�A���p�̕ۏ؏�No��A�Ԃłӂ�Ȃ���
                                For intTokuCnt As Integer = 0 To listRec.Count - 1
                                    With listRec(intTokuCnt)
                                        '�ԍ�
                                        .HosyousyoNo = strTmpBangou
                                        '�X�V�t���O
                                        If htTtUpdFlg.ContainsKey(.TokubetuTaiouCd) Then
                                            .UpdFlg = htTtUpdFlg(.TokubetuTaiouCd)
                                        End If
                                        '�X�V����
                                        If htTtDateTime.ContainsKey(.TokubetuTaiouCd) Then
                                            .UpdDatetime = htTtDateTime(.TokubetuTaiouCd)
                                        End If
                                    End With
                                Next
                            End If
                        End If

                        '���ʑΉ��f�[�^�X�V
                        If cbLogic.pf_setKey_TokubetuTaiou_TeibetuSeikyuu(sender, listRec, jibanRec.UpdLoginUserId, EarthEnum.emGamenInfo.Jutyuu) = False Then
                            Return False
                            Exit Function
                        End If

                        '*************************
                        ' �A�������Ή�
                        '*************************
                        '�ԍ����J�E���g�A�b�v
                        intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                        strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

                        jibanRec.SyoriKensuu += 1 '��������

                    Else
                        Return False
                    End If

                Next

                '����������󋵂̍ŏI�`�F�b�N
                If Me.ChkLatestBukkenNayoseJyky(sender, jibanRec) = False Then
                    ' �G���[�����������I��
                    Return False
                End If

                '*************************
                ' �A�������Ή�
                '*************************
                '��ʍĕ`��p�ɁA�n�Ճ��R�[�h�Ď擾(�ԍ��͓���)
                jibanRecAfterExe = GetJibanData(kbn, strRetBangou)

                ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        '��������(���v) += ��������(������)
        jibanRecAfterExe.SyoriKensuu += jibanRec.SyoriKensuu
        Return True

    End Function
#End Region

#Region "�n�Ճf�[�^�폜"
    ''' <summary>
    ''' �n�Ճf�[�^���폜���܂��B�֘A����@�ʐ����f�[�^�̍폜���s���܂�
    ''' </summary>
    ''' <param name="kbn">�敪</param>
    ''' <param name="hosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="deleteTeibetu">�@�ʐ������R�[�h���폜����ꍇ�FTrue</param>
    ''' <param name="userId">���O�C�����[�U�[ID</param>
    ''' <returns>True:�폜���� False:�폜���s</returns>
    ''' <remarks></remarks>
    Public Function DeleteJibanData(ByVal sender As System.Object, _
                                    ByVal kbn As String, _
                                    ByVal hosyousyoNo As String, _
                                    ByVal deleteTeibetu As Boolean, _
                                    ByVal userId As String) As Boolean
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".DeleteJibanData", _
                                            kbn, _
                                            hosyousyoNo, _
                                            deleteTeibetu, _
                                            userId)

        Try
            ' �n�Ճe�[�u���Ɠ@�ʐ����̓�����ۂ��߁A�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' �n�Ճf�[�^�擾�p�f�[�^�A�N�Z�X�N���X
                Dim dataAccess As JibanDataAccess = New JibanDataAccess
                '�A�g�Ǘ��p���W�b�N�N���X
                Dim clsRenkei As New RenkeiKanriLogic
                Dim intResult As Integer

                intResult = 0

                ' �폜����O�ɓ@�ʐ����f�[�^��Key��S�Ď擾���Ă���
                Dim teibetuSeikyuuList As List(Of TeibetuSeikyuuRecord) = _
                DataMappingHelper.Instance.getMapArray(Of TeibetuSeikyuuRecord)(GetType(TeibetuSeikyuuRecord), _
                dataAccess.GetTeibetuSeikyuuDataKey(kbn, hosyousyoNo))

                If teibetuSeikyuuList.Count <= 0 Then
                    deleteTeibetu = False
                End If

                ' �n�Ճf�[�^�̍폜�����{�A�@�ʐ����͕K�v�ɉ����č폜�\�i��{�I�ɍ폜�͓����K�v�j
                If dataAccess.DeleteJibanData(kbn, hosyousyoNo, deleteTeibetu, userId) = True Then

                    ' �A�g�p�e�[�u���ɓo�^����i�n�Ձ|�폜�j
                    Dim renkeiJibanRec As New JibanRenkeiRecord
                    renkeiJibanRec.Kbn = kbn
                    renkeiJibanRec.HosyousyoNo = hosyousyoNo
                    renkeiJibanRec.RenkeiSijiCd = 9
                    renkeiJibanRec.SousinJykyCd = 0
                    renkeiJibanRec.UpdLoginUserId = userId
                    If clsRenkei.DeleteJibanRenkeiData(renkeiJibanRec) <> 1 Then
                        '�o�^�Ɏ��s�����̂ŏ������f
                        Return False
                    End If

                    ' �A�g�e�[�u���ɓo�^�i�@�ʐ����|�폜�j
                    For Each rec As TeibetuSeikyuuRecord In teibetuSeikyuuList
                        Dim renkeiTeibetuRec As New TeibetuSeikyuuRenkeiRecord
                        renkeiTeibetuRec.Kbn = rec.Kbn
                        renkeiTeibetuRec.HosyousyoNo = rec.HosyousyoNo
                        renkeiTeibetuRec.BunruiCd = rec.BunruiCd
                        renkeiTeibetuRec.GamenHyoujiNo = rec.GamenHyoujiNo
                        renkeiTeibetuRec.RenkeiSijiCd = 9
                        renkeiTeibetuRec.SousinJykyCd = 0
                        renkeiTeibetuRec.UpdLoginUserId = userId
                        If clsRenkei.DeleteTeibetuSeikyuuRenkeiData(renkeiTeibetuRec) <> 1 Then
                            '�o�^�Ɏ��s�����̂ŏ������f
                            Return False
                        End If
                    Next

                    ' �폜�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                    scope.Complete()
                Else
                    Return False
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function
#End Region

#Region "�n��t���O�擾"
    ''' <summary>
    ''' �n��R�[�h���n��t���O���擾���܂�
    ''' </summary>
    ''' <param name="keiretuCd">�n��R�[�h</param>
    ''' <returns>�n��t���O</returns>
    ''' <remarks>1:�n��</remarks>
    Public Function GetKeiretuFlg(ByVal keiretuCd As String) As Integer

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Select Case keiretuCd
            Case EarthConst.KEIRETU_AIFURU
                Return 1
            Case EarthConst.KEIRETU_TH
                Return 1
            Case EarthConst.KEIRETU_WANDA
                Return 1
        End Select

        Return 0

    End Function

    ''' <summary>
    ''' �n��R�[�h���n��t���O���擾���܂�(2�n��̂� 0001,THTH)
    ''' </summary>
    ''' <param name="keiretuCd">�n��R�[�h</param>
    ''' <returns>�n��t���O</returns>
    ''' <remarks>1:�n��</remarks>
    Public Function GetKeiretuFlg2(ByVal keiretuCd As String) As Integer

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKeiretuFlg", _
                                            keiretuCd)

        Select Case keiretuCd
            Case EarthConst.KEIRETU_AIFURU
                Return 1
            Case EarthConst.KEIRETU_TH
                Return 1
        End Select

        Return 0

    End Function
#End Region

#Region "�r���_�[���擾"
    ''' <summary>
    ''' �r���_�[����List(Of BuilderInfoRecord)�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>List(Of BuilderInfoRecord)</returns>
    ''' <remarks></remarks>
    Public Function GetBuilderInfo(ByVal strKameitenCd As String) As List(Of BuilderInfoRecord)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetBuilderInfo", _
                                            strKameitenCd)

        Dim dataAccess As New BuilderDataAccess
        Dim list As List(Of BuilderInfoRecord)

        ' �f�[�^���擾���AList(Of BuilderInfoRecord)�Ɋi�[����
        list = DataMappingHelper.Instance.getMapArray(Of BuilderInfoRecord)(GetType(BuilderInfoRecord), _
                                                      dataAccess.GetBuilderData(strKameitenCd))

        Return list

    End Function
#End Region

#Region "�Z����i���f�[�^�p�ɕϊ�"
    ''' <summary>
    ''' ��ʂœ��͂��ꂽ�Z���P�C�Q�C�R��i���f�[�^�p�Z���P�C�Q�ɕϊ�����
    ''' </summary>
    ''' <param name="juusyo1">��ʂœ��͂��ꂽ�����Z���P</param>
    ''' <param name="juusyo2">��ʂœ��͂��ꂽ�����Z���Q</param>
    ''' <param name="juusyo3">��ʂœ��͂��ꂽ�����Z���R</param>
    ''' <returns>ArrayList�F(0)�ɕϊ���Z���P�A(1)�ɕϊ���Z���Q</returns>
    ''' <remarks></remarks>
    Public Function ConvJuusyo2Report(ByVal juusyo1 As String, _
                                      ByVal juusyo2 As String, _
                                      ByVal juusyo3 As String) As Array

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ConvJuusyo2Report", _
                                            juusyo1, _
                                            juusyo2, _
                                            juusyo3)

        Dim retArr() As String = New String() {String.Empty, String.Empty}

        juusyo1 = IIf(juusyo1 Is Nothing, String.Empty, juusyo1)
        juusyo2 = IIf(juusyo2 Is Nothing, String.Empty, juusyo2)
        juusyo3 = IIf(juusyo3 Is Nothing, String.Empty, juusyo3)

        If sLogic.GetStrByteSJIS(juusyo3) <= 28 Then
            retArr(0) = juusyo1
            retArr(1) = juusyo2 & juusyo3
        Else
            Dim retJuusyo1 As String = juusyo1
            Dim retJuusyo2 As String = String.Empty

            '�ϊ��Z���P�ɏZ���Q���ꕶ�����ǉ����A60�o�C�g�𒴂������_�ŁA
            '�ȍ~��ϊ��Z���Q�ɃZ�b�g
            For sc As Integer = 0 To juusyo2.Length - 1
                Dim tmpJuusyo1 As String = retJuusyo1 + juusyo2.Substring(sc, 1)
                If sLogic.GetStrByteSJIS(tmpJuusyo1) > 60 Then
                    retJuusyo2 = juusyo2.Substring(sc)
                    Exit For
                Else
                    retJuusyo1 = tmpJuusyo1
                End If
            Next

            '�ϊ��Z���Q�ɏZ���R��ǉ�
            retJuusyo2 += juusyo3

            retArr(0) = retJuusyo1
            retArr(1) = retJuusyo2
        End If

        Return retArr

    End Function
#End Region

#Region "ReportIf�A�g�f�[�^�ҏW"

    ''' <summary>
    ''' ReportIf�A�g�`�F�b�N����
    ''' </summary>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strJigyousyoCd">���Ə��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkRJhsRenkei(ByVal strTysKaisyaCd As String, ByVal strJigyousyoCd As String) As Boolean
        ' �i���f�[�^�����n�f�[�^�A�N�Z�X�N���X
        Dim reportAccess As New ReportIfDataAccess

        ' �A�g������Аݒ�}�X�^�̑��݃`�F�b�N
        If reportAccess.ChkRenkeiTyousaKaisya(strTysKaisyaCd, strJigyousyoCd) Then
            Return True
        End If
        Return False
    End Function
    ''' <summary>
    ''' �n�Ճ��R�[�h�̓��e���i���f�[�^�̕ҏW���s���܂�
    ''' </summary>
    ''' <param name="jibanRec"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EditReportIfData(ByVal jibanRec As JibanRecordBase) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditReportIfData", _
                                            jibanRec)

        ' �i���f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New ReportIfDataAccess

        ' �ڋqNO�͋敪�{�ۏ؏�NO
        Dim kokyakuNo As String = jibanRec.Kbn.Trim() & jibanRec.HosyousyoNo.Trim()

        ' �i���e�[�u���̑��݃`�F�b�N�pSQL�����s����
        Dim chkList As List(Of ReportIfChkRecord) = _
                                        DataMappingHelper.Instance.getMapArray(Of ReportIfChkRecord)( _
                                        GetType(ReportIfChkRecord), _
                                        dataAccess.GetReportIf(kokyakuNo))

        ' �i���e�[�u���̓o�^�E�X�V����p
        Dim editMode As ReportIfDataAccess.EDIT_MODE

        If chkList.Count = 0 Then
            ' �f�[�^�����݂��Ȃ��̂ŐV�K
            editMode = ReportIfDataAccess.EDIT_MODE.MODE_INSERT
        Else
            ' �f�[�^�����݂���̂ōX�V
            editMode = ReportIfDataAccess.EDIT_MODE.MODE_UPDATE

            ' �i�����R�[�h���݃`�F�b�N�p���R�[�h�̍쐬
            Dim reportCheckRec As ReportIfChkRecord = chkList(0)

        End If

        ' �i�����R�[�h�̍쐬
        Dim reportRec As New ReportIfRecord
        reportRec.KokyakuNo = kokyakuNo                                         ' �ڋq�ԍ�
        reportRec.ChousaTachiai = GetTatiaiWord(jibanRec.TatiaisyaCd)           ' ���������
        '�����������s����O�ɓ��ʑΉ��f�[�^��DB�X�V����Ă��邱�ƁI
        reportRec.Options = Me.SetOptions(jibanRec)                             ' �I�v�V����

        ' �o�^�E�X�V�����s����
        If dataAccess.EditReportIf(reportRec, editMode) < 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ����҃R�[�h��蕶�����ԋp���܂�
    ''' </summary>
    ''' <param name="tatiaiCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTatiaiWord(ByVal tatiaiCd As Integer) As String

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTatiaiWord", _
                                            tatiaiCd)

        Dim retValue As String = ""

        Select Case tatiaiCd
            Case 1
                ' �{��l
                retValue = EarthConst.TATIAI_SESYU_SAMA
            Case 2
                ' �S����
                retValue = EarthConst.TATIAI_TANTOUSYA
            Case 3
                ' �{��l�A�S����
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_TANTOUSYA
            Case 4
                ' ���̑�
                retValue = EarthConst.TATIAI_SONOTA
            Case 5
                ' �{��l�A���̑�
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
            Case 6
                ' �S���ҁA���̑�
                retValue = EarthConst.TATIAI_TANTOUSYA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
            Case 7
                ' �{��l�A�S���ҁA���̑�
                retValue = EarthConst.TATIAI_SESYU_SAMA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_TANTOUSYA & _
                            EarthConst.TATIAI_SEP_STRING & _
                            EarthConst.TATIAI_SONOTA
        End Select

        Return retValue

    End Function

    ''' <summary>
    ''' �i���e�[�u���X�V�p�̃f�[�^���R�s�[����
    ''' </summary>
    ''' <param name="motoJibanRecord">�R�s�[���n�Ճ��R�[�h</param>
    ''' <param name="sakiJibanRecord">�R�s�[��n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetSintyokuJibanData(ByVal motoJibanRecord As JibanRecordBase, ByRef sakiJibanRecord As JibanRecordBase)

        Dim JibanLogic As New JibanLogic

        sakiJibanRecord.Kbn = motoJibanRecord.Kbn                                                   ' �敪
        sakiJibanRecord.HosyousyoNo = motoJibanRecord.HosyousyoNo                                   ' �ԍ�
        sakiJibanRecord.TysGaiyou = motoJibanRecord.TysGaiyou                                       ' �T�[�r�X�敪(�����T�v�R�[�h���Z�b�g)
        sakiJibanRecord.TysHouhouMeiIf = motoJibanRecord.TysHouhouMeiIf                             ' �������@���������@��
        sakiJibanRecord.KouzouMeiIf = motoJibanRecord.KouzouMeiIf                                   ' �v�挚�����\����
        sakiJibanRecord.SesyuMei = motoJibanRecord.SesyuMei                                         ' �{�喼
        sakiJibanRecord.BukkenJyuusyo1 = motoJibanRecord.BukkenJyuusyo1                             ' �����Z���P
        sakiJibanRecord.BukkenJyuusyo2 = motoJibanRecord.BukkenJyuusyo2                             ' �����Z���Q
        sakiJibanRecord.BukkenJyuusyo3 = motoJibanRecord.BukkenJyuusyo3                             ' �����Z���R
        sakiJibanRecord.TysKibouDate = motoJibanRecord.TysKibouDate                                 ' ������]��
        sakiJibanRecord.TysKibouJikan = motoJibanRecord.TysKibouJikan                               ' ������]����
        sakiJibanRecord.IraiTantousyaMei = motoJibanRecord.IraiTantousyaMei                         ' �����S���Җ�
        sakiJibanRecord.TatiaisyaCd = motoJibanRecord.TatiaisyaCd                                   ' ���������
        sakiJibanRecord.KameitenCd = motoJibanRecord.KameitenCd                                     ' �����X�R�[�h
        sakiJibanRecord.KameitenMeiIf = motoJibanRecord.KameitenMeiIf                               ' �����X��
        sakiJibanRecord.KameitenTelIf = motoJibanRecord.KameitenTelIf                               ' �����X�d�b�ԍ�
        sakiJibanRecord.KameitenFaxIf = motoJibanRecord.KameitenFaxIf                               ' �����XFAX�ԍ�
        sakiJibanRecord.KameitenMailIf = motoJibanRecord.KameitenMailIf                             ' �����X���[���A�h���X
        sakiJibanRecord.TysKaisyaCd = motoJibanRecord.TysKaisyaCd                                   ' ������ЃR�[�h
        sakiJibanRecord.TysKaisyaJigyousyoCd = motoJibanRecord.TysKaisyaJigyousyoCd                 ' ������Ў��Ə��R�[�h
        sakiJibanRecord.TysKaisyaMeiIf = motoJibanRecord.TysKaisyaMeiIf                             ' ������Ж�
        sakiJibanRecord.TysRenrakusakiAtesakiMei = motoJibanRecord.TysRenrakusakiAtesakiMei         ' �����A����(����A��Ж�)
        sakiJibanRecord.TysRenrakusakiTel = motoJibanRecord.TysRenrakusakiTel                       ' �����A����(TEL)
        sakiJibanRecord.TysRenrakusakiFax = motoJibanRecord.TysRenrakusakiFax                       ' �����A����(FAX)
        sakiJibanRecord.TysRenrakusakiMail = motoJibanRecord.TysRenrakusakiMail                     ' �����A����(MAIL)
        sakiJibanRecord.IraiTantousyaMei = motoJibanRecord.IraiTantousyaMei                         ' �����A����(�S����)
        sakiJibanRecord.Kouzou = motoJibanRecord.Kouzou                                             ' �\��(�����ł̓R�[�h���Z�b�g�B���̂�DB�o�^����SQL�Ŏ擾)
        sakiJibanRecord.Kaisou = motoJibanRecord.Kaisou                                             ' �K�w(�����ł̓R�[�h���Z�b�g�B���̂�DB�o�^����SQL�Ŏ擾)
        sakiJibanRecord.SekkeiKyoyouSijiryoku = motoJibanRecord.SekkeiKyoyouSijiryoku               ' �݌v���e�x����
        sakiJibanRecord.NegiriHukasa = motoJibanRecord.NegiriHukasa                                 ' ���؂�[��
        sakiJibanRecord.YoteiKs = motoJibanRecord.YoteiKs                                           ' �\���b��(�����ł̓R�[�h���Z�b�g�B���̂�DB�o�^����SQL�Ŏ擾)
        sakiJibanRecord.Syako = motoJibanRecord.Syako                                               ' �Ԍ�(�����ł̓R�[�h���Z�b�g�B���̂�DB�o�^����SQL�Ŏ擾)
        sakiJibanRecord.YoteiMoritutiAtusa = motoJibanRecord.YoteiMoritutiAtusa                     ' �\�萷�y����
        sakiJibanRecord.DoujiIraiTousuu = motoJibanRecord.DoujiIraiTousuu                           ' �����˗�����
        sakiJibanRecord.TatemonoYoutoNo = motoJibanRecord.TatemonoYoutoNo                           ' �����p�r��(�����ł̓R�[�h���Z�b�g�B���̂�DB�o�^����SQL�Ŏ擾)
        sakiJibanRecord.Keiyu = motoJibanRecord.Keiyu                                               ' �o�R

    End Sub

    ''' <summary>
    ''' �i���f�[�^�e�[�u����Options�ɃZ�b�g����l��ҏW����
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h�N���X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetOptions(ByVal jibanRec As JibanRecordBase) As String
        Dim strRet As String = String.Empty '�߂�l

        Dim intTotalCnt As Integer = 0
        Dim listRes As List(Of TokubetuTaiouRecordBase)
        Dim dtRec As TokubetuTaiouRecordBase
        Dim intCnt As Integer = 0 '�J�E���^
        Dim ttLogic As New TokubetuTaiouLogic
        Dim sender As New Object

        '���ʑΉ��}�X�^
        With jibanRec
            listRes = ttLogic.GetTokubetuTaiouDataInfo(sender, .Kbn, .HosyousyoNo, String.Empty, intTotalCnt)
        End With

        ' �������ʃ[�����̏ꍇ
        If intTotalCnt <= 0 Then
            '�������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Return strRet
            Exit Function
        End If

        '�Ώۃf�[�^��蒊�o
        For intCnt = 0 To listRes.Count - 1
            '���R�[�h�����݂���ꍇ�̂݉�ʕ\��
            dtRec = listRes(intCnt)
            If Not dtRec Is Nothing AndAlso dtRec.TokubetuTaiouCd <> Integer.MinValue Then
                With dtRec
                    '����łȂ��A���ʑΉ��R�[�h��0�ȏ�99�ȉ����Z�b�g�Ώ�
                    If .Torikesi = 0 AndAlso (.TokubetuTaiouCd >= 0 And .TokubetuTaiouCd <= 99) Then
                        If strRet = String.Empty Then
                            strRet = dtRec.TokubetuTaiouCd.ToString
                        Else
                            strRet &= EarthConst.SEP_STRING_REPORTIF & dtRec.TokubetuTaiouCd.ToString
                        End If
                    End If
                End With
            End If
        Next

        Return strRet
    End Function

    ''' <summary>
    ''' �i���f�[�^�̃��R�[�h�̓��e���擾���܂�
    ''' </summary>
    ''' <param name="jibanRec">�n�Ճ��R�[�h�N���X</param>
    ''' <param name="reportRec">�i���f�[�^���R�[�h�N���X</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetReportIfData(ByVal jibanRec As JibanRecordBase, ByRef reportRec As ReportIfGetRecord) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetReportIfData", _
                                            jibanRec, _
                                            reportRec)

        ' �i���f�[�^�A�N�Z�X�N���X
        Dim dataAccess As New ReportIfDataAccess

        ' �ڋqNO�͋敪�{�ۏ؏�NO
        Dim kokyakuNo As String = jibanRec.Kbn.Trim() & jibanRec.HosyousyoNo.Trim()

        ' �i���e�[�u���擾SQL�����s����
        Dim List As List(Of ReportIfGetRecord) = _
                                        DataMappingHelper.Instance.getMapArray(Of ReportIfGetRecord)( _
                                        GetType(ReportIfGetRecord), _
                                        dataAccess.GetReportIfData(kokyakuNo))

        If List.Count > 0 Then
            reportRec = List(0)
        End If
        Return True

    End Function
#End Region

    ''' <summary>
    ''' �@�ʐ������R�[�h�̃f�[�^���R�s�[����
    ''' </summary>
    ''' <param name="motoJibanRecord">�R�s�[���n�Ճ��R�[�h</param>
    ''' <param name="sakiJibanRecord">�R�s�[��n�Ճ��R�[�h</param>
    ''' <remarks></remarks>
    Public Sub ps_CopyTeibetuSyouhinData(ByVal motoJibanRecord As JibanRecordBase, ByRef sakiJibanRecord As JibanRecordBase)

        sakiJibanRecord.Syouhin1Record = motoJibanRecord.Syouhin1Record
        sakiJibanRecord.Syouhin2Records = motoJibanRecord.Syouhin2Records
        sakiJibanRecord.Syouhin3Records = motoJibanRecord.Syouhin3Records

    End Sub

#Region "�n��T.�����R�[�h�̑��݃`�F�b�N���s�Ȃ��܂�"
    ''' <summary>
    ''' �n��T.�����R�[�h�̑��݃`�F�b�N���s�Ȃ��܂�
    ''' </summary>
    ''' <param name="strBunjouCd">�����R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkJibanBunjouCd( _
                                        ByVal strBunjouCd As String _
                                        ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkJibanBunjouCd", strBunjouCd)
        Dim blnRes As Boolean = False

        ' �����R�[�h���擾���Č��ʂ�Ԃ�
        blnRes = dataAccess.GetBunjouCd(strBunjouCd)
        Return blnRes
    End Function
#End Region

#Region "�n�Ճf�[�^�̑��݃`�F�b�N"
    ''' <summary>
    ''' �n�Ճf�[�^�̑��݃`�F�b�N���s�Ȃ��܂�
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExistsJibanData(ByVal strKbn As String, ByVal strBangou As String) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ExistsJibanData", _
                                            strKbn, strBangou)

        Dim blnChk As Boolean = False

        ' �n�Ճf�[�^���`�F�b�N
        blnChk = dataAccess.IsJibanData(strKbn, strBangou)

        Return blnChk
    End Function

    ''' <summary>
    ''' �n�Ճf�[�^�𑖍����A�������̖���󋵂��`�F�b�N���܂�
    ''' </summary>
    ''' <param name="strBukkenNo">���.����NO(�敪 + �ԍ�)</param>
    ''' <param name="strNayoseNo">���.�����NO(�敪 + �ԍ�)</param>
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function ChkBukkenNayoseJyky( _
                                        ByVal strBukkenNo As String, _
                                         ByVal strNayoseNo As String _
                                        ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBukkenNayoseJyky", _
                                            strBukkenNo, strNayoseNo)

        Dim blnChk As Boolean = False

        Dim strRet() As String = {}
        Dim strRetNayose() As String = {}

        Dim strKbn As String = String.Empty
        Dim strBangou As String = String.Empty

        '����NO���敪�Ɣԍ��ɕ������Ď擾
        strRet = sLogic.GetSepBukkenNo(strBukkenNo)
        strRetNayose = sLogic.GetSepBukkenNo(strNayoseNo)

        ' �n�Ճf�[�^�Ɩ����R�[�h���`�F�b�N
        If strBukkenNo <> "" Then
            blnChk = dataAccess.ChkJibanDataNayoseNotChildren(strRet(0), strRet(1), strRetNayose(0), strRetNayose(1))
        End If

        Return blnChk
    End Function

    ''' <summary>
    ''' �n�Ճf�[�^�𑖍����āA�Y���������e�������ǂ����̔��f���s�Ȃ��܂�
    ''' </summary>
    ''' <param name="strBukkenNo">����NO(�敪 + �ԍ�)</param>
    ''' <param name="strNayoseCd">�����R�[�h(�敪 + �ԍ�)</param>
    ''' <returns>True:���݂��� False:���݂��Ȃ�</returns>
    ''' <remarks></remarks>
    Public Function ChkBukkenNayoseOyaBukken( _
                                        ByVal strBukkenNo As String, _
                                        ByVal strNayoseCd As String _
                                        ) As Boolean

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".ChkBukkenNayoseOyaBukken", _
                                            strBukkenNo, strNayoseCd)

        Dim blnChk As Boolean = False

        Dim strRet() As String = {}
        Dim strRetNayose() As String = {}

        Dim strKbn As String = String.Empty
        Dim strBangou As String = String.Empty

        '����NO���敪�Ɣԍ��ɕ������Ď擾
        strRet = sLogic.GetSepBukkenNo(strBukkenNo)
        strRetNayose = sLogic.GetSepBukkenNo(strNayoseCd)

        ' �n�Ճf�[�^�Ɩ����R�[�h���`�F�b�N
        If strNayoseCd <> "" Then
            blnChk = dataAccess.ChkJibanDataOyaBukken(strRet(0), strRet(1), strRetNayose(0), strRetNayose(1))
        End If

        Return blnChk
    End Function

#Region "��������󋵂̍ŐV��Ԃ��`�F�b�N"

    ''' <summary>
    ''' ��������󋵂̍ŐV��Ԃ��`�F�b�N����
    ''' </summary>
    ''' <param name="sender">�N���C�A���g �X�N���v�g �u���b�N��o�^����R���g���[��</param>
    ''' <param name="dtRec">�n�Ճ��R�[�h</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkLatestBukkenNayoseJyky(ByVal sender As Object, _
                                  ByVal dtRec As JibanRecordBase) As Boolean

        Dim blnRet As Boolean = True

        Dim jibanRec As New JibanRecordBase 'TMP�p

        Dim strBukkenNo As String = String.Empty
        Dim strBukkenNayoseCd As String = String.Empty
        Dim strErrMsg As String = String.Empty

        '�Ώےn�Ճ��R�[�h����Ɨp�̃��R�[�h�N���X�Ɋi�[����
        jibanRec = dtRec

        strBukkenNo = jibanRec.Kbn & jibanRec.HosyousyoNo
        strBukkenNayoseCd = jibanRec.BukkenNayoseCd

        '����悪�e�������̃`�F�b�N
        If Me.ChkBukkenNayoseOyaBukken(strBukkenNo, strBukkenNayoseCd) = False Then
            blnRet = False
            strErrMsg = Messages.MSG167E.Replace("@PARAM1", "�����̕���").Replace("@PARAM2", "�q����").Replace("@PARAM3", "��������R�[�h")
            mLogic.AlertMessage(sender, strErrMsg, 0, "ChkLatestBukkenNayoseJyky1")
            Exit Function
        End If

        '�������̖���󋵃`�F�b�N
        If Me.ChkBukkenNayoseJyky(strBukkenNo, strBukkenNayoseCd) = False Then
            blnRet = False
            strErrMsg = Messages.MSG167E.Replace("@PARAM1", "������NO").Replace("@PARAM2", "�������̖����").Replace("@PARAM3", "��������R�[�h")
            mLogic.AlertMessage(sender, strErrMsg, 0, "ChkLatestBukkenNayoseJyky2")
            Exit Function
        End If

        Return blnRet
    End Function
#End Region

#End Region

#Region "�@�ʐ����f�[�^�̎擾"
    ''' <summary>
    ''' �@�ʐ����f�[�^�̎擾
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <param name="strBunruiCd">���ރR�[�h[�f�t�H���g�F""]</param>
    ''' <returns>�f�[�^�e�[�u��</returns>
    ''' <remarks></remarks>
    Public Function GetTeibetuSeikyuuData(ByVal strKbn As String, ByVal strBangou As String, Optional ByVal strBunruiCd As String = "") As DataTable

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetTeibetuSeikyuuData", _
                                            strKbn, strBangou, strBunruiCd)
        Dim dt As DataTable

        ' �@�ʐ����f�[�^���擾
        dt = dataAccess.GetTeibetuSeikyuuData(strKbn, strBangou, strBunruiCd)

        Return dt
    End Function
#End Region

#Region "�ۏ؏��i�L���̎擾"

    ''' <summary>
    ''' �ۏ؏��i�L���̎擾
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBangou">�ԍ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHosyouSyouhinUmu(ByVal strKbn As String, ByVal strBangou As String) As String
        Dim strHosyouUmu As String = String.Empty

        strHosyouUmu = dataAccess.GetHosyouSyouhinUmu(strKbn, strBangou)

        Return strHosyouUmu
    End Function
#End Region

#Region "�����R�[�h�̔ԏ���"
    ''' <summary>
    ''' �����R�[�h�̔ԏ���
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' M<param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <returns>�̔Ԍ�̕����R�[�h</returns>
    ''' <remarks></remarks>
    Public Function getCntUpBunjouCd(ByVal sender As Object, ByVal strLoginUserId As String) As Integer
        Dim blnUpdate As Boolean
        Dim saibanDtAcc As New SaibanDataAccess
        Dim intBunjouCd As Integer

        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))


                '�����R�[�h���X�V
                blnUpdate = saibanDtAcc.updCntUpLastNo(EarthConst.SAIBAN_SYUBETU_BUNJOU_CD, strLoginUserId)

                If blnUpdate Then
                    intBunjouCd = saibanDtAcc.getSaibanRecord(EarthConst.SAIBAN_SYUBETU_BUNJOU_CD)
                    ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                    scope.Complete()
                Else
                    intBunjouCd = Integer.MinValue
                    mLogic.AlertMessage(sender, Messages.MSG019E.Replace("@PARAM1", "�����R�[�h�̍̔�"))
                    Return intBunjouCd
                End If

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return intBunjouCd
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return intBunjouCd
        End Try

        Return intBunjouCd
    End Function
#End Region

#Region "���ʑΉ��f�t�H���g�o�^����"
    ''' <summary>
    ''' ���ʑΉ��f�t�H���g�o�^����[��ʗp]�F�g�����U�N�V���������s
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strTysHouhouNo">�������@NO</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="dtUpdDatetime">�X�V����</param>
    ''' <param name="strRentouBukkenSuu">�A��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouUI(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strSyouhinCd As String, _
                                        ByVal strTysHouhouNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        ' �����X���i�������@���ʑΉ��}�X�^�擾�p�̃��W�b�N�N���X
        Dim ttLogic As New TokubetuTaiouLogic
        ' �����X���i�������@���ʑΉ��}�X�^�i�[�p�̃��X�g
        Dim listKtRec As New List(Of KameiTokubetuTaiouRecord)

        ' �����X���i�������@���ʑΉ��}�X�^�̎擾
        Dim total_count As Integer = 0 '�擾����
        listKtRec = ttLogic.GetDefaultTokubetuTaiouInfo(sender, strKameitenCd, strSyouhinCd, strTysHouhouNo, total_count)
        If total_count = -1 Then
            Return False
        ElseIf total_count = 0 Then
            Return True
        End If

        Dim intRentouBukkenSuu As Integer = 1 '�A��������
        Dim intSyoriKensuu As Integer = 0 '��������

        ' �ԍ�(�ۏ؏�NO) ��Ɨp
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '�ԍ�+��������
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

        '�Y����������X���i�������@���ʑΉ��}�X�^�����݂����ꍇ
        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' ���ʑΉ��e�[�u���̒ǉ�
                If Not listKtRec Is Nothing AndAlso listKtRec.Count > 0 Then
                    ' �A���������`�F�b�N
                    If strRentouBukkenSuu <> String.Empty Then
                        intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                        If intRentouBukkenSuu < 1 Then
                            intRentouBukkenSuu = 1
                        End If
                    End If

                    '�A�����������s�Ȃ�
                    For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                        '�V�K�ǉ��p�X�N���v�g����ю��s
                        If ttLogic.InsertTokubetuTaiou(sender, listKtRec, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                            Return False
                        End If

                        '*************************
                        ' �A�������Ή�
                        '*************************
                        '�ԍ����J�E���g�A�b�v
                        intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                        strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g
                    Next
                End If

                ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

    ''' <summary>
    ''' ���ʑΉ��f�t�H���g�o�^����[��ʗp](�V�K���p���̂�)�F�g�����U�N�V���������s
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNoPre">�ۏ؏�No(�O��o�^��)</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�No(����o�^��)</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="dtUpdDatetime">>�X�V����</param>
    ''' <param name="strRentouBukkenSuu">�A��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouUIHikitugi(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNoPre As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        Dim listRes As New List(Of TokubetuTaiouRecordBase)
        Dim ttLogic As New TokubetuTaiouLogic
        Dim intTotalCnt As Integer = 0      '���ʑΉ����R�[�h�J�E���g
        Dim intRentouBukkenSuu As Integer = 1 '�A��������
        Dim intSyoriKensuu As Integer = 0 '��������

        '���ʑΉ����X�g(�g�����E1�������Ώ�)
        listRes = ttLogic.GetTokubetuTaiouDataInfo(sender, strKbn, strHosyousyoNoPre, String.Empty, intTotalCnt)

        ' �������ʃ[�����̏ꍇ
        If intTotalCnt < 0 Then
            '�������ʌ�����-1�̏ꍇ�A�G���[�Ȃ̂ŁA�����I��
            Return False
        ElseIf intTotalCnt = 0 Then
            '������0���̏ꍇ�́A���p�����ʑΉ��������̂ŁA����I��
            Return True
        End If

        ' �ԍ�(�ۏ؏�NO) ��Ɨp
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '�ԍ�+��������
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

        Try
            '�g�����U�N�V����������s��
            Using scope As TransactionScope = New TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(EarthConst.TRAN_TIMEOUT))

                ' ���ʑΉ��e�[�u���̒ǉ�
                If Not listRes Is Nothing AndAlso listRes.Count > 0 Then
                    ' �A���������`�F�b�N
                    If strRentouBukkenSuu <> String.Empty Then
                        intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                        If intRentouBukkenSuu < 1 Then
                            intRentouBukkenSuu = 1
                        End If
                    End If

                    '�A�����������s�Ȃ�
                    For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                        '�V�K�ǉ��p�X�N���v�g����ю��s
                        If ttLogic.InsertTokubetuTaiou(sender, listRes, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                            Return False
                        End If

                        '*************************
                        ' �A�������Ή�
                        '*************************
                        '�ԍ����J�E���g�A�b�v
                        intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                        strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g
                    Next
                End If

                ' �X�V�ɐ��������ꍇ�A�g�����U�N�V�������R�~�b�g����
                scope.Complete()

            End Using

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' ���ʑΉ��f�t�H���g�o�^����[���W�b�N�p]�F�g�����U�N�V�������s��
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strHosyousyoNo">�ۏ؏�NO</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="strTysHouhouNo">�������@NO</param>
    ''' <param name="strLoginUserId">���O�C�����[�U�[ID</param>
    ''' <param name="dtUpdDatetime">�X�V����</param>
    ''' <param name="strRentouBukkenSuu">�A��������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertTokubetuTaiouLogic(ByVal sender As Object, _
                                        ByVal strKbn As String, _
                                        ByVal strHosyousyoNo As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strSyouhinCd As String, _
                                        ByVal strTysHouhouNo As String, _
                                        ByVal strLoginUserId As String, _
                                        ByVal dtUpdDatetime As DateTime, _
                                        ByVal strRentouBukkenSuu As String) As Boolean

        ' �����X���i�������@���ʑΉ��}�X�^�擾�p�̃��W�b�N�N���X
        Dim ttLogic As New TokubetuTaiouLogic
        ' �����X���i�������@���ʑΉ��}�X�^�i�[�p�̃��X�g
        Dim listKtRec As New List(Of KameiTokubetuTaiouRecord)

        ' �����X���i�������@���ʑΉ��}�X�^�̎擾
        Dim total_count As Integer = 0 '�擾����
        listKtRec = ttLogic.GetDefaultTokubetuTaiouInfo(sender, strKameitenCd, strSyouhinCd, strTysHouhouNo, total_count)
        If total_count = -1 Then
            Return False
        ElseIf total_count = 0 Then
            Return True
        End If

        Dim intRentouBukkenSuu As Integer = 1 '�A��������
        Dim intSyoriKensuu As Integer = 0 '��������

        ' �ԍ�(�ۏ؏�NO) ��Ɨp
        Dim intTmpBangou As Integer = CInt(strHosyousyoNo) + intSyoriKensuu '�ԍ�+��������
        Dim strTmpBangou As String = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g

        '�Y����������X���i�������@���ʑΉ��}�X�^�����݂����ꍇ
        Try
            ' ���ʑΉ��e�[�u���̒ǉ�
            If Not listKtRec Is Nothing AndAlso listKtRec.Count > 0 Then
                ' �A���������`�F�b�N
                If strRentouBukkenSuu <> String.Empty Then
                    intRentouBukkenSuu = CInt(strRentouBukkenSuu)
                    If intRentouBukkenSuu < 1 Then
                        intRentouBukkenSuu = 1
                    End If
                End If

                '�A�����������s�Ȃ�
                For intSyoriKensuu = 0 To intRentouBukkenSuu - 1
                    '�V�K�ǉ��p�X�N���v�g����ю��s
                    If ttLogic.InsertTokubetuTaiou(sender, listKtRec, strKbn, strTmpBangou, strLoginUserId, dtUpdDatetime) = False Then
                        Return False
                    End If

                    '*************************
                    ' �A�������Ή�
                    '*************************
                    '�ԍ����J�E���g�A�b�v
                    intTmpBangou = CInt(strTmpBangou) + 1 '�J�E���^
                    strTmpBangou = Format(intTmpBangou, "0000000000") '�t�H�[�}�b�g
                Next
            End If

        Catch exSqlException As System.Data.SqlClient.SqlException
            If exSqlException.Number = 1205 Then    '�G���[�L���b�`�F�f�b�h���b�N
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            ElseIf exSqlException.Number = -2 Then  '�G���[�L���b�`�F�^�C���A�E�g
                mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exSqlException.Number), 0, "SqlException")
            Else
                Throw exSqlException
            End If
            UnTrappedExceptionManager.Publish(exSqlException)
            Return False
        Catch exTransactionException As System.Transactions.TransactionException
            '�G���[�L���b�`�F�g�����U�N�V�����G���[
            UnTrappedExceptionManager.Publish(exTransactionException)
            mLogic.AlertMessage(sender, String.Format(Messages.MSG107E, exTransactionException.ToString), 0, "TransactionException")
            Return False
        End Try

        Return True
    End Function

#End Region

#Region "�������@�}�X�^���R�[�h�擾"

    ''' <summary>
    ''' �������@�}�X�^���A��L�[�ɕR�t�����R�[�h���擾����
    ''' </summary>
    ''' <param name="intTysHouhouNo">�������@NO</param>
    ''' <returns>Key�l�ɕR�t�����R�[�h</returns>
    ''' <remarks></remarks>
    Public Function getTyousahouhouRecord(ByVal intTysHouhouNo As Integer) As TyousahouhouRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getTyousahouhouRecord", intTysHouhouNo)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New TyousahouhouDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New TyousahouhouRecord

        dTblResult = clsDataAcc.getTyousahouhouRecord(intTysHouhouNo)

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of TyousahouhouRecord)(GetType(TyousahouhouRecord), dTblResult)(0)
        End If

        Return recResult

    End Function


#End Region

#Region "�敪�}�X�^���R�[�h�擾"

    ''' <summary>
    ''' �敪�}�X�^���A��L�[�ɕR�t�����R�[�h���擾����
    ''' </summary>
    ''' <param name="strKubun">�敪</param>
    ''' <returns>Key�l�ɕR�t�����R�[�h</returns>
    ''' <remarks></remarks>
    Public Function getKubunRecord(ByVal strKubun As String) As KubunRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord", strKubun)

        '�f�[�^�A�N�Z�X�N���X
        Dim clsDataAcc As New KubunDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New KubunRecord

        If strKubun <> Nothing Then
            dTblResult = clsDataAcc.getKubunRecord(strKubun)
        End If

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of KubunRecord)(GetType(KubunRecord), dTblResult)(0)
        End If

        Return recResult

    End Function

#End Region

#Region "���i�}�X�^���R�[�h�擾"

    ''' <summary>
    ''' ���i�}�X�^���A��L�[�ɕR�t�����R�[�h���擾����
    ''' </summary>
    ''' <param name="strSyouhinCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSyouhinRecord(ByVal strSyouhinCd As String) As SyouhinMeisaiRecord

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".getKubunRecord", strSyouhinCd)

        '���iDA�N���X
        Dim daSyouhin As New SyouhinDataAccess
        '�������ʊi�[�p�f�[�^�e�[�u��
        Dim dTblResult As New DataTable
        '�������ʊi�[�p���R�[�h���X�g
        Dim recResult As New SyouhinMeisaiRecord

        If strSyouhinCd <> Nothing Then
            dTblResult = daSyouhin.GetSyouhinInfo(strSyouhinCd)
        End If

        If dTblResult.Rows.Count > 0 Then
            ' �������ʂ��i�[�p���X�g�ɃZ�b�g
            recResult = DataMappingHelper.Instance.getMapArray(Of SyouhinMeisaiRecord)(GetType(SyouhinMeisaiRecord), dTblResult)(0)
        End If

        Return recResult
    End Function


#End Region

End Class
