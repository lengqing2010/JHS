@ECHO OFF
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e��
rem ���W���[���� : END0000A.BAT
rem ������       : �o�b�`���ʏI������
rem ����         : ����(%1):���킩�ُ�̃t���O�@9�F�ُ�@9�ȊO����
rem              : ����(%2)��/A������� %Mailto_GroupA% �փ��[�����M
rem              : ����(%2)��/B������� %Mailto_GroupB% �փ��[�����M
rem              : ����(%2)���w�肵�Ȃ��ꍇ�́A���[���𑗐M���Ȃ�
rem ���ӎ���     : �������e�ɂ�蕡���̃��[�����M���؂蕪����ꍇ��
rem                �e�o�b�`���ň���(%2)�̒l���w�肷��(�����̒l�͕ύX��)
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-#####  ����������������       �ύX
rem ***************************************************************************
echo %LINE02% >> %LOGFILE%

rem ---------------- Mail����̐ݒ� --------------------
rem --���悪3��ވȏ�̏ꍇ�̓P�[�X(�����̒l)��ǉ����Ă�������
if /I "%2"=="/A" (
 set MAIL_ADDRESS=%Mailto_GroupA%
) else if /I "%2"=="/B" (
 set MAIL_ADDRESS=%Mailto_GroupB%
) else (
 rem �f�t�H���g���t��
 set MAIL_ADDRESS=%Mailto_GroupA%
)

if %1 == 9 (
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% �����ُ�I�������� %%" >> %LOGFILE%
  rem ---------------- Send Mail --------------------
  call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "2" "�������ُ�I�����܂���"
) else (
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% ��������I�������� %%" >> %LOGFILE%
  if NOT "%2" == "" (
  rem ---------------- Send Mail --------------------
  call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "1" "����������I�����܂���"
  )
)

rem echo %LINE01% >> %LOGFILE%
rem ---------------- End Of Batch ---------------------------------------------
