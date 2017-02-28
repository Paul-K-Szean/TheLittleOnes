<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminProfile.aspx.cs" Inherits="AdminProfile" %>

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
                <a href="#">Profile Info</a>
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
                                    Edit Profile Information
                                </small>
                    </h1>
                </div>
                <!-- /.page-header -->
                <div class="row">
                    <div class="col-xs-12 ">
                        <div class="form-inline pull-right">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text=""></asp:Label>
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
                                                    <asp:Label ID="LBLProfileID" runat="server" Text="Profile ID" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBProfileID" runat="server" CssClass="form-control " placeholder="Profile ID" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLName" runat="server" Text="Name" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBName" runat="server" CssClass="form-control " placeholder="Name"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLContact" runat="server" Text="Contact" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBContact" runat="server" CssClass="form-control " placeholder="Contact" MaxLength="8"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAddress" runat="server" Text="Address" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                                </div>
                                                <br />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--photo--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Current Photo(s)</h4>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">

                                                <hr />
                                                <div class="center">
                                                    <div>
                                                        <div class="overflow-auto scrollbarHorizontal" style="max-width: 1110px;">
                                                            <asp:DataList ID="DLPhotoUploaded" runat="server" DataSourceID="SDSPhoto" Width="100%">
                                                                <ItemTemplate>
                                                                    <img src="<%# "uploadedFiles/database/profileinfo/" + Eval("ProfileID").ToString().ToLower().Replace(" ", "") + "_"+Eval("ProfileName").ToString().ToLower().Replace(" ", "") + "/" + Eval("PhotoName")  %>"
                                                                        style="max-height: 200px; margin: 0px 4px">
                                                                    <hr />
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                        <div class="space-4"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="space-6"></div>
                                </div>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Photos</h4>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="inline" AllowMultiple="true" />
                                                    <asp:Button ID="BTNPreview" runat="server" CssClass="btn btn-primary btn-xs pull-right" Text="Preview"
                                                        OnClick="BTNPreview_Click" />
                                                </div>
                                                <hr />
                                                <div id="photoPreview" runat="server" class="center overflow-scroll">
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
                <asp:SqlDataSource ID="SDSPhoto" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath, Profile.accountID, Profile.profileID, Profile.profileName, Profile.profileContact, Profile.profileAddress FROM (Profile INNER JOIN Photo ON Photo.photoOwnerID = Profile.profileID) WHERE (Photo.photoOwnerID = ?)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TBProfileID" Name="photoOwnerID" PropertyName="Text" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BTNPreview" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    <!-- /.page-content -->
</asp:Content>

