<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmationPage.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.ConfirmationPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron lead" style="background-color: #FFFFFF; font-family: 'Raleway';">
        <h2 style="color: #000000;">Checkout</h2>
        <hr />
        <center>
            <p style="color: #000000; font-family: 'Raleway';">Order completed successfully!</p>
            <p>&nbsp;</p>
            <p>
                <asp:Button ID="btnShopAgain" runat="server" Text="Shop Again!" OnClick="ShopAgain_Click"
                    class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
            </p>
        </center>
    </div>
</asp:Content>
