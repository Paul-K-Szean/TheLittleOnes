<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminShopInfoAdd.aspx.cs" Inherits="AdminShopInfoAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeaderMasterAdmin" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBodyMasterAdmin" runat="Server">
    <div class="breadcrumbs ace-save-state" id="breadcrumbs">
        <ul class="breadcrumb">
            <li>
                <i class="ace-icon fa fa-home home-icon"></i>
                <a href="Dashboard.aspx">Home</a>
            </li>

            <li>
                <a href="#">Shop Info</a>
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
                                    New Shop Information
                                </small>
                    </h1>
                </div>
                <!-- /.page-header -->
                <div class="row">
                    <div class="col-xs-12 ">
                        <div class="form-inline pull-right">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="BTNAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" OnClick="BTNAdd_Click" />
                            <asp:Button ID="BTNGenerate" runat="server" CssClass="btn btn-primary btn-sm" Text="Generate" OnClick="BTNGenerate_Click" />
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
                                                    <asp:Label ID="LBLShopName" runat="server" Text="Shop Name" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopName" runat="server" CssClass="form-control " placeholder="Shop Name"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLShopContact" runat="server" Text="Shop Contact" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopContact" runat="server" CssClass="form-control " placeholder="Shop Contact" MaxLength="8"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAddress" runat="server" Text="Address" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopAddress" runat="server" CssClass="form-control col-xs-10" placeholder="Address"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:CheckBox ID="CHKBXGroomingService" runat="server" CssClass="checkbox checkbox-inline" Text="Grooming Service" Checked="true" />
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label8" runat="server" Text="Shop Type" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLShopType" runat="server" CssClass="form-control" OnSelectedIndexChanged="DDLShopType_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="">Select Shop Type</asp:ListItem>
                                                        <asp:ListItem Value="Pet Shop">Pet Shop</asp:ListItem>
                                                        <asp:ListItem Value="Pet Clinic">Pet Clinic</asp:ListItem>
                                                        <asp:ListItem Value="Pet Shelter">Pet Shelter</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div class="clearfix">
                                                    <asp:Label ID="LBLShopDesc" runat="server" Text="Description" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBShopDesc" runat="server" CssClass="form-control col-xs-10" placeholder="Description" TextMode="MultiLine" Rows="15"></asp:TextBox>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <%--operating Hours--%>
                                <div class="col-md-5">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Operating Hours</h4>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <asp:Label ID="Label1" runat="server" Text="Operating Hours" Font-Bold="True"></asp:Label>

                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseMonday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLMonday" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Monday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeMonday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeMonday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseTuesday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLTuesday" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Tuesday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeTuesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeTuesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseWednesday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLWednesday" runat="server" CssClass="col-xs-2 control-label no-padding-right" Text="Wednesday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeWednesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeWednesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseThursday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="Label5" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Thursday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeThursday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeThursday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseFriday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLFriday" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Friday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeFriday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeFriday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKXBXCloseSaturday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLSaturday" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Saturday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeSaturday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeSaturday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CHKBXCloseSunday" runat="server" CssClass=" checkbox checkbox-inline " Text="Close" />
                                                    <asp:Label ID="LBLSunday" runat="server" CssClass="col-xs-2  control-label no-padding-right" Text="Sunday @"></asp:Label>
                                                    <div class="col-sm-4">
                                                        <asp:DropDownList ID="DDLOpenTimeSunday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Open Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        to
                                                        <asp:DropDownList ID="DDLCloseTimeSunday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:CheckBox ID="CHKBXCloseOnPublicHoliday" runat="server" CssClass="checkbox checkbox-inline" Text="Close on Public Holiday" Checked="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--photo--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Photos</h4>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="inline" AllowMultiple="true" />
                                                    <asp:Button ID="BTNPreview" runat="server" CssClass="btn btn-primary btn-xs pull-right" Text="Preview" OnClick="BTNPreview_Click" />
                                                </div>
                                                <hr />
                                                <div id="photoPreview" runat="server" class="center overflow-scroll">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="space-6"></div>
                    </div>
                    <!-- PAGE CONTENT ENDS -->
                </div>
                <!-- /.col -->
                </div>
    <!-- /.row -->

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BTNPreview" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    <!-- /.page-content -->
</asp:Content>

