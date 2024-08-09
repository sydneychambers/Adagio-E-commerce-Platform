<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Catalogue</h2>
    <hr />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="lead">
        <ContentTemplate>
            <div class="row" style="font-family: 'Raleway';">
                <asp:Repeater ID="productRepeater" runat="server" OnItemDataBound="productRepeater_ItemDataBound">
                    <ItemTemplate>
                        <div class="col col-md-4">
                            <a href="#" class="product-link" data-toggle="modal" data-target='<%# "#productModal" + Eval("ProductID") %>'>
                                <img src='<%# "Content/images/" + Eval("ImageUrl") %>' width="300" height="300" class="center-block img-fluid" />
                            </a>
                            <div class="product-details">
                                <p>
                                    &nbsp;
                                </p>
                                <p class="text-center"
                                    style="text-transform: capitalize; font-size: .85em; font-weight: 600; line-height: 1.5em; color: #212121; text-decoration: none;">
                                    <%# Eval("ProductName") %> - $<%# Eval("Price", "{0:0.00}") %>
                                </p>
                                <p><%# Eval("Description") %></p>
                                <p>&nbsp;</p>
                                <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="quantity-input"
                                    Style="width: 95px" AutoPostBack="false"
                                    data-productid='<%# Eval("ProductID") %>'
                                    data-commandargument='<%# "quantity_" + Eval("ProductID") %>'>
                                </asp:DropDownList>
                                <asp:Label ID="productIDLabel" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />

                                <p>&nbsp;</p>
                                <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="AddToCart_Click" class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
                                <p>&nbsp;</p>
                                <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click"
                                    CommandName="UpdateProduct" CommandArgument='<%# Eval("ProductID") %>'
                                    class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
                                <p>&nbsp;</p>
                                <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" ForeColor="Red" OnClick="btnDeleteProduct_Click"
                                    CommandName="DeleteProduct" CommandArgument='<%# Eval("ProductID") %>' OnClientClick="return confirm('Are you sure you want to delete this product?');"
                                    class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #FFFFFF; color: #CC0000; font-style: italic; margin-top: 0px;" />

                                <div id='<%# "confirmation" + Eval("ProductID") %>' style="display: none; color: green;">Added to Cart</div>
                                <p>
                                    <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <%# (Container.ItemIndex + 1) % 3 == 0 ? "</div><div class='row'>" : "" %>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="jumbotron" style="font-family: 'Raleway'; text-align: center; background-color: white;">
        <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProductToDB_Click"
            class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
    </div>
</asp:Content>
