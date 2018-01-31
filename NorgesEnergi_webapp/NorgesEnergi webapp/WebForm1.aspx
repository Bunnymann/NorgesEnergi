<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NorgesEnergi_webapp.WebForm1" %>

<!DOCTYPE html>
<link rel="stylesheet" type="text/css" href="css/main.css">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>NoregesEnergi</title>
</head>
    <form id="form1" runat="server">
        <input id="Hidden1" type="hidden" />
    <h1>Det nye gode hjelpesystemet til NorgesEnergi</h1>

    <div class="sidebar">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="http://youtube.com">Profil</a></li>
            <li class="mainmeny"><a href="http://googel.com">Hjelp</a></li>
            <li>
                <br />
            </li>
        </ul>
        <p>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderStyle="Outset" DataSourceID="SqlDataSource1" DataTextField="hovedkategori" DataValueField="hovedkategori" RepeatDirection="Horizontal">
            </asp:RadioButtonList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Norges_Energ_Db_connection %>" SelectCommand="SELECT [hovedkategori] FROM [hoved_kat]"></asp:SqlDataSource>
        </p>
            
  
    </div>

    <div class="staticinfo">
        <p>Test for statisk info</p><br />
        <p>Nå vises side 3</p><br />
        <p>Det statiske, tror jeg, skal være likt</p>
    </div>

    <div class="mainmeny">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="index.html">Marius</a></li>
            <li class="mainmeny"><a href="helppage2.html">Eirik</a></li>
            <li class="mainmeny"><a href="WebForm.1.aspx">Benjamin</a></li>
          
            <li></li>
            <li style="height: 34px">
            <asp:DropDownList ID="Drop_down_list_db" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="hovedkategori" DataValueField="hovedkategori">
            </asp:DropDownList>
                <br />
            </li>
        </ul>
    </div>

    <div class="info">
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp; <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="list_Db_list" runat="server" OnClick="list_Db_list_Click" Text="getDbList" Width="122px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:GridView ID="Db_GridView" runat="server" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                <EditRowStyle BorderColor="Red" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </p><br />
    </div>

    <div class="buttom">
        <br />
    </div>
    </form>
</body>
</html>
