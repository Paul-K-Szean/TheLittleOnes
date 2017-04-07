<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminAccount.aspx.cs" Inherits="AdminAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeaderMasterAdmin" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBodyMasterAdmin" runat="Server">
    <div class="breadcrumbs ace-save-state" id="breadcrumbs">
        <ul class="breadcrumb">
            <li>
                <i class="ace-icon fa fa-home home-icon"></i>
                <a href="AdminDashboard.aspx">Home</a>
            </li>
            <li>
                <a href="#">Account Info</a>
            </li>
            <li class="active">Settings</li>
        </ul>
        <!-- /.breadcrumb -->
        <div class="nav-search" id="nav-search">
            <div class="form-search">
                <span class="input-icon">
                    <input type="text" placeholder="Search ..." class="nav-search-input" id="nav-search-input" autocomplete="off" />
                    <i class="ace-icon fa fa-search nav-search-icon"></i>
                </span>
            </div>
        </div>
        <!-- /.nav-search -->
    </div>
    <div class="page-content">
        <div class="ace-settings-container" id="ace-settings-container">
            <div class="btn btn-app btn-xs btn-warning ace-settings-btn" id="ace-settings-btn">
                <i class="ace-icon fa fa-cog bigger-130"></i>
            </div>
            <div class="ace-settings-box clearfix" id="ace-settings-box">
                <div class="pull-left width-50">
                    <div class="ace-settings-item">
                        <div class="pull-left">
                            <select id="skin-colorpicker" class="hide">
                                <option data-skin="no-skin" value="#438EB9">#438EB9</option>
                                <option data-skin="skin-1" value="#222A2D">#222A2D</option>
                                <option data-skin="skin-2" value="#C6487E">#C6487E</option>
                                <option data-skin="skin-3" value="#D0D0D0">#D0D0D0</option>
                            </select>
                        </div>
                        <span>&nbsp; Choose Skin</span>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-navbar" autocomplete="off" />
                        <label class="lbl" for="ace-settings-navbar">Fixed Navbar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-sidebar" autocomplete="off" />
                        <label class="lbl" for="ace-settings-sidebar">Fixed Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-breadcrumbs" autocomplete="off" />
                        <label class="lbl" for="ace-settings-breadcrumbs">Fixed Breadcrumbs</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl" autocomplete="off" />
                        <label class="lbl" for="ace-settings-rtl">Right To Left (rtl)</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-add-container" autocomplete="off" />
                        <label class="lbl" for="ace-settings-add-container">
                            Inside
											<b>.container</b>
                        </label>
                    </div>
                </div>
                <!-- /.pull-left -->
                <div class="pull-left width-50">
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-hover" autocomplete="off" />
                        <label class="lbl" for="ace-settings-hover">Submenu on Hover</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-compact" autocomplete="off" />
                        <label class="lbl" for="ace-settings-compact">Compact Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-highlight" autocomplete="off" />
                        <label class="lbl" for="ace-settings-highlight">Alt. Active Item</label>
                    </div>
                </div>
                <!-- /.pull-left -->
            </div>
            <!-- /.ace-settings-box -->
        </div>
        <!-- /.ace-settings-container -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="page-header">
                    <h1>Settings
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    Edit Account Information
                                </small>
                    </h1>
                </div>
                <!-- /.page-header -->
                <div class="row">
                    <div class="col-xs-12 ">
                        <div class="form-inline pull-right">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="BTNSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="BTNSave_Click" />
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <div class="space-6"></div>
                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->
                        <div class="form-horizontal">
                            <div class="row">
                                <%--basic info--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Basic Info</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLAccountID" runat="server" Text="Account ID" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBAccountID" runat="server" CssClass="form-control " placeholder="Account ID" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLEmail" runat="server" Text="Email" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control " placeholder="Email" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAccountType" runat="server" Text="Account Type" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBAccountType" runat="server" CssClass="form-control " placeholder="Account Type" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPasswordOld" runat="server" Text="Old Password" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPasswordOld" runat="server" CssClass="form-control" placeholder="Old Password" type="password"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPasswordNew" runat="server" Text="New Password" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPasswordNew" runat="server" CssClass="form-control" placeholder="New Password" type="password"></asp:TextBox>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--shop info--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Shop Info</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLShopInfoID" runat="server" Text="ShopInfo S/N" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopInfoID" runat="server" CssClass="form-control " placeholder="Shopinfo ID" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLShopInfoName" runat="server" Text="Name" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopInfoName" runat="server" CssClass="form-control " placeholder="Name" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLShopInfoContact" runat="server" Text="Contact" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopInfoContact" runat="server" CssClass="form-control " placeholder="Contact" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLShopInfoAddress" runat="server" Text="Address" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopInfoAddress" runat="server" CssClass="form-control" placeholder="Address" disabled="disabled"></asp:TextBox>
                                                </div>
                                                <br />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- PAGE CONTENT ENDS -->
                        </div>
                        <!-- /.col -->
                    </div>
                </div>
                <!-- /.row -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- /.page-content -->
</asp:Content>
