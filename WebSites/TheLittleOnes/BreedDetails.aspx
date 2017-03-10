<%@ Page Title="" Language="C#" MasterPageFile="~/MasterTheLittleOnes.master" AutoEventWireup="true" CodeFile="BreedDetails.aspx.cs" Inherits="BreedDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHTHLOHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHTHLOBody" runat="Server">
    <asp:HiddenField ID="HDFPetInfoID" runat="server" />
    <asp:SqlDataSource ID="SDSPetInfo" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionStringTheLittleOnes %>"
        ProviderName="<%$ ConnectionStrings:ConnectionStringTheLittleOnes.ProviderName %>"
        SelectCommand="SELECT PetInfo.petInfoID, PetInfo.petInfoCategory, PetInfo.petInfoBreed, PetInfo.petInfoLifeSpanMin, PetInfo.petInfoHeightMin, PetInfo.petInfoWeightMin, PetInfo.petInfoLifeSpanMax, PetInfo.petInfoHeightMax, PetInfo.petInfoWeightMax, PetInfo.petInfoDesc, PetInfo.petInfoPersonality, PetInfo.petInfoDisplayStatus, PetCharacteristics.charID, PetCharacteristics.charOverallAdaptability, PetCharacteristics.charOverallFriendliness, PetCharacteristics.charOverallGrooming, PetCharacteristics.charOverallTrainability, PetCharacteristics.charOverallExercise, PetCharacteristics.charAdaptToSurrounding, PetCharacteristics.charAdaptToNovice, PetCharacteristics.charAdaptToLoneliness, PetCharacteristics.charAdaptToCold, PetCharacteristics.charAdaptToHot, PetCharacteristics.charFriendWithFamily, PetCharacteristics.charFriendWithKids, PetCharacteristics.charFriendWithStrangers, PetCharacteristics.charFriendWithOtherPet, PetCharacteristics.charGroomShedding, PetCharacteristics.charGroomDrooling, PetCharacteristics.charGroomLevel, PetCharacteristics.charTrainLevel, PetCharacteristics.charTrainIntelligence, PetCharacteristics.charTrainMouthiness, PetCharacteristics.charTrainPreyDrive, PetCharacteristics.charTrainBarkHowl, PetCharacteristics.charExerciseEnergyLevel, PetCharacteristics.charExerciseNeeds, PetCharacteristics.charExercisePlayfullness FROM (PetInfo INNER JOIN PetCharacteristics ON PetInfo.petInfoID = PetCharacteristics.petInfoID) WHERE (PetInfo.petInfoID = ?)">
        <SelectParameters>
            <asp:ControlParameter ControlID="HDFPetInfoID" Name="petInfoID" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:DataList ID="DDLPetInfo" runat="server" DataSourceID="SDSPetInfo" DataKeyField="petInfoID" Width="100%"
        OnItemDataBound="DDLPetInfo_ItemDataBound">
        <ItemTemplate>
            <!-- pet info details -->
            <div class="about">
                <div class="agileits-about-top">
                    <div class="container">
                        <div class="agileits-about-top-heading">
                            <h3>
                                <asp:Label ID="LBLPetInfoBreed" runat="server" Text='<%# splitCamelCase(Eval("petinfobreed").ToString()) %>'></asp:Label></h3>
                        </div>
                        <div class="agileinfo-top-grids">
                            <div class="col-sm-4 wthree-top-grid">
                                <h4>Breed</h4>

                                <div class="row">
                                    <div class="form-inline">
                                        <div class="col-xs-6 col-sm-6">
                                            <asp:Label ID="Label1" runat="server" Text="Life Span"></asp:Label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Label ID="petInfoLifeSpanMinLabel" runat="server" Text='<%# Eval("petInfoLifeSpanMin") %>' />
                                            to
                                            <asp:Label ID="petInfoLifeSpanMaxLabel" runat="server" Text='<%# Eval("petInfoLifeSpanMax") %>' />
                                            YRS
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-inline">
                                        <div class="col-xs-6 col-sm-6">
                                            <asp:Label ID="Label2" runat="server" Text="Height from shoulder"></asp:Label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Label ID="petInfoHeightMinLabel" runat="server" Text='<%# Eval("petInfoHeightMin") %>' />
                                            to
                                            <asp:Label ID="petInfoHeightMaxLabel" runat="server" Text='<%# Eval("petInfoHeightMax") %>' />
                                            CM
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-inline">
                                        <div class="col-xs-6 col-sm-6">
                                            <asp:Label ID="Label3" runat="server" Text="Weight"></asp:Label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("petInfoWeightMin") %>' />
                                            to
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("petInfoWeightMax") %>' />
                                            KG
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 wthree-top-grid">
                                <h4>Description</h4>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <asp:Label ID="petInfoDescLabel" runat="server" Text='<%# Eval("petInfoDesc") %>' />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 wthree-top-grid">
                                <h4>Personality</h4>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12">
                                        <asp:Label ID="petInfoPersonalityLabel" runat="server" Text='<%# Eval("petInfoPersonality") %>' />
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>
                    </div>
                </div>
                <!-- //pet info details -->
                <!-- choose -->
                <div class="w3-agileits-choose">
                    <div class="container">
                        <div class="agileits-about-top-heading agileits-w3layouts-choose-heading">
                            <h3>Why choose
                                <asp:Label ID="Label6" runat="server" Text='<%# splitCamelCase(Eval("petinfobreed").ToString()) %>'></asp:Label>?</h3>
                        </div>
                        <div class="agile-choose-grids marginTop-0">
                            <%--                <div class="col-sm-4 agile-choose-grid">
                                <div class="choose-icon">
                                    <i class="fa fa-user" aria-hidden="true"></i>
                                </div>
                                <div class="choose-info">
                                    <h4>Suspendisse ornare vitae ex nec aliquam</h4>
                                    <p>Nunc et massa ut purus porta euismod quis eu erat. Nam ornare faucibus elit sed tempor. Quisque iaculis odio nibh, et auctor tellus rhoncus vel. Maecenas arcu neque, semper eu commodo ut, pharetra vitae erat.</p>
                                </div>
                            </div>
                            <div class="col-sm-4 agile-choose-grid">
                                <div class="choose-icon">
                                    <i class="fa fa-cogs" aria-hidden="true"></i>
                                </div>
                                <div class="choose-info">
                                    <h4>Suspendisse ornare vitae ex nec aliquam</h4>
                                    <p>Nunc et massa ut purus porta euismod quis eu erat. Nam ornare faucibus elit sed tempor. Quisque iaculis odio nibh, et auctor tellus rhoncus vel. Maecenas arcu neque, semper eu commodo ut, pharetra vitae erat.</p>
                                </div>
                            </div>
                            <div class="col-sm-4 agile-choose-grid">
                                <div class="choose-icon">
                                    <i class="fa fa-thumbs-o-up" aria-hidden="true"></i>
                                </div>
                                <div class="choose-info">
                                    <h4>Suspendisse ornare vitae ex nec aliquam</h4>
                                    <p>Nunc et massa ut purus porta euismod quis eu erat. Nam ornare faucibus elit sed tempor. Quisque iaculis odio nibh, et auctor tellus rhoncus vel. Maecenas arcu neque, semper eu commodo ut, pharetra vitae erat.</p>
                                </div>
                            </div>--%>
                            <div class="BGWhite">
                                <div class="grid_3 grid_5 wow fadeInUp animated" data-wow-delay=".5s">
                                    <h3 class="hdg text-center">
                                        <asp:Label ID="Label7" runat="server" Text="Characteristic"></asp:Label></h3>
                                    <div class="tab-content">
                                        <div class="tab-pane active" id="domprogress">
                                            <div class="progress">
                                                <div class="progress-bar progress-bar-success" style="width: 35%"><span class="sr-only">35% Complete (success)</span></div>
                                                <div class="progress-bar progress-bar-warning" style="width: 20%"><span class="sr-only">20% Complete (warning)</span></div>
                                                <div class="progress-bar progress-bar-danger" style="width: 10%"><span class="sr-only">10% Complete (danger)</span></div>
                                                <div class="progress-bar progress-bar-primary" style="width: 35%"><span class="sr-only">35% Complete (primary)</span></div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="Label8" runat="server" Text="Adaptability" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="progress">
                                                        <div id="charBarOverallAdaptability" runat="server" class="progress-bar progress-bar-success" style="width: 35%"></div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="LBLOverallAdaptability" runat="server" Text='<%# Eval("charOverallAdaptability", "{0:0.0}") %>'></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="Label9" runat="server" Text="Friendliness" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="progress">
                                                        <div id="charBarOverallFriendliness" runat="server" class="progress-bar progress-bar-warning" style="width: 20%"></div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("charOverallFriendliness", "{0:0.0}") %>'></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="Label10" runat="server" Text="Grooming" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="progress">
                                                        <div id="charBarOverallGrooming" runat="server" class="progress-bar progress-bar-danger" style="width: 10%"></div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("charOverallGrooming", "{0:0.0}") %>'></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="Label11" runat="server" Text="Trainability" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="progress">
                                                        <div id="charBarOverallTrainability" runat="server" class="progress-bar progress-bar-blueviolet" style="width: 10%"></div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("charOverallTrainability", "{0:0.0}") %>'></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-2">
                                                    <asp:Label ID="Label12" runat="server" Text="Exercise" Font-Bold="true"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="progress">
                                                        <div id="charBarOverallExercise" runat="server" class="progress-bar progress-bar-primary" style="width: 10%"></div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label16" runat="server" Text='<%# Eval("charOverallExercise", "{0:0.0}") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- //choose -->
                <!-- team -->
                <div class="team">
                    <div class="container">
                        <div class="agile_team_grids">
                            <div class="col-md-3 agile_team_grid">
                                <div class="agile_team_grid_main">
                                    <img src="images/t2.jpg" alt=" " class="img-responsive">
                                    <div class="p-mask">
                                        <ul class="top-links two">
                                            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                            <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                            <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="agile_team_grid1">
                                    <h3>Riya John</h3>
                                    <p>Lorem ipsum</p>
                                </div>
                            </div>
                            <div class="col-md-3 agile_team_grid">
                                <div class="agile_team_grid_main">
                                    <img src="images/t1.jpg" alt=" " class="img-responsive">
                                    <div class="p-mask">
                                        <ul class="top-links two">
                                            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                            <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                            <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="agile_team_grid1">
                                    <h3>Williamson </h3>
                                    <p>Consectetur </p>
                                </div>
                            </div>
                            <div class="col-md-3 agile_team_grid three">
                                <div class="agile_team_grid_main">
                                    <img src="images/t3.jpg" alt=" " class="img-responsive">
                                    <div class="p-mask">
                                        <ul class="top-links two">
                                            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                            <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                            <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="agile_team_grid1">
                                    <h3>Rosy John</h3>
                                    <p>Suscipit</p>
                                </div>
                            </div>
                            <div class="col-md-3 agile_team_grid four">
                                <div class="agile_team_grid_main">
                                    <img src="images/t4.jpg" alt=" " class="img-responsive">
                                    <div class="p-mask">
                                        <ul class="top-links two">
                                            <li><a href="#"><i class="fa fa-facebook"></i></a></li>
                                            <li><a href="#"><i class="fa fa-twitter"></i></a></li>
                                            <li><a href="#"><i class="fa fa-linkedin"></i></a></li>
                                            <li><a href="#"><i class="fa fa-google-plus"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="agile_team_grid1">
                                    <h3>David Pal</h3>
                                    <p>Malesuada </p>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                <!-- //team -->
        </ItemTemplate>
    </asp:DataList>
</asp:Content>

