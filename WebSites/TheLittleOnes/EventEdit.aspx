<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnesUser.master" AutoEventWireup="true" CodeFile="EventEdit.aspx.cs" Inherits="EventEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHeadUser" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBodyUser" runat="Server">
    <asp:HiddenField ID="HDFAccountID" runat="server" />
    <asp:SqlDataSource ID="SDSEvent" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [Eventt] WHERE ([accountID] = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFAccountID" Name="accountID" PropertyName="Value" Type="Int32" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>
    <div class="blog-left-right">
        <h2>Event Edit</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--Grid view--%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-header">
                            <asp:Label ID="LBLSearchResultEvent" runat="server" Text="Records for Event"></asp:Label>
                            <asp:Label ID="LBLEntriesCount" runat="server" CssClass="pull-right"></asp:Label>
                        </div>
                        <!-- div.table-responsive -->
                        <div>
                            <div id="dynamic-table_wrapper" class="dataTables_wrapper form-inline no-footer">
                                <div class="row">
                                    <div class="col-xs-4">
                                        <div class="dataTables_length" id="dynamic-table_length">
                                            Display 
                                                <asp:DropDownList ID="DDLDisplayRecordCountEvent" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLDisplayRecordCountEvent_SelectedIndexChanged">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                </asp:DropDownList>
                                            records
                                        </div>
                                    </div>
                                    <div class="col-xs-8">
                                        <div id="dynamic-table_filter" class="dataTables_filter">
                                            <span class="block input-icon input-icon-right  toolbar">
                                                <asp:DropDownList ID="DDLFilterEventType" runat="server" CssClass=" form-control" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="DDLFilterEventType_SelectedIndexChanged">
                                                    <asp:ListItem Value="">Filter Event Type</asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                            <label class="block clearfix">
                                                <span class="block input-icon input-icon-right">Search:         
                                                    <asp:TextBox ID="TBSearchEvent" runat="server" CssClass="form-control  input-sm" placeholder="EG: Day"
                                                        AutoPostBack="true" OnTextChanged="TBSearchEvent_TextChanged"
                                                        Text=""></asp:TextBox>
                                                    <i class="ace-icon fa fa-search blue bigger-110"></i>
                                                </span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="GVEvent" runat="server"
                                    CssClass="table table-striped table-bordered table-hover dataTable no-footer"
                                    AutoGenerateColumns="False" DataSourceID="SDSEvent" Width="100%" AllowPaging="True"
                                    OnDataBound="GVEvent_DataBound"
                                    OnRowDataBound="GVEvent_RowDataBound"
                                    OnSelectedIndexChanged="GVEvent_SelectedIndexChanged"
                                    OnSelectedIndexChanging="GVEvent_SelectedIndexChanging"
                                    OnPageIndexChanging="GVEvent_PageIndexChanging" DataKeyNames="eventID">
                                    <PagerStyle CssClass="pagination-G5" />
                                    <Columns>
                                        <asp:BoundField DataField="eventID" HeaderText="S/N" InsertVisible="False" ReadOnly="True" SortExpression="eventID" />
                                        <asp:BoundField DataField="eventTitle" HeaderText="Title" SortExpression="eventTitle" />
                                        <asp:BoundField DataField="eventLocation" HeaderText="Location" SortExpression="eventLocation" />
                                        <asp:BoundField DataField="eventType" HeaderText="Type" SortExpression="eventType" />
                                        <asp:BoundField DataField="eventDateTime" HeaderText="Event Date" SortExpression="eventDateTime" />
                                        <asp:BoundField DataField="eventStatus" HeaderText="Status" SortExpression="eventStatus" />
                                        <asp:CommandField ShowSelectButton="true" SelectText="Edit"/>
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
                            <asp:Panel ID="PNLEventInfoEdit" runat="server" Visible="false">
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
                                    <%--Event info--%>
                                    <div class="col-md-8">
                                        <div class="widget-box">
                                            <div class="widget-header">
                                                <h3 class="widget-title">Event Info</h3>
                                            </div>
                                            <div class="widget-body">
                                                <div class="widget-main">
                                                    <asp:HiddenField ID="HDFEventInfoID" runat="server" />
                                                    <div>
                                                        <asp:Label ID="LBLEventType" runat="server" Text="Event Type" Font-Bold="True"></asp:Label>
                                                        <asp:DropDownList ID="DDLEventType" runat="server" CssClass=" form-control">
                                                            <asp:ListItem Value=""> Select Event Type</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLEventInfoTitle" runat="server" Text="Event Title" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="TBEventInfoTitle" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLEventInfoDesc" runat="server" Text="Event Description" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="TBEventInfoDesc" runat="server" CssClass="form-control" Text="" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                    </div>
                                                    <br />
                                                    <div>
                                                        <asp:Label ID="LBLEventInfoLocation" runat="server" Text="Event Location" Font-Bold="True"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="TBEventInfoLocation" runat="server" CssClass="form-control" Text=""></asp:TextBox>
                                                    </div>
                                                    <br />
                                                
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
                                                        <asp:Label ID="LBLEventDate" runat="server" Text="Event Date" Font-Bold="True"></asp:Label>
                                                        <div class="input-group">
                                                            <input id="INPUTEventDate" runat="server" class="form-control datepicker" type="text"
                                                                data-date-format="dd-MM-yyyy" placeholder="Select date" style="padding-left: 14px;">
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </span>
                                                            <asp:Button ID="BTNEventDate" runat="server" Style="display: none;" OnClick="BTNEventDate_Click" />
                                                        </div>
                                                        <asp:Label ID="LBLEventTime" runat="server" Text="Event Time" Font-Bold="True"></asp:Label>
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="DDLEventInfoTime" runat="server" CssClass="form-control">
                                                                <asp:ListItem Value="">Select Time</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="input-group-addon">
                                                                <i class="fa fa-clock-o"></i>
                                                            </span>
                                                        </div>
                                                        <asp:Label ID="Label5" runat="server" Text="Event Status" Font-Bold="True"></asp:Label>
                                                        <div class="input-group center">
                                                            <asp:Label runat="server" ID="LBLEventInfoStatus" Text=""></asp:Label>
                                                        </div>
                                                        <div class="input-group">
                                                            <asp:CheckBox ID="CHKBXCancelEvent" runat="server" Text="Cancel Event" CssClass=" checkbox-inline" ForeColor="Red" AutoPostBack="true" OnCheckedChanged="CHKBXCancelEvent_CheckedChanged" />
                                                        </div>
                                                        <asp:Label ID="Label4" runat="server" Text="Event Date Created" Font-Bold="True"></asp:Label>
                                                        <div class="input-group">
                                                            <asp:Label ID="LBLEventInfoDateCreated" runat="server" Text="" CssClass=""/>
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
                            document.getElementById('<%=BTNEventDate.ClientID %>').click();
                        });
                    }
                </script>
                <!--datepicker Slider ends Here-->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

