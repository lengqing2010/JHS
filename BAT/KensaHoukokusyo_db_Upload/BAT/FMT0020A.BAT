rem @echo off
rem ***************************************************************************
rem �V�X�e����   : JHS�ڋq�Ǘ��V�X�e��
rem ���W���[���� : FMT0020A.BAT
rem ������       : FMT�t�@�C���쐬���ʃo�b�`�X�N���v�g
rem ����           %1 ... TBL_NAME
rem                %2 ... ERR_FILE
rem                %3 ... LOG_FILE
rem
rem Date        EDP-No   ����/����              ��   �e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  �k�o(��A)          �V�K�쐬
rem yyyy/mm/dd  P-#####  ����������������       �ύX
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set ERRFILE=%2
set OUT_FILE=%3

rem ===========================================================================
rem �a�b�o �N��
rem ---------------------------------------------------------------------------
bcp %DBNAMEJHS%..%TBL_NAME% format nul -c -f %BCP_PATH%\%TBL_NAME%.FMT -t\t -r\n -S %DBServerJHS% -U %DBLoginNameJHS% -P %DBLoginPassJHS% >> %OUT_FILE%

if errorlevel 1 (
  echo �yreturn9�z >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem �a�b�o �G���[�`�F�b�N
rem ---------------------------------------------------------------------------
find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo �yreturn0�z >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %TBL_NAME%��FMT�t�@�C���쐬�G���[�����I >> %OUT_FILE%
  echo �yreturn8�z >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------