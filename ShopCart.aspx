<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopCart.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.ShopCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron lead" style="background-color: #FFFFFF; font-family: 'Raleway';">
        <h2 style="color: #000000;">Your Shopping Cart</h2>
        <hr />
        <center>
            <% if (sessionCart.Count == 0)
                { %>
            <p style="color: #000000; font-family: 'Raleway';">Your shopping cart is empty.</p>
            <% }
                else
                { %>
            <asp:UpdatePanel ID="updatePanelCart" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="cartRepeater" runat="server">
                        <HeaderTemplate>
                            <table class="table table-striped">
                                <tr>
                                    <th>Product</th>
                                    <th>Quantity</th>
                                    <th>Unit Price</th>
                                    <th>Subtotal</th>
                                    <th></th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("ProductName") %></td>
                                <td>
                                    <asp:DropDownList ID="ddlQuantity" runat="server" AutoPostBack="true" OnSelectedIndexChanged="UpdateQuantity_Click"
                                        data-commandargument='<%# Eval("ProductID") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>$<%# Eval("Price", "{0:0.00}") %></td>
                                <td>$<%# (Convert.ToDouble(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("0.00") %></td>
                                <td>
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-danger"
                                        OnClick="RemoveFromCart_Click"
                                        data-commandargument='<%# Eval("ProductID") %>'
                                        Style="background-color: transparent; border: none; font-weight: bold; color: #CC0000; text-decoration: underline;" />

                                </td>
                            </tr>
                        </ItemTemplate>

                        <FooterTemplate>
                            <tr>
                                <td colspan="3" style="text-align: right;">Grand Total:</td>
                                <td>$<%# ShowGrandTotal() %></td>
                                <td></td>
                            </tr>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                </ContentTemplate>
            </asp:UpdatePanel>
            <% } %>
        </center>
    </div>
    <div class="jumbotron" style="font-family: 'Raleway'; text-align: center; background-color: white;">
        <asp:Button ID="btnClearCart" runat="server" Text="Clear Cart" OnClick="ClearCart_Click"
            class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
        <asp:Button ID="btnCheckout" runat="server" Text="Checkout" OnClick="Checkout_Click"
            class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
    </div>
</asp:Content>
