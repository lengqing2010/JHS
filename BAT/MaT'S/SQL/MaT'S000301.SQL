SELECT
	k.kameiten_cd �����X���� 
	,k.torikesi ��� 
	,k.hattyuu_teisi_flg ������~FLG 
	,CASE WHEN k.kbn in ('A','C') THEN --�敪���@A�AC�@�̏ꍇ   �uFC�v 
		'FC' 
	ELSE 
		case when k.keiretu_cd in (@strKeiretuCd) then  
			'����'  
		else  
			CASE WHEN k.kbn in (@strKubun) THEN 
				'�c��'  
			END 
		end  
	END �c�Ƌ敪 
	,k.kameiten_mei1 �����X�� 
	,case when b1.sosiki_level=4 then b1.busyo_mei else b2.busyo_mei end �x�X��/*�����������x�X�ł͂Ȃ��Ƃ��́A��ʕ������擾*/ 
	,case when b1.sosiki_level=4 then b1.busyo_cd else b2.busyo_cd end �������� 
	,k.eigyou_tantousya_mei �c�ƒS����  
	,mb.displayname �c�ƒS���Җ� 
	,NULL �K�v�ʒk�� 
	,case when b1.sosiki_level=5 then b1.busyo_cd else null end �c�Ə�_�������� 
	,case when b1.sosiki_level=5 then b1.busyo_mei else null end �c�Ə���/*�����������x�X�ł͂Ȃ��Ƃ��́Anull���Z�b�g*/ 
	,k.todouhuken_cd �s���{������ 
	,f.todouhuken_mei �s���{���� 
	,k.keiretu_cd �n���� 
	,ke.keiretu_mei �n�� 
	,k.eigyousyo_cd �c�Ə����� 
	,k.nenkan_tousuu �N�ԓ��� 
	,NULL �O�N����_�N�ԓ��� 
	,null �v��l0FLG 
	,KJ.add_nengetu �����X�o�^�N�� 
	,CASE WHEN KJ.add_nengetu IS NULL THEN
		NULL
	ELSE
		CASE WHEN (REPLACE(KJ.add_nengetu,'/','') + '01') BETWEEN @beginYear AND @endYear THEN 
			'1'  
		ELSE  
			'0'  
		END 
	END �����X�V�KFLG  
	,isnull(tmax.naiyou,'') �Ƒ� 
	,null �������50��FLG 
	,null �������50��FLG 
	,null �������50��FLG 
	,0 �v��l�s��FLG 
	,k.add_login_user_id �o�^ID 
	,k.add_datetime �o�^���� 
	,k.upd_login_user_id �X�VID 
	,k.upd_datetime �X�V���� 
from m_kameiten k with(readuncommitted) 
LEFT JOIN m_kameiten_jyuusyo KJ  
	ON KJ.kameiten_cd = K.kameiten_cd 
	AND KJ.jyuusyo_no = '1' 
LEFT JOIN m_todoufuken f  
	ON f.todouhuken_cd = k.todouhuken_cd --�s���{���}�X�^ 
LEFT JOIN m_busyo_kanri b1  
	ON b1.busyo_cd = f.busyo_cd /*�s���{���̒�������*/ 
LEFT JOIN m_busyo_kanri b2  
	ON b2.busyo_cd = b1.joui_soiki /*���������̏�ʑg�D�����i�����������x�X�ł͂Ȃ��ꍇ�A�x�X���擾�ł���j*/ 
LEFT JOIN m_jhs_mailbox mb with(readuncommitted)  
	ON k.eigyou_tantousya_mei=mb.aliasname 
LEFT JOIN m_keiretu ke --�n��}�X�^ 
	ON ke.kbn = k.kbn 
	AND ke.keiretu_cd = k.keiretu_cd  
/*�����X���ӎ����}�X�^�̒��ӎ������61�̍ő����NO�̓��e�擾�B100�ȉ��E100�������E100�������EHM���擾�ł���*/ 
LEFT JOIN ( 
	 select t.kameiten_cd,naiyou from m_kameiten_tyuuijikou t with(readuncommitted)  
	 inner join ( 
		 select kameiten_cd,max(nyuuryoku_no) maxno from m_kameiten_tyuuijikou with(readuncommitted)  
		 where tyuuijikou_syubetu='61' group by kameiten_cd 
	 ) t61 on t.kameiten_cd=t61.kameiten_cd and t.nyuuryoku_no=t61.maxno 
) tmax on k.kameiten_cd=tmax.kameiten_cd 
where 
	    k.kbn in ('A','C') /*FC�̏ꍇ*/ 
		OR k.keiretu_cd in (@strKeiretuCd) /*���̂̏ꍇ*/ 
							/*�n��'TAMA','REOH','LEOP','ACEH','0001','6100','6800'��E�EG�ES�����Ȃ��̂ō��͂����OK�i����͈ȊO�̌n����ǉ��ɂȂ�\���L�Ȃ̂Œ��Ӂj*/ 
		OR k.kbn in (@strKubun) /*�c�Ƃ̏ꍇ*/ 