<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="Adoption.aspx.cs" Inherits="Adoption" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <asp:SqlDataSource ID="SDSAdoptInfo" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath, Photo.photoPurpose 
        FROM (((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON Pet.petID = AdoptInfo.petID) INNER JOIN Photo ON Photo.photoOwnerID = Pet.petID) 
        where photo.photopurpose = 'pet' and adoptinfostatus = 'available'"></asp:SqlDataSource>
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
                        <p>Dropdownlist</p>
                    </div>
                    <div class="col-sm-9 wthree-top-grid">
                        <div class="gallery-grids">
                            <asp:DataList ID="DataList2" runat="server" DataKeyField="adoptinfoid" DataSourceID="SDSAdoptInfo"
                                RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                <ItemTemplate>
                                    <div class="col-md-4 gallery-grid text-center">
                                        <div class="grid adoption">
                                            <figure class="effect-apollo">
                                                <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link"
                                                    NavigateUrl='<%# "~/AdoptionDetails.aspx?adoptinfoid="+Eval("adoptInfoID")%>' Target="_blank">
                                                    <div class="landscapeAdoption overflowHidden">
                                                        <asp:Image ID="imgBreedPhoto" runat="server"
                                                            ImageUrl='<%# "uploadedFiles/database/pet/" + Eval("photoOwnerID") + "/" + Eval("photoName") %>' alt="" />
                                                    </div>
                                                    <figcaption>
                                                        <p>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("petname")%>'></asp:Label>
                                                        </p>
                                                    </figcaption>
                                                </asp:HyperLink>
                                            </figure>
                                        </div>
                                        <%--   <asp:LinkButton ID="LKBTNPetInfo" runat="server" Text='<%# splitCamelCase( Eval("petinfobreed").ToString() )%>'
                                        CommandName="petinfoid" CommandArgument='<%# Eval("petinfoid")%>' OnCommand="LKBTNPetInfo_Command"></asp:LinkButton>--%>
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
