<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="sistema_de_presupuestos.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
	
	<link rel="stylesheet" href="fonts/fonts.css" />
	<script src="js/jquery-latest.js"></script>
	<script src="js/main.js"></script>


    <style id=":">


       * {
	padding:0;
	margin:0;
	-webkit-box-sizing: border-box;
	-moz-box-sizing: border-box;
	box-sizing: border-box;
}
 
body {background:#FEFEFE;}
 
.menu_bar {
	display:none;
}
 
header {
	width: 100%;
}
 
header nav {
	background:#00386B;
	z-index:1000;
	max-width: 1000px;
	width:95%;
	margin:20px auto;
}
 
header nav ul {
	list-style:none;
}
 
header nav ul li {
	display:inline-block;
	position: relative;
}
 
header nav ul li:hover {
	background:rgba(0,0,0,0.9);
}
 
header nav ul li a {
	color:#fff;
	display:block;
	text-decoration:none;
	padding: 20px;
}
 
header nav ul li a span {
	margin-right:10px;
}
 
header nav ul li:hover .children {
	display:block;
}
 
header nav ul li .children {
	display: none;
	background:#00386B;
	position: absolute;
	width: 150%;
	z-index:1000;
}
 
header nav ul li .children li {
	display:block;
	overflow: hidden;
	border-bottom: 1px solid rgba(255,255,255,.5);
}
 
header nav ul li .children li a {
	display: block;
}
 
header nav ul li .children li a span {
	float: right;
	position: relative;
	top:3px;
	margin-right:0;
	margin-left:10px;
}
 
header nav ul li .caret {
	position: relative;
	top:3px;
	margin-left:10px;
	margin-right:0px;
}
 
@media screen and (max-width: 800px) {
	body {
		padding-top:80px;
	}
 
	.menu_bar {
		display:block;
		width:100%;
		position: fixed;
		top:0;
		background:#00386B;
	}
 
	.menu_bar .bt-menu {
		display: block;
		padding: 20px;
		color: #fff;
		overflow: hidden;
		font-size: 25px;
		font-weight: bold;
		text-decoration: none;
	}
 
	.menu_bar span {
		float: right;
		font-size: 40px;
	}
 
	header nav {
		width: 80%;
		height: calc(100% - 80px);
		position: fixed;
		right:100%;
		margin: 0;
		overflow: scroll;
	}
 
	header nav ul li {
		display: block;
		border-bottom:1px solid rgba(255,255,255,.5);
	}
 
	header nav ul li a {
		display: block;
	}
 
	header nav ul li:hover .children {
		display: none;
	}
 
	header nav ul li .children {
		width: 100%;
		position: relative;
	}
 
	header nav ul li .children li a {
		margin-left:20px;
	}
 
	header nav ul li .caret {
		float: right;
	}
}

.wrapper {
  width: 80%;
  margin: auto;
  overflow:hidden;
}

 .contenido {
  padding-top: 80px;
}



        #DivLogoPICO-TEC {
            
            margin-left: 439px;
            margin-top: 35px;

            
        }
        

        #form1 {
            margin:auto;
            width: 35%;
            max-width: 500px;
            background:#00386B;
            padding:30px;
            border-radius:7px;
            border: 1px solid rgba(0,0,0,0.2)

        }

        .input {
            display:block;
            padding:10px;
            width:100%;
            margin: 30px 0;
            font-size: 20px;
            box-sizing:border-box;
        }

        .button {
            display:block;
            padding:10px;
            width:100%;
            margin: 30px 0;
            font-size: 20px;

            margin-bottom:0;
            background:rgba(0,0,0,0.4);
            color:#fff;
            border:0;
     
            
            cursor:pointer;
            border-radius:20px;
        }
            .button:hover {
                background:rgba(0,0,0,0.9);
            }



        footer {
            margin-top:16px;
            width:100%;
            background-color:#00386B;
            float:left;
            
        }
        

        
        
        
        

    </style>
</head>
<body>

    <header>
		<div class="menu_bar">
			<a href="#" class="bt-menu"><span class="icon-menu"></span>Menú</a>
		</div>
 
		<nav>
			<ul>
				<li><a href="Principal.aspx"><span class="icon-home"></span>Inicio</a></li>
				<li><a href="Registrarse.aspx"><span class="icon-user-plus"></span>Registrarse</a></li>
				<li><a href="Ayuda.aspx"><span class="icon-cog"></span>Ayuda</a></li>
			</ul>
		</nav>
	</header>

    
    <section class="contenido wrapper">

    <div id="DivLogoPICO-TEC">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/ico.JPG" Height="303px" Width="260px"/>
    </div>
    

    <form id="form1" runat="server">


        <asp:TextBox ID="TextBox1" runat="server"  placeholder="&#128272; Email" CssClass="input"></asp:TextBox>
   
       
        <asp:TextBox ID="TextBox2" runat="server"  placeholder="&#128272; Contraseña" CssClass="input" TextMode="Password"></asp:TextBox>
   


        <asp:Button ID="Button1" runat="server" Text="Iniciar sesión"    CssClass="button" Font-Size="Large" OnClick="Button1_Click" />

    </form>






    </section>

    

   

    <footer>  
        
        
            <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/images.png"  />
           
 
        
            <p style="text-align:center;color:white;">
                Escuela de Ingeniería en Construcción
                <br />
                Versión 1.0
                <br />
                <br />
            </p>
            

    </footer>


</body>
</html>
