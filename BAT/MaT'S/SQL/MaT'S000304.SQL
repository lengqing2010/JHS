SELECT
	k.kameiten_cd
FROM 
	m_keikaku_kameiten k with(readuncommitted)
WHERE
	k.keikaku_nendo = @year
	AND k.keikaku_huhen_flg = 1 --Œv‰æ’l•s•ÏFLG
