﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Administrador.aspx.cs" Inherits="sistema_de_presupuestos.Administrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title></title>

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
	
	<link rel="stylesheet" href="fonts/fonts.css" />
	<script src="js/jquery-latest.js"></script>
	<script src="js/main.js"></script>



    <link rel="stylesheet" href="css/nivo-slider.css" />
    <link rel="stylesheet" href="themes/default/default.css" />
    <link rel="stylesheet" href="themes/bar/bar.css" />
    <link rel="stylesheet" href="themes/dark/dark.css" />
    <link rel="stylesheet" href="themes/light/light.css" />
   

    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/jquery.nivo.slider.js"></script>

    <script type="text/javascript">
        $(window).on('load', function () {
            $('#slider').nivoSlider();
        });
    </script>

     

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
				<li><a href="Administrador.aspx?Usuario=<%=Usuario%>"><span class="icon-home"></span>Inicio</a></li>
				<li><a href="Profesores.aspx?Usuario=<%=Usuario%>"><span class="icon-user-tie"></span>Profesores</a></li>
				<li class="submenu">
					<a href="#"><span class="icon-cogs"></span>Administrar recursos<span class=""></span></a>
					<ul class="children">
						<li><a href="Materiales.aspx?Usuario=<%=Usuario%>">Materiales<span class="icon-wrench"></span></a></li>
						<li><a href="Equipo.aspx?Usuario=<%=Usuario%>">Equipo<span class="icon-hammer"></span></a></li>
						<li><a href="Mano de obra.aspx?Usuario=<%=Usuario%>">Mano de obra<span class="icon-users"></span></a></li>
                        <li><a href="Unidades.aspx?Usuario=<%=Usuario%>">Unidades de medida<span class="icon-eyedropper"></span></a></li>
					</ul>
				</li>

                <li class="submenu">
					<a href="#"><span class="icon-book"></span>Cursos<span class=""></span></a>
					<ul class="children">
						<li><a href="Curso.aspx?Usuario=<%=Usuario%>">Crear curso <span class="icon-database"></span></a></li>
						<li><a href="Administrar curso.aspx?Usuario=<%=Usuario%>">Administrar curso<span class="icon-profile"></span></a></li>
						
					</ul>
				</li>

                
				<li><a href="Presupuestos.aspx?Usuario=<%=Usuario%>"><span class="icon-clipboard"></span>Sistema de presupuestos</a></li>
                <li><a href="Principal.aspx"><span class="icon-switch"></span>Salir</a></li>
			</ul>
		</nav>
	</header>


    <section class="contenido wrapper">

        <form id="form1" runat="server">

            <div class="slider-wrapper theme-default">
                <div id="slider" class="nivoSlider">     
                <img src="Imagenes/example-slide-1.jpg" alt="" title="#htmlcaption1" />    
                <img src="Imagenes/example-slide-2.jpg" alt="" title="#htmlcaption2" />    
                <img src="Imagenes/example-slide-3.jpg" alt="" title="#htmlcaption3" />     
                
                </div>
                <div id="htmlcaption1" class="nivo-html-caption">     
                     <h1>Sean Bienvenidos</h1>
                    <p>lorem ipsum dolor sit amen</p>
                </div>
                <div id="htmlcaption2" class="nivo-html-caption">     
                     <h1>Arcerca de nosotros</h1>
                </div>
                <div id="htmlcaption3" class="nivo-html-caption">     
                     <h1>Gracias por visitar</h1>
                </div>
            </div>



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
