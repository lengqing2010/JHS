@ECHO OFF
rem ***************************************************************************
rem システム名   : EARTHシステム
rem モジュール名 : KensaHoukokusyo_db_Upload.BAT
rem 処理名       : 検査報告書アップロード
rem 注意事項     : 
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2015/12/04  P-xxxxx  大連/車龍              新規作成
rem yyyy/mm/dd  P-xxxxx  開発X部/○○           変更
rem ***************************************************************************

rem ===========================================================================
rem 環境変数の設定
rem ---------------------------------------------------------------------------
:USR_PATH
call H:\BAT\KensaHoukokusyo_db_Upload\BAT\SET_ENV.BAT
set BAT_NAME=検査報告書アップロード
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

rem ===========================================================================
rem 検査報告書アップロード
rem ---------------------------------------------------------------------------
:ST010
set ST_NAME=検査報告書アップロード
set ST_NO=ST010
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ===========================================================================
rem EXE実行
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
rem Batch End 処理
rem ---------------------------------------------------------------------------
:END
call %BAT_PATH%\END0000A.BAT %STATUS%

echo %LINE00% >> %LOGFILE%

aexit %STATUS%
if /I "%1"=="/O" GOTO :EOF
exit

rem ---------------- End Of Batch ---------------------------------------------