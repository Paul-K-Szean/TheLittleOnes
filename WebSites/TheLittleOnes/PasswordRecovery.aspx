<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TheLittleOnes</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta charset="utf-8" />
    <meta name="keywords" content="" />
    <script type="application/x-javascript"> 
        addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false); function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- bootstrap-css -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" media="all" />
    <!-- css -->
    <link href="assetsTheLittleOnes/css/style.css" rel="stylesheet" type="text/css" />
    <!-- font-awesome icons -->
    <link href="assetsTheLittleOnes/css/font-awesome.css" rel="stylesheet" type="text/css">
    <%--lightbox--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/lightbox2/2.9.0/css/lightbox.css" rel="stylesheet" type="text/css" />
    <%--responsive slides--%>
    <link href="assetsTheLittleOnes/css/responsiveslides.css" rel="stylesheet" />
    <%--jQuery UI--%>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" rel="stylesheet">
    <%--bootstrap datepicker--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.min.css" rel="stylesheet">
    <%--Group G5 Style--%>
    <link href="assetsG5/G5Style.css" rel="stylesheet" />

    <!-- font -->
    <link href="https://fonts.googleapis.com/css?family=Raleway:100,100i,200,200i,300,300i,400,400i,500,500i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Roboto+Condensed:400,700italic,700,400italic,300italic,300" rel="stylesheet" type="text/css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.4.1/jquery.easing.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/lightbox2/2.9.0/js/lightbox.min.js"></script>
    <script src="assetsTheLittleOnes/js/move-top.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.min.js"></script>
    <%--SOURCE: https://github.com/viljamis/ResponsiveSlides.js/blob/master/responsiveslides.min.js--%>
    <script src="assetsTheLittleOnes/js/responsiveslides.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/smooth-scroll/10.2.1/js/smooth-scroll.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div class="overlay-progress ">
                    <img src="assetsG5/images/loading-squares.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12 text-center">
                        <div class="page-header">
                            <h1>Password Recovery
                            </h1>
                        </div>
                        <!-- /.page-header -->
                    </div>
                </div>
                <!-- /.row -->

                <div class="space-6"></div>
                <div class="row">
                    <%--account info--%>
                    <div class="col-md-4 col-md-offset-4">
                        <div class="widget-box">
                            <div class="widget-header">
                                <h4 class="widget-title">Account Info</h4>
                            </div>
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div>
                                        <asp:Label ID="LBLAccountEmail" runat="server" Text="Email (Login ID)" Font-Bold="True" ForeColor="#666666"></asp:Label>
                                        <asp:TextBox ID="TBAccountEmail" runat="server" CssClass="form-control" placeholder="EG: SeanJeanDean09@hotmail.com"></asp:TextBox>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <div class="space-6"></div>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="form-inline text-center">
                            <asp:Button ID="BTNRecover" runat="server" CssClass="btn btn-primary btn-md" Text="Recover" OnClick="BTNRecover_Click" />
                        </div>
                        <br />
                        <div class="form-inline text-center">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text=" " Font-Size="Medium"></asp:Label>
                        </div>
                        <br />
                        <div class="form-inline text-center">
                            <a href="Home.aspx">Back to Login</a>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
