Imports System.Web.UI.WebControls


''' <summary>
''' �h���b�v�_�E�����X�g�����w���p�[�N���X
''' </summary>
''' <remarks></remarks>
Public Class DropDownHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�h���b�v�_�E�����X�g�̍쐬���"
    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̍쐬���
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DropDownType As Long
        ''' <summary>
        ''' �敪
        ''' </summary>
        ''' <remarks></remarks>
        Kubun = 0
        ''' <summary>
        ''' �����T�v
        ''' </summary>
        ''' <remarks></remarks>
        TyousaGaiyou = 1
        ''' <summary>
        ''' �������@
        ''' </summary>
        ''' <remarks></remarks>
        TyousaHouhou = 2
        ''' <summary>
        ''' �K�w
        ''' </summary>
        ''' <remarks></remarks>
        Kaisou = 3
        ''' <summary>
        ''' �\��
        ''' </summary>
        ''' <remarks></remarks>
        Kouzou = 4
        ''' <summary>
        ''' �V�z����
        ''' </summary>
        ''' <remarks></remarks>
        ShintikuTatekae = 5
        ''' <summary>
        ''' �Ԍ�
        ''' </summary>
        ''' <remarks></remarks>
        Syako = 6
        ''' <summary>
        ''' �����p�r
        ''' </summary>
        ''' <remarks></remarks>
        TatemonoYouto = 7
        ''' <summary>
        ''' �\���b
        ''' </summary>
        ''' <remarks></remarks>
        YoteiKiso = 8
        ''' <summary>
        ''' �ް��j�����
        ''' </summary>
        ''' <remarks></remarks>
        DataHaki = 9
        ''' <summary>
        ''' �o�R
        ''' </summary>
        ''' <remarks></remarks>
        Keiyu = 10
        ''' <summary>
        ''' �敪2�i�ۏ؏�NO�N���t�^�Łj
        ''' </summary>
        ''' <remarks></remarks>
        Kubun2 = 11
        ''' <summary>
        ''' �ۏ؏����s��
        ''' </summary>
        ''' <remarks></remarks>
        HosyousyoHakJyky = 12
        ''' <summary>
        ''' �ی����
        ''' </summary>
        ''' <remarks></remarks>
        HokenKaisya = 13
        ''' <summary>
        ''' �����
        ''' </summary>
        ''' <remarks></remarks>
        Syouhizei = 14
        ''' <summary>
        ''' �s���{��
        ''' </summary>
        ''' <remarks></remarks>
        Todoufuken = 15
        ''' <summary>
        ''' ���ǍH�����
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKoujiSyubetu = 26
        ''' <summary>
        ''' ��
        ''' </summary>
        ''' <remarks></remarks>
        HkksJuri = 27
        ''' <summary>
        ''' �S����(���/�H��)
        ''' </summary>
        ''' <remarks></remarks>
        Tantousya = 28
        ''' <summary>
        ''' ��b�d�l�ڑ���
        ''' </summary>
        ''' <remarks></remarks>
        KsSiyouSetuzokusi = 29
        ''' <summary>
        ''' �n��R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        KeiretuCd = 30
        ''' <summary>
        ''' �o�^�����i
        ''' </summary>
        ''' <remarks></remarks>
        TourokuRyouSyouhin = 34
        ''' <summary>
        ''' �̑��i�����c�[����
        ''' </summary>
        ''' <remarks></remarks>
        ToolRYouSyouhin = 35
        ''' <summary>
        ''' FC�̑��i���i
        ''' </summary>
        ''' <remarks></remarks>
        FcSyouhin = 36
        ''' <summary>
        ''' FC�ȊO�̑��i���i
        ''' </summary>
        ''' <remarks></remarks>
        NotFcSyouhin = 37
        ''' <summary>
        ''' ���i(���ރR�[�h:100)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin1 = 38
        ''' <summary>
        ''' ���i(���ރR�[�h:110)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_110 = 39
        ''' <summary>
        ''' ���i(���ރR�[�h:115)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_115 = 40
        ''' <summary>
        ''' ���i(���ރR�[�h:120)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3 = 41
        ''' <summary>
        ''' ���i(���ރR�[�h:130)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKouji = 42
        ''' <summary>
        ''' ���i(���ރR�[�h:140)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinTuika = 43
        ''' <summary>
        ''' ���i(���ރR�[�h:150)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinTysHkSaihakkou = 44
        ''' <summary>
        ''' ���i(���ރR�[�h:160)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKoujiHkSaihakkou = 45
        ''' <summary>
        ''' ���i(���ރR�[�h:170)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinHosyousyoSaihakkou = 46
        ''' <summary>
        ''' ���i(���ރR�[�h:180)
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinKaiyakuHaraiModosi = 47
        ''' <summary>
        ''' ���i2(���ރR�[�h:110,115)
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2Group = 48
        ''' <summary>
        ''' �������
        ''' </summary>
        ''' <remarks></remarks>
        TyousaKaisya = 55
    End Enum
#End Region

    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̐ݒ���s���܂�<br/>
    ''' HtmlSelect�p
    ''' </summary>
    ''' <param name="dropdown">�ݒ肷����ۯ���޳�ؽ�</param>
    ''' <param name="type">�ݒ肷��f�[�^���</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="selected" >�����I��l���C���f�b�N�X�Ŏw�肷��(withSpaceRow=True�̏ꍇ�Ȃǎw��ɒ���)</param>
    ''' <returns>�h���b�v�_�E�����X�g�̃C���X�^���X</returns>
    ''' <remarks></remarks>
    Public Function SetDropDownList(ByRef dropdown As HtmlSelect, _
                                    ByVal type As DropDownType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̐ݒ���s���܂��j<br/>
    ''' DropDownList�p
    ''' </summary>
    ''' <param name="dropdown">�ݒ肷����ۯ���޳�ؽ�</param>
    ''' <param name="type">�ݒ肷��f�[�^���</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="selected" >�����I��l���C���f�b�N�X�Ŏw�肷��(withSpaceRow=True�̏ꍇ�Ȃǎw��ɒ���)</param>
    ''' <returns>�h���b�v�_�E�����X�g�̃C���X�^���X</returns>
    ''' <remarks></remarks>
    Public Function SetDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As DropDownType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̐ݒ���s���܂�(������M�̖��̎�ʐ�p)<br/>
    ''' DropDownList�p
    ''' </summary>
    ''' <param name="dropdown">�ݒ肷����ۯ���޳�ؽ�</param>
    ''' <param name="type">�ݒ肷��f�[�^��ށ�EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="selected" >�����I��l���C���f�b�N�X�Ŏw�肷��(withSpaceRow=True�̏ꍇ�Ȃǎw��ɒ���)</param>
    ''' <returns>�h���b�v�_�E�����X�g�̃C���X�^���X</returns>
    ''' <remarks></remarks>
    Public Function SetMeisyouDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As EarthConst.emMeisyouType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetMeisyouComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̐ݒ���s���܂�(���g������M��p)<br/>
    ''' DropDownList�p
    ''' </summary>
    ''' <param name="dropdown">�ݒ肷����ۯ���޳�ؽ�</param>
    ''' <param name="type">�ݒ肷��f�[�^��ށ�EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="selected" >�����I��l���C���f�b�N�X�Ŏw�肷��(withSpaceRow=True�̏ꍇ�Ȃǎw��ɒ���)</param>
    ''' <returns>�h���b�v�_�E�����X�g�̃C���X�^���X</returns>
    ''' <remarks></remarks>
    Public Function SetKtMeisyouDropDownList(ByRef dropdown As DropDownList, _
                                    ByVal type As EarthConst.emKtMeisyouType, _
                                    Optional ByVal withSpaceRow As Boolean = True, _
                                    Optional ByVal withCode As Boolean = True, _
                                    Optional ByVal selected As Integer = 0, _
                                    Optional ByVal blnTorikesi As Boolean = True _
                                    )

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetKtMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetKtMeisyouComboList(type, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

    ''' <summary>
    ''' �h���b�v�_�E�����X�g�̐ݒ���s���܂�(���g������M��p�A�\�����ڂ��p�����[�^�Ŏw��)<br/>
    ''' DropDownList�p
    ''' </summary>
    ''' <param name="dropdown">�ݒ肷����ۯ���޳�ؽ�</param>
    ''' <param name="type">�ݒ肷��f�[�^��ށ�EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="ktMeisyouType">�g������M�h���b�v�_�E���^�C�v</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�e�L�X�g��Value�l�{"�F"������ꍇ:true</param>
    ''' <param name="selected" >�����I��l���C���f�b�N�X�Ŏw�肷��(withSpaceRow=True�̏ꍇ�Ȃǎw��ɒ���)</param>
    ''' <returns>�h���b�v�_�E�����X�g�̃C���X�^���X</returns>
    ''' <remarks></remarks>
    Public Function SetKtMeisyouHannyouDropDownList(ByRef dropdown As DropDownList, _
                                                    ByVal type As EarthConst.emKtMeisyouType, _
                                                    ByVal ktMeisyouType As EarthEnum.emKtMeisyouType, _
                                                    Optional ByVal withSpaceRow As Boolean = True, _
                                                    Optional ByVal withCode As Boolean = True, _
                                                    Optional ByVal selected As Integer = 0, _
                                                    Optional ByVal blnTorikesi As Boolean = True _
                                                    )
        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".SetKtMeisyouDropDownList", _
                                                    dropdown, _
                                                    type, _
                                                    ktMeisyouType, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    selected, _
                                                    blnTorikesi)

        Dim dropdown_logic As New DropDownLogic
        dropdown.DataSource = dropdown_logic.GetKtMeisyouHannyouComboList(type, ktMeisyouType, withSpaceRow, withCode, blnTorikesi)
        dropdown.DataTextField = "CmbTextField"
        dropdown.DataValueField = "CmbValueField"
        dropdown.DataBind()
        dropdown.SelectedIndex = selected

        Return dropdown

    End Function

End Class
