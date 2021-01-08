@echo off
rem ***************************************************************************
rem システム名   : JHS顧客管理システム
rem モジュール名 : SET_ENV.BAT
rem 処理名       : 環境変数の設定処理
rem 注意事項     : BATスクリプトの先頭でcallしてください
rem              : ※本番環境と開発環境で設定値が異なる変数は以下になります
rem              : (例)DAT_PATH環境変数が開発環境ではEドライブになります
rem
rem Date        EDP-No   所属/氏名              内   容
rem ---------------------------------------------------------------------------
rem 2013/01/08  P-45026  楊双(大連)          新規作成
rem yyyy/mm/dd  P-xxxxx  開発X部/○○           変更
rem ***************************************************************************

rem ===========================================================================
rem ディレクトリ定義
rem ---------------------------------------------------------------------------
set AUTO_BAT_PATH=H:\BAT\AUTO
set BAT_PATH=H:\BAT\KensaHoukokusyo_db_Upload\BAT
set BCP_PATH=H:\BAT\KensaHoukokusyo_db_Upload\BCP
set DAT_PATH=H:\BAT\KensaHoukokusyo_db_Upload\DATA
set FTP_PATH=H:\BAT\KensaHoukokusyo_db_Upload\FTP
set LOG_PATH=H:\BAT\KensaHoukokusyo_db_Upload\LOG
set INC_PATH=H:\BAT\KensaHoukokusyo_db_Upload\INC
set SQL_PATH=H:\BAT\KensaHoukokusyo_db_Upload\SQL
set VBS_PATH=H:\BAT\KensaHoukokusyo_db_Upload\VBS
set EXE_PATH=H:\BAT\KensaHoukokusyo_db_Upload\EXE
rem set AUTO_LOG_PATH=C:\BSP\AUW\JOBLOG
set TOOL_PATH=H:\BAT\KensaHoukokusyo_db_Upload\TOOLS
rem ===========================================================================

rem ===========================================================================
rem 標準子バッチ 定義
rem ---------------------------------------------------------------------------
set FTP_BAT=%BAT_PATH%\FTP0010A.BAT
set OSQL_BAT=%BAT_PATH%\SQL0010A.BAT
set OSQL_BAT2=%BAT_PATH%\SQL0020A.BAT
set DCP_BAT=%BAT_PATH%\DCP0010A.BAT
set BCP_IN_BAT=%BAT_PATH%\BCP0010A.BAT
set BCP_OUT_BAT=%BAT_PATH%\BCP0020A.BAT
set BCP_FMT_BAT=%BAT_PATH%\FMT0010A.BAT
set BCP_FMTIN_BAT=%BAT_PATH%\FMT0020A.BAT

rem ===========================================================================
rem アプリケーション・ツール 定義
rem ---------------------------------------------------------------------------
set ATIME_CMD=%TOOL_PATH%\ATIME\ATIME.EXE
set AEXE_CMD=%TOOL_PATH%\AEXE\AEXE.EXE
set AWK_CMD=%TOOL_PATH%\Gawk\Gawk.EXE
set CGREP_CMD=%TOOL_PATH%\CGREP\CGREP.EXE
set TEE_CMD=%TOOL_PATH%\TEE\TEE.EXE
set BLATJ_CMD=%TOOL_PATH%\BLATJ\BLATJ.EXE
set GAWK_CMD=%TOOL_PATH%\GAWK\GAWK.EXE
set DELOLD_CMD=%VBS_PATH%\DELOLD.VBS
set MAIL_CMD=%VBS_PATH%\SEND_MAIL.wsf
set BACKUP_CMD=%EXE_PATH%\mFileBackup.exe

rem *************************************************
rem DCP は商用製品であるため、S運用部を通して必ず購入してください
rem *************************************************
rem set DCP_CMD=%TOOL_PATH%\DCP\DCPEXP.EXE
rem set DCP_USERCODE=%TOOL_PATH%\DCP\USERCODE.TBL
rem set DCP_PRAM=-cr -code sjis -cnt -chg -table

rem ===========================================================================
rem DB接続 定義
rem ---------------------------------------------------------------------------
rem EARTHのＤＢ
REM テストＤＢ
set DBLoginNameEarth=sa
set DBLoginPassEarth=zaoriku
set DBNameEarth=JHS_EARTH
set DBServerEarth=dam498
rem set DBsaPass=

rem 報連相のＤＢ
set DATABASE=HRS_ICA178
set DBUSER=ASPSFA_ADMIN
set DBPASS=ASPSFA_ADMIN
set DBOWNER=ASPSFA_ADMIN

rem JHSのＤＢ
set DBLoginNameJHS=yangs1
set DBLoginPassJHS=123
set DBNameJHS=JHS_EKKS
set DBServerJHS=DAM104
rem set DBLoginNameJHS=jhs_sys
rem set DBLoginPassJHS=jhs_sys
rem set DBNameJHS=[JHS-MATStest]
rem set DBServerJHS=B55008
rem set DBsaPass=
set DBProvider=SQLOLEDB

rem ===========================================================================
rem FTP接続先
rem   必ず %SystemRoot\system32\drivers\etc\hosts ファイルを準備すること
rem   接続先が複数必要な場合は、FtpHost_ZZZ を複数定義してください
rem ---------------------------------------------------------------------------
rem set FtpHost_TOS1="ibmhost1 4021"
rem set FtpHost_Win1="winserver1"
rem set FtpHost_Unix1="unixserver1"
rem 本番環境
rem  set FtpHost_TOS4=""192.100.31.253 4021""
rem 検証環境
rem set FtpHost_TOS4=""192.100.31.251 4021""
rem 大連テスト環境
rem set FtpHost_TOS4="192.168.55.9 21"

rem ===========================================================================
rem Mail送信先
rem   MailTo_GroupA : 送信先一覧A
rem   MailTo_GroupB : 送信先一覧B
rem ---------------------------------------------------------------------------
rem set MailTo_GroupA=##########@exc.tostem.co.jp;##########@exc.tostem.co.jp
rem set MailTo_GroupB=##########@exc.tostem.co.jp;##########@exc.tostem.co.jp

rem set MailTo_GroupA=yangs1@exc.tostem.co.jp
rem set MailTo_GroupB=yangs1@exc.tostem.co.jp

rem ===========================================================================
rem その他の定義
rem ---------------------------------------------------------------------------
set LINE00=###########################################################################
set LINE01=***************************************************************************
set LINE02============================================================================
set LINE03=---------------------------------------------------------------------------
rem ---------------- End Of Batch ---------------------------------------------
