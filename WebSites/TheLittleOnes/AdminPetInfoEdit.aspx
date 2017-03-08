<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminPetInfoEdit.aspx.cs" Inherits="AdminPetInfoEdit" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeaderMasterAdmin" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBodyMasterAdmin" runat="Server">
    <div class="breadcrumbs ace-save-state" id="breadcrumbs">
        <ul class="breadcrumb">
            <li>
                <i class="ace-icon fa fa-home home-icon"></i>
                <a href="#">Home</a>
            </li>

            <li>
                <a href="#">Pet Info</a>
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
                                    Edit Pet Information 
                                </small>
            </h1>
        </div>
        <!-- /.page-header -->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Grid view--%>
                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="table-header">
                                    <asp:Label ID="LBLSearchResult" runat="server" Text="Records for Pet info"></asp:Label>

                                    <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                                </div>
                                <!-- div.table-responsive -->
                                <div>
                                    <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                        <div class="row">
                                            <div class="col-xs-6">
                                                <div class="dataTables_length" id="dynamic-table_length">
                                                    Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCount" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCount_SelectedIndexChanged">
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

                                                    <label class="block clearfix">
                                                        <span class="block input-icon input-icon-right">Search:         
                                                    <asp:TextBox ID="TBSearchPetInfo" runat="server" CssClass="form-control  input-sm" placeholder="EG: silky terrier" AutoPostBack="true"></asp:TextBox>
                                                            <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                        </span>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="GVPetInfoOverview" runat="server"
                                            CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                            AutoGenerateColumns="False" DataKeyNames="petInfoID" DataSourceID="SDSPetInfo" Width="100%"
                                            AllowPaging="true" OnDataBound="GVPetInfoOverview_DataBound"
                                            OnSelectedIndexChanged="GVPetInfoOverview_SelectedIndexChanged">
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
                                                <asp:BoundField DataField="petInfoDesc" HeaderText="Description" SortExpression="petInfoDesc" />
                                                <asp:BoundField DataField="petInfoPersonality" HeaderText="Personality" SortExpression="petInfoPersonality" />
                                                <asp:BoundField DataField="petInfoDisplayStatus" HeaderText="Status" SortExpression="petInfoDisplayStatus" />
                                                <asp:CommandField ShowSelectButton="True" SelectText="View" />
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
                        <!-- PAGE CONTENT ENDS -->
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->


                <%--Datalist--%>
                <asp:DataList ID="DLPetInfoDetails" runat="server" DataKeyField="petInfoID" DataSourceID="SDSPetChar" Width="100%"
                    OnItemCreated="DLPetInfoDetails_ItemCreated"
                    OnItemDataBound="DLPetInfoDetails_ItemDataBound" OnEditCommand="DLPetInfoDetails_EditCommand"
                    OnCancelCommand="DLPetInfoDetails_CancelCommand" OnUpdateCommand="DLPetInfoDetails_UpdateCommand">
                    <EditItemTemplate>
                        <%--action buttons--%>
                        <div class="row">
                            <div class="col-xs-12 ">
                                <div class="hr dotted"></div>
                                <div class="form-inline pull-right">
                                    <asp:Button ID="BTNUpdate" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" CommandName="Update" />
                                    <asp:Button ID="BTNCancel" runat="server" CssClass="btn btn-primary btn-sm" Text="Cancel" CommandName="Cancel" />
                                </div>
                            </div>
                        </div>
                        <!-- /.row -->

                        <%--basic info--%>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="hr dotted"></div>
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
                                                        <div class="form-inline">
                                                            <asp:Label ID="Label21" runat="server" Text="Pet S/N"></asp:Label>
                                                            <asp:TextBox ID="TBPetInfoID" runat="server" CssClass="input-small"
                                                                placeholder="S/N" Text='<%# Eval("petInfoID")  %>' ReadOnly="true"></asp:TextBox>
                                                            <asp:Label ID="Label23" runat="server" Text="Char S/N"></asp:Label>
                                                            <asp:TextBox ID="TBCharID" runat="server" CssClass="input-small"
                                                                placeholder="S/N" Text='<%# Eval("charID")  %>' ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                        <div>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLCategory" runat="server" Text="Category" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-control"
                                                                AppendDataBoundItems="True" AutoPostBack="True" SelectedValue='<%# Eval("petInfoCategory") %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="Cat">Cat</asp:ListItem>
                                                                <asp:ListItem Value="Dog">Dog</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="Label20" runat="server" Text="Status" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLStatus" runat="server" CssClass="form-control"
                                                                AppendDataBoundItems="True" AutoPostBack="True" SelectedValue='<%# Eval("petInfoDisplayStatus") %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="Display">Display</asp:ListItem>
                                                                <asp:ListItem Value="Hide">Hide</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLBreed" runat="server" Text="Breed" Font-Bold="True"></asp:Label>
                                                            <asp:TextBox ID="TBBreed" runat="server" CssClass="form-control" placeholder="Breed" Text='<%# Eval("petInfoBreed") %>'></asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="LBLLifeSpan" runat="server" Text="Life Span" Font-Bold="True"></asp:Label>
                                                        <div class="form-inline">
                                                            <asp:Label ID="Label1" runat="server" Text="Min"></asp:Label>
                                                            <asp:TextBox ID="TBLifeSpanMin" runat="server" CssClass="input-small" placeholder="Min" Text='<%# Eval("petInfoLifeSpanMin") %>'></asp:TextBox>
                                                            <asp:Label ID="Label2" runat="server" Text="Max"></asp:Label>
                                                            <asp:TextBox ID="TBLifeSpanMax" runat="server" CssClass="input-small" placeholder="Max" Text='<%# Eval("petInfoLifeSpanMax") %>'></asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="LBLHeight" runat="server" Text="Height" Font-Bold="True"></asp:Label>
                                                        <div class="form-inline">
                                                            <asp:Label ID="Label4" runat="server" Text="Min"></asp:Label>
                                                            <asp:TextBox ID="TBHeightMin" runat="server" CssClass="input-small" placeholder="Min" Text='<%# Eval("petInfoHeightMin") %>'></asp:TextBox>
                                                            <asp:Label ID="Label5" runat="server" Text="Max"></asp:Label>
                                                            <asp:TextBox ID="TBHeightMax" runat="server" CssClass="input-small" placeholder="Max" Text='<%# Eval("petInfoHeightMax") %>'></asp:TextBox>
                                                        </div>
                                                        <br />
                                                        <asp:Label ID="LBLWeight" runat="server" Text="Weight" Font-Bold="True"></asp:Label>
                                                        <div class="form-inline">
                                                            <asp:Label ID="Label7" runat="server" Text="Min"></asp:Label>
                                                            <asp:TextBox ID="TBWeightMin" runat="server" CssClass="input-small" placeholder="Min" Text='<%# Eval("petInfoWeightMin") %>'></asp:TextBox>
                                                            <asp:Label ID="Label8" runat="server" Text="Max"></asp:Label>
                                                            <asp:TextBox ID="TBWeightMax" runat="server" CssClass="input-small" placeholder="Max" Text='<%# Eval("petInfoWeightMax") %>'></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--basic info desc--%>
                                        <div class="col-md-3">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">Description</h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLDesc" runat="server" Text="Description" Font-Bold="True"></asp:Label>
                                                            <asp:TextBox ID="TBDesc" runat="server" CssClass="form-control" placeholder="Some description about the breed" TextMode="MultiLine" Text='<%# Eval("petInfoDesc") %>' Rows="15"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--basic info personality--%>
                                        <div class="col-md-3">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">Personality</h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLPersonality" runat="server" Text="Personality" Font-Bold="True"></asp:Label>
                                                            <asp:TextBox ID="TBPersonality" runat="server" CssClass="form-control" placeholder="Some personality about the breed" TextMode="MultiLine" Text='<%# Eval("petInfoPersonality") %>' Rows="15"></asp:TextBox>
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
                                    <div class="space-6"></div>
                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                        <%--characteristics--%>
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="form-horizontal">
                                    <div class="row">
                                        <%--adaptability--%>
                                        <div class="col-md-2">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">
                                                        <asp:Label ID="LBLOverallAdaptability" runat="server" Text='<%# "Adaptability (" + Eval("charOverallAdaptability") + "/5)"%> '></asp:Label></h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLAdaptSurrounding" runat="server" Text="Surrounding" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharAdaptToSurrounding" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharAdaptToSurrounding_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charAdaptToSurrounding")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLAdaptNovice" runat="server" Text="Novice" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharAdaptToNovice" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharAdaptToNovice_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charAdaptToNovice")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLCharAdaptToLoneliness" runat="server" Text="Loneliness" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharAdaptToLoneliness" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharAdaptToLoneliness_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charAdaptToLoneliness")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLAdaptCold" runat="server" Text="Cold" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharAdaptToCold" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharAdaptToCold_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charAdaptToCold")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLAdaptHot" runat="server" Text="Hot" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharAdaptToHot" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharAdaptToHot_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charAdaptToHot")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--friendliness--%>
                                        <div class="col-md-2">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">
                                                        <asp:Label ID="LBLOverallFriendliness" runat="server" Text='<%# "Friendliness (" + Eval("charOverallFriendliness") + "/5)"%> '></asp:Label></h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLFriendlinessFamily" runat="server" Text="Family" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharFriendWithFamily" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharFriendWithFamily_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charFriendWithFamily")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLFriendlinessKids" runat="server" Text="Kids" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharFriendWithKids" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharFriendWithKids_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charFriendWithKids")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLFriendlinessStrangers" runat="server" Text="Strangers" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharFriendWithStrangers" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharFriendWithStrangers_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charFriendWithStrangers") )))%>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLFriendlinessOtherPets" runat="server" Text="Other Pets" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharFriendWithOtherPet" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharFriendWithOtherPet_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble(Eval("charFriendWithOtherPet") )))%>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="Label22" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--grooming--%>
                                        <div class="col-md-2">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">
                                                        <asp:Label ID="LBLOverallGrooming" runat="server" Text='<%# "Grooming (" + Eval("charOverallGrooming") + "/5)"%> '></asp:Label></h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLGroomingLevel" runat="server" Text="Grooming" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharGroomLevel" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharGroomLevel_SelectedIndexChanged"
                                                                SelectedValue='<%#Convert.ToInt32((Convert.ToDouble( Eval("charGroomLevel")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLGroomingShedding" runat="server" Text="Shedding" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharGroomShedding" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DDLCharGroomShedding_SelectedIndexChanged"
                                                                SelectedValue='<%#Convert.ToInt32((Convert.ToDouble( Eval("charGroomShedding")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLGroomingDrooling" runat="server" Text="Drooling" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharGroomDrooling" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharGroomDrooling_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charGroomDrooling")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="Label25" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control" AppendDataBoundItems="True" Enabled="False">
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="Label26" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--trainability--%>
                                        <div class="col-md-2">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">
                                                        <asp:Label ID="LBLOverallTrainability" runat="server" Text='<%# "Trainability (" + Eval("charOverallTrainability") + "/5)"%> '></asp:Label></h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLTrainLevel" runat="server" Text="Train Level" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharTrainLevel" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharTrainLevel_SelectedIndexChanged"
                                                                SelectedValue='<%#Convert.ToInt32((Convert.ToDouble(  Eval("charTrainLevel")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLTrainIntelligence" runat="server" Text="Intelligence" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharTrainIntelligence" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharTrainIntelligence_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charTrainIntelligence")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLTrainMouthiness" runat="server" Text="Mouthiness" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharTrainMouthiness" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharTrainMouthiness_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charTrainMouthiness")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLTrainPreyDrive" runat="server" Text="Prey Drive" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharTrainPreyDrive" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharTrainPreyDrive_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charTrainPreyDrive")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLTrainBarkHowl" runat="server" Text="Bark/Howl" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharTrainBarkHowl" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharTrainBarkHowl_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charTrainBarkHowl")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--exercise--%>
                                        <div class="col-md-2">
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="widget-title">
                                                        <asp:Label ID="LBLOverallExercise" runat="server" Text='<%# "Exercise (" + Eval("charOverallExercise") + "/5)"%> '></asp:Label></h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <div>
                                                            <asp:Label ID="LBLExerciseEnergy" runat="server" Text="Energy" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharExerciseEnergyLevel" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharExerciseEnergyLevel_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charExerciseEnergyLevel")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLExerciseNeeds" runat="server" Text="Needs" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharExerciseNeeds" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharExerciseNeeds_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charExerciseNeeds")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="LBLExercisePlayfullness" runat="server" Text="Playfullness" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DDLCharExercisePlayfullness" runat="server" CssClass="form-control" AppendDataBoundItems="True"
                                                                AutoPostBack="True" OnSelectedIndexChanged="DDLCharExercisePlayfullness_SelectedIndexChanged"
                                                                SelectedValue='<%# Convert.ToInt32((Convert.ToDouble( Eval("charExercisePlayfullness")))) %>'>
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                        <div>
                                                            <asp:Label ID="Label16" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                                <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="space-6"></div>
                                </div>
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->


                    </EditItemTemplate>
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-xs-12 ">
                                <!-- PAGE CONTENT BEGINS -->
                                <div class="hr dotted"></div>
                                <div class="form-inline pull-right">
                                    <asp:Button ID="BTNEdit" runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit" />
                                </div>
                            </div>
                            <!-- PAGE CONTENT ENDS -->
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <!-- PAGE CONTENT BEGINS -->
                                <div class="hr dotted"></div>
                                <div>
                                    <div id="user-profile-1" class="user-profile row">
                                        <div class="col-xs-12 col-sm-4 center">
                                            <div class="space-6"></div>
                                            <div class="profile-user-info profile-user-info-striped align-left">
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">S/N</div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label22" runat="server" Text='<%# Eval("petInfoID") %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Category </div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label25" runat="server" Text='<%# Eval("petInfoCategory") %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Breed </div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="petInfoIDLabel" runat="server" Text='<%# Eval("petInfoBreed") %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Life Span </div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label26" runat="server" Text='<%# Eval("petInfoLifeSpanMin") + " to " + Eval("petInfoLifeSpanMax") + " years"  %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Heigh </div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label27" runat="server" Text='<%# Eval("petInfoHeightMin") + " to " + Eval("petInfoHeightMax") + " cm tall from shoulder"  %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Weight </div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label28" runat="server" Text='<%# Eval("petInfoWeightMin") + " to " + Eval("petInfoWeightMax") + " kg(s)"  %>' />
                                                    </div>
                                                </div>

                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Desciption</div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label29" runat="server" Text='<%# Eval("petInfoDesc") %>' />
                                                    </div>
                                                </div>
                                                <div class="profile-info-row">
                                                    <div class="profile-info-name">Personality</div>
                                                    <div class="profile-info-value">
                                                        <asp:Label ID="Label30" runat="server" Text='<%# Eval("petInfoPersonality") %>' />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--PHOTOS--%>
                                        <div class="col-xs-12 col-sm-8">
                                            <div class="center">
                                                <div>
                                                    <div class="overflow-auto scrollbarHorizontal" style="max-width: 1110px;">
                                                        <asp:DataList ID="DLPhotoUploaded" runat="server" DataSourceID="SDSPhoto" Width="100%"
                                                            RepeatDirection="Horizontal">
                                                            <ItemTemplate>
                                                                <img src="<%# "uploadedFiles/database/petinfo/" + Eval("petInfoID") + "/" + Eval("photoName") %>"
                                                                    style="max-height: 200px; margin: 0px 4px">
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                    <div class="space-4"></div>
                                                </div>
                                            </div>

                                            <div class="hr dotted"></div>
                                            <%--Adaptability--%>
                                            <div class="clearfix">
                                                <div class=" center">
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charAdaptToSurroundingLabel" runat="server" Text='<%# Eval("charAdaptToSurrounding") %>' />
                                                        </span>
                                                        <br>
                                                        Surrounding
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charAdaptToNoviceLabel" runat="server" Text='<%# Eval("charAdaptToNovice") %>' />
                                                        </span>
                                                        <br>
                                                        Novice
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charAdaptToLonelinessLabel" runat="server" Text='<%# Eval("charAdaptToLoneliness") %>' />
                                                        </span>
                                                        <br>
                                                        Loneliness
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charAdaptToColdLabel" runat="server" Text='<%# Eval("charAdaptToCold") %>' />
                                                        </span>
                                                        <br>
                                                        Cold
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charAdaptToHotLabel" runat="server" Text='<%# Eval("charAdaptToHot") %>' />
                                                        </span>
                                                        <br>
                                                        Hot
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="charOverallAdaptabilityLabel" runat="server" Text='<%# Eval("charOverallAdaptability", "{0:0.0}") %>' />
                                                        </span>
                                                        <br>
                                                        <h6 class="bolder">Overall Adaptability</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="hr dotted"></div>
                                            <%--Friendliness--%>
                                            <div class="clearfix">
                                                <div class=" center">
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("charFriendWithFamily") %>' />
                                                        </span>
                                                        <br>
                                                        Family
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("charFriendWithKids") %>' />
                                                        </span>
                                                        <br>
                                                        Kids
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("charFriendWithStrangers") %>' />
                                                        </span>
                                                        <br>
                                                        Strangers
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("charFriendWithOtherPet") %>' />
                                                        </span>
                                                        <br>
                                                        Other Pet
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <div class="space-1"></div>
                                                        </span>
                                                        <br>
                                                        <div class="space-8"></div>
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("charOverallFriendliness", "{0:0.0}") %>' />
                                                        </span>
                                                        <br>
                                                        <h6 class="bolder">Overall Friendliness</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="hr dotted"></div>
                                            <%--Grooming--%>
                                            <div class="clearfix">
                                                <div class=" center">
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("charGroomLevel") %>' />
                                                        </span>
                                                        <br>
                                                        Groom Level
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("charGroomShedding") %>' />
                                                        </span>
                                                        <br>
                                                        Shedding
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("charGroomDrooling") %>' />
                                                        </span>
                                                        <br>
                                                        Drooling
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <div class="space-1"></div>
                                                        </span>
                                                        <br>
                                                        <div class="space-8"></div>
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <div class="space-1"></div>
                                                        </span>
                                                        <br>
                                                        <div class="space-8"></div>
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("charOverallGrooming", "{0:0.0}") %>' />
                                                        </span>
                                                        <br>
                                                        <h6 class="bolder">Overall Grooming</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="hr dotted"></div>
                                            <%--Trainability--%>
                                            <div class="clearfix">
                                                <div class=" center">
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("charTrainLevel") %>' />
                                                        </span>
                                                        <br>
                                                        Train Level
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label11" runat="server" Text='<%# Eval("charTrainIntelligence") %>' />
                                                        </span>
                                                        <br>
                                                        Intelligence
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label12" runat="server" Text='<%# Eval("charTrainMouthiness") %>' />
                                                        </span>
                                                        <br>
                                                        Mouthiness
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label13" runat="server" Text='<%# Eval("charTrainPreyDrive") %>' />
                                                        </span>
                                                        <br>
                                                        Prey Drive
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label14" runat="server" Text='<%# Eval("charTrainBarkHowl") %>' />
                                                        </span>
                                                        <br>
                                                        Bark Howl
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label15" runat="server" Text='<%# Eval("charOverallTrainability", "{0:0.0}") %>' />
                                                        </span>
                                                        <br>
                                                        <h6 class="bolder">Overall Trainability</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="hr dotted"></div>
                                            <%--Exercise--%>
                                            <div class="clearfix">
                                                <div class=" center">
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label16" runat="server" Text='<%# Eval("charExerciseEnergyLevel") %>' />
                                                        </span>
                                                        <br>
                                                        Energy Level
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("charExerciseNeeds") %>' />
                                                        </span>
                                                        <br>
                                                        Exercise Needs
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label18" runat="server" Text='<%# Eval("charExercisePlayfullness") %>' />
                                                        </span>
                                                        <br>
                                                        Playfullness
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <div class="space-1"></div>
                                                        </span>
                                                        <br>
                                                        <div class="space-8"></div>
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <div class="space-1"></div>
                                                        </span>
                                                        <br>
                                                        <div class="space-8"></div>
                                                    </div>
                                                    <div class="grid6">
                                                        <span class="bigger-175 blue">
                                                            <asp:Label ID="Label19" runat="server" Text='<%# Eval("charOverallExercise", "{0:0.0}") %>' />
                                                        </span>
                                                        <br>
                                                        <h6 class="bolder">Overall Exercise</h6>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="hr dotted"></div>

                                        </div>
                                    </div>
                                </div>
                                <!-- PAGE CONTENT ENDS -->
                            </div>
                            <!-- /.col -->
                        </div>
                        <!-- /.row -->
                    </ItemTemplate>
                </asp:DataList>


                <%--SQLDataSource--%>
                <asp:SqlDataSource ID="SDSPetInfo" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT * FROM [PetInfo] ORDER BY [petInfoID] DESC,[petInfoCategory], [petInfoBreed]"
                    FilterExpression="petInfoCategory LIKE '%{0}%' OR petInfoBreed LIKE '%{0}%' OR petInfoDesc LIKE '%{0}%' OR petInfoPersonality LIKE '%{0}%' OR petInfoDisplayStatus LIKE '%{0}%'">
                    <FilterParameters>
                        <asp:ControlParameter Name="petInfoCategory" ControlID="TBSearchPetInfo" PropertyName="Text" />
                    </FilterParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPetChar" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT PetInfo.petInfoID, PetInfo.petInfoCategory, PetInfo.petInfoBreed, PetInfo.petInfoLifeSpanMin, PetInfo.petInfoHeightMin, PetInfo.petInfoWeightMin, PetInfo.petInfoLifeSpanMax, PetInfo.petInfoHeightMax, PetInfo.petInfoWeightMax, PetInfo.petInfoDesc, PetInfo.petInfoPersonality, PetInfo.petInfoDisplayStatus, PetCharacteristics.charID, PetCharacteristics.charOverallAdaptability, PetCharacteristics.charOverallFriendliness, PetCharacteristics.charOverallGrooming, PetCharacteristics.charOverallTrainability, PetCharacteristics.charOverallExercise, PetCharacteristics.charAdaptToSurrounding, PetCharacteristics.charAdaptToNovice, PetCharacteristics.charAdaptToLoneliness, PetCharacteristics.charAdaptToCold, PetCharacteristics.charAdaptToHot, PetCharacteristics.charFriendWithFamily, PetCharacteristics.charFriendWithKids, PetCharacteristics.charFriendWithStrangers, PetCharacteristics.charFriendWithOtherPet, PetCharacteristics.charGroomShedding, PetCharacteristics.charGroomDrooling, PetCharacteristics.charGroomLevel, PetCharacteristics.charTrainLevel, PetCharacteristics.charTrainIntelligence, PetCharacteristics.charTrainMouthiness, PetCharacteristics.charTrainPreyDrive, PetCharacteristics.charTrainBarkHowl, PetCharacteristics.charExerciseEnergyLevel, PetCharacteristics.charExerciseNeeds, PetCharacteristics.charExercisePlayfullness FROM (PetInfo INNER JOIN PetCharacteristics ON PetInfo.petInfoID = PetCharacteristics.petInfoID) WHERE (PetInfo.petInfoID = ?)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GVPetInfoOverview" Name="petInfoID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SDSPhoto" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                    ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                    SelectCommand="SELECT PetInfo.petInfoID, PetInfo.petInfoCategory, PetInfo.petInfoBreed, PetInfo.petInfoLifeSpanMin, PetInfo.petInfoHeightMin, PetInfo.petInfoWeightMin, PetInfo.petInfoLifeSpanMax, PetInfo.petInfoHeightMax, PetInfo.petInfoWeightMax, PetInfo.petInfoDesc, PetInfo.petInfoPersonality, PetInfo.petInfoDisplayStatus, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath FROM (PetInfo INNER JOIN Photo ON PetInfo.petInfoID = Photo.photoOwnerID) WHERE (Photo.photoOwnerID = ?)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GVPetInfoOverview" Name="photoOwnerID" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </ContentTemplate>

        </asp:UpdatePanel>
    </div>
    <!-- /.page-content -->

</asp:Content>

