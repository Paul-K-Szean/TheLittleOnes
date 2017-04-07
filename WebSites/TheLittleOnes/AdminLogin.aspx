<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" EnableEventValidation="false" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TheLittleOnesAdmin - Login</title>
    <meta name="description" content="overview &amp; stats" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />
    <!-- bootstrap & fontawesome -->
    <link rel="stylesheet" href="assetsAdmin/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assetsAdmin/font-awesome/4.5.0/css/font-awesome.min.css" />
    <!-- page specific plugin styles -->
    <!-- text fonts -->
    <link rel="stylesheet" href="assetsAdmin/css/fonts.googleapis.com.css" />
    <!-- ace styles -->
    <link rel="stylesheet" href="assetsAdmin/css/ace.min.css" />
    <link rel="stylesheet" href="assetsAdmin/css/ace-skins.min.css" />
    <link rel="stylesheet" href="assetsAdmin/css/ace-rtl.min.css" />
</head>
<body class="login-layout">
    <%--basic scripts--%>
    <script src="assetsAdmin/js/jquery-2.1.4.min.js"></script>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="main-container">
            <div class="main-content">
                <div class="row">
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="login-container marginTop-10">
                            <div class="center">
                                <h1>
                                    <i class="ace-icon fa fa-leaf green"></i>
                                    <a href="Home.aspx"><span class="white" id="id-text2">TheLittle<span class="red">Ones</span></span>
                                    </a>
                                </h1>
                                <h4 class="blue" id="id-company-text">&copy; G5</h4>
                            </div>
                            <div class="space-6"></div>
                            <div class="position-relative">
                                <div id="login-box" class="login-box visible widget-box no-border">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <h4 class="header blue lighter bigger">
                                                <i class="ace-icon fa fa-coffee green"></i>
                                                Please Enter Your Information
                                            </h4>
                                            <div class="space-6"></div>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <fieldset>
                                                            <label class="block clearfix">
                                                                <span class="block input-icon input-icon-right">
                                                                    <asp:TextBox ID="TBLoginEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                                                    <i class="ace-icon fa fa-user"></i>
                                                                </span>
                                                            </label>
                                                            <label class="block clearfix">
                                                                <span class="block input-icon input-icon-right">
                                                                    <asp:TextBox ID="TBLoginPassword" runat="server" CssClass="form-control" placeholder="password" type="password"></asp:TextBox>
                                                                    <i class="ace-icon fa fa-lock"></i>
                                                                </span>
                                                            </label>
                                                            <div class="space"></div>
                                                            <div class="clearfix">
                                                                <asp:Label ID="LBLErrorMsg" runat="server" Text=""></asp:Label>
                                                                <asp:Button ID="BTNLogin" runat="server" class="width-35 pull-right btn btn-sm btn-primary" Text="Login" OnClick="BTNLogin_Click" />
                                                            </div>
                                                            <div class="space-4"></div>
                                                        </fieldset>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- /.widget-main -->
                                    </div>
                                    <!-- /.widget-body -->
                                </div>
                                <!-- /.login-box -->
                            </div>
                            <!-- /.position-relative -->
                            <div class="navbar-fixed-top align-right">
                                <br />
                                &nbsp;
								<a id="btn-login-dark" href="#">Dark</a>
                                &nbsp;
								<span class="blue">/</span>
                                &nbsp;
								<a id="btn-login-blur" href="#">Blur</a>
                                &nbsp;
								<span class="blue">/</span>
                                &nbsp;
								<a id="btn-login-light" href="#">Light</a>
                                &nbsp; &nbsp; &nbsp;
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.main-content -->
        </div>
        <!-- /.main-container -->
    </form>
    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        if ('ontouchstart' in document.documentElement) document.write("<script src='assets/js/jquery.mobile.custom.min.js'>" + "<" + "/script>");
    </script>
    <script type="text/javascript">
        jQuery(function ($) {
            $(document).on('click', '.toolbar a[data-target]', function (e) {
                e.preventDefault();
                var target = $(this).data('target');
                $('.widget-box.visible').removeClass('visible');//hide others
                $(target).addClass('visible');//show target
            });
        });
        //you don't need this, just used for changing background
        jQuery(function ($) {
            $('#btn-login-dark').on('click', function (e) {
                $('body').attr('class', 'login-layout');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'blue');
                e.preventDefault();
            });
            $('#btn-login-light').on('click', function (e) {
                $('body').attr('class', 'login-layout light-login');
                $('#id-text2').attr('class', 'grey');
                $('#id-company-text').attr('class', 'blue');
                e.preventDefault();
            });
            $('#btn-login-blur').on('click', function (e) {
                $('body').attr('class', 'login-layout blur-login');
                $('#id-text2').attr('class', 'white');
                $('#id-company-text').attr('class', 'light-blue');
                e.preventDefault();
            });
        });
    </script>
</body>
</html>
