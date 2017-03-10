<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTHLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTHLOBody" runat="Server">
    <!-- banner -->
    <div class="banner">
        <div class="w3layouts-banner-slider">
            <div class="container">
                <div class="slider">
                    <div class="callbacks_container">
                        <ul class="rslides callbacks callbacks1" id="slider4">
                            <li>
                                <div class="agileits-banner-info">
                                    <h3>Lorem ipsum dolor sit amet <span>Nulla massa magna</span></h3>
                                    <div class="w3-button">
                                        <a href="single.html">More</a>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="agileits-banner-info">
                                    <h3>Donec posuere cursus nibh<span>Ut molestie lectus</span></h3>
                                    <div class="w3-button">
                                        <a href="single.html">More</a>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <script src="js/responsiveslides.min.js"></script>
                    <script>
						// You can also use "$(window).load(function() {"
						$(function () {
						  // Slideshow 4
						  $("#slider4").responsiveSlides({
							auto: true,
							pager:true,
							nav:true,
							speed: 500,
							namespace: "callbacks",
							before: function () {
							  $('.events').append("<li>before event fired.</li>");
							},
							after: function () {
							  $('.events').append("<li>after event fired.</li>");
							}
						  });
					
						});
                    </script>
                    <!--banner Slider starts Here-->
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
                                <img src="images/2.jpg" alt="" />
                                <figcaption>
                                    <h4>Proin nulla</h4>
                                    <p>Chico's main fear was missing the morning bus.</p>
                                </figcaption>
                            </figure>
                        </div>
                        <div class="col-md-6 agileits-left">
                            <figure class="effect-chico">
                                <img src="images/3.jpg" alt=" " />
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
                    <h4>Donec posuere cursus nibh. Mauris a sollicitudin metus. Mauris porttitor, dui maximus viverra bibendum</h4>
                    <p>Nulla massa magna, luctus at justo et, euismod convallis mauris. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Mauris rutrum fringilla fermentum.<span>Donec tincidunt, eros quis consectetur maximus, nibh justo pretium diam, id vehicula lacus nunc eu orci. Aliquam molestie, nibh vitae pharetra semper, nunc nisi laoreet diam, et placerat nulla quam non leo. </span></p>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- banner-bottom -->
    <!-- services -->
    <div class="services">
        <div class="container">
            <div class="w3ls-heading">
                <h3>Services</h3>
            </div>
            <div class="wthree-services-grids">
                <div class="col-md-6 w3ls-about-left">
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-cog" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Suspendisse consectetur dapibus volutpat.</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-heart" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Lorem ipsum dolor sit amet</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-paper-plane" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Suspendisse consectetur dapibus volutpat.</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="col-md-6 w3ls-about-left">
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-user" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Suspendisse consectetur dapibus volutpat.</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-thumbs-up" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Lorem ipsum dolor sit amet</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="agileits-icon-grid">
                        <div class="icon-left hvr-radial-out">
                            <i class="fa fa-bell" aria-hidden="true"></i>
                        </div>
                        <div class="icon-right">
                            <h5>Suspendisse consectetur dapibus volutpat.</h5>
                            <p>Phasellus dapibus felis elit, sed accumsan arcu gravida vitae. Nullam aliquam erat at lectus ullamcorper, nec interdum neque hendrerit.</p>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //services -->
    <!-- news -->
    <div class="news">
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
                        <a href="single.html">
                            <img src="images/4.jpg" alt="" /></a>
                        <h4><a href="single.html">Pellentesque habitant morbi tristique senectus et netus et malesuada fames</a></h4>
                        <p>Donec justo ante, maximus et aliquam et, elementum ac purus. Nam eget ante ac velit laoreet sodales ut vel nibh. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sed quam nibh. In sed nisi enim. Duis eget nunc vitae elit dignissim feugiat. Morbi molestie fringilla ipsum, id molestie dolor sodales vel.</p>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-6 news-right">
                    <div class="news-right-grid">
                        <a href="single.html">Nunc non neque ex. Aliquam neque massa</a>
                        <h5>06th Nov,2016</h5>
                        <p>Ut rutrum convallis erat at malesuada. Donec facilisis sem nisl, ut rhoncus ex ullamcorper ornare. Ut ac interdum velit. Cras nulla nibh, rutrum non venenatis sit amet, ullamcorper venenatis felis.</p>
                    </div>
                    <div class="news-right-grid">
                        <a href="single.html">Phasellus sapien eros, condimentum quis</a>
                        <h5>17th Nov,2016</h5>
                        <p>Ut rutrum convallis erat at malesuada. Donec facilisis sem nisl, ut rhoncus ex ullamcorper ornare. Ut ac interdum velit. Cras nulla nibh, rutrum non venenatis sit amet, ullamcorper venenatis felis.</p>
                    </div>
                    <div class="news-right-grid">
                        <a href="single.html">Donec justo ante, maximus et aliquam et</a>
                        <h5>29th Nov,2016</h5>
                        <p>
                            Ut rutrum convallis erat at malesuada.
                                <wbr>
                            Donec facilisis sem nisl, ut rhoncus ex ullamcorper ornare. Ut ac interdum velit. Cras nulla nibh, rutrum non venenatis sit amet, ullamcorper venenatis felis.
                        </p>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //news -->
    <!-- footer -->
    <footer>
        <div class="container">
            <div class="agileits-w3layouts-footer-grids">
                <div class="col-md-4 footer-left">
                    <h3>
                        <a href="index.html">Cat <span>Club</span></a>
                    </h3>
                    <div class="footer-social-grids">
                        <ul>
                            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                            <li><a href="#"><i class="fa fa-rss"></i></a></li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-4 footer-left">
                    <h4>Navigation</h4>
                    <div class="col-md-6 footer-grid-left">
                        <ul>
                            <li><a class="active" href="index.html">Home</a></li>
                            <li><a href="about.html">About</a></li>
                            <li><a href="gallery.html">Gallery</a></li>
                            <li><a href="icons.html">Icons</a></li>
                        </ul>
                    </div>
                    <div class="col-md-6 footer-grid-left">
                        <ul>
                            <li><a href="typography.html">Typography</a></li>
                            <li><a href="blog.html">Blog</a></li>
                            <li><a href="contact.html">Contact</a></li>
                        </ul>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="col-md-4 footer-left">
                    <h4>Newsletter</h4>
                    <p>Subscribe With Us</p>
                    <div action="#" method="post">
                        <input type="email" placeholder="Subscribe" name="Subscribe" required="">
                        <button class="btn1">Go</button>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </footer>
    <!-- footer -->
    <!-- copyright -->
    <div class="copyright">
        <div class="container">
            <p>© 2017 TheLittleOnes. All rights reserved | Design by <a href="http://w3layouts.com">W3layouts</a></p>
        </div>
    </div>
    <!-- //copyright -->
  
</asp:Content>

