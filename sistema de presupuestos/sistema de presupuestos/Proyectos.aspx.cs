using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web;

namespace sistema_de_presupuestos
{
    public partial class Proyectos : System.Web.UI.Page
    {

        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario = Request.QueryString["Usuario"];
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlCommand cmd2;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("verCursosSuscritos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuarioEncargado", Convert.ToInt32(Usuario));
                cn.Open();
                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "Curso";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
                cn.Close();
                //
                cmd2 = new MySqlCommand("mostrarProyectosUsuario", cn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@idUsuario", Convert.ToInt32(Usuario));
                cn.Open();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
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



        protected void Button1_Click1(object sender, EventArgs e)
        {
            int Usuario = Convert.ToInt32(Request.QueryString["Usuario"]);
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("registrarProyecto", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioProyecto", Usuario);
                cmd.Parameters.AddWithValue("@nombreProyecto", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@ubicacionProyecto", this.TextBox2.Text);
                cmd.Parameters.AddWithValue("@propietarioProyecto", this.TextBox3.Text);
                cmd.Parameters.AddWithValue("@descripcionProyecto", this.textarea1.Value);
                cmd.Parameters.AddWithValue("@DateInicio", this.Calendar1.SelectedDate);
                cmd.Parameters.AddWithValue("@DateFin", this.Calendar2.SelectedDate);
                cmd.Parameters.AddWithValue("@curso", this.DropDownList1.SelectedValue);   
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Proyecto registrado correctamente" + "');", true);
                }

                cn.Close();
                BindGridView();


            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            int Proyecto = Convert.ToInt32(row.Cells[2].Text);
            Response.Redirect("Proyecto especifico.aspx?Proyecto=" + Proyecto + "&Usuario=" + Usuario );
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                int SID = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Value);
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("deleteProyecto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idProject", SID);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Proyecto borrado correctamente" + "');", true);
                }
                GridView1.EditIndex = -1;
                BindGridView();
                cn.Close();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[3].Text;
                foreach (Button button in e.Row.Cells[1].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Esta seguro que desea borrar el proyecto: " + item + "?')){ return false; };";
                    }
                }
            }
        }

        

    }
}