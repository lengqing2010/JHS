@ECHO OFF
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : END0000A.BAT
rem 処理名       : バッチ共通終了処理
rem 引数         : 引数(%1):正常か異常のフラグ　9：異常　9以外正常
rem              : 引数(%2)に/Aがあると %Mailto_GroupA% へメール送信
rem              : 引数(%2)に/Bがあると %Mailto_GroupB% へメール送信
rem              : 引数(%2)を指定しない場合は、メールを送信しない
rem 注意事項     : 処理内容により複数のメール送信先を切り分ける場合に
rem                親バッチ側で引数(%2)の値を指定する(引数の値は変更可)
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-#####  ＃＃＃＃＃＃＃＃       変更
rem ***************************************************************************
echo %LINE02% >> %LOGFILE%

rem ---------------- Mail宛先の設定 --------------------
rem --宛先が3種類以上の場合はケース(引数の値)を追加してください
if /I "%2"=="/A" (
 set MAIL_ADDRESS=%Mailto_GroupA%
) else if /I "%2"=="/B" (
 set MAIL_ADDRESS=%Mailto_GroupB%
) else (
 rem デフォルト送付先
 set MAIL_ADDRESS=%Mailto_GroupA%
)

if %1 == 9 (
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% ■■異常終了■■⇒ %%" >> %LOGFILE%
  rem ---------------- Send Mail --------------------
  call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "2" "処理が異常終了しました"
) else (
  %ATIME_CMD% -L|%AEXE_CMD% "ECHO %BAT_ID% %BAT_NAME% □□正常終了□□⇒ %%" >> %LOGFILE%
  if NOT "%2" == "" (
  rem ---------------- Send Mail --------------------
  call %MAIL_CMD% "%MAIL_ADDRESS%" "%COMPUTERNAME%" "%BAT_ID%(%BAT_NAME%)" "1" "処理が正常終了しました"
  )
)

rem echo %LINE01% >> %LOGFILE%
rem ---------------- End Of Batch ---------------------------------------------

