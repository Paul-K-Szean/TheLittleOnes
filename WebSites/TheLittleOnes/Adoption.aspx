<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="Adoption.aspx.cs" Inherits="Adoption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <%-- <asp:SqlDataSource ID="SDSAdoptInfo" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath, Photo.photoPurpose 
        FROM (((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON Pet.petID = AdoptInfo.petID) INNER JOIN Photo ON Photo.photoOwnerID = Pet.petID) 
        where photo.photopurpose = 'pet' and adoptinfostatus = 'available'"></asp:SqlDataSource>--%>
    <!-- adoption-->
    <div class="about">
        <!--adoptiontop -->
        <div class="agileits-about-top">
            <div class="container">
                <div class="agileits-about-top-heading">
                    <h3>Adoption</h3>
                </div>
                <div class="agileinfo-top-grids">
                    <div class="col-sm-3 wthree-top-grid">
                        <h4>Filter</h4>
                        <div class="input-group">
                            <asp:DropDownList ID="DDLFilterGender" runat="server" CssClass=" form-control">
                                <asp:ListItem Value="">Filter Gender</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-9 wthree-top-grid">
                        <div class="gallery-grids">
                            <asp:DataList ID="DLAdoption" runat="server" DataKeyField="adoptinfoid"
                                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%" OnItemDataBound="DLAdoption_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-md-4  col-sm-4  gallery-grid text-center overflowHidden">
                                        <div class="space-6"></div>
                                        <div class="grid adoption">
                                            <figure class="effect-apollo">
                                                <asp:HiddenField ID="HDFAdoptInfoID" runat="server" Value='<%# Eval("adoptInfoID") %>' />
                                                <asp:HiddenField ID="HDFShopInfoID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "shopInfoEntity.shopInfoID") %>' />
                                                <asp:HiddenField ID="HDFPetID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "petEntity.petID") %>' />
                                                <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" Target="_blank">
                                                    <div class="portraitAdoption overflowHidden">
                                                        <asp:Image ID="IMGBreedPhoto" runat="server" alt="" />
                                                    </div>
                                                    <figcaption>
                                                        <p>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "petEntity.petName") %>'></asp:Label>
                                                        </p>
                                                    </figcaption>
                                                </asp:HyperLink>
                                            </figure>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <!-- adoption-top -->
    </div>
    <!-- //adoption-->
</asp:Content>
