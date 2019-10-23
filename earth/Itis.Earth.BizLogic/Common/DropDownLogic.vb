''' <summary>
''' �h���b�v�_�E���f�[�^�擾�p���W�b�N�ł�
''' </summary>
''' <remarks></remarks>
Public Class DropDownLogic
    Inherits DropDownLogicHelper

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

    ''' <summary>
    ''' �h���b�v�_�E���p�̃f�[�^�e�[�u�����擾���܂�
    ''' </summary>
    ''' <param name="type" >�擾�Ώۂ̃f�[�^�^�C�v�i���̃N���X��DropDownType���w��j</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetComboList(ByVal type As Long, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        Select Case type
            Case 0
                ' �敪�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New KubunDataAccess
            Case 1
                ' �����T�v�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New TyousagaiyouDataAccess
            Case 2
                ' �������@�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New TyousahouhouDataAccess
            Case 3
                ' �K�w�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New KaisouDataAccess
            Case 4
                ' �\�������擾����f�[�^�A�N�Z�X�N���X
                data_access = New KouzouDataAccess
            Case 5
                ' �V�z���֏����擾����f�[�^�A�N�Z�X�N���X
                data_access = New ShintikuTatekaeDataAccess
            Case 6
                ' �Ԍɏ����擾����f�[�^�A�N�Z�X�N���X
                data_access = New SyakoDataAccess
            Case 7
                ' �����p�r�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New TatemonoYoutoDataAccess
            Case 8
                ' �\���b�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New YoteiKisoDataAccess
            Case 9
                ' �ް��j�������擾����f�[�^�A�N�Z�X�N���X
                data_access = New DataHakiDataAccess
            Case 10
                ' �o�R�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New KeiyuDataAccess
            Case 11
                ' �敪�����擾����f�[�^�A�N�Z�X�N���X(�ۏ؏�NO�N���t�^��)
                data_access = New KubunDataAccess2
            Case 12
                ' �ۏ؏����s�󋵏����擾����f�[�^�A�N�Z�X�N���X
                data_access = New HosyousyoHakJykyAccess
            Case 13
                ' �ی���Џ����擾����f�[�^�A�N�Z�X�N���X
                data_access = New HokenKaisyaAccess
            Case 14
                ' ����ŏ����擾����f�[�^�A�N�Z�X�N���X
                data_access = New SyouhizeiAccess
            Case 15
                ' �s���{�������擾����f�[�^�A�N�Z�X�N���X
                data_access = New TodoufukenDataAccess
            Case 16
                Return data_access
            Case 17
                Return data_access
            Case 18
                Return data_access
            Case 19
                Return data_access
            Case 20
                Return data_access
            Case 21
                Return data_access
            Case 22
                Return data_access
            Case 23
                Return data_access
            Case 24
                Return data_access
            Case 25
                Return data_access
            Case 26
                ' ���ǍH����ʏ����擾����f�[�^�A�N�Z�X�N���X
                data_access = New KairyouKoujiSyubetuAccess
            Case 27
                ' �󗝏����擾����f�[�^�A�N�Z�X�N���X
                data_access = New HkksJuriAccess
            Case 28
                ' �S����(���/�H��)�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New TantousyaAccess
            Case 29
                ' ��b�d�l�ڑ��������擾����f�[�^�A�N�Z�X�N���X
                data_access = New KsSiyouSetuzokusiAccess
            Case 30
                ' �n��R�[�h�����擾����f�[�^�A�N�Z�X�N���X
                data_access = New KeiretuDataAccess
            Case 31
                Return data_access
            Case 32
                Return data_access
            Case 33
                Return data_access
            Case 34
                '�X�ʃf�[�^�C���̓o�^�����i���擾����f�[�^�A�N�Z�X�N���X
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.TOUROKU_RYOU)
            Case 35
                '�X�ʃf�[�^�C���̔̑��i�����c�[�������擾����f�[�^�A�N�Z�X�N���X
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.TOOL_RYOU)
            Case 36
                '�X�ʃf�[�^�C���̓o�^�����i���擾����f�[�^�A�N�Z�X�N���X
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.FC_RYOU)
            Case 37
                '�X�ʃf�[�^�C���̔̑��i�����c�[�������擾����f�[�^�A�N�Z�X�N���X
                data_access = New TenbetuSyuuseiSyouhinDataAccess(TenbetuSyuuseiSyouhinDataAccess.SearchMode.NOT_FC_RYOU)

                '************************
                '* ���i�R�[�h���
                '************************
            Case 38
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:100)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN100)
            Case 39
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:110)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN110)
            Case 40
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:115)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN115)
            Case 41
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:120)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN120)
            Case 42
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:130)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN130)
            Case 43
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:140)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN140)
            Case 44
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:150)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN150)
            Case 45
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:160)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN160)
            Case 46
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:170)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN170)
            Case 47
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:180)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN180)
            Case 48
                ' ���i�����擾����f�[�^�A�N�Z�X�N���X(���ރR�[�h:110,115)
                data_access = New SyouhinDataAccess(SyouhinDataAccess.BunruiCdType.SYOUHIN2)
            Case 49
                Return data_access
            Case 50
                Return data_access
            Case 51
                Return data_access
            Case 52
                Return data_access
            Case 53
                Return data_access
            Case 55
                ' ������Џ����擾����f�[�^�A�N�Z�X�N���X
                data_access = New TyousakaisyaSearchDataAccess

            Case Else
                Return data_access
        End Select

        ' �v�����ꂽ�R���{�f�[�^���擾
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.MExcpMeisyouSyubetu, blnTorikesi)

    End Function

    ''' <summary>
    ''' �h���b�v�_�E���p�̃f�[�^�e�[�u�����擾���܂�(���̎�ʐ�p)
    ''' </summary>
    ''' <param name="type" >�擾�Ώۂ̃f�[�^�^�C�v��EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMeisyouComboList(ByVal type As EarthConst.emMeisyouType, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetMeisyouComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        ' ���̎�ʏ����擾����f�[�^�A�N�Z�X�N���X
        data_access = New MeisyouDataAccess(type)

        ' �v�����ꂽ�R���{�f�[�^���擾
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.MMeisyouSyubetu, blnTorikesi)

    End Function

    ''' <summary>
    ''' �h���b�v�_�E���p�̃f�[�^�e�[�u�����擾���܂�(�g������M��p)
    ''' </summary>
    ''' <param name="type" >�擾�Ώۂ̃f�[�^�^�C�v��EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>   ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKtMeisyouComboList(ByVal type As EarthConst.emKtMeisyouType, _
                          ByVal withSpaceRow As Boolean, _
                          Optional ByVal withCode As Boolean = True, _
                          Optional ByVal blnTorikesi As Boolean = True _
                          ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouComboList", _
                                            type, _
                                            withCode)

        Dim data_access As AbsDataAccess = Nothing

        ' ���̎�ʏ����擾����f�[�^�A�N�Z�X�N���X
        data_access = New KakutyouMeisyouDataAccess(type)

        ' �v�����ꂽ�R���{�f�[�^���擾
        Return CreateList(data_access, withSpaceRow, withCode, EarthEnum.emDdlType.KtMeisyou, blnTorikesi)

    End Function

    ''' <summary>
    ''' �h���b�v�_�E���p�̃f�[�^�e�[�u�����擾���܂�(�g������M��p�A�\�����ڂ��p�����[�^�Ŏw��)
    ''' </summary>
    ''' <param name="type" >�擾�Ώۂ̃f�[�^�^�C�v��EarthConst�̖��̃^�C�v���w�肷��</param>
    ''' <param name="ktMeisyouType">�g������M�h���b�v�_�E���^�C�v</param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <param name="blnTorikesi"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetKtMeisyouHannyouComboList(ByVal type As EarthConst.emKtMeisyouType, _
                                          ByVal ktMeisyouType As EarthEnum.emKtMeisyouType, _
                                          ByVal withSpaceRow As Boolean, _
                                          Optional ByVal withCode As Boolean = True, _
                                          Optional ByVal blnTorikesi As Boolean = True _
                                          ) As ICollection

        '���\�b�h���A�����̏��̑ޔ�
        UnTrappedExceptionManager.AddMethodEntrance(ASSEMBLY_CLASSNAME & ".GetKtMeisyouHannyouComboList", _
                                                    type, _
                                                    ktMeisyouType, _
                                                    withSpaceRow, _
                                                    withCode, _
                                                    blnTorikesi)

        Dim data_access As AbsDataAccess = Nothing

        ' ���̎�ʏ����擾����f�[�^�A�N�Z�X�N���X
        data_access = New KakutyouMeisyouDataAccess(type)

        ' �v�����ꂽ�R���{�f�[�^���擾
        Return CreateHannyouList(data_access, ktMeisyouType, withSpaceRow, withCode, EarthEnum.emDdlType.KtMeisyou, blnTorikesi)

    End Function
End Class
