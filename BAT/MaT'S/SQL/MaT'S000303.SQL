delete from
	m_keikaku_kameiten
where 
	keikaku_nendo = @year
	and isnull(keikaku_huhen_flg,0) <> 1 --Œv‰æ’l•s•ÏFLG
