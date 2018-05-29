Option Explicit On
Option Strict On
Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess
Imports Lixil.JHS_EKKS.Utilities
Imports System.Text.RegularExpressions

''' <summary>
''' �v��Ǘ� CSV�捞
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriInputBC
    Private objCommonDA As New CommonDA
    Private objKeikakuKanriInputDA As New DataAccess.KeikakuKanriInputDA

    Private Const csvKeikaku As String = "B1"
    Private Const csvMikomi As String = "B2"
    Private Const csvFcKeikaku As String = "B3"
    Private Const csvFcMikomi As String = "B4"

    '�g�p�֎~������z��
    Private arrayKinsiStr() As String = New String() {vbTab, """", "�C", "'", "<", ">", "&", "$$$"}
    'CSV�A�b�v���[�h�������
    Private CsvInputMaxLineCount As Integer = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    '�u�v��Ǘ��e�[�u���v�̕K�{���͍��ڍ���
    Private KEIKAKU_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6, 7}
    '�u�\�茩���Ǘ��e�[�u���v�̕K�{���͍��ڍ���
    Private MIKOMI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6, 7}
    '�uFC�p�v��Ǘ��e�[�u���v�̕K�{���͍��ڍ���
    Private FC_KEIKAKU_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6}
    '�uFC�p�\�茩���Ǘ��e�[�u���v�̕K�{���͍��ڍ���
    Private FC_MIKOMI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 4, 5, 6}

    '�v��CSV�̍��ڍő咷
    Private KEIKAKU_MAX_LENGTH() As Integer = {2, 40, 1, 4, 8, 8, 40, 3, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12}
    '����CSV�̍��ڍő咷
    Private MIKOMI_MAX_LENGTH() As Integer = {2, 40, 1, 4, 8, 8, 40, 3, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}
    '�e�b�p�v��CSV�̍��ڍő咷
    Private FC_KEIKAKU_MAX_LENGTH() As Integer = {2, 40, 4, 4, 8, 40, 3, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12, 12, 12, 4, 4, 4, 12, 12, 12}
    '�e�b�p����CSV�̍��ڍő咷
    Private FC_MIKOMI_MAX_LENGTH() As Integer = {2, 40, 4, 4, 8, 40, 3, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}

    '�v��CSV�̐��l�^���ڍ���
    Private KEIKAKU_NUM_INDEX() As Integer = {8, 9, 13, 14, 15, 16, 17, 21, 22, 23, 24, 25, 29, 30, 31, 32, 33, 37, 38, 39, 40, 41, 45, 46, 47, 48, 49, 53, 54, 55, 56, 57, 61, 62, 63, 64, 65, 69, 70, 71, 72, 73, 77, 78, 79, 80, 81, 85, 86, 87, 88, 89, 93, 94, 95, 96, 97, 101, 102, 103}
    '����CSV�̐��l�^���ڍ���
    Private MIKOMI_NUM_INDEX() As Integer = {8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43}
    '�e�b�p�v��CSV�̐��l�^���ڍ���
    Private FC_KEIKAKU_NUM_INDEX() As Integer = {7, 8, 12, 13, 14, 15, 16, 20, 21, 22, 23, 24, 28, 29, 30, 31, 32, 36, 37, 38, 39, 40, 44, 45, 46, 47, 48, 52, 53, 54, 55, 56, 60, 61, 62, 63, 64, 68, 69, 70, 71, 72, 76, 77, 78, 79, 80, 84, 85, 86, 87, 88, 92, 93, 94, 95, 96, 100, 101, 102}
    '�e�b�p����CSV�̐��l�^���ڍ���
    Private FC_MIKOMI_NUM_INDEX() As Integer = {7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42}

    '�v��CSV�̏����^���ڍ���
    Private KEIKAKU_DEC_INDEX() As Integer = {10, 11, 12, 18, 19, 20, 26, 27, 28, 34, 35, 36, 42, 43, 44, 50, 51, 52, 58, 59, 60, 66, 67, 68, 74, 75, 76, 82, 83, 84, 90, 91, 92, 98, 99, 100}
    '�e�b�p�v��CSV�̏����^���ڍ���
    Private FC_KEIKAKU_DEC_INDEX() As Integer = {9, 10, 11, 17, 18, 19, 25, 26, 27, 33, 34, 35, 41, 42, 43, 49, 50, 51, 57, 58, 59, 65, 66, 67, 73, 74, 75, 81, 82, 83, 89, 90, 91, 97, 98, 99}

    '�v��CSV�t�@�C��.���ږ�
    Private Enum csvKeikakuItems
        keikaku_nendo = 3                   '�v��_�N�x
        kameiten_cd                         '�����X����
        keikaku_kanri_syouhin_cd            '�v��Ǘ�_���i�R�[�h
        kameiten_mei                        '�����X��
        bunbetu_cd                          '���ʃR�[�h

        gatu4_keisanyou_uri_heikin_tanka    '4��_�v�Z�p__���㕽�ϒP��
        gatu4_keisanyou_siire_heikin_tanka  '4��_�v�Z�p__�d�����ϒP��

        gatu4_keisanyou_koj_hantei_ritu     '4��_�v�Z�p_�H�����藦
        gatu4_keisanyou_koj_jyuchuu_ritu    '4��_�v�Z�p_�H���󒍗�
        gatu4_keisanyou_tyoku_koj_ritu      '4��_�v�Z�p_���H����
        gatu4_keikaku_kensuu                '4��_�v�挏��
        gatu4_keikaku_kingaku               '4��_�v����z
        gatu4_keikaku_arari                 '4��_�v��e��

        gatu5_keisanyou_uri_heikin_tanka    '5��_�v�Z�p__���㕽�ϒP��
        gatu5_keisanyou_siire_heikin_tanka  '5��_�v�Z�p__�d�����ϒP��

        gatu5_keisanyou_koj_hantei_ritu     '5��_�v�Z�p_�H�����藦
        gatu5_keisanyou_koj_jyuchuu_ritu    '5��_�v�Z�p_�H���󒍗�
        gatu5_keisanyou_tyoku_koj_ritu      '5��_�v�Z�p_���H����
        gatu5_keikaku_kensuu                '5��_�v�挏��
        gatu5_keikaku_kingaku               '5��_�v����z
        gatu5_keikaku_arari                 '5��_�v��e��

        gatu6_keisanyou_uri_heikin_tanka    '6��_�v�Z�p__���㕽�ϒP��
        gatu6_keisanyou_siire_heikin_tanka  '6��_�v�Z�p__�d�����ϒP��

        gatu6_keisanyou_koj_hantei_ritu     '6��_�v�Z�p_�H�����藦
        gatu6_keisanyou_koj_jyuchuu_ritu    '6��_�v�Z�p_�H���󒍗�
        gatu6_keisanyou_tyoku_koj_ritu      '6��_�v�Z�p_���H����
        gatu6_keikaku_kensuu                '6��_�v�挏��
        gatu6_keikaku_kingaku               '6��_�v����z
        gatu6_keikaku_arari                 '6��_�v��e��

        gatu7_keisanyou_uri_heikin_tanka    '7��_�v�Z�p__���㕽�ϒP��
        gatu7_keisanyou_siire_heikin_tanka  '7��_�v�Z�p__�d�����ϒP��

        gatu7_keisanyou_koj_hantei_ritu     '7��_�v�Z�p_�H�����藦
        gatu7_keisanyou_koj_jyuchuu_ritu    '7��_�v�Z�p_�H���󒍗�
        gatu7_keisanyou_tyoku_koj_ritu      '7��_�v�Z�p_���H����
        gatu7_keikaku_kensuu                '7��_�v�挏��
        gatu7_keikaku_kingaku               '7��_�v����z
        gatu7_keikaku_arari                 '7��_�v��e��

        gatu8_keisanyou_uri_heikin_tanka    '8��_�v�Z�p__���㕽�ϒP��
        gatu8_keisanyou_siire_heikin_tanka  '8��_�v�Z�p__�d�����ϒP��

        gatu8_keisanyou_koj_hantei_ritu     '8��_�v�Z�p_�H�����藦
        gatu8_keisanyou_koj_jyuchuu_ritu    '8��_�v�Z�p_�H���󒍗�
        gatu8_keisanyou_tyoku_koj_ritu      '8��_�v�Z�p_���H����
        gatu8_keikaku_kensuu                '8��_�v�挏��
        gatu8_keikaku_kingaku               '8��_�v����z
        gatu8_keikaku_arari                 '8��_�v��e��

        gatu9_keisanyou_uri_heikin_tanka    '9��_�v�Z�p__���㕽�ϒP��
        gatu9_keisanyou_siire_heikin_tanka  '9��_�v�Z�p__�d�����ϒP��

        gatu9_keisanyou_koj_hantei_ritu     '9��_�v�Z�p_�H�����藦
        gatu9_keisanyou_koj_jyuchuu_ritu    '9��_�v�Z�p_�H���󒍗�
        gatu9_keisanyou_tyoku_koj_ritu      '9��_�v�Z�p_���H����
        gatu9_keikaku_kensuu                '9��_�v�挏��
        gatu9_keikaku_kingaku               '9��_�v����z
        gatu9_keikaku_arari                 '9��_�v��e��

        gatu10_keisanyou_uri_heikin_tanka    '10��_�v�Z�p__���㕽�ϒP��
        gatu10_keisanyou_siire_heikin_tanka  '10��_�v�Z�p__�d�����ϒP��

        gatu10_keisanyou_koj_hantei_ritu    '10��_�v�Z�p_�H�����藦
        gatu10_keisanyou_koj_jyuchuu_ritu   '10��_�v�Z�p_�H���󒍗�
        gatu10_keisanyou_tyoku_koj_ritu     '10��_�v�Z�p_���H����
        gatu10_keikaku_kensuu               '10��_�v�挏��
        gatu10_keikaku_kingaku              '10��_�v����z
        gatu10_keikaku_arari                '10��_�v��e��

        gatu11_keisanyou_uri_heikin_tanka    '11��_�v�Z�p__���㕽�ϒP��
        gatu11_keisanyou_siire_heikin_tanka  '11��_�v�Z�p__�d�����ϒP��

        gatu11_keisanyou_koj_hantei_ritu    '11��_�v�Z�p_�H�����藦
        gatu11_keisanyou_koj_jyuchuu_ritu   '11��_�v�Z�p_�H���󒍗�
        gatu11_keisanyou_tyoku_koj_ritu     '11��_�v�Z�p_���H����
        gatu11_keikaku_kensuu               '11��_�v�挏��
        gatu11_keikaku_kingaku              '11��_�v����z
        gatu11_keikaku_arari                '11��_�v��e��

        gatu12_keisanyou_uri_heikin_tanka    '12��_�v�Z�p__���㕽�ϒP��
        gatu12_keisanyou_siire_heikin_tanka  '12��_�v�Z�p__�d�����ϒP��

        gatu12_keisanyou_koj_hantei_ritu    '12��_�v�Z�p_�H�����藦
        gatu12_keisanyou_koj_jyuchuu_ritu   '12��_�v�Z�p_�H���󒍗�
        gatu12_keisanyou_tyoku_koj_ritu     '12��_�v�Z�p_���H����
        gatu12_keikaku_kensuu               '12��_�v�挏��
        gatu12_keikaku_kingaku              '12��_�v����z
        gatu12_keikaku_arari                '12��_�v��e��

        gatu1_keisanyou_uri_heikin_tanka    '1��_�v�Z�p__���㕽�ϒP��
        gatu1_keisanyou_siire_heikin_tanka  '1��_�v�Z�p__�d�����ϒP��

        gatu1_keisanyou_koj_hantei_ritu     '1��_�v�Z�p_�H�����藦
        gatu1_keisanyou_koj_jyuchuu_ritu    '1��_�v�Z�p_�H���󒍗�
        gatu1_keisanyou_tyoku_koj_ritu      '1��_�v�Z�p_���H����
        gatu1_keikaku_kensuu                '1��_�v�挏��
        gatu1_keikaku_kingaku               '1��_�v����z
        gatu1_keikaku_arari                 '1��_�v��e��

        gatu2_keisanyou_uri_heikin_tanka    '2��_�v�Z�p__���㕽�ϒP��
        gatu2_keisanyou_siire_heikin_tanka  '2��_�v�Z�p__�d�����ϒP��

        gatu2_keisanyou_koj_hantei_ritu     '2��_�v�Z�p_�H�����藦
        gatu2_keisanyou_koj_jyuchuu_ritu    '2��_�v�Z�p_�H���󒍗�
        gatu2_keisanyou_tyoku_koj_ritu      '2��_�v�Z�p_���H����
        gatu2_keikaku_kensuu                '2��_�v�挏��
        gatu2_keikaku_kingaku               '2��_�v����z
        gatu2_keikaku_arari                 '2��_�v��e��

        gatu3_keisanyou_uri_heikin_tanka    '3��_�v�Z�p__���㕽�ϒP��
        gatu3_keisanyou_siire_heikin_tanka  '3��_�v�Z�p__�d�����ϒP��

        gatu3_keisanyou_koj_hantei_ritu     '3��_�v�Z�p_�H�����藦
        gatu3_keisanyou_koj_jyuchuu_ritu    '3��_�v�Z�p_�H���󒍗�
        gatu3_keisanyou_tyoku_koj_ritu      '3��_�v�Z�p_���H����
        gatu3_keikaku_kensuu                '3��_�v�挏��
        gatu3_keikaku_kingaku               '3��_�v����z
        gatu3_keikaku_arari                 '3��_�v��e��
    End Enum

    '����CSV�t�@�C��.���ږ�
    Private Enum csvMikomiItems
        keikaku_nendo = 3               '�v��_�N�x
        kameiten_cd                     '�����X����
        keikaku_kanri_syouhin_cd        '�v��Ǘ�_���i�R�[�h
        kameiten_mei                    '�����X��
        bunbetu_cd                      '���ʃR�[�h
        gatu4_mikomi_kensuu             '4��_��������
        gatu4_mikomi_kingaku            '4��_�������z
        gatu4_mikomi_arari              '4��_�����e��
        gatu5_mikomi_kensuu             '5��_��������
        gatu5_mikomi_kingaku            '5��_�������z
        gatu5_mikomi_arari              '5��_�����e��
        gatu6_mikomi_kensuu             '6��_��������
        gatu6_mikomi_kingaku            '6��_�������z
        gatu6_mikomi_arari              '6��_�����e��
        gatu7_mikomi_kensuu             '7��_��������
        gatu7_mikomi_kingaku            '7��_�������z
        gatu7_mikomi_arari              '7��_�����e��
        gatu8_mikomi_kensuu             '8��_��������
        gatu8_mikomi_kingaku            '8��_�������z
        gatu8_mikomi_arari              '8��_�����e��
        gatu9_mikomi_kensuu             '9��_��������
        gatu9_mikomi_kingaku            '9��_�������z
        gatu9_mikomi_arari              '9��_�����e��
        gatu10_mikomi_kensuu            '10��_��������
        gatu10_mikomi_kingaku           '10��_�������z
        gatu10_mikomi_arari             '10��_�����e��
        gatu11_mikomi_kensuu            '11��_��������
        gatu11_mikomi_kingaku           '11��_�������z
        gatu11_mikomi_arari             '11��_�����e��
        gatu12_mikomi_kensuu            '12��_��������
        gatu12_mikomi_kingaku           '12��_�������z
        gatu12_mikomi_arari             '12��_�����e��
        gatu1_mikomi_kensuu             '1��_��������
        gatu1_mikomi_kingaku            '1��_�������z
        gatu1_mikomi_arari              '1��_�����e��
        gatu2_mikomi_kensuu             '2��_��������
        gatu2_mikomi_kingaku            '2��_�������z
        gatu2_mikomi_arari              '2��_�����e��
        gatu3_mikomi_kensuu             '3��_��������
        gatu3_mikomi_kingaku            '3��_�������z
        gatu3_mikomi_arari              '3��_�����e��
    End Enum

    '�e�b�p�v��CSV�t�@�C��.���ږ�
    Private Enum csvFcKeikakuItems
        keikaku_nendo = 2                           '�v��_�N�x
        busyo_cd                                    '��������
        keikaku_kanri_syouhin_cd                    '�v��Ǘ�_���i�R�[�h
        siten_mei                                   '�x�X��
        bunbetu_cd                                  '���ʃR�[�h

        gatu4_keisanyou_uri_heikin_tanka    '4��_�v�Z�p__���㕽�ϒP��
        gatu4_keisanyou_siire_heikin_tanka  '4��_�v�Z�p__�d�����ϒP��

        gatu4_keisanyou_koj_hantei_ritu             '4��_�v�Z�p_�H�����藦
        gatu4_keisanyou_koj_jyuchuu_ritu            '4��_�v�Z�p_�H���󒍗�
        gatu4_keisanyou_tyoku_koj_ritu              '4��_�v�Z�p_���H����
        gatu4_keikaku_kensuu                        '4��_�v�挏��
        gatu4_keikaku_kingaku                       '4��_�v����z
        gatu4_keikaku_arari                         '4��_�v��e��

        gatu5_keisanyou_uri_heikin_tanka    '5��_�v�Z�p__���㕽�ϒP��
        gatu5_keisanyou_siire_heikin_tanka  '5��_�v�Z�p__�d�����ϒP��

        gatu5_keisanyou_koj_hantei_ritu             '5��_�v�Z�p_�H�����藦
        gatu5_keisanyou_koj_jyuchuu_ritu            '5��_�v�Z�p_�H���󒍗�
        gatu5_keisanyou_tyoku_koj_ritu              '5��_�v�Z�p_���H����
        gatu5_keikaku_kensuu                        '5��_�v�挏��
        gatu5_keikaku_kingaku                       '5��_�v����z
        gatu5_keikaku_arari                         '5��_�v��e��

        gatu6_keisanyou_uri_heikin_tanka    '6��_�v�Z�p__���㕽�ϒP��
        gatu6_keisanyou_siire_heikin_tanka  '6��_�v�Z�p__�d�����ϒP��

        gatu6_keisanyou_koj_hantei_ritu             '6��_�v�Z�p_�H�����藦
        gatu6_keisanyou_koj_jyuchuu_ritu            '6��_�v�Z�p_�H���󒍗�
        gatu6_keisanyou_tyoku_koj_ritu              '6��_�v�Z�p_���H����
        gatu6_keikaku_kensuu                        '6��_�v�挏��
        gatu6_keikaku_kingaku                       '6��_�v����z
        gatu6_keikaku_arari                         '6��_�v��e��

        gatu7_keisanyou_uri_heikin_tanka    '7��_�v�Z�p__���㕽�ϒP��
        gatu7_keisanyou_siire_heikin_tanka  '7��_�v�Z�p__�d�����ϒP��

        gatu7_keisanyou_koj_hantei_ritu             '7��_�v�Z�p_�H�����藦
        gatu7_keisanyou_koj_jyuchuu_ritu            '7��_�v�Z�p_�H���󒍗�
        gatu7_keisanyou_tyoku_koj_ritu              '7��_�v�Z�p_���H����
        gatu7_keikaku_kensuu                        '7��_�v�挏��
        gatu7_keikaku_kingaku                       '7��_�v����z
        gatu7_keikaku_arari                         '7��_�v��e��

        gatu8_keisanyou_uri_heikin_tanka    '8��_�v�Z�p__���㕽�ϒP��
        gatu8_keisanyou_siire_heikin_tanka  '8��_�v�Z�p__�d�����ϒP��

        gatu8_keisanyou_koj_hantei_ritu             '8��_�v�Z�p_�H�����藦
        gatu8_keisanyou_koj_jyuchuu_ritu            '8��_�v�Z�p_�H���󒍗�
        gatu8_keisanyou_tyoku_koj_ritu              '8��_�v�Z�p_���H����
        gatu8_keikaku_kensuu                        '8��_�v�挏��
        gatu8_keikaku_kingaku                       '8��_�v����z
        gatu8_keikaku_arari                         '8��_�v��e��

        gatu9_keisanyou_uri_heikin_tanka    '9��_�v�Z�p__���㕽�ϒP��
        gatu9_keisanyou_siire_heikin_tanka  '9��_�v�Z�p__�d�����ϒP��

        gatu9_keisanyou_koj_hantei_ritu             '9��_�v�Z�p_�H�����藦
        gatu9_keisanyou_koj_jyuchuu_ritu            '9��_�v�Z�p_�H���󒍗�
        gatu9_keisanyou_tyoku_koj_ritu              '9��_�v�Z�p_���H����
        gatu9_keikaku_kensuu                        '9��_�v�挏��
        gatu9_keikaku_kingaku                       '9��_�v����z
        gatu9_keikaku_arari                         '9��_�v��e��

        gatu10_keisanyou_uri_heikin_tanka    '10��_�v�Z�p__���㕽�ϒP��
        gatu10_keisanyou_siire_heikin_tanka  '10��_�v�Z�p__�d�����ϒP��

        gatu10_keisanyou_koj_hantei_ritu            '10��_�v�Z�p_�H�����藦
        gatu10_keisanyou_koj_jyuchuu_ritu           '10��_�v�Z�p_�H���󒍗�
        gatu10_keisanyou_tyoku_koj_ritu             '10��_�v�Z�p_���H����
        gatu10_keikaku_kensuu                       '10��_�v�挏��
        gatu10_keikaku_kingaku                      '10��_�v����z
        gatu10_keikaku_arari                        '10��_�v��e��

        gatu11_keisanyou_uri_heikin_tanka    '11��_�v�Z�p__���㕽�ϒP��
        gatu11_keisanyou_siire_heikin_tanka  '11��_�v�Z�p__�d�����ϒP��

        gatu11_keisanyou_koj_hantei_ritu            '11��_�v�Z�p_�H�����藦
        gatu11_keisanyou_koj_jyuchuu_ritu           '11��_�v�Z�p_�H���󒍗�
        gatu11_keisanyou_tyoku_koj_ritu             '11��_�v�Z�p_���H����
        gatu11_keikaku_kensuu                       '11��_�v�挏��
        gatu11_keikaku_kingaku                      '11��_�v����z
        gatu11_keikaku_arari                        '11��_�v��e��

        gatu12_keisanyou_uri_heikin_tanka    '12��_�v�Z�p__���㕽�ϒP��
        gatu12_keisanyou_siire_heikin_tanka  '12��_�v�Z�p__�d�����ϒP��

        gatu12_keisanyou_koj_hantei_ritu            '12��_�v�Z�p_�H�����藦
        gatu12_keisanyou_koj_jyuchuu_ritu           '12��_�v�Z�p_�H���󒍗�
        gatu12_keisanyou_tyoku_koj_ritu             '12��_�v�Z�p_���H����
        gatu12_keikaku_kensuu                       '12��_�v�挏��
        gatu12_keikaku_kingaku                      '12��_�v����z
        gatu12_keikaku_arari                        '12��_�v��e��

        gatu1_keisanyou_uri_heikin_tanka    '1��_�v�Z�p__���㕽�ϒP��
        gatu1_keisanyou_siire_heikin_tanka  '1��_�v�Z�p__�d�����ϒP��

        gatu1_keisanyou_koj_hantei_ritu             '1��_�v�Z�p_�H�����藦
        gatu1_keisanyou_koj_jyuchuu_ritu            '1��_�v�Z�p_�H���󒍗�
        gatu1_keisanyou_tyoku_koj_ritu              '1��_�v�Z�p_���H����
        gatu1_keikaku_kensuu                        '1��_�v�挏��
        gatu1_keikaku_kingaku                       '1��_�v����z
        gatu1_keikaku_arari                         '1��_�v��e��

        gatu2_keisanyou_uri_heikin_tanka    '2��_�v�Z�p__���㕽�ϒP��
        gatu2_keisanyou_siire_heikin_tanka  '2��_�v�Z�p__�d�����ϒP��

        gatu2_keisanyou_koj_hantei_ritu             '2��_�v�Z�p_�H�����藦
        gatu2_keisanyou_koj_jyuchuu_ritu            '2��_�v�Z�p_�H���󒍗�
        gatu2_keisanyou_tyoku_koj_ritu              '2��_�v�Z�p_���H����
        gatu2_keikaku_kensuu                        '2��_�v�挏��
        gatu2_keikaku_kingaku                       '2��_�v����z
        gatu2_keikaku_arari                         '2��_�v��e��

        gatu3_keisanyou_uri_heikin_tanka    '3��_�v�Z�p__���㕽�ϒP��
        gatu3_keisanyou_siire_heikin_tanka  '3��_�v�Z�p__�d�����ϒP��

        gatu3_keisanyou_koj_hantei_ritu             '3��_�v�Z�p_�H�����藦
        gatu3_keisanyou_koj_jyuchuu_ritu            '3��_�v�Z�p_�H���󒍗�
        gatu3_keisanyou_tyoku_koj_ritu              '3��_�v�Z�p_���H����
        gatu3_keikaku_kensuu                        '3��_�v�挏��
        gatu3_keikaku_kingaku                       '3��_�v����z
        gatu3_keikaku_arari                         '3��_�v��e��
    End Enum

    '�e�b�p����CSV�t�@�C��.���ږ�
    Private Enum csvFcMikomiItems
        keikaku_nendo = 2                  '�v��_�N�x
        busyo_cd                           '��������
        keikaku_kanri_syouhin_cd           '�v��Ǘ�_���i�R�[�h
        siten_mei                          '�x�X��
        bunbetu_cd                         '���ʃR�[�h
        gatu4_mikomi_kensuu                '4��_��������
        gatu4_mikomi_kingaku               '4��_�������z
        gatu4_mikomi_arari                 '4��_�����e��
        gatu5_mikomi_kensuu                '5��_��������
        gatu5_mikomi_kingaku               '5��_�������z
        gatu5_mikomi_arari                 '5��_�����e��
        gatu6_mikomi_kensuu                '6��_��������
        gatu6_mikomi_kingaku               '6��_�������z
        gatu6_mikomi_arari                 '6��_�����e��
        gatu7_mikomi_kensuu                '7��_��������
        gatu7_mikomi_kingaku               '7��_�������z
        gatu7_mikomi_arari                 '7��_�����e��
        gatu8_mikomi_kensuu                '8��_��������
        gatu8_mikomi_kingaku               '8��_�������z
        gatu8_mikomi_arari                 '8��_�����e��
        gatu9_mikomi_kensuu                '9��_��������
        gatu9_mikomi_kingaku               '9��_�������z
        gatu9_mikomi_arari                 '9��_�����e��
        gatu10_mikomi_kensuu               '10��_��������
        gatu10_mikomi_kingaku              '10��_�������z
        gatu10_mikomi_arari                '10��_�����e��
        gatu11_mikomi_kensuu               '11��_��������
        gatu11_mikomi_kingaku              '11��_�������z
        gatu11_mikomi_arari                '11��_�����e��
        gatu12_mikomi_kensuu               '12��_��������
        gatu12_mikomi_kingaku              '12��_�������z
        gatu12_mikomi_arari                '12��_�����e��
        gatu1_mikomi_kensuu                '1��_��������
        gatu1_mikomi_kingaku               '1��_�������z
        gatu1_mikomi_arari                 '1��_�����e��
        gatu2_mikomi_kensuu                '2��_��������
        gatu2_mikomi_kingaku               '2��_�������z
        gatu2_mikomi_arari                 '2��_�����e��
        gatu3_mikomi_kensuu                '3��_��������
        gatu3_mikomi_kingaku               '3��_�������z
        gatu3_mikomi_arari                 '3��_�����e��
    End Enum

    ''' <summary>
    ''' �������ʎ擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function GetInputKanri() As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return objKeikakuKanriInputDA.SelInputKanri()

    End Function

    ''' <summary>
    ''' �S�������ʌ����擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function GetInputKanriCount() As String
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return objKeikakuKanriInputDA.SelInputKanriCount()

    End Function

    ''' <summary>
    ''' CSV�捞�t�@�C���̃f�[�^���`�F�b�N����
    ''' </summary>
    ''' <param name="fupCsv">CSV�捞�̃R���g���[��</param>
    ''' <param name="strCsvKbn">CSV�敪</param>
    ''' <param name="strUmuFlg">�G���[�L��</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/11/23 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function ChkCsvFile(ByVal fupCsv As Web.UI.WebControls.FileUpload, _
                               ByVal strCsvKbn As String, _
                               ByRef strUmuFlg As String) As String
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, fupCsv, strCsvKbn, strUmuFlg)

        Dim fileStream As IO.Stream                         '���o�̓X�g���[��
        Dim fileReader As IO.StreamReader                   '�X�g���[�����[�_�[
        Dim strReadLine As String                           '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                     '���C����
        Dim strCsvLine() As String                          'CSV�t�@�C�����e
        Dim strFileMei As String                            'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String               'EDI���쐬��
        Dim strUploadDate As String                         '�A�b�v���[�h����
        Dim dtOkKeikakuKanri As New Data.DataTable          '�v��Ǘ��e�[�u��
        Dim dtOkYoteiMikomiKanri As New Data.DataTable      '�\�茩���Ǘ��e�[�u��
        Dim dtOkFcKeikakuKanri As New Data.DataTable        'FC�p�v��Ǘ��e�[�u��
        Dim dtOkFcYoteiMikomiKanri As New Data.DataTable    'FC�p�\�茩���Ǘ��e�[�u��
        Dim intCsvFieldCount As Integer                     'CSV�t�@�C���t�B�[���h��
        Dim strCsvKbnName As String                         'CSV�敪����
        Dim strErrorNaiyou As String                        '�G���[���e
        Dim intErrorFlg As Integer = 0                      '�G���[FLG
        Dim strEigyouKbn As String                          '�c�Ƌ敪
        Dim strKeikakuNendo As String                       '�v��N�x
        Dim strKameitenCd As String                         '�����X����
        Dim strBusyoCd As String                            '��������
        Dim strSyouhinCd As String                          '�v��Ǘ�_���i�R�[�h
        Dim dtError As New Data.DataTable                   '�G���[
        Dim ninsyou As New NinsyouBC
        Dim strUserId As String                             '���[�U�[ID

        '2013/10/15 ���F�ǉ� ��
        Dim strUccrpdev As String = String.Empty '��A��_�ڋq�敪
        Dim strUccrpseq As String = String.Empty '��A��_�ڋq�R�[�hSEQ
        '2013/10/15 ���F�ǉ� ��

        Try
            '�u���������v�擾
            strUploadDate = objCommonDA.SelSystemDate().Rows(0).Item(0).ToString

            '���[�U�[ID
            strUserId = ninsyou.GetUserID()
            strErrorNaiyou = String.Empty
            strCsvKbnName = String.Empty
            strEigyouKbn = String.Empty
            Select Case strCsvKbn
                Case csvKeikaku
                    '�v��Ǘ��e�[�u�����쐬
                    CreateOkKeikakuKanri(dtOkKeikakuKanri)
                    intCsvFieldCount = 104
                    strCsvKbnName = "�v��b�r�u"
                Case csvMikomi
                    '�\�茩���Ǘ��e�[�u�����쐬
                    CreateOkYoteiMikomiKanri(dtOkYoteiMikomiKanri)
                    intCsvFieldCount = 44
                    strCsvKbnName = "�����b�r�u"
                Case csvFcKeikaku
                    'FC�p�v��Ǘ��e�[�u�����쐬
                    CreateOkFcKeikakuKanri(dtOkFcKeikakuKanri)
                    intCsvFieldCount = 103
                    strCsvKbnName = "�e�b�p�v��b�r�u"
                Case csvFcMikomi
                    'FC�p�\�茩���Ǘ��e�[�u�����쐬
                    CreateOkFcYoteiMikomiKanri(dtOkFcYoteiMikomiKanri)
                    intCsvFieldCount = 43
                    strCsvKbnName = "�e�b�p�����b�r�u"
            End Select

            '�v��Ǘ��\_�捞�G���[���e�[�u�����쐬
            CreateErrorDataTable(dtError)

            'CSV�t�@�C����
            strFileMei = CutMaxLength(fupCsv.FileName, 128)

            '���o�̓X�g���[��
            fileStream = fupCsv.FileContent

            '�X�g���[�����[�_�[
            fileReader = New IO.StreamReader(fileStream, System.Text.Encoding.GetEncoding(932))
            Do
                '�捞�t�@�C����ǂݍ���
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = fileReader.ReadLine()
                intLineCount += 1
            Loop Until fileReader.EndOfStream

            'CSV�A�b�v���[�h��������`�F�b�N
            For i As Integer = strCsvLine.Length - 1 To 0 Step -1
                If Not strCsvLine(i).Trim.Equals(String.Empty) Then
                    If i >= CsvInputMaxLineCount Then
                        Return String.Format(CommonMessage.MSG061E, CStr(CsvInputMaxLineCount))
                    End If
                End If
            Next

            'EDI���쐬��
            strEdiJouhouSakuseiDate = strCsvLine(0).Split(","c)(1).Trim

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 0 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'CSV��ރ`�F�b�N
                    If strReadLine.Split(CChar(","))(0) <> strCsvKbn Then
                        '�G���[���b�Z�[�W��\�����āA�����I������
                        Return String.Format(CommonMessage.MSG056E, strCsvKbnName)
                    End If

                    '�J���}��ǉ�
                    If strReadLine.Split(","c).Length < intCsvFieldCount Then
                        strReadLine = strReadLine & StrDup(intCsvFieldCount - strReadLine.Split(","c).Length, ",")
                    End If

                    '�t�B�[���h���`�F�b�N
                    If strReadLine.Split(","c).Length > intCsvFieldCount Then
                        strErrorNaiyou = CommonMessage.MSG065E
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '�K�{�`�F�b�N
                    If Not ChkNotNull(strReadLine, strCsvKbn) Then
                        Select Case strCsvKbn
                            Case csvKeikaku
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "�v��_�N�x,�����X����,�v��Ǘ�_���i�R�[�h")
                            Case csvMikomi
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "�v��_�N�x,�����X����,�v��Ǘ�_���i�R�[�h")
                            Case csvFcKeikaku
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "�v��_�N�x,��������,�v��Ǘ�_���i�R�[�h")
                            Case csvFcMikomi
                                strErrorNaiyou = String.Format(CommonMessage.MSG046E, "�v��_�N�x,��������,�v��Ǘ�_���i�R�[�h")
                        End Select
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '���ڍő咷�`�F�b�N
                    If Not ChkMaxLength(strReadLine, strCsvKbn) Then
                        strErrorNaiyou = CommonMessage.MSG045E
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '�֑������`�F�b�N
                    If Not ChkKinjiMoji(strReadLine) Then
                        strErrorNaiyou = CommonMessage.MSG044E
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '���l�^���ڃ`�F�b�N
                    If Not ChkSuuti(strReadLine, strCsvKbn) Then
                        strErrorNaiyou = CommonMessage.MSG057E
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    '�����^���ڃ`�F�b�N
                    If ChkSyouSuu(strReadLine, strCsvKbn) = 1 Then
                        strErrorNaiyou = CommonMessage.MSG057E
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    ElseIf ChkSyouSuu(strReadLine, strCsvKbn) = 2 Then
                        strErrorNaiyou = String.Format(CommonMessage.MSG006E, "�H�����藦,�H���󒍗�,���H����", "2", "1")
                        '�G���[�f�[�^���쐬����
                        Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                        intErrorFlg = 1
                    End If

                    If strCsvKbn = csvKeikaku OrElse strCsvKbn = csvMikomi Then
                        strEigyouKbn = strReadLine.Split(","c)(2)
                        strKeikakuNendo = strReadLine.Split(","c)(3)
                        strKameitenCd = strReadLine.Split(","c)(4)
                        strSyouhinCd = strReadLine.Split(","c)(5)
                        '�c�Ƌ敪��2�i�V�K�j�̏ꍇ
                        If strEigyouKbn = "2" AndAlso Not String.IsNullOrEmpty(strKameitenCd) Then
                            '�����X���ޑ��݃`�F�b�N
                            If Not objKeikakuKanriInputDA.SelKameitenCd(strKeikakuNendo, strKameitenCd) Then
                                strErrorNaiyou = String.Format(CommonMessage.MSG022E, "�����X����")
                                '�G���[�f�[�^���쐬����
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1
                            End If
                        End If
                    End If

                    '2013/10/15 ���F�ǉ� ��
                    '�����X�R�[�h��NULL�ł͂Ȃ�
                    If Not String.IsNullOrEmpty(strReadLine.Split(","c)(4).Trim) Then
                        '�����X�R�[�h��8���ł�
                        If strReadLine.Split(","c)(4).Trim.Length.Equals(8) Then

                            '��A�����擾����
                            Dim dt As Data.DataTable
                            dt = objKeikakuKanriInputDA.GetHorennSou(strReadLine.Split(","c)(csvKeikakuItems.kameiten_cd).Trim)
                            '8���̉����X�R�[�hAnd��A���f�[�^���擾�ł���
                            If (dt.Rows.Count = 1) _
                                AndAlso (Not String.IsNullOrEmpty(dt.Rows(0).Item("uccrpdev").ToString)) _
                                AndAlso (Not String.IsNullOrEmpty(dt.Rows(0).Item("uccrpseq").ToString)) Then

                                strUccrpdev = dt.Rows(0).Item("uccrpdev").ToString
                                strUccrpseq = dt.Rows(0).Item("uccrpseq").ToString

                                '8���̉����X�R�[�hAnd��A���f�[�^�͕������擾����
                            ElseIf dt.Rows.Count > 1 Then
                                strErrorNaiyou = CommonMessage.MSG084E
                                '�G���[�f�[�^���쐬����
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1

                                '�����X�R�[�h��8���ł��̂ŁA��A���f�[�^�̎擾�ł��Ȃ�
                            Else
                                strErrorNaiyou = CommonMessage.MSG083E
                                '�G���[�f�[�^���쐬����
                                Call SetKeikakuTorikomiErrorData(i + 1, strErrorNaiyou, dtError)
                                intErrorFlg = 1
                            End If

                        Else
                            '�����X�R�[�h��5���ł��̂ŁA��A���f�[�^�̎擾�͕K�v���Ȃ�
                            If strReadLine.Split(","c)(4).Trim.Length.Equals(5) Then
                                '�u��A��_�ڋq�敪�v�Ɓu��A��_�ڋq�R�[�hSEQ�v��NULL���Z�b�g����
                                strUccrpdev = String.Empty
                                strUccrpseq = String.Empty
                            End If
                        End If
                    End If
                    '2013/10/15 ���F�ǉ� ��

                    '�v��CSV�捞���A�捞�s�`�F�b�N
                    If strCsvKbn = csvKeikaku Then
                        strKeikakuNendo = strReadLine.Split(","c)(3)
                        strKameitenCd = strReadLine.Split(","c)(4)
                        strSyouhinCd = strReadLine.Split(","c)(5)
                        If (Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strKameitenCd) AndAlso Not String.IsNullOrEmpty(strSyouhinCd)) AndAlso _
                           objKeikakuKanriInputDA.SelKeikakuKakuteiCount(strKeikakuNendo, strKameitenCd, strSyouhinCd) Then
                            Return CommonMessage.MSG064E
                        End If
                    End If

                    '�e�b�p�v��CSV�捞���A�捞�s�`�F�b�N
                    If strCsvKbn = csvFcKeikaku Then
                        strKeikakuNendo = strReadLine.Split(","c)(2)
                        strBusyoCd = strReadLine.Split(","c)(3)
                        strSyouhinCd = strReadLine.Split(","c)(4)
                        If (Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strKeikakuNendo) AndAlso Not String.IsNullOrEmpty(strSyouhinCd)) AndAlso _
                         objKeikakuKanriInputDA.SelFCKeikakuKakuteiCount(strKeikakuNendo, strBusyoCd, strSyouhinCd) Then
                            Return CommonMessage.MSG064E
                        End If
                    End If

                    '���i�f�[�^�̏���
                    If intErrorFlg = 0 Then
                        Select Case strCsvKbn
                            Case csvKeikaku
                                '�v��Ǘ�OK�f�[�^�̏���
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkKeikakuKanri, strUccrpdev, strUccrpseq)
                            Case csvMikomi
                                '�\�茩���Ǘ�OK�f�[�^�̏���
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkYoteiMikomiKanri, strUccrpdev, strUccrpseq)
                            Case csvFcKeikaku
                                'FC�p�v��Ǘ�OK�f�[�^�̏���
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkFcKeikakuKanri, strUccrpdev, strUccrpseq)
                            Case csvFcMikomi
                                'FC�p�\�茩���Ǘ�OK�f�[�^�̏���
                                Call Me.SetOkDataRow(strReadLine, strCsvKbn, dtOkFcYoteiMikomiKanri, strUccrpdev, strUccrpseq)
                        End Select
                    Else
                        '�G���[�L����ݒ�
                        strUmuFlg = "1"
                    End If
                End If
            Next

            'CSV�t�@�C�����捞
            Select Case strCsvKbn
                Case csvKeikaku
                    '�v��CSV�捞
                    If Not CsvFileInput(dtOkKeikakuKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvMikomi
                    '����CSV�捞
                    If Not CsvFileInput(dtOkYoteiMikomiKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvFcKeikaku
                    '�e�b�p�v��CSV�捞
                    If Not CsvFileInput(dtOkFcKeikakuKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
                Case csvFcMikomi
                    '�e�b�p����CSV�捞
                    If Not CsvFileInput(dtOkFcYoteiMikomiKanri, dtError, strUploadDate, strUserId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn) Then
                        Return CommonMessage.MSG070E
                    End If
            End Select

        Catch ex As Exception
            Return CommonMessage.MSG069E
        End Try

        Return String.Empty
    End Function

    ''' <summary>
    ''' CSV�t�@�C�����捞����
    ''' </summary>
    ''' <param name="dtOk">OK�f�[�^</param>
    ''' <param name="dtError">�G���[�f�[�^</param>
    ''' <param name="strUploadDate">��������</param>
    ''' <param name="userId">���O�C�����[�U�[ID</param>
    ''' <param name="strFileMei">�t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/20 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, _
                                 ByVal dtError As Data.DataTable, _
                                 ByVal strUploadDate As String, _
                                 ByVal userId As String, _
                                 ByVal strFileMei As String, _
                                 ByVal strEdiJouhouSakuseiDate As String, _
                                 ByVal intErrorFlg As Integer, _
                                 ByVal strCsvKbn As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                               dtOk, dtError, strUploadDate, userId, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, strCsvKbn)

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)

            Try
                If intErrorFlg = 0 Then
                    Select Case strCsvKbn
                        Case csvKeikaku
                            '�v��Ǘ��e�[�u���ɓo�^����
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsKeikakuKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvMikomi
                            '�\�茩���Ǘ��e�[�u���ɓo�^����
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsYoteiMikomiKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvFcKeikaku
                            'FC�p�v��Ǘ��e�[�u���ɓo�^����
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsFCKeikakuKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                        Case csvFcMikomi
                            'FC�p�\�茩���Ǘ��e�[�u���ɓo�^����
                            For i As Integer = 0 To dtOk.Rows.Count - 1
                                If Not objKeikakuKanriInputDA.InsFCYoteiMikomiKanriData(dtOk.Rows(i), userId, strUploadDate) Then
                                    scope.Dispose()
                                    Throw New ApplicationException
                                End If
                            Next
                    End Select
                Else
                    '�v��Ǘ��\_�捞�G���[���e�[�u���ɓo�^����
                    For i As Integer = 0 To dtError.Rows.Count - 1
                        If Not objKeikakuKanriInputDA.InsKeikakuTorikomiError(strEdiJouhouSakuseiDate, strUploadDate, dtError.Rows(i), userId) Then
                            scope.Dispose()
                            Throw New ApplicationException
                        End If
                    Next
                End If

                '�A�b�v���[�h�Ǘ��e�[�u����o�^
                If Not objKeikakuKanriInputDA.InsInputKanri(strUploadDate, strFileMei, strEdiJouhouSakuseiDate, intErrorFlg, userId) Then
                    scope.Dispose()
                    Throw New ApplicationException
                End If

                scope.Complete()

                Return True
            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

        Return True
    End Function


    Private Function GetValue(ByVal strValue As String) As Object

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strValue)

        If String.IsNullOrEmpty(strValue.Trim) Then
            Return System.DBNull.Value
        Else
            Return strValue.Trim
        End If

    End Function

    ''' <summary>
    ''' OK�f�[�^���C����ǉ�����
    ''' </summary>
    ''' <param name="strLine">�f�[�^���C��</param>
    ''' <param name="strCsvKbn">CSV�捞�敪</param>
    ''' <param name="dtOkTable">OK�f�[�^�e�[�u��</param>
    ''' <param name="strUccrpdev">��A��_�ڋq�敪</param>
    ''' <param name="strUccrpseq">��A��_�ڋq�R�[�hSEQ</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/20 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Sub SetOkDataRow(ByVal strLine As String, ByVal strCsvKbn As String, ByRef dtOkTable As Data.DataTable, _
                            ByVal strUccrpdev As String, ByVal strUccrpseq As String)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                strLine, strCsvKbn, dtOkTable, strUccrpdev, strUccrpseq)
        Dim dr As Data.DataRow

        dr = dtOkTable.NewRow
        Select Case strCsvKbn
            Case csvKeikaku
                '�v��Ǘ��e�[�u���̃f�[�^�ǉ�
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvKeikakuItems.keikaku_nendo).Trim
                dr.Item("kameiten_cd") = strLine.Split(","c)(csvKeikakuItems.kameiten_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvKeikakuItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("kameiten_mei") = strLine.Split(","c)(csvKeikakuItems.kameiten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvKeikakuItems.bunbetu_cd).Trim

                '4��_�v�Z�p__���㕽�ϒP��
                dr.Item("4gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_uri_heikin_tanka))

                '4��_�v�Z�p__�d�����ϒP��
                dr.Item("4gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_siire_heikin_tanka))

                '4��_�v�Z�p_�H�����藦
                dr.Item("4gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_koj_hantei_ritu))

                '4��_�v�Z�p_�H���󒍗�
                dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu))

                '4��_�v�Z�p_���H����
                dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu))

                '4��_�v�挏��
                dr.Item("4gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_kensuu))

                '4��_�v����z
                dr.Item("4gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_kingaku))

                '4��_�v��e��
                dr.Item("4gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu4_keikaku_arari))

                '5��_�v�Z�p__���㕽�ϒP��
                dr.Item("5gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_uri_heikin_tanka))

                '5��_�v�Z�p__�d�����ϒP��
                dr.Item("5gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_siire_heikin_tanka))

                '5��_�v�Z�p_�H�����藦
                dr.Item("5gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_koj_hantei_ritu))

                '5��_�v�Z�p_�H���󒍗�
                dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu))

                '5��_�v�Z�p_���H����
                dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu))

                '5��_�v�挏��
                dr.Item("5gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_kensuu))

                '5��_�v����z
                dr.Item("5gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_kingaku))

                '5��_�v��e��
                dr.Item("5gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu5_keikaku_arari))

                '6��_�v�Z�p__���㕽�ϒP��
                dr.Item("6gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_uri_heikin_tanka))

                '6��_�v�Z�p__�d�����ϒP��
                dr.Item("6gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_siire_heikin_tanka))

                '6��_�v�Z�p_�H�����藦
                dr.Item("6gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_koj_hantei_ritu))

                '6��_�v�Z�p_�H���󒍗�
                dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu))

                '6��_�v�Z�p_���H����
                dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu))

                '6��_�v�挏��
                dr.Item("6gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_kensuu))

                '6��_�v����z
                dr.Item("6gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_kingaku))

                '6��_�v��e��
                dr.Item("6gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu6_keikaku_arari))

                '7��_�v�Z�p__���㕽�ϒP��
                dr.Item("7gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_uri_heikin_tanka))

                '7��_�v�Z�p__�d�����ϒP��
                dr.Item("7gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_siire_heikin_tanka))

                '7��_�v�Z�p_�H�����藦
                dr.Item("7gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_koj_hantei_ritu))

                '7��_�v�Z�p_�H���󒍗�
                dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu))

                '7��_�v�Z�p_���H����
                dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu))

                '7��_�v�挏��
                dr.Item("7gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_kensuu))

                '7��_�v����z
                dr.Item("7gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_kingaku))

                '7��_�v��e��
                dr.Item("7gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu7_keikaku_arari))

                '8��_�v�Z�p__���㕽�ϒP��
                dr.Item("8gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_uri_heikin_tanka))

                '8��_�v�Z�p__�d�����ϒP��
                dr.Item("8gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_siire_heikin_tanka))

                '8��_�v�Z�p_�H�����藦
                dr.Item("8gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_koj_hantei_ritu))

                '8��_�v�Z�p_�H���󒍗�
                dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu))

                '8��_�v�Z�p_���H����
                dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu))

                '8��_�v�挏��
                dr.Item("8gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_kensuu))

                '8��_�v����z
                dr.Item("8gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_kingaku))

                '8��_�v��e��
                dr.Item("8gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu8_keikaku_arari))

                '9��_�v�Z�p__���㕽�ϒP��
                dr.Item("9gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_uri_heikin_tanka))

                '9��_�v�Z�p__�d�����ϒP��
                dr.Item("9gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_siire_heikin_tanka))

                '9��_�v�Z�p_�H�����藦
                dr.Item("9gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_koj_hantei_ritu))

                '9��_�v�Z�p_�H���󒍗�
                dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu))

                '9��_�v�Z�p_���H����
                dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu))

                '9��_�v�挏��
                dr.Item("9gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_kensuu))

                '9��_�v����z
                dr.Item("9gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_kingaku))

                '9��_�v��e��
                dr.Item("9gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu9_keikaku_arari))

                '10��_�v�Z�p__���㕽�ϒP��
                dr.Item("10gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_uri_heikin_tanka))

                '10��_�v�Z�p__�d�����ϒP��
                dr.Item("10gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_siire_heikin_tanka))

                '10��_�v�Z�p_�H�����藦
                dr.Item("10gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_koj_hantei_ritu))

                '10��_�v�Z�p_�H���󒍗�
                dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu))

                '10��_�v�Z�p_���H����
                dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu))

                '10��_�v�挏��
                dr.Item("10gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_kensuu))

                '10��_�v����z
                dr.Item("10gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_kingaku))

                '10��_�v��e��
                dr.Item("10gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu10_keikaku_arari))

                '11��_�v�Z�p__���㕽�ϒP��
                dr.Item("11gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_uri_heikin_tanka))

                '11��_�v�Z�p__�d�����ϒP��
                dr.Item("11gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_siire_heikin_tanka))

                '11��_�v�Z�p_�H�����藦
                dr.Item("11gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_koj_hantei_ritu))

                '11��_�v�Z�p_�H���󒍗�
                dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu))

                '11��_�v�Z�p_���H����
                dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu))

                '11��_�v�挏��
                dr.Item("11gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_kensuu))

                '11��_�v����z
                dr.Item("11gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_kingaku))

                '11��_�v��e��
                dr.Item("11gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu11_keikaku_arari))

                '12��_�v�Z�p__���㕽�ϒP��
                dr.Item("12gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_uri_heikin_tanka))

                '12��_�v�Z�p__�d�����ϒP��
                dr.Item("12gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_siire_heikin_tanka))

                '12��_�v�Z�p_�H�����藦
                dr.Item("12gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_koj_hantei_ritu))

                '12��_�v�Z�p_�H���󒍗�
                dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu))

                '12��_�v�Z�p_���H����
                dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu))

                '12��_�v�挏��
                dr.Item("12gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_kensuu))

                '12��_�v����z
                dr.Item("12gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_kingaku))

                '12��_�v��e��
                dr.Item("12gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu12_keikaku_arari))

                '1��_�v�Z�p__���㕽�ϒP��
                dr.Item("1gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_uri_heikin_tanka))

                '1��_�v�Z�p__�d�����ϒP��
                dr.Item("1gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_siire_heikin_tanka))

                '1��_�v�Z�p_�H�����藦
                dr.Item("1gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_koj_hantei_ritu))

                '1��_�v�Z�p_�H���󒍗�
                dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu))

                '1��_�v�Z�p_���H����
                dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu))

                '1��_�v�挏��
                dr.Item("1gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_kensuu))

                '1��_�v����z
                dr.Item("1gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_kingaku))

                '1��_�v��e��
                dr.Item("1gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu1_keikaku_arari))

                '2��_�v�Z�p__���㕽�ϒP��
                dr.Item("2gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_uri_heikin_tanka))

                '2��_�v�Z�p__�d�����ϒP��
                dr.Item("2gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_siire_heikin_tanka))

                '2��_�v�Z�p_�H�����藦
                dr.Item("2gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_koj_hantei_ritu))

                '2��_�v�Z�p_�H���󒍗�
                dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu))

                '2��_�v�Z�p_���H����
                dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu))

                '2��_�v�挏��
                dr.Item("2gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_kensuu))

                '2��_�v����z
                dr.Item("2gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_kingaku))

                '2��_�v��e��
                dr.Item("2gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu2_keikaku_arari))

                '3��_�v�Z�p__���㕽�ϒP��
                dr.Item("3gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_uri_heikin_tanka))

                '3��_�v�Z�p__�d�����ϒP��
                dr.Item("3gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_siire_heikin_tanka))

                '3��_�v�Z�p_�H�����藦
                dr.Item("3gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_koj_hantei_ritu))

                '3��_�v�Z�p_�H���󒍗�
                dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu))

                '3��_�v�Z�p_���H����
                dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu))

                '3��_�v�挏��
                dr.Item("3gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_kensuu))

                '3��_�v����z
                dr.Item("3gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_kingaku))

                '3��_�v��e��
                dr.Item("3gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvKeikakuItems.gatu3_keikaku_arari))

                '2013/10/15 ���F�ǉ� ��
                '��A��_�ڋq�敪
                dr.Item("UCCRPDEV") = Me.GetValue(strUccrpdev)

                '��A��_�ڋq�R�[�hSEQ
                dr.Item("UCCRPSEQ") = Me.GetValue(strUccrpseq)
                '2013/10/15 ���F�ǉ� ��

            Case csvMikomi
                '�\�茩���Ǘ��e�[�u���̃f�[�^�ǉ�
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvMikomiItems.keikaku_nendo).Trim
                dr.Item("kameiten_cd") = strLine.Split(","c)(csvMikomiItems.kameiten_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvMikomiItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("kameiten_mei") = strLine.Split(","c)(csvMikomiItems.kameiten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvMikomiItems.bunbetu_cd).Trim
                '4��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kensuu).Trim) Then
                    dr.Item("4gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kensuu).Trim
                End If
                '4��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kingaku).Trim) Then
                    dr.Item("4gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_kingaku).Trim
                End If
                '4��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_arari).Trim) Then
                    dr.Item("4gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("4gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu4_mikomi_arari).Trim
                End If
                '5��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kensuu).Trim) Then
                    dr.Item("5gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kensuu).Trim
                End If
                '5��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kingaku).Trim) Then
                    dr.Item("5gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_kingaku).Trim
                End If
                '5��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_arari).Trim) Then
                    dr.Item("5gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("5gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu5_mikomi_arari).Trim
                End If
                '6��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kensuu).Trim) Then
                    dr.Item("6gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kensuu).Trim
                End If
                '6��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kingaku).Trim) Then
                    dr.Item("6gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_kingaku).Trim
                End If
                '6��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_arari).Trim) Then
                    dr.Item("6gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("6gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu6_mikomi_arari).Trim
                End If
                '7��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kensuu).Trim) Then
                    dr.Item("7gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kensuu).Trim
                End If
                '7��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kingaku).Trim) Then
                    dr.Item("7gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_kingaku).Trim
                End If
                '7��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_arari).Trim) Then
                    dr.Item("7gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("7gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu7_mikomi_arari).Trim
                End If
                '8��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kensuu).Trim) Then
                    dr.Item("8gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kensuu).Trim
                End If
                '8��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kingaku).Trim) Then
                    dr.Item("8gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_kingaku).Trim
                End If
                '8��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_arari).Trim) Then
                    dr.Item("8gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("8gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu8_mikomi_arari).Trim
                End If
                '9��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kensuu).Trim) Then
                    dr.Item("9gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kensuu).Trim
                End If
                '9��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kingaku).Trim) Then
                    dr.Item("9gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_kingaku).Trim
                End If
                '9��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_arari).Trim) Then
                    dr.Item("9gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("9gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu9_mikomi_arari).Trim
                End If
                '10��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kensuu).Trim) Then
                    dr.Item("10gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kensuu).Trim
                End If
                '10��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kingaku).Trim) Then
                    dr.Item("10gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_kingaku).Trim
                End If
                '10��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_arari).Trim) Then
                    dr.Item("10gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("10gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu10_mikomi_arari).Trim
                End If
                '11��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kensuu).Trim) Then
                    dr.Item("11gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kensuu).Trim
                End If
                '11��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kingaku).Trim) Then
                    dr.Item("11gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_kingaku).Trim
                End If
                '11��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_arari).Trim) Then
                    dr.Item("11gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("11gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu11_mikomi_arari).Trim
                End If
                '12��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kensuu).Trim) Then
                    dr.Item("12gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kensuu).Trim
                End If
                '12��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kingaku).Trim) Then
                    dr.Item("12gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_kingaku).Trim
                End If
                '12��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_arari).Trim) Then
                    dr.Item("12gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("12gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu12_mikomi_arari).Trim
                End If
                '1��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kensuu).Trim) Then
                    dr.Item("1gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kensuu).Trim
                End If
                '1��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kingaku).Trim) Then
                    dr.Item("1gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_kingaku).Trim
                End If
                '1��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_arari).Trim) Then
                    dr.Item("1gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("1gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu1_mikomi_arari).Trim
                End If
                '2��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kensuu).Trim) Then
                    dr.Item("2gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kensuu).Trim
                End If
                '2��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kingaku).Trim) Then
                    dr.Item("2gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_kingaku).Trim
                End If
                '2��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_arari).Trim) Then
                    dr.Item("2gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("2gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu2_mikomi_arari).Trim
                End If
                '3��_��������
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kensuu).Trim) Then
                    dr.Item("3gatu_mikomi_kensuu") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_kensuu") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kensuu).Trim
                End If
                '3��_�������z
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kingaku).Trim) Then
                    dr.Item("3gatu_mikomi_kingaku") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_kingaku") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_kingaku).Trim
                End If
                '3��_�����e��
                If String.IsNullOrEmpty(strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_arari).Trim) Then
                    dr.Item("3gatu_mikomi_arari") = System.DBNull.Value
                Else
                    dr.Item("3gatu_mikomi_arari") = strLine.Split(","c)(csvMikomiItems.gatu3_mikomi_arari).Trim
                End If
            Case csvFcKeikaku
                'FC�p�v��Ǘ��e�[�u���̃f�[�^�ǉ�
                dr.Item("keikaku_nendo") = strLine.Split(","c)(csvFcKeikakuItems.keikaku_nendo).Trim
                dr.Item("busyo_cd") = strLine.Split(","c)(csvFcKeikakuItems.busyo_cd).Trim
                dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvFcKeikakuItems.keikaku_kanri_syouhin_cd).Trim
                dr.Item("siten_mei") = strLine.Split(","c)(csvFcKeikakuItems.siten_mei).Trim
                dr.Item("bunbetu_cd") = strLine.Split(","c)(csvFcKeikakuItems.bunbetu_cd).Trim

                '4��_�v�Z�p__���㕽�ϒP��
                dr.Item("4gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_uri_heikin_tanka))

                '4��_�v�Z�p__�d�����ϒP��
                dr.Item("4gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_siire_heikin_tanka))

                '4��_�v�Z�p_�H�����藦
                dr.Item("4gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu))

                '4��_�v�Z�p_�H���󒍗�
                dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu))

                '4��_�v�Z�p_���H����
                dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu))

                '4��_�v�挏��
                dr.Item("4gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu))

                '4��_�v����z
                dr.Item("4gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku))

                '4��_�v��e��
                dr.Item("4gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari))

                '5��_�v�Z�p__���㕽�ϒP��
                dr.Item("5gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_uri_heikin_tanka))

                '5��_�v�Z�p__�d�����ϒP��
                dr.Item("5gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_siire_heikin_tanka))

                '5��_�v�Z�p_�H�����藦
                dr.Item("5gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu))

                '5��_�v�Z�p_�H���󒍗�
                dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu))

                '5��_�v�Z�p_���H����
                dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu))

                '5��_�v�挏��
                dr.Item("5gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu))

                '5��_�v����z
                dr.Item("5gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku))

                '5��_�v��e��
                dr.Item("5gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari))

                '6��_�v�Z�p__���㕽�ϒP��
                dr.Item("6gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_uri_heikin_tanka))

                '6��_�v�Z�p__�d�����ϒP��
                dr.Item("6gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_siire_heikin_tanka))

                '6��_�v�Z�p_�H�����藦
                dr.Item("6gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu))

                '6��_�v�Z�p_�H���󒍗�
                dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu))

                '6��_�v�Z�p_���H����
                dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu))

                '6��_�v�挏��
                dr.Item("6gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu))

                '6��_�v����z
                dr.Item("6gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku))

                '6��_�v��e��
                dr.Item("6gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari))

                '7��_�v�Z�p__���㕽�ϒP��
                dr.Item("7gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_uri_heikin_tanka))

                '7��_�v�Z�p__�d�����ϒP��
                dr.Item("7gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_siire_heikin_tanka))

                '7��_�v�Z�p_�H�����藦
                dr.Item("7gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu))

                '7��_�v�Z�p_�H���󒍗�
                dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu))

                '7��_�v�Z�p_���H����
                dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu))

                '7��_�v�挏��
                dr.Item("7gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu))

                '7��_�v����z
                dr.Item("7gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku))

                '7��_�v��e��
                dr.Item("7gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari))

                '8��_�v�Z�p__���㕽�ϒP��
                dr.Item("8gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_uri_heikin_tanka))

                '8��_�v�Z�p__�d�����ϒP��
                dr.Item("8gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_siire_heikin_tanka))

                '8��_�v�Z�p_�H�����藦
                dr.Item("8gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu))

                '8��_�v�Z�p_�H���󒍗�
                dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu))

                '8��_�v�Z�p_���H����
                dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu))

                '8��_�v�挏��
                dr.Item("8gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu))

                '8��_�v����z
                dr.Item("8gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku))

                '8��_�v��e��
                dr.Item("8gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari))

                '9��_�v�Z�p__���㕽�ϒP��
                dr.Item("9gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_uri_heikin_tanka))

                '9��_�v�Z�p__�d�����ϒP��
                dr.Item("9gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_siire_heikin_tanka))

                '9��_�v�Z�p_�H�����藦
                dr.Item("9gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu))

                '9��_�v�Z�p_�H���󒍗�
                dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu))

                '9��_�v�Z�p_���H����
                dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu))

                '9��_�v�挏��
                dr.Item("9gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu))

                '9��_�v����z
                dr.Item("9gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku))

                '9��_�v��e��
                dr.Item("9gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari))

                '10��_�v�Z�p__���㕽�ϒP��
                dr.Item("10gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_uri_heikin_tanka))

                '10��_�v�Z�p__�d�����ϒP��
                dr.Item("10gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_siire_heikin_tanka))

                '10��_�v�Z�p_�H�����藦
                dr.Item("10gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu))

                '10��_�v�Z�p_�H���󒍗�
                dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu))

                '10��_�v�Z�p_���H����
                dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu))

                '10��_�v�挏��
                dr.Item("10gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu))

                '10��_�v����z
                dr.Item("10gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku))

                '10��_�v��e��
                dr.Item("10gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari))

                '11��_�v�Z�p__���㕽�ϒP��
                dr.Item("11gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_uri_heikin_tanka))

                '11��_�v�Z�p__�d�����ϒP��
                dr.Item("11gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_siire_heikin_tanka))

                '11��_�v�Z�p_�H�����藦
                dr.Item("11gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu))

                '11��_�v�Z�p_�H���󒍗�
                dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu))

                '11��_�v�Z�p_���H����
                dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu))

                '11��_�v�挏��
                dr.Item("11gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu))

                '11��_�v����z
                dr.Item("11gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku))

                '11��_�v��e��
                dr.Item("11gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari))

                '12��_�v�Z�p__���㕽�ϒP��
                dr.Item("12gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_uri_heikin_tanka))

                '12��_�v�Z�p__�d�����ϒP��
                dr.Item("12gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_siire_heikin_tanka))

                '12��_�v�Z�p_�H�����藦
                dr.Item("12gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu))

                '12��_�v�Z�p_�H���󒍗�
                dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu))

                '12��_�v�Z�p_���H����
                dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu))

                '12��_�v�挏��
                dr.Item("12gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu))

                '12��_�v����z
                dr.Item("12gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku))

                '12��_�v��e��
                dr.Item("12gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari))

                '1��_�v�Z�p__���㕽�ϒP��
                dr.Item("1gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_uri_heikin_tanka))

                '1��_�v�Z�p__�d�����ϒP��
                dr.Item("1gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_siire_heikin_tanka))

                '1��_�v�Z�p_�H�����藦
                dr.Item("1gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu))

                '1��_�v�Z�p_�H���󒍗�
                dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu))

                '1��_�v�Z�p_���H����
                dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu))

                '1��_�v�挏��
                dr.Item("1gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu))

                '1��_�v����z
                dr.Item("1gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku))

                '1��_�v��e��
                dr.Item("1gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari))

                '2��_�v�Z�p__���㕽�ϒP��
                dr.Item("2gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_uri_heikin_tanka))

                '2��_�v�Z�p__�d�����ϒP��
                dr.Item("2gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_siire_heikin_tanka))

                '2��_�v�Z�p_�H�����藦
                dr.Item("2gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu))

                '2��_�v�Z�p_�H���󒍗�
                dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu))

                '2��_�v�Z�p_���H����
                dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu))

                '2��_�v�挏��
                dr.Item("2gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu))

                '2��_�v����z
                dr.Item("2gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku))

                '2��_�v��e��
                dr.Item("2gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari))

                '3��_�v�Z�p__���㕽�ϒP��
                dr.Item("3gatu_keisanyou_uri_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_uri_heikin_tanka))

                '3��_�v�Z�p__�d�����ϒP��
                dr.Item("3gatu_keisanyou_siire_heikin_tanka") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_siire_heikin_tanka))

                '3��_�v�Z�p_�H�����藦
                dr.Item("3gatu_keisanyou_koj_hantei_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu))

                '3��_�v�Z�p_�H���󒍗�
                dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu))

                '3��_�v�Z�p_���H����
                dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu))

                '3��_�v�挏��
                dr.Item("3gatu_keikaku_kensuu") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu))

                '3��_�v����z
                dr.Item("3gatu_keikaku_kingaku") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku))

                '3��_�v��e��
                dr.Item("3gatu_keikaku_arari") = Me.GetValue(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari))



                '    '4��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '4��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '4��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '4��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu).Trim) Then
                '        dr.Item("4gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kensuu).Trim
                '    End If
                '    '4��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku).Trim) Then
                '        dr.Item("4gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_kingaku).Trim
                '    End If
                '    '4��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari).Trim) Then
                '        dr.Item("4gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu4_keikaku_arari).Trim
                '    End If
                '    '5��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '5��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '5��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '5��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu).Trim) Then
                '        dr.Item("5gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kensuu).Trim
                '    End If
                '    '5��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku).Trim) Then
                '        dr.Item("5gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_kingaku).Trim
                '    End If
                '    '5��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari).Trim) Then
                '        dr.Item("5gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu5_keikaku_arari).Trim
                '    End If
                '    '6��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '6��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '6��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '6��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu).Trim) Then
                '        dr.Item("6gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kensuu).Trim
                '    End If
                '    '6��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku).Trim) Then
                '        dr.Item("6gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_kingaku).Trim
                '    End If
                '    '6��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari).Trim) Then
                '        dr.Item("6gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu6_keikaku_arari).Trim
                '    End If
                '    '7��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '7��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '7��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '7��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu).Trim) Then
                '        dr.Item("7gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kensuu).Trim
                '    End If
                '    '7��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku).Trim) Then
                '        dr.Item("7gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_kingaku).Trim
                '    End If
                '    '7��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari).Trim) Then
                '        dr.Item("7gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu7_keikaku_arari).Trim
                '    End If
                '    '8��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '8��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '8��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '8��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu).Trim) Then
                '        dr.Item("8gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kensuu).Trim
                '    End If
                '    '8��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku).Trim) Then
                '        dr.Item("8gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_kingaku).Trim
                '    End If
                '    '8��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari).Trim) Then
                '        dr.Item("8gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu8_keikaku_arari).Trim
                '    End If
                '    '9��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '9��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '9��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '9��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu).Trim) Then
                '        dr.Item("9gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kensuu).Trim
                '    End If
                '    '9��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku).Trim) Then
                '        dr.Item("9gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_kingaku).Trim
                '    End If
                '    '9��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari).Trim) Then
                '        dr.Item("9gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu9_keikaku_arari).Trim
                '    End If
                '    '10��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '10��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '10��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '10��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu).Trim) Then
                '        dr.Item("10gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kensuu).Trim
                '    End If
                '    '10��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku).Trim) Then
                '        dr.Item("10gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_kingaku).Trim
                '    End If
                '    '10��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari).Trim) Then
                '        dr.Item("10gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu10_keikaku_arari).Trim
                '    End If
                '    '11��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '11��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '11��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '11��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu).Trim) Then
                '        dr.Item("11gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kensuu).Trim
                '    End If
                '    '11��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku).Trim) Then
                '        dr.Item("11gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_kingaku).Trim
                '    End If
                '    '11��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari).Trim) Then
                '        dr.Item("11gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu11_keikaku_arari).Trim
                '    End If
                '    '12��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '12��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '12��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '12��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu).Trim) Then
                '        dr.Item("12gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kensuu).Trim
                '    End If
                '    '12��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku).Trim) Then
                '        dr.Item("12gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_kingaku).Trim
                '    End If
                '    '12��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari).Trim) Then
                '        dr.Item("12gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu12_keikaku_arari).Trim
                '    End If
                '    '1��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '1��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '1��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '1��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu).Trim) Then
                '        dr.Item("1gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kensuu).Trim
                '    End If
                '    '1��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku).Trim) Then
                '        dr.Item("1gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_kingaku).Trim
                '    End If
                '    '1��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari).Trim) Then
                '        dr.Item("1gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu1_keikaku_arari).Trim
                '    End If
                '    '2��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '2��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '2��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '2��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu).Trim) Then
                '        dr.Item("2gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kensuu).Trim
                '    End If
                '    '2��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku).Trim) Then
                '        dr.Item("2gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_kingaku).Trim
                '    End If
                '    '2��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari).Trim) Then
                '        dr.Item("2gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu2_keikaku_arari).Trim
                '    End If
                '    '3��_�v�Z�p_�H�����藦
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_koj_hantei_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_koj_hantei_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_hantei_ritu).Trim
                '    End If
                '    '3��_�v�Z�p_�H���󒍗�
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_koj_jyuchuu_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_koj_jyuchuu_ritu).Trim
                '    End If
                '    '3��_�v�Z�p_���H����
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu).Trim) Then
                '        dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keisanyou_tyoku_koj_ritu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keisanyou_tyoku_koj_ritu).Trim
                '    End If
                '    '3��_�v�挏��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu).Trim) Then
                '        dr.Item("3gatu_keikaku_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_kensuu") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kensuu).Trim
                '    End If
                '    '3��_�v����z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku).Trim) Then
                '        dr.Item("3gatu_keikaku_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_kingaku") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_kingaku).Trim
                '    End If
                '    '3��_�v��e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari).Trim) Then
                '        dr.Item("3gatu_keikaku_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_keikaku_arari") = strLine.Split(","c)(csvFcKeikakuItems.gatu3_keikaku_arari).Trim
                '    End If
                'Case csvFcMikomi
                '    'FC�p�\�茩���Ǘ��e�[�u���̃f�[�^�ǉ�
                '    dr.Item("keikaku_nendo") = strLine.Split(","c)(csvFcMikomiItems.keikaku_nendo).Trim
                '    dr.Item("busyo_cd") = strLine.Split(","c)(csvFcMikomiItems.busyo_cd).Trim
                '    dr.Item("keikaku_kanri_syouhin_cd") = strLine.Split(","c)(csvFcMikomiItems.keikaku_kanri_syouhin_cd).Trim
                '    dr.Item("siten_mei") = strLine.Split(","c)(csvFcMikomiItems.siten_mei).Trim
                '    dr.Item("bunbetu_cd") = strLine.Split(","c)(csvFcMikomiItems.bunbetu_cd).Trim
                '    '4��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kensuu).Trim) Then
                '        dr.Item("4gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kensuu).Trim
                '    End If
                '    '4��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kingaku).Trim) Then
                '        dr.Item("4gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_kingaku).Trim
                '    End If
                '    '4��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_arari).Trim) Then
                '        dr.Item("4gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("4gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu4_mikomi_arari).Trim
                '    End If
                '    '5��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kensuu).Trim) Then
                '        dr.Item("5gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kensuu).Trim
                '    End If
                '    '5��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kingaku).Trim) Then
                '        dr.Item("5gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_kingaku).Trim
                '    End If
                '    '5��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_arari).Trim) Then
                '        dr.Item("5gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("5gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu5_mikomi_arari).Trim
                '    End If
                '    '6��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kensuu).Trim) Then
                '        dr.Item("6gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kensuu).Trim
                '    End If
                '    '6��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kingaku).Trim) Then
                '        dr.Item("6gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_kingaku).Trim
                '    End If
                '    '6��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_arari).Trim) Then
                '        dr.Item("6gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("6gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu6_mikomi_arari).Trim
                '    End If
                '    '7��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kensuu).Trim) Then
                '        dr.Item("7gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kensuu).Trim
                '    End If
                '    '7��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kingaku).Trim) Then
                '        dr.Item("7gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_kingaku).Trim
                '    End If
                '    '7��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_arari).Trim) Then
                '        dr.Item("7gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("7gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu7_mikomi_arari).Trim
                '    End If
                '    '8��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kensuu).Trim) Then
                '        dr.Item("8gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kensuu).Trim
                '    End If
                '    '8��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kingaku).Trim) Then
                '        dr.Item("8gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_kingaku).Trim
                '    End If
                '    '8��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_arari).Trim) Then
                '        dr.Item("8gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("8gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu8_mikomi_arari).Trim
                '    End If
                '    '9��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kensuu).Trim) Then
                '        dr.Item("9gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kensuu).Trim
                '    End If
                '    '9��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kingaku).Trim) Then
                '        dr.Item("9gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_kingaku).Trim
                '    End If
                '    '9��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_arari).Trim) Then
                '        dr.Item("9gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("9gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu9_mikomi_arari).Trim
                '    End If
                '    '10��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kensuu).Trim) Then
                '        dr.Item("10gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kensuu).Trim
                '    End If
                '    '10��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kingaku).Trim) Then
                '        dr.Item("10gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_kingaku).Trim
                '    End If
                '    '10��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_arari).Trim) Then
                '        dr.Item("10gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("10gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu10_mikomi_arari).Trim
                '    End If
                '    '11��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kensuu).Trim) Then
                '        dr.Item("11gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kensuu).Trim
                '    End If
                '    '11��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kingaku).Trim) Then
                '        dr.Item("11gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_kingaku).Trim
                '    End If
                '    '11��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_arari).Trim) Then
                '        dr.Item("11gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("11gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu11_mikomi_arari).Trim
                '    End If
                '    '12��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kensuu).Trim) Then
                '        dr.Item("12gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kensuu).Trim
                '    End If
                '    '12��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kingaku).Trim) Then
                '        dr.Item("12gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_kingaku).Trim
                '    End If
                '    '12��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_arari).Trim) Then
                '        dr.Item("12gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("12gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu12_mikomi_arari).Trim
                '    End If
                '    '1��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kensuu).Trim) Then
                '        dr.Item("1gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kensuu).Trim
                '    End If
                '    '1��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kingaku).Trim) Then
                '        dr.Item("1gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_kingaku).Trim
                '    End If
                '    '1��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_arari).Trim) Then
                '        dr.Item("1gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("1gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu1_mikomi_arari).Trim
                '    End If
                '    '2��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kensuu).Trim) Then
                '        dr.Item("2gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kensuu).Trim
                '    End If
                '    '2��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kingaku).Trim) Then
                '        dr.Item("2gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_kingaku).Trim
                '    End If
                '    '2��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_arari).Trim) Then
                '        dr.Item("2gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("2gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu2_mikomi_arari).Trim
                '    End If
                '    '3��_��������
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kensuu).Trim) Then
                '        dr.Item("3gatu_mikomi_kensuu") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_kensuu") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kensuu).Trim
                '    End If
                '    '3��_�������z
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kingaku).Trim) Then
                '        dr.Item("3gatu_mikomi_kingaku") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_kingaku") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_kingaku).Trim
                '    End If
                '    '3��_�����e��
                '    If String.IsNullOrEmpty(strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_arari).Trim) Then
                '        dr.Item("3gatu_mikomi_arari") = System.DBNull.Value
                '    Else
                '        dr.Item("3gatu_mikomi_arari") = strLine.Split(","c)(csvFcMikomiItems.gatu3_mikomi_arari).Trim
                '    End If
        End Select

        dtOkTable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' �v��Ǘ�OK�f�[�^�̃X�L�[�}���쐬
    ''' </summary>
    ''' <param name="dtOk">�v��Ǘ�OK�f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Sub CreateOkKeikakuKanri(ByRef dtOk As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '�v��_�N�x
        dtOk.Columns.Add("kameiten_cd")                        '�����X����
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '�v��Ǘ�_���i�R�[�h
        dtOk.Columns.Add("kameiten_mei")                       '�����X��
        dtOk.Columns.Add("bunbetu_cd")                         '���ʃR�[�h

        dtOk.Columns.Add("4gatu_keisanyou_uri_heikin_tanka")   '4��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("4gatu_keisanyou_siire_heikin_tanka") '4��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("4gatu_keisanyou_koj_hantei_ritu")    '4��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("4gatu_keisanyou_koj_jyuchuu_ritu")   '4��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("4gatu_keisanyou_tyoku_koj_ritu")     '4��_�v�Z�p_���H����
        dtOk.Columns.Add("4gatu_keikaku_kensuu")               '4��_�v�挏��
        dtOk.Columns.Add("4gatu_keikaku_kingaku")              '4��_�v����z
        dtOk.Columns.Add("4gatu_keikaku_arari")                '4��_�v��e��

        dtOk.Columns.Add("5gatu_keisanyou_uri_heikin_tanka")   '5��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("5gatu_keisanyou_siire_heikin_tanka") '5��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("5gatu_keisanyou_koj_hantei_ritu")    '5��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("5gatu_keisanyou_koj_jyuchuu_ritu")   '5��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("5gatu_keisanyou_tyoku_koj_ritu")     '5��_�v�Z�p_���H����
        dtOk.Columns.Add("5gatu_keikaku_kensuu")               '5��_�v�挏��
        dtOk.Columns.Add("5gatu_keikaku_kingaku")              '5��_�v����z
        dtOk.Columns.Add("5gatu_keikaku_arari")                '5��_�v��e����

        dtOk.Columns.Add("6gatu_keisanyou_uri_heikin_tanka")   '6��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("6gatu_keisanyou_siire_heikin_tanka") '6��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("6gatu_keisanyou_koj_hantei_ritu")    '6��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("6gatu_keisanyou_koj_jyuchuu_ritu")   '6��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("6gatu_keisanyou_tyoku_koj_ritu")     '6��_�v�Z�p_���H����
        dtOk.Columns.Add("6gatu_keikaku_kensuu")               '6��_�v�挏��
        dtOk.Columns.Add("6gatu_keikaku_kingaku")              '6��_�v����z
        dtOk.Columns.Add("6gatu_keikaku_arari")                '6��_�v��e��

        dtOk.Columns.Add("7gatu_keisanyou_uri_heikin_tanka")   '7��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("7gatu_keisanyou_siire_heikin_tanka") '7��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("7gatu_keisanyou_koj_hantei_ritu")    '7��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("7gatu_keisanyou_koj_jyuchuu_ritu")   '7��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("7gatu_keisanyou_tyoku_koj_ritu")     '7��_�v�Z�p_���H����
        dtOk.Columns.Add("7gatu_keikaku_kensuu")               '7��_�v�挏��
        dtOk.Columns.Add("7gatu_keikaku_kingaku")              '7��_�v����z
        dtOk.Columns.Add("7gatu_keikaku_arari")                '7��_�v��e��

        dtOk.Columns.Add("8gatu_keisanyou_uri_heikin_tanka")   '8��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("8gatu_keisanyou_siire_heikin_tanka") '8��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("8gatu_keisanyou_koj_hantei_ritu")    '8��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("8gatu_keisanyou_koj_jyuchuu_ritu")   '8��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("8gatu_keisanyou_tyoku_koj_ritu")     '8��_�v�Z�p_���H����
        dtOk.Columns.Add("8gatu_keikaku_kensuu")               '8��_�v�挏��
        dtOk.Columns.Add("8gatu_keikaku_kingaku")              '8��_�v����z
        dtOk.Columns.Add("8gatu_keikaku_arari")                '8��_�v��e��

        dtOk.Columns.Add("9gatu_keisanyou_uri_heikin_tanka")   '9��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("9gatu_keisanyou_siire_heikin_tanka") '9��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("9gatu_keisanyou_koj_hantei_ritu")    '9��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("9gatu_keisanyou_koj_jyuchuu_ritu")   '9��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("9gatu_keisanyou_tyoku_koj_ritu")     '9��_�v�Z�p_���H����
        dtOk.Columns.Add("9gatu_keikaku_kensuu")               '9��_�v�挏��
        dtOk.Columns.Add("9gatu_keikaku_kingaku")              '9��_�v����z
        dtOk.Columns.Add("9gatu_keikaku_arari")                '9��_�v��e��

        dtOk.Columns.Add("10gatu_keisanyou_uri_heikin_tanka")   '10��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("10gatu_keisanyou_siire_heikin_tanka") '10��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("10gatu_keisanyou_koj_hantei_ritu")   '10��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("10gatu_keisanyou_koj_jyuchuu_ritu")  '10��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("10gatu_keisanyou_tyoku_koj_ritu")    '10��_�v�Z�p_���H����
        dtOk.Columns.Add("10gatu_keikaku_kensuu")              '10��_�v�挏��
        dtOk.Columns.Add("10gatu_keikaku_kingaku")             '10��_�v����z
        dtOk.Columns.Add("10gatu_keikaku_arari")               '10��_�v��e��

        dtOk.Columns.Add("11gatu_keisanyou_uri_heikin_tanka")   '11��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("11gatu_keisanyou_siire_heikin_tanka") '11��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("11gatu_keisanyou_koj_hantei_ritu")   '11��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("11gatu_keisanyou_koj_jyuchuu_ritu")  '11��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("11gatu_keisanyou_tyoku_koj_ritu")    '11��_�v�Z�p_���H����
        dtOk.Columns.Add("11gatu_keikaku_kensuu")              '11��_�v�挏��
        dtOk.Columns.Add("11gatu_keikaku_kingaku")             '11��_�v����z
        dtOk.Columns.Add("11gatu_keikaku_arari")               '11��_�v��e��

        dtOk.Columns.Add("12gatu_keisanyou_uri_heikin_tanka")   '12��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("12gatu_keisanyou_siire_heikin_tanka") '12��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("12gatu_keisanyou_koj_hantei_ritu")   '12��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("12gatu_keisanyou_koj_jyuchuu_ritu")  '12��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("12gatu_keisanyou_tyoku_koj_ritu")    '12��_�v�Z�p_���H����
        dtOk.Columns.Add("12gatu_keikaku_kensuu")              '12��_�v�挏��
        dtOk.Columns.Add("12gatu_keikaku_kingaku")             '12��_�v����z
        dtOk.Columns.Add("12gatu_keikaku_arari")               '12��_�v��e��

        dtOk.Columns.Add("1gatu_keisanyou_uri_heikin_tanka")   '1��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("1gatu_keisanyou_siire_heikin_tanka") '1��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("1gatu_keisanyou_koj_hantei_ritu")    '1��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("1gatu_keisanyou_koj_jyuchuu_ritu")   '1��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("1gatu_keisanyou_tyoku_koj_ritu")     '1��_�v�Z�p_���H����
        dtOk.Columns.Add("1gatu_keikaku_kensuu")               '1��_�v�挏��
        dtOk.Columns.Add("1gatu_keikaku_kingaku")              '1��_�v����z
        dtOk.Columns.Add("1gatu_keikaku_arari")                '1��_�v��e��

        dtOk.Columns.Add("2gatu_keisanyou_uri_heikin_tanka")   '2��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("2gatu_keisanyou_siire_heikin_tanka") '2��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("2gatu_keisanyou_koj_hantei_ritu")    '2��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("2gatu_keisanyou_koj_jyuchuu_ritu")   '2��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("2gatu_keisanyou_tyoku_koj_ritu")     '2��_�v�Z�p_���H����
        dtOk.Columns.Add("2gatu_keikaku_kensuu")               '2��_�v�挏��
        dtOk.Columns.Add("2gatu_keikaku_kingaku")              '2��_�v����z
        dtOk.Columns.Add("2gatu_keikaku_arari")                '2��_�v��e��

        dtOk.Columns.Add("3gatu_keisanyou_uri_heikin_tanka")   '3��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("3gatu_keisanyou_siire_heikin_tanka") '3��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("3gatu_keisanyou_koj_hantei_ritu")    '3��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("3gatu_keisanyou_koj_jyuchuu_ritu")   '3��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("3gatu_keisanyou_tyoku_koj_ritu")     '3��_�v�Z�p_���H����
        dtOk.Columns.Add("3gatu_keikaku_kensuu")               '3��_�v�挏��
        dtOk.Columns.Add("3gatu_keikaku_kingaku")              '3��_�v����z
        dtOk.Columns.Add("3gatu_keikaku_arari")                '3��_�v��e��

        '2013/10/14 ���F�ǉ��@��
        dtOk.Columns.Add("UCCRPDEV")                '��A��_�ڋq�敪
        dtOk.Columns.Add("UCCRPSEQ")                '��A��_�ڋq�R�[�hSEQ
        '2013/10/14 ���F�ǉ��@��

    End Sub

    ''' <summary>
    ''' �\�茩���Ǘ�OK�f�[�^�̃X�L�[�}���쐬
    ''' </summary>
    ''' <param name="dtOk">�\�茩���Ǘ�OK�f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Sub CreateOkYoteiMikomiKanri(ByRef dtOk As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '�v��_�N�x
        dtOk.Columns.Add("kameiten_cd")                        '�����X����
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '�v��Ǘ�_���i�R�[�h
        dtOk.Columns.Add("kameiten_mei")                       '�����X��
        dtOk.Columns.Add("bunbetu_cd")                         '���ʃR�[�h
        dtOk.Columns.Add("4gatu_mikomi_kensuu")                '4��_��������
        dtOk.Columns.Add("4gatu_mikomi_kingaku")               '4��_�������z
        dtOk.Columns.Add("4gatu_mikomi_arari")                 '4��_�����e��
        dtOk.Columns.Add("5gatu_mikomi_kensuu")                '5��_��������
        dtOk.Columns.Add("5gatu_mikomi_kingaku")               '5��_�������z
        dtOk.Columns.Add("5gatu_mikomi_arari")                 '5��_�����e��
        dtOk.Columns.Add("6gatu_mikomi_kensuu")                '6��_��������
        dtOk.Columns.Add("6gatu_mikomi_kingaku")               '6��_�������z
        dtOk.Columns.Add("6gatu_mikomi_arari")                 '6��_�����e��
        dtOk.Columns.Add("7gatu_mikomi_kensuu")                '7��_��������
        dtOk.Columns.Add("7gatu_mikomi_kingaku")               '7��_�������z
        dtOk.Columns.Add("7gatu_mikomi_arari")                 '7��_�����e��
        dtOk.Columns.Add("8gatu_mikomi_kensuu")                '8��_��������
        dtOk.Columns.Add("8gatu_mikomi_kingaku")               '8��_�������z
        dtOk.Columns.Add("8gatu_mikomi_arari")                 '8��_�����e��
        dtOk.Columns.Add("9gatu_mikomi_kensuu")                '9��_��������
        dtOk.Columns.Add("9gatu_mikomi_kingaku")               '9��_�������z
        dtOk.Columns.Add("9gatu_mikomi_arari")                 '9��_�����e��
        dtOk.Columns.Add("10gatu_mikomi_kensuu")               '10��_��������
        dtOk.Columns.Add("10gatu_mikomi_kingaku")              '10��_�������z
        dtOk.Columns.Add("10gatu_mikomi_arari")                '10��_�����e��
        dtOk.Columns.Add("11gatu_mikomi_kensuu")               '11��_��������
        dtOk.Columns.Add("11gatu_mikomi_kingaku")              '11��_�������z
        dtOk.Columns.Add("11gatu_mikomi_arari")                '11��_�����e��
        dtOk.Columns.Add("12gatu_mikomi_kensuu")               '12��_��������
        dtOk.Columns.Add("12gatu_mikomi_kingaku")              '12��_�������z
        dtOk.Columns.Add("12gatu_mikomi_arari")                '12��_�����e��
        dtOk.Columns.Add("1gatu_mikomi_kensuu")                '1��_��������
        dtOk.Columns.Add("1gatu_mikomi_kingaku")               '1��_�������z
        dtOk.Columns.Add("1gatu_mikomi_arari")                 '1��_�����e��
        dtOk.Columns.Add("2gatu_mikomi_kensuu")                '2��_��������
        dtOk.Columns.Add("2gatu_mikomi_kingaku")               '2��_�������z
        dtOk.Columns.Add("2gatu_mikomi_arari")                 '2��_�����e��
        dtOk.Columns.Add("3gatu_mikomi_kensuu")                '3��_��������
        dtOk.Columns.Add("3gatu_mikomi_kingaku")               '3��_�������z
        dtOk.Columns.Add("3gatu_mikomi_arari")                 '3��_�����e��
    End Sub

    ''' <summary>
    ''' FC�p�v��Ǘ�OK�f�[�^�̃X�L�[�}���쐬
    ''' </summary>
    ''' <param name="dtOk">FC�p�v��Ǘ�OK�f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Sub CreateOkFcKeikakuKanri(ByRef dtOk As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                           '�v��_�N�x
        dtOk.Columns.Add("busyo_cd")                                '��������
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")                '�v��Ǘ�_���i�R�[�h
        dtOk.Columns.Add("siten_mei")                               '�x�X��
        dtOk.Columns.Add("bunbetu_cd")                              '���ʃR�[�h

        dtOk.Columns.Add("4gatu_keisanyou_uri_heikin_tanka")   '4��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("4gatu_keisanyou_siire_heikin_tanka") '4��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("4gatu_keisanyou_koj_hantei_ritu")         '4��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("4gatu_keisanyou_koj_jyuchuu_ritu")        '4��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("4gatu_keisanyou_tyoku_koj_ritu")          '4��_�v�Z�p_���H����
        dtOk.Columns.Add("4gatu_keikaku_kensuu")                    '4��_�v�挏��
        dtOk.Columns.Add("4gatu_keikaku_kingaku")                   '4��_�v����z
        dtOk.Columns.Add("4gatu_keikaku_arari")                     '4��_�v��e��

        dtOk.Columns.Add("5gatu_keisanyou_uri_heikin_tanka")   '5��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("5gatu_keisanyou_siire_heikin_tanka") '5��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("5gatu_keisanyou_koj_hantei_ritu")         '5��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("5gatu_keisanyou_koj_jyuchuu_ritu")        '5��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("5gatu_keisanyou_tyoku_koj_ritu")          '5��_�v�Z�p_���H����
        dtOk.Columns.Add("5gatu_keikaku_kensuu")                    '5��_�v�挏��
        dtOk.Columns.Add("5gatu_keikaku_kingaku")                   '5��_�v����z
        dtOk.Columns.Add("5gatu_keikaku_arari")                     '5��_�v��e��

        dtOk.Columns.Add("6gatu_keisanyou_uri_heikin_tanka")   '6��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("6gatu_keisanyou_siire_heikin_tanka") '6��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("6gatu_keisanyou_koj_hantei_ritu")         '6��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("6gatu_keisanyou_koj_jyuchuu_ritu")        '6��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("6gatu_keisanyou_tyoku_koj_ritu")          '6��_�v�Z�p_���H����
        dtOk.Columns.Add("6gatu_keikaku_kensuu")                    '6��_�v�挏��
        dtOk.Columns.Add("6gatu_keikaku_kingaku")                   '6��_�v����z
        dtOk.Columns.Add("6gatu_keikaku_arari")                     '6��_�v��e��

        dtOk.Columns.Add("7gatu_keisanyou_uri_heikin_tanka")   '7��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("7gatu_keisanyou_siire_heikin_tanka") '7��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("7gatu_keisanyou_koj_hantei_ritu")         '7��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("7gatu_keisanyou_koj_jyuchuu_ritu")        '7��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("7gatu_keisanyou_tyoku_koj_ritu")          '7��_�v�Z�p_���H����
        dtOk.Columns.Add("7gatu_keikaku_kensuu")                    '7��_�v�挏��
        dtOk.Columns.Add("7gatu_keikaku_kingaku")                   '7��_�v����z
        dtOk.Columns.Add("7gatu_keikaku_arari")                     '7��_�v��e��

        dtOk.Columns.Add("8gatu_keisanyou_uri_heikin_tanka")   '8��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("8gatu_keisanyou_siire_heikin_tanka") '8��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("8gatu_keisanyou_koj_hantei_ritu")         '8��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("8gatu_keisanyou_koj_jyuchuu_ritu")        '8��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("8gatu_keisanyou_tyoku_koj_ritu")          '8��_�v�Z�p_���H����
        dtOk.Columns.Add("8gatu_keikaku_kensuu")                    '8��_�v�挏��
        dtOk.Columns.Add("8gatu_keikaku_kingaku")                   '8��_�v����z
        dtOk.Columns.Add("8gatu_keikaku_arari")                     '8��_�v��e��

        dtOk.Columns.Add("9gatu_keisanyou_uri_heikin_tanka")   '9��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("9gatu_keisanyou_siire_heikin_tanka") '9��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("9gatu_keisanyou_koj_hantei_ritu")         '9��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("9gatu_keisanyou_koj_jyuchuu_ritu")        '9��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("9gatu_keisanyou_tyoku_koj_ritu")          '9��_�v�Z�p_���H����
        dtOk.Columns.Add("9gatu_keikaku_kensuu")                    '9��_�v�挏��
        dtOk.Columns.Add("9gatu_keikaku_kingaku")                   '9��_�v����z
        dtOk.Columns.Add("9gatu_keikaku_arari")                     '9��_�v��e��

        dtOk.Columns.Add("10gatu_keisanyou_uri_heikin_tanka")   '10��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("10gatu_keisanyou_siire_heikin_tanka") '10��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("10gatu_keisanyou_koj_hantei_ritu")        '10��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("10gatu_keisanyou_koj_jyuchuu_ritu")       '10��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("10gatu_keisanyou_tyoku_koj_ritu")         '10��_�v�Z�p_���H����
        dtOk.Columns.Add("10gatu_keikaku_kensuu")                   '10��_�v�挏��
        dtOk.Columns.Add("10gatu_keikaku_kingaku")                  '10��_�v����z
        dtOk.Columns.Add("10gatu_keikaku_arari")                    '10��_�v��e��

        dtOk.Columns.Add("11gatu_keisanyou_uri_heikin_tanka")   '11��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("11gatu_keisanyou_siire_heikin_tanka") '11��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("11gatu_keisanyou_koj_hantei_ritu")        '11��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("11gatu_keisanyou_koj_jyuchuu_ritu")       '11��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("11gatu_keisanyou_tyoku_koj_ritu")         '11��_�v�Z�p_���H����
        dtOk.Columns.Add("11gatu_keikaku_kensuu")                   '11��_�v�挏��
        dtOk.Columns.Add("11gatu_keikaku_kingaku")                  '11��_�v����z
        dtOk.Columns.Add("11gatu_keikaku_arari")                    '11��_�v��e��

        dtOk.Columns.Add("12gatu_keisanyou_uri_heikin_tanka")   '12��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("12gatu_keisanyou_siire_heikin_tanka") '12��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("12gatu_keisanyou_koj_hantei_ritu")        '12��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("12gatu_keisanyou_koj_jyuchuu_ritu")       '12��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("12gatu_keisanyou_tyoku_koj_ritu")         '12��_�v�Z�p_���H����
        dtOk.Columns.Add("12gatu_keikaku_kensuu")                   '12��_�v�挏��
        dtOk.Columns.Add("12gatu_keikaku_kingaku")                  '12��_�v����z
        dtOk.Columns.Add("12gatu_keikaku_arari")                    '12��_�v��e��

        dtOk.Columns.Add("1gatu_keisanyou_uri_heikin_tanka")   '1��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("1gatu_keisanyou_siire_heikin_tanka") '1��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("1gatu_keisanyou_koj_hantei_ritu")         '1��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("1gatu_keisanyou_koj_jyuchuu_ritu")        '1��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("1gatu_keisanyou_tyoku_koj_ritu")          '1��_�v�Z�p_���H����
        dtOk.Columns.Add("1gatu_keikaku_kensuu")                    '1��_�v�挏��
        dtOk.Columns.Add("1gatu_keikaku_kingaku")                   '1��_�v����z
        dtOk.Columns.Add("1gatu_keikaku_arari")                     '1��_�v��e��

        dtOk.Columns.Add("2gatu_keisanyou_uri_heikin_tanka")   '2��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("2gatu_keisanyou_siire_heikin_tanka") '2��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("2gatu_keisanyou_koj_hantei_ritu")         '2��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("2gatu_keisanyou_koj_jyuchuu_ritu")        '2��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("2gatu_keisanyou_tyoku_koj_ritu")          '2��_�v�Z�p_���H����
        dtOk.Columns.Add("2gatu_keikaku_kensuu")                    '2��_�v�挏��
        dtOk.Columns.Add("2gatu_keikaku_kingaku")                   '2��_�v����z
        dtOk.Columns.Add("2gatu_keikaku_arari")                     '2��_�v��e��

        dtOk.Columns.Add("3gatu_keisanyou_uri_heikin_tanka")   '3��_�v�Z�p__���㕽�ϒP��
        dtOk.Columns.Add("3gatu_keisanyou_siire_heikin_tanka") '3��_�v�Z�p__�d�����ϒP��

        dtOk.Columns.Add("3gatu_keisanyou_koj_hantei_ritu")         '3��_�v�Z�p_�H�����藦
        dtOk.Columns.Add("3gatu_keisanyou_koj_jyuchuu_ritu")        '3��_�v�Z�p_�H���󒍗�
        dtOk.Columns.Add("3gatu_keisanyou_tyoku_koj_ritu")          '3��_�v�Z�p_���H����
        dtOk.Columns.Add("3gatu_keikaku_kensuu")                    '3��_�v�挏��
        dtOk.Columns.Add("3gatu_keikaku_kingaku")                   '3��_�v����z
        dtOk.Columns.Add("3gatu_keikaku_arari")                     '3��_�v��e��
        dtOk.Columns.Add("keikaku_kakutei_flg")                     '�v��m��FLG
        dtOk.Columns.Add("keikaku_kakutei_id")                      '�v��m���ID
        dtOk.Columns.Add("keikaku_kakutei_datetime")                '�v��m�����
        dtOk.Columns.Add("kakutei_minaosi_id")                      '�m�茩������ID
        dtOk.Columns.Add("kakutei_minaosi_datetime")                '�m�茩��������
        dtOk.Columns.Add("keikaku_minaosi_flg")                     '�v�挩����FLG
        dtOk.Columns.Add("keikaku_huhen_flg")                       '�v��l�s��FLG
    End Sub

    ''' <summary>
    ''' FC�p�\�茩���Ǘ�OK�f�[�^�̃X�L�[�}���쐬
    ''' </summary>
    ''' <param name="dtOk">FC�p�\�茩���Ǘ�OK�f�[�^</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>																															
    ''' </history>
    Public Sub CreateOkFcYoteiMikomiKanri(ByRef dtOk As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtOk)

        dtOk.Columns.Add("keikaku_nendo")                      '�v��_�N�x
        dtOk.Columns.Add("busyo_cd")                           '��������
        dtOk.Columns.Add("keikaku_kanri_syouhin_cd")           '�v��Ǘ�_���i�R�[�h
        dtOk.Columns.Add("siten_mei")                          '�x�X��
        dtOk.Columns.Add("bunbetu_cd")                         '���ʃR�[�h
        dtOk.Columns.Add("4gatu_mikomi_kensuu")                '4��_��������
        dtOk.Columns.Add("4gatu_mikomi_kingaku")               '4��_�������z
        dtOk.Columns.Add("4gatu_mikomi_arari")                 '4��_�����e��
        dtOk.Columns.Add("5gatu_mikomi_kensuu")                '5��_��������
        dtOk.Columns.Add("5gatu_mikomi_kingaku")               '5��_�������z
        dtOk.Columns.Add("5gatu_mikomi_arari")                 '5��_�����e��
        dtOk.Columns.Add("6gatu_mikomi_kensuu")                '6��_��������
        dtOk.Columns.Add("6gatu_mikomi_kingaku")               '6��_�������z
        dtOk.Columns.Add("6gatu_mikomi_arari")                 '6��_�����e��
        dtOk.Columns.Add("7gatu_mikomi_kensuu")                '7��_��������
        dtOk.Columns.Add("7gatu_mikomi_kingaku")               '7��_�������z
        dtOk.Columns.Add("7gatu_mikomi_arari")                 '7��_�����e��
        dtOk.Columns.Add("8gatu_mikomi_kensuu")                '8��_��������
        dtOk.Columns.Add("8gatu_mikomi_kingaku")               '8��_�������z
        dtOk.Columns.Add("8gatu_mikomi_arari")                 '8��_�����e��
        dtOk.Columns.Add("9gatu_mikomi_kensuu")                '9��_��������
        dtOk.Columns.Add("9gatu_mikomi_kingaku")               '9��_�������z
        dtOk.Columns.Add("9gatu_mikomi_arari")                 '9��_�����e��
        dtOk.Columns.Add("10gatu_mikomi_kensuu")               '10��_��������
        dtOk.Columns.Add("10gatu_mikomi_kingaku")              '10��_�������z
        dtOk.Columns.Add("10gatu_mikomi_arari")                '10��_�����e��
        dtOk.Columns.Add("11gatu_mikomi_kensuu")               '11��_��������
        dtOk.Columns.Add("11gatu_mikomi_kingaku")              '11��_�������z
        dtOk.Columns.Add("11gatu_mikomi_arari")                '11��_�����e��
        dtOk.Columns.Add("12gatu_mikomi_kensuu")               '12��_��������
        dtOk.Columns.Add("12gatu_mikomi_kingaku")              '12��_�������z
        dtOk.Columns.Add("12gatu_mikomi_arari")                '12��_�����e��
        dtOk.Columns.Add("1gatu_mikomi_kensuu")                '1��_��������
        dtOk.Columns.Add("1gatu_mikomi_kingaku")               '1��_�������z
        dtOk.Columns.Add("1gatu_mikomi_arari")                 '1��_�����e��
        dtOk.Columns.Add("2gatu_mikomi_kensuu")                '2��_��������
        dtOk.Columns.Add("2gatu_mikomi_kingaku")               '2��_�������z
        dtOk.Columns.Add("2gatu_mikomi_arari")                 '2��_�����e��
        dtOk.Columns.Add("3gatu_mikomi_kensuu")                '3��_��������
        dtOk.Columns.Add("3gatu_mikomi_kingaku")               '3��_�������z
        dtOk.Columns.Add("3gatu_mikomi_arari")                 '3��_�����e��

    End Sub

    ''' <summary>
    ''' �v��Ǘ��\_�捞�G���[���f�[�^�̃X�L�[�}���쐬
    ''' </summary>
    ''' <param name="dtError">�v��Ǘ��\_�捞�G���[���f�[�^�e�[�u��</param>
    ''' <remarks></remarks>
    ''' <history>																
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬</para>																															
    ''' </history>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtError)

        dtError.Columns.Add("edi_jouhou_sakusei_date")             'EDI���쐬��
        dtError.Columns.Add("syori_datetime")                      '��������
        dtError.Columns.Add("gyou_no")                             '�sNO
        dtError.Columns.Add("error_naiyou")                        '�G���[���e
    End Sub

    ''' <summary>
    ''' �v��Ǘ��\_�捞�G���[���e�[�u�����쐬����
    ''' </summary>
    ''' <param name="intLineNo">�捞�f�[�^����O�s</param>
    ''' <param name="strErrorNaiyou">�G���[�敪</param>
    ''' <param name="dtKeikakuTorikomiError">�v��Ǘ��\_�捞�G���[���e�[�u��</param>
    ''' <remarks>�v��Ǘ��\_�捞�G���[���e�[�u�����쐬����</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Private Sub SetKeikakuTorikomiErrorData(ByVal intLineNo As Integer, _
                                            ByVal strErrorNaiyou As String, _
                                            ByRef dtKeikakuTorikomiError As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                intLineNo, strErrorNaiyou, dtKeikakuTorikomiError)

        Dim dr As Data.DataRow
        dr = dtKeikakuTorikomiError.NewRow
        dr.Item("gyou_no") = intLineNo                                  '�v��_�N�x
        dr.Item("error_naiyou") = strErrorNaiyou.Replace("\r\n", "")    '�G���[�敪

        dtKeikakuTorikomiError.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' �K�{�`�F�b�N
    ''' </summary>
    ''' <param name="strLine">�捞�f�[�^����O�s</param>
    ''' <param name="strCsvKbn">�捞CSV�敪</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s�K�{���݃`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function ChkNotNull(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                For Each i As Integer In KEIKAKU_NOTNULL_INDEX
                    '�f�[�^���ڂ�K�{�`�F�b�N
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvMikomi
                For Each i As Integer In MIKOMI_NOTNULL_INDEX
                    '�f�[�^���ڂ�K�{�`�F�b�N
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvFcKeikaku
                For Each i As Integer In FC_KEIKAKU_NOTNULL_INDEX
                    '�f�[�^���ڂ�K�{�`�F�b�N
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
            Case csvFcMikomi
                For Each i As Integer In FC_MIKOMI_NOTNULL_INDEX
                    '�f�[�^���ڂ�K�{�`�F�b�N
                    If String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) Then
                        Return False
                    End If
                Next
        End Select

        Return True
    End Function

    ''' <summary>
    ''' ���ڍő咷�`�F�b�N
    ''' </summary>
    ''' <param name="strLine">����O�s</param>
    ''' <param name="strCsvKbn">�捞CSV�敪</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s�e���ڍő咷�`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function ChkMaxLength(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        For i As Integer = 0 To strLine.Split(CChar(",")).Length - 1
            Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(CChar(","))(i))
            Select Case strCsvKbn
                Case csvKeikaku
                    If i < KEIKAKU_MAX_LENGTH.Length AndAlso btBytes.LongLength > KEIKAKU_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvMikomi
                    If i < MIKOMI_MAX_LENGTH.Length AndAlso btBytes.LongLength > MIKOMI_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvFcKeikaku
                    If i < FC_KEIKAKU_MAX_LENGTH.Length AndAlso btBytes.LongLength > FC_KEIKAKU_MAX_LENGTH(i) Then
                        Return False
                    End If
                Case csvFcMikomi
                    If i < FC_MIKOMI_MAX_LENGTH.Length AndAlso btBytes.LongLength > FC_MIKOMI_MAX_LENGTH(i) Then
                        Return False
                    End If
            End Select
        Next

        Return True
    End Function

    ''' <summary>
    ''' �ő咷��؂���
    ''' </summary>
    ''' <param name="strValue">�捞�t�@�C���ڍ�</param>
    ''' <param name="intMaxByteCount">"128"���Œ�</param>
    ''' <returns>�捞�t�@�C���̖���</returns>
    ''' <remarks>�捞�t�@�C���̖��̂��擾����</remarks>
    ''' <history>
    ''' <para>2012/12/20 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strValue, intMaxByteCount)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>
    ''' �֑������`�F�b�N
    ''' </summary>
    ''' <param name="target">����O����</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O���ڋ֑������`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/20 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function ChkKinjiMoji(ByVal target As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, target)

        For Each st As String In arrayKinsiStr

            If target.IndexOf(st) >= 0 Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' ���l�^���ڃ`�F�b�N
    ''' </summary>
    ''' <param name="strLine">�捞�f�[�^����O�s</param>
    ''' <param name="strCsvKbn">�捞CSV�敪</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s���l�^���ڃ`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function ChkSuuti(ByVal strLine As String, ByVal strCsvKbn As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                '�v��CSV�̐��l�^���ڃ`�F�b�N
                For Each i As Integer In KEIKAKU_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvMikomi
                '����CSV�̐��l�^���ڃ`�F�b�N
                For Each i As Integer In MIKOMI_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvFcKeikaku
                '�e�b�p�v��CSV�̐��l�^���ڃ`�F�b�N
                For Each i As Integer In FC_KEIKAKU_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
            Case csvFcMikomi
                '�e�b�p����CSV�̐��l�^���ڃ`�F�b�N
                For Each i As Integer In FC_MIKOMI_NUM_INDEX

                    If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                        Return False
                    End If
                Next
        End Select

        Return True
    End Function

    ''' <summary>
    ''' �����^���ڃ`�F�b�N
    ''' </summary>
    ''' <param name="strLine">�捞�f�[�^����O�s</param>
    ''' <param name="strCsvKbn">�捞CSV�敪</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s�����^���ڃ`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function ChkSyouSuu(ByVal strLine As String, ByVal strCsvKbn As String) As Integer
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, strCsvKbn)

        Select Case strCsvKbn
            Case csvKeikaku
                '�v��CSV�̏����^���ڃ`�F�b�N
                For Each i As Integer In KEIKAKU_DEC_INDEX
                    If strLine.Split(CChar(","))(i).Trim <> "100" Then
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i).Replace(".", ""))) Then
                            Return 1
                        End If
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckDecimal(strLine.Split(CChar(","))(i), 2, 1)) Then
                            Return 2
                        End If
                    End If
                Next
            Case csvFcKeikaku
                '�e�b�p�v��CSV�̏����^���ڃ`�F�b�N
                For Each i As Integer In FC_KEIKAKU_DEC_INDEX
                    If strLine.Split(CChar(","))(i).Trim <> "100" Then
                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i).Replace(".", ""))) Then
                            Return 1
                        End If

                        If Not String.IsNullOrEmpty(strLine.Split(CChar(","))(i).Trim) AndAlso (Not CheckDecimal(strLine.Split(CChar(","))(i), 2, 1)) Then
                            Return 2
                        End If
                    End If
                Next
        End Select

        Return 0
    End Function

    ''' <summary>
    ''' �����`�F�b�N
    ''' </summary>
    ''' <param name="inTarget">����O����</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O���ڐ����`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Function CheckHankaku(ByVal inTarget As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, inTarget)

        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Long = CLng(inTarget)
            Catch ex As Exception
                Return False
            End Try

            If InStr(inTarget, ".") > 0 Or InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' ������ + �����̃`�F�b�N
    ''' </summary>
    ''' <param name="checkValue">�`�F�b�N�Ώ�</param>
    ''' <param name="seisuuCount">��������</param>
    ''' <param name="syousuuCount">��������</param>
    ''' <returns>�`�F�b�N����</returns>
    ''' <remarks>������ + �����̃`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Public Function CheckDecimal(ByVal checkValue As String, _
                                 ByVal seisuuCount As Integer, _
                                 ByVal syousuuCount As Integer) As Boolean
        Dim patternValue As String
        If syousuuCount > 0 Then
            patternValue = "^[0-9]{1," & Convert.ToInt32(seisuuCount) & _
                           "}$|^[0-9]{1," & Convert.ToInt32(seisuuCount) & _
                           "}\.[0-9]{1," & Convert.ToInt32(syousuuCount) & "}$"

        Else
            patternValue = "^[0-9]{1," & Convert.ToInt32(seisuuCount) & "}$"

        End If

        Return CheckRegex(checkValue, patternValue)
    End Function

    ''' <summary>
    ''' ���K�\������
    ''' </summary>
    ''' <param name="checkValue">���䕶��</param>
    ''' <param name="patternValue">���K�\����</param>
    ''' <returns>���K�\������</returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' <para>2012/12/21 P-44979 ���h�m �V�K�쐬 </para>
    ''' </history>
    Private Function CheckRegex(ByVal checkValue As String, ByVal patternValue As String) As Boolean
        If checkValue.Equals(String.Empty) Then
            Return True
        End If

        Return Regex.Match(checkValue, patternValue).Success
    End Function
End Class
