<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminShopInfoEdit.aspx.cs" Inherits="AdminShopInfoEdit" %>
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
                <a href="#">Shop Info</a>
            </li>
            <li class="active">Settings</li>
        </ul>
        <!-- /.breadcrumb -->
        <div class="nav-search" id="nav-search">
            <div class="form-search">
                <span class="input-icon">
                    <input type="text" placeholder="Search ..." class="nav-search-input" id="nav-search-input" autocomplete="off">
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
                                    Edit Shop Information
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
                            <asp:Label ID="LBLSearchResultShopInfo" runat="server" Text="Records for Shop info"></asp:Label>
                            <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                        </div>
                        <!-- div.table-responsive -->
                        <div>
                            <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="dataTables_length" id="dynamic-table_length">
                                            Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountShopInfo" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountShopInfo_SelectedIndexChanged">
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
                                                    <asp:TextBox ID="TBSearchShopInfo" runat="server" CssClass="form-control  input-sm" placeholder="EG: Vet"
                                                        AutoPostBack="true" OnTextChanged="TBSearchShopInfo_TextChanged"
                                                        Text=""></asp:TextBox>
                                                    <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVShopInfoOverview" runat="server"
                                    CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                    AutoGenerateColumns="False" DataKeyNames="shopInfoID" DataSourceID="SDSShopInfo" Width="100%"
                                    AllowPaging="True" OnDataBound="GVShopInfoOverview_DataBound"
                                    OnSelectedIndexChanged="GVShopInfoOverview_SelectedIndexChanged"
                                    OnSelectedIndexChanging="GVShopInfoOverview_SelectedIndexChanging"
                                    OnPageIndexChanging="GVShopInfoOverview_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="shopInfoID" HeaderText="S/N" InsertVisible="False" ReadOnly="True" SortExpression="shopInfoID" />
                                        <asp:BoundField DataField="shopInfoName" HeaderText="Name" SortExpression="shopInfoName" />
                                        <asp:BoundField DataField="shopInfoContact" HeaderText="Contact" SortExpression="shopInfoContact" HeaderStyle-Width="100" />
                                        <asp:BoundField DataField="shopInfoAddress" HeaderText="Address" SortExpression="shopInfoAddress" />
                                        <asp:TemplateField HeaderText="Grooming Service" SortExpression="Grooming Service">
                                            <ItemTemplate><%# (bool.Parse(Eval("shopInfoGrooming").ToString())) ? "Yes" : "No" %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="shopInfoType" HeaderText="Type" SortExpression="shopInfoType" />
                                        <asp:TemplateField HeaderText="Close on PH" SortExpression="shopInfoCloseOnPublicHoliday">
                                            <ItemTemplate><%# (bool.Parse(Eval("shopInfoCloseOnPublicHoliday").ToString())) ? "Yes" : "No" %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination-G5" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Previous" />
                                </asp:GridView>
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
                            <asp:Panel ID="PNLShopInfoEdit" runat="server" Visible="false">
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
                                    <%--basic info--%>
                                    <div class="col-md-5">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Basic Info</h4>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="LBLShopInfoID" runat="server" Text="Shop Info ID" Font-Bold="True"></asp:Label>
                                                        <asp:TextBox ID="TBShopInfoID" runat="server" CssClass="form-control " placeholder="Shop Info ID" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <br />
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
                                                        <asp:DropDownList ID="DDLShopType" runat="server" CssClass="form-control">
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
                                    <div class="col-md-4">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Operating Hours</h4>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <asp:Label ID="Label1" runat="server" Text="Operating Hours" Font-Bold="True"></asp:Label>
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseMonday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLMonday" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Monday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeMonday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeMonday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseTuesday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLTuesday" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Tuesday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeTuesday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeTuesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseWednesday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLWednesday" runat="server" CssClass="col-md-3 col-sm-3 control-label no-padding-right" Text="Wednesday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeWednesday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeWednesday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseThursday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="Label5" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Thursday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeThursday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeThursday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseFriday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLFriday" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Friday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeFriday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeFriday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseSaturday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLSaturday" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Saturday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
                                                            <asp:DropDownList ID="DDLOpenTimeSaturday" runat="server" CssClass="">
                                                                <asp:ListItem Value="">Open Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            to
                                                        <asp:DropDownList ID="DDLCloseTimeSaturday" runat="server" CssClass="">
                                                            <asp:ListItem Value="">Close Time</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <br />
                                                    <div class="form-inline">
                                                        <asp:CheckBox ID="CHKBXCloseSunday" runat="server" CssClass=" checkbox-inline " Text="Close" />
                                                        <asp:Label ID="LBLSunday" runat="server" CssClass="col-md-3 col-sm-3  control-label no-padding-right" Text="Sunday @"></asp:Label>
                                                        <div class="col-md-6 col-sm-5">
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
                                                        <asp:CheckBox ID="CHKBXCloseOnPublicHoliday" runat="server" CssClass="checkbox checkbox-inline"
                                                            Text="Close on Public Holiday" />
                                                    </div>
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
                                                                <asp:DataList ID="DLPhotoUploaded" runat="server" DataSourceID="SDSPhoto" Width="100%"
                                                                    RepeatDirection="Horizontal">
                                                                    <ItemTemplate>
                                                                        <img src="<%# "uploadedFiles/database/shopinfo/" + Eval("shopInfoID").ToString() + "/" + Eval("photoName") %>"
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
                            </asp:Panel>
                        </div>
                        <div class="space-6"></div>
                    </div>
                    <!-- PAGE CONTENT ENDS -->
                </div>
                <!-- /.col -->
                <%--SQLDataSource--%>
                <asp:SqlDataSource ID="SDSShopInfo" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT * FROM [ShopInfo] ORDER BY [shopInfoID] DESC,[shopInfoName] "></asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPhoto" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT ShopInfo.shopInfoID, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath FROM (ShopInfo INNER JOIN Photo ON ShopInfo.shopInfoID = Photo.photoOwnerID) WHERE (ShopInfo.shopInfoID = ? AND Photo.PhotoPurpose ='ShopInfo')">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GVShopInfoOverview" Name="photoOwnerID" PropertyName="SelectedValue" Type="Int32" />
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
