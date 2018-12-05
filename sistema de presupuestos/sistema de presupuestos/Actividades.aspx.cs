using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace sistema_de_presupuestos
{
    public partial class Actividades : System.Web.UI.Page
    {

        public string Proyecto;
        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Proyecto = Request.QueryString["Proyecto"];
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
                cmd = new MySqlCommand("select idUnidadMedida,Nombre from unidadmedida", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "Nombre";
                DropDownList1.DataValueField = "idUnidadMedida";
                DropDownList1.DataBind();
                cn.Close();
                cmd2 = new MySqlCommand("verActividadesProyecto", cn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@IDProyecto", Convert.ToInt32(Request.QueryString["Proyecto"]));
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;

            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("registrarActividad", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreActividad", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@FKProyect", Convert.ToInt32(Request.QueryString["Proyecto"]));
                cmd.Parameters.AddWithValue("@FKUniMedida", this.DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@cantidadAct", Convert.ToInt32(this.TextBox2.Text));
                cmd.Parameters.AddWithValue("@DateInicio", this.Calendar1.SelectedDate);
                cmd.Parameters.AddWithValue("@DateFin", this.Calendar2.SelectedDate);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Actividad registrada correctamente" + "');", true);
                }
                BindGridView();
                cn.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[4].Text;
                foreach (Button button in e.Row.Cells[0].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Esta seguro que desea borrar la actividad: " + item + "?')){ return false; };";
                    }
                }
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                int SID = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Value);
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("deleteActividad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idActividad", SID);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Actividad borrada correctamente" + "');", true);
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

        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            int Actividad = Convert.ToInt32(row.Cells[3].Text);
            Response.Redirect("Desglose.aspx?Actividad=" + Actividad + "&Proyecto=" + Proyecto + "&Usuario=" + Usuario);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                ID.Text = row.Cells[3].Text;
                TextBox1.Text = row.Cells[4].Text;
                TextBox2.Text = row.Cells[6].Text;
                Calendar1.SelectedDate = Convert.ToDateTime(row.Cells[7].Text);
                Calendar2.SelectedDate = Convert.ToDateTime(row.Cells[8].Text);
                Button1.Visible = false;
                btnUpdate.Visible = true;
            }
            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            int SID = Convert.ToInt32(ID.Text);
            Response.Write(SID);
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("updateActividad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDActividad", SID);
                cmd.Parameters.AddWithValue("@nombreAct", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@unidad", this.DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@cantidadAct", Convert.ToInt32(this.TextBox2.Text));
                cmd.Parameters.AddWithValue("@FechaInicioAct", this.Calendar1.SelectedDate );
                cmd.Parameters.AddWithValue("@FechaFinAct", this.Calendar2.SelectedDate);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Actividad actualizada correctamente" + "');", true);
                }
                GridView1.EditIndex = -1;
                BindGridView();
                btnUpdate.Visible = false;
                Button1.Visible = true;
                cn.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }

    }
}