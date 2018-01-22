<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="NorgesEnergi_webapp.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<html>
<link rel="stylesheet" type="text/css" href="css/main.css">
<head>
    <title>NoregesEnergi</title>
</head>
    <form id="form1" runat="server">
    <h1>Det nye gode hjelpesystemet til NorgesEnergi</h1>

    <div class="sidebar">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="http://youtube.com">Profil</a></li>
            <li class="mainmeny"><a href="http://googel.com">Hjelp</a></li>
            <li>
                </asp:FormView>
            </li>
            <li>
            </li>
            <li>
                <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" DataTextField="hovedkategori" DataValueField="hovedkategori"></asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Norges_EnergiConnectionString %>" SelectCommand="SELECT [hovedkategori] FROM [hoved_kat]"></asp:SqlDataSource>
            </li>
            
        </ul>

    </div>
    <div class="staticinfo">
        <p>Test for statisk info</p><br />
        <p>Nå vises side 1</p><br />
        <p>Det statiske, tror jeg, skal være likt</p>
    </div>

    <div class="mainmeny">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="index.html">Marius</a></li>
            <li class="mainmeny"><a href="helppage2.html">Eirik</a></li>
            <li class="mainmeny"><a href="WebForm.1.aspx">Benjamin</a></li>
        </ul>
    </div>

    <div class="info">
        <p>Test for meny relatert info</p><br />
        <p>Nå vises side 1</p><br />
        <p>Her vil informasjonen variere iht. siden</p>
    </div>

    <div class="buttom">
        <br />
    </div>
    </form>
</body>
</html>
