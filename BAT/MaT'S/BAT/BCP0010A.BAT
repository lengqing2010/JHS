@echo off
rem ***************************************************************************
rem ƒVƒXƒeƒ€–¼   : JHSŒÚ‹qŠÇ—ƒVƒXƒeƒ€
rem ƒ‚ƒWƒ…[ƒ‹–¼ : BCP0010A.BAT
rem ˆ—–¼       : BCP(IN)‹N“®‹¤’Êƒoƒbƒ`ƒXƒNƒŠƒvƒg
rem ˆø”           %1 ... TBL_NAME
rem                %2 ... DATA_FILE_NAME(CSV)
rem                %3 ... FMT_FILE
rem                %4 ... ERR_FILE
rem                %5 ... LOG_FILE
rem
rem Date        EDP-No   Š‘®/Ž–¼              “à   —e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  —k‘o(‘å˜A)          V‹Kì¬
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set DATFILE=%2
set FMT_FILE=%3
set ERRFILE=%4
set OUT_FILE=%5

rem ===========================================================================
rem ‚a‚b‚o ‹N“®
rem ---------------------------------------------------------------------------
bcp %DBNameJHS%..%TBL_NAME% in %DATFILE% -f %FMT_FILE% -U %DBLoginNameJHS% -P %DBLoginPassJHS% -S %DBServerJHS% -b 1000 >> %OUT_FILE%
if errorlevel 1 (
  echo yreturn9z >> %OUT_FILE%
  endlocal
  exit /B 9
)

rem ===========================================================================
rem ‚a‚b‚o ƒGƒ‰[ƒ`ƒFƒbƒN
rem ---------------------------------------------------------------------------
find /C "sƒRƒs[‚³‚ê‚Ü‚µ‚½" %OUT_FILE% > nul
if errorlevel 1 (
  echo %FMT_FILE%‚ÅBCPƒGƒ‰[”­¶I >> %OUT_FILE%
  echo yreturn7z >> %OUT_FILE%
  endlocal
  exit /B 7
)

find /C "Error" %OUT_FILE% > nul
if errorlevel 1 (
    echo yreturn0z >> %OUT_FILE%
    endlocal
    exit /B 0
) else (
  echo %FMT_FILE%‚ÅBCPƒGƒ‰[”­¶I >> %OUT_FILE%
  echo yreturn8z >> %OUT_FILE%
  endlocal
  exit /B 8
)
rem ---------------- End Of Batch ---------------------------------------------

