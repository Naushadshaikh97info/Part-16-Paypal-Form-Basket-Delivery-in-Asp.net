<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="BasketDelivery.aspx.cs"
    Inherits="BasketDelivery" Title="HNCPL" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .input-text1
        {
            background: #fcfcfc;
            border: 1px solid #d4d4d4;
            padding: 6px;
            width: 250px;
            margin: 10px;
        }
        .button1
        {
            float: left;
            height: 33px;
            padding: 0 18px;
            font: bold 14px/33px Arial, Helvetica, sans-serif;
            text-align: center;
            white-space: nowrap;
            color: #fff;
            text-transform: uppercase;
            border-radius: 5px;
            background: url(skin/frontend/default/theme170k/images/button.png);
            border: 0 none;
            color: #E4EE4B;
            cursor: pointer;
        }
        .aaa
        {
            height:30px;
        }
         .aaa1
        {
            padding:7px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
       
        <asp:GridView ID="grd_shoping" runat="server" DataKeyNames="productcode" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" Width="100%" OnRowDeleting="grd_shoping_RowDeleting"
            OnRowDataBound="grd_shoping_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Photos" HeaderStyle-CssClass="aaa1" >
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtn" runat="server" ImageUrl='<%#Eval("productimg") %>' Height="80"
                            Width="80" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="productname" HeaderText="Product Name" HeaderStyle-CssClass="aaa1"/>
               
                <asp:BoundField DataField="discountprice" HeaderText="Discount Price" HeaderStyle-CssClass="aaa1"/>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="aaa1">
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
                <asp:BoundField DataField="totalprice" HeaderText="Total Price" HeaderStyle-CssClass="aaa1"/>
                <%--<asp:CommandField HeaderText="delete" ShowDeleteButton="true" ButtonType="Link" />--%>
                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="aaa1">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to remove this product from cart?')"
                            OnClick="lnkdelete_Click" CommandArgument='<%#Bind("productcode") %>'>Delete</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--  <asp:CommandField HeaderText="Delete" ButtonType="Image" ShowDeleteButton="true"
                                    DeleteImageUrl="~/images/delete_icon.gif" />--%>
            </Columns>
            <HeaderStyle BackColor="#5A7A36" Font-Bold="True" ForeColor="White" Font-Size="Medium"
            CssClass="aaa" />
        </asp:GridView>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panMain" runat="server">
                    <table width="100%">
                        <tr>
                            <td width="200px">
                                <asp:Button ID="btncontinue" runat="server" Text="CONTINUE SHOPPING" OnClick="btncontinue_Click"
                                    CssClass="button1" />
                            </td>
                            <td>
                                <asp:Button ID="btncancel" runat="server" Text="CANCEL ORDER" OnClick="btncancel_Click"
                                    CssClass="button1" />
                            </td>
                            <td colspan="2">
                                <asp:Label ID="Label4" runat="server" Text="Total Amount" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lbltotal_amt" runat="server" Text="" Font-Bold="true"></asp:Label>
                                <asp:Button ID="btncheckout" runat="server" Text="CHECK OUT" Style="float: right"
                                    CssClass="button1" OnClick="btncheckout_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                                *Delivery charges [India]* INR0 (Not applicable for HNCPL Members) *Cash on Delivery
                                Charges extra * INR 50 *Cash on Delivery only in India and below INR 10000 /-
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panform" runat="server" Visible="false">
                        <div>
                            <table width="100%" style="margin:5px;">
                                <tr>
                                    <td style="float: left; border: 1px solid #ccc; padding-left: 25px; background-color: #f1f1f1">
                                        <asp:Panel ID="panregisterform" runat="server" Visible="false">
                                            <div>
                                                <table>
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>Shipping Address</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>
                                                                <asp:Label ID="Label3" runat="server" Text="Select Type:" Visible="false"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="drptype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drptype_SelectedIndexChanged"
                                                                Visible="false" CssClass="input-text1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                                <asp:ListItem>Ewallet</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>
                                                                <asp:Label ID="Label2" runat="server" Text="Your Balance:" Visible="false"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblewallbal" runat="server" Text="" Font-Bold="true" Font-Size="Medium"
                                                                Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>Your Name:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtname" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtname" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <br />
                                                            <b>Address:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="input-text1"
                                                                Height="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtaddress" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>Country:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcountry" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtcountry" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>State:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtstate" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtstate" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>City:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcity" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtcity" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>Pin Code:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpincode" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="txtpincode_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtpincode">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtpincode" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>EmailId:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtemail" runat="server" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtemail" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fill In Proper Way"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:Label ID="Label1" runat="server" Text="Password:" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpassword1" runat="server" TextMode="Password" CssClass="input-text1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtpassword1" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <b>Mobile No:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" CssClass="input-text1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="txtmobile_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" FilterType="Numbers" TargetControlID="txtmobile">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                                ControlToValidate="txtmobile" ValidationGroup="a"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <br />
                                                            <asp:Label ID="lbl_Captcha" runat="server" Text="Enter Captcha" Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <div style="width: 140px; height: 40px; border: 1px solid #ccc; background-color: #f1f1f1">
                                                                <div style="padding: 11px 0px 10px 0px; font-size: 18px; text-align: center">
                                                                    2 + 5 + 7 ??
                                                                </div>
                                                            </div>
                                                            <asp:TextBox class="tb7" ID="txtcapcha" runat="server" TabIndex="13" OnClick="btn_submit_Click"
                                                                CssClass="input-text1"></asp:TextBox>
                                                            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender6" runat="server" TargetControlID="txtcapcha"
                                                                WatermarkText="Type Answer">
                                                            </cc1:TextBoxWatermarkExtender>
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <%--<cc2:CaptchaControl ID="ccjoin" runat="server" CaptchaBackgroundNoise="Extreme" CaptchaLength="5"
                                                                        CaptchaHeight="40" CaptchaWidth="140" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                                                        CaptchaMaxTimeout="240" Width="140px" BorderColor="#ccc" BorderStyle="Solid"
                                                                        BorderWidth="1px" Height="45px" Font-Size="10pt" CaptchaChars="1234567890abcdefghijklmnopqrstuvwxyz"
                                                                        Font-Overline="true" FontColor="black" Font-Italic="true" CaptchaFont="MS Reference Sans Serif"
                                                                        BackColor="WhiteSmoke" />--%>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <asp:Button ID="btnpay" runat="server" Text="REGISTER" ValidationGroup="a" OnClick="btnpay_Click"
                                                                CssClass="button1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                    <td style="float: right">
                                        <asp:Panel ID="panloginform" runat="server" Visible="false" DefaultButton="Button1">
                                            <table border="0" cellpadding="7" cellspacing="7">
                                                <tr>
                                                    <td colspan="2" style="padding-left: 180px">
                                                        <img src="images/memberlogin.png" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <b>User Name:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtadmin" runat="server" CssClass="input-text1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtadmin" ValidationGroup="Button1"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <b>Password:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="input-text1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtpassword" ValidationGroup="Button1"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <br />
                                                        <a href="ForgotPassword.aspx">Forgot Password</a>
                                                    </td>
                                                    <td style="padding-left:50px">

                                                        <asp:Button ID="Button1" runat="server" CssClass="button1" OnClick="Button1_Click"
                                                            Text="Submit" ValidationGroup="Button1" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pancontinue" runat="server" Visible="false">
                        <asp:Button ID="btncontinue_pay" runat="server" Text="CONTINUTE PAY" OnClick="btncontinue_pay_Click"
                            CssClass="button1" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
