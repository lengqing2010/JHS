SELECT 
	k.kameiten_cd, 
	s.syouhin_syubetu1 AS syouhin_cd, 
	CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END),0) = 0 THEN 0 ELSE 
	ISNULL(SUM(urisiire.uri_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) END AS heikin_tanka, 
	CASE WHEN ISNULL(SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END),0) = 0 THEN 0 ELSE 
	ISNULL(SUM(urisiire.siire_gaku),0) / SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) END AS siire_heikin_tanka 
FROM 
	m_kameiten k WITH(READUNCOMMITTED) 
	LEFT JOIN 
	( 
		SELECT 
			kameiten_cd, 
			u.syouhin_cd, 
			uri_gaku, 
			u.suu uri_suu, 
			0 siire_gaku, 
			0 siire_suu, 
			ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  
		FROM 
			t_uriage_data u WITH(READUNCOMMITTED) 
		WHERE 
			u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND u.himoduke_table_type=1 
		UNION ALL 
		SELECT 
			SUBSTRING(u.himoduke_cd,1,5), 
			u.syouhin_cd, 
			uri_gaku, 
			u.suu uri_suu, 
			0 siire_gaku, 
			0 siire_suu, 
			'' AS bukken_bangou  
		FROM 
			t_uriage_data u WITH(READUNCOMMITTED) 
		WHERE 
			u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND u.himoduke_table_type=2  
			AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' 
		UNION ALL 
		SELECT 
			SUBSTRING(u.himoduke_cd,1,5), 
			u.syouhin_cd, 
			uri_gaku, 
			u.suu uri_suu, 
			0 siire_gaku, 
			0 siire_suu, 
			'' AS bukken_bangou  
		FROM 
			t_uriage_data u WITH(READUNCOMMITTED) 
			INNER JOIN 
			t_jiban j  
				ON 
				u.kbn=j.kbn  
				AND u.bangou=j.hosyousyo_no 
		WHERE 
			u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND u.himoduke_table_type=3 
		UNION ALL 
		SELECT 
			j.kameiten_cd, 
			u.syouhin_cd, 
			uri_gaku, 
			u.suu uri_suu, 
			0 siire_gaku, 
			0 siire_suu, 
			ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou  
		FROM 
			t_uriage_data u WITH(READUNCOMMITTED) 
			INNER JOIN 
			t_jiban j  
				ON 
				u.kbn=j.kbn  
				AND u.bangou=j.hosyousyo_no 
		WHERE 
			u.denpyou_uri_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND u.himoduke_table_type=9 
		UNION ALL  
		SELECT 
			j.kameiten_cd, 
			s.syouhin_cd, 
			0 uri_gaku, 
			0 uri_suu, 
			siire_gaku, 
			s.suu siire_suu, 
			ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  
		FROM 
			t_siire_data s WITH(READUNCOMMITTED) 
			INNER JOIN 
			t_jiban j WITH(READUNCOMMITTED)  
				ON 
				s.kbn=j.kbn  
				AND s.bangou=j.hosyousyo_no 
		WHERE 
			s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND s.himoduke_table_type=1 
		UNION ALL  
		SELECT 
			ISNULL(h.kameiten_cd,j.kameiten_cd), 
			s.syouhin_cd, 
			0 uri_gaku, 
			0 uri_suu, 
			siire_gaku, 
			s.suu siire_suu, 
			ISNULL(s.kbn,'')+ISNULL(s.bangou,'') AS bukken_bangou  
		FROM 
			t_siire_data s WITH(READUNCOMMITTED) 
			INNER JOIN 
			t_hannyou_siire h WITH(READUNCOMMITTED)  
				ON 
				s.himoduke_cd=h.han_siire_unique_no 
			LEFT JOIN 
			t_jiban j  
				ON 
				s.kbn=j.kbn  
				and s.bangou=j.hosyousyo_no 
		WHERE 
			s.denpyou_siire_date BETWEEN DATEADD(YYYY,-1,@beginYear) AND DATEADD(YYYY,-1,@endYear) 
			AND s.himoduke_table_type=9 
	) urisiire   
		ON 
		k.kameiten_cd=urisiire.kameiten_cd 
	INNER JOIN 
	m_syouhin s WITH(READUNCOMMITTED)  
		ON 
		urisiire.syouhin_cd=s.syouhin_cd 
	INNER JOIN 
	m_kakutyou_meisyou km  
		ON 
		s.syouhin_syubetu1=km.code  
		AND km.meisyou_syubetu='51' 
	LEFT JOIN 
	t_teibetu_seikyuu t WITH(READUNCOMMITTED) 
		ON 
		urisiire.bukken_bangou=t.kbn+t.hosyousyo_no 
		AND s.souko_cd='140' 
		AND t.bunrui_cd='130' 
		AND t.uri_gaku<>0 
		AND t.denpyou_uri_date IS NOT NULL 
WHERE 
	ISNULL(s.syouhin_syubetu1,'')<>''  
	AND k.kbn IN (@strKubun)  
GROUP BY 
	k.kameiten_cd, 
	s.syouhin_syubetu1 
ORDER BY 
	k.kameiten_cd, 
	s.syouhin_syubetu1 
