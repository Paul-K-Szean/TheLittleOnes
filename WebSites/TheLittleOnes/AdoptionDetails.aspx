<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="AdoptionDetails.aspx.cs" Inherits="uploadedFiles_AdoptionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTHLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTHLOBody" runat="Server">
    <asp:HiddenField ID="HDFAdoptInfoID" runat="server" />
    <asp:HiddenField ID="HDFPetID" runat="server" />
    <asp:HiddenField ID="HDFShopInfoID" runat="server" />
    <asp:SqlDataSource ID="SDSAdoptInfo" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo FROM ((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) WHERE (AdoptInfo.adoptInfoID = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFAdoptInfoID" Name="AdoptInfoID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SDSPhoto" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT * FROM [Photo] INNER JOIN Pet ON photo.photoownerid=pet.petid WHERE (([photoPurpose] = ?) AND ([photoOwnerID] = ?))">
        <SelectParameters>
            <asp:Parameter DefaultValue="Pet" Name="photoPurpose" Type="String" />
            <asp:ControlParameter ControlID="HDFPetID" Name="photoOwnerID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="SDSMorePet" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo FROM ((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) WHERE (AdoptInfo.shopInfoID = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFShopInfoID" Name="ShopInfoID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:DataList ID="DLAdoptInfo" runat="server" DataSourceID="SDSAdoptInfo" Width="100%"
        OnItemDataBound="DLAdoptInfo_ItemDataBound">
        <ItemTemplate>
            <!-- adopt info details -->
            <div class="gallery">
                <div class="container">
                    <div class="col-md-9">
                        <div class="agileits-about-top-heading">
                            <h3>
                                <asp:Label ID="Label2" runat="server" Text='<%# splitCamelCase( Eval("petName").ToString() )%>'></asp:Label></h3>
                        </div>
                        <div class="space-8"></div>
                        <div class="row">
                            <div class="col-md-4 col-sm-4">
                                <div class="text-center overflowHidden" style="padding:0px 10px">
                                    <asp:DataList ID="DataList2" runat="server" DataKeyField="photoID" DataSourceID="SDSPhoto"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                        <ItemTemplate>
                                            <div class="grid">
                                                <figure class="effect-apollo">
                                                    <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" data-lightbox="example-set"
                                                        data-title='<%# Eval("petdesc") %>' NavigateUrl='<%# Eval("photopath")%>'>
                                                        <div class="landscapeAdoption overflowHidden">
                                                            <asp:Image ID="imgBreedPhoto" runat="server"
                                                                ImageUrl='<%# "uploadedFiles/database/pet/" + Eval("photoOwnerID") + "/" + Eval("photoName") %>' alt="" />
                                                        </div>
                                                        <figcaption></figcaption>
                                                    </asp:HyperLink>
                                                </figure>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <div class="hr hr-double2 "></div>
                                    <div class="marginTop-0">
                                        <asp:Button ID="BTNAdoptMe" runat="server" Text="Adopt Me" CssClass="btn btn-primary" />
                                        <div class="space-6"></div>
                                        <asp:Label ID="LBLAdoptInfoStatus" runat="server" Text='<%# Eval("adoptInfoStatus") %>' Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8 col-sm-8">
                                <%--pet--%>
                                <div>
                                    <div class="hr hr-double2 "></div>
                                    <h4>
                                        <asp:Label ID="Label9" runat="server" Text="Who am I?"></asp:Label>
                                    </h4>
                                    <div class="hr hr-double2 "></div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Breed</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="petGenderLabel" runat="server" Text='<%# Eval("petBreed") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Gender</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("petGender") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Weight</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("petWeight") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Size</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("petSize") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Description</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("petDesc") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <%--Organisation info--%>
                                <div>
                                    <div class="hr hr-double2 "></div>
                                    <h4>
                                        <asp:Label ID="Label8" runat="server" Text="Organisation Info"></asp:Label>
                                    </h4>
                                    <div class="hr hr-double2 "></div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Name</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("shopinfoname") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Contact</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("shopinfocontact") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <%--Organisation info--%>
                                <div>
                                    <div class="hr hr-double2 "></div>
                                    <h4>
                                        <asp:Label ID="Label10" runat="server" Text="Organisation Info"></asp:Label>
                                    </h4>
                                    <div class="hr hr-double2 "></div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Name</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("shopinfoname") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Contact</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label12" runat="server" Text='<%# Eval("shopinfocontact") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <%--Organisation info--%>
                                <div>
                                    <div class="hr hr-double2 "></div>
                                    <h4>
                                        <asp:Label ID="Label13" runat="server" Text="Organisation Info"></asp:Label>
                                    </h4>
                                    <div class="hr hr-double2 "></div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Name</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label14" runat="server" Text='<%# Eval("shopinfoname") %>' />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right">Contact</label>
                                        <div class="col-sm-9">
                                            <div class="input-group">
                                                <asp:Label ID="Label15" runat="server" Text='<%# Eval("shopinfocontact") %>' />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <h4>
                            <asp:Label ID="Label16" runat="server" Text="Other Pets in this organisation" Font-Bold="true"></asp:Label>
                        </h4>
                        <div>
                            <asp:DataList ID="DLMorePet" runat="server" DataSourceID="SDSMorePet" Width="100%" >
                                <ItemTemplate>
                                    <div></div>
                                    <asp:Label ID="petNameLabel" runat="server" Text='<%# Eval("petName") %>' />
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
            <!-- //adopt info details -->



            shopInfoID:
            <asp:Label ID="shopInfoIDLabel" runat="server" Text='<%# Eval("shopInfoID") %>' />
            <br />
            petID:
            <asp:Label ID="petIDLabel" runat="server" Text='<%# Eval("petID") %>' />
            <br />
            adoptInfoID:
            <asp:Label ID="adoptInfoIDLabel" runat="server" Text='<%# Eval("adoptInfoID") %>' />
            <br />

        </ItemTemplate>
    </asp:DataList>
</asp:Content>
