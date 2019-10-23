CREATE OR REPLACE VIEW ZIBAN.VIEW_��������
AS
/***********************************************
 ������}�X�^���x�[�X�ɁA�֘A�}�X�^���琿���於��Z�������擾����VIEW�iOracle�p�j
 �J�i���ł̐����挟�����Ɏg�p
 ���ځF������R�[�h�A������}�ԁA������敪�A������}�X�^.����A
       �����於�A�����於�Q�A������J�i�A
       ���������t��[�Z���P�A�Z���Q�A�X�֔ԍ��A�d�b�ԍ��AFAX�ԍ�]
 ���j�[�N�����L�[�F������R�[�h�A������}�ԁA������敪
***********************************************/
SELECT
     MSS.�����溰��
   , MSS.������}��
   , MSS.������敪
   , MSS.���
   , MMM.�����於
   , MMM.�����於2
   , MMM.�������
   , (
          CASE
               WHEN MSS.���������t��Z��1 IS NULL
               THEN MMM.���������t��Z��1
               ELSE MSS.���������t��Z��1
          END) AS "���������t��Z��1"
   , (
          CASE
               WHEN MSS.���������t��Z��1 IS NULL
               THEN MMM.���������t��Z��2
               ELSE MSS.���������t��Z��2
          END) AS "���������t��Z��2"
   , (
          CASE
               WHEN MSS.���������t��Z��1 IS NULL
               THEN MMM.���������t��X�֔ԍ�
               ELSE MSS.���������t��X�֔ԍ�
          END) AS "���������t��X�֔ԍ�"
   , (
          CASE
               WHEN MSS.���������t��Z��1 IS NULL
               THEN MMM.���������t��d�b�ԍ�
               ELSE MSS.���������t��d�b�ԍ�
          END) AS "���������t��d�b�ԍ�"
   , (
          CASE
               WHEN MSS.���������t��Z��1 IS NULL
               THEN MMM.���������t��FAX�ԍ�
               ELSE MSS.���������t��FAX�ԍ�
          END) AS "���������t��FAX�ԍ�"
FROM
     ZIBAN.TBL_M������ MSS
          INNER JOIN
          --�����X�}�X�^
              (SELECT
                    MKM.�����X���� AS "�����溰��"
                  , NULL AS "������}��"
                  , '0' AS "������敪"
                  , MKM.�����X������ AS "�����於"
                  , MKM.�����X�������� AS "�����於2"
                  , (MKM.�X����1 || MKM.�X����2) AS "�������"
                  , MKJ.�Z��1 AS "���������t��Z��1"
                  , MKJ.�Z��2 AS "���������t��Z��2"
                  , MKJ.�X�֔ԍ� AS "���������t��X�֔ԍ�"
                  , MKJ.�d�b�ԍ� AS "���������t��d�b�ԍ�"
                  , MKJ.FAX�ԍ� AS "���������t��FAX�ԍ�"
               FROM
                    ZIBAN.TBL_M�����X MKM
                  , ZIBAN.TBL_M�����X�Z�� MKJ
               WHERE
                    MKM.�����X���� = MKJ.�����X����(+)
                AND MKJ.������FLG(+) = '-1'
               UNION ALL
               --������Ѓ}�X�^
               SELECT
                    MTY.������к��� AS "�����溰��"
                  , MTY.���Ə����� AS "������}��"
                  , '1' AS "������敪"
                  , NVL(NULLIF(MTY.������x���於,''),MTY.������Ж�) AS "�����於"
                  , NULL AS "�����於2"
                  , NVL(NULLIF(MTY.������x���於��,''),MTY.������Ж���) AS "�������"
                  , NVL(NULLIF(MTY.���������t��Z��1,''),MTY.�Z��1) AS "���������t��Z��1"
                  , NVL(NULLIF(MTY.���������t��Z��2,''),MTY.�Z��2) AS "���������t��Z��2"
                  , NVL(NULLIF(MTY.���������t��X�֔ԍ�,''),MTY.�X�֔ԍ�) AS "���������t��X�֔ԍ�"
                  , NVL(NULLIF(MTY.���������t��d�b�ԍ�,''),MTY.�d�b�ԍ�) AS "���������t��d�b�ԍ�"
                  , NVL(NULLIF(MTY.�x���pFAX�ԍ�,''),MTY.FAX�ԍ�) AS "���������t��FAX�ԍ�"
               FROM
                    ZIBAN.TBL_M������� MTY
              )
               MMM
            ON MSS.�����溰�� = MMM.�����溰��
           AND MSS.������}�� =(
                    CASE
                         WHEN MMM.������}�� IS NULL
                         THEN MSS.������}��
                         ELSE MMM.������}��
                    END)
           AND MSS.������敪 = MMM.������敪
;
