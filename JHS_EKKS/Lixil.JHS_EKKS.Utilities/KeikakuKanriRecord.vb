''' <summary>
'''  計画管理のクラス
''' </summary>
''' <remarks>2012/11/28 高</remarks>
<System.Serializable()> Public Class KeikakuKanriRecord
    ''' <summary>
    ''' POP画面の区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum popKbn As Integer
        ''' <summary>
        ''' 支店
        ''' </summary>
        ''' <remarks></remarks>
        Shiten = 1
        ''' <summary>
        ''' 営業マン
        ''' </summary>
        ''' <remarks></remarks>
        User = 2
        ''' <summary>
        ''' 加盟店
        ''' </summary>
        ''' <remarks></remarks>
        Kameiten = 3
        ''' <summary>
        ''' 系列
        ''' </summary>
        ''' <remarks></remarks>
        Keiretu = 4
        ''' <summary>
        ''' 営業所
        ''' </summary>
        ''' <remarks></remarks>
        Eigyou = 5
        ''' <summary>
        ''' 営業所
        ''' </summary>
        ''' <remarks></remarks>
        TouituHoujin = 6
        ''' <summary>
        ''' 営業所
        ''' </summary>
        ''' <remarks></remarks>
        Houjin = 7

    End Enum
    ''' <summary>
    ''' 明細SQL区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum selectKbn
        ''' <summary>
        ''' 明細
        ''' </summary>
        ''' <remarks></remarks>
        meisai = 0
        ''' <summary>
        ''' 小計
        ''' </summary>
        ''' <remarks></remarks>
        syoukei = 1
        ''' <summary>
        ''' 全体（FC除外）
        ''' </summary>
        ''' <remarks></remarks>
        goukei = 2
        ''' <summary>
        ''' FC小計
        ''' </summary>
        ''' <remarks></remarks>
        FC = 3
        ''' <summary>
        ''' 全体合計
        ''' </summary>
        ''' <remarks></remarks>
        goukeiFC = 4
    End Enum
    Private arrHead(selectIndex.max) As String
    Public Function GetHeadString(ByVal row As Integer, ByVal index As Integer) As String
        If row = 0 Then
            arrHead(0) = "ビルダー情報"
            arrHead(1) = "ビルダー情報"
            arrHead(2) = "ビルダー情報"
            arrHead(3) = "ビルダー情報"
            arrHead(4) = "ビルダー情報"
            arrHead(5) = "ビルダー情報"
            arrHead(6) = "ビルダー情報"
            arrHead(7) = "前年データ"
            arrHead(8) = "ビルダー情報"
            arrHead(9) = "前年データ"
            arrHead(10) = "前年データ"
            arrHead(11) = "前年データ"
            arrHead(12) = "前年データ"
            arrHead(13) = "前年データ"
            arrHead(14) = "前年データ"
            arrHead(15) = "実績"
            arrHead(16) = "実績"
            arrHead(17) = "実績"
            arrHead(18) = "実績"
            arrHead(19) = "実績"
            arrHead(20) = "実績"
            arrHead(21) = "実績"
            arrHead(22) = "実績"
            arrHead(23) = "実績"
            arrHead(24) = "実績"
            arrHead(25) = "実績"
            arrHead(26) = "実績"
            arrHead(27) = "実績"
            arrHead(28) = "実績"
            arrHead(29) = "実績"
            arrHead(30) = "実績"
            arrHead(31) = "実績"
            arrHead(32) = "実績"
            arrHead(33) = "実績"
            arrHead(34) = "実績"
            arrHead(35) = "実績"
            arrHead(36) = "実績"
            arrHead(37) = "実績"
            arrHead(38) = "実績"
            arrHead(39) = "実績"
            arrHead(40) = "実績"
            arrHead(41) = "実績"
            arrHead(42) = "実績"
            arrHead(43) = "実績"
            arrHead(44) = "実績"
            arrHead(45) = "実績"
            arrHead(46) = "実績"
            arrHead(47) = "実績"
            arrHead(48) = "実績"
            arrHead(49) = "実績"
            arrHead(50) = "実績"
            arrHead(51) = "計画"
            arrHead(52) = "計画"
            arrHead(53) = "計画"
            arrHead(54) = "計画"
            arrHead(55) = "計画"
            arrHead(56) = "計画"
            arrHead(57) = "計画"
            arrHead(58) = "計画"
            arrHead(59) = "計画"
            arrHead(60) = "計画"
            arrHead(61) = "計画"
            arrHead(62) = "計画"
            arrHead(63) = "計画"
            arrHead(64) = "計画"
            arrHead(65) = "計画"
            arrHead(66) = "計画"
            arrHead(67) = "計画"
            arrHead(68) = "計画"
            arrHead(69) = "計画"
            arrHead(70) = "計画"
            arrHead(71) = "計画"
            arrHead(72) = "計画"
            arrHead(73) = "計画"
            arrHead(74) = "計画"
            arrHead(75) = "計画"
            arrHead(76) = "計画"
            arrHead(77) = "計画"
            arrHead(78) = "計画"
            arrHead(79) = "計画"
            arrHead(80) = "計画"
            arrHead(81) = "計画"
            arrHead(82) = "計画"
            arrHead(83) = "計画"
            arrHead(84) = "計画"
            arrHead(85) = "計画"
            arrHead(86) = "計画"
            arrHead(87) = "見込"
            arrHead(88) = "見込"
            arrHead(89) = "見込"
            arrHead(90) = "見込"
            arrHead(91) = "見込"
            arrHead(92) = "見込"
            arrHead(93) = "見込"
            arrHead(94) = "見込"
            arrHead(95) = "見込"
            arrHead(96) = "見込"
            arrHead(97) = "見込"
            arrHead(98) = "見込"
            arrHead(99) = "見込"
            arrHead(100) = "見込"
            arrHead(101) = "見込"
            arrHead(102) = "見込"
            arrHead(103) = "見込"
            arrHead(104) = "見込"
            arrHead(105) = "見込"
            arrHead(106) = "見込"
            arrHead(107) = "見込"
            arrHead(108) = "見込"
            arrHead(109) = "見込"
            arrHead(110) = "見込"
            arrHead(111) = "見込"
            arrHead(112) = "見込"
            arrHead(113) = "見込"
            arrHead(114) = "見込"
            arrHead(115) = "見込"
            arrHead(116) = "見込"
            arrHead(117) = "見込"
            arrHead(118) = "見込"
            arrHead(119) = "見込"
            arrHead(120) = "見込"
            arrHead(121) = "見込"
            arrHead(122) = "見込"
            arrHead(123) = "前年実績"
            arrHead(124) = "前年実績"
            arrHead(125) = "前年実績"
            arrHead(126) = "前年実績"
            arrHead(127) = "前年実績"
            arrHead(128) = "前年実績"
            arrHead(129) = "前年実績"
            arrHead(130) = "前年実績"
            arrHead(131) = "前年実績"
            arrHead(132) = "前年実績"
            arrHead(133) = "前年実績"
            arrHead(134) = "前年実績"
            arrHead(135) = "前年実績"
            arrHead(136) = "前年実績"
            arrHead(137) = "前年実績"
            arrHead(138) = "前年実績"
            arrHead(139) = "前年実績"
            arrHead(140) = "前年実績"
            arrHead(141) = "前年実績"
            arrHead(142) = "前年実績"
            arrHead(143) = "前年実績"
            arrHead(144) = "前年実績"
            arrHead(145) = "前年実績"
            arrHead(146) = "前年実績"
            arrHead(147) = "前年実績"
            arrHead(148) = "前年実績"
            arrHead(149) = "前年実績"
            arrHead(150) = "前年実績"
            arrHead(151) = "前年実績"
            arrHead(152) = "前年実績"
            arrHead(153) = "前年実績"
            arrHead(154) = "前年実績"
            arrHead(155) = "前年実績"
            arrHead(156) = "前年実績"
            arrHead(157) = "前年実績"
            arrHead(158) = "前年実績"
            arrHead(159) = "工事比率（計算用）"
            arrHead(160) = "工事比率（計算用）"
            arrHead(161) = "工事比率（計算用）"
            arrHead(162) = "工事比率（計算用）"
            arrHead(163) = "工事比率（計算用）"
            arrHead(164) = "工事比率（計算用）"
            arrHead(165) = "工事比率（計算用）"
            arrHead(166) = "工事比率（計算用）"
            arrHead(167) = "工事比率（計算用）"
            arrHead(168) = "工事比率（計算用）"
            arrHead(169) = "工事比率（計算用）"
            arrHead(170) = "工事比率（計算用）"
            arrHead(171) = "工事比率（計算用）"
            arrHead(172) = "工事比率（計算用）"
            arrHead(173) = "工事比率（計算用）"
            arrHead(174) = "工事比率（計算用）"
            arrHead(175) = "工事比率（計算用）"
            arrHead(176) = "工事比率（計算用）"
            arrHead(177) = "工事比率（計算用）"
            arrHead(178) = "工事比率（計算用）"
            arrHead(179) = "工事比率（計算用）"
            arrHead(180) = "工事比率（計算用）"
            arrHead(181) = "工事比率（計算用）"
            arrHead(182) = "工事比率（計算用）"
            arrHead(183) = "工事比率（計算用）"
            arrHead(184) = "工事比率（計算用）"
            arrHead(185) = "工事比率（計算用）"
            arrHead(186) = "工事比率（計算用）"
            arrHead(187) = "工事比率（計算用）"
            arrHead(188) = "工事比率（計算用）"
            arrHead(189) = "工事比率（計算用）"
            arrHead(190) = "工事比率（計算用）"
            arrHead(191) = "工事比率（計算用）"
            arrHead(192) = "工事比率（計算用）"
            arrHead(193) = "工事比率（計算用）"
            arrHead(194) = "工事比率（計算用）"
            arrHead(195) = "営業区分"
            arrHead(196) = "登録日時"
            arrHead(197) = "入力_平均単価"
            arrHead(198) = "入力_平均単価"
            arrHead(199) = "入力_平均単価"
            arrHead(200) = "入力_平均単価"
            arrHead(201) = "入力_平均単価"
            arrHead(202) = "入力_平均単価"
            arrHead(203) = "入力_平均単価"
            arrHead(204) = "入力_平均単価"
            arrHead(205) = "入力_平均単価"
            arrHead(206) = "入力_平均単価"
            arrHead(207) = "入力_平均単価"
            arrHead(208) = "入力_平均単価"
            arrHead(209) = "入力_平均単価"
            arrHead(210) = "入力_平均単価"
            arrHead(211) = "入力_平均単価"
            arrHead(212) = "入力_平均単価"
            arrHead(213) = "入力_平均単価"
            arrHead(214) = "入力_平均単価"
            arrHead(215) = "入力_平均単価"
            arrHead(216) = "入力_平均単価"
            arrHead(217) = "入力_平均単価"
            arrHead(218) = "入力_平均単価"
            arrHead(219) = "入力_平均単価"
            arrHead(220) = "入力_平均単価"
            arrHead(221) = "分別コード表示順"
            arrHead(222) = "営業区分表示順"
            arrHead(223) = "明細、小計、合計区分"
            arrHead(224) = "前年データ"
            arrHead(225) = "前年データ"
            arrHead(226) = "商品マスタ"
            arrHead(227) = "商品マスタ"
            arrHead(228) = "加盟店情報ﾏｽﾀ"
            arrHead(229) = "計画"
            arrHead(230) = "商品マスタ"
            arrHead(231) = "商品マスタ"
            arrHead(232) = "商品マスタ"
            arrHead(233) = "商品マスタ"

        Else
            arrHead(0) = "加盟店名"
            arrHead(1) = "加盟店ｺｰﾄﾞ"
            arrHead(2) = "営業区分"
            arrHead(3) = "営業担当者"
            arrHead(4) = "年間棟数"
            arrHead(5) = "計画用_年間棟数"
            arrHead(6) = "業態"
            arrHead(7) = "売上比率"
            arrHead(8) = "SDS開始年月"
            arrHead(9) = "工事判定率"
            arrHead(10) = "工事受注率"
            arrHead(11) = "直工事率"
            arrHead(12) = "商品分類"
            arrHead(13) = "商品名"
            arrHead(14) = "平均単価"
            arrHead(15) = "4月_実績件数"
            arrHead(16) = "4月_実績金額"
            arrHead(17) = "4月_実績粗利"
            arrHead(18) = "5月_実績件数"
            arrHead(19) = "5月_実績金額"
            arrHead(20) = "5月_実績粗利"
            arrHead(21) = "6月_実績件数"
            arrHead(22) = "6月_実績金額"
            arrHead(23) = "6月_実績粗利"
            arrHead(24) = "7月_実績件数"
            arrHead(25) = "7月_実績金額"
            arrHead(26) = "7月_実績粗利"
            arrHead(27) = "8月_実績件数"
            arrHead(28) = "8月_実績金額"
            arrHead(29) = "8月_実績粗利"
            arrHead(30) = "9月_実績件数"
            arrHead(31) = "9月_実績金額"
            arrHead(32) = "9月_実績粗利"
            arrHead(33) = "10月_実績件数"
            arrHead(34) = "10月_実績金額"
            arrHead(35) = "10月_実績粗利"
            arrHead(36) = "11月_実績件数"
            arrHead(37) = "11月_実績金額"
            arrHead(38) = "11月_実績粗利"
            arrHead(39) = "12月_実績件数"
            arrHead(40) = "12月_実績金額"
            arrHead(41) = "12月_実績粗利"
            arrHead(42) = "1月_実績件数"
            arrHead(43) = "1月_実績金額"
            arrHead(44) = "1月_実績粗利"
            arrHead(45) = "2月_実績件数"
            arrHead(46) = "2月_実績金額"
            arrHead(47) = "2月_実績粗利"
            arrHead(48) = "3月_実績件数"
            arrHead(49) = "3月_実績金額"
            arrHead(50) = "3月_実績粗利"
            arrHead(51) = "4月_計画件数"
            arrHead(52) = "4月_計画金額"
            arrHead(53) = "4月_計画粗利"
            arrHead(54) = "5月_計画件数"
            arrHead(55) = "5月_計画金額"
            arrHead(56) = "5月_計画粗利"
            arrHead(57) = "6月_計画件数"
            arrHead(58) = "6月_計画金額"
            arrHead(59) = "6月_計画粗利"
            arrHead(60) = "7月_計画件数"
            arrHead(61) = "7月_計画金額"
            arrHead(62) = "7月_計画粗利"
            arrHead(63) = "8月_計画件数"
            arrHead(64) = "8月_計画金額"
            arrHead(65) = "8月_計画粗利"
            arrHead(66) = "9月_計画件数"
            arrHead(67) = "9月_計画金額"
            arrHead(68) = "9月_計画粗利"
            arrHead(69) = "10月_計画件数"
            arrHead(70) = "10月_計画金額"
            arrHead(71) = "10月_計画粗利"
            arrHead(72) = "11月_計画件数"
            arrHead(73) = "11月_計画金額"
            arrHead(74) = "11月_計画粗利"
            arrHead(75) = "12月_計画件数"
            arrHead(76) = "12月_計画金額"
            arrHead(77) = "12月_計画粗利"
            arrHead(78) = "1月_計画件数"
            arrHead(79) = "1月_計画金額"
            arrHead(80) = "1月_計画粗利"
            arrHead(81) = "2月_計画件数"
            arrHead(82) = "2月_計画金額"
            arrHead(83) = "2月_計画粗利"
            arrHead(84) = "3月_計画件数"
            arrHead(85) = "3月_計画金額"
            arrHead(86) = "3月_計画粗利"
            arrHead(87) = "4月_見込件数"
            arrHead(88) = "4月_見込金額"
            arrHead(89) = "4月_見込粗利"
            arrHead(90) = "5月_見込件数"
            arrHead(91) = "5月_見込金額"
            arrHead(92) = "5月_見込粗利"
            arrHead(93) = "6月_見込件数"
            arrHead(94) = "6月_見込金額"
            arrHead(95) = "6月_見込粗利"
            arrHead(96) = "7月_見込件数"
            arrHead(97) = "7月_見込金額"
            arrHead(98) = "7月_見込粗利"
            arrHead(99) = "8月_見込件数"
            arrHead(100) = "8月_見込金額"
            arrHead(101) = "8月_見込粗利"
            arrHead(102) = "9月_見込件数"
            arrHead(103) = "9月_見込金額"
            arrHead(104) = "9月_見込粗利"
            arrHead(105) = "10月_見込件数"
            arrHead(106) = "10月_見込金額"
            arrHead(107) = "10月_見込粗利"
            arrHead(108) = "11月_見込件数"
            arrHead(109) = "11月_見込金額"
            arrHead(110) = "11月_見込粗利"
            arrHead(111) = "12月_見込件数"
            arrHead(112) = "12月_見込金額"
            arrHead(113) = "12月_見込粗利"
            arrHead(114) = "1月_見込件数"
            arrHead(115) = "1月_見込金額"
            arrHead(116) = "1月_見込粗利"
            arrHead(117) = "2月_見込件数"
            arrHead(118) = "2月_見込金額"
            arrHead(119) = "2月_見込粗利"
            arrHead(120) = "3月_見込件数"
            arrHead(121) = "3月_見込金額"
            arrHead(122) = "3月_見込粗利"
            arrHead(123) = "4月_実績件数"
            arrHead(124) = "4月_実績金額"
            arrHead(125) = "4月_実績粗利"
            arrHead(126) = "5月_実績件数"
            arrHead(127) = "5月_実績金額"
            arrHead(128) = "5月_実績粗利"
            arrHead(129) = "6月_実績件数"
            arrHead(130) = "6月_実績金額"
            arrHead(131) = "6月_実績粗利"
            arrHead(132) = "7月_実績件数"
            arrHead(133) = "7月_実績金額"
            arrHead(134) = "7月_実績粗利"
            arrHead(135) = "8月_実績件数"
            arrHead(136) = "8月_実績金額"
            arrHead(137) = "8月_実績粗利"
            arrHead(138) = "9月_実績件数"
            arrHead(139) = "9月_実績金額"
            arrHead(140) = "9月_実績粗利"
            arrHead(141) = "10月_実績件数"
            arrHead(142) = "10月_実績金額"
            arrHead(143) = "10月_実績粗利"
            arrHead(144) = "11月_実績件数"
            arrHead(145) = "11月_実績金額"
            arrHead(146) = "11月_実績粗利"
            arrHead(147) = "12月_実績件数"
            arrHead(148) = "12月_実績金額"
            arrHead(149) = "12月_実績粗利"
            arrHead(150) = "1月_実績件数"
            arrHead(151) = "1月_実績金額"
            arrHead(152) = "1月_実績粗利"
            arrHead(153) = "2月_実績件数"
            arrHead(154) = "2月_実績金額"
            arrHead(155) = "2月_実績粗利"
            arrHead(156) = "3月_実績件数"
            arrHead(157) = "3月_実績金額"
            arrHead(158) = "3月_実績粗利"
            arrHead(159) = "4月_計算用_工事判定率"
            arrHead(160) = "4月_計算用_工事受注率"
            arrHead(161) = "4月_計算用_直工事率"
            arrHead(162) = "5月_計算用_工事判定率"
            arrHead(163) = "5月_計算用_工事受注率"
            arrHead(164) = "5月_計算用_直工事率"
            arrHead(165) = "6月_計算用_工事判定率"
            arrHead(166) = "6月_計算用_工事受注率"
            arrHead(167) = "6月_計算用_直工事率"
            arrHead(168) = "7月_計算用_工事判定率"
            arrHead(169) = "7月_計算用_工事受注率"
            arrHead(170) = "7月_計算用_直工事率"
            arrHead(171) = "8月_計算用_工事判定率"
            arrHead(172) = "8月_計算用_工事受注率"
            arrHead(173) = "8月_計算用_直工事率"
            arrHead(174) = "9月_計算用_工事判定率"
            arrHead(175) = "9月_計算用_工事受注率"
            arrHead(176) = "9月_計算用_直工事率"
            arrHead(177) = "10月_計算用_工事判定率"
            arrHead(178) = "10月_計算用_工事受注率"
            arrHead(179) = "10月_計算用_直工事率"
            arrHead(180) = "11月_計算用_工事判定率"
            arrHead(181) = "11月_計算用_工事受注率"
            arrHead(182) = "11月_計算用_直工事率"
            arrHead(183) = "12月_計算用_工事判定率"
            arrHead(184) = "12月_計算用_工事受注率"
            arrHead(185) = "12月_計算用_直工事率"
            arrHead(186) = "1月_計算用_工事判定率"
            arrHead(187) = "1月_計算用_工事受注率"
            arrHead(188) = "1月_計算用_直工事率"
            arrHead(189) = "2月_計算用_工事判定率"
            arrHead(190) = "2月_計算用_工事受注率"
            arrHead(191) = "2月_計算用_直工事率"
            arrHead(192) = "3月_計算用_工事判定率"
            arrHead(193) = "3月_計算用_工事受注率"
            arrHead(194) = "3月_計算用_直工事率"
            arrHead(195) = "営業区分"
            arrHead(196) = "登録日時"
            arrHead(197) = "4月_計算用__売上平均単価"
            arrHead(198) = "4月_計算用__仕入平均単価"
            arrHead(199) = "5月_計算用__売上平均単価"
            arrHead(200) = "5月_計算用__仕入平均単価"
            arrHead(201) = "6月_計算用__売上平均単価"
            arrHead(202) = "6月_計算用__仕入平均単価"
            arrHead(203) = "7月_計算用__売上平均単価"
            arrHead(204) = "7月_計算用__仕入平均単価"
            arrHead(205) = "8月_計算用__売上平均単価"
            arrHead(206) = "8月_計算用__仕入平均単価"
            arrHead(207) = "9月_計算用__売上平均単価"
            arrHead(208) = "9月_計算用__仕入平均単価"
            arrHead(209) = "10月_計算用__売上平均単価"
            arrHead(210) = "10月_計算用__仕入平均単価"
            arrHead(211) = "11月_計算用__売上平均単価"
            arrHead(212) = "11月_計算用__仕入平均単価"
            arrHead(213) = "12月_計算用__売上平均単価"
            arrHead(214) = "12月_計算用__仕入平均単価"
            arrHead(215) = "1月_計算用__売上平均単価"
            arrHead(216) = "1月_計算用__仕入平均単価"
            arrHead(217) = "2月_計算用__売上平均単価"
            arrHead(218) = "2月_計算用__仕入平均単価"
            arrHead(219) = "3月_計算用__売上平均単価"
            arrHead(220) = "3月_計算用__仕入平均単価"
            arrHead(221) = "分別コード表示順"
            arrHead(222) = "営業区分表示順"
            arrHead(223) = "明細、小計、合計区分"
            arrHead(224) = "商品コード"
            arrHead(225) = "商品の表示順"
            arrHead(226) = "件数カウント有無"
            arrHead(227) = "計画確定FLG"
            arrHead(228) = "計画値不変FLG"
            arrHead(229) = "前年_仕入平均単価"
            arrHead(230) = "計画_SS_SDS入力有無"
            arrHead(231) = "見込_SS_SDS入力有無"
            arrHead(232) = "計画_SDS追加金額有無"
            arrHead(233) = "見込_SDS追加金額有無"

        End If
        Return arrHead(index)
    End Function
    ''' <summary>
    ''' SQLのindex
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum selectIndex
        ''' <summary>
        ''' 加盟店名
        ''' </summary>
        ''' <remarks></remarks>
        kameiten_mei = 0
        ''' <summary>
        ''' 加盟店コード
        ''' </summary>
        ''' <remarks></remarks>
        kameiten_cd = 1
        ''' <summary>
        ''' 営業区分
        ''' </summary>
        ''' <remarks></remarks>
        meisyou = 2
        ''' <summary>
        ''' 営業担当者名
        ''' </summary>
        ''' <remarks></remarks>
        eigyou_tantousya_mei = 3
        ''' <summary>
        ''' 年間棟数
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_nenkan_tousuu = 4

        ''' <summary>
        ''' 計画用_年間棟数
        ''' </summary>
        ''' <remarks></remarks>
        keikakuyou_nenkan_tousuu = 5

        ''' <summary>
        ''' 業態
        ''' </summary>
        ''' <remarks></remarks>
        gyoutai = 6
        ''' <summary>
        ''' 売上比率
        ''' </summary>
        ''' <remarks></remarks>
        uri_hiritu = 7

        ''' <summary>
        ''' SDS開始年月
        ''' </summary>
        ''' <remarks></remarks>
        sds_kaisi_nengetu = 8

        ''' <summary>
        ''' 工事判定率
        ''' </summary>
        ''' <remarks></remarks>
        koj_hantei_ritu = 9
        ''' <summary>
        ''' 工事受注率
        ''' </summary>
        ''' <remarks></remarks>
        koj_jyuchuu_ritu = 10
        ''' <summary>
        ''' 直工事率
        ''' </summary>
        ''' <remarks></remarks>
        tyoku_koj_ritu = 11
        ''' <summary>
        ''' 名称種別
        ''' </summary>
        ''' <remarks></remarks>
        meisyou2 = 12
        ''' <summary>
        ''' 商品名
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_mei = 13
        ''' <summary>
        ''' 前年_平均単価
        ''' </summary>
        ''' <remarks></remarks>
        zennen_heikin_tanka = 14
        ''' <summary>
        ''' 実績
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
        ''' 計画
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
        ''' 見込
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
        ''' 前年実績
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
        ''' 計算用
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
        ''' 営業区分
        ''' </summary>
        ''' <remarks></remarks>
        eigyou_kbn = 195
        ''' <summary>
        ''' 登録日時
        ''' </summary>
        ''' <remarks></remarks>
        add_datetime = 196

        ''' <summary>
        ''' 計算用__売上平均単価、計算用__仕入平均単価
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
        ''' 分別コード表示順
        ''' </summary>
        ''' <remarks></remarks>
        hyouji_jyun2 = 221
        ''' <summary>
        ''' 営業区分表示順
        ''' </summary>
        ''' <remarks></remarks>
        hyouji_jyun = 222
        ''' <summary>
        ''' 明細、小計、合計区分
        ''' </summary>
        ''' <remarks></remarks>
        data_type = 223

        ''' <summary>
        ''' 商品コード
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_cd = 224
        ''' <summary>
        '''  商品の表示順
        ''' </summary>
        ''' <remarks></remarks>
        syouhin_jyun = 225
        ''' <summary>
        ''' 件数カウント有無
        ''' </summary>
        ''' <remarks></remarks>
        kensuu_count_umu = 226
        ''' <summary>
        '''  計画確定FLG
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_kakutei_flg = 227
        ''' <summary>
        ''' 計画値不変FLG
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_huhen_flg = 228
        ''' <summary>
        '''   前年_仕入平均単価
        ''' </summary>
        ''' <remarks></remarks>
        zennen_siire_heikin_tanka = 229
        ''' <summary>
        '''   計画_SS_SDS入力有無
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_ss_sds_umu = 230
        ''' <summary>
        '''   見込_SS_SDS入力有無
        ''' </summary>
        ''' <remarks></remarks>
        mikomi_ss_sds_umu = 231
        ''' <summary>
        '''   計画_SDS追加金額有無
        ''' </summary>
        ''' <remarks></remarks>
        keikaku_sds_tuika_kin_umu = 232
        ''' <summary>
        '''  見込_SDS追加金額有無
        ''' </summary>
        ''' <remarks></remarks>
        mikomi_sds_tuika_kin_umu = 233
        max = 233
    End Enum
    Public Enum taisyouKikan
        ''' <summary>
        ''' 今月
        ''' </summary>
        ''' <remarks></remarks>
        Kongetu = 1
        ''' <summary>
        ''' 直近3ヶ月
        ''' </summary>
        ''' <remarks></remarks>
        Sangetu = 2
        ''' <summary>
        ''' 先行4ヶ月
        ''' </summary>
        ''' <remarks></remarks>
        Yogetu = 3
        ''' <summary>
        ''' 年間
        ''' </summary>
        ''' <remarks></remarks>
        Nenkan = 4
    End Enum
    ''' <summary>
    ''' 検索条件を取得
    ''' </summary>
    ''' <returns>検索条件</returns>
    ''' <remarks>2012/11/28 高</remarks>
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
            '営業区分
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
            '絞込み選択
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
            '表示欄選択
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
    ''' 比率
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum HirituItem As Integer
        KoujiHiritu = 1 '工事比率
    End Enum

    ''' <summary>
    ''' 前年同月
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ZennenItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum

    ''' <summary>
    ''' 計画
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum KeikakuItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum

    ''' <summary>
    ''' 見込
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MikomiItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum

    ''' <summary>
    ''' 実績
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum JissekiItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum

    ''' <summary>
    ''' 達成率
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TasseirituItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum

    ''' <summary>
    ''' 進捗率
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SintyokurituItem As Integer
        Kensuu = 1 '件数
        Kingaku = 2 '金額
        Arari = 3 '粗利
    End Enum



#Region "年月日"
    ''' <summary>
    ''' 年月日
    ''' </summary>
    ''' <remarks></remarks>
    Private strNendo As String
    ''' <summary>
    ''' 年度
    ''' </summary>
    ''' <value></value>
    ''' <returns>年度</returns>
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
#Region "支店"
    ''' <summary>
    ''' 支店
    ''' </summary>
    ''' <remarks></remarks>
    Private strShiten As String
    ''' <summary>
    ''' 支店
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店</returns>
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
#Region "営業マン"
    ''' <summary>
    ''' 営業マン
    ''' </summary>
    ''' <remarks></remarks>
    Private strUser As String
    ''' <summary>
    ''' 営業マン
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業マン</returns>
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
#Region "加盟店"
    ''' <summary>
    ''' 加盟店
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiten As String
    ''' <summary>
    ''' 加盟店
    ''' </summary>
    ''' <value></value>
    ''' <returns>加盟店</returns>
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
#Region "系列"
    ''' <summary>
    ''' 系列
    ''' </summary>
    ''' <remarks></remarks>
    Private strShitenMei As String
    ''' <summary>
    ''' 系列
    ''' </summary>
    ''' <value></value>
    ''' <returns>系列</returns>
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
#Region "営業所"
    ''' <summary>
    ''' 営業所
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyou As String
    ''' <summary>
    ''' 営業所
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業所</returns>
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

#Region "所属"
    ''' <summary>
    ''' 所属
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyozoku As String
    ''' <summary>
    ''' 所属
    ''' </summary>
    ''' <value></value>
    ''' <returns>所属</returns>
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
#Region "都道府県"
    ''' <summary>
    ''' 都道府県
    ''' </summary>
    ''' <remarks></remarks>
    Private strTodoufuken As String
    ''' <summary>
    ''' 都道府県
    ''' </summary>
    ''' <value></value>
    ''' <returns>都道府県</returns>
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
#Region "統一法人"
    ''' <summary>
    ''' 統一法人
    ''' </summary>
    ''' <remarks></remarks>
    Private strTouituHoujin As String
    ''' <summary>
    ''' 統一法人
    ''' </summary>
    ''' <value></value>
    ''' <returns>統一法人</returns>
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
#Region "法人"
    ''' <summary>
    ''' 法人
    ''' </summary>
    ''' <remarks></remarks>
    Private strHoujin As String
    ''' <summary>
    ''' 法人
    ''' </summary>
    ''' <value></value>
    ''' <returns>法人</returns>
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
#Region "属性1"
    ''' <summary>
    ''' 属性1
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei1 As String
    ''' <summary>
    ''' 属性1
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性1</returns>
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
#Region "属性2"
    ''' <summary>
    ''' 属性2
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei2 As String
    ''' <summary>
    ''' 属性2
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性2</returns>
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
#Region "属性3"
    ''' <summary>
    ''' 属性3
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei3 As String
    ''' <summary>
    ''' 属性3
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性3</returns>
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
#Region "属性4"
    ''' <summary>
    ''' 属性4
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei4 As String
    ''' <summary>
    ''' 属性4
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性4</returns>
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
#Region "属性5"
    ''' <summary>
    ''' 属性5
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei5 As String
    ''' <summary>
    ''' 属性5
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性5</returns>
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
#Region "属性6"
    ''' <summary>
    ''' 属性6
    ''' </summary>
    ''' <remarks></remarks>
    Private strZokusei6 As String
    ''' <summary>
    ''' 属性6
    ''' </summary>
    ''' <value></value>
    ''' <returns>属性6</returns>
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

#Region "対象期間"
    ''' <summary>
    ''' 対象期間
    ''' </summary>
    ''' <remarks></remarks>
    Private strTaisyou As taisyouKikan = taisyouKikan.Nenkan
    ''' <summary>
    ''' 対象期間
    ''' </summary>
    ''' <value></value>
    ''' <returns>対象期間</returns>
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
#Region "営業区分"
    ''' <summary>
    ''' 営業区分
    ''' </summary>
    ''' <remarks></remarks>
    Private lstEigyouKBN As EigyouJyouken
    ''' <summary>
    ''' 営業区分
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業区分</returns>
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
#Region "絞込み選択"
    ''' <summary>
    ''' 絞込み選択
    ''' </summary>
    ''' <remarks></remarks>
    Private lstSiborikomi As SiborikomiJyouken
    ''' <summary>
    ''' 絞込み選択
    ''' </summary>
    ''' <value></value>
    ''' <returns>絞込み選択</returns>
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
#Region "表示欄選択"
    ''' <summary>
    ''' 表示欄選択
    ''' </summary>
    ''' <remarks></remarks>
    Private lstHyoujiSentaku As HyoujiSentakuJyouken
    ''' <summary>
    ''' 表示欄選択
    ''' </summary>
    ''' <value></value>
    ''' <returns>表示欄選択</returns>
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

#Region "年間棟数範囲"
    ''' <summary>
    ''' 年間棟数範囲
    ''' </summary>
    ''' <remarks></remarks>
    Private lstNenkanTousuuHani As NenkanTousuuHaniJyouken
    ''' <summary>
    ''' 年間棟数範囲
    ''' </summary>
    ''' <value></value>
    ''' <returns>年間棟数範囲</returns>
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

#Region "表示件数"
    ''' <summary>
    ''' 表示件数
    ''' </summary>
    ''' <remarks></remarks>
    Private strKensuu As String
    ''' <summary>
    ''' 表示件数
    ''' </summary>
    ''' <value></value>
    ''' <returns>表示件数</returns>
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
''' 表示欄選択
''' </summary>
''' <remarks>2012/11/28 高</remarks>
<System.Serializable()> Public Class HyoujiSentakuJyouken
#Region " 計画"
    ''' <summary>
    '''  計画
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikaku As Boolean
    ''' <summary>
    '''  計画
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画</returns>
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
#Region " 見込"
    ''' <summary>
    '''  見込
    ''' </summary>
    ''' <remarks></remarks>
    Private blnMikomi As Boolean
    ''' <summary>
    '''  見込
    ''' </summary>
    ''' <value></value>
    ''' <returns> 見込</returns>
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
#Region " 実績"
    ''' <summary>
    '''  実績
    ''' </summary>
    ''' <remarks></remarks>
    Private blnJisseki As Boolean
    ''' <summary>
    '''  実績
    ''' </summary>
    ''' <value></value>
    ''' <returns> 実績</returns>
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
#Region "達成率"
    ''' <summary>
    '''  達成率
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTassei As Boolean
    ''' <summary>
    '''  達成率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 達成率</returns>
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
#Region "進捗率"
    ''' <summary>
    '''  進捗率
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSintyoku As Boolean
    ''' <summary>
    '''  進捗率
    ''' </summary>
    ''' <value></value>
    ''' <returns> 進捗率</returns>
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

#Region "前年同月"
    ''' <summary>
    '''  前年同月
    ''' </summary>
    ''' <remarks></remarks>
    Private blnZennenDougetu As Boolean
    ''' <summary>
    '''  前年同月
    ''' </summary>
    ''' <value></value>
    ''' <returns> 前年同月</returns>
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
''' 絞込み選択
''' </summary>
''' <remarks>2012/11/28 高</remarks>
<System.Serializable()> Public Class SiborikomiJyouken
#Region " 計画値"
    ''' <summary>
    '''  計画値
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikakuTi As Boolean
    ''' <summary>
    '''  計画値
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画値</returns>
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
#Region " 新規登録事業者"
    ''' <summary>
    '''  計画値
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSinkiTouroku As Boolean
    ''' <summary>
    '''  新規登録事業者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新規登録事業者</returns>
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
#Region " 分譲50社"
    ''' <summary>
    '''  分譲50社
    ''' </summary>
    ''' <remarks></remarks>
    Private blnBunjyou As Boolean
    ''' <summary>
    '''  分譲50社
    ''' </summary>
    ''' <value></value>
    ''' <returns> 分譲50社</returns>
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
#Region " 注文50社"
    ''' <summary>
    '''  注文50社
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTyumon As Boolean
    ''' <summary>
    '''  注文50社
    ''' </summary>
    ''' <value></value>
    ''' <returns> 注文50社</returns>
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
#Region "年間棟数"
    ''' <summary>
    '''  年間棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private blnNenkanTouSuu As Boolean
    ''' <summary>
    '''  年間棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 年間棟数</returns>
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
''' 年間棟数範囲
''' </summary>
''' <remarks>2013/10/11 車</remarks>
<System.Serializable()> Public Class NenkanTousuuHaniJyouken
#Region "計画用"
    ''' <summary>
    '''  計画用
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKeikakuyou As Boolean
    ''' <summary>
    '''  計画用
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画用</returns>
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
#Region "年間棟数値From"
    ''' <summary>
    '''  年間棟数値From
    ''' </summary>
    ''' <remarks></remarks>
    Private strNenkanTouSuuFrom As String
    ''' <summary>
    '''  年間棟数値From
    ''' </summary>
    ''' <value></value>
    ''' <returns> 年間棟数値From</returns>
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
#Region "年間棟数値To"
    ''' <summary>
    '''  年間棟数値To
    ''' </summary>
    ''' <remarks></remarks>
    Private strNenkanTouSuuTo As String
    ''' <summary>
    '''  年間棟数値To
    ''' </summary>
    ''' <value></value>
    ''' <returns> 年間棟数値To</returns>
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
''' 営業区分クラス
''' </summary>
''' <remarks>2012/11/28 高</remarks>
<System.Serializable()> Public Class EigyouJyouken
#Region " 営業"
    ''' <summary>
    '''  営業
    ''' </summary>
    ''' <remarks></remarks>
    Private blnEigyou As Boolean
    ''' <summary>
    '''  営業
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業</returns>
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
#Region " 営業(新規)"
    ''' <summary>
    '''  営業(新規)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnEigyouNew As Boolean
    ''' <summary>
    '''  営業(新規)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 営業(新規)</returns>
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
#Region " 特販"
    ''' <summary>
    '''  特販
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTokuhan As Boolean
    ''' <summary>
    '''  特販
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特販</returns>
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
#Region " 特販(新規)"
    ''' <summary>
    '''  特販(新規)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnTokuhanNew As Boolean
    ''' <summary>
    '''  特販(新規)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 特販(新規)</returns>
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
#Region "新規"
    ''' <summary>
    '''  新規
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSinki As Boolean
    ''' <summary>
    '''  新規
    ''' </summary>
    ''' <value></value>
    ''' <returns> 新規</returns>
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
#Region "FC(新規)"
    ''' <summary>
    '''  FC(新規)
    ''' </summary>
    ''' <remarks></remarks>
    Private blnFCNew As Boolean
    ''' <summary>
    '''  FC(新規)
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
