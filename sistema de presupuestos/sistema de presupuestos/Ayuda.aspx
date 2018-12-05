<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ayuda.aspx.cs" Inherits="sistema_de_presupuestos.Ayuda" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
	
	<link rel="stylesheet" href="fonts/fonts.css" />
	<script src="js/jquery-latest.js"></script>
	<script src="js/main.js"></script>

    <script type="text/javascript">
        $(function () {
            $(".accordion-titulo").click(function (e) {

                e.preventDefault();

                var contenido = $(this).next(".accordion-content");

                if (contenido.css("display") == "none") { //open        
                    contenido.slideDown(250);
                    $(this).addClass("open");
                }
                else { //close       
                    contenido.slideUp(250);
                    $(this).removeClass("open");
                }

            });
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
  padding-top: 40px;
}


footer {
            margin-top:16px;
            width:100%;
            background-color:#00386B;
            float:left;
            
            
        }

 #Vista{
            text-align:left;
            background-color:#00386B;
            width:895px;
            height:65px;
            
        }

/*Acordeón*/

#container-main{
    margin:40px auto;
    width:95%;
    min-width:320px;
    max-width:960px;
}

#container-main h1{
    font-weight: normal;
    text-align:left;
    background:#00386B;
    color:#fff;
    padding:20px;
    
}

.accordion-container {
    width: 100%;
    margin: 0 0 20px;
    clear:both;
}

.accordion-titulo {
    position: relative;
    display: block;
    padding: 20px;
    font-size: 24px;
    font-weight: 300;
    /*background: #00386B;*/
    background:#00386B;
    color: #fff;
    text-decoration: none;
}
.accordion-titulo.open {
    /*#16a085*/
    background: #16a085;
    color: #fff;
}
.accordion-titulo:hover {
    /*background: rgba(0,0,0,0.9);*/
    background:crimson;
}

.accordion-titulo span.toggle-icon:before {
    content:"+";
}

.accordion-titulo.open span.toggle-icon:before {
    content:"-";
}

.accordion-titulo span.toggle-icon {
    position: absolute;
    top: 10px;
    right: 20px;
    font-size: 38px;
    font-weight:bold;
}

.accordion-content {
    display: none;
    padding: 20px;
    overflow: auto;

    font-family:Arial,Helvetica, sans-serif, "Myriad Pro";
    color:#16a085;
}

.accordion-content p{
    margin:0;
}

.accordion-content img {
    display: block;
    float: left;
    margin: 0 15px 10px 0;
    width: 50%;
    height: auto;
}


@media (max-width: 767px) {
    .accordion-content {
        padding: 10px 0;
    }
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
				<li><a href="Ayuda.aspx"><span class="icon-cog"></span>Servicio de ayuda</a></li>
			</ul>
		</nav>
	</header>

    <section class="contenido wrapper">

    <form id="form1" runat="server">
        
    
        <div id="container-main">

            <h1>Servicio de ayuda</h1>

            <br />
       
    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Creación de cuenta<span class="toggle-icon"></span></a>
        <div class="accordion-content">

           <p>Para crear una cuenta : </p>
            <ol>
                <li>Abre la aplicación y toca <b>Registrarse</b>.</li>
                <li>Escribe el nombre que usas en tu vida cotidiana.</li>
                <li>Escribe tu primer apellido.</li>
                <li>Escribe tu segundo apellido.</li>
                <li>Escribe tu dirección de correo electrónico.</li>
                <li>Escribe tu número de carné.</li>
                <li>Crea una contraseña y toca <b>Registrarse</b>. </li>
            </ol>

            
        </div>  
    </div>
    
    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Registrar profesores<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para registrar un profesor : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Profesores</b>.</li>
                <li>Escribe el nombre del profesor.</li>
                <li>Escribe el primer apellido del profesor.</li>
                <li>Escribe el segundo apellido del profesor.</li>
                <li>Escribe la dirección de correo electrónico del profesor.</li>
                <li>Crea una contraseña y toca <b>Registrar</b>.</li>
            </ol>
        </div>
    </div>

   <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear Material<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un material : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Administrar recursos > Materiales</b>.</li>
                <li>Escribe el nombre del material.</li>
                <li>Selecciona la unidad de medida.</li>
                <li>Escribe el precio unitario y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>

   <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear Equipo de construcción<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un Equipo de construcción : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Administrar recursos > Equipo</b>.</li>
                <li>Escribe la descripción del Equipo de construcción.</li>
                <li>Escribe el precio unitario y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear Mano de obra<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un recurso de Mano de obra : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Administrar recursos > Mano de obra</b>.</li>
                <li>Escribe la descripción del recurso de Mano de obra.</li>
                <li>Escribe el precio unitario .</li>
                <li>Selecciona la unidad de medida y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>


    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear Unidad de medida<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear una Unidad de medida : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Administrar recursos > Unidades de medida</b>.</li>
                <li>Escribe el nombre de la unidad de medida y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>


    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear Curso<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un Curso : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Cursos > Crear curso</b>.</li>
                <li>Escribe el nombre del curso.</li>
                <li>Escribe el Semestre.</li>
                <li>Escribe el Año.</li>
                <li>Escribe el número de grupo y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Administrar Curso<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para administrar un Curso : </p>
            <ol>
                <li>Ingresa a la aplicación como Administrador y toca <b>Cursos > Administrar curso</b>.</li>
                <li>Escribe el carné del estudiante y toca <b>Buscar</b>.</li>
                <li>Selecciona el curso y toca <b>Agregar estudiante</b>.</li>
                
                
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear proyecto<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un proyecto: </p>
            <ol>
                <li>Ingresa a la aplicación y toca <b>Proyectos</b>.</li>
                <li>Escribe el nombre del proyecto.</li>
                <li>Escribe la ubicación del proyecto.</li>
                <li>Escribe el propietario del proyecto.</li>
                <li>Selecciona la fecha de inicio y la fecha de fin del proyecto y toca <b>Crear</b>.</li>
                
                
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear actividad<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear una actividad: </p>
            <ol>
                <li>Ingresa a la aplicación y toca <b>Actividades</b>.</li>
                <li>Escribe el nombre de la actividad.</li>
                <li>Selecciona la unidad de medida.</li>
                <li>Escribe la cantidad.</li>
                <li>Escribe el costo unitario y toca <b>Crear</b>.</li>
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Crear desglose<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para crear un desglose: </p>
            <ol>
                <li>Ingresa a la aplicación y toca <b>Desglose</b>.</li>
                <li>Selecciona un material de la lista de materiales.</li>
                <li>Toca <b>Agregar Material</b>.</li>
                
            </ol>
        </div>
    </div>

    <div class="accordion-container">
        <a href="#" class="accordion-titulo">Servicio de ayuda<span class="toggle-icon"></span></a>
        <div class="accordion-content">
            
           <p> Para acceder al servicio de ayuda: </p>
            <ol>
                <li>Ingresa a la aplicación y toca <b>Servicio de ayuda</b>.</li>
                <li>Selecciona <b>+</b> para ver la descripción de la opción.</li>
            
            </ol>
        </div>
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
