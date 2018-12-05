<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desglose.aspx.cs" Inherits="sistema_de_presupuestos.Desglose" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0" />
	
	<link rel="stylesheet" href="fonts/fonts.css" />
	<script src="js/jquery-latest.js"></script>
	<script src="js/main.js"></script>

     <style id=":">

         * {
	padding:0;
	-webkit-box-sizing: border-box;
	-moz-box-sizing: border-box;
	box-sizing: border-box;
             margin-left: 0;
             margin-right: 0;
             margin-top: 0;
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
             height: 767px;
         }


      

        footer {
            margin-top:16px;
            width:100%;
            background-color:#00386B;
            float:left;
            
            
        }



        #divRegistrar {
            text-align:left;
            background-color:#00386B;
            width:895px;
            height:65px;
        }

        #Vista{
            text-align:left;
            background-color:#00386B;
            width:895px;
            height:65px;
            
        }


        #formulario {
            width:87%;
            border:1px solid #ccc;
            margin:20px;
            padding:20px;
        }

        .label {
            font-size:20px;
            display:block;
            font-family: sans-serif;
        }


        

        .input {
            margin-bottom:20px;
            width:50%;
            padding:10px;
            box-sizing:content-box;
            border:1px solid #ccc;

            box-sizing:border-box;
        }

            .input:focus {
                border:1px solid #1668C4;
            }
        .button {
            margin-bottom:0;
            background:#00386B;
            color:#fff;
            border:none;
            width:50%;
            height:50px;

            cursor:pointer;
            border-radius:20px;
        }
            .button:hover {
                background:#1668C4;
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
				<li><a href="Presupuestos.aspx?Usuario=<%=Usuario%>"><span class="icon-home"></span>Inicio</a></li>
				<li><a href="Proyectos.aspx?Usuario=<%=Usuario%>"><span class="icon-briefcase"></span>Proyectos</a></li>
				
                <li><a href="Actividades.aspx?Proyecto=<%=Proyecto%>&Usuario=<%=Usuario%>"><span class="icon-pushpin"></span>Actividades</a></li>
				<li><a href="Desglose.aspx?Actividad=<%=Actividad%>&Proyecto=<%=Proyecto%>&Usuario=<%=Usuario%>"><span class="icon-stack"></span>Desglose</a></li>
				<li><a href="Ayuda.aspx?Usuario=<%=Usuario%>"><span class="icon-cog"></span>Ayuda</a></li>
                <li><a href="Principal.aspx"><span class="icon-switch"></span>Salir</a></li>
			</ul>
		</nav>
	</header>


     <section class="contenido wrapper">

         <form id="form1" runat="server">

         <div id="divRegistrar">
            <br />
            <p style="text-align:justify;color:white;font: 150% sans-serif;margin-left:20px; width: 763px;">CREAR DESGLOSE DE ACTIVIDAD:
                <asp:Label ID="Label2" runat="server"></asp:Label>
             </p>
        </div>

            <div id="formulario">

                <asp:Label ID="Label1" runat="server" Text="Recursos : " CssClass="label" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Value='0'>&lt;Todos&gt;</asp:ListItem>
                </asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;

                <asp:Button ID="Button2" runat="server" Text="Filtrar" CssClass="button"  Font-Size="Large" OnClick="Button2_Click" />
    
                <br />

                <br />

             <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="GridView2_RowDataBound" >
                 <Columns>
                     <asp:TemplateField >
                     <ItemTemplate>
                        <asp:CheckBox ID="chkAccept" runat="server" EnableViewState="true"  />
                     </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                 <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                 <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                 <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                 <RowStyle BackColor="White" ForeColor="#003399" />
                 <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                 <SortedAscendingCellStyle BackColor="#EDF6F6" />
                 <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                 <SortedDescendingCellStyle BackColor="#D6DFDF" />
                 <SortedDescendingHeaderStyle BackColor="#002876" />
             </asp:GridView>


                <br />
                <br />

                <asp:Button ID="Button1" runat="server" Text="Agregar Recursos" CssClass="button"  Font-Size="Large" OnClick="Button1_Click" />
    
            </div>


        <div id="Vista">
            <br />
            <p style="text-align:justify;color:white;font: 150% sans-serif;margin-left:20px;">DESGLOSE </p>
        </div>
             <br />
             <asp:GridView ID="GridView1" DataKeyNames="ID" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDeleting="GridView1_RowDeleting" OnRowDataBound = "OnRowDataBound" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit">
                 <Columns>
                     <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                     <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                 </Columns>
                 <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                 <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                 <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                 <RowStyle BackColor="White" ForeColor="#003399" />
                 <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                 <SortedAscendingCellStyle BackColor="#EDF6F6" />
                 <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                 <SortedDescendingCellStyle BackColor="#D6DFDF" />
                 <SortedDescendingHeaderStyle BackColor="#002876" />
             </asp:GridView>


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