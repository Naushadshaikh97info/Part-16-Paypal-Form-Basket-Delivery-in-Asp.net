<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="BasketDelivery.aspx.cs" Inherits="BasketDelivery" Title="JEWELRY WHOLESALE CLUB" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .textboxbas
        {
            width: 170px;
            height: 23px;
            background-color: #fff;
        }
        .gridViewRow
        {
            padding-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 900px;">
        <table>
            <tr>
                <td>
                    <h2>
                        Shopping Cart</h2>
                </td>
                <td style="width: 150px">
                </td>
                <td align="right">
                    <h3>
                        Minimum Order : $50</h3>
                </td>
                <td style="width: 90px">
                </td>
                <td align="right">
                    <asp:Button ID="Button2" runat="server" Text="CONTINUE SHOPPING" PostBackUrl="~/Default.aspx"
                        BackColor="White" BorderColor="#FF9E81" BorderWidth="1px" ForeColor="#FE3C00"
                        Font-Bold="true" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <b>Your cart will automatically save the items you have selected for 3 days only.</b>
                </td>
            </tr>
        </table>
        <asp:GridView ID="grd_shoping" runat="server" DataKeyNames="productcode" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="10" Width="750px" OnRowDeleting="grd_shoping_RowDeleting"
            OnRowDataBound="grd_shoping_RowDataBound" CellPadding="5" BorderStyle="NotSet">
            <Columns>
                <asp:TemplateField HeaderText="Photos">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtn" runat="server" ImageUrl='<%#Eval("productimg") %>' Height="80"
                            Width="80" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="productname" HeaderText="Product Name" />
                <asp:BoundField DataField="actualprice" HeaderText="Actual Price" />
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="drpquantity" runat="server" OnTextChanged="drpquantity_SelectedIndexChanged1"
                            AutoPostBack="true" Width="50px"></asp:TextBox>
                        <%--<asp:TextBox ID="TextBox8" runat="server" AutoPostBack="true" OnTextChanged="drpquantity_SelectedIndexChanged"></asp:TextBox>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Price">
                    <ItemTemplate>
                        <asp:Label ID="lbl_total_price" runat="server" Text='<%#Eval("totalprice") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotalPrice" runat="server" />
                    </FooterTemplate>
                    <%--<asp:BoundField DataField="totalprice" HeaderText="Total Price" />--%>
                </asp:TemplateField>
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" ButtonType="Link" ControlStyle-Font-Bold="true"
                    ControlStyle-ForeColor="#84D8D1" />
                <%--  <asp:CommandField HeaderText="Delete" ButtonType="Image" ShowDeleteButton="true"
                                    DeleteImageUrl="~/images/delete_icon.gif" />--%>
            </Columns>
            <HeaderStyle BackColor="#eeeeee" Font-Bold="True" ForeColor="#64647b" />
            <RowStyle HorizontalAlign="Center" Height="100px" Width="80px" CssClass="gridViewRow" />
            <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
        </asp:GridView>
        <asp:Panel ID="panMain" runat="server">
            <table style="float: right; margin-right: 150px;">
                <tr>
                    <td>
                        <b>Total Ammount $</b>
                    </td>
                    <td>
                        <asp:Label ID="lbltotal_amt" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btncontinue" runat="server" Text="CONTINUE SHOPPING" OnClick="btncontinue_Click"
                            BackColor="White" BorderColor="#FF9E81" BorderWidth="1px" ForeColor="#FE3C00"
                            Font-Bold="true" />
                    </td>
                    <td>
                        <asp:Button ID="btncancel" runat="server" Text="CANCEL ORDER" OnClick="btncancel_Click"
                            BackColor="White" BorderColor="#FF9E81" BorderWidth="1px" ForeColor="#FE3C00"
                            Font-Bold="true" />
                    </td>
                    <td valign="middle">
                        <asp:Button ID="btncheckout" runat="server" Text="CHECK OUT" Style="float: right"
                            OnClick="btncheckout_Click" BackColor="White" BorderColor="#FF9E81" BorderWidth="1px"
                            ForeColor="#FE3C00" Font-Bold="true" />
                    </td>
                </tr>
            </table>
            <div style="font-family: Arial">
                <table width="100%">
                    <tr>
                        <td style="float: left; border: 1px solid #ccc; padding-left: 25px; background-color: #f1f1f1">
                            <asp:Panel ID="panregisterform" runat="server" Visible="false">
                                <div>
                                    <table cellpadding="3" cellspacing="3" style="padding: 10px 10px 10px 50px">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <b>Shipping Information</b>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Your Name"
                                                    ControlToValidate="txtname" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle">
                                                <b>Address:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" CssClass="textboxbas"
                                                    Height="60px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Your Shipping Address"
                                                    ControlToValidate="txtaddress" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Country:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcountry" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Your Contry"
                                                    ControlToValidate="txtcountry" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>State:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtstate" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Your State"
                                                    ControlToValidate="txtstate" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>City:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtcity" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Your City"
                                                    ControlToValidate="txtcity" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Pin Code:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpincode" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Your Pincode"
                                                    ControlToValidate="txtpincode" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>EmailId:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Your Email id"
                                                    ControlToValidate="txtemail" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Email In Proper Format"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="a1"
                                                    ControlToValidate="txtemail">*</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Mobile No:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" CssClass="textboxbas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Your Mobile"
                                                    ControlToValidate="txtmobile" ValidationGroup="a1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a1"
                                                    ShowSummary="false" ShowMessageBox="true" />
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btn_from_1_next" runat="server" Text="Next" ValidationGroup="a1"
                                                    OnClick="btn_from_1_next_click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </td>
                        <td style="float: right">
                            <asp:Panel ID="panloginform" runat="server" Visible="false">
                                <table border="0" style="border: 0px solid #ccc; background-image: url(images/logionbg.png)"
                                    width="450px" height="225px">
                                    <tr>
                                        <td style="float: left; border: 1px solid #ccc; padding-left: 25px; background-color: #f1f1f1">
                                            <div>
                                                <table cellpadding="3" cellspacing="3" style="padding: 0px 10px 10px 50px">
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <b>Billing Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>
                                                                <asp:Label ID="Label3" runat="server" Text="Your Balance:" Visible="false"></asp:Label></b>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Your Name:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Your Name"
                                                                ControlToValidate="txtname" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="middle">
                                                            <b>Address:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" CssClass="textboxbas"
                                                                Height="60px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Enter Your Billing Address"
                                                                ControlToValidate="txtaddress" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Country:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Your Country"
                                                                ControlToValidate="txtcountry" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>State:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox4" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Enter Your state"
                                                                ControlToValidate="txtstate" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>City:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox5" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter Your City"
                                                                ControlToValidate="txtcity" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Pin Code:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox6" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Enter Your Pincode"
                                                                ControlToValidate="txtpincode" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>EmailId:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox7" runat="server" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Enter Your Email"
                                                                ControlToValidate="txtemail" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter Your Email In Proper Format"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail"
                                                                ValidationGroup="bbb"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Mobile No:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBox9" runat="server" MaxLength="10" CssClass="textboxbas"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter Your Mobile No"
                                                                ControlToValidate="txtmobile" ValidationGroup="bbb">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Enter Captcha"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox class="tb7" ID="TextBox10" runat="server" Width="120px" OnClick="btn_submit_Click"
                                                                CssClass="textboxbas"></asp:TextBox>
                                                            <cc2:CaptchaControl ID="ccjoin" runat="server" CaptchaBackgroundNoise="Extreme" CaptchaLength="5"
                                                                CaptchaHeight="40" CaptchaWidth="140" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                                                CaptchaMaxTimeout="240" Width="140px" BorderColor="#ccc" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="45px" Font-Size="10pt" CaptchaChars="1234567890abcdefghijklmnopqrstuvwxyz"
                                                                Font-Overline="true" FontColor="black" Font-Italic="true" CaptchaFont="MS Reference Sans Serif"
                                                                BackColor="WhiteSmoke" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="bbb"
                                                                ShowMessageBox="true" ShowSummary="false" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button ID="Button1" runat="server" Text="REGISTER" ValidationGroup="bbb" OnClick="btnpay_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pancontinue" runat="server" Visible="false">
                    <asp:Button ID="btncontinue_pay" runat="server" Text="CONTINUTE PAY" OnClick="btncontinue_pay_Click" />
                </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
