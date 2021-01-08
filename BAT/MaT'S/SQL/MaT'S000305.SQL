SELECT
	code
	,meisyou
FROM 
	m_keikakuyou_meisyou with(readuncommitted)
WHERE 
	meisyou_syubetu = '05' --‰c‹Æ‹æ•ª
