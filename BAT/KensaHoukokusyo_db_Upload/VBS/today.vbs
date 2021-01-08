dim v_today
v_today = dateadd( "d", 0, now() )
WScript.Echo( year(v_today) & right(100 + month(v_today),2) & right(100 + day(v_today),2) )
