''' <summary>
'''  �v��Ǘ��̃N���X
''' </summary>
''' <remarks>2012/11/28 ��</remarks>
<System.Serializable()> Public Class KeikakuKanriRecord
    ''' <summary>
    ''' POP��ʂ̋敪
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum popKbn As Integer
        ''' <summary>
        ''' �x�X
        ''' </summary>
        ''' <remarks></remarks>
        Shiten = 1
        ''' <summary>
        ''' �c�ƃ}��
        ''' </summary>
        ''' <remarks></remarks>
        User = 2
        ''' <summary>
        ''' �����X
        ''' </summary>
        ''' <remarks></remarks>
        Kameiten = 3
        ''' <summary>
        ''' �n��
        ''' </summary>
        ''' <remarks></remarks>
        Keiretu = 4
        ''' <summary>
        ''' �c�Ə�
        ''' </summary>
        ''' <remarks></remarks>
        Eigyou = 5
        ''' <summary>
        ''' �c�Ə�
        ''' </summary>
        ''' <remarks></remarks>
        TouituHoujin = 6
        ''' <summary>
        ''' �c�Ə�
        ''' </summary>
        ''' <remarks></remarks>
        Houjin = 7

    End Enum
    ''' <summary>
    ''' ����SQL�敪
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum selectKbn
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        meisai = 0
        ''' <summary>
        ''' ���v
        ''' </summary>
        ''' <remarks></remarks>
        syoukei = 1
        ''' <summary>
        ''' �S�́iFC���O�j
        ''' </summary>
        ''' <remarks></remarks>
        goukei = 2
        ''' <summary>
        ''' FC���v
        ''' </summary>
        ''' <remarks></remarks>
        FC = 3
        ''' <summary>
        ''' �S�̍��v
        ''' </summary>
        ''' <remarks></remarks>
        goukeiFC = 4
    End Enum
    Private arrHead(selectIndex.max) As String
    Public Function GetHeadString(ByVal row As Integer, ByVal index As Integer) As String
        If row = 0 Then
            arrHead(0) = "�r���_�[���"
            arrHead(1) = "�r���_�[���"
            arrHead(2) = "�r���_�[���"
            arrHead(3) = "�r���_�[���"
            arrHead(4) = "�r���_�[���"
            arrHead(5) = "�r���_�[���"
            arrHead(6) = "�r���_�[���"
            arrHead(7) = "�O�N�f�[�^"
            arrHead(8) = "�r���_�[���"
            arrHead(9) = "�O�N�f�[�^"
            arrHead(10) = "�O�N�f�[�^"
            arrHead(11) = "�O�N�f�[�^"
            arrHead(12) = "�O�N�f�[�^"
            arrHead(13) = "�O�N�f�[�^"
            arrHead(14) = "�O�N�f�[�^"
            arrHead(15) = "����"
            arrHead(16) = "����"
            arrHead(17) = "����"
            arrHead(18) = "����"
            arrHead(19) = "����"
            arrHead(20) = "����"
            arrHead(21) = "����"
            arrHead(22) = "����"
            arrHead(23) = "����"
            arrHead(24) = "����"
            arrHead(25) = "����"
            arrHead(26) = "����"
            arrHead(27) = "����"
            arrHead(28) = "����"
            arrHead(29) = "����"
            arrHead(30) = "����"
            arrHead(31) = "����"
            arrHead(32) = "����"
            arrHead(33) = "����"
            arrHead(34) = "����"
            arrHead(35) = "����"
            arrHead(36) = "����"
            arrHead(37) = "����"
            arrHead(38) = "����"
            arrHead(39) = "����"
            arrHead(40) = "����"
            arrHead(41) = "����"
            arrHead(42) = "����"
            arrHead(43) = "����"
            arrHead(44) = "����"
            arrHead(45) = "����"
            arrHead(46) = "����"
            arrHead(47) = "����"
            arrHead(48) = "����"
            arrHead(49) = "����"
            arrHead(50) = "����"
            arrHead(51) = "�v��"
            arrHead(52) = "�v��"
            arrHead(53) = "�v��"
            arrHead(54) = "�v��"
            arrHead(55) = "�v��"
            arrHead(56) = "�v��"
            arrHead(57) = "�v��"
            arrHead(58) = "�v��"
            arrHead(59) = "�v��"
            arrHead(60) = "�v��"
            arrHead(61) = "�v��"
            arrHead(62) = "�v��"
            arrHead(63) = "�v��"
            arrHead(64) = "�v��"
            arrHead(65) = "�v��"
            arrHead(66) = "�v��"
            arrHead(67) = "�v��"
            arrHead(68) = "�v��"
            arrHead(69) = "�v��"
            arrHead(70) = "�v��"
            arrHead(71) = "�v��"
            arrHead(72) = "�v��"
            arrHead(73) = "�v��"
            arrHead(74) = "�v��"
            arrHead(75) = "�v��"
            arrHead(76) = "�v��"
            arrHead(77) = "�v��"
            arrHead(78) = "�v��"
            arrHead(79) = "�v��"
            arrHead(80) = "�v��"
            arrHead(81) = "�v��"
            arrHead(82) = "�v��"
            arrHead(83) = "�v��"
            arrHead(84) = "�v��"
            arrHead(85) = "�v��"
            arrHead(86) = "�v��"
            arrHead(87) = "����"
            arrHead(88) = "����"
            arrHead(89) = "����"
            arrHead(90) = "����"
            arrHead(91) = "����"
            arrHead(92) = "����"
            arrHead(93) = "����"
            arrHead(94) = "����"
            arrHead(95) = "����"
            arrHead(96) = "����"
            arrHead(97) = "����"
            arrHead(98) = "����"
            arrHead(99) = "����"
            arrHead(100) = "����"
            arrHead(101) = "����"
            arrHead(102) = "����"
            arrHead(103) = "����"
            arrHead(104) = "����"
            arrHead(105) = "����"
            arrHead(106) = "����"
            arrHead(107) = "����"
            arrHead(108) = "����"
            arrHead(109) = "����"
            arrHead(110) = "����"
            arrHead(111) = "����"
            arrHead(112) = "����"
            arrHead(113) = "����"
            arrHead(114) = "����"
            arrHead(115) = "����"
            arrHead(116) = "����"
            arrHead(117) = "����"
            arrHead(118) = "����"
            arrHead(119) = "����"
            arrHead(120) = "����"
            arrHead(121) = "����"
            arrHead(122) = "����"
            arrHead(123) = "�O�N����"
            arrHead(124) = "�O�N����"
            arrHead(125) = "�O�N����"
            arrHead(126) = "�O�N����"
            arrHead(127) = "�O�N����"
            arrHead(128) = "�O�N����"
            arrHead(129) = "�O�N����"
            arrHead(130) = "�O�N����"
            arrHead(131) = "�O�N����"
            arrHead(132) = "�O�N����"
            arrHead(133) = "�O�N����"
            arrHead(134) = "�O�N����"
            arrHead(135) = "�O�N����"
            arrHead(136) = "�O�N����"
            arrHead(137) = "�O�N����"
            arrHead(138) = "�O�N����"
            arrHead(139) = "�O�N����"
            arrHead(140) = "�O�N����"
            arrHead(141) = "�O�N����"
            arrHead(142) = "�O�N����"
            arrHead(143) = "�O�N����"
            arrHead(144) = "�O�N����"
            arrHead(145) = "�O�N����"
            arrHead(146) = "�O�N����"
            arrHead(147) = "�O�N����"
            arrHead(148) = "�O�N����"
            arrHead(149) = "�O�N����"
            arrHead(150) = "�O�N����"
            arrHead(151) = "�O�N����"
            arrHead(152) = "�O�N����"
            arrHead(153) = "�O�N����"
            arrHead(154) = "�O�N����"
            arrHead(155) = "�O�N����"
            arrHead(156) = "�O�N����"
            arrHead(157) = "�O�N����"
            arrHead(158) = "�O�N����"
            arrHead(159) = "�H���䗦�i�v�Z�p�j"
            arrHead(160) = "�H���䗦�i�v�Z�p�j"
            arrHead(161) = "�H���䗦�i�v�Z�p�j"
            arrHead(162) = "�H���䗦�i�v�Z�p�j"
            arrHead(163) = "�H���䗦�i�v�Z�p�j"
            arrHead(164) = "�H���䗦�i�v�Z�p�j"
            arrHead(165) = "�H���䗦�i�v�Z�p�j"
            arrHead(166) = "�H���䗦�i�v�Z�p�j"
            arrHead(167) = "�H���䗦�i�v�Z�p�j"
            arrHead(168) = "�H���䗦�i�v�Z�p�j"
            arrHead(169) = "�H���䗦�i�v�Z�p�j"
            arrHead(170) = "�H���䗦�i�v�Z�p�j"
            arrHead(171) = "�H���䗦�i�v�Z�p�j"
            arrHead(172) = "�H���䗦�i�v�Z�p�j"
            arrHead(173) = "�H���䗦�i�v�Z�p�j"
            arrHead(174) = "�H���䗦�i�v�Z�p�j"
            arrHead(175) = "�H���䗦�i�v�Z�p�j"
            arrHead(176) = "�H���䗦�i�v�Z�p�j"
            arrHead(177) = "�H���䗦�i�v�Z�p�j"
            arrHead(178) = "�H���䗦�i�v�Z�p�j"
            arrHead(179) = "�H���䗦�i�v�Z�p�j"
            arrHead(180) = "�H���䗦�i�v�Z�p�j"
            arrHead(181) = "�H���䗦�i�v�Z�p�j"
            arrHead(182) = "�H���䗦�i�v�Z�p�j"
            arrHead(183) = "�H���䗦�i�v�Z�p�j"
            arrHead(184) = "�H���䗦�i�v�Z�p�j"
            arrHead(185) = "�H���䗦�i�v�Z�p�j"
            arrHead(186) = "�H���䗦�i�v�Z�p�j"
            arrHead(187) = "�H���䗦�i�v�Z�p�j"
            arrHead(188) = "�H���䗦�i�v�Z�p�j"
            arrHead(189) = "�H���䗦�i�v�Z�p�j"
            arrHead(190) = "�H���䗦�i�v�Z�p�j"
            arrHead(191) = "�H���䗦�i�v�Z�p�j"
            arrHead(192) = "�H���䗦�i�v�Z�p�j"
            arrHead(193) = "�H���䗦�i�v�Z�p�j"
            arrHead(194) = "�H���䗦�i�v�Z�p�j"
            arrHead(195) = "�c�Ƌ敪"
            arrHead(196) = "�o�^����"
            arrHead(197) = "����_���ϒP��"
            arrHead(198) = "����_���ϒP��"
            arrHead(199) = "����_���ϒP��"
            arrHead(200) = "����_���ϒP��"
            arrHead(201) = "����_���ϒP��"
            arrHead(202) = "����_���ϒP��"
            arrHead(203) = "����_���ϒP��"
            arrHead(204) = "����_���ϒP��"
            arrHead(205) = "����_���ϒP��"
            arrHead(206) = "����_���ϒP��"
            arrHead(207) = "����_���ϒP��"
            arrHead(208) = "����_���ϒP��"
            arrHead(209) = "����_���ϒP��"
            arrHead(210) = "����_���ϒP��"
            arrHead(211) = "����_���ϒP��"
            arrHead(212) = "����_���ϒP��"
            arrHead(213) = "����_���ϒP��"
            arrHead(214) = "����_���ϒP��"
            arrHead(215) = "����_���ϒP��"
            arrHead(216) = "����_���ϒP��"
            arrHead(217) = "����_���ϒP��"
            arrHead(218) = "����_���ϒP��"
            arrHead(219) = "����_���ϒP��"
            arrHead(220) = "����_���ϒP��"
            arrHead(221) = "���ʃR�[�h�\����"
            arrHead(222) = "�c�Ƌ敪�\����"
            arrHead(223) = "���ׁA���v�A���v�敪"
            arrHead(224) = "�O�N�f�[�^"
            arrHead(225) = "�O�N�f�[�^"
            arrHead(226) = "���i�}�X�^"
            arrHead(227) = "���i�}�X�^"
            arrHead(228) = "�����X���Ͻ�"
            arrHead(229) = "�v��"
            arrHead(230) = "���i�}�X�^"
            arrHead(231) = "���i�}�X�^"
            arrHead(232) = "���i�}�X�^"
            arrHead(233) = "���i�}�X�^"

        Else
            arrHead(0) = "�����X��"
            arrHead(1) = "�����X����"
            arrHead(2) = "�c�Ƌ敪"
            arrHead(3) = "�c�ƒS����"
            arrHead(4) = "�N�ԓ���"
            arrHead(5) = "�v��p_�N�ԓ���"
            arrHead(6) = "�Ƒ�"
            arrHead(7) = "����䗦"
            arrHead(8) = "SDS�J�n�N��"
            arrHead(9) = "�H�����藦"
            arrHead(10) = "�H���󒍗�"
            arrHead(11) = "���H����"
            arrHead(12) = "���i����"
            arrHead(13) = "���i��"
            arrHead(14) = "���ϒP��"
            arrHead(15) = "4��_���ь���"
            arrHead(16) = "4��_���ы��z"
            arrHead(17) = "4��_���ёe��"
            arrHead(18) = "5��_���ь���"
            arrHead(19) = "5��_���ы��z"
            arrHead(20) = "5��_���ёe��"
            arrHead(21) = "6��_���ь���"
            arrHead(22) = "6��_���ы��z"
            arrHead(23) = "6��_���ёe��"
            arrHead(24) = "7��_���ь���"
            arrHead(25) = "7��_���ы��z"
            arrHead(26) = "7��_���ёe��"
            arrHead(27) = "8��_���ь���"
            arrHead(28) = "8��_���ы��z"
            arrHead(29) = "8��_���ёe��"
            arrHead(30) = "9��_���ь���"
            arrHead(31) = "9��_���ы��z"
            arrHead(32) = "9��_���ёe��"
            arrHead(33) = "10��_���ь���"
            arrHead(34) = "10��_���ы��z"
            arrHead(35) = "10��_���ёe��"
            arrHead(36) = "11��_���ь���"
            arrHead(37) = "11��_���ы��z"
            arrHead(38) = "11��_���ёe��"
            arrHead(39) = "12��_���ь���"
            arrHead(40) = "12��_���ы��z"
            arrHead(41) = "12��_���ёe��"
            arrHead(42) = "1��_���ь���"
            arrHead(43) = "1��_���ы��z"
            arrHead(44) = "1��_���ёe��"
            arrHead(45) = "2��_���ь���"
            arrHead(46) = "2��_���ы��z"
            arrHead(47) = "2��_���ёe��"
            arrHead(48) = "3��_���ь���"
            arrHead(49) = "3��_���ы��z"
            arrHead(50) = "3��_���ёe��"
            arrHead(51) = "4��_�v�挏��"
            arrHead(52) = "4��_�v����z"
            arrHead(53) = "4��_�v��e��"
            arrHead(54) = "5��_�v�挏��"
            arrHead(55) = "5��_�v����z"
            arrHead(56) = "5��_�v��e��"
            arrHead(57) = "6��_�v�挏��"
            arrHead(58) = "6��_�v����z"
            arrHead(59) = "6��_�v��e��"
            arrHead(60) = "7��_�v�挏��"
            arrHead(61) = "7��_�v����z"
            arrHead(62) = "7��_�v��e��"
            arrHead(63) = "8��_�v�挏��"
            arrHead(64) = "8��_�v����z"
            arrHead(65) = "8��_�v��e��"
            arrHead(66) = "9��_�v�挏��"
            arrHead(67) = "9��_�v����z"
            arrHead(68) = "9��_�v��e��"
            arrHead(69) = "10��_�v�挏��"
            arrHead(70) = "10��_�v����z"
            arrHead(71) = "10��_�v��e��"
            arrHead(72) = "11��_�v�挏��"
            arrHead(73) = "11��_�v����z"
            arrHead(74) = "11��_�v��e��"
            arrHead(75) = "12��_�v�挏��"
            arrHead(76) = "12��_�v����z"
            arrHead(77) = "12��_�v��e��"
            arrHead(78) = "1��_�v�挏��"
            arrHead(79) = "1��_�v����z"
            arrHead(80) = "1��_�v��e��"
            arrHead(81) = "2��_�v�挏��"
            arrHead(82) = "2��_�v����z"
            arrHead(83) = "2��_�v��e��"
            arrHead(84) = "3��_�v�挏��"
            arrHead(85) = "3��_�v����z"
            arrHead(86) = "3��_�v��e��"
            arrHead(87) = "4��_��������"
            arrHead(88) = "4��_�������z"
            arrHead(89) = "4��_�����e��"
            arrHead(90) = "5��_��������"
            arrHead(91) = "5��_�������z"
            arrHead(92) = "5��_�����e��"
            arrHead(93) = "6��_��������"
            arrHead(94) = "6��_�������z"
            arrHead(95) = "6��_�����e��"
            arrHead(96) = "7��_��������"
            arrHead(97) = "7��_�������z"
            arrHead(98) = "7��_�����e��"
            arrHead(99) = "8��_��������"
            arrHead(100) = "8��_�������z"
            arrHead(101) = "8��_�����e��"
            arrHead(102) = "9��_��������"
            arrHead(103) = "9��_�������z"
            arrHead(104) = "9��_�����e��"
            arrHead(105) = "10��_��������"
            arrHead(106) = "10��_�������z"
            arrHead(107) = "10��_�����e��"
            arrHead(108) = "11��_��������"
            arrHead(109) = "11��_�������z"
            arrHead(110) = "11��_�����e��"
            arrHead(111) = "12��_��������"
            arrHead(112) = "12��_�������z"
            arrHead(113) = "12��_�����e��"
            arrHead(114) = "1��_��������"
            arrHead(115) = "1��_�������z"
            arrHead(116) = "1��_�����e��"
            arrHead(117) = "2��_��������"
            arrHead(118) = "2��_�������z"
            arrHead(119) = "2��_�����e��"
            arrHead(120) = "3��_��������"
            arrHead(121) = "3��_�������z"
            arrHead(122) = "3��_�����e��"
            arrHead(123) = "4��_���ь���"
            arrHead(124) = "4��_���ы��z"
            arrHead(125) = "4��_���ёe��"
            arrHead(126) = "5��_���ь���"
            arrHead(127) = "5��_���ы��z"
            arrHead(128) = "5��_���ёe��"
            arrHead(129) = "6��_���ь���"
            arrHead(130) = "6��_���ы��z"
            arrHead(131) = "6��_���ёe��"
            arrHead(132) = "7��_���ь���"
            arrHead(133) = "7��_���ы��z"
            arrHead(134) = "7��_���ёe��"
            arrHead(135) = "8��_���ь���"
            arrHead(136) = "8��_���ы��z"
            arrHead(137) = "8��_���ёe��"
            arrHead(138) = "9��_���ь���"
            arrHead(139) = "9��_���ы��z"
            arrHead(140) = "9��_���ёe��"
            arrHead(141) = "10��_���ь���"
            arrHead(142) = "10��_���ы��z"
            arrHead(143) = "10��_���ёe��"
            arrHead(144) = "11��_���ь���"
            arrHead(145) = "11��_���ы��z"
            arrHead(146) = "11��_���ёe��"
            arrHead(147) = "12��_���ь���"
            arrHead(148) = "12��_���ы��z"
            arrHead(149) = "12��_���ёe��"
            arrHead(150) = "1��_���ь���"
            arrHead(151) = "1��_���ы��z"
            arrHead(152) = "1��_���ёe��"
            arrHead(153) = "2��_���ь���"
            arrHead(154) = "2��_���ы��z"
            arrHead(155) = "2��_���ёe��"
            arrHead(156) = "3��_���ь���"
            arrHead(157) = "3��_���ы��z"
            arrHead(158) = "3��_���ёe��"
            arrHead(159) = "4��_�v�Z�p_�H�����藦"
            arrHead(160) = "4��_�v�Z�p_�H���󒍗�"
            arrHead(161) = "4��_�v�Z�p_���H����"
            arrHead(162) = "5��_�v�Z�p_�H�����藦"
            arrHead(163) = "5��_�v�Z�p_�H���󒍗�"
            arrHead(164) = "5��_�v�Z�p_���H����"
            arrHead(165) = "6��_�v�Z�p_�H�����藦"
            arrHead(166) = "6��_�v�Z�p_�H���󒍗�"
            arrHead(167) = "6��_�v�Z�p_���H����"
            arrHead(168) = "7��_�v�Z�p_�H�����藦"
            arrHead(169) = "7��_�v�Z�p_�H���󒍗�"
            arrHead(170) = "7��_�v�Z�p_���H����"
            arrHead(171) = "8��_�v�Z�p_�H�����藦"
            arrHead(172) = "8��_�v�Z�p_�H���󒍗�"
            arrHead(173) = "8��_�v�Z�p_���H����"
            arrHead(174) = "9��_�v�Z�p_�H�����藦"
            arrHead(175) = "9��_�v�Z�p_�H���󒍗�"
            arrHead(176) = "9��_�v�Z�p_���H����"
            arrHead(177) = "10��_�v�Z�p_�H�����藦"
            arrHead(178) = "10��_�v�Z�p_�H���󒍗�"
            arrHead(179) = "10��_�v�Z�p_���H����"
            arrHead(180) = "11��_�v�Z�p_�H�����藦"
            arrHead(181) = "11��_�v�Z�p_�H���󒍗�"
            arrHead(182) = "11��_�v�Z�p_���H����"
            arrHead(183) = "12��_�v�Z�p_�H�����藦"
            arrHead(184) = "12��_�v�Z�p_�H���󒍗�"
            arrHead(185) = "12��_�v�Z�p_���H����"
            arrHead(186) = "1��_�v�Z�p_�H�����藦"
            arrHead(187) = "1��_�v�Z�p_�H���󒍗�"
            arrHead(188) = "1��_�v�Z�p_���H����"
            arrHead(189) = "2��_�v�Z�p_�H�����藦"
            arrHead(190) = "2��_�v�Z�p_�H���󒍗�"
            arrHead(191) = "2��_�v�Z�p_���H����"
            arrHead(192) = "3��_�v�Z�p_�H�����藦"
            arrHead(193) = "3��_�v�Z�p_�H���󒍗�"
            arrHead(194) = "3��_�v�Z�p_���H����"
            arrHead(195) = "�c�Ƌ敪"
            arrHead(196) = "�o�^����"
            arrHead(197) = "4��_�v�Z�p__���㕽�ϒP��"
            arrHead(198) = "4��_�v�Z�p__�d�����ϒP��"
            arrHead(199) = "5��_�v�Z�p__���㕽�ϒP��"
            arrHead(200) = "5��_�v�Z�p__�d�����ϒP��"
            arrHead(201) = "6��_�v�Z�p__���㕽�ϒP��"
            arrHead(202) = "6��_�v�Z�p__�d�����ϒP��"
            arrHead(203) = "7��_�v�Z�p__���㕽�ϒP��"
            arrHead(204) = "7��_�v�Z�p__�d�����ϒP��"
            arrHead(205) = "8��_�v�Z�p__���㕽�ϒP��"
            arrHead(206) = "8��_�v�Z�p__�d�����ϒP��"
            arrHead(207) = "9��_�v�Z�p__���㕽�ϒP��"
            arrHead(208) = "9��_�v�Z�p__�d�����ϒP��"
            arrHead(209) = "10��_�v�Z�p__���㕽�ϒP��"
            arrHead(210) = "10��_�v�Z�p__�d�����ϒP��"
            arrHead(211) = "11��_�v�Z�p__���㕽�ϒP��"
            arrHead(212) = "11��_�v�Z�p__�d�����ϒP��"
            arrHead(213) = "12��_�v�Z�p__���㕽�ϒP��"
            arrHead(214) = "12��_�v�Z�p__�d�����ϒP��"
            arrHead(215) = "1��_�v�Z�p__���㕽�ϒP��"
            arrHead(216) = "1��_�v�Z�p__�d�����ϒP��"
            arrHead(217) = "2��_�v�Z�p__���㕽�ϒP��"
            arrHead(218) = "2��_�v�Z�p__�d�����ϒP��"
            arrHead(219) = "3��_�v�Z�p__���㕽�ϒP��"
            arrHead(220) = "3��_�v�Z�p__�d�����ϒP��"
            arrHead(221) = "���ʃR�[�h�\����"
            arrHead(222) = "�c�Ƌ敪�\����"
            arrHead(223) = "���ׁA���v�A���v�敪"
            arrHead(224) = "���i�R�[�h"
            arrHead(225) = "���i�̕\����"
            arrHead(226) = "�����J�E���g�L��"
            arrHead(227) = "�v��m��FLG"
            arrHead(228) = "�v��l�s��FLG"
            arrHead(229) = "�O�N_�d�����ϒP��"
            arrHead(230) = "�v��_SS_SDS���͗L��"
            arrHead(231) = "����_SS_SDS���͗L��"
            arrHead(232) = "�v��_SDS�ǉ����z�L��"
            arrHead(233) = "����_SDS�ǉ����z�L��"

        End If
        Return arrHead(index)
    End Function
    ''' <summary>
    ''' SQL��index
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum selectIndex
        ''' <summary>
        ''' �����X��
        ''' </summary>
        ''' <remarks></remarks>
        kameiten_mei = 0
        ''' <summary>
        ''' �����X�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        kameiten_cd = 1
        ''' <summary>
        ''' �c�Ƌ敪
        ''' </summary>
        ''' <remarks></remarks>
        meisyou = 2
        ''' <summary>
        ''' �c�ƒS���Җ�
        ''' </summary>
        ''' <remarks></remarks>
        eigyou_tantousya_mei = 3
        ''' <summary>
        ''' �N�ԓ���
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_nenkan_tousuu = 4

        ''' <summary>
        ''' �v��p_�N�ԓ���
        ''' </summary>
        ''' <remarks></remarks>
        keikakuyou_nenkan_tousuu = 5

        ''' <summary>
        ''' �Ƒ�
        ''' </summary>
        ''' <remarks></remarks>
        gyoutai = 6
        ''' <summary>
        ''' ����䗦
        ''' </summary>
        ''' <remarks></remarks>
        uri_hiritu = 7

        ''' <summary>
        ''' SDS�J�n�N��
        ''' </summary>
        ''' <remarks></remarks>
        sds_kaisi_nengetu = 8

        ''' <summary>
        ''' �H�����藦
        ''' </summary>
        ''' <remarks></remarks>
        koj_hantei_ritu = 9
        ''' <summary>
        ''' �H���󒍗�
        ''' </summary>
        ''' <remarks></remarks>
        koj_jyuchuu_ritu = 10
        ''' <summary>
        ''' ���H����
        ''' </summary>
        ''' <remarks></remarks>
        tyoku_koj_ritu = 11
        ''' <summary>
        ''' ���̎��
        ''' </summary>
        ''' <remarks></remarks>
        meisyou2 = 12
        ''' <summary>
        ''' ���i��
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_mei = 13
        ''' <summary>
        ''' �O�N_���ϒP��
        ''' </summary>
        ''' <remarks></remarks>
        zennen_heikin_tanka = 14
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        gatu_jisseki_kensuu4 = 15
        gatu_jisseki_kingaku4 = 16
        gatu_jisseki_arari4 = 17
        gatu_jisseki_kensuu5 = 18
        gatu_jisseki_kingaku5 = 19
        gatu_jisseki_arari5 = 20
        gatu_jisseki_kensuu6 = 21
        gatu_jisseki_kingaku6 = 22
        gatu_jisseki_arari6 = 23
        gatu_jisseki_kensuu7 = 24
        gatu_jisseki_kingaku7 = 25
        gatu_jisseki_arari7 = 26
        gatu_jisseki_kensuu8 = 27
        gatu_jisseki_kingaku8 = 28
        gatu_jisseki_arari8 = 29
        gatu_jisseki_kensuu9 = 30
        gatu_jisseki_kingaku9 = 31
        gatu_jisseki_arari9 = 32
        gatu_jisseki_kensuu10 = 33
        gatu_jisseki_kingaku10 = 34
        gatu_jisseki_arari10 = 35
        gatu_jisseki_kensuu11 = 36
        gatu_jisseki_kingaku11 = 37
        gatu_jisseki_arari11 = 38
        gatu_jisseki_kensuu12 = 39
        gatu_jisseki_kingaku12 = 40
        gatu_jisseki_arari12 = 41
        gatu_jisseki_kensuu1 = 42
        gatu_jisseki_kingaku1 = 43
        gatu_jisseki_arari1 = 44
        gatu_jisseki_kensuu2 = 45
        gatu_jisseki_kingaku2 = 46
        gatu_jisseki_arari2 = 47
        gatu_jisseki_kensuu3 = 48
        gatu_jisseki_kingaku3 = 49
        gatu_jisseki_arari3 = 50

        ''' <summary>
        ''' �v��
        ''' </summary>
        ''' <remarks></remarks>
        gatu_keikaku_kensuu4 = 51
        gatu_keikaku_kingaku4 = 52
        gatu_keikaku_arari4 = 53
        gatu_keikaku_kensuu5 = 54
        gatu_keikaku_kingaku5 = 55
        gatu_keikaku_arari5 = 56
        gatu_keikaku_kensuu6 = 57
        gatu_keikaku_kingaku6 = 58
        gatu_keikaku_arari6 = 59
        gatu_keikaku_kensuu7 = 60
        gatu_keikaku_kingaku7 = 61
        gatu_keikaku_arari7 = 62
        gatu_keikaku_kensuu8 = 63
        gatu_keikaku_kingaku8 = 64
        gatu_keikaku_arari8 = 65
        gatu_keikaku_kensuu9 = 66
        gatu_keikaku_kingaku9 = 67
        gatu_keikaku_arari9 = 68
        gatu_keikaku_kensuu10 = 69
        gatu_keikaku_kingaku10 = 70
        gatu_keikaku_arari10 = 71
        gatu_keikaku_kensuu11 = 72
        gatu_keikaku_kingaku11 = 73
        gatu_keikaku_arari11 = 74
        gatu_keikaku_kensuu12 = 75
        gatu_keikaku_kingaku12 = 76
        gatu_keikaku_arari12 = 77
        gatu_keikaku_kensuu1 = 78
        gatu_keikaku_kingaku1 = 79
        gatu_keikaku_arari1 = 80
        gatu_keikaku_kensuu2 = 81
        gatu_keikaku_kingaku2 = 82
        gatu_keikaku_arari2 = 83
        gatu_keikaku_kensuu3 = 84
        gatu_keikaku_kingaku3 = 85
        gatu_keikaku_arari3 = 86

        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        gatu_mikomi_kensuu4 = 87
        gatu_mikomi_kingaku4 = 88
        gatu_mikomi_arari4 = 89
        gatu_mikomi_kensuu5 = 90
        gatu_mikomi_kingaku5 = 91
        gatu_mikomi_arari5 = 92
        gatu_mikomi_kensuu6 = 93
        gatu_mikomi_kingaku6 = 94
        gatu_mikomi_arari6 = 95
        gatu_mikomi_kensuu7 = 96
        gatu_mikomi_kingaku7 = 97
        gatu_mikomi_arari7 = 98
        gatu_mikomi_kensuu8 = 99
        gatu_mikomi_kingaku8 = 100
        gatu_mikomi_arari8 = 101
        gatu_mikomi_kensuu9 = 102
        gatu_mikomi_kingaku9 = 103
        gatu_mikomi_arari9 = 104
        gatu_mikomi_kensuu10 = 105
        gatu_mikomi_kingaku10 = 106
        gatu_mikomi_arari10 = 107
        gatu_mikomi_kensuu11 = 108
        gatu_mikomi_kingaku11 = 109
        gatu_mikomi_arari11 = 110
        gatu_mikomi_kensuu12 = 111
        gatu_mikomi_kingaku12 = 112
        gatu_mikomi_arari12 = 113
        gatu_mikomi_kensuu1 = 114
        gatu_mikomi_kingaku1 = 115
        gatu_mikomi_arari1 = 116
        gatu_mikomi_kensuu2 = 117
        gatu_mikomi_kingaku2 = 118
        gatu_mikomi_arari2 = 119
        gatu_mikomi_kensuu3 = 120
        gatu_mikomi_kingaku3 = 121
        gatu_mikomi_arari3 = 122

        ''' <summary>
        ''' �O�N����
        ''' </summary>
        ''' <remarks></remarks>
        z_gatu_jisseki_kensuu4 = 123
        z_gatu_jisseki_kingaku4 = 124
        z_gatu_jisseki_arari4 = 125
        z_gatu_jisseki_kensuu5 = 126
        z_gatu_jisseki_kingaku5 = 127
        z_gatu_jisseki_arari5 = 128
        z_gatu_jisseki_kensuu6 = 129
        z_gatu_jisseki_kingaku6 = 130
        z_gatu_jisseki_arari6 = 131
        z_gatu_jisseki_kensuu7 = 132
        z_gatu_jisseki_kingaku7 = 133
        z_gatu_jisseki_arari7 = 134
        z_gatu_jisseki_kensuu8 = 135
        z_gatu_jisseki_kingaku8 = 136
        z_gatu_jisseki_arari8 = 137
        z_gatu_jisseki_kensuu9 = 138
        z_gatu_jisseki_kingaku9 = 139
        z_gatu_jisseki_arari9 = 140
        z_gatu_jisseki_kensuu10 = 141
        z_gatu_jisseki_kingaku10 = 142
        z_gatu_jisseki_arari10 = 143
        z_gatu_jisseki_kensuu11 = 144
        z_gatu_jisseki_kingaku11 = 145
        z_gatu_jisseki_arari11 = 146
        z_gatu_jisseki_kensuu12 = 147
        z_gatu_jisseki_kingaku12 = 148
        z_gatu_jisseki_arari12 = 149
        z_gatu_jisseki_kensuu1 = 150
        z_gatu_jisseki_kingaku1 = 151
        z_gatu_jisseki_arari1 = 152
        z_gatu_jisseki_kensuu2 = 153
        z_gatu_jisseki_kingaku2 = 154
        z_gatu_jisseki_arari2 = 155
        z_gatu_jisseki_kensuu3 = 156
        z_gatu_jisseki_kingaku3 = 157
        z_gatu_jisseki_arari3 = 158

        ''' <summary>
        ''' �v�Z�p
        ''' </summary>
        ''' <remarks></remarks>
        gatu_keisanyou_koj_hantei_ritu4 = 159
        gatu_keisanyou_koj_jyuchuu_ritu4 = 160
        gatu_keisanyou_tyoku_koj_ritu4 = 161
        gatu_keisanyou_koj_hantei_ritu5 = 162
        gatu_keisanyou_koj_jyuchuu_ritu5 = 163
        gatu_keisanyou_tyoku_koj_ritu5 = 164
        gatu_keisanyou_koj_hantei_ritu6 = 165
        gatu_keisanyou_koj_jyuchuu_ritu6 = 166
        gatu_keisanyou_tyoku_koj_ritu6 = 167
        gatu_keisanyou_koj_hantei_ritu7 = 168
        gatu_keisanyou_koj_jyuchuu_ritu7 = 169
        gatu_keisanyou_tyoku_koj_ritu7 = 170
        gatu_keisanyou_koj_hantei_ritu8 = 171
        gatu_keisanyou_koj_jyuchuu_ritu8 = 172
        gatu_keisanyou_tyoku_koj_ritu8 = 173
        gatu_keisanyou_koj_hantei_ritu9 = 174
        gatu_keisanyou_koj_jyuchuu_ritu9 = 175
        gatu_keisanyou_tyoku_koj_ritu9 = 176
        gatu_keisanyou_koj_hantei_ritu10 = 177
        gatu_keisanyou_koj_jyuchuu_ritu10 = 178
        gatu_keisanyou_tyoku_koj_ritu10 = 179
        gatu_keisanyou_koj_hantei_ritu11 = 180
        gatu_keisanyou_koj_jyuchuu_ritu11 = 181
        gatu_keisanyou_tyoku_koj_ritu11 = 182
        gatu_keisanyou_koj_hantei_ritu12 = 183
        gatu_keisanyou_koj_jyuchuu_ritu12 = 184
        gatu_keisanyou_tyoku_koj_ritu12 = 185
        gatu_keisanyou_koj_hantei_ritu1 = 186
        gatu_keisanyou_koj_jyuchuu_ritu1 = 187
        gatu_keisanyou_tyoku_koj_ritu1 = 188
        gatu_keisanyou_koj_hantei_ritu2 = 189
        gatu_keisanyou_koj_jyuchuu_ritu2 = 190
        gatu_keisanyou_tyoku_koj_ritu2 = 191
        gatu_keisanyou_koj_hantei_ritu3 = 192
        gatu_keisanyou_koj_jyuchuu_ritu3 = 193
        gatu_keisanyou_tyoku_koj_ritu3 = 194

        ''' <summary>
        ''' �c�Ƌ敪
        ''' </summary>
        ''' <remarks></remarks>
        eigyou_kbn = 195
        ''' <summary>
        ''' �o�^����
        ''' </summary>
        ''' <remarks></remarks>
        add_datetime = 196

        ''' <summary>
        ''' �v�Z�p__���㕽�ϒP���A�v�Z�p__�d�����ϒP��
        ''' </summary>
        ''' <remarks></remarks>
        gatu_keisanyou_uri_heikin_tanka4 = 197
        gatu_keisanyou_siire_heikin_tanka4 = 198
        gatu_keisanyou_uri_heikin_tanka5 = 199
        gatu_keisanyou_siire_heikin_tanka5 = 200
        gatu_keisanyou_uri_heikin_tanka6 = 201
        gatu_keisanyou_siire_heikin_tanka6 = 202
        gatu_keisanyou_uri_heikin_tanka7 = 203
        gatu_keisanyou_siire_heikin_tanka7 = 204
        gatu_keisanyou_uri_heikin_tanka8 = 205
        gatu_keisanyou_siire_heikin_tanka8 = 206
        gatu_keisanyou_uri_heikin_tanka9 = 207
        gatu_keisanyou_siire_heikin_tanka9 = 208
        gatu_keisanyou_uri_heikin_tanka10 = 209
        gatu_keisanyou_siire_heikin_tanka10 = 210
        gatu_keisanyou_uri_heikin_tanka11 = 211
        gatu_keisanyou_siire_heikin_tanka11 = 212
        gatu_keisanyou_uri_heikin_tanka12 = 213
        gatu_keisanyou_siire_heikin_tanka12 = 214
        gatu_keisanyou_uri_heikin_tanka1 = 215
        gatu_keisanyou_siire_heikin_tanka1 = 216
        gatu_keisanyou_uri_heikin_tanka2 = 217
        gatu_keisanyou_siire_heikin_tanka2 = 218
        gatu_keisanyou_uri_heikin_tanka3 = 219
        gatu_keisanyou_siire_heikin_tanka3 = 220

        ''' <summary>
        ''' ���ʃR�[�h�\����
        ''' </summary>
        ''' <remarks></remarks>
        hyouji_jyun2 = 221
        ''' <summary>
        ''' �c�Ƌ敪�\����
        ''' </summary>
        ''' <remarks></remarks>
        hyouji_jyun = 222
        ''' <summary>
        ''' ���ׁA���v�A���v�敪
        ''' </summary>
        ''' <remarks></remarks>
        data_type = 223

        ''' <summary>
        ''' ���i�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_cd = 224
        ''' <summary>
        '''  ���i�̕\����
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_jyun = 225
        ''' <summary>
        ''' �����J�E���g�L��
        ''' </summary>
        ''' <remarks></remarks>
        kensuu_count_umu = 226
        ''' <summary>
        '''  �v��m��FLG
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_kakutei_flg = 227
        ''' <summary>
        ''' �v��l�s��FLG
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_huhen_flg = 228
        ''' <summary>
        '''   �O�N_�d�����ϒP��
        ''' </summary>
        ''' <remarks></remarks>
        zennen_siire_heikin_tanka = 229
        ''' <summary>
        '''   �v��_SS_SDS���͗L��
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_ss_sds_umu = 230
        ''' <summary>
        '''   ����_SS_SDS���͗L��
        ''' </summary>
        ''' <remarks></remarks>
        mikomi_ss_sds_umu = 231
        ''' <summary>
        '''   �v��_SDS�ǉ����z�L��
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_sds_tuika_kin_umu = 232
        ''' <summary>
        '''  ����_SDS�ǉ����z�L��
        ''' </summary>
        ''' <remarks></remarks>
        mikomi_sds_tuika_kin_umu = 233
        max = 233
    End Enum
    Public Enum taisyouKikan
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        Kongetu = 1
        ''' <summary>
        ''' ����3����
        ''' </summary>
        ''' <remarks></remarks>
        Sangetu = 2
        ''' <summary>
        ''' ��s4����
        ''' </summary>
        ''' <remarks></remarks>
        Yogetu = 3
        ''' <summary>
        ''' �N��
        ''' </summary>
        ''' <remarks></remarks>
        Nenkan = 4
    End Enum
    ''' <summary>
    ''' �����������擾
    ''' </summary>
    ''' <returns>��������</returns>
    ''' <remarks>2012/11/28 ��</remarks>
    Public Function GetKensakuString() As String
        Dim strBuffer As New System.Text.StringBuilder
        With strBuffer
            .AppendLine(strNendo)
            .AppendLine(strShiten)
            .AppendLine(strUser)
            .AppendLine(strKameiten)
            .AppendLine("")
            .AppendLine(strEigyou)
            .AppendLine(CStr(CInt(strTaisyou)))
            '�c�Ƌ敪
            If lstEigyouKBN.Eigyou Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstEigyouKBN.Tokuhan Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstEigyouKBN.Sinki Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstEigyouKBN.FC Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            '�i���ݑI��
            If lstSiborikomi.KeikakuTi Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstSiborikomi.SinkiTouroku Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstSiborikomi.Bunjyou Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstSiborikomi.Tyumon Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstSiborikomi.NenkanTouSuu Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            '�\�����I��
            If lstHyoujiSentaku.Keikaku Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstHyoujiSentaku.Mikomi Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstHyoujiSentaku.Jisseki Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstHyoujiSentaku.Tassei Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            If lstHyoujiSentaku.Sintyoku Then
                .AppendLine(CStr(True))
            Else
                .AppendLine(CStr(False))
            End If
            .AppendLine(strKensuu)
        End With
        Dim strTmp As String = Replace(strBuffer.ToString, vbCrLf, ",")
        Return Left(strTmp, Len(strTmp) - 1)
    End Function


    ''' <summary>
    ''' �䗦
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum HirituItem As Integer
        KoujiHiritu = 1 '�H���䗦
    End Enum

    ''' <summary>
    ''' �O�N����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ZennenItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum

    ''' <summary>
    ''' �v��
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum KeikakuItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum

    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MikomiItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum

    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum JissekiItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum

    ''' <summary>
    ''' �B����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TasseirituItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum

    ''' <summary>
    ''' �i����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SintyokurituItem As Integer
        Kensuu = 1 '����
        Kingaku = 2 '���z
        Arari = 3 '�e��
    End Enum



#Region "�N����"
    ''' <summary>
    ''' �N����
    ''' </summary>
    ''' <remarks></remarks>
    Private strNendo As String
    ''' <summary>
    ''' �N�x
    ''' </summary>
    ''' <value></value>
    ''' <returns>�N�x</returns>
    ''' <remarks></remarks>
    Public Property Nendo() As String
        Get
            Return strNendo
        End Get
        Set(ByVal value As String)
            strNendo = value
        End Set
    End Property
#End Region
#Region "�x�X"
    ''' <summary>
    ''' �x�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strShiten As String
    ''' <summary>
    ''' �x�X
    ''' </summary>
    ''' <value></value>
    ''' <returns>�x�X</returns>
    ''' <remarks></remarks>
    Public Property Shiten() As String
        Get
            Return strShiten
        End Get
        Set(ByVal value As String)
            strShiten = value
        End Set
    End Property
#End Region
#Region "�c�ƃ}��"
    ''' <summary>
    ''' �c�ƃ}��
    ''' </summary>
    ''' <remarks></remarks>
    Private strUser As String
    ''' <summary>
    ''' �c�ƃ}��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�c�ƃ}��</returns>
    ''' <remarks></remarks>
    Public Property User() As String
        Get
            Return strUser
        End Get
        Set(ByVal value As String)
            strUser = value
        End Set
    End Property
#End Region
#Region "�����X"
    ''' <summary>
    ''' �����X
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiten As String
    ''' <summary>
    ''' �����X
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X</returns>
    ''' <remarks></remarks>
    Public Property Kameiten() As String
        Get
            Return strKameiten
        End Get
        Set(ByVal value As String)
            strKameiten = value
        End Set
    End Property
#End Region
#Region "�n��"
    ''' <summary>
    ''' �n��
    ''' </summary>
    ''' <remarks></remarks>
    Private strShitenMei As String
    ''' <summary>
    ''' �n��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�n��</returns>
    ''' <remarks></remarks>
    Public Property ShitenMei() As String
        Get
            Return strShitenMei
        End Get
        Set(ByVal value As String)
            strShitenMei = value
        End Set
    End Property
#End Region
#Region "�c�Ə�"
    ''' <summary>
    ''' �c�Ə�
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyou As String
    ''' <summary>
    ''' �c�Ə�
    ''' </summary>
    ''' <value></value>
    ''' <returns>�c�Ə�</returns>
    ''' <remarks></remarks>
    Public Property Eigyou() As String
        Get
            Return strEigyou
        End Get
        Set(ByVal value As String)
            strEigyou = value
        End Set
    End Property
#End Region

#Region "����"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyozoku As String
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns>����</returns>
    ''' <remarks></remarks>
    Public Property Syozoku() As String
        Get
            Return strSyozoku
        End Get
        Set(ByVal value As String)
            strSyozoku = value
        End Set
    End Property
#End Region
#Region "�s���{��"
    ''' <summary>
    ''' �s���{��
    ''' </summary>
    ''' <remarks></remarks>
    Private strTodoufuken As String
    ''' <summary>
    ''' �s���{��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�s���{��</returns>
    ''' <remarks></remarks>
    Public Property Todoufuken() As String
        Get
            Return strTodoufuken
        End Get
        Set(ByVal value As String)
            strTodoufuken = value
        End Set
    End Property
#End Region
#Region "����@�l"
    ''' <summary>
    ''' ����@�l
    ''' </summary>
    ''' <remarks></remarks>
    Private strTouituHoujin As String
    ''' <summary>
    ''' ����@�l
    ''' </summary>
    ''' <value></value>
    ''' <returns>����@�l</returns>
    ''' <remarks></remarks>
    Public Property TouituHoujin() As String
        Get
            Return strTouituHoujin
        End Get
        Set(ByVal value As String)
            strTouituHoujin = value
        End Set
    End Property
#End Region
#Region "�@�l"
    ''' <summary>
    ''' �@�l
    ''' </summary>
    ''' <remarks></remarks>
    Private strHoujin As String
    ''' <summary>
    ''' �@�l
    ''' </summary>
    ''' <value></value>
    ''' <returns>�@�l</returns>
    ''' <remarks></remarks>
    Public Property Houjin() As String
        Get
            Return strHoujin
        End Get
        Set(ByVal value As String)
            strHoujin = value
        End Set
    End Property
#End Region
#Region "����1"
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei1 As String
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <value></value>
    ''' <returns>����1</returns>
    ''' <remarks></remarks>
    Public Property Zokusei1() As String
        Get
            Return strZokusei1
        End Get
        Set(ByVal value As String)
            strZokusei1 = value
        End Set
    End Property
#End Region
#Region "����2"
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei2 As String
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <value></value>
    ''' <returns>����2</returns>
    ''' <remarks></remarks>
    Public Property Zokusei2() As String
        Get
            Return strZokusei2
        End Get
        Set(ByVal value As String)
            strZokusei2 = value
        End Set
    End Property
#End Region
#Region "����3"
    ''' <summary>
    ''' ����3
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei3 As String
    ''' <summary>
    ''' ����3
    ''' </summary>
    ''' <value></value>
    ''' <returns>����3</returns>
    ''' <remarks></remarks>
    Public Property Zokusei3() As String
        Get
            Return strZokusei3
        End Get
        Set(ByVal value As String)
            strZokusei3 = value
        End Set
    End Property
#End Region
#Region "����4"
    ''' <summary>
    ''' ����4
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei4 As String
    ''' <summary>
    ''' ����4
    ''' </summary>
    ''' <value></value>
    ''' <returns>����4</returns>
    ''' <remarks></remarks>
    Public Property Zokusei4() As String
        Get
            Return strZokusei4
        End Get
        Set(ByVal value As String)
            strZokusei4 = value
        End Set
    End Property
#End Region
#Region "����5"
    ''' <summary>
    ''' ����5
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei5 As String
    ''' <summary>
    ''' ����5
    ''' </summary>
    ''' <value></value>
    ''' <returns>����5</returns>
    ''' <remarks></remarks>
    Public Property Zokusei5() As String
        Get
            Return strZokusei5
        End Get
        Set(ByVal value As String)
            strZokusei5 = value
        End Set
    End Property
#End Region
#Region "����6"
    ''' <summary>
    ''' ����6
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei6 As String
    ''' <summary>
    ''' ����6
    ''' </summary>
    ''' <value></value>
    ''' <returns>����6</returns>
    ''' <remarks></remarks>
    Public Property Zokusei6() As String
        Get
            Return strZokusei6
        End Get
        Set(ByVal value As String)
            strZokusei6 = value
        End Set
    End Property
#End Region

#Region "�Ώۊ���"
    ''' <summary>
    ''' �Ώۊ���
    ''' </summary>
    ''' <remarks></remarks>
    Private strTaisyou As taisyouKikan = taisyouKikan.Nenkan
    ''' <summary>
    ''' �Ώۊ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�Ώۊ���</returns>
    ''' <remarks></remarks>
    Public Property Taisyou() As taisyouKikan
        Get
            Return strTaisyou
        End Get
        Set(ByVal value As taisyouKikan)
            strTaisyou = value
        End Set
    End Property
#End Region
#Region "�c�Ƌ敪"
    ''' <summary>
    ''' �c�Ƌ敪
    ''' </summary>
    ''' <remarks></remarks>
    Private lstEigyouKBN As EigyouJyouken
    ''' <summary>
    ''' �c�Ƌ敪
    ''' </summary>
    ''' <value></value>
    ''' <returns>�c�Ƌ敪</returns>
    ''' <remarks></remarks>
    Public Property EigyouKBN() As EigyouJyouken
        Get
            Return lstEigyouKBN
        End Get
        Set(ByVal value As EigyouJyouken)
            lstEigyouKBN = value
        End Set
    End Property
#End Region
#Region "�i���ݑI��"
    ''' <summary>
    ''' �i���ݑI��
    ''' </summary>
    ''' <remarks></remarks>
    Private lstSiborikomi As SiborikomiJyouken
    ''' <summary>
    ''' �i���ݑI��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�i���ݑI��</returns>
    ''' <remarks></remarks>
    Public Property Siborikomi() As SiborikomiJyouken
        Get
            Return lstSiborikomi
        End Get
        Set(ByVal value As SiborikomiJyouken)
            lstSiborikomi = value
        End Set
    End Property
#End Region
#Region "�\�����I��"
    ''' <summary>
    ''' �\�����I��
    ''' </summary>
    ''' <remarks></remarks>
    Private lstHyoujiSentaku As HyoujiSentakuJyouken
    ''' <summary>
    ''' �\�����I��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�\�����I��</returns>
    ''' <remarks></remarks>
    Public Property HyoujiSentaku() As HyoujiSentakuJyouken
        Get
            Return lstHyoujiSentaku
        End Get
        Set(ByVal value As HyoujiSentakuJyouken)
            lstHyoujiSentaku = value
        End Set
    End Property
#End Region

#Region "�N�ԓ����͈�"
    ''' <summary>
    ''' �N�ԓ����͈�
    ''' </summary>
    ''' <remarks></remarks>
    Private lstNenkanTousuuHani As NenkanTousuuHaniJyouken
    ''' <summary>
    ''' �N�ԓ����͈�
    ''' </summary>
    ''' <value></value>
    ''' <returns>�N�ԓ����͈�</returns>
    ''' <remarks></remarks>
    Public Property NenkanTousuuHani() As NenkanTousuuHaniJyouken
        Get
            Return lstNenkanTousuuHani
        End Get
        Set(ByVal value As NenkanTousuuHaniJyouken)
            lstNenkanTousuuHani = value
        End Set
    End Property
#End Region

#Region "�\������"
    ''' <summary>
    ''' �\������
    ''' </summary>
    ''' <remarks></remarks>
    Private strKensuu As String
    ''' <summary>
    ''' �\������
    ''' </summary>
    ''' <value></value>
    ''' <returns>�\������</returns>
    ''' <remarks></remarks>
    Public Property Kensuu() As String
        Get
            Return strKensuu
        End Get
        Set(ByVal value As String)
            strKensuu = value
        End Set
    End Property
#End Region
End Class
''' <summary>
''' �\�����I��
''' </summary>
''' <remarks>2012/11/28 ��</remarks>
<System.Serializable()> Public Class HyoujiSentakuJyouken
#Region " �v��"
    ''' <summary>
    '''  �v��
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikaku As Boolean
    ''' <summary>
    '''  �v��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v��</returns>
    ''' <remarks></remarks>
    Public Property Keikaku() As Boolean
        Get
            Return blnKeikaku
        End Get
        Set(ByVal value As Boolean)
            blnKeikaku = value
        End Set
    End Property
#End Region
#Region " ����"
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnMikomi As Boolean
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����</returns>
    ''' <remarks></remarks>
    Public Property Mikomi() As Boolean
        Get
            Return blnMikomi
        End Get
        Set(ByVal value As Boolean)
            blnMikomi = value
        End Set
    End Property
#End Region
#Region " ����"
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnJisseki As Boolean
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����</returns>
    ''' <remarks></remarks>
    Public Property Jisseki() As Boolean
        Get
            Return blnJisseki
        End Get
        Set(ByVal value As Boolean)
            blnJisseki = value
        End Set
    End Property
#End Region
#Region "�B����"
    ''' <summary>
    '''  �B����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTassei As Boolean
    ''' <summary>
    '''  �B����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �B����</returns>
    ''' <remarks></remarks>
    Public Property Tassei() As Boolean
        Get
            Return blnTassei
        End Get
        Set(ByVal value As Boolean)
            blnTassei = value
        End Set
    End Property
#End Region
#Region "�i����"
    ''' <summary>
    '''  �i����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSintyoku As Boolean
    ''' <summary>
    '''  �i����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i����</returns>
    ''' <remarks></remarks>
    Public Property Sintyoku() As Boolean
        Get
            Return blnSintyoku
        End Get
        Set(ByVal value As Boolean)
            blnSintyoku = value
        End Set
    End Property
#End Region

#Region "�O�N����"
    ''' <summary>
    '''  �O�N����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnZennenDougetu As Boolean
    ''' <summary>
    '''  �O�N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �O�N����</returns>
    ''' <remarks></remarks>
    Public Property ZennenDougetu() As Boolean
        Get
            Return blnZennenDougetu
        End Get
        Set(ByVal value As Boolean)
            blnZennenDougetu = value
        End Set
    End Property
#End Region

End Class
''' <summary>
''' �i���ݑI��
''' </summary>
''' <remarks>2012/11/28 ��</remarks>
<System.Serializable()> Public Class SiborikomiJyouken
#Region " �v��l"
    ''' <summary>
    '''  �v��l
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikakuTi As Boolean
    ''' <summary>
    '''  �v��l
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v��l</returns>
    ''' <remarks></remarks>
    Public Property KeikakuTi() As Boolean
        Get
            Return blnKeikakuTi
        End Get
        Set(ByVal value As Boolean)
            blnKeikakuTi = value
        End Set
    End Property
#End Region
#Region " �V�K�o�^���Ǝ�"
    ''' <summary>
    '''  �v��l
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSinkiTouroku As Boolean
    ''' <summary>
    '''  �V�K�o�^���Ǝ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �V�K�o�^���Ǝ�</returns>
    ''' <remarks></remarks>
    Public Property SinkiTouroku() As Boolean
        Get
            Return blnSinkiTouroku
        End Get
        Set(ByVal value As Boolean)
            blnSinkiTouroku = value
        End Set
    End Property
#End Region
#Region " ����50��"
    ''' <summary>
    '''  ����50��
    ''' </summary>
    ''' <remarks></remarks>
    Private blnBunjyou As Boolean
    ''' <summary>
    '''  ����50��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����50��</returns>
    ''' <remarks></remarks>
    Public Property Bunjyou() As Boolean
        Get
            Return blnBunjyou
        End Get
        Set(ByVal value As Boolean)
            blnBunjyou = value
        End Set
    End Property
#End Region
#Region " ����50��"
    ''' <summary>
    '''  ����50��
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTyumon As Boolean
    ''' <summary>
    '''  ����50��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����50��</returns>
    ''' <remarks></remarks>
    Public Property Tyumon() As Boolean
        Get
            Return blnTyumon
        End Get
        Set(ByVal value As Boolean)
            blnTyumon = value
        End Set
    End Property
#End Region
#Region "�N�ԓ���"
    ''' <summary>
    '''  �N�ԓ���
    ''' </summary>
    ''' <remarks></remarks>
    Private blnNenkanTouSuu As Boolean
    ''' <summary>
    '''  �N�ԓ���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �N�ԓ���</returns>
    ''' <remarks></remarks>
    Public Property NenkanTouSuu() As Boolean
        Get
            Return blnNenkanTouSuu
        End Get
        Set(ByVal value As Boolean)
            blnNenkanTouSuu = value
        End Set
    End Property
#End Region

End Class

''' <summary>
''' �N�ԓ����͈�
''' </summary>
''' <remarks>2013/10/11 ��</remarks>
<System.Serializable()> Public Class NenkanTousuuHaniJyouken
#Region "�v��p"
    ''' <summary>
    '''  �v��p
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikakuyou As Boolean
    ''' <summary>
    '''  �v��p
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v��p</returns>
    ''' <remarks></remarks>
    Public Property Keikakuyou() As Boolean
        Get
            Return blnKeikakuyou
        End Get
        Set(ByVal value As Boolean)
            blnKeikakuyou = value
        End Set
    End Property
#End Region
#Region "�N�ԓ����lFrom"
    ''' <summary>
    '''  �N�ԓ����lFrom
    ''' </summary>
    ''' <remarks></remarks>
    Private strNenkanTouSuuFrom As String
    ''' <summary>
    '''  �N�ԓ����lFrom
    ''' </summary>
    ''' <value></value>
    ''' <returns> �N�ԓ����lFrom</returns>
    ''' <remarks></remarks>
    Public Property NenkanTouSuuFrom() As String
        Get
            Return strNenkanTouSuuFrom
        End Get
        Set(ByVal value As String)
            strNenkanTouSuuFrom = value
        End Set
    End Property
#End Region
#Region "�N�ԓ����lTo"
    ''' <summary>
    '''  �N�ԓ����lTo
    ''' </summary>
    ''' <remarks></remarks>
    Private strNenkanTouSuuTo As String
    ''' <summary>
    '''  �N�ԓ����lTo
    ''' </summary>
    ''' <value></value>
    ''' <returns> �N�ԓ����lTo</returns>
    ''' <remarks></remarks>
    Public Property NenkanTouSuuTo() As String
        Get
            Return strNenkanTouSuuTo
        End Get
        Set(ByVal value As String)
            strNenkanTouSuuTo = value
        End Set
    End Property
#End Region

End Class

''' <summary>
''' �c�Ƌ敪�N���X
''' </summary>
''' <remarks>2012/11/28 ��</remarks>
<System.Serializable()> Public Class EigyouJyouken
#Region " �c��"
    ''' <summary>
    '''  �c��
    ''' </summary>
    ''' <remarks></remarks>
    Private blnEigyou As Boolean
    ''' <summary>
    '''  �c��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c��</returns>
    ''' <remarks></remarks>
    Public Property Eigyou() As Boolean
        Get
            Return blnEigyou
        End Get
        Set(ByVal value As Boolean)
            blnEigyou = value
        End Set
    End Property
#End Region
#Region " �c��(�V�K)"
    ''' <summary>
    '''  �c��(�V�K)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnEigyouNew As Boolean
    ''' <summary>
    '''  �c��(�V�K)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c��(�V�K)</returns>
    ''' <remarks></remarks>
    Public Property EigyouNew() As Boolean
        Get
            Return blnEigyouNew
        End Get
        Set(ByVal value As Boolean)
            blnEigyouNew = value
        End Set
    End Property
#End Region
#Region " ����"
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTokuhan As Boolean
    ''' <summary>
    '''  ����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����</returns>
    ''' <remarks></remarks>
    Public Property Tokuhan() As Boolean
        Get
            Return blnTokuhan
        End Get
        Set(ByVal value As Boolean)
            blnTokuhan = value
        End Set
    End Property
#End Region
#Region " ����(�V�K)"
    ''' <summary>
    '''  ����(�V�K)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTokuhanNew As Boolean
    ''' <summary>
    '''  ����(�V�K)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����(�V�K)</returns>
    ''' <remarks></remarks>
    Public Property TokuhanNew() As Boolean
        Get
            Return blnTokuhanNew
        End Get
        Set(ByVal value As Boolean)
            blnTokuhanNew = value
        End Set
    End Property
#End Region
#Region "�V�K"
    ''' <summary>
    '''  �V�K
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSinki As Boolean
    ''' <summary>
    '''  �V�K
    ''' </summary>
    ''' <value></value>
    ''' <returns> �V�K</returns>
    ''' <remarks></remarks>
    Public Property Sinki() As Boolean
        Get
            Return blnSinki
        End Get
        Set(ByVal value As Boolean)
            blnSinki = value
        End Set
    End Property
#End Region
#Region "FC"
    ''' <summary>
    '''  FC
    ''' </summary>
    ''' <remarks></remarks>
    Private blnFC As Boolean
    ''' <summary>
    '''  FC
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC</returns>
    ''' <remarks></remarks>
    Public Property FC() As Boolean
        Get
            Return blnFC
        End Get
        Set(ByVal value As Boolean)
            blnFC = value
        End Set
    End Property
#End Region
#Region "FC(�V�K)"
    ''' <summary>
    '''  FC(�V�K)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnFCNew As Boolean
    ''' <summary>
    '''  FC(�V�K)
    ''' </summary>
    ''' <value></value>
    ''' <returns> FC</returns>
    ''' <remarks></remarks>
    Public Property FCNew() As Boolean
        Get
            Return blnFCNew
        End Get
        Set(ByVal value As Boolean)
            blnFCNew = value
        End Set
    End Property
#End Region
End Class
