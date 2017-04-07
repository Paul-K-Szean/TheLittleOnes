<%@ Page Title="" Language="C#" MasterPageFile="~/MasterAdmin.master" AutoEventWireup="true" CodeFile="AdminForbidden.aspx.cs" Inherits="AdminForbidden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHHeaderMasterAdmin" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHBodyMasterAdmin" Runat="Server">
    <div class=" text-center" style="margin: 104.6px auto">
        <div class="error-container">
            <div class="well">
                <h1 class="grey lighter smaller">
                    <span class="blue bigger-125">
                        <i class="ace-icon fa fa-ban"></i>
                        403
                    </span>
                    Access is denied
                </h1>
                <hr>
                <h3 class="lighter smaller">You do not have the permission to view this directory/page or resource.
                </h3>

                <div class="space"></div>
                <hr>
                <div class=" space-18"></div>
                <div class="center">
                    <a href="javascript:history.back()" class="btn btn-grey">
                        <i class="ace-icon fa fa-arrow-left"></i>
                        Go Back
                    </a>
                    <a href="AdminDashboard.aspx" class="btn btn-primary">
                        <i class="ace-icon fa fa-home"></i>
                        Home
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

