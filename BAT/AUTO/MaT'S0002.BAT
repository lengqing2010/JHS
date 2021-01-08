@echo off
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : MaT'S0002.BAT
rem 処理名       : 報連相データ 取り込み処理
rem 注意事項     : 
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-xxxxx  開発X部/○○           変更
rem ***************************************************************************

rem ===========================================================================
rem 環境変数の設定
rem ---------------------------------------------------------------------------
:USR_PATH
call H:\BAT\MaT'S\BAT\SET_ENV.BAT
set BAT_NAME=報連相データ 取り込み処理
set BAT_ID=%~n0
set STATUS=0

rem ===========================================================================
rem Batch Start 処理
rem ---------------------------------------------------------------------------
:START
set LOGFILE=%LOG_PATH%\%BAT_ID%.LOG
set TMPFILE=%LOG_PATH%\%BAT_ID%.TMP
set ERRFILE=%LOG_PATH%\%BAT_ID%.ERR

call %BAT_PATH%\START00A.BAT

rem 子バッチを呼び出す
for /f "delims=" %%a in (%INC_PATH%\SfamtTable.ini) do (call %BAT_PATH%\MaT'S0002A.BAT %%a)

echo %LINE01% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% 終了⇒ %%" >> %LOGFILE%
echo %LINE00% >> %LOGFILE%

rem ===========================================================================
rem 子バッチ エラーチェック
rem ---------------------------------------------------------------------------
find /C "■■異常終了■■" %LOGFILE% > nul
if errorlevel 1 (
  set STATUS=0
) else (
  set STATUS=9
)

aexit %STATUS%
if /I "%1"=="/O" GOTO :EOF
exit