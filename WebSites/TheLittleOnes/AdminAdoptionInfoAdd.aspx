﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminAdoptionInfoAdd.aspx.cs" Inherits="AdminAdoptionInfoAdd" %>
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
                <a href="#">Adoption Info</a>
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
                                    New Adoption Information
                                </small>
                    </h1>
                </div>
                <!-- /.page-header -->
                <div class="row">
                    <div class="col-xs-12 ">
                        <div class="form-inline pull-right">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="BTNAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" OnClick="BTNAdd_Click" />
                            <asp:Button ID="BTNGenerate" runat="server" CssClass="btn btn-primary btn-sm" Text="Generate" OnClick="BTNGenerate_Click"  Visible="false"/>
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
                                <%--organisation--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Organisation</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="Label2" runat="server" Text="Organisation" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLOrangisation" runat="server" CssClass="form-control" DataSourceID="SDSShopInfo" DataTextField="shopInfoName" DataValueField="shopInfoID"
                                                        AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DDLOrangisation_SelectedIndexChanged">
                                                        <asp:ListItem Value="">Select Organisation</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SDSShopInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>" ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>" SelectCommand="SELECT * FROM [ShopInfo]"></asp:SqlDataSource>
                                                    <div class=" space-6"></div>
                                                    <asp:Panel ID="PNLShopInfoDetails" runat="server" Visible="false">
                                                        <div class="widget-box">
                                                            <div class="widget-header widget-header-flat widget-header-small">
                                                                <h5 class="widget-title">
                                                                    <asp:Label ID="LBLShopName" runat="server" Text=""></asp:Label>
                                                                </h5>
                                                            </div>
                                                            <div class="widget-body">
                                                                <div class="widget-main">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <p>
                                                                                <asp:Label ID="Label3" runat="server" Text="Contact : " Font-Bold="true"></asp:Label>
                                                                                <asp:Label ID="LBLShopInfoContact" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                            <p>
                                                                                <asp:Label ID="Label4" runat="server" Text="Address : " Font-Bold="true"></asp:Label>
                                                                                <asp:Label ID="LBLShopInfoAddress" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                            <p>
                                                                                <asp:Label ID="Label5" runat="server" Text="Description : " Font-Bold="true"></asp:Label>
                                                                                <asp:Label ID="LBLShopInfoGrooming" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                            <p>
                                                                                <asp:Label ID="LBLShopInfoType" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                            <p>
                                                                                <asp:Label ID="LBLShopInfoDesc" runat="server" Text=""></asp:Label>
                                                                            </p>
                                                                        </div>
                                                                    </div>
                                                                    <!-- /.row -->
                                                                </div>
                                                                <!-- /.widget-main -->
                                                            </div>
                                                            <!-- /.widget-body -->
                                                        </div>
                                                        <!-- /.widget-box -->
                                                    </asp:Panel>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--basic info--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Basic Info</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLPetBreed" runat="server" Text="Breed" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLPetBreed" runat="server" CssClass="form-control" DataSourceID="SDSBreed" DataTextField="petInfoBreed" DataValueField="petInfoBreed"
                                                        AppendDataBoundItems="True" AutoPostBack="True">
                                                        <asp:ListItem Value="">Select Breed</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SDSBreed" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>" ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>" SelectCommand="SELECT * FROM [PetInfo]"></asp:SqlDataSource>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetPetName" runat="server" Text="Pet Name" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPetName" runat="server" CssClass="form-control" placeholder="EG: Sunhine"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetGender" runat="server" Text="Gender" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLPetGender" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="">Select Gender</asp:ListItem>
                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetWeight" runat="server" Text="Weight (KG)" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPetWeight" runat="server" CssClass="form-control" placeholder="EG: 12"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label1" runat="server" Text="Size" Font-Bold="True"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="DDLPetSize" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="">Select Size</asp:ListItem>
                                                        <asp:ListItem Value="Small">Small</asp:ListItem>
                                                        <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                        <asp:ListItem Value="Large">Large</asp:ListItem>
                                                        <asp:ListItem Value="X-Large">X-Large</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div class="clearfix">
                                                    <asp:Label ID="LBLShopDesc" runat="server" Text="Description" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPetDesc" runat="server" CssClass="form-control col-xs-10" placeholder="Description" TextMode="MultiLine" Rows="15"></asp:TextBox>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--characteristic--%>
                                <div class="col-md-3">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Characteristics</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLPetEnergyLevel" runat="server" Text="Energy Level" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLPetEnergy" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="">Select Level</asp:ListItem>
                                                        <asp:ListItem Value="Low">Low</asp:ListItem>
                                                        <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                        <asp:ListItem Value="High">High</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetFriendlyWithPet" runat="server" Text="Friendly with pets?" Font-Bold="True"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="DDLPetFriendlyWithPet" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="">Select Yes/No</asp:ListItem>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetFriendlyWithPeople" runat="server" Text="Friendly with people?" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLPetFriendlyWithPeople" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="">Select Yes/No</asp:ListItem>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetToiletTrain" runat="server" Text="Toilet Trained?" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLPetToiletTrain" runat="server" CssClass="form-control" AutoPostBack="true">
                                                        <asp:ListItem Value="">Select Yes/No</asp:ListItem>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLPetHealthInfo" runat="server" Text="Health Info" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBPetHealthInfo" runat="server" CssClass="form-control" placeholder="EG: She is good and healthy" TextMode="MultiLine" Rows="15"></asp:TextBox>
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
                                            <h4 class="widget-title">Photos</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="inline" AllowMultiple="false" />
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
