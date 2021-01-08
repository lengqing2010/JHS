@echo off
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e��
rem ���W���[���� : MaT'S0002A.BAT
rem ������       : ��A���f�[�^�i�e�[�u�����ƂɁj ��荞�ݏ���
rem ���ӎ���     : 
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-xxxxx  �J��X��/����           �ύX
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set BAT_NAME=%TBL_NAME%�f�[�^ ��荞�ݏ���

echo %LINE01% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% �J�n�� %%" >> %LOGFILE%

rem ===========================================================================
rem EXE�G�N�X�|�[�g����
rem ---------------------------------------------------------------------------
:ST010
set ST_NAME=%TBL_NAME%�f�[�^�G�N�X�|�[�g����
set ST_NO=ST010
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem EXE�̈���
rem   ��P�����FOutputFile(���O�t�@�C��)
rem   ��Q�����F�����e�[�u��
rem   ��R�����F�f�[�^�t�@�C���p�X
rem   ��S�����FDBName
rem   ��T�����FDBLoginName
rem   ��U�����FDBLoginPass
rem   ��V�����FDBOwner
rem ///////////////////////////////////////////////////////////////////////////
call %EXE_PATH%\ASPSFAGetData.exe %TMPFILE% %TBL_NAME% %DAT_PATH%\ %DATABASE% %DBUSER% %DBPASS% %DBOWNER%

find /C "success,perfect,excellent" %TMPFILE% > nul
if errorlevel 1 (
  set STATUS=9
)

call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem SQL����
rem ---------------------------------------------------------------------------
:ST020
set ST_NAME=%TBL_NAME%�f�[�^��TRUNCATE����
set ST_NO=ST020
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem EXE�̈���
rem   ��P�����FOutputFile(���O�t�@�C��)
rem   ��Q�����FProvider(SQLOLEDB)
rem   ��R�����FDBServer
rem   ��S�����FDBName
rem   ��T�����FDBLoginName
rem   ��U�����FDBLoginPass
rem   ��V�����F�����e�[�u��
rem ///////////////////////////////////////////////////////////////////////////
call %EXE_PATH%\DataDelete.exe %TMPFILE% %DBProvider% %DBServerJHS% %DBNameJHS% %DBLoginNameJHS% %DBLoginPassJHS% %TBL_NAME%

find /C "TRUNCATE���s" %TMPFILE% > nul
if errorlevel 1 (
  set STATUS=0
) else (
  set STATUS=9
)

call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem BCP���[�h����
rem ---------------------------------------------------------------------------
:ST030
set ST_NAME=%TBL_NAME%��BCP���[�h����
set ST_NO=ST030
set STATUS=0
set BCP_IN1=%TBL_NAME%
set BCP_IN2=%DAT_PATH%\%TBL_NAME%.dat
set BCP_IN3=%BCP_PATH%\%TBL_NAME%.FMT

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem BCP_BAT�̈���
rem   ��P�����F�G�N�X�|�[�g���e�[�u����
rem   ��Q�����FErrFile
rem   ��R�����FOutputFile(���O�t�@�C��)
rem ///////////////////////////////////////////////////////////////////////////
call %BCP_FMTIN_BAT% %BCP_IN1% %ERRFILE% %TMPFILE%
if errorlevel 1 (
  set STATUS=9
)

rem ///////////////////////////////////////////////////////////////////////////
rem BCP_BAT�̈���
rem   ��P�����F���[�h��e�[�u����
rem   ��Q�����F���[�h���t�@�C����
rem   ��R�����FBCP�t�H�[�}�b�g�t�@�C��
rem   ��S�����FErrFile
rem   ��T�����FOutputFile(���O�t�@�C��)
rem ///////////////////////////////////////////////////////////////////////////
call %BCP_IN_BAT% %BCP_IN1% %BCP_IN2% %BCP_IN3% %ERRFILE% %TMPFILE%
if errorlevel 1 (
  set STATUS=9
)
call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem Batch End ����
rem ---------------------------------------------------------------------------
:END
cd %AUTO_BAT_PATH%
call %BAT_PATH%\END0000A.BAT %STATUS%

endlocal