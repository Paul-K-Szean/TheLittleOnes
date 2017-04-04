<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnesUser.master" AutoEventWireup="true" CodeFile="EventAdd.aspx.cs" Inherits="EventAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHeadUser" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBodyUser" runat="Server">
    
    <div class="page-content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h1>Add Event 
                </h1>
                <hr />
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
                                <%--event info--%>
                                <div class="col-md-6">
                                    <div class="widget-box">
                                        <div class="widget-header">
                                            <h4 class="widget-title">Event Info</h4>
                                        </div>
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <div>
                                                    <asp:Label ID="LBLEventType" runat="server" Text="Event Type" Font-Bold="True"></asp:Label>
                                                    <asp:DropDownList ID="DDLEventType" runat="server" CssClass=" form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="DDLEventType_SelectedIndexChanged">
                                                        <asp:ListItem Value=""> Select Event Type</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <br /> 
                                                <asp:Label ID="LBLEventDate" runat="server" Text="Event Date" Font-Bold="True"></asp:Label>
                                                <div class="input-group">
                                                    <input id="INPUTEventDate" runat="server" class="form-control datepicker" type="text"
                                                        data-date-format="dd-MM-yyyy" placeholder="Select date" style="padding-left: 14px;" autocomplete="off">
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-calendar"></i>
                                                    </span>
                                                    <asp:Button ID="BTNAppmtDate" runat="server" Style="display: none;" OnClick="BTNAppmtDate_Click" />
                                                </div>
                                                <asp:Label ID="LBLEventTime" runat="server" Text="Event Time" Font-Bold="True"></asp:Label>
                                                <div class="input-group">
                                                    <asp:DropDownList ID="DDLEventTime" runat="server" CssClass="form-control"
                                                        AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DDLAppmtTime_SelectedIndexChanged"
                                                        AppendDataBoundItems="true">
                                                        <asp:ListItem Value="">Select Time</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <span class="input-group-addon">
                                                        <i class="fa fa-clock-o"></i>
                                                    </span>
                                                </div>
                                                <div>
                                                    <asp:Label ID="LBLEventLocation" runat="server" Text="Location" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBEventLocation" runat="server" CssClass="form-control" placeholder="EG: Yay Nay Ave Kay"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLEventTitle" runat="server" Text="Title" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBEventTitle" runat="server" CssClass="form-control" placeholder="EG: Yay Nay"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div>
                                                    <asp:Label ID="LBLEventDesc" runat="server" Text="Description" Font-Bold="True"></asp:Label>
                                                    <asp:TextBox ID="TBEventDesc" runat="server" CssClass="form-control" placeholder="EG: Yay Nay Hay Day" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--photo--%>
                                <div class="col-md-6">
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
                                                <div id="photoPreview" runat="server" class="center scrollbarVertical">
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
</asp:Content>

