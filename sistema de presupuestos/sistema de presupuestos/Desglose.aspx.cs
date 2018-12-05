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
    public partial class Desglose : System.Web.UI.Page
    {
        public string Actividad;
        public string Proyecto;
        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Proyecto = Request.QueryString["Proyecto"];
            Usuario = Request.QueryString["Usuario"];
            Actividad = Request.QueryString["Actividad"];
            
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlCommand cmd1;
            if (!IsPostBack)
            {
                BindGridView();
                try
                {
                    cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                    cmd = new MySqlCommand("verTipoRecurso", cn);
                    cmd1 = new MySqlCommand("select Nombre from actividades where idActividades = " + Convert.ToInt32(Request.QueryString["Actividad"]) + "", cn);
                    cn.Open();
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.Read())
                    {
                        Label2.Text =  (Convert.ToString(reader["Nombre"])).ToUpper();
                    }
                    cn.Close();
                    cn.Open();
                    DropDownList1.DataSource = cmd.ExecuteReader();
                    DropDownList1.DataTextField = "Nombre";
                    DropDownList1.DataValueField = "ID";
                    DropDownList1.DataBind();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                }
            }
        }

        private void BindGridView(int tipoRecurso = 0)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlCommand cmd2;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("mostrarRecursos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoResource", tipoRecurso );
                cn.Open();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dataTable);
                this.GridView2.DataSource = dataTable;
                this.GridView2.DataBind();
                cn.Close();
         
                cmd2 = new MySqlCommand("mostrarDesgloce", cn);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@fkActivity", Convert.ToInt32(Request.QueryString["Actividad"]));
                cn.Open();
                DataTable dataTable2 = new DataTable();
                MySqlDataAdapter da2 = new MySqlDataAdapter(cmd2);
                da2.Fill(dataTable2);
                this.GridView1.DataSource = dataTable2;
                this.GridView1.DataBind();
                cn.Close();

            }
            catch (Exception ex)
            {
                Response.Write(ex);
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BindGridView(Convert.ToInt16(this.DropDownList1.SelectedValue));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
            
            foreach (GridViewRow dRow in this.GridView2.Rows)
            {
                CheckBox chkBox = dRow.FindControl("chkAccept") as CheckBox;
                if (chkBox != null && chkBox.Checked)
                {
                    cmd = new MySqlCommand("registrarDesgloseActividad", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FKActivity", Convert.ToInt32(Request.QueryString["Actividad"]));
                    cmd.Parameters.AddWithValue("@FKResources", Convert.ToInt32(dRow.Cells[1].Text));
                    cn.Open();
                    int res = cmd.ExecuteNonQuery();
                    if (res != 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
                    }
                    cn.Close();
                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Recursos agregados correctamente" + "');", true);
            BindGridView();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[3].Text;
                foreach (Button button in e.Row.Cells[0].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Esta seguro que desea borrar el recurso: " + item + "?')){ return false; };";
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
                cmd = new MySqlCommand("deleteDesgloce", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDesgloce", SID);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Recurso borrado correctamente" + "');", true);
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

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("updateDesgloce", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDesgloce", Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value));
                cmd.Parameters.AddWithValue("@costo", Convert.ToDouble(e.NewValues[3].ToString()));
                cmd.Parameters.AddWithValue("@cantity", Convert.ToInt32(e.NewValues[2].ToString()));
           
                cn.Open();
                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Registro actualizado correctamente" + "');", true);
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

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }

       
    }
}