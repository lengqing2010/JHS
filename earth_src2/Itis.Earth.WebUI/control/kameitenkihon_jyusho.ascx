<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kameitenkihon_jyusho.ascx.vb" Inherits="Itis.Earth.WebUI.kameitenkihon_jyusho_user" %>
<%@ Register Src="common_drop.ascx" TagName="common_drop" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
// <!CDATA[


function chkNengetu(obj){
if (obj.value==""){return true;}


    var checkFlg = true;
    
    

 
    obj.value = obj.value.Trim();
    var val = obj.value;
    
    val = SetDateNoSign(val,"/");
    val = SetDateNoSign(val,"-");
        
    val = val+'01';

  if(val == "")return;  //��̏ꍇ�A�I��
  
  val = removeSlash(val); //�X���b�V������
  val = val.replace(/\-/g, "");  //�n�C�t�����폜

  if(val.length == 6){  //6���̏ꍇ
    if(val.substring(0, 2) > 70){
      val = "19" + val;
    }else{
      val = "20" + val;
    }
  }else if(val.length == 4){  //4���̏ꍇ
    dd = new Date();
    year = dd.getFullYear();
    val = year + val;
  }
  
  if(val.length != 8){
    checkFlg = false; //���̎��_��8���Ŗ������NG
  }else{  //8���̏ꍇ
    val = addSlash(val);  //�X���b�V���t�^
    var arrD = val.split("/");
    if(checkDateVali(arrD[0],arrD[1],arrD[2]) == false){
      checkFlg = false; //���t�Ó����`�F�b�NNG
    }
  }
  
  if(!checkFlg){
    event.returnValue = false;
    obj.select();
    alert("���t�ȊO�����͂���Ă��܂��B");
    return false;
  }else{
    obj.value = val.substring(0,7);
  }

}


function chkN(value){
  //���l�ȊO�̏ꍇ�A�A���[�g��\��
  if(isNaN(value)){
    return false;
  }
  return true;
}


// ]]>
</script>


    

<table id="Table4" cellpadding="1" class="mainTable" style="margin-top: 10px; width: 916px;
    text-align: left" readonly="readonly">
    <thead>
        <tr>
            <th class="tableTitle" colspan="10" rowspan="1" style="text-align: left">
                <a id="titleText5" runat="server">�Z�����</a>&nbsp; &nbsp;<span id="titleInfobar5" runat="server"
                    style="display: none"></span>
                <asp:Button ID="btnTouroku" runat="server" Text="�o�^"   />
            </th>
        </tr>
    </thead>

    <tbody id="meisaiTbody5" runat="server" >

                                        <tr>
                                            <td class="koumokuMei" style="width: 120px">
                                            �o�^�N��</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxAddNengetu" runat="server" Style="width: 72px" CssClass = "codeNumber" MaxLength="7"></asp:TextBox></td>
                                            <td colspan="4" style="width: 486px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px" >
                                                �X�֔ԍ�</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxYuubinNo1" runat="server" Style="width: 72px" CssClass = "codeNumber"></asp:TextBox>
                                                <asp:Button ID="btnKensaku" runat="server" Text="����" OnClientClick = "" /></td>
                                            <td   class="hissu" style="width: 120px; font-weight:bold;">
                                                �Z���P</td>
                                            <td class="hissu" colspan="3" style="width: 605px;">
                                                <asp:TextBox ID="tbxJyuusyo1" runat="server" Style="width: 296px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px"  >
                                                ���ݒn����</td>
                                            <td colspan="3" style="width: 340px;">
                                                <uc1:common_drop ID="ddlTodoufuken" runat="server" GetStyle="todoufuken" />
                                            </td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                �Z���Q</td>
                                            <td colspan="3" >
                                                <asp:TextBox ID="tbxJyuusyo2" runat="server" Style="width: 296px"  CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="koumokuMei" style="width: 120px"  >
                                                ������</td>
                                            <td colspan="3" style="width: 340px;">
                                                <asp:TextBox ID="tbxBusyoMei1" runat="server" Style="width: 296px"  CssClass = "input"></asp:TextBox></td>
                                            <td  class="koumokuMei" style="width: 120px" >
                                                ��\�Җ�</td>
                                            <td colspan="3" style="width: 360px; ">
                                                <asp:TextBox ID="tbxDaihyousyaMei1" runat="server" Style="width: 144px"  CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                �d�b�ԍ�</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxTelNo1" runat="server" Style="width: 145px" CssClass = "codeNumber"></asp:TextBox></td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                �e�`�]�ԍ�</td>
                                            <td >
                                                <asp:TextBox ID="tbxFaxNo1" runat="server" Style="width: 100px" CssClass = "codeNumber"></asp:TextBox></td>
                                            <td style="width: 120px" class="koumokuMei" >
                                                �\���S����</td>
                                            <td>
                                                <asp:TextBox ID="tbxMailAddress" runat="server" Style="width: 144px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                ���l�P</td>
                                            <td colspan="3" style="width: 340px">
                                                <asp:TextBox ID="tbxBikou11" runat="server" Style="width: 200px" CssClass = "input"></asp:TextBox></td>
                                            <td style="WIDTH: 120px" class="koumokuMei" >
                                                ���l�Q</td>
                                            <td  colspan="3" style="width: 355px">
                                                <asp:TextBox ID="tbxBikou21" runat="server" Style="width: 280px" CssClass = "input"></asp:TextBox></td>
                                        </tr>
           
    </tbody>

</table>
    <asp:HiddenField ID="hidUpdTime" runat="server" />

