Imports System.Reflection

Public Interface ISqlStringHelper
    ''' <summary>
    ''' ���R�[�h�N���X�̃v���p�e�B�����SQL���𐶐�����
    ''' </summary>
    ''' <param name="recordType">�ݒ�Ώۂ̃��R�[�h�^�C�v</param>
    ''' <param name="row">�X�V�f�[�^���R�[�h</param>
    ''' <param name="paramList">�p�����[�^���R�[�h�̃��X�g</param>
    ''' <param name="recordTypeRow">�X�V�f�[�^���R�[�h�^�C�v</param>
    ''' <returns>�X�VSQL��</returns>
    ''' <remarks></remarks>
    Function MakeUpdateInfo(ByVal recordType As Type, _
                            ByVal row As Object, _
                            ByRef paramList As List(Of ParamRecord), _
                            Optional ByVal recordTypeRow As Type = Nothing) As String
End Interface
