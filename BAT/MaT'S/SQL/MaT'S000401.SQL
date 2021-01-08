SELECT 
	kameiten_mei1, 
	kameiten_cd, 
	syouhin_cd, 
	syouhin_mei, 
	SUM(uri_gaku4) AS uri_gaku4, 
	SUM(uri_gaku5) AS uri_gaku5, 
	SUM(uri_gaku6) AS uri_gaku6, 
	SUM(uri_gaku7) AS uri_gaku7, 
	SUM(uri_gaku8) AS uri_gaku8, 
	SUM(uri_gaku9) AS uri_gaku9, 
	SUM(uri_gaku10) AS uri_gaku10, 
	SUM(uri_gaku11) AS uri_gaku11, 
	SUM(uri_gaku12) AS uri_gaku12, 
	SUM(uri_gaku1) AS uri_gaku1, 
	SUM(uri_gaku2) AS uri_gaku2, 
	SUM(uri_gaku3) AS uri_gaku3, 
	SUM(uri_suu4) AS uri_suu4, 
	SUM(uri_suu5) AS uri_suu5, 
	SUM(uri_suu6) AS uri_suu6, 
	SUM(uri_suu7) AS uri_suu7, 
	SUM(uri_suu8) AS uri_suu8, 
	SUM(uri_suu9) AS uri_suu9, 
	SUM(uri_suu10) AS uri_suu10, 
	SUM(uri_suu11) AS uri_suu11, 
	SUM(uri_suu12) AS uri_suu12, 
	SUM(uri_suu1) AS uri_suu1, 
	SUM(uri_suu2) AS uri_suu2, 
	SUM(uri_suu3) AS uri_suu3, 
	SUM(siire_gaku4) AS siire_gaku4, 
	SUM(siire_gaku5) AS siire_gaku5, 
	SUM(siire_gaku6) AS siire_gaku6, 
	SUM(siire_gaku7) AS siire_gaku7, 
	SUM(siire_gaku8) AS siire_gaku8, 
	SUM(siire_gaku9) AS siire_gaku9, 
	SUM(siire_gaku10) AS siire_gaku10, 
	SUM(siire_gaku11) AS siire_gaku11, 
	SUM(siire_gaku12) AS siire_gaku12, 
	SUM(siire_gaku1) AS siire_gaku1, 
	SUM(siire_gaku2) AS siire_gaku2, 
	SUM(siire_gaku3) AS siire_gaku3, 
	SUM(siire_suu4) AS siire_suu4, 
	SUM(siire_suu5) AS siire_suu5, 
	SUM(siire_suu6) AS siire_suu6, 
	SUM(siire_suu7) AS siire_suu7, 
	SUM(siire_suu8) AS siire_suu8, 
	SUM(siire_suu9) AS siire_suu9, 
	SUM(siire_suu10) AS siire_suu10, 
	SUM(siire_suu11) AS siire_suu11, 
	SUM(siire_suu12) AS siire_suu12, 
	SUM(siire_suu1) AS siire_suu1, 
	SUM(siire_suu2) AS siire_suu2, 
	SUM(siire_suu3) AS siire_suu3, 
	ISNULL(SUM(uri_gaku4),0) - ISNULL(SUM(siire_gaku4),0) AS uri_arari4, 
	ISNULL(SUM(uri_gaku5),0) - ISNULL(SUM(siire_gaku5),0) AS uri_arari5, 
	ISNULL(SUM(uri_gaku6),0) - ISNULL(SUM(siire_gaku6),0) AS uri_arari6, 
	ISNULL(SUM(uri_gaku7),0) - ISNULL(SUM(siire_gaku7),0) AS uri_arari7, 
	ISNULL(SUM(uri_gaku8),0) - ISNULL(SUM(siire_gaku8),0) AS uri_arari8, 
	ISNULL(SUM(uri_gaku9),0) - ISNULL(SUM(siire_gaku9),0) AS uri_arari9, 
	ISNULL(SUM(uri_gaku10),0) - ISNULL(SUM(siire_gaku10),0) AS uri_arari10, 
	ISNULL(SUM(uri_gaku11),0) - ISNULL(SUM(siire_gaku11),0) AS uri_arari11, 
	ISNULL(SUM(uri_gaku12),0) - ISNULL(SUM(siire_gaku12),0) AS uri_arari12, 
	ISNULL(SUM(uri_gaku1),0) - ISNULL(SUM(siire_gaku1),0) AS uri_arari1, 
	ISNULL(SUM(uri_gaku2),0) - ISNULL(SUM(siire_gaku2),0) AS uri_arari2, 
	ISNULL(SUM(uri_gaku3),0) - ISNULL(SUM(siire_gaku3),0) AS uri_arari3, 
	0 AS zennen_heikin_tanka, 
	0 AS zennen_siire_heikin_tanka 
FROM 
	( 
		SELECT 
			kameiten_mei1, 
			kameiten_cd, 
			syouhin_cd, 
			syouhin_mei, 
			CASE WHEN denpyou_date = '/4' THEN uri_gaku ELSE 0 END AS uri_gaku4, 
			CASE WHEN denpyou_date = '/5' THEN uri_gaku ELSE 0 END AS uri_gaku5, 
			CASE WHEN denpyou_date = '/6' THEN uri_gaku ELSE 0 END AS uri_gaku6, 
			CASE WHEN denpyou_date = '/7' THEN uri_gaku ELSE 0 END AS uri_gaku7, 
			CASE WHEN denpyou_date = '/8' THEN uri_gaku ELSE 0 END AS uri_gaku8, 
			CASE WHEN denpyou_date = '/9' THEN uri_gaku ELSE 0 END AS uri_gaku9, 
			CASE WHEN denpyou_date = '/10' THEN uri_gaku ELSE 0 END AS uri_gaku10, 
			CASE WHEN denpyou_date = '/11' THEN uri_gaku ELSE 0 END AS uri_gaku11, 
			CASE WHEN denpyou_date = '/12' THEN uri_gaku ELSE 0 END AS uri_gaku12, 
			CASE WHEN denpyou_date = '/1' THEN uri_gaku ELSE 0 END AS uri_gaku1, 
			CASE WHEN denpyou_date = '/2' THEN uri_gaku ELSE 0 END AS uri_gaku2, 
			CASE WHEN denpyou_date = '/3' THEN uri_gaku ELSE 0 END AS uri_gaku3, 
			CASE WHEN denpyou_date = '/4' THEN uri_suu ELSE 0 END AS uri_suu4, 
			CASE WHEN denpyou_date = '/5' THEN uri_suu ELSE 0 END AS uri_suu5, 
			CASE WHEN denpyou_date = '/6' THEN uri_suu ELSE 0 END AS uri_suu6, 
			CASE WHEN denpyou_date = '/7' THEN uri_suu ELSE 0 END AS uri_suu7, 
			CASE WHEN denpyou_date = '/8' THEN uri_suu ELSE 0 END AS uri_suu8, 
			CASE WHEN denpyou_date = '/9' THEN uri_suu ELSE 0 END AS uri_suu9, 
			CASE WHEN denpyou_date = '/10' THEN uri_suu ELSE 0 END AS uri_suu10, 
			CASE WHEN denpyou_date = '/11' THEN uri_suu ELSE 0 END AS uri_suu11, 
			CASE WHEN denpyou_date = '/12' THEN uri_suu ELSE 0 END AS uri_suu12, 
			CASE WHEN denpyou_date = '/1' THEN uri_suu ELSE 0 END AS uri_suu1, 
			CASE WHEN denpyou_date = '/2' THEN uri_suu ELSE 0 END AS uri_suu2, 
			CASE WHEN denpyou_date = '/3' THEN uri_suu ELSE 0 END AS uri_suu3, 
			CASE WHEN denpyou_date = '/4' THEN siire_gaku ELSE 0 END AS siire_gaku4, 
			CASE WHEN denpyou_date = '/5' THEN siire_gaku ELSE 0 END AS siire_gaku5, 
			CASE WHEN denpyou_date = '/6' THEN siire_gaku ELSE 0 END AS siire_gaku6, 
			CASE WHEN denpyou_date = '/7' THEN siire_gaku ELSE 0 END AS siire_gaku7, 
			CASE WHEN denpyou_date = '/8' THEN siire_gaku ELSE 0 END AS siire_gaku8, 
			CASE WHEN denpyou_date = '/9' THEN siire_gaku ELSE 0 END AS siire_gaku9, 
			CASE WHEN denpyou_date = '/10' THEN siire_gaku ELSE 0 END AS siire_gaku10, 
			CASE WHEN denpyou_date = '/11' THEN siire_gaku ELSE 0 END AS siire_gaku11, 
			CASE WHEN denpyou_date = '/12' THEN siire_gaku ELSE 0 END AS siire_gaku12, 
			CASE WHEN denpyou_date = '/1' THEN siire_gaku ELSE 0 END AS siire_gaku1, 
			CASE WHEN denpyou_date = '/2' THEN siire_gaku ELSE 0 END AS siire_gaku2, 
			CASE WHEN denpyou_date = '/3' THEN siire_gaku ELSE 0 END AS siire_gaku3, 
			CASE WHEN denpyou_date = '/4' THEN siire_suu ELSE 0 END AS siire_suu4, 
			CASE WHEN denpyou_date = '/5' THEN siire_suu ELSE 0 END AS siire_suu5, 
			CASE WHEN denpyou_date = '/6' THEN siire_suu ELSE 0 END AS siire_suu6, 
			CASE WHEN denpyou_date = '/7' THEN siire_suu ELSE 0 END AS siire_suu7, 
			CASE WHEN denpyou_date = '/8' THEN siire_suu ELSE 0 END AS siire_suu8, 
			CASE WHEN denpyou_date = '/9' THEN siire_suu ELSE 0 END AS siire_suu9, 
			CASE WHEN denpyou_date = '/10' THEN siire_suu ELSE 0 END AS siire_suu10, 
			CASE WHEN denpyou_date = '/11' THEN siire_suu ELSE 0 END AS siire_suu11, 
			CASE WHEN denpyou_date = '/12' THEN siire_suu ELSE 0 END AS siire_suu12, 
			CASE WHEN denpyou_date = '/1' THEN siire_suu ELSE 0 END AS siire_suu1, 
			CASE WHEN denpyou_date = '/2' THEN siire_suu ELSE 0 END AS siire_suu2, 
			CASE WHEN denpyou_date = '/3' THEN siire_suu ELSE 0 END AS siire_suu3 
		FROM 
			( 
				SELECT 
					k.kameiten_mei1, 
					k.kameiten_cd, 
					'/'+CONVERT(VARCHAR,MONTH(denpyou_date)) AS denpyou_date, 
					s.syouhin_syubetu1 AS syouhin_cd, 
					km.meisyou AS syouhin_mei, 
					SUM(urisiire.uri_gaku) AS uri_gaku, 
					SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE uri_suu END) AS uri_suu, 
					SUM(urisiire.siire_gaku) AS siire_gaku, 
					SUM(CASE WHEN (SUBSTRING(s.syouhin_syubetu1,1,3)='Ke1' AND s.syouhin_syubetu1<>'Ke1003' AND s.souko_cd<>'100') OR (s.souko_cd='140' AND t.kbn IS NOT NULL) THEN 0 ELSE siire_suu END) AS siire_suu 
				FROM 
					m_kameiten k WITH(READUNCOMMITTED) 
					LEFT JOIN 
					( 
						SELECT 
							kameiten_cd, 
							u.syouhin_cd, 
							denpyou_uri_date denpyou_date, 
							uri_gaku, 
							u.suu uri_suu, 
							0 siire_gaku, 
							0 siire_suu, 
							ISNULL(u.kbn,'')+ISNULL(u.bangou,'') AS bukken_bangou 
						FROM 
							t_uriage_data u WITH(READUNCOMMITTED) 
						WHERE 
							u.denpyou_uri_date BETWEEN @beginYear AND @endYear 
							AND u.himoduke_table_type=1 
						UNION ALL 
						SELECT 
							SUBSTRING(u.himoduke_cd,1,5), 
							u.syouhin_cd, 
							denpyou_uri_date denpyou_date, 
							uri_gaku, 
							u.suu uri_suu, 
							0 siire_gaku, 
							0 siire_suu, 
							'' AS bukken_bangou 
						FROM 
							t_uriage_data u WITH(READUNCOMMITTED) 
						WHERE 
							u.denpyou_uri_date BETWEEN @beginYear AND @endYear 
							AND u.himoduke_table_type=2  
							AND SUBSTRING(u.himoduke_cd,1,2)<>'AF' 
						UNION ALL 
						SELECT 
							SUBSTRING(u.himoduke_cd,1,5), 
							u.syouhin_cd, 
							denpyou_uri_date denpyou_date, 
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
							u.denpyou_uri_date BETWEEN @beginYear AND @endYear 
							AND u.himoduke_table_type=3 
						UNION ALL 
						SELECT 
							j.kameiten_cd, 
							u.syouhin_cd, 
							denpyou_uri_date denpyou_date, 
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
							u.denpyou_uri_date BETWEEN @beginYear AND @endYear 
							AND u.himoduke_table_type=9 
						UNION ALL 
						SELECT 
							j.kameiten_cd, 
							s.syouhin_cd, 
							s.denpyou_siire_date denpyou_date, 
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
							s.denpyou_siire_date BETWEEN @beginYear AND @endYear 
							AND s.himoduke_table_type=1 
						UNION ALL 
						SELECT 
							ISNULL(h.kameiten_cd,j.kameiten_cd), 
							s.syouhin_cd, 
							s.denpyou_siire_date denpyou_date, 
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
							s.denpyou_siire_date BETWEEN @beginYear AND @endYear 
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
					k.kameiten_mei1, 
					k.kameiten_cd, 
					'/'+CONVERT(VARCHAR,MONTH(denpyou_date)), 
					s.syouhin_syubetu1, 
					km.meisyou 
			) AS SUB_MK 
	) AS MK 
GROUP BY 
	kameiten_mei1, 
	kameiten_cd, 
	syouhin_cd, 
	syouhin_mei 
ORDER BY  
	kameiten_cd, 
	syouhin_cd 
