@ECHO OFF
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e�� 
rem ���W���[���� : START00A.BAT
rem ������       : �o�b�`���ʊJ�n����
rem ����         : ����(%1)��/A������� %Mailto_GroupA% �փ��[�����M
rem              : ����(%1)��/B������� %Mailto_GroupB% �փ��[�����M
rem              : ����(%1)���w�肵�Ȃ��ꍇ�́A���[���𑗐M���Ȃ�
rem ���ӎ���     : �������e�ɂ�蕡���̃��[�����M���؂蕪����ꍇ��
rem                �e�o�b�`���ň���(%1)�̒l���w�肷��(�����̒l�͕ύX��)
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-#####  ����������������       �ύX
rem ***************************************************************************
color F9
TITLE %BAT_ID%(%BAT_NAME%)

copy /y %LOGFILE% %LOG_PATH%\BAK\%BAT_ID%.BAK > nul

del %LOGFILE% 2> nul
del %TMPFILE% 2> nul
del %ERRFILE% 2> nul

echo %LINE00% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% �J�n�� %%" >> %LOGFILE%
rem whoami|%AEXE_CMD% "ECHO ���sUser�� %%" >> %LOGFILE%

rem ---------------- Mail����̐ݒ� --------------------
rem --���悪3��ވȏ�̏ꍇ�̓P�[�X(�����̒l)��ǉ����Ă�������
if /I "%1"=="/A" (
 set MAIL_ADDRESS=%Mailto_GroupA%
) else if /I "%1"=="/B" (
 set MAIL_ADDRESS=%Mailto_GroupB%
) else (
 rem �f�t�H���g���t��
 set MAIL_ADDRESS=%Mailto_GroupA%
)

if NOT "%1"=="" (
rem ---------------- Send Mail --------------------
 call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "0" "�������J�n���܂���"
)

rem ---------------- End Of Batch ---------------------------------------------
