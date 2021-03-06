SELECT
	k.kameiten_cd 加盟店ｺｰﾄﾞ 
	,k.torikesi 取消 
	,k.hattyuu_teisi_flg 発注停止FLG 
	,CASE WHEN k.kbn in ('A','C') THEN --区分が　A、C　の場合   「FC」 
		'FC' 
	ELSE 
		case when k.keiretu_cd in (@strKeiretuCd) then  
			'特販'  
		else  
			CASE WHEN k.kbn in (@strKubun) THEN 
				'営業'  
			END 
		end  
	END 営業区分 
	,k.kameiten_mei1 加盟店名 
	,case when b1.sosiki_level=4 then b1.busyo_mei else b2.busyo_mei end 支店名/*直轄部署が支店ではないときは、上位部署を取得*/ 
	,case when b1.sosiki_level=4 then b1.busyo_cd else b2.busyo_cd end 部署ｺｰﾄﾞ 
	,k.eigyou_tantousya_mei 営業担当者  
	,mb.displayname 営業担当者名 
	,NULL 必要面談回数 
	,case when b1.sosiki_level=5 then b1.busyo_cd else null end 営業所_部署ｺｰﾄﾞ 
	,case when b1.sosiki_level=5 then b1.busyo_mei else null end 営業所名/*直轄部署が支店ではないときは、nullをセット*/ 
	,k.todouhuken_cd 都道府県ｺｰﾄﾞ 
	,f.todouhuken_mei 都道府県名 
	,k.keiretu_cd 系列ｺｰﾄﾞ 
	,ke.keiretu_mei 系列名 
	,k.eigyousyo_cd 営業所ｺｰﾄﾞ 
	,k.nenkan_tousuu 年間棟数 
	,NULL 前年実績_年間棟数 
	,null 計画値0FLG 
	,KJ.add_nengetu 加盟店登録年月 
	,CASE WHEN KJ.add_nengetu IS NULL THEN
		NULL
	ELSE
		CASE WHEN (REPLACE(KJ.add_nengetu,'/','') + '01') BETWEEN @beginYear AND @endYear THEN 
			'1'  
		ELSE  
			'0'  
		END 
	END 加盟店新規FLG  
	,isnull(tmax.naiyou,'') 業態 
	,null 分譲上位50社FLG 
	,null 注文上位50社FLG 
	,null 併売上位50社FLG 
	,0 計画値不変FLG 
	,k.add_login_user_id 登録ID 
	,k.add_datetime 登録日時 
	,k.upd_login_user_id 更新ID 
	,k.upd_datetime 更新日時 
from m_kameiten k with(readuncommitted) 
LEFT JOIN m_kameiten_jyuusyo KJ  
	ON KJ.kameiten_cd = K.kameiten_cd 
	AND KJ.jyuusyo_no = '1' 
LEFT JOIN m_todoufuken f  
	ON f.todouhuken_cd = k.todouhuken_cd --都道府県マスタ 
LEFT JOIN m_busyo_kanri b1  
	ON b1.busyo_cd = f.busyo_cd /*都道府県の直轄部署*/ 
LEFT JOIN m_busyo_kanri b2  
	ON b2.busyo_cd = b1.joui_soiki /*直轄部署の上位組織部署（直轄部署が支店ではない場合、支店が取得できる）*/ 
LEFT JOIN m_jhs_mailbox mb with(readuncommitted)  
	ON k.eigyou_tantousya_mei=mb.aliasname 
LEFT JOIN m_keiretu ke --系列マスタ 
	ON ke.kbn = k.kbn 
	AND ke.keiretu_cd = k.keiretu_cd  
/*加盟店注意事項マスタの注意事項種別61の最大入力NOの内容取得。100以下・100超注文・100超分譲・HMが取得できる*/ 
LEFT JOIN ( 
	 select t.kameiten_cd,naiyou from m_kameiten_tyuuijikou t with(readuncommitted)  
	 inner join ( 
		 select kameiten_cd,max(nyuuryoku_no) maxno from m_kameiten_tyuuijikou with(readuncommitted)  
		 where tyuuijikou_syubetu='61' group by kameiten_cd 
	 ) t61 on t.kameiten_cd=t61.kameiten_cd and t.nyuuryoku_no=t61.maxno 
) tmax on k.kameiten_cd=tmax.kameiten_cd 
where 
	    k.kbn in ('A','C') /*FCの場合*/ 
		OR k.keiretu_cd in (@strKeiretuCd) /*特販の場合*/ 
							/*系列'TAMA','REOH','LEOP','ACEH','0001','6100','6800'はE・G・Sしかないので今はこれでOK（今後は以外の系列も追加になる可能性有なので注意）*/ 
		OR k.kbn in (@strKubun) /*営業の場合*/ 
