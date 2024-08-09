<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron lead" style="background-color: #FFFFFF; font-family: 'Raleway';">
        <h2 style="color: #000000;">Manage Users</h2>
        <hr />
        <center>
            <% if (HasUsers)
                { %>
            <asp:Repeater ID="userRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>Username</th>
                            <th>Email</th>
                            <th>Date of Birth</th>
                            <th>User Type</th>
                            <th></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Username") %></td>
                        <td><%# Eval("Email") %></td>
                        <td><%# Eval("DateOfBirth", "{0:yyyy-MM-dd}") %></td>
                        <td><%# Eval("UserType") %></td>
                        <td>
                            <asp:Button ID="btnEditUsers" runat="server" Text="Edit" CssClass="btn btn-primary"
                                OnClick="EditUsers_Click"
                                CommandArgument='<%# Eval("Username") %>'
                                Style="background-color: transparent; border: none; font-weight: bold; color: black; text-decoration: underline; outline: none;" />

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <% }
                else
                { %>
            <p style="color: #000000; font-family: 'Raleway';">There are no existing users.</p>
            <% } %>
            <table>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnReturn" runat="server" Text="Return" OnClick="Return_Click" CssClass="btn btn-primary"
                            Style="margin: 0 auto; display: block; background-color: #CC0000;" />
                    </td>
                </tr>
            </table>
        </center>
    </div>

</asp:Content>
