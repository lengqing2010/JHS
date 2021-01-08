select  
  k.kameiten_cd �����X����   
  ,k.kameiten_mei1 �����X��   
  ,case when isnull(usum_eigyo.urigaku,0)=0 then 
	   0  
   else  
	   case when convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0)*100,1)  
	   end 
   end ����䗦�c�ƃ}��   
  ,case when isnull(kaiseki_cnt.cnt,0)=0 then 
       0  
   else  
	   case when convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0)*100,1)  
	   end 
   end �H�����藦   
  ,case when isnull(koj_cnt.cnt,0)=0 then  
	   0  
   else  
	   case when convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0)*100,1)  
	   end 
   end �H���󒍗�   
  ,case when isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)=0 then  
	   0  
   else  
	   case when convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))*100,1)  
	   end 
   end ���H����   
from m_kameiten k with(readuncommitted)   
/*2011�N�x�E�Ώۏ��i�̔�����z�������X���ƂɏW�v*/   
left join (   
	select kameiten_cd,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    
	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   
	where denpyou_uri_date @strKikan  
	and isnull(s.syouhin_syubetu1,'')<>''    
	group by kameiten_cd   
) usum on k.kameiten_cd=usum.kameiten_cd   
/*2011�N�x�E�Ώۏ��i�̔�����z���c�ƒS�����ƂɏW�v 
�i�c�ƒS�������������X�ɂ͓����l���Z�b�g�����E�u����䗦�c�ƃ}���v�̌v�Z�ɗ��p�j*/   
left join (   
	select eigyou_tantousya_mei,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    
	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   
	inner join m_kameiten k with(readuncommitted) on u.kameiten_cd=k.kameiten_cd   
	where denpyou_uri_date @strKikan   
	and isnull(s.syouhin_syubetu1,'')<>''    
	group by eigyou_tantousya_mei   
) usum_eigyo on k.eigyou_tantousya_mei=usum_eigyo.eigyou_tantousya_mei   
/*2011�N�x�E�H�����藦�v�Z���p���i�̔��㌏���i���蔻�蕨���������j�������X���ƂɏW�v 
�i�u�H�����藦�v�̌v�Z�ɗ��p�j*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where isnull(s.syouhin_syubetu1,'') in ('Ke1001','Ke1002','Ke1004','Ke1006','Ke1007','Ke1008','Ke1011','Ke1012')    
	and t.uri_date @strKikan     
	and hantei_cd1 not in(97,113,1635)   
	group by j.kameiten_cd   
) kaiseki_cnt on k.kameiten_cd=kaiseki_cnt.kameiten_cd   
/*2011�N�x�E�H�����藦�v�Z���p���i�E�H�����茋��FLG=1�̔��㌏���������X���ƂɏW�v 
�i�u�H�����藦�v�A�u�H���󒍗��v�̌v�Z�ɗ��p�j*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where isnull(s.syouhin_syubetu1,'') in ('Ke1001','Ke1002','Ke1004','Ke1006','Ke1007','Ke1008','Ke1011','Ke1012')    
	and t.uri_date @strKikan     
	and j.koj_hantei_kekka_flg=1   
	group by j.kameiten_cd   
) koj_cnt on k.kameiten_cd=koj_cnt.kameiten_cd   
/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v 
�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where t.bunrui_cd='130'  
	and isnull(s.syouhin_syubetu1,'') = 'Ke2001'    
	and t.uri_date @strKikan    
	group by j.kameiten_cd   
) JHS_koj_cnt on k.kameiten_cd=JHS_koj_cnt.kameiten_cd   
/*2011�N�x�E�H���Ώۏ��i�i���޺���130�j�̔��㌏���������X���ƂɏW�v 
�i�u�H���󒍗��v�A�u���H�����v�̌v�Z�ɗ��p�j*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where t.bunrui_cd='130'  
	and isnull(s.syouhin_syubetu1,'') = 'Ke2002'    
	and t.uri_date @strKikan 
	group by j.kameiten_cd   
) tyoku_koj_cnt on k.kameiten_cd=tyoku_koj_cnt.kameiten_cd   
where k.kbn in (@strKubun) 
/*�n��'TAMA','REOH','LEOP','ACEH','0001','6100','6800'��E�EG�ES�����Ȃ��̂ō��͂����OK 
�i����͈ȊO�̌n����ǉ��ɂȂ�\���L�Ȃ̂Œ��Ӂj*/   