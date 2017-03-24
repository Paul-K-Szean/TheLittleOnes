<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="PetClinic.aspx.cs" Inherits="PetClinic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <asp:SqlDataSource ID="SDSPetClinic" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [ShopInfo] WHERE ([shopInfoType] LIKE '%' + ? + '%')">
        <SelectParameters>
            <asp:Parameter DefaultValue="Clinic" Name="shopInfoType" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <div class="about">
        <div class="agileits-about-top">
            <div class="container">
                <div class="agileits-about-top-heading">
                    <h3>Pet Clinics</h3>
                </div>
                <div class="agileinfo-top-grids">
                    <div class="col-sm-3 wthree-top-grid">
                        <h4>Filter</h4>
                        <p>Dropdownlist</p>
                    </div>
                    <div class="col-sm-9 wthree-top-grid">
                        <div class="gallery-grids">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                                    <asp:DataList ID="DLShopInfo" runat="server" Width="100%" DataKeyField="shopInfoID" DataSourceID="SDSPetClinic"
                                        OnItemDataBound="DLShopInfo_ItemDataBound">
                                        <ItemTemplate>
                                            <%--shop info--%>
                                            <asp:HiddenField ID="HDFShopInfoID" runat="server" Value='<%# Eval("shopInfoID") %>' />
                                            <div class="widget-box">
                                                <div class="widget-header">
                                                    <h4 class="smaller">
                                                        <asp:Label ID="shopInfoNameLabel" runat="server" Text='<%# Eval("shopInfoName") %>' />
                                                    </h4>
                                                </div>
                                                <div class="widget-body">
                                                    <div class="widget-main">
                                                        <%--photos--%>
                                                        <div class="col-sm-4">
                                                            <div class="center">
                                                                <div>
                                                                    <div class="overflow-auto" style="max-width: 1110px;">
                                                                        <asp:DataList ID="DLPhoto" runat="server" Width="100%" RepeatDirection="Horizontal"
                                                                            OnItemDataBound="DLPhoto_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("photoPath").ToString().Replace("~/","")%>' Style="max-height: 200px; margin: 24px 4px" />
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-8">
                                                            <div class="input-group">
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Contact: </b>
                                                                <asp:Label ID="shopInfoContactLabel" runat="server" Text='<%# Eval("shopInfoContact") %>' />
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Address: </b>
                                                                <asp:Label ID="shopInfoAddressLabel" runat="server" Text='<%# Eval("shopInfoAddress") %>' />
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Grooming: </b>
                                                                <asp:Label ID="shopInfoGroomingLabel" runat="server" Text='<%# Eval("shopInfoGrooming") %>' />
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Description: </b>
                                                                <asp:Label ID="shopInfoDescLabel" runat="server" Text='<%# Eval("shopInfoDesc") %>' />
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Close On Public Holiday: </b>
                                                                <asp:Label ID="shopInfoCloseOnPublicHolidayLabel" runat="server" Text='<%# bool.Parse(Eval("shopInfoCloseOnPublicHoliday").ToString()) ? "Yes" : "No" %>' />
                                                            </div>
                                                            <div class="input-group">
                                                                <b>Operation Hours </b>
                                                                <asp:Label ID="LBLShopTimeStatus" runat="server" Text="Label"></asp:Label>
                                                                <asp:DataList ID="DLShopTime" runat="server" Width="100%" DataKeyField="shopTimeID" RepeatLayout="Flow">
                                                                    <ItemTemplate>
                                                                        <div class=" row">
                                                                            <div class="col-xs-4 text-right">
                                                                                <asp:Label ID="shopDayOfWeekLabel" runat="server" Text='<%# Eval("shopDayOfWeek")  %>' />
                                                                            </div>
                                                                            <div class="col-xs-8 text-left">
                                                                                <asp:Label ID="shopOpenTimeLabel" runat="server"
                                                                                    Text='<%# " @ " + DateTime.Parse(Eval("shopOpenTime").ToString()).ToString("HH:mm tt") + " to " + 
                                                                                              DateTime.Parse(Eval("shopCloseTime").ToString()).ToString("HH:mm tt")%>' />
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:DataList>
                                                            </div>
                                                        </div>
                                                        <hr>

                                                        <p>
                                                            <span class="btn btn-sm" data-rel="tooltip" title="" data-original-title="Default">Default</span>
                                                            <span class="btn btn-warning btn-sm tooltip-warning" data-rel="tooltip" data-placement="left" title="" data-original-title="Left Warning">Left</span>
                                                            <span class="btn btn-success btn-sm tooltip-success" data-rel="tooltip" data-placement="right" title="" data-original-title="Right Success">Right</span>
                                                            <span class="btn btn-danger btn-sm tooltip-error" data-rel="tooltip" data-placement="top" title="" data-original-title="Top Danger">Top</span>
                                                            <span class="btn btn-info btn-sm tooltip-info" data-rel="tooltip" data-placement="bottom" title="" data-original-title="Bottm Info">Bottom</span>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="space-6"></div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

