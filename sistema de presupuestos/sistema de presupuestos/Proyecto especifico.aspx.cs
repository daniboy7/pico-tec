using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using DayPilot.Web.Ui.Events.Gantt;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace sistema_de_presupuestos
{
    public partial class Proyecto_especifico : System.Web.UI.Page
    {
        public string Proyecto;
        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Proyecto = Request.QueryString["Proyecto"];
            Usuario = Request.QueryString["Usuario"];

            DayPilotGantt1.BeforeTaskRender += DayPilotGantt1_BeforeTaskRender;

            if (!IsPostBack)
            {
                MySqlConnection cn;
                MySqlCommand cmd;
                try
                {
                    cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                    cmd = new MySqlCommand("verFechasActividades", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProyecto", Convert.ToInt32(Proyecto));
                    cn.Open();

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dataTable);

                    DayPilotGantt1.StartDate = (DateTime)dataTable.Rows[0]["Fecha Inicio"];
                    DayPilotGantt1.ScrollTo((DateTime)dataTable.Rows[0]["Fecha Fin"]);

                    cn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                }

                BindGridView();
                DayPilotGantt1.Days = 30;
                LoadTasksAndLinks();  
            }
        }

        private void LoadTasksAndLinks()
        {
            DayPilotGantt1.Tasks.Clear();
            DayPilotGantt1.Links.Clear();

            DateTime hoy = DateTime.Today;

            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("verActividadesGantt", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDProyecto", Convert.ToInt32(Proyecto));
                cn.Open();

                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);

                foreach (DataRow dr in dataTable.Rows)
                {
                    DayPilot.Web.Ui.Task group1 = new DayPilot.Web.Ui.Task((string)dr["Nombre"], Convert.ToString(dr["ID"]), (DateTime)dr["Fecha Inicio"], (DateTime)dr["Fecha Fin"]);
                    double diasTotales = ((DateTime)dr["Fecha Fin"] - (DateTime)dr["Fecha Inicio"]).TotalDays;
                    double diasTranscurridos = (hoy - (DateTime)dr["Fecha Inicio"]).TotalDays;
                    if ( diasTranscurridos < 0) {
                        diasTranscurridos = 0;
                    }
                    int porcentaje = Convert.ToInt32((100 / diasTotales) * diasTranscurridos);
                    if (porcentaje > 100) {
                        porcentaje = 100;
                    }

                    group1.Complete = porcentaje;
                    DayPilotGantt1.Tasks.Add(group1);
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }

        }

        void DayPilotGantt1_BeforeTaskRender(object sender, BeforeTaskRenderEventArgs e)
        {
            e.Box.BubbleHtml = "Nombre: " + e.Text + "<br/>Fecha Inicio: " + e.Start + "<br/>Fecha Fin: " + e.End;
        }

        private void BindGridView()
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("verProyectoEspecifico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDProject", Convert.ToInt32(Proyecto) );
                cn.Open();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                this.GridView1.DataSource = dataTable;
                this.GridView1.DataBind();
                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
            GridView1.DataBind();

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            String nombreproy = null;
            MySqlConnection cn;
            MySqlCommand cmdNameProy;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmdNameProy = new MySqlCommand("select Nombre from proyecto where idProyecto = " + Proyecto + "", cn);
                cn.Open();
                MySqlDataReader reader = cmdNameProy.ExecuteReader();
                if (reader.Read())
                {
                    nombreproy = Convert.ToString(reader["Nombre"]);
                }
                 
                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
            if (RadioButtonList1.SelectedItem.Value == "Simple")
            {
                reporteSimple(nombreproy);
            }
            else
            {
                reporteDesglosado(nombreproy);
            }
                       
        }

        protected void reporteSimple(String nombreproy)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

                //Open PDF Document to write data 
                pdfDoc.Open();

                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Escribimos el encabezado en el documento

                
                Paragraph para = new Paragraph("Nombre del proyecto: " + nombreproy);
                para.Alignment = Element.ALIGN_CENTER;
                para.Font.Size = 18;
                pdfDoc.Add(para);
                pdfDoc.Add(Chunk.NEWLINE);
                    

                pdfDoc.Add(new Paragraph("Actividades: "));
                pdfDoc.Add(Chunk.NEWLINE); 

                // Creamos una tabla que contendrá el nombre, ubicación y propietario
                // de nuestro proyecto
                PdfPTable tblProyecto = new PdfPTable(10);
                tblProyecto.WidthPercentage = 100;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
                clNombre.BorderWidth = 0;
                clNombre.BorderWidthBottom = 0.25f;

                PdfPCell clUnidad = new PdfPCell(new Phrase("Unidad", _standardFont));
                clUnidad.BorderWidth = 0;
                clUnidad.BorderWidthBottom = 0.25f;

                PdfPCell clCantidad = new PdfPCell(new Phrase("Cantidad", _standardFont));
                clCantidad.BorderWidth = 0;
                clCantidad.BorderWidthBottom = 0.25f;

                PdfPCell clFechaInicio = new PdfPCell(new Phrase("Fecha Inicio", _standardFont));
                clFechaInicio.BorderWidth = 0;
                clFechaInicio.BorderWidthBottom = 0.25f;

                PdfPCell clFechaFin = new PdfPCell(new Phrase("Fecha Fin", _standardFont));
                clFechaFin.BorderWidth = 0;
                clFechaFin.BorderWidthBottom = 0.25f;

                PdfPCell clCostoMateriales = new PdfPCell(new Phrase("Costo Materiales", _standardFont));
                clCostoMateriales.BorderWidth = 0;
                clCostoMateriales.BorderWidthBottom = 0.25f;

                PdfPCell clCostoEquipo = new PdfPCell(new Phrase("Costo Equipo", _standardFont));
                clCostoEquipo.BorderWidth = 0;
                clCostoEquipo.BorderWidthBottom = 0.25f;

                PdfPCell clCostoManoObra = new PdfPCell(new Phrase("Costo Mano de Obra", _standardFont));
                clCostoManoObra.BorderWidth = 0;
                clCostoManoObra.BorderWidthBottom = 0.25f;

                PdfPCell clCostoUnitario = new PdfPCell(new Phrase("Costo Unitario", _standardFont));
                clCostoUnitario.BorderWidth = 0;
                clCostoUnitario.BorderWidthBottom = 0.25f;

                PdfPCell clCostoTotal = new PdfPCell(new Phrase("Costo Total", _standardFont));
                clCostoTotal.BorderWidth = 0;
                clCostoTotal.BorderWidthBottom = 0.25f;

                // Añadimos las celdas a la tabla
                tblProyecto.AddCell(clNombre);
                tblProyecto.AddCell(clUnidad);
                tblProyecto.AddCell(clCantidad);
                tblProyecto.AddCell(clFechaInicio);
                tblProyecto.AddCell(clFechaFin);
                tblProyecto.AddCell(clCostoMateriales);
                tblProyecto.AddCell(clCostoEquipo);
                tblProyecto.AddCell(clCostoManoObra);
                tblProyecto.AddCell(clCostoUnitario);
                tblProyecto.AddCell(clCostoTotal);

                MySqlConnection cn;
                MySqlCommand cmd;

                try
                {
                    cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                    cmd = new MySqlCommand("verActividadesProyecto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProyecto", Convert.ToInt32(Proyecto));
                    cn.Open();

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dataTable);

                    foreach (DataRow dr in dataTable.Rows)
                    {

                        // Llenamos la tabla con información
                        clNombre = new PdfPCell(new Phrase(Convert.ToString(dr["Nombre"]), _standardFont));
                        clNombre.BorderWidth = 0;

                        clUnidad = new PdfPCell(new Phrase(Convert.ToString(dr["Unidad"]), _standardFont));
                        clUnidad.BorderWidth = 0;

                        clCantidad = new PdfPCell(new Phrase(Convert.ToString(dr["Cantidad"]), _standardFont));
                        clCantidad.BorderWidth = 0;

                        clFechaInicio = new PdfPCell(new Phrase(Convert.ToString(dr["Fecha Inicio"]), _standardFont));
                        clFechaInicio.BorderWidth = 0;

                        clFechaFin = new PdfPCell(new Phrase(Convert.ToString(dr["Fecha Fin"]), _standardFont));
                        clFechaFin.BorderWidth = 0;

                        clCostoMateriales = new PdfPCell(new Phrase(Convert.ToString(dr["Costo Materiales"]), _standardFont));
                        clCostoMateriales.BorderWidth = 0;

                        clCostoEquipo = new PdfPCell(new Phrase(Convert.ToString(dr["Costo Equipo"]), _standardFont));
                        clCostoEquipo.BorderWidth = 0;

                        clCostoManoObra = new PdfPCell(new Phrase(Convert.ToString(dr["Costo Mano de Obra"]), _standardFont));
                        clCostoManoObra.BorderWidth = 0;

                        clCostoUnitario = new PdfPCell(new Phrase(Convert.ToString(dr["Costo Unitario"]), _standardFont));
                        clCostoUnitario.BorderWidth = 0;

                        clCostoTotal = new PdfPCell(new Phrase(Convert.ToString(dr["Costo Total"]), _standardFont));
                        clCostoTotal.BorderWidth = 0;

                        // Añadimos las celdas a la tabla
                        tblProyecto.AddCell(clNombre);
                        tblProyecto.AddCell(clUnidad);
                        tblProyecto.AddCell(clCantidad);
                        tblProyecto.AddCell(clFechaInicio);
                        tblProyecto.AddCell(clFechaFin);
                        tblProyecto.AddCell(clCostoMateriales);
                        tblProyecto.AddCell(clCostoEquipo);
                        tblProyecto.AddCell(clCostoManoObra);
                        tblProyecto.AddCell(clCostoUnitario);
                        tblProyecto.AddCell(clCostoTotal);

                        
                    
                    }

                    cn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                }

                pdfDoc.Add(tblProyecto);

                //Close your PDF 
                pdfDoc.Close();

                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime 
                Response.AddHeader("content-disposition", "attachment; filename=Reporte.pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
                

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void reporteDesglosado(String nombreproy)
        {
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

            //Open PDF Document to write data 
            pdfDoc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font _actividadFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            Paragraph para = new Paragraph("Nombre del proyecto: " + nombreproy);
            para.Alignment = Element.ALIGN_CENTER;
            para.Font.Size = 18;
            pdfDoc.Add(para);
            pdfDoc.Add(Chunk.NEWLINE);

            MySqlConnection cn;
            MySqlCommand cmd;

                try
                {
                    cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                    cmd = new MySqlCommand("verActividadesProyecto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDProyecto", Convert.ToInt32(Proyecto));
                    cn.Open();

                    DataTable dataTable = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dataTable);

                    foreach (DataRow dr in dataTable.Rows)
                    {

                        pdfDoc.Add(new Paragraph("Actividad: " + Convert.ToString(dr["Nombre"])));
                        pdfDoc.Add(new Paragraph("Unidad: " + Convert.ToString(dr["Unidad"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Cantidad: " + Convert.ToString(dr["Cantidad"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Fecha Inicio: " + Convert.ToString(dr["Fecha Inicio"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Fecha Fin: " + Convert.ToString(dr["Fecha Fin"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Costo Materiales: " + Convert.ToString(dr["Costo Materiales"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Costo Equipo: " + Convert.ToString(dr["Costo Equipo"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Costo Mano de Obra: " + Convert.ToString(dr["Costo Mano de Obra"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Costo Unitario: " + Convert.ToString(dr["Costo Unitario"]), _actividadFont));
                        pdfDoc.Add(new Paragraph("Costo Total: " + Convert.ToString(dr["Costo Total"]), _actividadFont));

                        pdfDoc.Add(Chunk.NEWLINE);

                        pdfDoc.Add(new Paragraph("Desglose Materiales: "));

                        PdfPTable tblMateriales = new PdfPTable(5);
                        tblMateriales.WidthPercentage = 100;

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
                        clNombre.BorderWidth = 0;
                        clNombre.BorderWidthBottom = 0.25f;

                        PdfPCell clUnidad = new PdfPCell(new Phrase("Unidad", _standardFont));
                        clUnidad.BorderWidth = 0;
                        clUnidad.BorderWidthBottom = 0.25f;

                        PdfPCell clCantidad= new PdfPCell(new Phrase("Cantidad", _standardFont));
                        clCantidad.BorderWidth = 0;
                        clCantidad.BorderWidthBottom = 0.25f;
                        
                        PdfPCell clCostoUnitario = new PdfPCell(new Phrase("Costo Unitario", _standardFont));
                        clCostoUnitario.BorderWidth = 0;
                        clCostoUnitario.BorderWidthBottom = 0.25f;

                        PdfPCell clCostoTotal = new PdfPCell(new Phrase("Costo Total", _standardFont));
                        clCostoTotal.BorderWidth = 0;
                        clCostoTotal.BorderWidthBottom = 0.25f;

                        // Añadimos las celdas a la tabla
                        tblMateriales.AddCell(clNombre);
                        tblMateriales.AddCell(clUnidad);
                        tblMateriales.AddCell(clCantidad);
                        tblMateriales.AddCell(clCostoUnitario);
                        tblMateriales.AddCell(clCostoTotal);

                        MySqlConnection cnMateriales;
                        MySqlCommand cmdMateriales;

                        try
                        {
                            cnMateriales = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                            cmdMateriales = new MySqlCommand("mostrarDesglocePorTipo", cnMateriales);
                            cmdMateriales.CommandType = CommandType.StoredProcedure;
                            cmdMateriales.Parameters.AddWithValue("@fkActivity", Convert.ToInt32(dr["ID"]));
                            cmdMateriales.Parameters.AddWithValue("@tipo", 1 );
                            cnMateriales.Open();

                            DataTable dataTableMateriales = new DataTable();
                            MySqlDataAdapter daMateriales = new MySqlDataAdapter(cmdMateriales);
                            daMateriales.Fill(dataTableMateriales);

                            foreach (DataRow drMateriales in dataTableMateriales.Rows)
                            {
                                clNombre = new PdfPCell(new Phrase(Convert.ToString(drMateriales["Recurso"]), _standardFont));
                                clNombre.BorderWidth = 0;

                                clUnidad = new PdfPCell(new Phrase(Convert.ToString(drMateriales["Unidad"]), _standardFont));
                                clUnidad.BorderWidth = 0;

                                clCantidad = new PdfPCell(new Phrase(Convert.ToString(drMateriales["Cantidad"]), _standardFont));
                                clCantidad.BorderWidth = 0;

                                clCostoUnitario = new PdfPCell(new Phrase(Convert.ToString(drMateriales["Costo Unitario"]), _standardFont));
                                clCostoUnitario.BorderWidth = 0;

                                clCostoTotal = new PdfPCell(new Phrase(Convert.ToString(drMateriales["Costo Total"]), _standardFont));
                                clCostoTotal.BorderWidth = 0;

                                // Añadimos las celdas a la tabla
                                tblMateriales.AddCell(clNombre);
                                tblMateriales.AddCell(clUnidad);
                                tblMateriales.AddCell(clCantidad);
                                tblMateriales.AddCell(clCostoUnitario);
                                tblMateriales.AddCell(clCostoTotal);
                            }

                            cnMateriales.Close();
                            pdfDoc.Add(tblMateriales);
                           

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                        }

                        // Desglose equipo

                        pdfDoc.Add(Chunk.NEWLINE);

                        pdfDoc.Add(new Paragraph("Desglose Equipo: "));

                        PdfPTable tblEquipo = new PdfPTable(5);
                        tblEquipo.WidthPercentage = 100;

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clNombreEquipo = new PdfPCell(new Phrase("Nombre", _standardFont));
                        clNombreEquipo.BorderWidth = 0;
                        clNombreEquipo.BorderWidthBottom = 0.25f;

                        PdfPCell clUnidadEquipo = new PdfPCell(new Phrase("Unidad", _standardFont));
                        clUnidadEquipo.BorderWidth = 0;
                        clUnidadEquipo.BorderWidthBottom = 0.25f;

                        PdfPCell clCantidadEquipo = new PdfPCell(new Phrase("Cantidad", _standardFont));
                        clCantidadEquipo.BorderWidth = 0;
                        clCantidadEquipo.BorderWidthBottom = 0.25f;

                        PdfPCell clCostoUnitarioEquipo = new PdfPCell(new Phrase("Costo Unitario", _standardFont));
                        clCostoUnitarioEquipo.BorderWidth = 0;
                        clCostoUnitarioEquipo.BorderWidthBottom = 0.25f;

                        PdfPCell clCostoTotalEquipo = new PdfPCell(new Phrase("Costo Total", _standardFont));
                        clCostoTotalEquipo.BorderWidth = 0;
                        clCostoTotalEquipo.BorderWidthBottom = 0.25f;

                        // Añadimos las celdas a la tabla
                        tblEquipo.AddCell(clNombreEquipo);
                        tblEquipo.AddCell(clUnidadEquipo);
                        tblEquipo.AddCell(clCantidadEquipo);
                        tblEquipo.AddCell(clCostoUnitarioEquipo);
                        tblEquipo.AddCell(clCostoTotalEquipo);

                        MySqlConnection cnEquipo;
                        MySqlCommand cmdEquipo;

                        try
                        {
                            cnEquipo = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                            cmdEquipo = new MySqlCommand("mostrarDesglocePorTipo", cnEquipo);
                            cmdEquipo.CommandType = CommandType.StoredProcedure;
                            cmdEquipo.Parameters.AddWithValue("@fkActivity", Convert.ToInt32(dr["ID"]));
                            cmdEquipo.Parameters.AddWithValue("@tipo", 2);
                            cnEquipo.Open();

                            DataTable dataTableEquipo = new DataTable();
                            MySqlDataAdapter daEquipo = new MySqlDataAdapter(cmdEquipo);
                            daEquipo.Fill(dataTableEquipo);

                            foreach (DataRow drEquipo in dataTableEquipo.Rows)
                            {
                                clNombreEquipo = new PdfPCell(new Phrase(Convert.ToString(drEquipo["Recurso"]), _standardFont));
                                clNombreEquipo.BorderWidth = 0;

                                clUnidadEquipo = new PdfPCell(new Phrase(Convert.ToString(drEquipo["Unidad"]), _standardFont));
                                clUnidadEquipo.BorderWidth = 0;

                                clCantidadEquipo = new PdfPCell(new Phrase(Convert.ToString(drEquipo["Cantidad"]), _standardFont));
                                clCantidadEquipo.BorderWidth = 0;

                                clCostoUnitarioEquipo = new PdfPCell(new Phrase(Convert.ToString(drEquipo["Costo Unitario"]), _standardFont));
                                clCostoUnitarioEquipo.BorderWidth = 0;

                                clCostoTotalEquipo = new PdfPCell(new Phrase(Convert.ToString(drEquipo["Costo Total"]), _standardFont));
                                clCostoTotalEquipo.BorderWidth = 0;

                                // Añadimos las celdas a la tabla
                                tblEquipo.AddCell(clNombreEquipo);
                                tblEquipo.AddCell(clUnidadEquipo);
                                tblEquipo.AddCell(clCantidadEquipo);
                                tblEquipo.AddCell(clCostoUnitarioEquipo);
                                tblEquipo.AddCell(clCostoTotalEquipo);
                            }

                            cnEquipo.Close();
                            pdfDoc.Add(tblEquipo);
                            

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                        }

                        // Desglose mano de obra

                        pdfDoc.Add(Chunk.NEWLINE);

                        pdfDoc.Add(new Paragraph("Desglose Mano de Obra: "));

                        PdfPTable tblManoObra = new PdfPTable(5);
                        tblManoObra.WidthPercentage = 100;

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clNombreManoObra = new PdfPCell(new Phrase("Nombre", _standardFont));
                        clNombreManoObra.BorderWidth = 0;
                        clNombreManoObra.BorderWidthBottom = 0.25f;

                        PdfPCell clUnidadManoObra = new PdfPCell(new Phrase("Unidad", _standardFont));
                        clUnidadManoObra.BorderWidth = 0;
                        clUnidadManoObra.BorderWidthBottom = 0.25f;

                        PdfPCell clCantidadManoObra = new PdfPCell(new Phrase("Cantidad", _standardFont));
                        clCantidadManoObra.BorderWidth = 0;
                        clCantidadManoObra.BorderWidthBottom = 0.25f;

                        PdfPCell clCostoUnitarioManoObra = new PdfPCell(new Phrase("Costo Unitario", _standardFont));
                        clCostoUnitarioManoObra.BorderWidth = 0;
                        clCostoUnitarioManoObra.BorderWidthBottom = 0.25f;

                        PdfPCell clCostoTotalManoObra = new PdfPCell(new Phrase("Costo Total", _standardFont));
                        clCostoTotalManoObra.BorderWidth = 0;
                        clCostoTotalManoObra.BorderWidthBottom = 0.25f;

                        // Añadimos las celdas a la tabla
                        tblManoObra.AddCell(clNombreManoObra);
                        tblManoObra.AddCell(clUnidadManoObra);
                        tblManoObra.AddCell(clCantidadManoObra);
                        tblManoObra.AddCell(clCostoUnitarioManoObra);
                        tblManoObra.AddCell(clCostoTotalManoObra);

                        MySqlConnection cnManoObra;
                        MySqlCommand cmdManoObra;

                        try
                        {
                            cnManoObra = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                            cmdManoObra = new MySqlCommand("mostrarDesglocePorTipo", cnManoObra);
                            cmdManoObra.CommandType = CommandType.StoredProcedure;
                            cmdManoObra.Parameters.AddWithValue("@fkActivity", Convert.ToInt32(dr["ID"]));
                            cmdManoObra.Parameters.AddWithValue("@tipo", 2);
                            cnManoObra.Open();

                            DataTable dataTableManoObra = new DataTable();
                            MySqlDataAdapter daManoObra = new MySqlDataAdapter(cmdManoObra);
                            daManoObra.Fill(dataTableManoObra);

                            foreach (DataRow drManoObra in dataTableManoObra.Rows)
                            {
                                clNombreManoObra = new PdfPCell(new Phrase(Convert.ToString(drManoObra["Recurso"]), _standardFont));
                                clNombreManoObra.BorderWidth = 0;

                                clUnidadManoObra = new PdfPCell(new Phrase(Convert.ToString(drManoObra["Unidad"]), _standardFont));
                                clUnidadManoObra.BorderWidth = 0;

                                clCantidadManoObra = new PdfPCell(new Phrase(Convert.ToString(drManoObra["Cantidad"]), _standardFont));
                                clCantidadManoObra.BorderWidth = 0;

                                clCostoUnitarioManoObra = new PdfPCell(new Phrase(Convert.ToString(drManoObra["Costo Unitario"]), _standardFont));
                                clCostoUnitarioManoObra.BorderWidth = 0;

                                clCostoTotalManoObra = new PdfPCell(new Phrase(Convert.ToString(drManoObra["Costo Total"]), _standardFont));
                                clCostoTotalManoObra.BorderWidth = 0;

                                // Añadimos las celdas a la tabla
                                tblManoObra.AddCell(clNombreManoObra);
                                tblManoObra.AddCell(clUnidadManoObra);
                                tblManoObra.AddCell(clCantidadManoObra);
                                tblManoObra.AddCell(clCostoUnitarioManoObra);
                                tblManoObra.AddCell(clCostoTotalManoObra);
                            }

                            cnManoObra.Close();
                            pdfDoc.Add(tblManoObra);
                            pdfDoc.Add(Chunk.NEWLINE);

                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex);
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                        }

                    }

                    cn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                }



            //Close your PDF 
            pdfDoc.Close();

            Response.ContentType = "application/pdf";

            //Set default file Name as current datetime 
            Response.AddHeader("content-disposition", "attachment; filename=ReporteDesglosado.pdf");
            System.Web.HttpContext.Current.Response.Write(pdfDoc);

            Response.Flush();
            Response.End();

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("updateProyecto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDProject", Convert.ToInt32(Proyecto));
                cmd.Parameters.AddWithValue("@nombreProy", e.NewValues[0].ToString());
                cmd.Parameters.AddWithValue("@ubicacionProy", e.NewValues[1].ToString());
                cmd.Parameters.AddWithValue("@propietarioProy", e.NewValues[2].ToString());
                cmd.Parameters.AddWithValue("@descripcionProy", e.NewValues[5].ToString());
                cmd.Parameters.AddWithValue("@FechaInicioProy", Convert.ToDateTime(e.NewValues[3]).ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@FechaFinProy", Convert.ToDateTime(e.NewValues[4]).ToString("yyyy/MM/dd"));
                cn.Open();
                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Proyecto actualizado correctamente" + "');", true);
                }
                cn.Close();
                GridView1.EditIndex = -1;
                BindGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}