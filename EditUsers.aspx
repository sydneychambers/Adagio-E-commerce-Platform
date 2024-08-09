<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUsers.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.EditUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit User</h2>
    <hr />
    <center>
        <div style="display: inline-block; text-align: left;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUsername" runat="server" Text="Username:" AssociatedControlID="txtUsername"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth:" AssociatedControlID="txtDateOfBirth"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateOfBirth" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px" type="date"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUserType" runat="server" Text="User Type:" AssociatedControlID="txtUserType"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserType" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <p>&nbsp;</p>
            <asp:Button ID="btnUpdate" runat="server" Text="Update User" CssClass="btn btn-primary" OnClick="UpdateUser_Click"
                Style="margin: 0 auto; display: block; background-color: #CC0000;" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="Cancel_Click"
                Style="margin: 0 auto; display: block; background-color: transparent; border: none; font-weight: bold; color: #CC0000; text-decoration: underline;" />

            <p>&nbsp;</p>
            <p>
                <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="true"></asp:Label>
            </p>
        </div>
    </center>

</asp:Content>
