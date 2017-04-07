<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnesUser.master" AutoEventWireup="true" CodeFile="UserDetails.aspx.cs" Inherits="AccountProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHeadUser" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBodyUser" Runat="Server">
    <div class="blog-left-right">
        <h2>Account And Profile Details</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--action buttons--%>
                <div class="row">
                    <div class="col-xs-12 ">
                        <div class="form-inline pull-right">
                            <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                            <asp:Button ID="BTNSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Save" OnClick="BTNSave_Click" />
                        </div>
                    </div>
                </div>
                <!-- /.row -->
                <div class="space-6"></div>
                <div class="row">
                    <%--account info--%>
                    <div class="col-md-6">
                        <div class="widget-box">
                            <div class="widget-header">
                                <h4 class="widget-title">Account Info</h4>
                            </div>
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div>
                                        <asp:Label ID="LBLAccountID" runat="server" Text="Account ID" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBAccountID" runat="server" CssClass="form-control " placeholder="Account ID" disabled="disabled"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLEmail" runat="server" Text="Email" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBAccountEmail" runat="server" CssClass="form-control " placeholder="Email" disabled="disabled"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLAccountType" runat="server" Text="Account Type" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBAccountType" runat="server" CssClass="form-control " placeholder="Account Type" disabled="disabled"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLPasswordOld" runat="server" Text="Old Password" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBAccountPasswordOld" runat="server" CssClass="form-control" placeholder="Old Password" type="password"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLPasswordNew" runat="server" Text="New Password" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBAccountPasswordNew" runat="server" CssClass="form-control" placeholder="New Password" type="password"></asp:TextBox>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--profile info--%>
                    <div class="col-md-6">
                        <div class="widget-box">
                            <div class="widget-header">
                                <h4 class="widget-title">Profile Info</h4>
                            </div>
                            <div class="widget-body">
                                <div class="widget-main">
                                    <div>
                                        <asp:Label ID="LBLProfileID" runat="server" Text="Profile ID" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBProfileID" runat="server" CssClass="form-control " placeholder="Profile ID" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLProfileName" runat="server" Text="Name" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBProfileName" runat="server" CssClass="form-control " placeholder="Name" ></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLProfileContact" runat="server" Text="Contact" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBProfileContact" runat="server" CssClass="form-control " placeholder="Contact" MaxLength="8"></asp:TextBox>
                                    </div>
                                    <br />
                                    <div>
                                        <asp:Label ID="LBLProfileAddress" runat="server" Text="Address" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="TBProfileAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                    </div>
                                    <br />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

