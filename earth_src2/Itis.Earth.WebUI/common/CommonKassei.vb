Public Class CommonKassei
    Public Shared Sub SetDropdownListReadonly(ByRef control As DropDownList)
        control.Attributes.Item("onfocus") = "this.defaultIndex=this.selectedIndex;"
        control.Attributes.Item("onchange") = "this.selectedIndex=this.defaultIndex;"
        control.CssClass = "readOnly"
        control.Style.Item("background-color") = "Silver"
    End Sub

    Public Shared Sub SetDropdownListNotReadonly(ByRef control As DropDownList)
        control.Attributes.Item("onfocus") = ""
        control.Attributes.Item("onchange") = ""
        control.CssClass = ""
        control.Style.Item("background-color") = "#FFF"
    End Sub

End Class
