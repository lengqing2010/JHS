@ECHO OFF
rem ***************************************************************************
rem VXe¼   : JHSÚqÇVXe
rem W[¼ : END0010A.BAT
rem ¼       : STEP¤ÊI¹
rem Ó     : 
rem
rem Date        EDP-No   ®/¼              à   e
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  ko(åA)          VKì¬
rem ***************************************************************************
if %1 == 9 (
  echo %LINE03% >> %ERRFILE%
  type %TMPFILE% >> %ERRFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% ABENDijË %%" >> %ERRFILE%
  echo %LINE03% >> %LOGFILE%
  type %TMPFILE% >> %LOGFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% ABENDijË %%" >> %LOGFILE%
) else (
  type %TMPFILE% >> %LOGFILE%
  echo. >> %LOGFILE%
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %ST_NO% %ST_NAME% ENDiOOjË %%" >> %LOGFILE%
)
del %TMPFILE% 2> nul
rem ---------------- End Of Batch ---------------------------------------------

