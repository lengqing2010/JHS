@echo off
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : BCP0010A.BAT
rem 処理名       : BCP(IN)起動共通バッチスクリプト
rem 引数           %1 ... TBL_NAME
rem                %2 ... DATA_FILE_NAME(CSV)
rem                %3 ... FMT_FILE
rem                %4 ... ERR_FILE
rem                %5 ... LOG_FILE
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set DATFILE=%2
set FMT_FILE=%3
set ERRFILE=%4
set OUT_FILE=%5

rem ===========================================================================
rem ＢＣＰ 起動
rem ---------------------------------------------------------------------------
bcp %DBNameJHS%..%TBL_NAME% in %DATFILE% -f %FMT_FILE% -U %DBLoginNameJHS% -P %DBLoginPassJHS% -S %DBServerJHS% -b 1000 >> %OUT_FILE%
if errorlevel 1 (
  echo 【return9】 >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem ＢＣＰ エラーチェック
rem ---------------------------------------------------------------------------
find /C "行コピーされました" %OUT_FILE% > nul
if errorlevel 1 (
  echo %FMT_FILE%でBCPエラー発生！ >> %OUT_FILE%
  echo 【return7】 >> %OUT_FILE%
  endlocal
  exit /B 7
)

find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo 【return0】 >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %FMT_FILE%でBCPエラー発生！ >> %OUT_FILE%
  echo 【return8】 >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------

