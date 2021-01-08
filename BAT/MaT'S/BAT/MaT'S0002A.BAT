@echo off
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : MaT'S0002A.BAT
rem 処理名       : 報連相データ（テーブルごとに） 取り込み処理
rem 注意事項     : 
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-xxxxx  開発X部/○○           変更
rem ***************************************************************************

setlocal
set TBL_NAME=%1
set BAT_NAME=%TBL_NAME%データ 取り込み処理

echo %LINE01% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% 開始⇒ %%" >> %LOGFILE%

rem ===========================================================================
rem EXEエクスポート処理
rem ---------------------------------------------------------------------------
:ST010
set ST_NAME=%TBL_NAME%データエクスポート処理
set ST_NO=ST010
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem EXEの引数
rem   第１引数：OutputFile(ログファイル)
rem   第２引数：処理テーブル
rem   第３引数：データファイルパス
rem   第４引数：DBName
rem   第５引数：DBLoginName
rem   第６引数：DBLoginPass
rem   第７引数：DBOwner
rem ///////////////////////////////////////////////////////////////////////////
call %EXE_PATH%\ASPSFAGetData.exe %TMPFILE% %TBL_NAME% %DAT_PATH%\ %DATABASE% %DBUSER% %DBPASS% %DBOWNER%

find /C "success,perfect,excellent" %TMPFILE% > nul
if errorlevel 1 (
  set STATUS=9
)

call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem SQL処理
rem ---------------------------------------------------------------------------
:ST020
set ST_NAME=%TBL_NAME%データのTRUNCATE処理
set ST_NO=ST020
set STATUS=0

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem EXEの引数
rem   第１引数：OutputFile(ログファイル)
rem   第２引数：Provider(SQLOLEDB)
rem   第３引数：DBServer
rem   第４引数：DBName
rem   第５引数：DBLoginName
rem   第６引数：DBLoginPass
rem   第７引数：処理テーブル
rem ///////////////////////////////////////////////////////////////////////////
call %EXE_PATH%\DataDelete.exe %TMPFILE% %DBProvider% %DBServerJHS% %DBNameJHS% %DBLoginNameJHS% %DBLoginPassJHS% %TBL_NAME%

find /C "TRUNCATE失敗" %TMPFILE% > nul
if errorlevel 1 (
  set STATUS=0
) else (
  set STATUS=9
)

call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem BCPロード処理
rem ---------------------------------------------------------------------------
:ST030
set ST_NAME=%TBL_NAME%でBCPロード処理
set ST_NO=ST030
set STATUS=0
set BCP_IN1=%TBL_NAME%
set BCP_IN2=%DAT_PATH%\%TBL_NAME%.dat
set BCP_IN3=%BCP_PATH%\%TBL_NAME%.FMT

call %BAT_PATH%\START10A.BAT

rem ///////////////////////////////////////////////////////////////////////////
rem BCP_BATの引数
rem   第１引数：エクスポート元テーブル名
rem   第２引数：ErrFile
rem   第３引数：OutputFile(ログファイル)
rem ///////////////////////////////////////////////////////////////////////////
call %BCP_FMTIN_BAT% %BCP_IN1% %ERRFILE% %TMPFILE%
if errorlevel 1 (
  set STATUS=9
)

rem ///////////////////////////////////////////////////////////////////////////
rem BCP_BATの引数
rem   第１引数：ロード先テーブル名
rem   第２引数：ロード元ファイル名
rem   第３引数：BCPフォーマットファイル
rem   第４引数：ErrFile
rem   第５引数：OutputFile(ログファイル)
rem ///////////////////////////////////////////////////////////////////////////
call %BCP_IN_BAT% %BCP_IN1% %BCP_IN2% %BCP_IN3% %ERRFILE% %TMPFILE%
if errorlevel 1 (
  set STATUS=9
)
call %BAT_PATH%\END0010A.BAT %STATUS%
if %STATUS%==9 goto END

rem ===========================================================================
rem Batch End 処理
rem ---------------------------------------------------------------------------
:END
cd %AUTO_BAT_PATH%
call %BAT_PATH%\END0000A.BAT %STATUS%

endlocal
