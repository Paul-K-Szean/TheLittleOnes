<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminPetInfoAdd.aspx.cs" Inherits="AdminPetInfoAdd" %>
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
                <a href="#">Pet Info </a>
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
                                    New Pet Information
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
                                                    <asp:Label ID="LBLCategory" runat="server" Text="Category" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLBreed" runat="server" Text="Breed" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBBreed" runat="server" CssClass="form-control" placeholder="Breed"></asp:TextBox>
                                                </div>
                                                <br />
                                                <asp:Label ID="LBLLifeSpan" runat="server" Text="Life Span" Font-Bold="True"></asp:Label>
                                                <div class="form-inline">
                                                    <asp:Label ID="Label1" runat="server" Text="Min"></asp:Label>
                                                    <asp:TextBox ID="TBLifeSpanMin" runat="server" CssClass="input-small" placeholder="Min"></asp:TextBox>
                                                    <asp:Label ID="Label2" runat="server" Text="Max"></asp:Label>
                                                    <asp:TextBox ID="TBLifeSpanMax" runat="server" CssClass="input-small" placeholder="Max"></asp:TextBox>
                                                </div>
                                                <br />
                                                <asp:Label ID="LBLHeight" runat="server" Text="Height" Font-Bold="True"></asp:Label>
                                                <div class="form-inline">
                                                    <asp:Label ID="Label4" runat="server" Text="Min"></asp:Label>
                                                    <asp:TextBox ID="TBHeightMin" runat="server" CssClass="input-small" placeholder="Min"></asp:TextBox>
                                                    <asp:Label ID="Label5" runat="server" Text="Max"></asp:Label>
                                                    <asp:TextBox ID="TBHeightMax" runat="server" CssClass="input-small" placeholder="Max"></asp:TextBox>
                                                </div>
                                                <br />
                                                <asp:Label ID="LBLWeight" runat="server" Text="Weight" Font-Bold="True"></asp:Label>
                                                <div class="form-inline">
                                                    <asp:Label ID="Label7" runat="server" Text="Min"></asp:Label>
                                                    <asp:TextBox ID="TBWeightMin" runat="server" CssClass="input-small" placeholder="Min"></asp:TextBox>
                                                    <asp:Label ID="Label8" runat="server" Text="Max"></asp:Label>
                                                    <asp:TextBox ID="TBWeightMax" runat="server" CssClass="input-small" placeholder="Max"></asp:TextBox>
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
                                                    <asp:TextBox ID="TBDesc" runat="server" CssClass="form-control" placeholder="Some description about the breed" TextMode="MultiLine" Rows="15"></asp:TextBox>
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
                                                    <asp:TextBox ID="TBPersonality" runat="server" CssClass="form-control" placeholder="Some personality about the breed" TextMode="MultiLine" Rows="15"></asp:TextBox>
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
                <div class="row">
                    <div class="col-xs-12">
                        <!-- PAGE CONTENT BEGINS -->
                        <div class="form-horizontal">
                            <div class="row">
                                <%--adaptability--%>
                                <div class="col-md-2">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">
                                                <asp:Label ID="LBLOverallAdaptability" runat="server" Text="Adaptability"></asp:Label></h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLAdaptSurrounding" runat="server" Text="Surrounding" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLAdaptSurrounding" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLAdaptSurrounding_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAdaptNovice" runat="server" Text="Novice" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLAdapt" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLAdapt_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAdaptLoneliness" runat="server" Text="Loneliness" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLAdaptLoneliness" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLAdaptLoneliness_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAdaptCold" runat="server" Text="Cold" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLAdaptCold" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLAdaptCold_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLAdaptHot" runat="server" Text="Hot" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLAdaptHot" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLAdaptHot_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
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
                                                <asp:Label ID="LBLOverallFriendliness" runat="server" Text="Friendliness"></asp:Label></h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLFriendlinessFamily" runat="server" Text="Family" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLFriendlinessFamily" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLFriendlinessFamily_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLFriendlinessKids" runat="server" Text="Kids" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLFriendlinessKids" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLFriendlinessKids_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLFriendlinessStrangers" runat="server" Text="Strangers" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLFriendlinessStrangers" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLFriendlinessStrangers_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLFriendlinessOtherPets" runat="server" Text="Other Pets" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLFriendlinessOtherPets" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLFriendlinessOtherPets_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label22" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
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
                                                <asp:Label ID="LBLOverallGrooming" runat="server" Text="Grooming"></asp:Label></h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLGroomingLevel" runat="server" Text="Grooming" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLGroomingLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLGroomingLevel_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLGroomingShedding" runat="server" Text="Shedding" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLGroomingShedding" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLGroomingShedding_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLGroomingDrooling" runat="server" Text="Drooling" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLGroomingDrooling" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLGroomingDrooling_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label25" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label26" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
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
                                                <asp:Label ID="LBLOverallTrainability" runat="server" Text="Trainability"></asp:Label></h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLTrainLevel" runat="server" Text="Train Level" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLTrainLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLTrainLevel_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLTrainIntelligence" runat="server" Text="Intelligence" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLTrainIntelligence" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLTrainIntelligence_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLTrainMouthiness" runat="server" Text="Mouthiness" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLTrainMouthiness" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLTrainMouthiness_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLTrainPreyDrive" runat="server" Text="Prey Drive" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLTrainPreyDrive" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLTrainPreyDrive_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLTrainBarkHowl" runat="server" Text="Bark/Howl" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLTrainBarkHowl" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLTrainBarkHowl_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
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
                                                <asp:Label ID="LBLOverallExercise" runat="server" Text="Exercise"></asp:Label></h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <%--   <div>
                                                    <asp:Label ID="LBLExerciseLevel" runat="server" Text="Exercise Level" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLExerciseLevel" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLExerciseLevel_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />--%>
                                                <div>
                                                    <asp:Label ID="LBLExerciseEnergy" runat="server" Text="Energy" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLExerciseEnergy" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLExerciseEnergy_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLExerciseNeeds" runat="server" Text="Needs" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLExerciseNeeds" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLExerciseNeeds_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLExercisePlayfullness" runat="server" Text="Playfullness" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLExercisePlayfullness" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DDLExercisePlayfullness_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label16" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="Label3" runat="server" Text="Not In Use" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" AppendDataBoundItems="true" Enabled="false">
                                                        <asp:ListItem Value="0">Please select something</asp:ListItem>
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
