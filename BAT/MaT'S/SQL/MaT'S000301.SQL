SELECT
	k.kameiten_cd ‰Á–¿“Xº°ÄŞ 
	,k.torikesi æÁ 
	,k.hattyuu_teisi_flg ”­’’â~FLG 
	,CASE WHEN k.kbn in ('A','C') THEN --‹æ•ª‚ª@AAC@‚Ìê‡   uFCv 
		'FC' 
	ELSE 
		case when k.keiretu_cd in (@strKeiretuCd) then  
			'“Á”Ì'  
		else  
			CASE WHEN k.kbn in (@strKubun) THEN 
				'‰c‹Æ'  
			END 
		end  
	END ‰c‹Æ‹æ•ª 
	,k.kameiten_mei1 ‰Á–¿“X–¼ 
	,case when b1.sosiki_level=4 then b1.busyo_mei else b2.busyo_mei end x“X–¼/*’¼Š•”‚ªx“X‚Å‚Í‚È‚¢‚Æ‚«‚ÍAãˆÊ•”‚ğæ“¾*/ 
	,case when b1.sosiki_level=4 then b1.busyo_cd else b2.busyo_cd end •”º°ÄŞ 
	,k.eigyou_tantousya_mei ‰c‹Æ’S“–Ò  
	,mb.displayname ‰c‹Æ’S“–Ò–¼ 
	,NULL •K—v–Ê’k‰ñ” 
	,case when b1.sosiki_level=5 then b1.busyo_cd else null end ‰c‹ÆŠ_•”º°ÄŞ 
	,case when b1.sosiki_level=5 then b1.busyo_mei else null end ‰c‹ÆŠ–¼/*’¼Š•”‚ªx“X‚Å‚Í‚È‚¢‚Æ‚«‚ÍAnull‚ğƒZƒbƒg*/ 
	,k.todouhuken_cd “s“¹•{Œ§º°ÄŞ 
	,f.todouhuken_mei “s“¹•{Œ§–¼ 
	,k.keiretu_cd Œn—ñº°ÄŞ 
	,ke.keiretu_mei Œn—ñ–¼ 
	,k.eigyousyo_cd ‰c‹ÆŠº°ÄŞ 
	,k.nenkan_tousuu ”NŠÔ“” 
	,NULL ‘O”NÀÑ_”NŠÔ“” 
	,null Œv‰æ’l0FLG 
	,KJ.add_nengetu ‰Á–¿“X“o˜^”NŒ 
	,CASE WHEN KJ.add_nengetu IS NULL THEN
		NULL
	ELSE
		CASE WHEN (REPLACE(KJ.add_nengetu,'/','') + '01') BETWEEN @beginYear AND @endYear THEN 
			'1'  
		ELSE  
			'0'  
		END 
	END ‰Á–¿“XV‹KFLG  
	,isnull(tmax.naiyou,'') ‹Æ‘Ô 
	,null •ª÷ãˆÊ50ĞFLG 
	,null ’•¶ãˆÊ50ĞFLG 
	,null •¹”„ãˆÊ50ĞFLG 
	,0 Œv‰æ’l•s•ÏFLG 
	,k.add_login_user_id “o˜^ID 
	,k.add_datetime “o˜^“ú 
	,k.upd_login_user_id XVID 
	,k.upd_datetime XV“ú 
from m_kameiten k with(readuncommitted) 
LEFT JOIN m_kameiten_jyuusyo KJ  
	ON KJ.kameiten_cd = K.kameiten_cd 
	AND KJ.jyuusyo_no = '1' 
LEFT JOIN m_todoufuken f  
	ON f.todouhuken_cd = k.todouhuken_cd --“s“¹•{Œ§ƒ}ƒXƒ^ 
LEFT JOIN m_busyo_kanri b1  
	ON b1.busyo_cd = f.busyo_cd /*“s“¹•{Œ§‚Ì’¼Š•”*/ 
LEFT JOIN m_busyo_kanri b2  
	ON b2.busyo_cd = b1.joui_soiki /*’¼Š•”‚ÌãˆÊ‘gD•”i’¼Š•”‚ªx“X‚Å‚Í‚È‚¢ê‡Ax“X‚ªæ“¾‚Å‚«‚éj*/ 
LEFT JOIN m_jhs_mailbox mb with(readuncommitted)  
	ON k.eigyou_tantousya_mei=mb.aliasname 
LEFT JOIN m_keiretu ke --Œn—ñƒ}ƒXƒ^ 
	ON ke.kbn = k.kbn 
	AND ke.keiretu_cd = k.keiretu_cd  
/*‰Á–¿“X’ˆÓ–€ƒ}ƒXƒ^‚Ì’ˆÓ–€í•Ê61‚ÌÅ‘å“ü—ÍNO‚Ì“à—eæ“¾B100ˆÈ‰ºE100’´’•¶E100’´•ª÷EHM‚ªæ“¾‚Å‚«‚é*/ 
LEFT JOIN ( 
	 select t.kameiten_cd,naiyou from m_kameiten_tyuuijikou t with(readuncommitted)  
	 inner join ( 
		 select kameiten_cd,max(nyuuryoku_no) maxno from m_kameiten_tyuuijikou with(readuncommitted)  
		 where tyuuijikou_syubetu='61' group by kameiten_cd 
	 ) t61 on t.kameiten_cd=t61.kameiten_cd and t.nyuuryoku_no=t61.maxno 
) tmax on k.kameiten_cd=tmax.kameiten_cd 
where 
	    k.kbn in ('A','C') /*FC‚Ìê‡*/ 
		OR k.keiretu_cd in (@strKeiretuCd) /*“Á”Ì‚Ìê‡*/ 
							/*Œn—ñ'TAMA','REOH','LEOP','ACEH','0001','6100','6800'‚ÍEEGES‚µ‚©‚È‚¢‚Ì‚Å¡‚Í‚±‚ê‚ÅOKi¡Œã‚ÍˆÈŠO‚ÌŒn—ñ‚à’Ç‰Á‚É‚È‚é‰Â”\«—L‚È‚Ì‚Å’ˆÓj*/ 
		OR k.kbn in (@strKubun) /*‰c‹Æ‚Ìê‡*/ 
