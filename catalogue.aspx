<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="catalogue.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="css/reset.css" type="text/css" media="screen">
    <link rel="stylesheet" href="css/style.css" type="text/css" media="screen">
    <link rel="stylesheet" href="css/layout.css" type="text/css" media="screen">
    <link rel="stylesheet" href="css/prettyPhoto.css" type="text/css" media="screen">
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="js/cufon-yui.js" type="text/javascript"></script>
    <script src="js/cufon-replace.js" type="text/javascript"></script>
    <script src="js/Dynalight_400.font.js" type="text/javascript"></script>
    <script src="js/FF-cash.js" type="text/javascript"></script>
    <script src="js/jquery.prettyPhoto.js" type="text/javascript"></script>
    <script src="js/hover-image.js" type="text/javascript"></script>
    <script src="js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script src="js/jquery.bxSlider.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#slider-2').bxSlider({
                pager: true,
                controls: false,
                moveSlideQty: 1,
                displaySlideQty: 4
            });
            $("a[data-gal^='prettyPhoto']").prettyPhoto({
                theme: 'facebook'
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <body id="page3">
        <!--==============================header=================================-->
    
        <!--==============================content================================-->
        <section id="content">
            <div class="main">
                <div class="container">
                    <h3 class="prev-indent-bot">Catalogue</h3>
                    <asp:DataList ID="DataList1" runat="server" RepeatColumns="5" RepeatDirection="Horizontal" Width="250">
                        <ItemTemplate>
                            <div id="slider" style="min-height: 300px; height: 300px;">
                                <div>
                                    <div class="p4">
                                        <figure>
                                            <a class="lightbox-image" href='<%#Eval("image") %>' data-gal="prettyPhoto[prettyPhoto]" >
                                                <img src='<%#Eval("image") %>' alt="" height="100" width="150"></a>

                                        </figure>
                                        <h5>Nam liber tempor cusoluta </h5>
                                        <p class="p1">
                                            <%#Eval("discription") %>
                                        </p>
                                        <p class="p2"><strong class="color-2">Price: <%#Eval("price") %></strong></p>
                                        <asp:Button ID="Button1" runat="server" Text="Add to cart" class="button-2" CommandArgument='<%#Eval("code") %>' OnClick="Button1_Click"/>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
        </section>
</asp:Content>

