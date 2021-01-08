@ECHO OFF
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : END0010A.BAT
rem 処理名       : STEP共通終了処理
rem 注意事項     : 
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem ***************************************************************************
if %1 == 9 (
  echo %LINE03% >> %ERRFILE%
  type %TMPFILE% >> %ERRFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% ABEND（＞＜）⇒ %%" >> %ERRFILE%
  echo %LINE03% >> %LOGFILE%
  type %TMPFILE% >> %LOGFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% ABEND（＞＜）⇒ %%" >> %LOGFILE%
) else (
  type %TMPFILE% >> %LOGFILE%
  echo. >> %LOGFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% END（＾＾）⇒ %%" >> %LOGFILE%
)
del %TMPFILE% 2> nul
rem ---------------- End Of Batch ---------------------------------------------

