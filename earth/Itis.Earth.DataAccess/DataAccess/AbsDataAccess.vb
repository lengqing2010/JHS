''' <summary>
''' �f�[�^�A�N�Z�X�N���X�p���ۃN���X�ł�
''' ��ɃR���{�{�b�N�X�f�[�^�̐ݒ�Ɏg�p����ׁA
''' �R���{�{�b�N�X�f�[�^�̍쐬�Ɋ֌W�Ȃ��ꍇ��Inherits�s�v
''' </summary>
''' <remarks>��ʕ\���p�̃f�[�^�J�����@"CmbTextField"<br/>
'''          Value�̃f�[�^�J�����@     "CmbValueField"</remarks>
Public Class AbsDataAccess

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̃f�[�^�e�[�u�����擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode, _
                                            blnTorikesi)

        ' ���N���X�ł͏������܂���

    End Sub

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̃f�[�^�e�[�u�����擾���܂�(������M.���̎�ʐ�p)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetMeisyouDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMeisyouDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        ' ���N���X�ł͏������܂���

    End Sub

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̃f�[�^�e�[�u�����擾���܂�(���g������M��p)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetKtMeisyouDropdownData(ByRef dt As DataTable, _
                                           ByVal withSpaceRow As Boolean, _
                                           Optional ByVal withCode As Boolean = True, _
                                           Optional ByVal blnTorikesi As Boolean = True)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouDropdownData", _
                                            dt, _
                                            withSpaceRow, _
                                            withCode)

        ' ���N���X�ł͏������܂���

    End Sub

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̃f�[�^�e�[�u�����擾���܂�(���g������M��p�A�\�����ڂ��p�����[�^�Ŏw��)
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="type">�g������M�h���b�v�_�E���^�C�v</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi">�i�C�Ӂj�g������M�̍��ڂɎ���͑��݂��Ȃ�</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetKtMeisyouHannyouDropdownData(ByRef dt As DataTable, _
                                                           ByVal type As EarthEnum.emKtMeisyouType, _
                                                           ByVal withSpaceRow As Boolean, _
                                                           Optional ByVal withCode As Boolean = True, _
                                                           Optional ByVal blnTorikesi As Boolean = True)

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouDropdownData", _
                                                    dt, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

        ' ���N���X�ł͏������܂���

    End Sub

    ''' <summary>
    ''' �敪����DataRow���쐬���܂�
    ''' </summary>
    ''' <param name="Text">��ʕ\�������e�L�X�g �敪�F�敪��</param>
    ''' <param name="Value">�敪</param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateRow(ByVal Text As String, _
                       ByVal Value As String, _
                       ByVal dt As DataTable) As DataRow

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateRow", _
                                            Text, _
                                            Value, _
                                            dt)

        Dim dr As DataRow = dt.NewRow()
        dr(0) = Text
        dr(1) = Value
        Return dr

    End Function

End Class
