@ECHO OFF
rem ***************************************************************************
rem �V�X�e����   : EARTH�V�X�e��
rem ���W���[���� : KensaHoukokusyo_db_Upload.BAT
rem ������       : �����񍐏��A�b�v���[�h
rem ���ӎ���     : 
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2015/12/04  P-xxxxx  ��A/�ԗ�              �V�K�쐬
rem yyyy/mm/dd  P-xxxxx  �J��X��/����           �ύX
rem ***************************************************************************

rem ===========================================================================
rem ���ϐ��̐ݒ�
rem ---------------------------------------------------------------------------
:USR_PATH
call H:\BAT\KensaHoukokusyo_db_Upload\BAT\SET_ENV.BAT
set BAT_NAME=�����񍐏��A�b�v���[�h
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

rem ===========================================================================
rem �����񍐏��A�b�v���[�h
rem ---------------------------------------------------------------------------
:ST010
set ST_NAME=�����񍐏��A�b�v���[�h
set ST_NO=ST010
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ===========================================================================
rem EXE���s
rem ---------------------------------------------------------------------------
call %EXE_PATH%\KensaHoukokusyo_db_Upload.exe >> %TMPFILE%
if %errorlevel% == 0 (
    set STATUS=0
) else (
    set STATUS=9
)

call %BAT_PATH%\END0010A.BAT %STATUS%

if %STATUS%==9 goto END

rem ===========================================================================
rem Batch End ����
rem ---------------------------------------------------------------------------
:END
call %BAT_PATH%\END0000A.BAT %STATUS%

echo %LINE00% >> %LOGFILE%

aexit %STATUS%
if /I "%1"=="/O" GOTO :EOF
exit

rem ---------------- End Of Batch ---------------------------------------------