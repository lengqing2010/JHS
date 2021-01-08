select  
  k.kameiten_cd 加盟店ｺｰﾄﾞ   
  ,k.kameiten_mei1 加盟店名   
  ,case when isnull(usum_eigyo.urigaku,0)=0 then 
	   0  
   else  
	   case when convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(usum.urigaku,0))/isnull(usum_eigyo.urigaku,0)*100,1)  
	   end 
   end 売上比率営業マン   
  ,case when isnull(kaiseki_cnt.cnt,0)=0 then 
       0  
   else  
	   case when convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(koj_cnt.cnt,0))/isnull(kaiseki_cnt.cnt,0)*100,1)  
	   end 
   end 工事判定率   
  ,case when isnull(koj_cnt.cnt,0)=0 then  
	   0  
   else  
	   case when convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))/isnull(koj_cnt.cnt,0)*100,1)  
	   end 
   end 工事受注率   
  ,case when isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)=0 then  
	   0  
   else  
	   case when convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0)) >= 1 then 
	       100 
	   else  
	       round(convert(float,isnull(tyoku_koj_cnt.cnt,0))/(isnull(JHS_koj_cnt.cnt,0)+isnull(tyoku_koj_cnt.cnt,0))*100,1)  
	   end 
   end 直工事率   
from m_kameiten k with(readuncommitted)   
/*2011年度・対象商品の売上金額を加盟店ごとに集計*/   
left join (   
	select kameiten_cd,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    
	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   
	where denpyou_uri_date @strKikan  
	and isnull(s.syouhin_syubetu1,'')<>''    
	group by kameiten_cd   
) usum on k.kameiten_cd=usum.kameiten_cd   
/*2011年度・対象商品の売上金額を営業担当ごとに集計 
（営業担当が同じ加盟店には同じ値がセットされる・「売上比率営業マン」の計算に利用）*/   
left join (   
	select eigyou_tantousya_mei,sum(uri_gaku) urigaku from t_uriage_data u with(readuncommitted)    
	inner join m_syouhin s with(readuncommitted) on u.syouhin_cd=s.syouhin_cd   
	inner join m_kameiten k with(readuncommitted) on u.kameiten_cd=k.kameiten_cd   
	where denpyou_uri_date @strKikan   
	and isnull(s.syouhin_syubetu1,'')<>''    
	group by eigyou_tantousya_mei   
) usum_eigyo on k.eigyou_tantousya_mei=usum_eigyo.eigyou_tantousya_mei   
/*2011年度・工事判定率計算利用商品の売上件数（特定判定物件を除く）を加盟店ごとに集計 
（「工事判定率」の計算に利用）*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where isnull(s.syouhin_syubetu1,'') in ('Ke1001','Ke1002','Ke1004','Ke1006','Ke1007','Ke1008','Ke1011','Ke1012')    
	and t.uri_date @strKikan     
	and hantei_cd1 not in(97,113,1635)   
	group by j.kameiten_cd   
) kaiseki_cnt on k.kameiten_cd=kaiseki_cnt.kameiten_cd   
/*2011年度・工事判定率計算利用商品・工事判定結果FLG=1の売上件数を加盟店ごとに集計 
（「工事判定率」、「工事受注率」の計算に利用）*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where isnull(s.syouhin_syubetu1,'') in ('Ke1001','Ke1002','Ke1004','Ke1006','Ke1007','Ke1008','Ke1011','Ke1012')    
	and t.uri_date @strKikan     
	and j.koj_hantei_kekka_flg=1   
	group by j.kameiten_cd   
) koj_cnt on k.kameiten_cd=koj_cnt.kameiten_cd   
/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計 
（「工事受注率」、「直工事率」の計算に利用）*/   
left join (   
	select j.kameiten_cd,count(j.kbn) cnt from t_jiban j with(readuncommitted)    
	inner join t_teibetu_seikyuu t with(readuncommitted) on j.kbn=t.kbn and j.hosyousyo_no=t.hosyousyo_no   
	inner join m_syouhin s with(readuncommitted) on t.syouhin_cd=s.syouhin_cd    
	where t.bunrui_cd='130'  
	and isnull(s.syouhin_syubetu1,'') = 'Ke2001'    
	and t.uri_date @strKikan    
	group by j.kameiten_cd   
) JHS_koj_cnt on k.kameiten_cd=JHS_koj_cnt.kameiten_cd   
/*2011年度・工事対象商品（分類ｺｰﾄﾞ130）の売上件数を加盟店ごとに集計 
（「工事受注率」、「直工事率」の計算に利用）*/   
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
/*系列'TAMA','REOH','LEOP','ACEH','0001','6100','6800'はE・G・Sしかないので今はこれでOK 
（今後は以外の系列も追加になる可能性有なので注意）*/   
