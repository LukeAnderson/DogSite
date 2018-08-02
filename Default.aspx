<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DogSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Dog</h1>
    </div>
    <hr />

    <div>
        Dogs are man's best friend. They are bundles of joy. This site will offer some resources about dogs.
    </div>

    <asp:Image ID="whiteGoldenRetriever" runat="server" Height="500px" ImageUrl="~/Images/whiteGoldenRetriever.jpg" />

</asp:Content>
