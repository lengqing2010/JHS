Attribute VB_Name = "Cmdline"
Option Explicit

'*< TOSTEM >*******************************************************************'
'*
'*�y�v���V�[�W�����z
'*!     GetCommandLine
'*
'*�y�����T�v�z
'*?     �����ײ݈����̒l���擾����
'*      �i��؂�̓X�y�[�X���^�u�j
'*      �w�肵�������̐���葽���ꍇ�͖�������܂�
'*      (�w��Ȃ���10�ȏ�͖������܂�)
'*      ������������ٺ�ð��݂ň͂��Ă�OK�ł��i�������܂��̂Łj
'*
'*�y�p�����[�^�z
'*  <   Name            Type        Comment                               >
'*P     MaxArgs;        Variant;    �����̐�(�ȗ��\�F�ȗ����͍ő��10�܂Łj;
'*
'*�y�߂�l�z
'*  <   Name            Type        Comment                               >
'*R     GetCommandLine; Array;      �擾���������ײ݈���(���������݂��Ȃ��ꍇ�͋󕶎���Ԃ��j
'*
'******************************************************************************'
'*�y�ύX�����z
'*  < Ver          ���t         ���O(��Ж�)            ����                   >
'*M   Ver 1. 0. 0  2004/09/14;  K.Fukutomi;            �V�K�쐬;
'*
'*< Comment End >**************************************************************'
Public Function GetCommandLine(Optional MaxArgs)
On Error GoTo Err_GetCommandLine
    ' �ϐ���錾���܂��B
    Dim C As String
    Dim Cmdline As Variant
    Dim CmdLnLen As Long
    Dim InArg As Boolean
    Dim i As Long
    Dim NumArgs As Long
    Dim argArray() As String
    
    ' MaxArgs ���񋟂���邩�ǂ����𒲂ׂ܂��B
    If IsMissing(MaxArgs) Then MaxArgs = 10
    
    NumArgs = 0: InArg = True
    
    ' �R�}���h ���C���̈������擾���܂��B
    Cmdline = Command()
    CmdLnLen = Len(Cmdline)
    ' �����ɃR�}���h ���C���̈������擾���܂��B
    If Trim(Cmdline) <> "" Then
        ReDim argArray(0)
    End If

    For i = 1 To CmdLnLen
        C = Mid(Cmdline, i, 1)
        ' �X�y�[�X�܂��̓^�u�𒲂ׂ܂��B
        If (C <> " " And C <> vbTab) Then
            ' �X�y�[�X�܂��̓^�u�̂�����ł�����܂���B
            ' ���Ɉ����̒��ł͂Ȃ����ǂ����𒲂ׂ܂��B
            If Not InArg Then
                ' �V�����������n�܂�܂��B
                ' �������������Ȃ����𒲂ׂ܂��B
                If NumArgs = MaxArgs Then Exit For
                NumArgs = NumArgs + 1
                ' ���������ׂĊi�[�ł���悤�ɔz��̃T�C�Y��ύX���܂��B
                ReDim Preserve argArray(NumArgs)
                InArg = True
            End If

            'If C <> """" And C <> "'" Then
            If C <> """" Then
                ' ���݂̈����ɕ�����ǉ����܂��B
                argArray(NumArgs) = argArray(NumArgs) + C
            End If
        Else
            ' �X�y�[�X�܂��̓^�u�������܂����B
            ' InArg �t���O�� False ��ݒ肵�܂��B
            InArg = False
        End If
    Next i
    If Trim(Cmdline) <> "" Then
        ReDim Preserve argArray(MaxArgs)
        ' �֐����ɔz���Ԃ��܂��B
        GetCommandLine = argArray
    Else
        Erase argArray
        GetCommandLine = ""
    End If
    
Exit_GetCommandLine:
    Exit Function
    
Err_GetCommandLine:
    Erase argArray
    GetCommandLine = "��������ERROR:GetCommandLine��������"
    Resume Exit_GetCommandLine
    
End Function



