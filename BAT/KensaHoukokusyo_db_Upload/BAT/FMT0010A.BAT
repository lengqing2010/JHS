rem @echo off
rem ***************************************************************************
rem ƒVƒXƒeƒ€–¼   : JHSŒÚ‹qŠÇ—ƒVƒXƒeƒ€
rem ƒ‚ƒWƒ…[ƒ‹–¼ : FMT0010A.BAT
rem ˆ—–¼       : FMTƒtƒ@ƒCƒ‹ì¬‹¤’Êƒoƒbƒ`ƒXƒNƒŠƒvƒg
rem ˆø”           %1 ... TBL_NAME
rem                %2 ... ERR_FILE
rem                %3 ... LOG_FILE
rem
rem Date        EDP-No   Š‘®/Ž–¼              “à   —e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  —k‘o(‘å˜A)          V‹Kì¬
rem yyyy/mm/dd  P-#####  ””””””””       •ÏX
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set ERRFILE=%2
set OUT_FILE=%3

rem ===========================================================================
rem ‚a‚b‚o ‹N“®
rem ---------------------------------------------------------------------------
bcp %DBNAMEEarth%..%TBL_NAME% format nul -c -f %BCP_PATH%\%TBL_NAME%.FMT -t\t -r\n -S %DBServerEarth% -U %DBLoginNameEarth% -P %DBLoginPassEarth% >> %OUT_FILE%

if errorlevel 1 (
  echo yreturn9z >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem ‚a‚b‚o ƒGƒ‰[ƒ`ƒFƒbƒN
rem ---------------------------------------------------------------------------
find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo yreturn0z >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %TBL_NAME%‚ÅFMTƒtƒ@ƒCƒ‹ì¬ƒGƒ‰[”­¶I >> %OUT_FILE%
  echo yreturn8z >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------
