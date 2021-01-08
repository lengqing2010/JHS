 INSERT INTO t_nendo_hiritu_kanri WITH(UPDLOCK) ( 
	keikaku_nendo 
	,kameiten_cd 
	,kameiten_mei 
	,uri_hiritu 
	,koj_hantei_ritu 
	,koj_jyuchuu_ritu 
	,tyoku_koj_ritu 
	,add_login_user_id 
	,add_datetime 
	,upd_login_user_id 
	,upd_datetime 
 ) 
 VALUES ( 
	@keikaku_nendo 
	,@kameiten_cd 
	,@kameiten_mei 
	,@uri_hiritu 
	,@koj_hantei_ritu 
	,@koj_jyuchuu_ritu 
	,@tyoku_koj_ritu 
	,@add_login_user_id 
	,GETDATE()  
	,null 
	,null  
 ); 
