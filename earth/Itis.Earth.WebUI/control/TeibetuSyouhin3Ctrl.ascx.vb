
Partial Public Class TeibetuSyouhin3Ctrl
    Inherits System.Web.UI.UserControl

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ' �X�^�C���w�蔻��p
    Private flgColor As Boolean
    Dim cbLogic As New CommonBizLogic

#Region "�v���p�e�B"

#Region "���i�R�[�h�R�̓@�ʐ������R�[�hDictionary"
    ''' <summary>
    ''' ���i�R�[�h�R�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Private htbSyouhin3Records As Dictionary(Of Integer, TeibetuSeikyuuRecord)
    ''' <summary>
    ''' ���i�R�[�h�R�̓@�ʐ������R�[�hDictionary
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��ʕ\��NO��Key�Ƃ������i�R�[�h�R�̓@�ʐ������R�[�h���X�g</returns>
    ''' <remarks>TeibetuSeikyuuRecord��Dictionary�ł��鎖</remarks>
    Public Property Syouhin3Records() As Dictionary(Of Integer, TeibetuSeikyuuRecord)
        Get
            ' �X�V�p��Dictionary���Đ�������
            htbSyouhin3Records = New Dictionary(Of Integer, TeibetuSeikyuuRecord)

            ' ���i�R�|�P
            If Not Syouhin3Record01.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(1, Syouhin3Record01.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�Q
            If Not Syouhin3Record02.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(2, Syouhin3Record02.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�R
            If Not Syouhin3Record03.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(3, Syouhin3Record03.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�S
            If Not Syouhin3Record04.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(4, Syouhin3Record04.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�T
            If Not Syouhin3Record05.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(5, Syouhin3Record05.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�U
            If Not Syouhin3Record06.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(6, Syouhin3Record06.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�V
            If Not Syouhin3Record07.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(7, Syouhin3Record07.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�W
            If Not Syouhin3Record08.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(8, Syouhin3Record08.TeibetuSeikyuuRec)
            End If

            ' ���i�R�|�X
            If Not Syouhin3Record09.TeibetuSeikyuuRec Is Nothing Then
                htbSyouhin3Records.Add(9, Syouhin3Record09.TeibetuSeikyuuRec)
            End If

            Return htbSyouhin3Records
        End Get
        Set(ByVal value As Dictionary(Of Integer, TeibetuSeikyuuRecord))
            htbSyouhin3Records = value
            ' �f�[�^���R���g���[���ɐݒ�
            SetCtrlData(htbSyouhin3Records)
        End Set
    End Property
#End Region

#Region "�@�ʃf�[�^���ʐݒ���"
    ''' <summary>
    ''' �@�ʃf�[�^���ʐݒ���
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SettingInfo() As TeibetuSettingInfoRecord
        Get
            Dim info As New TeibetuSettingInfoRecord
            info.Kubun = HiddenKubun.Value                                                  ' �敪
            info.Bangou = HiddenBangou.Value                                                ' �ԍ��i�ۏ؏�NO�j
            info.UpdLoginUserId = HiddenLoginUserId.Value                                   ' ���O�C�����[�U�[ID
            info.KeiriGyoumuKengen = Integer.Parse(HiddenKeiriGyoumuKengen.Value)           ' �o���Ɩ�����
            info.HattyuusyoKanriKengen = Integer.Parse(HiddenHattyuusyoKanriKengen.Value)   ' �������Ǘ�����
            Return info
        End Get
        Set(ByVal value As TeibetuSettingInfoRecord)
            HiddenKubun.Value = value.Kubun                                 ' �敪
            HiddenBangou.Value = value.Bangou                               ' �ԍ��i�ۏ؏�NO�j
            HiddenLoginUserId.Value = value.UpdLoginUserId                  ' ���O�C�����[�U�[ID
            ' �o���Ɩ�����
            HiddenKeiriGyoumuKengen.Value = value.KeiriGyoumuKengen.ToString()
            ' �������Ǘ�����
            HiddenHattyuusyoKanriKengen.Value = value.HattyuusyoKanriKengen.ToString()
        End Set
    End Property
#End Region

#Region "�����^�C�v"
    ''' <summary>
    ''' �����^�C�v
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SeikyuuType() As String
        Get
            Return HiddenSeikyuuType.Value
        End Get
        Set(ByVal value As String)
            HiddenSeikyuuType.Value = value
        End Set
    End Property
#End Region

#Region "�n��R�[�h"
    ''' <summary>
    ''' �n��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiretuCd() As String
        Get
            Return HiddenKeiretuCd.Value
        End Get
        Set(ByVal value As String)
            HiddenKeiretuCd.Value = value
        End Set
    End Property
#End Region

#Region "��������UpdatePanel"
    ''' <summary>
    ''' ��������UpdatePanel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateSeikyuusakiPanel() As UpdatePanel
        Get
            Return UpdatePanelIraiInfo
        End Get
        Set(ByVal value As UpdatePanel)
            UpdatePanelIraiInfo = value
        End Set
    End Property
#End Region

#Region "�ō����z"
    ''' <summary>
    ''' �ō����z
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiKingaku() As Integer
        Get
            Dim zeikomi As Integer = 0

            If Not Syouhin3Record01 Is Nothing Then
                zeikomi = Syouhin3Record01.ZeikomiKingaku
            End If
            If Not Syouhin3Record02 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record02.ZeikomiKingaku
            End If
            If Not Syouhin3Record03 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record03.ZeikomiKingaku
            End If
            If Not Syouhin3Record04 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record04.ZeikomiKingaku
            End If
            If Not Syouhin3Record05 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record05.ZeikomiKingaku
            End If
            If Not Syouhin3Record06 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record06.ZeikomiKingaku
            End If
            If Not Syouhin3Record07 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record07.ZeikomiKingaku
            End If
            If Not Syouhin3Record08 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record08.ZeikomiKingaku
            End If
            If Not Syouhin3Record09 Is Nothing Then
                zeikomi = zeikomi + Syouhin3Record09.ZeikomiKingaku
            End If

            Return zeikomi
        End Get
    End Property
#End Region

#Region "�������@"
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <remarks></remarks>
    Private _tysHouhou As String
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property tysHouhou() As String
        Get
            Return _tysHouhou
        End Get
        Set(ByVal value As String)
            _tysHouhou = value
        End Set
    End Property
#End Region

#End Region

#Region "�C�x���g"
    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i������E�d������ݒ�p�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuSiireSakiAction(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�����^�C�v�ݒ�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v�i���ڐ���/�������j</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Event SetSeikyuuTypeAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByVal strId As String _
                                        , ByVal strSeikyuuSakiTypeStr As String _
                                        , ByVal strKeiretuCd As String _
                                        , ByVal strKameitenCd As String)

    ''' <summary>
    ''' �e��ʂ̏������s�p�J�X�^���C�x���g�i�������@�ݒ�j
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">�������@No</param>
    ''' <remarks></remarks>
    Public Event SetTysHouhouAction(ByVal sender As System.Object _
                                        , ByVal e As System.EventArgs _
                                        , ByRef TyousaHouhouNo As Integer)

    ''' <summary>
    ''' �y�[�W���[�h���̃C�x���g�n���h��
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "��������ݒ�"
    ''' <summary>
    ''' ���i3-1���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei31(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record01.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("1")
    End Sub

    ''' <summary>
    ''' ���i3-2���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei32(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record02.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("2")
    End Sub

    ''' <summary>
    ''' ���i3-3���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei33(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record03.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("3")
    End Sub

    ''' <summary>
    ''' ���i3-4���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei34(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record04.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("4")
    End Sub

    ''' <summary>
    ''' ���i3-5���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei35(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record05.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("5")
    End Sub

    ''' <summary>
    ''' ���i3-6���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei36(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record06.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("6")
    End Sub

    ''' <summary>
    ''' ���i3-7���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei37(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record07.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("7")
    End Sub

    ''' <summary>
    ''' ���i3-8���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei38(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record08.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("8")
    End Sub

    ''' <summary>
    ''' ���i3-9���R�[�h�ɐ��������ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ExecSeikyuuInfoSettei39(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs) Handles Syouhin3Record09.GetSeikyuuInfo
        ' ������������i�R�R���g���[���֐ݒ肷��
        SetSeikyuuInfo("9")
    End Sub

#End Region

    ''' <summary>
    ''' �ō����z�̕ύX��e�R���g���[���ɒʒm����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeZeikomiGaku(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByVal zeikomigaku As Integer)

#Region "�ō����z�ύX���̃C�x���g�Q"
    ''' <summary>
    ''' ���i�R�|�P�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin31(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record01.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�Q�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin32(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record02.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�R�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin33(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record03.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�S�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin34(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record04.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�T�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin35(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record05.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�U�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin36(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record06.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�V�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin37(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record07.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�W�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin38(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record08.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

    ''' <summary>
    ''' ���i�R�|�X�ō����z�ύX���̃C�x���g
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeZeikomiSyouhin39(ByVal sender As System.Object, _
                                      ByVal e As System.EventArgs, _
                                      ByVal zeikomigaku As Integer) Handles Syouhin3Record09.ChangeZeikomiGaku
        RaiseEvent ChangeZeikomiGaku(Me, e, ZeikomiKingaku)
    End Sub

#End Region
#End Region

#Region "�v���C�x�[�g���\�b�h"

    ''' <summary>
    ''' ���������ݒ肷��
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuInfo(ByVal rowNo As String)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl

        teibetuRecCtrl = FindControl("Syouhin3Record0" & rowNo)

        ' ������������i�R���g���[���֐ݒ肷��
        teibetuRecCtrl.SeikyuuType = SeikyuuType
        teibetuRecCtrl.KeiretuCd = KeiretuCd
        teibetuRecCtrl.UpdateSyouhinPanel.Update()

    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">�J�X�^���C�x���g������ID</param>
    ''' <param name="strSeikyuuSakiTypeStr">������^�C�v(���ڐ���/������)</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuType(ByVal sender As System.Object _
                                    , ByVal e As System.EventArgs _
                                    , ByVal strId As String _
                                    , ByVal strSeikyuuSakiTypeStr As String _
                                    , ByVal strKeiretuCd As String _
                                    , ByVal strKameitenCd As String) Handles Syouhin3Record01.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record02.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record03.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record04.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record05.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record06.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record07.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record08.SetSeikyuuTypeAction _
                                                                            , Syouhin3Record09.SetSeikyuuTypeAction

        '�����^�C�v�̐ݒ�
        RaiseEvent SetSeikyuuTypeAction(sender _
                                        , e _
                                        , strId _
                                        , strSeikyuuSakiTypeStr _
                                        , strKeiretuCd _
                                        , strKameitenCd)
    End Sub

    ''' <summary>
    ''' �˗��R���g���[���̒������@���v���p�e�B�ɐݒ�
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="TyousaHouhouNo">�������@No</param>
    ''' <remarks></remarks>
    Private Sub SetTysHouhou(ByVal sender As System.Object, _
                                   ByVal e As System.EventArgs, _
                                   ByRef TyousaHouhouNo As Integer) Handles Syouhin3Record01.SetTysHouhouAction _
                                                                            , Syouhin3Record02.SetTysHouhouAction _
                                                                            , Syouhin3Record03.SetTysHouhouAction _
                                                                            , Syouhin3Record04.SetTysHouhouAction _
                                                                            , Syouhin3Record05.SetTysHouhouAction _
                                                                            , Syouhin3Record06.SetTysHouhouAction _
                                                                            , Syouhin3Record07.SetTysHouhouAction _
                                                                            , Syouhin3Record08.SetTysHouhouAction _
                                                                            , Syouhin3Record09.SetTysHouhouAction
        RaiseEvent SetTysHouhouAction(sender, e, TyousaHouhouNo)
    End Sub

    ''' <summary>
    ''' �擾�f�[�^���R���g���[���ɐݒ肵�܂�
    ''' </summary>
    ''' <param name="dicTeibetuRec"></param>
    ''' <remarks></remarks>
    Private Sub SetCtrlData(ByVal dicTeibetuRec As Dictionary(Of Integer, TeibetuSeikyuuRecord))

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetCtrlData", _
                                            dicTeibetuRec)

        Dim i As Integer
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim tableRow As HtmlTableRow

        flgColor = True

        ' �R���g���[���̐����������s��
        For i = 1 To 9
            teibetuRecCtrl = FindControl("Syouhin3Record0" & i.ToString())
            tableRow = FindControl("trSyouhin3Record0" & i.ToString())

            ' ���i�R�̐ݒ�
            EditRecord(teibetuRecCtrl, i, tableRow)
        Next

    End Sub

    ''' <summary>
    ''' �@�ʐ������R�[�h�̃f�[�^���R���g���[���ɐݒ肷��
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <param name="idx"></param>
    ''' <remarks></remarks>
    Private Sub EditRecord(ByVal ctrl As TeibetuSyouhinRecordCtrl, _
                           ByVal idx As Integer, _
                           ByVal tr As HtmlTableRow)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".EditRecord", _
                                            ctrl, idx)

        If Not htbSyouhin3Records Is Nothing Then
            ' �f�[�^�̐ݒ�
            If htbSyouhin3Records.ContainsKey(idx) Then

                ' KEY�͐ݒ肷��
                ctrl.SettingInfo = Me.SettingInfo

                ctrl.TeibetuSeikyuuRec = htbSyouhin3Records.Item(idx)

            End If
        End If

        ' �\���ݒ�
        If ctrl.TeibetuSeikyuuRec Is Nothing Then
            ' ��\���ɂ���
            ctrl.Visible = False
            tr.Visible = False
        Else
            ' �X�^�C���V�[�g��ݒ肷��
            ctrl.CssName = IIf(flgColor, "odd", "even")
            flgColor = Not flgColor
        End If

    End Sub

    ''' <summary>
    ''' ���[�U�[�R���g���[���Őݒ肵��������E�d���������ʂ̉B�����ڂɔ��f����
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <param name="strId">���ID</param>
    ''' <remarks></remarks>
    Private Sub SetSeikyuuSiireInfo(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal strId As String) Handles Syouhin3Record01.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record02.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record03.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record04.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record05.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record06.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record07.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record08.SetSeikyuuSiireSakiAction _
                                                                                                                                , Syouhin3Record09.SetSeikyuuSiireSakiAction _
        '�����d���p�J�X�^���C�x���g
        RaiseEvent SetSeikyuuSiireSakiAction(sender, e, strId)
    End Sub
#End Region

#Region "�p�u���b�N���\�b�h"
    ''' <summary>
    ''' �G���[�`�F�b�N
    ''' </summary>
    ''' <param name="errMess"></param>
    ''' <param name="arrFocusTargetCtrl"></param>
    ''' <param name="typeWord"></param>
    ''' <param name="denpyouNgList"></param>
    ''' <param name="denpyouErrMess"></param>
    ''' <param name="seikyuuUmuErrMess"></param>
    ''' <param name="strChgPartMess"></param>
    ''' <remarks></remarks>
    Public Sub CheckInput(ByRef errMess As String, _
                          ByRef arrFocusTargetCtrl As List(Of Control), _
                          ByVal typeWord As String, _
                          ByVal denpyouNgList As String, _
                          ByRef denpyouErrMess As String, _
                          ByRef seikyuuUmuErrMess As String, _
                          ByRef strChgPartMess As String _
                          )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CheckKinsoku", _
                                                    errMess, _
                                                    arrFocusTargetCtrl, _
                                                    typeWord, _
                                                    denpyouNgList, _
                                                    denpyouErrMess, _
                                                    seikyuuUmuErrMess, _
                                                    strChgPartMess _
                                                    )

        Dim setuzoku As String = typeWord
        If typeWord <> "" Then
            setuzoku = typeWord & "_"
        End If

        ' ���i�R�|�P�̃G���[�`�F�b�N
        Syouhin3Record01.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�P", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�Q�̃G���[�`�F�b�N
        Syouhin3Record02.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�Q", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�R�̃G���[�`�F�b�N
        Syouhin3Record03.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�R", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�S�̃G���[�`�F�b�N
        Syouhin3Record04.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�S", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�T�̃G���[�`�F�b�N
        Syouhin3Record05.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�T", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�U�̃G���[�`�F�b�N
        Syouhin3Record06.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�U", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�V�̃G���[�`�F�b�N
        Syouhin3Record07.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�V", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�W�̃G���[�`�F�b�N
        Syouhin3Record08.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�W", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

        ' ���i�R�|�X�̃G���[�`�F�b�N
        Syouhin3Record09.CheckInput(errMess, arrFocusTargetCtrl, setuzoku & "�X", denpyouNgList, denpyouErrMess, seikyuuUmuErrMess, strChgPartMess)

    End Sub

    Private Const SYOUHIN3_RECORDS_ID = "Syouhin3Record0"
    ''' <summary>
    ''' ��{������E�d������̐ݒ�
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strTysKaisyaCd">������ЃR�[�h</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strCtrlId">�w�胆�[�U�[�R���g���[��ID</param>
    ''' <remarks></remarks>
    Public Sub SetDefaultSeikyuuSiireSakiInfo(ByVal strKameitenCd As String _
                                            , ByVal strTysKaisyaCd As String _
                                            , ByVal strKeiretuCd As String _
                                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin3Record01, Syouhin3Record02, Syouhin3Record03, Syouhin3Record04, Syouhin3Record05 _
                                                        , Syouhin3Record06, Syouhin3Record07, Syouhin3Record08, Syouhin3Record09}
        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer
            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN3_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetDefaultSeikyuuSiireSakiInfo(strKameitenCd, strTysKaisyaCd, strKeiretuCd)
            Next
        End If
    End Sub

    ''' <summary>
    ''' �����^�C�v�̐ݒ�
    ''' </summary>
    ''' <param name="enSeikyuuType">�����^�C�v</param>
    ''' <param name="strKeiretuCd">�n��R�[�h</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <remarks></remarks>
    Public Sub SetSeikyuuType(ByVal enSeikyuuType As EarthEnum.EnumSeikyuuType _
                            , ByVal strKeiretuCd As String _
                            , ByVal strKameitenCd As String _
                            , Optional ByVal strCtrlId As String = "")
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin3Record01, Syouhin3Record02, Syouhin3Record03, Syouhin3Record04, Syouhin3Record05 _
                                                        , Syouhin3Record06, Syouhin3Record07, Syouhin3Record08, Syouhin3Record09}

        If strCtrlId <> String.Empty Then
            Dim intIndex As Integer
            intIndex = Integer.Parse(strCtrlId.Replace(Me.ClientID & Me.ClientIDSeparator & SYOUHIN3_RECORDS_ID, String.Empty)) - 1
            syouhinCtrl(intIndex).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
        Else
            For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
                syouhinCtrl(intCnt).SetSeikyuuType(enSeikyuuType, strKeiretuCd, strKameitenCd)
            Next
        End If
    End Sub

    ''' <summary>
    '''���i���̐����E�d���悪�ύX����Ă��Ȃ������`�F�b�N���A
    '''�ύX����Ă���ꍇ���̍Ď擾
    ''' </summary>
    ''' <remarks></remarks>
    Public Function setSeikyuuSiireHenkou(ByVal sender As Object, ByVal e As System.EventArgs) As Boolean
        Dim syouhinCtrl() As TeibetuSyouhinRecordCtrl = {Syouhin3Record01, Syouhin3Record02, Syouhin3Record03, Syouhin3Record04, Syouhin3Record05 _
                                                        , Syouhin3Record06, Syouhin3Record07, Syouhin3Record08, Syouhin3Record09}

        For intCnt As Integer = LBound(syouhinCtrl) To UBound(syouhinCtrl)
            If syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = "1" Then
                '���i�Q�C�R�����{�^���������̏��������s
                syouhinCtrl(intCnt).SetSyouhin23(sender, e)
                syouhinCtrl(intCnt).AccSeikyuuSiireLink.AccHiddenChkSeikyuuSakiChg.Value = String.Empty
                '�ύX���ꂽ���i���L�����ꍇ�A���[�v�I��(�����Ƃ��āA1���i�������ύX����Ȃ�����)
                Return True
                Exit Function
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' ���ʑΉ��c�[���`�b�v�ɓ��ʑΉ��R�[�h��ݒ肷��
    ''' </summary>
    ''' <param name="strTokubetuTaiouCd">���ʑΉ��R�[�h</param>
    ''' <param name="rowNo">��ʕ\��NO</param>
    ''' <param name="ttRec">���ʑΉ����R�[�h�N���X</param>
    ''' <remarks></remarks>
    Public Sub SetTokubetuTaiouToolTip(ByVal strTokubetuTaiouCd As String, ByVal rowNo As String, ByVal ttRec As TokubetuTaiouRecordBase)
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim emType As EarthEnum.emToolTipType           '�c�[���`�b�v�\���^�C�v

        '�Ώۂ̏��i3�̍s���擾
        teibetuRecCtrl = FindControl("Syouhin3Record0" & rowNo)

        '�c�[���`�b�v�ݒ�Ώۂ��`�F�b�N
        emType = cbLogic.checkToolTipSetValue(Me, ttRec, teibetuRecCtrl.BunruiCd, teibetuRecCtrl.GamenHyoujiNo, teibetuRecCtrl.AccUriageSyori.SelectedValue)
        If emType <> EarthEnum.emToolTipType.NASI Then
            '�\���p
            teibetuRecCtrl.AccTokubetuTaiouToolTip.SetDisplayCd(strTokubetuTaiouCd)

            '���ʑΉ��R�[�h�Ώ�Hdn�Ɋi�[(�o�^�p)
            If teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = strTokubetuTaiouCd
            Else
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value &= EarthConst.SEP_STRING & strTokubetuTaiouCd
            End If

            If emType = EarthEnum.emToolTipType.SYUSEI Then
                teibetuRecCtrl.AccTokubetuTaiouToolTip.AcclblTokubetuTaiou.Text = EarthConst.SYUU_TOOL_TIP
            End If
        End If

    End Sub

    ''' <summary>
    ''' ���ʑΉ��f�[�^�X�V�t���O������������
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearTokubetuTaiouUpdFlg()
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer

        ' �R���g���[���̐����������s��
        For i = 1 To EarthConst.SYOUHIN3_COUNT
            teibetuRecCtrl = FindControl("Syouhin3Record0" & i.ToString())
            teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.NASI_VAL
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value = String.Empty
            teibetuRecCtrl.AccTokubetuTaiouToolTip.AccDisplayCd.Value = String.Empty
        Next
    End Sub

    ''' <summary>
    ''' �c�[���`�b�v������ʑΉ��R�[�h���擾����
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCdFromToolTip() As String
        Dim teibetuRecCtrl As TeibetuSyouhinRecordCtrl
        Dim i As Integer
        Dim strTmp As String
        Dim strTokubetuTaiouCd As String = String.Empty

        ' �R���g���[���̐����������s��
        For i = 1 To EarthConst.SYOUHIN3_COUNT
            teibetuRecCtrl = FindControl("Syouhin3Record0" & i.ToString())

            '���ʑΉ��f�[�^�X�V�t���O=1�̏ꍇ
            If teibetuRecCtrl.AccTokubetuTaiouUpdFlg.Value = EarthConst.ARI_VAL Then
                strTmp = teibetuRecCtrl.AccTokubetuTaiouToolTip.AccTaisyouCd.Value
                If strTmp <> String.Empty Then
                    If strTokubetuTaiouCd = String.Empty Then
                        strTokubetuTaiouCd = strTmp
                    Else
                        strTokubetuTaiouCd &= EarthConst.SEP_STRING & strTmp
                    End If
                End If
            End If
        Next

        Return strTokubetuTaiouCd
    End Function

#End Region
End Class