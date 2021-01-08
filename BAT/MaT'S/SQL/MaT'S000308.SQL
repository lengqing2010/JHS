SELECT
	count(k.kameiten_cd)
FROM 
	m_keikaku_kameiten k with(readuncommitted)
WHERE
	k.keikaku_nendo = @year
	AND k.kameiten_cd = @kameiten_cd
