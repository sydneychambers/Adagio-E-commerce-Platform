<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="font-family: 'Raleway';">
        <div class="jumbotron lead" style="background-color: #FFFFFF;">
            <h2>Sign in</h2>
            <hr />
            <center>
                <table style="margin: 0 auto;">
                    <tr>
                        <td style="text-align: right; height: 60px;">
                            <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" Style="width: 400px;" CssClass="borderless-textbox no-highlight"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; height: 60px;">
                            <asp:Label ID="Label2" runat="server" Text="Password:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Style="width: 400px;" CssClass="borderless-textbox no-highlight"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 60px;">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"
                                class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 60px;">
                            <asp:Label ID="Label3" runat="server" Text="Label" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 60px;">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Registration.aspx" Text="Don't have an account? Sign up!" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Username is required" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Password is required" ForeColor="#CC0000" ControlToValidate="TextBox2"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>
</asp:Content>

