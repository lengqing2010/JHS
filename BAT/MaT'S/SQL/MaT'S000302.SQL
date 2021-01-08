SELECT 
	SFUM.UCCRPCOD AS �����X���� 
	,SFUM.UCDELFLG AS ��� 
	,9 AS ������~FLG 
	,CASE WHEN UCSTROT8 IS NOT NULL THEN 
		CASE WHEN SUBSTR(UCSTROT8,-1) IN ('0','1','2','3','4','5','6','7','8','9') THEN
	          to_char(SUBSTR(UCSTROT8, -1))
		ELSE
	          '0'
	      End
	ELSE
	      '0'
	END AS �c�Ƌ敪 
	,SFUM.UCCRPNAM AS �����X�� 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTNAM 
	     ELSE SFOM.SUB_OGPSTNAM END AS �x�X�� 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '04' THEN SFOM.OGPSTCOD 
	     ELSE SFOM.SUB_OGPSTCOD END AS �������� 
	,SOM1.OEUSRNMR AS �c�ƒS����  
	,SOM1.OEBASLID AS �c�ƒS���Җ� 
	,NULL AS �K�v�ʒk�� 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTCOD 
	     ELSE NULL END AS �c�Ə�_�������� 
	,CASE WHEN SUBSTR(SFOM.OGPSTCOD,1,2) = '05' THEN SFOM.OGPSTNAM 
	     ELSE NULL END AS �c�Ə��� 
	,NULL AS �s���{������ 
	,NULL AS �s���{���� 
	,NULL AS �n���� 
	,NULL AS �n�� 
	,NULL AS �c�Ə����� 
	,NULL AS �N�ԓ��� 
	,NULL AS �O�N����_�N�ԓ��� 
	,NULL AS �v��l0FLG 
	,NULL AS �����X�o�^�N�� 
	,'1' AS �����X�V�KFLG 
	,NULL AS �Ƒ� 
	,NULL AS �������50��FLG 
	,NULL AS �������50��FLG 
	,NULL AS �������50��FLG 
	,NULL AS �o�^ID 
	,SFUM.UCMAKDAT AS �o�^���� 
	,NULL AS �X�VID 
	,SFUM.UCDBADAT AS �X�V���� 
FROM SFAMT_USRCORP_MVR  SFUM				--��A���̉����XϽ� 
	LEFT JOIN ( 
	    SELECT SFOM.OGPSTSEQ,						--����No 
	           SFOM.OGPSTCOD,						--�����R�[�h 
	           SFOM.OGPSTNAM,						--������ 
	           SUB_SFOM.OGPSTCOD AS SUB_OGPSTCOD,	--��ʑg�D�̕����R�[�h 
	           SUB_SFOM.OGPSTNAM AS SUB_OGPSTNAM	--��ʑg�D�̕����� 
	    FROM SFAMT_OWNPOSGRP_MVR  SFOM			--��A���̕���Ͻ� 
	    LEFT JOIN SFAMT_OWNPOSGRP_MVR  SUB_SFOM	--��A���̕���Ͻ� 
	    ON SFOM.OGPHLSEQ = SUB_SFOM.OGPSTSEQ		--��ʑg�D 
	)  SFOM 
	ON SFUM.UCPSTSEQ = SFOM.OGPSTSEQ				--����No 
	LEFT JOIN ( 
		SELECT MIN(OGEMPSEQ) OGEMPSEQ 
			   ,OGCRPSEQ	 
		FROM SFAMT_OWNCSTCHG_MVR 
		GROUP BY OGCRPSEQ 
		) SOM				--��A���̉����X_�S����Ͻ�  
	ON SFUM.UCCRPSEQ = SOM.OGCRPSEQ					--�S���R�t������  
	LEFT JOIN SFAMT_OWNEMP_MVR SOM1					--��A���̒S����Ͻ�  
	ON SOM.OGEMPSEQ = SOM1.OEEMPSEQ					--�S���Һ��� 
WHERE 
	length(nvl(SFUM.UCCRPCOD,'')) = 8	--8�� �œo�^�̂���R�[�h���擾���� 
	AND SFUM.UCDELFLG = '0'