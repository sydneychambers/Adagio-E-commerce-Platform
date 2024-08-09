<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseHistory.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.PurchaseHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron lead" style="background-color: #FFFFFF; font-family: 'Raleway';">
        <h2 style="color: #000000;">Your Purchase History</h2>
        <hr />
        <center>
            <% if (HasPurchaseHistory)
                { %>
            <asp:DropDownList ID="OrderID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OrderID_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Repeater ID="purchaseHistoryRepeater" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <tr>
                            <th>OrderID</th>
                            <th>Order Date</th>
                            <th>Product</th>
                            <th>Quantity</th>
                            <th>Subtotal</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("OrderID") %></td>
                        <td><%# Eval("OrderDateTime", "{0:yyyy-MM-dd HH:mm:ss}") %></td>
                        <td><%# Eval("ProductName") %></td>
                        <td><%# Eval("Quantity") %></td>
                        <td>$<%# Eval("Subtotal", "{0:0.00}") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

            <% }
                else
                { %>
            <p style="color: #000000; font-family: 'Raleway';">You have no purchase history.</p>
            <% } %>
            <table>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnReturn" runat="server" Text="Return" OnClick="Return_Click" CssClass="btn btn-primary"
                            Style="margin: 0 auto; display: block; background-color: #CC0000;" />
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
