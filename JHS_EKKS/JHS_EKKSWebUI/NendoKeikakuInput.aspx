<%@ Page Language="VB" MasterPageFile="~/EKKSMaster.master" AutoEventWireup="false" CodeFile="NendoKeikakuInput.aspx.vb" Inherits="NendoKeikakuInput" title="�N�x�v��l�ݒ�" %>
<%@ MasterType VirtualPath="~/EKKSMaster.master" %>
<%@ Register Src="CommonControl/CommonButton.ascx" TagName="CommonButton" TagPrefix="uc1" %>
<%@ Register Assembly="Lixil.JHS_EKKS.Utilities" Namespace="Lixil.JHS_EKKS.Utilities"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
    //onload
    window.onload = function(){
        setMenuBgColor();
    }      
   
    //�����O�N��߰
    function fncKensuuCopy()
    {
        var ctlNum;
        var conJittuseki1 = "numJittuseki1_1";
        var conJittuseki2 = "numJittuseki1_2";
        var conJittuseki3 = "numJittuseki1_3";
        var conJittuseki4 = "numJittuseki1_4";
        
        var conKeikaku1 = "numKeikaku1_1";
        var conKeikaku2 = "numKeikaku1_2";
        var conKeikaku3 = "numKeikaku1_3";
        var conKeikaku4 = "numKeikaku1_4";
        
        var numJittuseki1;
        var numJittuseki2;
        var numJittuseki3;
        var numJittuseki4;
        
        var numKeikaku1;
        var numKeikaku2;
        var numKeikaku3;
        var numKeikaku4;
        var intCount;

        var table = document.getElementById('ctl00_ContentPlaceHolder1_grdMeisai');
        var trs =  table.getElementsByTagName("tr");
        intCount = trs.length / 3;
        
        for(var i = 0; i < intCount; i++) 
        {                
            ctlNum = PadLeft(i.toString(),2);
            ctlNum = "ctl" +  ctlNum + "_";
                            
            numJittuseki1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki1);                
            numJittuseki2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki2);
            numJittuseki3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki3);
            numJittuseki4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki4);
        
            numKeikaku1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku1);
            numKeikaku2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku2);
            numKeikaku3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku3);
            numKeikaku4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku4);
            
            numKeikaku1.value = numJittuseki1.value;
            numKeikaku2.value = numJittuseki2.value;
            numKeikaku3.value = numJittuseki3.value;
            numKeikaku4.value = numJittuseki4.value;
        }
        
        //�Y����̍��v
        fncColSum(conKeikaku1);
        fncColSum(conKeikaku2);
        fncColSum(conKeikaku3);
        
        //�����v
        fncSum();
    }
    
    //����O�N��߰
    function fncUriKingakuCopy()
    {
        var ctlNum;
        var conJittuseki1 = "numJittuseki2_1";
        var conJittuseki2 = "numJittuseki2_2";
        var conJittuseki3 = "numJittuseki2_3";
        var conJittuseki4 = "numJittuseki2_4";
        
        var conKeikaku1 = "numKeikaku2_1";
        var conKeikaku2 = "numKeikaku2_2";
        var conKeikaku3 = "numKeikaku2_3";
        var conKeikaku4 = "numKeikaku2_4";
        
        var numJittuseki1;
        var numJittuseki2;
        var numJittuseki3;
        var numJittuseki4;
        
        var numKeikaku1;
        var numKeikaku2;
        var numKeikaku3;
        var numKeikaku4;
        var intCount;

        var table = document.getElementById('ctl00_ContentPlaceHolder1_grdMeisai');
        var trs =  table.getElementsByTagName("tr");
        intCount = trs.length / 3;
        
        for(var i = 0; i < intCount; i++) 
        {                
            ctlNum = PadLeft(i.toString(),2);
            ctlNum = "ctl" +  ctlNum + "_";
                            
            numJittuseki1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki1);                
            numJittuseki2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki2);
            numJittuseki3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki3);
            numJittuseki4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki4);
        
            numKeikaku1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku1);
            numKeikaku2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku2);
            numKeikaku3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku3);
            numKeikaku4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku4);
            
            numKeikaku1.value = numJittuseki1.value;
            numKeikaku2.value = numJittuseki2.value;
            numKeikaku3.value = numJittuseki3.value;
            numKeikaku4.value = numJittuseki4.value;
        }
        
        //�Y����̍��v
        fncColSum(conKeikaku1);
        fncColSum(conKeikaku2);
        fncColSum(conKeikaku3);
        
        //�����v
        fncSum();
    }
    
    //�e���O�N��߰
    function fncArariCopy()
    {
        var ctlNum;
        var conJittuseki1 = "numJittuseki3_1";
        var conJittuseki2 = "numJittuseki3_2";
        var conJittuseki3 = "numJittuseki3_3";
        var conJittuseki4 = "numJittuseki3_4";
        
        var conKeikaku1 = "numKeikaku3_1";
        var conKeikaku2 = "numKeikaku3_2";
        var conKeikaku3 = "numKeikaku3_3";
        var conKeikaku4 = "numKeikaku3_4";
        
        var numJittuseki1;
        var numJittuseki2;
        var numJittuseki3;
        var numJittuseki4;
        
        var numKeikaku1;
        var numKeikaku2;
        var numKeikaku3;
        var numKeikaku4;
        var intCount;

        var table = document.getElementById('ctl00_ContentPlaceHolder1_grdMeisai');
        var trs =  table.getElementsByTagName("tr");
        intCount = trs.length / 3;
        
        for(var i = 0; i < intCount; i++) 
        {                
            ctlNum = PadLeft(i.toString(),2);
            ctlNum = "ctl" +  ctlNum + "_";
                            
            numJittuseki1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki1);                
            numJittuseki2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki2);
            numJittuseki3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki3);
            numJittuseki4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conJittuseki4);
        
            numKeikaku1 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku1);
            numKeikaku2 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku2);
            numKeikaku3 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku3);
            numKeikaku4 = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + conKeikaku4);
            
            numKeikaku1.value = numJittuseki1.value;
            numKeikaku2.value = numJittuseki2.value;
            numKeikaku3.value = numJittuseki3.value;
            numKeikaku4.value = numJittuseki4.value;
        }
        
        //�Y����̍��v
        fncColSum(conKeikaku1);
        fncColSum(conKeikaku2);
        fncColSum(conKeikaku3);
        
        //�����v
        fncSum();
    }
    
    //�v�捀�ڂ̌v�Z
    function fncKeikakuSum(strControlId)
    {
        //�Y���s�̍��v
        fncRowSum(strControlId);
        
        //�Y����̍��v
        fncColSum(strControlId);
        
        //�����v
        fncSum();
    }
    
    //�Y���s���v
    function fncRowSum(strControlId)
    {
        var strId;
        var strJittuseki = "numJittuseki";
        var strKeikaku = "numKeikaku";
        
        var strKeikaku1;
        var strKeikaku2;
        var strKeikaku3;
        var strKeikaku4;
        
        var numKeikaku1;
        var numKeikaku2;
        var numKeikaku3;
        var numKeikaku4;
        
        strId = strControlId.substr(0,strControlId.length - 1);
        strKeikaku1 = document.getElementById(strId + "1").value;        
        strKeikaku2 = document.getElementById(strId + "2").value;
        strKeikaku3 = document.getElementById(strId + "3").value;
        strKeikaku4 = document.getElementById(strId + "4").value;
        
        if (strKeikaku1 == '')
        {
            numKeikaku1 = 0;
        }else
        {
            numKeikaku1 = GetValueToNumber(strKeikaku1);
        }
        
        if (strKeikaku2 == '')
        {
            numKeikaku2 = 0;
        }else
        {
            numKeikaku2 = GetValueToNumber(strKeikaku2);
        }
        
        if (strKeikaku3 == '')
        {
            numKeikaku3 = 0;
        }else
        {
            numKeikaku3 = GetValueToNumber(strKeikaku3);
        }
        numKeikaku4 = numKeikaku1 + numKeikaku2 + numKeikaku3;
        strKeikaku4 = numKeikaku4.toString();
        
        while(strKeikaku4 != (strKeikaku4 = strKeikaku4.replace(/^(-?\d+)(\d{3})/, "$1,$2")));
        
        document.getElementById(strId + "4").value = strKeikaku4;
    }
    
    //�Y���񍇌v
    function fncColSum(strControlId)
    {
        var ctlNum;             //�s�ԍ�
        var strKeikaku;         //�Y���s�̃R���g���[���R�[�h
        var conKeikaku;         //�Y���R���g���[��
        var numKeikaku;         //�Y�����ڂ̒l
        var numSubColKeikaku;   //�Y����̍��v
        var strSubColKeikaku;   //�Y����̍��v
        
        var table = document.getElementById('ctl00_ContentPlaceHolder1_grdMeisai');
        var trs =  table.getElementsByTagName("tr");
        var intCount;
        var i;
        
        numSubColKeikaku = 0;
        
        //�Y���s�̃R���g���[���R�[�h���擾����
        strKeikaku = strControlId.substr(strControlId.lastIndexOf("numKeikaku"));
        
        //�����R�[�h�����擾����
        intCount = trs.length / 3
        
        for (i=0;i<intCount;i++)
        {
            //�s�ԍ���ݒ肷��
            ctlNum = PadLeft(i.toString(),2);
            ctlNum = "ctl" +  ctlNum + "_";
            
            //�Y���R���g���[�����擾����
            conKeikaku = document.getElementById("ctl00_ContentPlaceHolder1_grdMeisai_" + ctlNum + strKeikaku);
            
            if (conKeikaku.value == '')
            {
                numKeikaku = 0;
            }else
            {
                numKeikaku = GetValueToNumber(conKeikaku.value);
            }
        
            //�Y����̑����v���v�Z����
            numSubColKeikaku = numSubColKeikaku + numKeikaku;
        }
        
        //�s�ԍ���ݒ肷��
        ctlNum = PadLeft(intCount.toString(),2);
        ctlNum = "ctl" +  ctlNum + "_";
        
        //�Y���R���g���[�����擾����
        conKeikaku = document.getElementById("ctl00_ContentPlaceHolder1_" +  strKeikaku + "_Sum");
            
        //�Y����̑����v��ݒ肷��
        strSubColKeikaku = numSubColKeikaku.toString();
        while(strSubColKeikaku != (strSubColKeikaku = strSubColKeikaku.replace(/^(-?\d+)(\d{3})/, "$1,$2")));
        conKeikaku.value = strSubColKeikaku;
    }
        
    //�����v
    function fncSum()
    {        
        var strId;
        var strKeikaku;
        var numKeikaku;
        var numSumKeikaku;
        var i;
        var j;
        
        strId = "ctl00_ContentPlaceHolder1_numKeikaku";
        for (i=1;i<4;i++)
        {
            numSumKeikaku = 0;
            for (j=1;j<4;j++)
            {                
                strKeikaku = document.getElementById(strId + i.toString() + "_" + j.toString() + "_Sum").value
                
                if (strKeikaku == "" )
                {
                    numKeikaku = 0;
                }else
                {
                    numKeikaku = GetValueToNumber(strKeikaku);
                }
                
                numSumKeikaku = numSumKeikaku + numKeikaku;
            }
            
            strKeikaku = numSumKeikaku.toString();
            
            while(strKeikaku != (strKeikaku = strKeikaku.replace(/^(-?\d+)(\d{3})/, "$1,$2")));
            document.getElementById(strId + i.toString() + "_4_Sum").value = strKeikaku;
        }
    }
</script>
    
    <table cellpadding="0" cellspacing="0" width="980px" style="border-bottom-style: solid; border-bottom-color:Black; border-width:2px">        
        <tr style="padding-top: 10px;">            
            <td colspan="5"><span style="font-size:15px; font-weight: bold;">�S�ЁE�x�X�� �N�x�v��Ǘ�</span></td>
        </tr>
        <tr style="padding-top: 10px;">
            <td style="width:10px"></td>
            <td colspan="4" style="font-weight: bold;">�E�S�� �v��l�ݒ�</td>
        </tr>
        <tr style="padding-top: 10px;">
            <td style="width:10px"></td>
            <td style="font-weight: bold;">&nbsp;&nbsp;�N�x �I��</td>
            <td colspan="3">
                <cc1:CommonDropDownList ID="drpNendo" runat="server" DataTextField="meisyou" DataValueField="code" AutoPostBack="True" IsAddNullRow="True">
                </cc1:CommonDropDownList></td>
        </tr>
        <tr style="padding-top: 10px;">
            <td style="width:5px"></td>
            <td colspan="4">
                <table cellpadding="0" cellspacing="0" style="border-width:1px; border-style:solid;">
                    <tr>
                        <td align="center" style="background-color: #ccecff;width:100px;font-weight: bold;">�v�� ��������</td>
                        <td style="background-color: #e6e6e6;width:180px">
    <cc1:CommonNumber ID="numKeikakuKensuu" MaxLength="12" runat="server" Width="120px"></cc1:CommonNumber>���ȏ�</td>
                        <td align="center" style="background-color: #ccecff;width:100px;font-weight: bold;">�v�� ������z</td>
                        <td style="background-color: #e6e6e6;width:150px">
                            <cc1:CommonNumber ID="numKeikakuUriKingaku" MaxLength="12" runat="server" Width="120px" ObjName=""></cc1:CommonNumber>�~</td>
                        <td align="center" style="background-color: #ccecff;width:100px;font-weight: bold;">�v�� �e�����z</td>
                        <td style="background-color: #e6e6e6;">
                            <cc1:CommonNumber ID="numKeikakuArari" MaxLength="12" runat="server" Width="120px"></cc1:CommonNumber>�~</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="padding-top: 10px; padding-bottom: 5px;">
            <td style="width:10px;"></td>
            <td align="right" style="width: 10%">�R�����g&nbsp;&nbsp;</td>
            <td style="width: 60%">
                <cc1:CommonText ID="txtKome" runat="server" MaxLength="40" Width="580px"></cc1:CommonText></td>
            <td align="center" style="width: 15%">
                <uc1:CommonButton ID="btnAllSave" ButtonKegen="ZensyaKeikakuKengen" Text="�v��l�ۑ�" runat="server" /></td>
            <td>
                <uc1:CommonButton ID="btnAllConfirm" ButtonKegen="ZensyaKeikakuKengen" Text="�m ��" BackColor="#FF00FE"  runat="server" />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="font-weight: bold;">�E�x�X�� �v��l�ݒ�</td>
            <td style="width: 67px"><asp:Label ID="lblNendo" runat="server" Font-Bold="True" Font-Strikeout="False" Font-Underline="False"></asp:Label></td>
            <td align="right" style="width:120px"><asp:Button ID="btnKensuuCopy" OnClientClick="fncKensuuCopy();return false;" style="padding-top:2px;" runat="server" Text="���� �O�N��߰" /></td>
            <td align="right" style="width:322px"><asp:Button ID="btnUriKingakuCopy" OnClientClick="fncUriKingakuCopy();return false;" style="padding-top:2px;" runat="server" Text="���� �O�N��߰" /></td>
            <td align="right" style="width:307px"><asp:Button ID="btnArariCopy" OnClientClick="fncArariCopy();return false;" style="padding-top:2px;" runat="server" Text="�e�� �O�N��߰" /></td>
        </tr>        
    </table>
    <div style="z-index: 1; left: 12px; position: absolute; top: 250px;"><asp:Button ID="btnSitenbetu" style="padding-top:2px;" runat="server" Text="���� �v��l�ݒ�" Width="110px" /></div>
    <table cellpadding="0" cellspacing="0" width="963px" style="border-right: black 2px solid; border-bottom: black 0px solid;">
        <tr style="border-top-style: solid; border-right-style: solid; border-left-style: solid; border-bottom-style: solid;">
            <td style="width:124px" ></td>
            <td colspan="4" align="center" style="background-color:#D2FEFF;border-top: black 2px solid; border-left: black 2px solid; border-bottom: black 2px solid;">�v�� ��������</td>
            <td colspan="4" align="center" style="background-color:#D2FEFF;border-top: black 2px solid; border-left: black 2px solid; border-bottom: black 2px solid;">�v�� ������z</td>
            <td colspan="4" align="center" style="background-color:#D2FEFF;border-top: black 2px solid; border-left: black 2px solid; border-bottom: black 2px solid;">�v�� �e�����z</td>
        </tr>
        <tr>
            <td runat="server" id="tdMeisaiHead">&nbsp;</td>
            <td align="center" style="width:47px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�c��</td>
            <td align="center" style="width:47px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">����</td>
            <td align="center" style="width:46px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�e�b</td>
            <td align="center" style="width:49px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">���v</td>
            <td align="center" style="width:79px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�c��</td>
            <td align="center" style="width:78px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">����</td>
            <td align="center" style="width:79px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�e�b</td>
            <td align="center" style="width:84px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">���v</td>
            <td align="center" style="width:75px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�c��</td>
            <td align="center" style="width:74px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">����</td>
            <td align="center" style="width:75px;background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">�e�b</td>
            <td align="center" style="background-color:#D2FEFF; border-left: black 2px solid;border-bottom: black 2px solid;">���v</td>
        </tr>
    </table>
    <table cellspacing="0" cellpadding="0" border="0" width="982px">
     <tr><td></td>
       <td>
        <div style="height:255px;overflow:auto;margin-top:-1px;width:982px;" runat="server" id="divMeisai">
            <asp:DataList ID="grdMeisai" Width="963px" runat="server" ShowFooter="False" ShowHeader="False" Font-Size="5pt" >
                
                <ItemTemplate>
                        <tr id="jittusekiRows" style="border-top: black 2px solid;">
                            <td rowspan="2" style="width:87px;background-color:#FFC991;border-right: black 2px solid; border-left: black 2px solid; border-bottom: black 2px solid;">
                                <cc1:CommonText ID="CommonText2" Width="83px" Text='<%#Eval("busyo_mei")%>' runat="server" BackColor="Transparent" BorderStyle="None" ReadOnly="true" TabIndex="-1"></cc1:CommonText>
                            </td>
                            <td  style="background-color:#E4E8EA;border-right: black 2px solid; width:28px;"><asp:Label ID="Label1" runat="server" Text="�O�N"></asp:Label>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_1" PageReadOnly="true" Width="40px" Font-Size="8pt" Value='<%#Eval("eigyou_jittuseki_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_2" PageReadOnly="true" Width="40px" Font-Size="8pt" Value='<%#Eval("tokuhan_jittuseki_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_3" PageReadOnly="true" Width="40px" Font-Size="8pt" Value='<%#Eval("FC_jittuseki_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki1_4" Width="41px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_jittuseki_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_1" PageReadOnly="true" Width="72px" Font-Size="8pt" Value='<%#Eval("eigyou_jittuseki_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_2" PageReadOnly="true" Width="72px" Font-Size="8pt" Value='<%#Eval("tokuhan_jittuseki_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_3" PageReadOnly="true" Width="72px" Font-Size="8pt" Value='<%#Eval("FC_jittuseki_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki2_4" Width="76px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_jittuseki_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_1" PageReadOnly="true" Width="68px" Font-Size="8pt" Value='<%#Eval("eigyou_jittuseki_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_2" PageReadOnly="true" Width="68px" Font-Size="8pt" Value='<%#Eval("tokuhan_jittuseki_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_3" PageReadOnly="true" Width="68px" Font-Size="8pt" Value='<%#Eval("FC_jittuseki_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki3_4" Width="72px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_jittuseki_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                        </tr>
                        <tr  id="keikakuRows">
                            <td style="background-color:#D2FEFF;border-bottom: black 2px solid;border-top: black 1px solid;border-right: black 2px solid;"><asp:Label ID="Label2" runat="server" Text="�v��"></asp:Label>
                            </td>
                            <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_1" MaxLength="12" Width="40px" Font-Size="8pt" Value='<%#Eval("eigyou_keikaku_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_2" MaxLength="12" Width="40px" Font-Size="8pt" Value='<%#Eval("tokuhan_keikaku_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_3" MaxLength="12" Width="40px" Font-Size="8pt" Value='<%#Eval("FC_keikaku_kensuu")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku1_4" Width="41px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_keikaku_kensuu")%>'  runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_1" MaxLength="12" Width="72px" Font-Size="8pt" Value='<%#Eval("eigyou_keikaku_uri_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_2" MaxLength="12" Width="72px" Font-Size="8pt" Value='<%#Eval("tokuhan_keikaku_uri_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_3" MaxLength="12" Width="72px" Font-Size="8pt" Value='<%#Eval("FC_keikaku_uri_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku2_4" Width="76px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_keikaku_uri_kingaku")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_1" MaxLength="12" Width="68px" Font-Size="8pt" Value='<%#Eval("eigyou_keikaku_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_2" MaxLength="12" Width="68px" Font-Size="8pt" Value='<%#Eval("tokuhan_keikaku_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_3" MaxLength="12" Width="68px" Font-Size="8pt" Value='<%#Eval("FC_keikaku_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku3_4" Width="72px" Font-Size="8pt" PageReadOnly="true" Value='<%#Eval("Sum_keikaku_arari")%>' runat="server"></cc1:CommonNumber>
                            </td>
                        </tr>
                </ItemTemplate>
            </asp:DataList>
        </div> 
     </td></tr> 
    </table>
    <table id="tbSum" runat="server" cellpadding="0" cellspacing="0" width="963px" style="border-top: black 2px solid;" >
        <tr style="border-top: black 2px solid;">
            <td rowspan="2" style="width:89px;background-color:#FFC991;border-right: black 2px solid; border-left: black 2px solid; border-bottom: black 2px solid;">
                <cc1:CommonText ID="CommonText2" Width="83px" Text='���v' runat="server" BackColor="Transparent" BorderStyle="None" ReadOnly="true" TabIndex="-1"></cc1:CommonText>
            </td>
            <td style="background-color:#E4E8EA;border-right: black 2px solid;width:29px;"><asp:Label ID="Label1" runat="server" Text="�O�N"></asp:Label>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_1_Sum" PageReadOnly="true" Width="41px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_2_Sum" PageReadOnly="true" Width="41px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki1_3_Sum" PageReadOnly="true" Width="40px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki1_4_Sum" Width="42px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_1_Sum" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_2_Sum" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki2_3_Sum" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki2_4_Sum" Width="76px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_1_Sum" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_2_Sum" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;"><cc1:CommonNumber ID="numJittuseki3_3_Sum" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
            <td style="background-color:#E4E8EA;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numJittuseki3_4_Sum" Width="72px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
        </tr>
        <tr  id="keikakuRows">
            <td style="background-color:#D2FEFF;border-bottom: black 2px solid;border-top: black 1px solid;border-right: black 2px solid;"><asp:Label ID="Label2" runat="server" Text="�v��"></asp:Label>
            </td>
            <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_1_Sum" MaxLength="12" PageReadOnly="true" Width="41px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_2_Sum" MaxLength="12" PageReadOnly="true" Width="41px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku1_3_Sum" MaxLength="12" PageReadOnly="true" Width="40px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku1_4_Sum" Width="42px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_1_Sum" MaxLength="12" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_2_Sum" MaxLength="12" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku2_3_Sum" MaxLength="12" PageReadOnly="true" Width="72px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku2_4_Sum" Width="76px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_1_Sum" MaxLength="12" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_2_Sum" MaxLength="12" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;"><cc1:CommonNumber ID="numKeikaku3_3_Sum" MaxLength="12" PageReadOnly="true" Width="68px" Font-Size="8pt" runat="server">0</cc1:CommonNumber>
            </td>
             <td style="background-color:#E4E8EA;border-bottom: black 2px solid;border-top: black 1px solid;border-left: black 2px solid;border-right: black 2px solid;"><cc1:CommonNumber ID="numKeikaku3_4_Sum" Width="72px" Font-Size="8pt" PageReadOnly="true" Text="0" runat="server"></cc1:CommonNumber>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width:720px"></td>
            <td>
                <uc1:CommonButton ID="btnSitenbetuSave" ButtonKegen="SitenbetuNenKeikakuKengen" Text="�v��l�ۑ�" runat="server" /></td>
            <td>
                <uc1:CommonButton ID="btnSitenbetuConfirm" ButtonKegen="SitenbetuNenKeikakuKengen" Text="�x�X�ʔN�x�v��l�m��" BackColor="#FF00FE" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

