@echo off
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e��
rem ���W���[���� : BCP0020A.BAT
rem ������       : BCP(OUT)�N�����ʃo�b�`�X�N���v�g
rem ����           %1 ... TBL_NAME
rem                %2 ... DATA_FILE_NAME(OUT-CSV)
rem                %3 ... FMT_FILE
rem                %4 ... ERR_FILE
rem                %5 ... LOG_FILE
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-#####  ����������������       �ύX
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set DATFILE=%2
set FMT_FILE=%3
set ERRFILE=%4
set OUT_FILE=%5

rem ===========================================================================
rem �a�b�o �N��
rem ---------------------------------------------------------------------------
bcp %DBNameEarth%..%TBL_NAME% out %DATFILE% -f %FMT_FILE% -U %DBLoginNameEarth% -P %DBLoginPassEarth% -S %DBServerEarth% >> %OUT_FILE%
if errorlevel 1 (
  echo �yreturn9�z >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem �a�b�o �G���[�`�F�b�N
rem ---------------------------------------------------------------------------
find /C "�s�R�s�[����܂���" %OUT_FILE% > nul
if errorlevel 1 (
  echo %FMT_FILE%��BCP�G���[�����I >> %OUT_FILE%
  echo �yreturn7�z >> %OUT_FILE%
  endlocal
  exit /B 7
)

find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo �yreturn0�z >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %FMT_FILE%��BCP�G���[�����I >> %OUT_FILE%
  echo �yreturn8�z >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------