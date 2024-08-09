<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.Account" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron lead" style="color: #000000; background-color: #ffffff; font-family: 'Raleway';">
    <h2>Your Account</h2>
    <hr />
    <table style="width: 100%;">
        <tr>
            <td style="width: 25%;">
                <img src="Content/images/3201525-200.png" alt="Profile Picture" />
            </td>
            <td style="width: 37.5%;" colspan="2">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 40%;">Username:</td>
                        <td style="width: 60%;">
                            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%;">Date of Birth:</td>
                        <td style="width: 60%;">
                            <asp:Label ID="lblDateOfBirth" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 40%;">Email:</td>
                        <td style="width: 60%">
                            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="text-align: center;">
        <asp:Button ID="btnPurchaseHistory" runat="server" Text="View Purchases" OnClick="PurchaseHistory_Click" CssClass="btn btn-danger" 
            class="btn btn-primary btn-lg" style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; 
            font-style: italic; margin-top: 0px;"/>
        <asp:Button ID="btnManageUsers" runat="server" Text="Manage Users" OnClick="ManageUsers_Click" CssClass="btn btn-danger" 
            class="btn btn-primary btn-lg" style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; 
            font-style: italic; margin-top: 0px;"/>
        <asp:Button ID="btnSignOut" runat="server" Text="Sign Out" OnClick="SignOut_Click" CssClass="btn btn-danger" 
            class="btn btn-primary btn-lg" style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; 
            font-style: italic; margin-top: 0px;"/>
    </div>
</div>
</asp:Content>

