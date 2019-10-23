
''' <summary>
''' �R���{�{�b�N�X�p�̃f�[�^�擾���N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class DropDownLogicHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �R���{�p�̃f�[�^�e�[�u�����擾���܂�
    ''' </summary>
    ''' <param name="data_access">�R���{�ݒ�f�[�^�擾�p�A�N�Z�X�N���X�̃C���X�^���X</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>    
    ''' <param name="intDdlType" >�h���b�v�_�E�����X�g�^�C�v[����M or �g������M]</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateList(ByVal data_access As AbsDataAccess, _
                        ByVal withSpaceRow As Boolean, _
                        Optional ByVal withCode As Boolean = True, _
                        Optional ByVal intDdlType As EarthEnum.emDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu, _
                        Optional ByVal blnTorikesi As Boolean = True _
                        ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateList", _
                                            data_access, _
                                            withSpaceRow, _
                                            withCode)
        ' �R���{�f�[�^�ƂȂ�DataTable
        Dim dt As DataTable = New DataTable()

        ' DataTable�ւ̃J�����ݒ�
        dt.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dt.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        ' �h���b�v�_�E�����X�g�^�C�v
        If intDdlType = EarthEnum.emDdlType.MMeisyouSyubetu Then
            data_access.GetMeisyouDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        ElseIf intDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu Then
            data_access.GetDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        ElseIf intDdlType = EarthEnum.emDdlType.KtMeisyou Then
            data_access.GetKtMeisyouDropdownData(dt, withSpaceRow, withCode, blnTorikesi)
        End If

        ' DataView ���쐬���ԋp
        Dim dv As DataView = New DataView(dt)

        Return dv

    End Function

    ''' <summary>
    ''' �R���{�p�̃f�[�^�e�[�u�����擾���܂�(�\�����ڂ��p�����[�^�Ŏw��)
    ''' </summary>
    ''' <param name="data_access">�R���{�ݒ�f�[�^�擾�p�A�N�Z�X�N���X�̃C���X�^���X</param>
    ''' <param name="type">�g������M�h���b�v�_�E���^�C�v</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>    
    ''' <param name="intDdlType" >�h���b�v�_�E�����X�g�^�C�v[����M or �g������M]</param>
    ''' <param name="blnTorikesi">�i�C�Ӂj�g������M�̍��ڂɎ���͑��݂��Ȃ�</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreateHannyouList(ByVal data_access As AbsDataAccess, _
                               ByVal type As EarthEnum.emKtMeisyouType, _
                               ByVal withSpaceRow As Boolean, _
                               Optional ByVal withCode As Boolean = True, _
                               Optional ByVal intDdlType As EarthEnum.emDdlType = EarthEnum.emDdlType.MExcpMeisyouSyubetu, _
                               Optional ByVal blnTorikesi As Boolean = True _
                               ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".CreateHannyouList", _
                                                    data_access, _
                                                    type, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)
        ' �R���{�f�[�^�ƂȂ�DataTable
        Dim dt As DataTable = New DataTable()

        ' DataTable�ւ̃J�����ݒ�
        dt.Columns.Add(New DataColumn("CmbTextField", GetType(String)))
        dt.Columns.Add(New DataColumn("CmbValueField", GetType(String)))

        ' �h���b�v�_�E�����X�g�^�C�v
        If intDdlType = EarthEnum.emDdlType.KtMeisyou Then
            data_access.GetKtMeisyouHannyouDropdownData(dt, type, withSpaceRow, withCode, blnTorikesi)
        End If

        ' DataView ���쐬���ԋp
        Dim dv As DataView = New DataView(dt)

        Return dv

    End Function
End Class
