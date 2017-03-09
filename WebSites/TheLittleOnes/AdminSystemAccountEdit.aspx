<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminSystemAccountEdit.aspx.cs" Inherits="AdminSystemAccountEdit" %>

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
                <a href="#">System Account</a>
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
                                    Edit System Account
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
                            <asp:Label ID="LBLSearchResultSystemAccount" runat="server" Text="Records for System Account"></asp:Label>
                            <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                        </div>
                        <!-- div.table-responsive -->
                        <div>
                            <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="dataTables_length" id="dynamic-table_length">
                                            Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountSystemAccount" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountSystemAccount_SelectedIndexChanged">
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
                                                <asp:DropDownList ID="DDLFilterAccountType" runat="server" AutoPostBack="true"
                                                    OnSelectedIndexChanged="DDLFilterAccountType_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Filter Account Type</asp:ListItem>
                                                    <asp:ListItem Value="WebAdmin">WebAdmin</asp:ListItem>
                                                    <asp:ListItem Value="WebShelterGroup">WebShelterGroup</asp:ListItem>
                                                    <asp:ListItem Value="WebSponsorGroup">WebSponsorGroup</asp:ListItem>
                                                    <asp:ListItem Value="WebUser">WebUser</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                            <div class="space-6"></div>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">Search:         
                                                    <asp:TextBox ID="TBSearchSystemAccount" runat="server" CssClass="form-control  input-sm"
                                                        placeholder="EG: Shelter" AutoPostBack="true"
                                                        OnTextChanged="TBSearchSystemAccount_TextChanged" Text=""></asp:TextBox>
                                                    <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVSystemAccountOverview" runat="server" DataKeyNames="accountID"
                                    CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                    AutoGenerateColumns="False" DataSourceID="SDSSystemAccount" Width="100%"
                                    AllowPaging="True"
                                    OnDataBound="GVSystemAccountOverview_DataBound"
                                    OnRowDataBound="GVSystemAccountOverview_RowDataBound"
                                    OnSelectedIndexChanged="GVSystemAccountOverview_SelectedIndexChanged"
                                    OnSelectedIndexChanging="GVSystemAccountOverview_SelectedIndexChanging"
                                    OnPageIndexChanging="GVSystemAccountOverview_PageIndexChanging">
                                    <Columns>

                                        <asp:BoundField DataField="accountID" HeaderText="S/N" SortExpression="accountID" InsertVisible="False" />
                                        <asp:BoundField DataField="accountEmail" HeaderText="Email" SortExpression="accountEmail" />
                                        <asp:BoundField DataField="accountType" HeaderText="Type" SortExpression="accountType" />
                                        <asp:BoundField DataField="dateJoined" HeaderText="Date Joined" SortExpression="dateJoined" />
                                        <asp:BoundField DataField="profileID" HeaderText="Profile S/N" SortExpression="profileID" InsertVisible="False" />
                                        <asp:BoundField DataField="profileName" HeaderText="Name" SortExpression="profileName" />
                                        <asp:BoundField DataField="profileContact" HeaderText="Contact" SortExpression="profileContact" />
                                        <asp:BoundField DataField="profileAddress" HeaderText="Address" SortExpression="profileAddress" />
                                        <asp:BoundField DataField="shopInfoID" HeaderText="ShopInfo S/N" SortExpression="shopInfoID" />
                                        <asp:CommandField SelectText="Edit" ShowSelectButton="true" />
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
                            <asp:Panel ID="PNLSystemAccountEdit" runat="server" Visible="false">

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

                                    </div>
                                    <%--account info--%>
                                    <div class="col-md-3">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Basic Info</h4>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="LBLAccountID" runat="server" Text="Account S/N" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBAccountID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLAccountType" runat="server" Text="Account Type" Font-Bold="True"></asp:Label>
                                                        <asp:DropDownList ID="DDLAccountType" runat="server" CssClass=" form-control" AutoPostBack="true"
                                                            OnSelectedIndexChanged="DDLAccountType_SelectedIndexChanged">
                                                            <asp:ListItem Value=""> Select Account Type</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="Label2" runat="server" Text="Email (Login ID)" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBAccountEmail" runat="server" CssClass="form-control" placeholder="EG: SeanJeanDean@hotmail.com" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:CheckBox ID="CHKBXReset" runat="server"  CssClass=" checkbox-inline" Text="Reset Old Password"/>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLDateJoined" runat="server" Text="Date Joined" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBDateJoined" runat="server" CssClass="form-control" placeholder="EG: 02/04/2015" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--profile info--%>
                                    <div class="col-md-3">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Profile Info</h4>
                                            </div>

                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="LBLProfileID" runat="server" Text="Profile S/N" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBProfileID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLProfileName" runat="server" Text="User Name" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBProfileName" runat="server" CssClass="form-control" placeholder="EG: Garry Cooper"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLProfileContact" runat="server" Text="Contact" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="TBProfileContact" runat="server" CssClass="form-control" placeholder="EG: 87675234"></asp:TextBox>

                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLProfileAddress" runat="server" Text="Address" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBProfileAddress" runat="server" CssClass="form-control" placeholder="EG:Singapore address"></asp:TextBox>

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
                                                                        <img src="<%# "uploadedFiles/database/profileinfo/" + Eval("ProfileID").ToString().ToLower().Replace(" ", "") + "/" + Eval("PhotoName")  %>"
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
                            </asp:Panel>
                        </div>
                        <div class="space-6"></div>
                    </div>
                    <!-- PAGE CONTENT ENDS -->
                </div>
                <!-- /.col -->



                <%--SQLDataSource--%>
                <asp:SqlDataSource ID="SDSSystemAccount" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT Account.shopInfoID, Account.accountID, Account.accountEmail, Account.accountPassword, Account.accountType, Account.dateJoined, Profile.accountID AS Expr1, Profile.profileID, Profile.profileName, Profile.profileContact, Profile.profileAddress FROM (Account INNER JOIN Profile ON Account.accountID = Profile.accountID) ORDER BY ACCOUNT.ACCOUNTID DESC"></asp:SqlDataSource>

                <asp:SqlDataSource ID="SDSPetInfo" runat="server"></asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPhoto" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT Profile.accountID, Profile.profileID, Profile.profileName, Profile.profileContact, Profile.profileAddress, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath, Photo.photoPurpose FROM (Profile INNER JOIN Photo ON Profile.profileID = Photo.photoOwnerID) WHERE (Profile.profileID = ? AND Photo.PhotoPurpose='ProfileInfo')">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="TBProfileID" Name="profileID" PropertyName="Text" Type="Int32" />
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

