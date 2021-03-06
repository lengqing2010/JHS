rem @echo off
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : FMT0010A.BAT
rem 処理名       : FMTファイル作成共通バッチスクリプト
rem 引数           %1 ... TBL_NAME
rem                %2 ... ERR_FILE
rem                %3 ... LOG_FILE
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-#####  ＃＃＃＃＃＃＃＃       変更
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set ERRFILE=%2
set OUT_FILE=%3

rem ===========================================================================
rem ＢＣＰ 起動
rem ---------------------------------------------------------------------------
bcp %DBNAMEEarth%..%TBL_NAME% format nul -c -f %BCP_PATH%\%TBL_NAME%.FMT -t\t -r\n -S %DBServerEarth% -U %DBLoginNameEarth% -P %DBLoginPassEarth% >> %OUT_FILE%

if errorlevel 1 (
  echo 【return9】 >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem ＢＣＰ エラーチェック
rem ---------------------------------------------------------------------------
find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo 【return0】 >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %TBL_NAME%でFMTファイル作成エラー発生！ >> %OUT_FILE%
  echo 【return8】 >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------
