<%@ Page Title="TheLittleOnes - Breed" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="Breed.aspx.cs" Inherits="Breed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <!-- gallery Cat-->
    <div class="gallery">
        <div class="container">
            <h2>Cat </h2>
            <div class="gallery-grids">
                <asp:DataList ID="DLPetInfoCat" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3"
                    DataSourceID="SDSPetInfoCat" DataKeyField="petInfoID"
                    OnItemDataBound="DLPetInfo_ItemDataBound" RepeatLayout="Flow">
                    <ItemTemplate>
                        <div class="col-md-4  col-sm-4  gallery-grid text-center overflowHidden">
                            <div class="space-6"></div>
                            <div class="grid">
                                <figure class="effect-apollo">
                                    <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" data-lightbox="example-set"
                                        data-title='<%# Eval("petinfodesc") %>'>
                                        <div class="landscape overflowHidden">
                                            <asp:Image ID="imgBreedPhoto" runat="server" alt="" CssClass="imageMaxHeight200" />
                                        </div>
                                        <figcaption>
                                            <p>
                                                <asp:Label ID="Label2" runat="server" Text='<%# splitCamelCase(Eval("petinfobreed").ToString()) %>'></asp:Label>
                                            </p>
                                        </figcaption>
                                    </asp:HyperLink>
                                </figure>
                            </div>
                            <asp:HyperLink ID="HYPLKPetInfoDetails" runat="server" Text='<%# splitCamelCase(Eval("petinfobreed").ToString()) %>'
                                NavigateUrl='<%# "~/BreedDetails.aspx?petinfoid=" + Eval("petinfoid") %>' Target="_blank"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //gallery -->
    <!-- gallery Dog-->
    <div class="gallery">
        <div class="container">
            <h2>Dog </h2>
            <div class="gallery-grids">
                <asp:DataList ID="DLPetInfoDog" runat="server" Width="100%" RepeatDirection="Horizontal" RepeatColumns="3"
                    DataSourceID="SDSPetInfoDog" DataKeyField="petInfoID"
                    OnItemDataBound="DLPetInfo_ItemDataBound" RepeatLayout="Flow">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-4 gallery-grid text-center overflowHidden">
                            <div class="space-6"></div>
                            <div class="grid">
                                <figure class="effect-apollo">
                                    <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" data-lightbox="example-set"
                                        data-title='<%# Eval("petinfodesc") %>'>
                                        <div class="landscape overflowHidden">
                                            <asp:Image ID="imgBreedPhoto" runat="server" alt="" CssClass="imageMaxHeight200" />
                                        </div>
                                        <figcaption>
                                            <p>
                                                <asp:Label ID="Label2" runat="server" Text='<%# splitCamelCase( Eval("petinfobreed").ToString() )%>'></asp:Label>
                                            </p>
                                        </figcaption>
                                    </asp:HyperLink>
                                </figure>
                            </div>
                            <asp:HyperLink ID="HYPLKPetInfoDetails" runat="server" Text='<%# splitCamelCase(Eval("petinfobreed").ToString()) %>'
                                NavigateUrl='<%# "~/BreedDetails.aspx?petinfoid=" + Eval("petinfoid") %>' Target="_blank"></asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //gallery -->
    <%--Cat--%>
    <asp:SqlDataSource ID="SDSPetInfoCat" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [PetInfo] WHERE ([petInfoCategory] = ?) ORDER BY [petInfoBreed]">
        <SelectParameters>
            <asp:Parameter DefaultValue="Cat" Name="petInfoCategory" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <%--Dog--%>
    <asp:SqlDataSource ID="SDSPetInfoDog" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [PetInfo] WHERE ([petInfoCategory] = ?) ORDER BY [petInfoBreed]">
        <SelectParameters>
            <asp:Parameter DefaultValue="Dog" Name="petInfoCategory" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
