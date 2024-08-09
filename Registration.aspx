<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="jumbotron" style="background-color: #FFFFFF; font-family: 'Raleway';">
            <h2>Registration</h2>
            <hr />
            <table style="margin: 0 auto; width: 600px;" class="lead">
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
                    <td style="text-align: right; height: 60px;">
                        <asp:Label ID="Label3" runat="server" Text="Re-enter Password:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" Style="width: 400px;" CssClass="borderless-textbox no-highlight"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 60px;">
                        <asp:Label ID="Label4" runat="server" Text="Age:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox4" runat="server" TextMode="Date" Style="width: 400px" CssClass="borderless-textbox no-highlight"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; height: 60px;">
                        <asp:Label ID="Label5" runat="server" Text="E-mail:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" Style="width: 400px;" CssClass="borderless-textbox no-highlight" TextMode="Email"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 60px;">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="Register_Click"
                            class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 60px;">
                        <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 60px;">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Login.aspx" Text="Already have an account? Sign in!" />
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
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Password must contain at least one capital letter and one number"
                            ForeColor="#CC0000" ValidationExpression="^(?=.*[A-Z])(?=.*\d).*$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2" ErrorMessage="Passwords do not match" ForeColor="#CC0000" ControlToValidate="TextBox3"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="TextBox5"
                            ErrorMessage="Invalid email address format."
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            Display="Dynamic" ForeColor="Red" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
