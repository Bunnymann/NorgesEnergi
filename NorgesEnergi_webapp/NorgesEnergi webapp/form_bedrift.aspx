<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form_bedrift.aspx.cs" Inherits="NorgesEnergi_webapp.form_bedrift" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 50%">
            <asp:Menu ID="Menu1" runat="server" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal" BackColor="#B5C7DE" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" StaticSubMenuIndent="10px">
                <DynamicHoverStyle BackColor="#284E98" ForeColor="White" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <DynamicMenuStyle BackColor="#B5C7DE" />
                <DynamicSelectedStyle BackColor="#507CD1" />
                <Items>
                    <asp:MenuItem NavigateUrl="~/form_privat.aspx" Text="Privat" Value="Privat"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/form_bedrift.aspx" Text="Bedrift" Value="Bedrift" Enabled="False"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/index.html" Text="index" Value="index"></asp:MenuItem>
                </Items>
                <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticSelectedStyle BackColor="#507CD1" />
            </asp:Menu>
        <asp:GridView ID="GridView1" runat="server" BorderWidth="0px" CellPadding="5" CellSpacing="15" Font-Bold="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="True" GridLines="None" ShowHeader="False">
        </asp:GridView>
         &nbsp;
        </div>
            <asp:Button ID="SearchBtn" runat="server" Text="Search" OnClick="SearchBtn_Click" />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </form>
</body>
</html>
