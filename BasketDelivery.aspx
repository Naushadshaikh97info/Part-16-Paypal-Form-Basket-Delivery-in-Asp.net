<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BasketDelivery.aspx.cs"
    Inherits="BasketDelivery" Title="BasketDelivery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="js/highslide-with-gallery.js" type="text/javascript"></script>
    <link href="css/highslide.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        hs.graphicsDir = 'images/graphics/';
        hs.align = 'center';
        hs.transitions = ['expand', 'crossfade'];
        hs.wrapperClassName = 'dark borderless floating-caption';
        hs.fadeInOut = true;
        hs.dimmingOpacity = .75;
        if (hs.addSlideshow) hs.addSlideshow({
            interval: 5000,
            repeat: false,
            useControls: true,
            fixedControls: 'fit',
            overlayOptions: {
                opacity: .6,
                position: 'bottom center',
                hideOnMouseOut: true
            }
        });
    </script>
    <style type="text/css">
        .textboxbas
        {
            width: 170px;
            height: 23px;
            background-color: #fff;
            border: 1px solid #ccc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="productHolder">
        <div class="productHolder1">
            <asp:GridView ID="grd_shoping" runat="server" DataKeyNames="productcode" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" Width="100%" OnRowDeleting="grd_shoping_RowDeleting"
                OnRowDataBound="grd_shoping_RowDataBound" CssClass ="bas">
                <Columns>
                    <asp:TemplateField HeaderText="Photos">
                        <ItemTemplate>
                            &nbsp;<asp:ImageButton ID="imgbtn" runat="server" ImageUrl='<%#Eval("productimg") %>' Height="100"
                                Width="100" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="productname" HeaderText="Product Name" />
                    <asp:BoundField DataField="actualprice" HeaderText="Actual Price" />
                 
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpquantity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpquantity_SelectedIndexChanged">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="totalprice" HeaderText="Total Price" />
                    <%--<asp:CommandField HeaderText="delete" ShowDeleteButton="true" ButtonType="Link" />--%>
                      <asp:CommandField HeaderText="Delete" ButtonType="Image" ShowDeleteButton="true"
                                    DeleteImageUrl="~/admin/icon/byz_sys_closewindow.png"/>
                </Columns>
                <HeaderStyle BackColor="#6EB89F" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="panMain" runat="server">
                        <table style="float: right">
                            <%-- <tr>
                            <td>
                                <b>Total Quantity</b>
                            </td>
                            <td>
                                <asp:Label ID="lbltotal_qty" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>--%>
                            <tr>
                                <td>
                                    <b>Total Ammount</b>
                                </td>
                                <td>
                                   <asp:Label ID="lbltotal_amt" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style ="background-color:#ccc">
                            <tr>
                                <td>
                                    <asp:Button ID="btncontinue" runat="server" Text="CONTINUE SHOPPING" Style="margin-top: 30px;"
                                        OnClick="btncontinue_Click" class="btn btn-primary" />
                                    <asp:Button ID="btncancel" runat="server" Text="CANCEL ORDER" Style="margin-top: 30px;"
                                        OnClick="btncancel_Click" class="btn btn-primary" />
                                </td>
                                <td>
                                    <p align="justify" style="font-family: Arial">
                                        *Delivery charges [India]* INR0 (Not applicable for Club99 Members) *Cash on Delivery<br />
                                        charges extra * INR 50 *Cash on Delivery only in India and below INR 10000 /-
                                    </p>
                                </td>
                                <td valign="middle">
                                    <asp:Button ID="btncheckout" runat="server" Text="CHECK OUT" Style="float: right;
                                        margin-top: 30px;" class="btn btn-primary" OnClick="btncheckout_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="Panform" runat="server" Visible="false">
                            <div style="font-family: Arial">
                                <table width="100%">
                                    <tr>
                                        <td style="float: left; border: 1px solid #ccc; padding-left: 25px; background-color: #f1f1f1">
                                            <asp:Panel ID="panregisterform" runat="server" Visible="false">
                                                <div>
                                                    <table cellpadding="3" cellspacing="3" style="padding: 10px 10px 10px 50px">
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <b>Shipping Address</b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>
                                                                    <asp:Label ID="Label2" runat="server" Text="Your Balance:" Visible="false"></asp:Label></b>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblewallbal" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Your Name:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtname" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="middle">
                                                                <b>Address:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="textboxbas"
                                                                    Height="60px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtaddress" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Country:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcountry" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtcountry" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>State:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtstate" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtstate" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>City:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtcity" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtcity" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Pin Code:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtpincode" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txtpincode_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtpincode">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtpincode" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>EmailId:</b>&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtemail" runat="server" CssClass="textboxbas" TextMode ="Email"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtemail" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                               
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td>
                                                                <b>Mobile No:</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" CssClass="textboxbas"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="txtmobile_FilteredTextBoxExtender" runat="server"
                                                                    Enabled="True" FilterType="Numbers" TargetControlID="txtmobile">
                                                                </cc1:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txtmobile" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="btnpay" runat="server" Text="REGISTER"  ValidationGroup="a" class="btn btn-primary"
                                                                    OnClick="btnpay_Click1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                        <td style="float: right">
                                            <asp:Panel ID="panloginform" runat="server" Visible="false">
                                                <table border="0" style="border: 0px solid #ccc; background-image: url(images/logionbg.png)"
                                                    width="200px" height="225px">
                                                    <tr>
                                                        <td valign="top" style="padding-top: 15px">
                                                            <table border="0" cellpadding="7" cellspacing="7">
                                                                <tr>
                                                                    <td colspan="2" style="padding-left: 70px">
                                                                        <strong>Payment Method</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:RadioButtonList ID="radaccount" runat="server" RepeatDirection="Vertical">
                                                                            <asp:ListItem>Online Payment </asp:ListItem>
                                                                            <asp:ListItem>Offline Payment </asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <%-- <tr>
                                                                    <td>
                                                                        <b>User Name:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtadmin" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                                            ControlToValidate="txtadmin" ValidationGroup="Button1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <b>Password:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                                            ControlToValidate="txtpassword" ValidationGroup="Button1"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" class="btn btn-primary"
                                                                            ValidationGroup="Button1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pancontinue" runat="server" Visible="false" BorderWidth="1px" BorderColor="Black">

                            <table style="margin-left:500px;" class ="bas" >
                                <tr>
                                    <td>
                                       <h1> <strong>Payment Method</strong></h1>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButtonList ID="rdo_payment" runat="server" RepeatDirection="Horizontal" Width="300px">
                                            <asp:ListItem>Online Payment</asp:ListItem>
                                            <asp:ListItem>Offline Payment</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btncontinue_pay" runat="server" Text="CONTINUTE PAY" class="btn btn-primary"
                                            OnClick="btncontinue_pay_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
