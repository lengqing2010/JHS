''' <summary>
''' �f�[�^�A�N�Z�X�N���X�p���ۃN���X�ł�
''' ��ɃR���{�{�b�N�X�f�[�^�̐ݒ�Ɏg�p����ׁA
''' �R���{�{�b�N�X�f�[�^�̍쐬�Ɋ֌W�Ȃ��ꍇ��Inherits�s�v
''' </summary>
''' <remarks>��ʕ\���p�̃f�[�^�J�����@"CmbTextField"<br/>
'''          Value�̃f�[�^�J�����@     "CmbValueField"</remarks>
Public Class AbsDataAccess

    ''' <summary>
    ''' �R���{�{�b�N�X�ݒ�p�̃f�[�^�e�[�u�����擾���܂�
    ''' </summary>
    ''' <param name="dt" ></param>
    ''' <param name="withSpaceRow" >�擪�ɋ󔒍s���Z�b�g����ꍇ:true</param>
    ''' <param name="withCode" >�i�C�ӁjValue��Key���ڂ�t������ꍇ:true " 1:aaaa "</param>
    ''' <remarks></remarks>
    Public Overridable Sub GetDropdownData(ByRef dt As DataTable, ByVal withSpaceRow As Boolean, Optional ByVal withCode As Boolean = True)
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
    Function CreateRow(ByVal Text As String, ByVal Value As String, ByVal dt As DataTable) As DataRow

        Dim dr As DataRow = dt.NewRow()
        dr(0) = Text
        dr(1) = Value
        Return dr

    End Function

End Class
