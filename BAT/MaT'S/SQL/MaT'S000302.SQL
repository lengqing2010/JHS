SELECT 
	SFUM.UCCRPCOD AS Á¿Xº°ÄÞ 
	,SFUM.UCDELFLG AS æÁ 
	,9 AS ­â~FLG 
	,CASE WHEN UCSTROT8 IS NOT NULL THEN 
		CASE WHEN SUBSTR(UCSTROT8,-1) IN ('0','1','2','3','4','5','6','7','8','9') THEN
	          to_char(SUBSTR(UCSTROT8, -1))
		ELSE
	          '0'
	      End
	ELSE
	      '0'
	END AS cÆæª 
	,SFUM.UCCRPNAM AS Á¿X¼ 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTNAM 
	     ELSE SFOM.SUB_OGPSTNAM END AS xX¼ 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTCOD 
	     ELSE SFOM.SUB_OGPSTCOD END AS º°ÄÞ 
	,SOM1.OEUSRNMR AS cÆSÒ  
	,SOM1.OEBASLID AS cÆSÒ¼ 
	,NULL AS KvÊkñ 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTCOD 
	     ELSE NULL END AS cÆ_º°ÄÞ 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTNAM 
	     ELSE NULL END AS cÆ¼ 
	,NULL AS s¹{§º°ÄÞ 
	,NULL AS s¹{§¼ 
	,NULL AS nñº°ÄÞ 
	,NULL AS nñ¼ 
	,NULL AS cÆº°ÄÞ 
	,NULL AS NÔ 
	,NULL AS ONÀÑ_NÔ 
	,NULL AS væl0FLG 
	,NULL AS Á¿Xo^N 
	,'1' AS Á¿XVKFLG 
	,NULL AS ÆÔ 
	,NULL AS ª÷ãÊ50ÐFLG 
	,NULL AS ¶ãÊ50ÐFLG 
	,NULL AS ¹ãÊ50ÐFLG 
	,NULL AS o^ID 
	,SFUM.UCMAKDAT AS o^ú 
	,NULL AS XVID 
	,SFUM.UCDBADAT AS XVú 
FROM SFAMT_USRCORP_MVR  SFUM				--ñAÌÁ¿XÏ½À 
	LEFT JOIN ( 
	    SELECT SFOM.OGPSTSEQ,						--No 
	           SFOM.OGPSTCOD,						--R[h 
	           SFOM.OGPSTNAM,						--¼ 
	           SUB_SFOM.OGPSTCOD AS SUB_OGPSTCOD,	--ãÊgDÌR[h 
	           SUB_SFOM.OGPSTNAM AS SUB_OGPSTNAM	--ãÊgDÌ¼ 
	    FROM SFAMT_OWNPOSGRP_MVR  SFOM			--ñAÌÏ½À 
	    LEFT JOIN SFAMT_OWNPOSGRP_MVR  SUB_SFOM	--ñAÌÏ½À 
	    ON SFOM.OGPHLSEQ = SUB_SFOM.OGPSTSEQ		--ãÊgD 
	)  SFOM 
	ON SFUM.UCPSTSEQ = SFOM.OGPSTSEQ				--No 
	LEFT JOIN ( 
		SELECT MIN(OGEMPSEQ) OGEMPSEQ 
			   ,OGCRPSEQ	 
		FROM SFAMT_OWNCSTCHG_MVR 
		GROUP BY OGCRPSEQ 
		) SOM				--ñAÌÁ¿X_SÒÏ½À  
	ON SFUM.UCCRPSEQ = SOM.OGCRPSEQ					--SRt¯º°ÄÞ  
	LEFT JOIN SFAMT_OWNEMP_MVR SOM1					--ñAÌSÒÏ½À  
	ON SOM.OGEMPSEQ = SOM1.OEEMPSEQ					--SÒº°ÄÞ 
WHERE 
	length(nvl(SFUM.UCCRPCOD,'')) = 8	--8 Åo^Ì éR[hðæ¾·é 
	AND SFUM.UCDELFLG = '0'
