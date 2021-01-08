SELECT 
	isnull([4gatu_keikaku_kensuu],0) + isnull([5gatu_keikaku_kensuu],0) +isnull([6gatu_keikaku_kensuu],0)
	+isnull([7gatu_keikaku_kensuu],0) + isnull([8gatu_keikaku_kensuu],0) +isnull([9gatu_keikaku_kensuu],0)
	+isnull([10gatu_keikaku_kensuu],0) + isnull([11gatu_keikaku_kensuu],0) +isnull([12gatu_keikaku_kensuu],0)
	+isnull([1gatu_keikaku_kensuu],0) + isnull([2gatu_keikaku_kensuu],0) +isnull([3gatu_keikaku_kensuu],0)
	as keikaku_kensuu
FROM 
	t_keikaku_kanri with(readuncommitted)
WHERE 
	keikaku_nendo = @keikaku_nendo
	AND kameiten_cd = @kameiten_cd
