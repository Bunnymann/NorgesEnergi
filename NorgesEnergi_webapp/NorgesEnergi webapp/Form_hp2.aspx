<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form_hp2.aspx.cs" Inherits="NorgesEnergi_webapp.Views.Form_hp2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<html>
<link rel="stylesheet" type="text/css" href="css/main.css">
<head>
    <title>NoregesEnergi</title>
</head>
    <form id="form1" runat="server">
    

    <title>NorgesEnergi</title>
    <style type="text/css">
        #Text1 {
            height: 141px;
            width: 704px;
            margin-top: 64px;
        }
    </style>
</head>
<body>
    <h1>Det nye hjelpesystemet til NorgesEnergi</h1>

    <div class="sidebar">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="http://youtube.com">Profil</a></li>
            <li class="mainmeny"><a href="http://googel.com">Hjelp</a></li>
        </ul>
        <p>Test av sidebar info</p><br />
        <p>Nå vises side 2</p><br />
        <p>Profilen er den samme for begge sider</p>
    </div>

    <div class="staticinfo">
        <p>Test for statisk info</p><br />
        <p>Nå vises side 2</p><br />
        <p>Det statiske, tror jeg, skal være likt</p>
    </div>

    <div class="mainmeny">
        <ul class="mainmeny">
            <li class="mainmeny"><a href="index.html">Marius</a></li>
            <li class="mainmeny"><a href="helppage2.html">Eirik</a></li>
            <li class="mainmeny"><a href="WebForm1.aspx">Benjamin</a></li>
        </ul>
    </div>

    <ul class="info">
         <li>
                <p>
                    <asp:GridView ID="Text1" runat="server">
                    </asp:GridView>
                </p>
            </li>
            
        </ul>

    <div class="buttom"> 
        <br />
    </div> 
    </form>
</body>
</html>