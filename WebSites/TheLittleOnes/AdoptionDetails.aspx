<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="AdoptionDetails.aspx.cs" Inherits="uploadedFiles_AdoptionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTLOBody" runat="Server">
    <asp:HiddenField ID="HDFAdoptInfoID" runat="server" />
    <asp:HiddenField ID="HDFPetID" runat="server" />
    <asp:HiddenField ID="HDFShopInfoID" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--Adoption info--%>
            <asp:SqlDataSource ID="SDSAdoptInfo" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo FROM ((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) WHERE (AdoptInfo.adoptInfoID = ?)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HDFAdoptInfoID" Name="AdoptInfoID" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SDSAdoptionInfoPhoto" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                SelectCommand="SELECT * FROM [Photo] INNER JOIN Pet ON photo.photoownerid=pet.petid WHERE (([photoPurpose] = ?) AND ([photoOwnerID] = ?))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Pet" Name="photoPurpose" Type="String" />
                    <asp:ControlParameter ControlID="HDFPetID" Name="photoOwnerID" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <%--More pet--%>
            <asp:SqlDataSource ID="SDSMorePet" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                SelectCommand="SELECT AdoptInfo.shopInfoID, AdoptInfo.petID, AdoptInfo.adoptInfoID, AdoptInfo.adoptInfoStatus, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Pet.petBreed, Pet.petName, Pet.petGender, Pet.petWeight, Pet.petSize, Pet.petDesc, Pet.petEnergy, Pet.petFriendlyWithPet, Pet.petFriendlyWithPeople, Pet.petToiletTrained, Pet.petHealthInfo FROM ((AdoptInfo INNER JOIN ShopInfo ON AdoptInfo.shopInfoID = ShopInfo.shopInfoID) INNER JOIN Pet ON AdoptInfo.petID = Pet.petID) WHERE (AdoptInfo.shopInfoID = ?)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HDFShopInfoID" Name="ShopInfoID" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SDSMorePetPhoto" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                SelectCommand="SELECT * FROM [Photo] INNER JOIN Pet ON photo.photoownerid=pet.petid WHERE (([photoPurpose] = ?) AND ([photoOwnerID] = ?))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Pet" Name="photoPurpose" Type="String" />
                    <asp:ControlParameter ControlID="HDFPetID" Name="photoOwnerID" PropertyName="Value" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <%--Shop info--%>
            <asp:SqlDataSource ID="SDSShopInfoPhoto" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                SelectCommand="SELECT * FROM [Photo] INNER JOIN SHOPINFO ON PHOTO.PHOTOOWNERID = SHOPINFO.SHOPINFOID WHERE (([photoOwnerID] = ?) AND ([photoPurpose] = ?))">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HDFShopInfoID" Name="photoOwnerID" PropertyName="Value" Type="Int32" />
                    <asp:Parameter DefaultValue="ShopInfo" Name="photoPurpose" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <!-- adopt info details -->
            <div class="gallery">
                <div class="container">
                    <div class="agileits-about-top-heading">
                        <h3>Adoption Details</h3>
                    </div>
                    <div class="agileinfo-top-grids">
                        <div class="row">
                            <%--Left panel--%>
                            <div class="col-md-8">
                                <asp:DataList ID="DLAdoptInfo" runat="server" DataSourceID="SDSAdoptInfo" Width="100%"
                                    OnItemDataBound="DLAdoptInfo_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="row">
                                            <div class="col-md-4 col-sm-4">
                                                <div class="hr hr-double2 "></div>
                                                <h4>
                                                    <asp:Label ID="Label6" runat="server" Text="Pet Image" Font-Bold="true"></asp:Label></h4>
                                                <div class="hr hr-double2 "></div>
                                                <div class="text-center overflowHidden" style="padding: 0px 10px">
                                                    <asp:DataList ID="DLPet" runat="server" DataKeyField="photoID" DataSourceID="SDSAdoptionInfoPhoto"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                                        <ItemTemplate>
                                                            <div class="grid adoption">
                                                                <figure class="effect-apollo">
                                                                    <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" data-lightbox="example-set"
                                                                        data-title='<%# Eval("petdesc") %>' NavigateUrl='<%# Eval("photopath")%>'>
                                                                        <div class="landscapeAdoption overflowHidden">
                                                                            <asp:Image ID="IMGPhoto" runat="server"
                                                                                ImageUrl='<%# "uploadedFiles/database/pet/" + Eval("photoOwnerID") + "/" + Eval("photoName") %>' alt="" />
                                                                        </div>
                                                                        <figcaption></figcaption>
                                                                    </asp:HyperLink>
                                                                </figure>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    <div>
                                                        <asp:Label ID="LBLTotalRequest" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-8 col-sm-8">
                                                <%--pet--%>
                                                <div class="hr hr-double2 "></div>
                                                <h4>
                                                    <asp:Label ID="Label9" runat="server" Text="Who am I?" Font-Bold="true"></asp:Label>
                                                </h4>
                                                <div class="hr hr-double2 "></div>
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label text-right">My Name</label>
                                                    <div class="col-sm-9">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label16" runat="server" Text='<%# Eval("petname") %>' />
                                                        </div>
                                                    </div>
                                                </div>
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
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label text-right">My Health</label>
                                                    <div class="col-sm-9">
                                                        <div class="input-group">
                                                            <asp:Label ID="Label18" runat="server" Text='<%# Eval("petHealthInfo") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-4">
                                                <%--Organisation info--%>
                                                <div class="hr hr-double2 "></div>
                                                <h4>
                                                    <asp:Label ID="Label8" runat="server" Text="Shop Image" Font-Bold="true"></asp:Label>
                                                </h4>
                                                <div class="hr hr-double2 "></div>
                                                <div class="text-center overflowHidden" style="padding: 0px 10px">
                                                    <asp:DataList ID="DLShopInfoPhoto" runat="server" DataKeyField="photoID" DataSourceID="SDSShopInfoPhoto"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%">
                                                        <ItemTemplate>
                                                            <div class="grid adoption">
                                                                <figure class="effect-apollo">
                                                                    <asp:HyperLink ID="HYPLKPetInfo" runat="server" class="example-image-link" data-lightbox="example-set"
                                                                        data-title='<%# Eval("shopinfodesc") %>' NavigateUrl='<%# Eval("photopath") %>'>
                                                                        <div class="landscapeAdoption overflowHidden">
                                                                            <asp:Image ID="IMGPhoto" runat="server"
                                                                                ImageUrl='<%# "uploadedFiles/database/shopinfo/" + Eval("photoOwnerID") + "/" + Eval("photoName") %>' alt="" />
                                                                        </div>
                                                                        <figcaption></figcaption>
                                                                    </asp:HyperLink>
                                                                </figure>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                                <div class=" clearfix"></div>
                                            </div>
                                            <div class="col-md-8">
                                                <%--Organisation info--%>
                                                <div>
                                                    <div class="hr hr-double2 "></div>
                                                    <h4>
                                                        <asp:Label ID="Label13" runat="server" Text="Organisation Info" Font-Bold="true"></asp:Label>
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
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label text-right">Address</label>
                                                        <div class="col-sm-9">
                                                            <div class="input-group">
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("shopinfoaddress") %>' /><asp:Label ID="Label11" runat="server" Font-Size="Small" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label text-right">Operating Hours</label>
                                                        <div class="col-sm-9">
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
                                                    <asp:SqlDataSource ID="SDSShopTime" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                        SelectCommand="SELECT * FROM [ShopTime] WHERE ([shopInfoID] = ?)">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="HDFShopInfoID" Name="shopInfoID" PropertyName="Value" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <asp:SqlDataSource ID="SDSAdoptionInfoPhotoShopInfo" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
                                                        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
                                                        SelectCommand="SELECT ShopInfo.shopInfoID, ShopInfo.shopInfoName, ShopInfo.shopInfoContact, ShopInfo.shopInfoAddress, ShopInfo.shopInfoGrooming, ShopInfo.shopInfoType, ShopInfo.shopInfoDesc, ShopInfo.shopInfoCloseOnPublicHoliday, Photo.photoOwnerID, Photo.photoID, Photo.photoName, Photo.photoPath FROM (ShopInfo INNER JOIN Photo ON ShopInfo.shopInfoID = Photo.photoOwnerID) WHERE (ShopInfo.shopInfoID = ?)">
                                                        <SelectParameters>
                                                            <asp:ControlParameter ControlID="HDFShopInfoID" Name="photoOwnerID" PropertyName="Value" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </div>
                                            </div>
                                        </div>
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
                            </div>
                            <%--Right panel--%>
                            <div class="col-md-4 ">
                                <%--Appointment --%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="hr hr-double2 "></div>
                                        <h4>
                                            <asp:Label ID="Label2" runat="server" Text="Appointment Info" Font-Bold="true"></asp:Label>
                                        </h4>
                                        <div class="hr hr-double2 "></div>
                                        <asp:Panel ID="PNLAdoptReq" runat="server" Visible="false">
                                            <asp:Label ID="LBLAppmtDate" runat="server" Text="Appointment Date"></asp:Label>
                                            <div class="input-group">
                                                <input id="INPUTAppmtDate" runat="server" class="form-control datepicker" type="text"
                                                    data-date-format="dd-MM-yyyy" placeholder="Select date" style="padding-left: 14px;">
                                                <span class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </span>
                                                <asp:Button ID="BTNAppmtDate" runat="server" Style="display: none;" OnClick="BTNAppmtDate_Click" />
                                            </div>
                                            <asp:Label ID="LBLAppmtTime" runat="server" Text="Appointment Time"></asp:Label>
                                            <div class="input-group">
                                                <asp:DropDownList ID="DDLAppmtTime" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DDLAppmtTime_SelectedIndexChanged"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Value="">Select Time</asp:ListItem>
                                                </asp:DropDownList>
                                                <span class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </span>
                                            </div>
                                            <div class="text-center">
                                                <asp:Button ID="BTNAdoptMe" runat="server" CssClass="btn btn-primary btn-sm" Text="Adopt Me"
                                                    OnClick="BTNAdoptMe_Click" Enabled="false" />
                                                <br />
                                                <asp:Label ID="LBLErrorMsg" runat="server" Text="" Font-Size="Medium"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PNLAdoptReqExist" runat="server" Visible="false">
                                            <asp:Label ID="LBLAppmtDateTimeExist" runat="server" Text="Appointment Date/Time" Font-Bold="true"></asp:Label>
                                            <asp:HyperLink ID="HYPLKEdit" runat="server" CssClass="pull-right" Text="Edit" NavigateUrl="~/AppointmentDetails.aspx"></asp:HyperLink>
                                            <div class="input-group">
                                                <asp:Label ID="LBLAppmtDateTimeExistDetails" runat="server" Text=""></asp:Label>
                                            </div>
                                            <asp:Label ID="LBLAppmtDateTimeStatus" runat="server" Text="Appointment Status" Font-Bold="true"></asp:Label>
                                            <div class="input-group">
                                                <asp:Label ID="LBLAppmtDateTimeStatusDetails" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <%--More pet for adoption--%>
                                <div>
                                    <div class="text-center">
                                        <div class="hr hr-double2 "></div>
                                        <h4>
                                            <asp:Label ID="Label9" runat="server" Text="Other pets in this organisation" Font-Bold="true"></asp:Label>
                                        </h4>
                                        <div class="hr hr-double2 "></div>
                                    </div>
                                    <div class="gallery-grids">
                                        <asp:DataList ID="DLMorePet" runat="server" DataKeyField="adoptinfoid" DataSourceID="SDSMorePet"
                                            RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%" OnItemDataBound="DLMorePet_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="col-md-6 gallery-grid text-center">
                                                    <div class="grid adoption">
                                                        <figure class="effect-apollo">
                                                            <asp:HiddenField ID="HDFMoreAdoptInfoID" runat="server" Value='<%# Eval("adoptinfoid")%>' />
                                                            <asp:HiddenField ID="HDFMorePetID" runat="server" Value='<%# Eval("petid")%>' />
                                                            <asp:HyperLink ID="HYPLKMorePet" runat="server" class="example-image-link">
                                                                <div class="portraitAdoption overflowHidden">
                                                                    <asp:Image ID="IMGPhoto" runat="server" alt="" />
                                                                </div>
                                                                <figcaption>
                                                                    <p>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("petname")%>' Font-Size="Small"></asp:Label>
                                                                    </p>
                                                                </figcaption>
                                                            </asp:HyperLink>
                                                        </figure>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- //adopt info details -->
        </ContentTemplate>
    </asp:UpdatePanel>
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
