<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminAdoptionInfoEdit.aspx.cs" Inherits="AdminAdoptionInfoEdit" %>
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
                            </select><div class="dropdown dropdown-colorpicker">
                                <a data-toggle="dropdown" class="dropdown-toggle"><span class="btn-colorpicker" style="background-color: #438EB9"></span></a>
                                <ul class="dropdown-menu dropdown-caret">
                                    <li><a class="colorpick-btn selected" style="background-color: #438EB9;" data-color="#438EB9"></a></li>
                                    <li><a class="colorpick-btn" style="background-color: #222A2D;" data-color="#222A2D"></a></li>
                                    <li><a class="colorpick-btn" style="background-color: #C6487E;" data-color="#C6487E"></a></li>
                                    <li><a class="colorpick-btn" style="background-color: #D0D0D0;" data-color="#D0D0D0"></a></li>
                                </ul>
                            </div>
                        </div>
                        <span>&nbsp; Choose Skin</span>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-navbar" autocomplete="off">
                        <label class="lbl" for="ace-settings-navbar">Fixed Navbar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-sidebar" autocomplete="off">
                        <label class="lbl" for="ace-settings-sidebar">Fixed Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-breadcrumbs" autocomplete="off">
                        <label class="lbl" for="ace-settings-breadcrumbs">Fixed Breadcrumbs</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-rtl" autocomplete="off">
                        <label class="lbl" for="ace-settings-rtl">Right To Left (rtl)</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2 ace-save-state" id="ace-settings-add-container" autocomplete="off">
                        <label class="lbl" for="ace-settings-add-container">
                            Inside
											<b>.container</b>
                        </label>
                    </div>
                </div>
                <!-- /.pull-left -->
                <div class="pull-left width-50">
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-hover" autocomplete="off">
                        <label class="lbl" for="ace-settings-hover">Submenu on Hover</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-compact" autocomplete="off">
                        <label class="lbl" for="ace-settings-compact">Compact Sidebar</label>
                    </div>
                    <div class="ace-settings-item">
                        <input type="checkbox" class="ace ace-checkbox-2" id="ace-settings-highlight" autocomplete="off">
                        <label class="lbl" for="ace-settings-highlight">Alt. Active Item</label>
                    </div>
                </div>
                <!-- /.pull-left -->
            </div>
            <!-- /.ace-settings-box -->
        </div>
        <!-- /.ace-settings-container -->
        <div class="page-header">
            <h1>Settings
								<small>
                                    <i class="ace-icon fa fa-angle-double-right"></i>
                                    Edit Adoption Information
                                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Grid view--%>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="table-header">
                            <asp:Label ID="LBLSearchResultAdoptInfo" runat="server" Text="Records for Adopt Info"></asp:Label>
                            <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                        </div>
                        <!-- div.table-responsive -->
                        <div>
                            <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="dataTables_length" id="dynamic-table_length">
                                            Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountAdoptInfo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountAdoptInfo_SelectedIndexChanged">
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
                                                <asp:DropDownList ID="DDLFilterGender" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLFilterGender_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Filter Gender</asp:ListItem>
                                                    <asp:ListItem Value="Female">Female</asp:ListItem>
                                                    <asp:ListItem Value="Male">Male</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDLFilterSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLFilterSize_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Filter Size</asp:ListItem>
                                                    <asp:ListItem Value="Small">Small</asp:ListItem>
                                                    <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                    <asp:ListItem Value="Large">Large</asp:ListItem>
                                                    <asp:ListItem Value="X-Large">X-Large</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="DDLFilterStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLFilterStatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Filter Status</asp:ListItem>
                                                    <asp:ListItem Value="Adopted">Adopted</asp:ListItem>
                                                    <asp:ListItem Value="Available">Available</asp:ListItem>
                                                    <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                            <div class="space-6"></div>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">Search:         
                                                    <asp:TextBox ID="TBSearchAdoptInfo" runat="server" CssClass="form-control  input-sm" placeholder="EG: Shelter"
                                                        AutoPostBack="true" OnTextChanged="TBSearchAdoptInfo_TextChanged" Text=""></asp:TextBox>
                                                    <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVAdoptInfoOverview" runat="server" DataKeyNames="adoptInfoID"
                                    CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                    AutoGenerateColumns="False" DataSourceID="SDSAdoptInfo" Width="100%"
                                    AllowPaging="True" 
                                    OnDataBound="GVAdoptInfoOverview_DataBound"
                                    OnSelectedIndexChanged="GVAdoptInfoOverview_SelectedIndexChanged"
                                    OnSelectedIndexChanging="GVAdoptInfoOverview_SelectedIndexChanging"
                                     OnPageIndexChanging="GVAdoptInfoOverview_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="adoptInfoID" HeaderText="S/N" SortExpression="adoptInfoID" />
                                        <asp:BoundField DataField="shopInfoID" HeaderText="ShopInfo S/N" SortExpression="shopInfoID" />
                                        <asp:BoundField DataField="petID" HeaderText="Pet S/N" SortExpression="petID" />
                                        <asp:BoundField DataField="adoptInfoStatus" HeaderText="Adopt Status" SortExpression="adoptInfoStatus" />
                                        <asp:BoundField DataField="petBreed" HeaderText="Breed" SortExpression="petBreed" />
                                        <asp:BoundField DataField="petName" HeaderText="Name" SortExpression="petName" />
                                        <asp:BoundField DataField="petGender" HeaderText="Gender" SortExpression="petGender" />
                                        <asp:BoundField DataField="petWeight" HeaderText="Weight" SortExpression="petWeight" />
                                        <asp:BoundField DataField="petSize" HeaderText="Size" SortExpression="petSize" />
                                        <asp:BoundField DataField="shopInfoName" HeaderText="Organisation" SortExpression="shopInfoName" />
                                        <asp:BoundField DataField="shopInfoContact" HeaderText="Contact" SortExpression="shopInfoContact" />
                                        <asp:CommandField ShowSelectButton="true" SelectText="Edit" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination-G5" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                </asp:GridView>
                                <asp:HiddenField ID="HDFPetID" runat="server"></asp:HiddenField>
                            </div>
                            <!-- div.dataTables_borderWrap -->
                        </div>
                        <div class="space-6"></div>
                        <div>
                            <div class="form-inline pull-right">
                                <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <div class="space-6"></div>
                <div class="hr hr-12"></div>
                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->
                        <div class="form-horizontal">
                            <asp:Panel ID="PNLAdoptInfoEdit" runat="server" Visible="false">
                                <%--action buttons--%>
                                <div class="row">
                                    <div class="col-xs-12 ">
                                        <div class="form-inline pull-right">
                                            <asp:Button ID="BTNUpdate" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="BTNUpdate_Click" />
                                            <asp:Button ID="BTNCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" OnClick="BTNCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                                <!-- /.row -->
                                <div class="space-6"></div>
                                <div class="row">
                                    <%--shopinfo--%>
                                    <div class="col-md-3">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Organisation</h4>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="LblOrganisation" runat="server" Text="Organisation" Font-Bold="True"></asp:Label>
                                                        <asp:DropDownList ID="DDLShopInfo" runat="server" CssClass="form-control" DataSourceID="SDSShopInfo"
                                                            DataTextField="shopInfoName" DataValueField="shopInfoID"
                                                            AutoPostBack="true" AppendDataBoundItems="true"
                                                            OnSelectedIndexChanged="DDLOrangisation_SelectedIndexChanged">
                                                            <asp:ListItem Value="">Select Organisation</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="SDSShopInfo" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                            ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                            SelectCommand="SELECT * FROM [ShopInfo]"></asp:SqlDataSource>
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
                                        <div class=" space-6"></div>
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Organisation</h4>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <div>
                                                            <asp:Label ID="LBLAdoptInfoID" runat="server" Text="AdoptInfo S/N" Font-Bold="True"></asp:Label>
                                                            <asp:TextBox ID="TBAdoptInfoID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="Label6" runat="server" Text="Organisation" Font-Bold="True"></asp:Label>
                                                        <asp:DropDownList ID="DDLAdoptInfoStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                                            <asp:ListItem Value="">Select Status</asp:ListItem>
                                                            <asp:ListItem Value="Adopted">Adopted</asp:ListItem>
                                                            <asp:ListItem Value="Available">Available</asp:ListItem>
                                                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                        </asp:DropDownList>
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
                                                        <asp:Label ID="LBLPetID" runat="server" Text="Pet S/N" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBPetID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
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
                                                                        <img src="<%# "uploadedFiles/database/pet/" + Eval("PetID").ToString().ToLower().Replace(" ", "") + "/" + Eval("PhotoName")  %>"
                                                                            style="max-height: 200px; margin: 0px 4px">
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
                            </asp:Panel>
                        </div>
                        <div class="space-6"></div>
                    </div>
                    <!-- PAGE CONTENT ENDS -->
                </div>
                <!-- /.col -->
                <%--SQLDataSource--%>
                <asp:SqlDataSource ID="SDSAdoptInfo" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, Pet.petID AS Expr1, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo, ShopInfo.shopInfoID AS Expr2, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday FROM ((AdoptInfo INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) ORDER BY [adoptInfoId] DESC"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPetInfo" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPhoto" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath, Photo.photoPurpose, Pet.petID, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo FROM (Pet INNER JOIN Photo ON Photo.photoOwnerID = Pet.petID) WHERE (Pet.petID = ?) AND (Photo.photoPurpose = 'Pet')">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="HDFPetID" Name="PetID" PropertyName="Value" Type="Int32" />
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
