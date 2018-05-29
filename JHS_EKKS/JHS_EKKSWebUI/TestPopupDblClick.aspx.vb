
Partial Class TestPopupDblClick
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '系列コード検索 
        Me.Button1.Attributes("onclick") = _
                      "window.open('./PopupSearch/KeiretuSearch.aspx?formName=" & Me.Form.ClientID & "&strKeiretuCd='+ escape($ID('" & Me.tbxKeiretuCd.ClientID & "').value) +'&field=" & Me.tbxKeiretuCd.ClientID & "'+'&fieldMei=" & Me.tbxKeiretuMei.ClientID & "', 'KeiretuSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '営業所検索 
        Me.Button2.Attributes("onclick") = _
                      "window.open('./PopupSearch/EigyousyoSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyousyoMei='+ escape($ID('" & Me.tbxEigyousyo.ClientID & "').value)+'&field=" & Me.tbxEigyousyo.ClientID & "'+'&fieldCd=" & Me.tbxEigyousyoCd.ClientID & "', 'EigyousyoSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '登録事業者検索 
        Me.Button3.Attributes("onclick") = _
                      "window.open('./PopupSearch/TourokuJigyousyaSearch.aspx?formName=" & Me.Form.ClientID & "&strTourokuJigyousya='+ escape($ID('" & Me.tbxTourokusy.ClientID & "').value)+'&field=" & Me.tbxTourokusy.ClientID & "'+'&fieldCd=" & Me.tbxTourokusyCd.ClientID & "', 'TourokuJigyousya', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=850,height=500,top=30,left=0');return false;"

        '都道府県検索 
        Me.Button4.Attributes("onclick") = _
                      "window.open('./PopupSearch/TodoufukenSearch.aspx?formName=" & Me.Form.ClientID & "&strTodouhukenMei='+ escape($ID('" & Me.tbxToudouhuken.ClientID & "').value)+'&field=" & Me.tbxToudouhuken.ClientID & "'+'&fieldCd=" & Me.tbxToudouhukenCd.ClientID & "', 'TodouhukenMei', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '支店 検索
        Me.Button5.Attributes("onclick") = _
                      "window.open('./PopupSearch/SitenSearch.aspx?formName=" & Me.Form.ClientID & "&strSitenMei='+ escape($ID('" & Me.tbxCiten.ClientID & "').value)+'&field=" & Me.tbxCiten.ClientID & "'+'&fieldCd=" & Me.tbxCitenCd.ClientID & "', 'SitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '営業マン検索
        Me.Button6.Attributes("onclick") = _
                      "window.open('./PopupSearch/EigyouManSearch.aspx?formName=" & Me.Form.ClientID & "&strEigyouManMei='+ escape($ID('" & Me.tbxEigyouMan.ClientID & "').value)+'&field=" & Me.tbxEigyouMan.ClientID & "'+'&fieldCd=" & Me.tbxEigyouManCd.ClientID & "', 'EigyouManSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '計画管理_加盟店　検索
        Me.Button7.Attributes("onclick") = _
                      "window.open('./PopupSearch/KeikakuKanriKameitenSearch.aspx?formName=" & Me.Form.ClientID & "&strYear=2012&strTorikesi=true&strKameitenCdValue='+ escape($ID('" & Me.tbxKameitenCd.ClientID & "').value)+'&strKameitenCdId=" & Me.tbxKameitenCd.ClientID & "'+'&strKameitenMeiId=" & Me.tbxKameitenMei.ClientID & "', 'KeikakuKanriKameitenSearch', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

        '営業所検索(紹介指示用)
        Me.Button8.Attributes("onclick") = _
                      "window.open('./PopupSearch/EigyousyoSearchSyoukaiJisiyou.aspx?formName=" & Me.Form.ClientID & "&strTorikesi=true&strEigyousyoCdValue='+ escape($ID('" & Me.tbxEigyousyoCdEarth.ClientID & "').value)+'&strEigyousyoCdId=" & Me.tbxEigyousyoCdEarth.ClientID & "'+'&strEigyousyoMeiId=" & Me.tbxEigyousyoMeiEarth.ClientID & "', 'EigyousyoSearchSyoukaiJisiyou', 'menubar=no,toolbar=no,location=no,status=no,scrollbars=no,resizable=no,width=700,height=500,top=30,left=0');return false;"

    End Sub

End Class
