''' <summary>
''' �n��C���^�[�t�F�[�X
''' </summary>
''' <remarks>�n��I�u�W�F�N�g�̊��</remarks>
Public Interface IKeiretuDataAccess

    ''' <summary>
    ''' �������z���擾���܂�
    ''' </summary>
    ''' <param name="intMode">�擾���[�h</param>
    ''' <param name="strSyouhinCd">���i�R�[�h</param>
    ''' <param name="intKingaku">TH�����p���i�}�X�^��KEY,�|���v�Z���̋��z</param>
    ''' <param name="intReturnKingaku">�擾���z�i�߂�l�j</param>
    ''' <returns>1:�擾OK,0:�o���v�A��,-1:�擾NG</returns>
    ''' <remarks></remarks>
    Function getSeikyuKingaku(ByVal intMode As Integer, _
                        ByVal strSyouhinCd As String, _
                        ByVal intKingaku As Integer, _
                        ByRef intReturnKingaku As Integer) As Integer
End Interface
