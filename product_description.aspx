<%@ Page Title="JEWELRY WHOLE SALE CLUB" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="product_description.aspx.cs" Inherits="product_description" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js" type="text/javascript"></script>
    <link href="popup/css/prettyPhoto.css" rel="stylesheet" type="text/css" />
    <script src="popup/js/jquery.prettyPhoto.js" type="text/javascript"></script>
        
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
       
      <link href="css/styles2.css" rel="stylesheet" />
    <link href="css/cloud-zoom.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <td width="700" align="right" valign="top">
            <table width="715" border="0" cellspacing="0" cellpadding="0">
                <!--hot itmes -->
                <tr>
                    <td class="td2">
                        <table width="915" border="0" cellspacing="0" cellpadding="0" class="border2">
                            <tr>
                                <td align="left" class="td4" valign="top">
                                    <table width="785" border="0" cellspacing="0" cellpadding="0" height="60">
                                        <tr>
                                            <td width="200" align="left">
                                                <div class="navigation" style="float: left;">
                                                    &nbsp;&nbsp;&nbsp;<strong style="font-size: 12px;float:left;margin-right:-20px;">Home > New Arrivals</strong></div>
                                            </td>
                                            <td align="right" width="185">
                                            </td>
                                            <td width="200" align="right">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <div style="height: 1px; width: 680px; margin: 10 5 0 0">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td4" style="background-color: #ededed">
                                    <table width="785" border="0" cellspacing="0" cellpadding="0" bgcolor="#EDEDED">
                                        <tr>
                                            <td class="td5" width="320">
                                                <ul class="gallery clearfix">
                                                    <li><a id="Main_Imga" runat="server" href="" rel="prettyPhoto" title="">
                                                        <img id="Main_Img" runat="server" src="" width="320" height="240" style="margin-right: 38px;"
                                                            alt="JEWELRY WHOLE SALE CLUB" /></a></li>
                                                </ul>
                                                <%--  <asp:Image ID="Main_Img" runat="server" Width="320" Height="240" Style="cursor: pointer;" />--%><br />
                                            </td>
                                            <td valign="top" class="td4" bgcolor="#EDEDED">
                                                <table width="410" border="0" cellspacing="0" cellpadding="3">
                                                    <tr>
                                                        <td class="product-title" style="text-transform: uppercase;" colspan="2">
                                                        <br />
                                                            <asp:Label ID="p_name" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="5" bgcolor="#CCCCCC" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100" style="font-size: 12px; color: #646464;">
                                                            <strong>Item Code</strong>
                                                        </td>
                                                        <td width="210" style="font-size: 12px; color: #646464;">
                                                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100" style="font-size: 12px; color: #646464;">
                                                            <strong>Price</strong>
                                                        </td>
                                                        <td width="210" style="font-size: 12px; color: #646464;">
                                                            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100" style="font-size: 12px; color: #646464;">
                                                            <strong>Qty</strong>
                                                        </td>
                                                        <td width="210">
                                                            <asp:DropDownList ID="drpquantity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpquantity_SelectedIndexChanged">
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 12px; color: #646464;">
                                                            <strong>Total</strong>
                                                        </td>
                                                        <td style="font-size: 12px; color: #646464;">
                                                            $<span id="total">
                                                                <asp:Label ID="total_price" runat="server" Text="Label"></asp:Label></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="5" bgcolor="#CCCCCC" colspan="2">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="50" width="300" colspan="2">
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="image/button/selected-items.png"
                                                                Width="150" Height="29" OnClick="ImageButton2_Click" />
                                                            <a href="Default.aspx">
                                                                <img src="image/button/continue.png" alt="Continue Shopping" width="150" height="29" /></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td4" align="right">
                                    <div style="height: 1px; width: 680px; border-top: 1px solid #CCCCCC; margin: 10 5 0 0">
                                    </div>
                                </td>
                            </tr>
                            <!--related products -->
                            <tr>
                                <td class="td5" align="right">
                                    <table width="885" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <img src="image/button/related-product.gif" alt="Related Product">
                                            </td>
                                            <td>
                                                <asp:DataList ID="dlCountry" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"
                                    Width="100%">
                                    <ItemTemplate>
                                        <table width="165" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td valign="top">
                                                    <div id="featured">
                                                        <div class="thumbnails">
                                                            <li class="span3">
                                                                <div class="thumbnail">
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Eval("code") %>'
                                                                        OnClick="onclick_image1">
                                                                        <asp:Image ID="ImageButton1" runat="server" ImageUrl='<%#Eval("Imagename") %>' Width="180px"
                                                                            Height="210" Style="border: ridge  1px #CACACA; border-width: 6px;" /></asp:LinkButton>
                                                                </div>
                                                            </li>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <script type="text/javascript" charset="utf-8">
            $(document).ready(function () {
                $("area[rel^='prettyPhoto']").prettyPhoto();

                $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'light_square', slideshow: 3000, autoplay_slideshow: true });
                $(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'fast', slideshow: 10000, hideflash: true });

                $("#custom_content a[rel^='prettyPhoto']:first").prettyPhoto({
                    custom_markup: '<div id="map_canvas" style="width:260px; height:265px"></div>',
                    changepicturecallback: function () { initialize(); }
                });

                $("#custom_content a[rel^='prettyPhoto']:last").prettyPhoto({
                    custom_markup: '<div id="bsap_1259344" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div><div id="bsap_1237859" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6" style="height:260px"></div><div id="bsap_1251710" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div>',
                    changepicturecallback: function () { _bsap.exec(); }
                });
            });
        </script>
        <script src="js/jquery.js"></script>
        <script src="js/cloud-zoom.1.0.2.js"></script>
</asp:Content>
