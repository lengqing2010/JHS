@echo off
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e��
rem ���W���[���� : MaT'S0002.BAT
rem ������       : ��A���f�[�^ ��荞�ݏ���
rem ���ӎ���     : 
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-xxxxx  �J��X��/����           �ύX
rem ***************************************************************************

rem ===========================================================================
rem ���ϐ��̐ݒ�
rem ---------------------------------------------------------------------------
:USR_PATH
call H:\BAT\MaT'S\BAT\SET_ENV.BAT
set BAT_NAME=��A���f�[�^ ��荞�ݏ���
set BAT_ID=%~n0
set STATUS=0

rem ===========================================================================
rem Batch Start ����
rem ---------------------------------------------------------------------------
:START
set LOGFILE=%LOG_PATH%\%BAT_ID%.LOG
set TMPFILE=%LOG_PATH%\%BAT_ID%.TMP
set ERRFILE=%LOG_PATH%\%BAT_ID%.ERR

call %BAT_PATH%\START00A.BAT

rem �q�o�b�`���Ăяo��
for /f "delims=" %%a in (%INC_PATH%\SfamtTable.ini) do (call %BAT_PATH%\MaT'S0002A.BAT %%a)

echo %LINE01% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% �I���� %%" >> %LOGFILE%
echo %LINE00% >> %LOGFILE%

rem ===========================================================================
rem �q�o�b�` �G���[�`�F�b�N
rem ---------------------------------------------------------------------------
find /C "�����ُ�I������" %LOGFILE% > nul
if errorlevel 1 (
  set STATUS=0
) else (
  set STATUS=9
)

aexit %STATUS%
if /I "%1"=="/O" GOTO :EOF
exit