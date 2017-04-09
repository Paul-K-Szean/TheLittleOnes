<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <!-- banner -->
    <div class="banner">
        <div class="w3layouts-banner-slider">
            <div class="container">
                <div class="slider">
                    <div class="callbacks_container">
                        <ul class="rslides callbacks callbacks1" id="slider4">
                            <li>
                                <div class="agileits-banner-info">
                                    <h3>Osteoporosis <span> Their bones can get brittle too!</span></h3>
                                    <div class="w3-button">
                                        <a href="single.html">More</a>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="agileits-banner-info">
                                    <h3>Cataract <span> Their eye can get weaken too!</span></h3>
                                    <div class="w3-button">
                                        <a href="single.html">More</a>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="agileits-banner-info">
                                    <h3>Allergies <span>Their skin can get weaken too!</span></h3>
                                    <div class="w3-button">
                                        <a href="#.html">More</a>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- //banner -->
    <!-- banner-bottom -->
    <div class="welcome">
        <div class="container">
            <div class="w3ls-heading">
                <h2>Welcome To TheLittleOnes</h2>
            </div>
            <div class="welcome-grids">
                <div class="col-md-6 agile-welcome-grid">
                    <div class="grid">
                        <div class="col-md-6 agileits-left">
                            <figure class="effect-chico">
                                <img src="assetsTheLittleOnes/images/2.jpg" alt="" />
                                <figcaption>
                                    <h4>Proin nulla</h4>
                                    <p>Chico's main fear was missing the morning bus.</p>
                                </figcaption>
                            </figure>
                        </div>
                        <div class="col-md-6 agileits-left">
                            <figure class="effect-chico">
                                <img src="assetsTheLittleOnes/images/3.jpg" alt=" " />
                                <figcaption>
                                    <h4>Nam ornare</h4>
                                    <p>Chico's main fear was missing the morning bus.</p>
                                </figcaption>
                            </figure>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="col-md-6 agileinfo-welcome-right">
                    <h4>G5 Organisation</h4>
                    <p>We are here to help, assist and guide novice pet owners. Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah Blah</p>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- banner-bottom -->
    <!-- news -->
    <div id="newsevent"class="news">
        <div class="container">
            <div class="w3ls-heading">
                <h3>News & Events</h3>
            </div>
            <div class="w3-agileits-news-grids">
                <div class="col-md-6 news-left">
                    <div class="w3-agile-news-date">
                        <div class="agile-news-icon">
                            <i class="fa fa-calendar" aria-hidden="true"></i>
                            <p>Nov 24</p>
                        </div>
                        <div class="agileits-line"></div>
                        <div class="agile-news-icon">
                            <a href="#"><i class="fa fa-comments-o" aria-hidden="true"></i></a>
                            <p>2 comments</p>
                        </div>
                        <div class="agileits-line"></div>
                        <div class="agile-news-icon">
                            <a href="#"><i class="fa fa-thumbs-o-up" aria-hidden="true"></i></a>
                            <p>3482</p>
                        </div>
                    </div>
                    <div class="w3-agile-news-img">
                        <a href="#">
                            <img src="assetsTheLittleOnes/images/4.jpg" alt="" /></a>
                        <h4><a href="single.html">Birthday Party for Luna</a></h4>
                        <h5>Saturday, 02th April,2017</h5>
                        <p>Come over to Paya Lebar Park to celebrate my dearest Luna's birthday! It is his 5th birthday celeration.</p>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-6 news-right">
                    <asp:SqlDataSource ID="SDSEvents" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>" 
                        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>" 
                        SelectCommand="SELECT * FROM [Eventt] WHERE ([eventStatus] = ?) ORDER BY eventDateTime">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="Confirmed" Name="eventStatus" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DataList ID="DLEvents" runat="server" DataKeyField="eventID" DataSourceID="SDSEvents" Width="100%">
                        <ItemTemplate>
                            <div class="news-right-grid">
                                <a href="single.html">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("eventTitle") %>' /></a>
                                <h5>
                                    <asp:Label ID="eventDateTimeLabel" runat="server" Text='<%# DateTime.Parse(Eval("eventDateTime").ToString()).ToString("dddd, dd MMMM yyyy, HH:mm tt") %>' /></h5>
                                <p>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("eventDesc") %>' /></p>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //news -->
    
</asp:Content>
