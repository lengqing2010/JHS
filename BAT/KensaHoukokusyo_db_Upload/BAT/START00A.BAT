@ECHO OFF
rem ***************************************************************************
rem システム名   : JHS顧客管理システム 
rem モジュール名 : START00A.BAT
rem 処理名       : バッチ共通開始処理
rem 引数         : 引数(%1)に/Aがあると %Mailto_GroupA% へメール送信
rem              : 引数(%1)に/Bがあると %Mailto_GroupB% へメール送信
rem              : 引数(%1)を指定しない場合は、メールを送信しない
rem 注意事項     : 処理内容により複数のメール送信先を切り分ける場合に
rem                親バッチ側で引数(%1)の値を指定する(引数の値は変更可)
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-#####  ＃＃＃＃＃＃＃＃       変更
rem ***************************************************************************
color F9
TITLE %BAT_ID%(%BAT_NAME%)

set filename=%date:~0,4%%date:~5,2%%date:~8,2%%time:~0,2%%time:~3,2%
copy /y %LOGFILE% %LOG_PATH%\BAK\%BAT_ID%_%filename%.BAK > nul

del %LOGFILE% 2> nul
del %TMPFILE% 2> nul
del %ERRFILE% 2> nul

echo %LINE00% >> %LOGFILE%
%ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% 開始⇒ %%" >> %LOGFILE%
rem whoami|%AEXE_CMD% "ECHO 実行User⇒ %%" >> %LOGFILE%

rem ---------------- Mail宛先の設定 --------------------
rem --宛先が3種類以上の場合はケース(引数の値)を追加してください
if /I "%1"=="/A" (
 set MAIL_ADDRESS=%Mailto_GroupA%
) else if /I "%1"=="/B" (
 set MAIL_ADDRESS=%Mailto_GroupB%
) else (
 rem デフォルト送付先
 set MAIL_ADDRESS=%Mailto_GroupA%
)

if NOT "%1"=="" (
rem ---------------- Send Mail --------------------
 call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "0" "処理を開始しました"
)

rem ---------------- End Of Batch ---------------------------------------------

