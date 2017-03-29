<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs" Inherits="AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeaderMasterAdmin" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBodyMasterAdmin" runat="Server">
    <div class="breadcrumbs ace-save-state" id="breadcrumbs">
        <ul class="breadcrumb">
            <li>
                <i class="ace-icon fa fa-home home-icon"></i>
                <a href="#">Home</a>
            </li>
            <li class="active">Dashboard</li>
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
        <div class="page-header">
            <h1>Dashboard
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    overview &amp; stats
                                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <div class="row">
            <div class="col-xs-12">
                <!-- PAGE CONTENT BEGINS -->
                <div class="alert alert-block alert-success">
                    <button type="button" class="close" data-dismiss="alert">
                        <i class="ace-icon fa fa-times"></i>
                    </button>
                    <i class="ace-icon fa fa-check green"></i>
                    Welcome to
									<strong class="green">Ace
										<small>(v1.4)</small>
                                    </strong>,
	                    лёгкий, многофункциональный и простой в использовании шаблон для админки на bootstrap 3.3.6. Загрузить исходники с <a href="https://github.com/bopoda/ace">github</a> (with minified ace js/css files).
                </div>
                <%--PET INFO--%>
                <div class="row">
                    <div class="space-6"></div>
                    <div class="col-sm-12">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat">
                                <h4 class="widget-title lighter">
                                    <i class="ace-icon fa  fa-id-card-o"></i>
                                    Pet Info   
                                
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse">
                                        <i class="ace-icon fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-main padding-4">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-lg-7">
                                                <div class="table-header">
                                                    <asp:Label ID="LBLSearchResultPetInfo" runat="server" Text="Records for Pet Info"></asp:Label>
                                                    <asp:Label ID="LBLEntriesCountPetInfo" runat="server" CssClass="pull-right"></asp:Label>
                                                </div>
                                                <!-- div.table-responsive -->
                                                <div class="dataTables_wrapper form-inline no-footer">
                                                    <div class="row">
                                                        <div class="col-xs-6">
                                                            <div class="dataTables_length">
                                                                Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountPetInfo" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountPetInfo_SelectedIndexChanged">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                </asp:DropDownList>
                                                                records
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="dataTables_filter">
                                                                <span class="block input-icon input-icon-right  toolbar">
                                                                    <asp:DropDownList ID="DDLFilterBreed" runat="server" CssClass="" AutoPostBack="True"
                                                                        DataSourceID="SDSBreeds" DataTextField="petInfoBreed" DataValueField="petInfoBreed"
                                                                        OnSelectedIndexChanged="DDLFilterBreed_SelectedIndexChanged" AppendDataBoundItems="true">
                                                                        <asp:ListItem Value="">Filter Breed</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:SqlDataSource ID="SDSBreeds" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                                        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                                        SelectCommand="SELECT DISTINCT [petInfoBreed], [petInfoID] FROM [PetInfo]"></asp:SqlDataSource>
                                                                </span>
                                                                <div class="space-4"></div>
                                                                <div class="form-search">
                                                                    <span class=" input-icon input-icon-right">
                                                                        <asp:TextBox ID="TBSearchPetInfo" runat="server" placeholder="EG: silky terrier"
                                                                            AutoPostBack="true" OnTextChanged="TBSearchPetInfo_TextChanged"></asp:TextBox>
                                                                        <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:GridView ID="GVPetInfoOverview" runat="server"
                                                        OnDataBound="GVPetInfoOverview_DataBound"
                                                        OnSelectedIndexChanging="GVPetInfoOverview_SelectedIndexChanging"
                                                        OnSelectedIndexChanged="GVPetInfoOverview_SelectedIndexChanged"
                                                        OnPageIndexChanging="GVPetInfoOverview_PageIndexChanging"
                                                        EmptyDataText="No Data Found"
                                                        CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                                        AutoGenerateColumns="False" DataKeyNames="petInfoID" DataSourceID="SDSPetInfo" Width="100%"
                                                        AllowPaging="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="petInfoID" HeaderText="S/N" InsertVisible="False" ReadOnly="True" SortExpression="petInfoID" />
                                                            <asp:BoundField DataField="petInfoCategory" HeaderText="Category" SortExpression="petInfoCategory" />
                                                            <asp:BoundField DataField="petInfoBreed" HeaderText="Breed" SortExpression="petInfoBreed" />
                                                            <asp:BoundField DataField="petInfoLifeSpanMin" HeaderText="LifeSpanMin" SortExpression="petInfoLifeSpanMin" />
                                                            <asp:BoundField DataField="petInfoLifeSpanMax" HeaderText="LifeSpanMax" SortExpression="petInfoLifeSpanMax" />
                                                            <asp:BoundField DataField="petInfoHeightMin" HeaderText="HeightMin" SortExpression="petInfoHeightMin" />
                                                            <asp:BoundField DataField="petInfoHeightMax" HeaderText="HeightMax" SortExpression="petInfoHeightMax" />
                                                            <asp:BoundField DataField="petInfoWeightMin" HeaderText="WeightMin" SortExpression="petInfoWeightMin" />
                                                            <asp:BoundField DataField="petInfoWeightMax" HeaderText="WeightMax" SortExpression="petInfoWeightMax" />
                                                            <asp:BoundField DataField="petInfoDisplayStatus" HeaderText="Status" SortExpression="petInfoDisplayStatus" />
                                                            <asp:CommandField ShowSelectButton="True" SelectText="View" />
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <h3>No Record(s) Found</h3>
                                                        </EmptyDataTemplate>
                                                        <PagerStyle CssClass="pagination-G5" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                                    </asp:GridView>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                            </div>
                                            <div class="col-lg-5">
                                                <asp:Panel ID="PNLPetInfoDetails" runat="server" Visible="false">
                                                    <div class="widget-box">
                                                        <div class="widget-header widget-header-flat widget-header-small">
                                                            <h5 class="widget-title">
                                                                <i class="ace-icon fa fa-signal"></i>
                                                                <asp:Label ID="LBLCategory" runat="server"></asp:Label>
                                                                - 
                                                                <asp:Label ID="LBLBreed" runat="server"></asp:Label>
                                                            </h5>
                                                            <div class="widget-toolbar no-border">
                                                                <div class="inline dropdown-hover">
                                                                    <button class="btn btn-minier btn-primary">
                                                                        This Week
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                        <li class="active">
                                                                            <a href="#" class="blue">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110">&nbsp;</i>
                                                                                This Week
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                Last Week
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                This Month
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                Last Month
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main">
                                                                <div class="row">
                                                                    <div class="col-md-7">
                                                                        <asp:Chart ID="chart1" runat="server"></asp:Chart>
                                                                    </div>
                                                                    <div class="col-md-5">
                                                                        <h4>Description</h4>
                                                                        <p>
                                                                            <asp:Label ID="LBLDesc" runat="server"></asp:Label>
                                                                        </p>
                                                                        <h4>Personality </h4>
                                                                        <p>
                                                                            <asp:Label ID="LBLPersonality" runat="server"></asp:Label>
                                                                        </p>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <!-- /.widget-main -->
                                                        </div>
                                                        <!-- /.widget-body -->
                                                    </div>
                                                    <!-- /.widget-box -->
                                                </asp:Panel>
                                            </div>
                                            <asp:SqlDataSource ID="SDSPetInfo" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                SelectCommand="SELECT * FROM [PetInfo] ORDER BY [petInfoCategory], [petInfoBreed]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SDSPetChar" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                SelectCommand="SELECT * FROM (PetInfo INNER JOIN PetCharacteristics ON PetInfo.petInfoID = PetCharacteristics.petInfoID) WHERE (PetInfo.petInfoID = ?)">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="GVPetInfoOverview" Name="petInfoID" PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- /.widget-main -->
                            </div>
                            <!-- /.widget-body -->
                        </div>
                        <!-- /.widget-box -->
                    </div>
                    <div class="vspace-12-sm"></div>
                </div>
                <!-- /.row -->
                <div class="hr hr32 hr-dotted"></div>
                <%--SHOP INFO--%>
                <div class="row">
                    <div class="space-6"></div>
                    <div class="col-sm-12">
                        <div class="widget-box transparent">
                            <div class="widget-header widget-header-flat">
                                <h4 class="widget-title lighter">
                                    <i class="ace-icon fa  fa-id-card-o"></i>
                                    Shop Info 
                                </h4>
                                <div class="widget-toolbar">
                                    <a href="#" data-action="collapse">
                                        <i class="ace-icon fa fa-chevron-up"></i>
                                    </a>
                                </div>
                            </div>
                            <div class="widget-body">
                                <div class="widget-main padding-4">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="col-lg-7">
                                                <div class="table-header">
                                                    <asp:Label ID="LBLSearchResultShopInfo" runat="server" Text="Records for Shop Info"></asp:Label>
                                                    <asp:Label ID="LBLEntriesCountShopInfo" runat="server" CssClass="pull-right"></asp:Label>
                                                </div>
                                                <!-- div.table-responsive -->
                                                <div class="dataTables_wrapper form-inline no-footer">
                                                    <div class="row">
                                                        <div class="col-xs-6">
                                                            <div class="dataTables_length">
                                                                Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountShopInfo" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountShopInfo_SelectedIndexChanged">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                </asp:DropDownList>
                                                                records
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div id="dynamic-table_filter" class="dataTables_filter">
                                                                <span class="block input-icon input-icon-right  toolbar">
                                                                    <asp:CheckBox ID="CHKBXFilterPetShop" runat="server" CssClass="checkbox-inline" Text="Shop"
                                                                        AutoPostBack="true" OnCheckedChanged="CHKBXFilterShop_CheckedChanged" />
                                                                    <asp:CheckBox ID="CHKBXFilterPetClinic" runat="server" CssClass="checkbox-inline" Text="Clinic"
                                                                        AutoPostBack="true" OnCheckedChanged="CHKBXFilterClinic_CheckedChanged" />
                                                                    <asp:CheckBox ID="CHKBXFilterGrooming" runat="server" CssClass="checkbox-inline" Text="Grooming"
                                                                        AutoPostBack="true" OnCheckedChanged="CHKBXFilterGrooming_CheckedChanged" />
                                                                </span>
                                                                <label class="block clearfix">
                                                                    <span class="block input-icon input-icon-right">Search:         
                                                                    <asp:TextBox ID="TBSearchShopInfo" runat="server" CssClass="form-control  input-sm" placeholder="EG: Shelter"
                                                                        AutoPostBack="true" OnTextChanged="TBSearchShopInfo_TextChanged" Text=""></asp:TextBox>
                                                                        <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                                    </span>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <asp:GridView ID="GVShopInfoOverview" runat="server" OnSelectedIndexChanging="GVShopInfoOverview_SelectedIndexChanging" OnSelectedIndexChanged="GVShopInfoOverview_SelectedIndexChanged"
                                                        CssClass="table table-striped table-bordered table-hover dataTable no-footer" OnDataBound="GVShopInfoOverview_DataBound"
                                                        AutoGenerateColumns="False" DataKeyNames="shopInfoID" DataSourceID="SDSShopInfo" Width="100%"
                                                        AllowPaging="True">
                                                        <Columns>
                                                            <asp:BoundField DataField="shopInfoID" HeaderText="S/N" InsertVisible="False" ReadOnly="True" SortExpression="shopInfoID" />
                                                            <asp:BoundField DataField="shopInfoName" HeaderText="Name" SortExpression="shopInfoName" />
                                                            <asp:BoundField DataField="shopInfoContact" HeaderText="Contact" SortExpression="shopInfoContact" HeaderStyle-Width="100" />
                                                            <asp:BoundField DataField="shopInfoAddress" HeaderText="Address" SortExpression="shopInfoAddress" />
                                                            <%--<asp:BoundField DataField="shopInfoGrooming" HeaderText="Grooming Service" SortExpression="shopInfoGrooming" />--%>
                                                            <asp:TemplateField HeaderText="Grooming Service" SortExpression="shopInfoGrooming">
                                                                <ItemTemplate><%# (bool.Parse(Eval("shopInfoGrooming").ToString())) ? "Yes" : "No" %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="shopInfoType" HeaderText="Type" SortExpression="shopInfoType" />
                                                            <%--<asp:BoundField DataField="shopInfoCloseOnPublicHoliday" HeaderText="Close On PH" SortExpression="shopInfoCloseOnPublicHoliday" />--%>
                                                            <asp:TemplateField HeaderText="Close on PH" SortExpression="shopInfoCloseOnPublicHoliday">
                                                                <ItemTemplate><%# (bool.Parse(Eval("shopInfoCloseOnPublicHoliday").ToString())) ? "Yes" : "No" %></ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowSelectButton="True" SelectText="View" />
                                                        </Columns>
                                                        <PagerStyle CssClass="pagination-G5" />
                                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                                    </asp:GridView>
                                                </div>
                                                <!-- div.dataTables_borderWrap -->
                                            </div>
                                            <div class="col-lg-5">
                                                <asp:Panel ID="PNLShopInfoDetails" runat="server" Visible="false">
                                                    <div class="widget-box">
                                                        <div class="widget-header widget-header-flat widget-header-small">
                                                            <h5 class="widget-title">
                                                                <i class="ace-icon fa fa-signal"></i>
                                                                Shop Information
                                                            </h5>
                                                            <div class="widget-toolbar no-border">
                                                                <div class="inline dropdown-hover">
                                                                    <button class="btn btn-minier btn-primary">
                                                                        This Week
															<i class="ace-icon fa fa-angle-down icon-on-right bigger-110"></i>
                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right dropdown-125 dropdown-lighter dropdown-close dropdown-caret">
                                                                        <li class="active">
                                                                            <a href="#" class="blue">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110">&nbsp;</i>
                                                                                This Week
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                Last Week
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                This Month
                                                                            </a>
                                                                        </li>
                                                                        <li>
                                                                            <a href="#">
                                                                                <i class="ace-icon fa fa-caret-right bigger-110 invisible">&nbsp;</i>
                                                                                Last Month
                                                                            </a>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="widget-body">
                                                            <div class="widget-main">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="center">
                                                                            <div>
                                                                                <div class="overflow-auto scrollbarHorizontal">
                                                                                    <asp:DataList ID="DLPhotoUploaded" runat="server" DataSourceID="SDSPhotoShopInfo" RepeatDirection="Horizontal" Width="100%">
                                                                                        <ItemTemplate>
                                                                                            <img src="<%# "uploadedFiles/database/shopinfo/" + Eval("shopInfoID").ToString()+ "/" + Eval("photoName")  %>"
                                                                                                style="max-height: 200px; margin: 0px 4px">
                                                                                        </ItemTemplate>
                                                                                    </asp:DataList>
                                                                                </div>
                                                                                <div class="space-4">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-4">
                                                                    </div>
                                                                </div>
                                                                <!-- /.row -->
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <h4>Operating Hours
                                                                            <asp:Label ID="LBLShopTimeStatus" runat="server" Font-Size="Small" /></h4>
                                                                        <asp:DataList ID="DLPShopTime" runat="server" DataKeyField="shopTimeID" DataSourceID="SDSShopTime" Width="100%">
                                                                            <ItemTemplate>
                                                                                <div class=" row">
                                                                                    <div class="col-sm-6 text-right">
                                                                                        <asp:Label ID="shopDayOfWeekLabel" runat="server" Text='<%# Eval("shopDayOfWeek") + " @ "  %>' />
                                                                                    </div>
                                                                                    <div class="col-sm-6 text-left">
                                                                                        <asp:Label ID="shopOpenTimeLabel" runat="server" Text='<%# Eval("shopOpenTime","{0:HH:mm}") + " to " %>' />
                                                                                        <asp:Label ID="shopCloseTimeLabel" runat="server" Text='<%# Eval("shopCloseTime","{0:HH:mm}") %>' />
                                                                                    </div>
                                                                                </div>
                                                                                <div class="hr hr-6"></div>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <h4>
                                                                            <asp:Label ID="LBLShopName" runat="server" Text=""></asp:Label>
                                                                        </h4>
                                                                        <p>
                                                                            <asp:Label ID="Label1" runat="server" Text="Contact : " Font-Bold="true"></asp:Label>
                                                                            <asp:Label ID="LBLShopInfoContact" runat="server" Text=""></asp:Label>
                                                                        </p>
                                                                        <p>
                                                                            <asp:Label ID="Label2" runat="server" Text="Address : " Font-Bold="true"></asp:Label>
                                                                            <asp:Label ID="LBLShopInfoAddress" runat="server" Text=""></asp:Label>
                                                                        </p>
                                                                        <p>
                                                                            <asp:Label ID="Label3" runat="server" Text="Description : " Font-Bold="true"></asp:Label>
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
                                            <asp:SqlDataSource ID="SDSShopInfo" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                SelectCommand="SELECT * FROM [ShopInfo] ORDER BY [shopInfoName]"></asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SDSShopTime" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                SelectCommand="SELECT * FROM [ShopTime] WHERE ([shopInfoID] = ?)">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="GVShopInfoOverview" Name="shopInfoID" PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:SqlDataSource ID="SDSPhotoShopInfo" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                SelectCommand="SELECT ShopInfo.shopInfoID, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath FROM (ShopInfo INNER JOIN Photo ON ShopInfo.shopInfoID = Photo.photoOwnerID) WHERE (ShopInfo.shopInfoID = ?)">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="GVShopInfoOverview" Name="photoOwnerID" PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- /.widget-main -->
                            </div>
                            <!-- /.widget-body -->
                        </div>
                        <!-- /.widget-box -->
                    </div>
                    <div class="vspace-12-sm"></div>
                </div>
                <!-- /.row -->
                <div class="hr hr32 hr-dotted"></div>
                <!-- PAGE CONTENT ENDS -->
            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
        <!-- inline scripts related to this page -->
        <script type="text/javascript">
            jQuery(function ($) {
                $('.easy-pie-chart.percentage').each(function () {
                    var $box = $(this).closest('.infobox');
                    var barColor = $(this).data('color') || (!$box.hasClass('infobox-dark') ? $box.css('color') : 'rgba(255,255,255,0.95)');
                    var trackColor = barColor == 'rgba(255,255,255,0.95)' ? 'rgba(255,255,255,0.25)' : '#E2E2E2';
                    var size = parseInt($(this).data('size')) || 50;
                    $(this).easyPieChart({
                        barColor: barColor,
                        trackColor: trackColor,
                        scaleColor: false,
                        lineCap: 'butt',
                        lineWidth: parseInt(size / 10),
                        animate: ace.vars['old_ie'] ? false : 1000,
                        size: size
                    });
                })
                $('.sparkline').each(function () {
                    var $box = $(this).closest('.infobox');
                    var barColor = !$box.hasClass('infobox-dark') ? $box.css('color') : '#FFF';
                    $(this).sparkline('html',
                        {
                            tagValuesAttribute: 'data-values',
                            type: 'bar',
                            barColor: barColor,
                            chartRangeMin: $(this).data('min') || 0
                        });
                });
                //flot chart resize plugin, somehow manipulates default browser resize event to optimize it!
                //but sometimes it brings up errors with normal resize event handlers
                $.resize.throttleWindow = false;
                var placeholder = $('#piechart-placeholder').css({ 'width': '90%', 'min-height': '150px' });
                var data = [
                    { label: "social networks", data: 38.7, color: "#68BC31" },
                    { label: "search engines", data: 24.5, color: "#2091CF" },
                    { label: "ad campaigns", data: 8.2, color: "#AF4E96" },
                    { label: "direct traffic", data: 18.6, color: "#DA5430" },
                    { label: "other", data: 10, color: "#FEE074" }
                ]
                function drawPieChart(placeholder, data, position) {
                    $.plot(placeholder, data, {
                        series: {
                            pie: {
                                show: true,
                                tilt: 0.8,
                                highlight: {
                                    opacity: 0.25
                                },
                                stroke: {
                                    color: '#fff',
                                    width: 2
                                },
                                startAngle: 2
                            }
                        },
                        legend: {
                            show: true,
                            position: position || "ne",
                            labelBoxBorderColor: null,
                            margin: [-30, 15]
                        }
                        ,
                        grid: {
                            hoverable: true,
                            clickable: true
                        }
                    })
                }
                drawPieChart(placeholder, data);
                /**
                we saved the drawing function and the data to redraw with different position later when switching to RTL mode dynamically
                so that's not needed actually.
                */
                placeholder.data('chart', data);
                placeholder.data('draw', drawPieChart);
                //pie chart tooltip example
                var $tooltip = $("<div class='tooltip top in'><div class='tooltip-inner'></div></div>").hide().appendTo('body');
                var previousPoint = null;
                placeholder.on('plothover', function (event, pos, item) {
                    if (item) {
                        if (previousPoint != item.seriesIndex) {
                            previousPoint = item.seriesIndex;
                            var tip = item.series['label'] + " : " + item.series['percent'] + '%';
                            $tooltip.show().children(0).text(tip);
                        }
                        $tooltip.css({ top: pos.pageY + 10, left: pos.pageX + 10 });
                    } else {
                        $tooltip.hide();
                        previousPoint = null;
                    }
                });
                /////////////////////////////////////
                $(document).one('ajaxloadstart.page', function (e) {
                    $tooltip.remove();
                });
                var d1 = [];
                for (var i = 0; i < Math.PI * 2; i += 0.5) {
                    d1.push([i, Math.sin(i)]);
                }
                var d2 = [];
                for (var i = 0; i < Math.PI * 2; i += 0.5) {
                    d2.push([i, Math.cos(i)]);
                }
                var d3 = [];
                for (var i = 0; i < Math.PI * 2; i += 0.2) {
                    d3.push([i, Math.tan(i)]);
                }
                var sales_charts = $('#sales-charts').css({ 'width': '100%', 'height': '220px' });
                $.plot("#sales-charts", [
                    { label: "Domains", data: d1 },
                    { label: "Hosting", data: d2 },
                    { label: "Services", data: d3 }
                ], {
                        hoverable: true,
                        shadowSize: 0,
                        series: {
                            lines: { show: true },
                            points: { show: true }
                        },
                        xaxis: {
                            tickLength: 0
                        },
                        yaxis: {
                            ticks: 10,
                            min: -2,
                            max: 2,
                            tickDecimals: 3
                        },
                        grid: {
                            backgroundColor: { colors: ["#fff", "#fff"] },
                            borderWidth: 1,
                            borderColor: '#555'
                        }
                    });
                $('#recent-box [data-rel="tooltip"]').tooltip({ placement: tooltip_placement });
                function tooltip_placement(context, source) {
                    var $source = $(source);
                    var $parent = $source.closest('.tab-content')
                    var off1 = $parent.offset();
                    var w1 = $parent.width();
                    var off2 = $source.offset();
                    //var w2 = $source.width();
                    if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
                    return 'left';
                }
                $('.dialogs,.comments').ace_scroll({
                    size: 300
                });
                //Android's default browser somehow is confused when tapping on label which will lead to dragging the task
                //so disable dragging when clicking on label
                var agent = navigator.userAgent.toLowerCase();
                if (ace.vars['touch'] && ace.vars['android']) {
                    $('#tasks').on('touchstart', function (e) {
                        var li = $(e.target).closest('#tasks li');
                        if (li.length == 0) return;
                        var label = li.find('label.inline').get(0);
                        if (label == e.target || $.contains(label, e.target)) e.stopImmediatePropagation();
                    });
                }
                $('#tasks').sortable({
                    opacity: 0.8,
                    revert: true,
                    forceHelperSize: true,
                    placeholder: 'draggable-placeholder',
                    forcePlaceholderSize: true,
                    tolerance: 'pointer',
                    stop: function (event, ui) {
                        //just for Chrome!!!! so that dropdowns on items don't appear below other items after being moved
                        $(ui.item).css('z-index', 'auto');
                    }
                }
                );
                $('#tasks').disableSelection();
                $('#tasks input:checkbox').removeAttr('checked').on('click', function () {
                    if (this.checked) $(this).closest('li').addClass('selected');
                    else $(this).closest('li').removeClass('selected');
                });
                //show the dropdowns on top or bottom depending on window height and menu position
                $('#task-tab .dropdown-hover').on('mouseenter', function (e) {
                    var offset = $(this).offset();
                    var $w = $(window)
                    if (offset.top > $w.scrollTop() + $w.innerHeight() - 100)
                        $(this).addClass('dropup');
                    else $(this).removeClass('dropup');
                });
            })
        </script>
    </div>
    <!-- /.page-content -->
</asp:Content>
