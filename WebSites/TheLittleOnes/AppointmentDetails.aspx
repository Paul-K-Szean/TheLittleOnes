<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnesUser.master" AutoEventWireup="true" CodeFile="AppointmentDetails.aspx.cs" Inherits="AppointmentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHeadUser" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBodyUser" runat="Server">
    <asp:HiddenField ID="HDFAccountID" runat="server" />
    <asp:HiddenField ID="HDFShopInfoID" runat="server" />
    <asp:SqlDataSource ID="SDSAppointment" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [Appointment] WHERE ([appmtFromID] = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFAccountID" Name="appmtFromID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSShopTime" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [ShopTime] WHERE ([shopInfoID] = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFShopInfoID" Name="shopInfoID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <div class="blog-left-right">
        <h2>Your Appointment Details</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Grid view--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-header">
                            <asp:Label ID="LBLSearchResultAppointment" runat="server" Text="Records for Appointment"></asp:Label>
                            <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                        </div>
                        <!-- div.table-responsive -->
                        <div>
                            <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                <div class="row">
                                    <div class="col-xs-6">
                                        <div class="dataTables_length" id="dynamic-table_length">
                                            Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountAppointment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountAppointment_SelectedIndexChanged">
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
                                                    AutoPostBack="true" />
                                                <asp:CheckBox ID="CHKBXFilterPetClinic" runat="server" CssClass="checkbox-inline" Text="Clinic"
                                                    AutoPostBack="true" />
                                                <asp:CheckBox ID="CHKBXFilterGrooming" runat="server" CssClass="checkbox-inline" Text="Grooming"
                                                    AutoPostBack="true" />
                                            </span>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">Search:         
                                                    <asp:TextBox ID="TBSearchAppointment" runat="server" CssClass="form-control  input-sm" placeholder="EG: Vet"
                                                        AutoPostBack="true" OnTextChanged="TBSearchAppointment_TextChanged"
                                                        Text=""></asp:TextBox>
                                                    <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVAppointment" runat="server"
                                    CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                    AutoGenerateColumns="False" DataSourceID="SDSAppointment" Width="100%"
                                    AllowPaging="True" OnDataBound="GVAppointment_DataBound"
                                    OnRowDataBound="GVAppointment_RowDataBound"
                                    OnSelectedIndexChanged="GVAppointment_SelectedIndexChanged"
                                    OnSelectedIndexChanging="GVAppointment_SelectedIndexChanging"
                                    OnPageIndexChanging="GVAppointment_PageIndexChanging" DataKeyNames="appmtID">
                                    <PagerStyle CssClass="pagination-G5" />
                                    <Columns>
                                        <asp:BoundField DataField="appmtID" HeaderText="S/N" InsertVisible="False" ReadOnly="True" SortExpression="appmtID" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                To
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LBLAppmtToID" runat="server" Text='<%# Eval("appmtToID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Pet Name
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LBLPetName" runat="server" Text="Nil"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="appmtDateTime" HeaderText="Date Time" SortExpression="appmtDateTime" />
                                        <asp:BoundField DataField="appmtDateCreated" HeaderText="Date Created" SortExpression="appmtDateCreated" />
                                        <asp:BoundField DataField="appmtStatus" HeaderText="Status" SortExpression="appmtStatus" />
                                        <asp:BoundField DataField="appmtType" HeaderText="Type" SortExpression="appmtStatus" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" Text="Edit"
                                                    Visible='<%# !isCancelled(Eval("appmtStatus").ToString()) %>'
                                                    CommandName="Select" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
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
                <div class="hr"></div>
                <%--Edit Panel--%>
                <div class="row">
                    <div class="col-xs-12">
                        <div class="form-horizontal">
                            <asp:Panel ID="PNLAppointmentEdit" runat="server" Visible="true">
                                <%--action buttons--%>
                                <div class="row">
                                    <div class="col-xs-12 ">
                                        <div class="form-inline pull-right">
                                            <asp:Button ID="BTNUpdate" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="BTNUpdate_Click" />
                                            <asp:Button ID="BTNClose" runat="server" CssClass="btn btn-primary btn-sm" Text="Close" OnClick="BTNClose_Click" />
                                        </div>
                                    </div>
                                </div>
                                <!-- /.row -->
                                <div class="space-6"></div>
                                <div class="row">
                                    <%--shop info--%>
                                    <div class="col-md-8">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h3 class="widget-title">Shop Info</h3>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="Label1" runat="server" Text="Shop Name" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="LBLShopInfoName" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="Label2" runat="server" Text="Shop Contact" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="LBLShopInfoContact" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="Label3" runat="server" Text="Shop Address" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="LBLShopInfoAddress" runat="server" Text=""></asp:Label>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="Label4" runat="server" Text="Operating Hours" Font-Bold="True"></asp:Label>
                                                        <asp:DataList ID="DLPShopTime" runat="server" DataKeyField="shopTimeID" DataSourceID="SDSShopTime" Width="100%">
                                                            <ItemTemplate>
                                                                <div class="text-right col-xs-4">
                                                                    <asp:Label ID="shopDayLabel" runat="server" Text='<%# Eval("shopDay")   %>' />
                                                                </div>
                                                                <div class="text-left col-sm-8">
                                                                    <asp:Label ID="shopOpenTimeLabel" runat="server" Text='<%#  "@ " + Eval("shopOpenTime","{0:HH:mm}") + " to " %>' />
                                                                    <asp:Label ID="shopCloseTimeLabel" runat="server" Text='<%# Eval("shopCloseTime","{0:HH:mm}") %>' />
                                                                </div>
                                                                <div class="clearfix"></div>
                                                                <div class="hr hr-6"></div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="space-6"></div>
                                    </div>
                                    <%--appointment info--%>
                                    <div class="col-md-4">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h4 class="widget-title">Basic Info</h4>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <div>
                                                        <asp:Label ID="LBLAppmtDate" runat="server" Text="Appointment Date" Font-Bold="True"></asp:Label>
                                                        <div class="input-group">
                                                            <input id="INPUTAppmtDate" runat="server" class="form-control datepicker" type="text"
                                                                data-date-format="dd-MM-yyyy" placeholder="Select date" style="padding-left: 14px;">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </span>
                                                            <asp:Button ID="BTNAppmtDate" runat="server" Style="display: none;" OnClick="BTNAppmtDate_Click" />
                                                        </div>
                                                        <asp:Label ID="LBLAppmtTime" runat="server" Text="Appointment Time" Font-Bold="True"></asp:Label>
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="DDLAppmtTime" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DDLAppmtTime_SelectedIndexChanged"
                                                                AppendDataBoundItems="true">
                                                                <asp:ListItem Value="">Select Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-clock-o"></i>
                                                            </span>
                                                        </div>
                                                        <asp:Label ID="Label5" runat="server" Text="Appointment Status" Font-Bold="True"></asp:Label>
                                                        <div class="input-group center">
                                                            <asp:Label runat="server" ID="LBLAppmtStatus" Text=""></asp:Label>
                                                        </div>
                                                        <div class="input-group">
                                                            <asp:CheckBox ID="CHKBXCancelAppointment" runat="server" Text="Cancel Appointment" CssClass=" checkbox-inline" ForeColor="Red" AutoPostBack="true" OnCheckedChanged="CHKBXCancelAppointment_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="space-6"></div>
                    </div>
                </div>
                <!--datepicker Slider starts Here-->
                <script type="text/javascript">
                    $(document).ready(function () {
                    });
                    function pageLoad() {
                        $('.datepicker').unbind()
                        $(".datepicker").datepicker({
                            autoclose: true,
                            todayHighlight: true,
                            startDate: "today",
                            constrainInput: true
                        });
                        // to make a postback after selecting date, use a button click
                        $('.datepicker').on('changeDate', function () {
                            document.getElementById('<%=BTNAppmtDate.ClientID %>').click();
                        });
                    }
                </script>
                <!--datepicker Slider ends Here-->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
