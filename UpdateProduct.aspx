<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateProduct.aspx.cs" Inherits="EC1ContinuousAssessment_2005734.UpdateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron lead" style="color: #000000; background-color: #ffffff; font-family: 'Raleway';">
        <h2>Update Product</h2>
        <hr />
        <center>
            <div style="display: inline-block; text-align: left;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblProductID" runat="server" Text="Product ID:" AssociatedControlID="txtProductID"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtProductID" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProductName" runat="server" Text="Product Name:" AssociatedControlID="txtProductName"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPrice" runat="server" Text="Price:" AssociatedControlID="txtPrice"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" AssociatedControlID="txtDescription"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control borderless" ClientIDMode="Static" TextMode="MultiLine" Style="width: 300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblImageUrl" runat="server" Text="Image URL:" AssociatedControlID="txtImageUrl"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtImageUrl" runat="server" CssClass="form-control borderless" ClientIDMode="Static" Style="width: 300px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p>&nbsp;</p>
                <asp:Button ID="btnUpdate" runat="server" Text="Update Product" CssClass="btn btn-danger" OnClick="btnUpdate_Click"
                    class="btn btn-primary btn-lg" Style="border-style: none; border-color: inherit; border-width: medium; background-color: #CC0000; color: #FFFFFF; font-style: italic; margin-top: 0px;" />
                <p>&nbsp;</p>
                <p>
                    <asp:Label ID="errorMessageLabel" runat="server" ForeColor="Red" Visible="true"></asp:Label>
                </p>
            </div>
        </center>

    </div>
</asp:Content>

