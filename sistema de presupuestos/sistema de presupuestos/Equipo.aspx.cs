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
    public partial class Equipo : System.Web.UI.Page
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
                cmd = new MySqlCommand("select idUnidadMedida,Nombre from unidadmedida", cn);
                cmd2 = new MySqlCommand("verEquipo", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                DropDownList1.DataSource = cmd.ExecuteReader();
                DropDownList1.DataTextField = "Nombre";
                DropDownList1.DataValueField = "idUnidadMedida";
                DropDownList1.DataBind();
                cn.Close();
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

                cmd = new MySqlCommand("insertarRecurso", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreRecurso", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@costoRecurso", Convert.ToDouble(this.TextBox2.Text));
                cmd.Parameters.AddWithValue("@FKUnidadMedida", this.DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@FKTipoRecurso", 2);
                cmd.Parameters.AddWithValue("@FKFamilia", null);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Equipo agregado correctamente" + "');", true);
                }
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
            ID.Text = row.Cells[2].Text;
            TextBox1.Text = row.Cells[3].Text;
            TextBox2.Text = row.Cells[4].Text;
            Button1.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            try
            {
                int SID = Convert.ToInt16(GridView1.DataKeys[e.RowIndex].Value);
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");
                cmd = new MySqlCommand("deleteRecurso", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRecurso", SID);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Equipo borrado correctamente" + "');", true);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            int SID = Convert.ToInt16(ID.Text);
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("actualizarRecurso", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", SID);
                cmd.Parameters.AddWithValue("@nombreRecurso", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@costoRecurso", Convert.ToDouble(this.TextBox2.Text));
                cmd.Parameters.AddWithValue("@FKUnidadMedida", this.DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@FKTipoRecurso", 2);
                cmd.Parameters.AddWithValue("@FKFamilia", null);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Equipo actualizado correctamente" + "');", true);
                }
                GridView1.EditIndex = -1;
                BindGridView();
                btnUpdate.Visible = false;
                Button1.Visible = true;
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
                        button.Attributes["onclick"] = "if(!confirm('Esta seguro que desea borrar: " + item + "?')){ return false; };";
                    }
                }
            }
        }

    }
}