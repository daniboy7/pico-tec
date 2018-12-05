using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace sistema_de_presupuestos
{
    public partial class Unidades : System.Web.UI.Page
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
            try
            {

                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("verUnidadesMedida", cn);

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

                cmd = new MySqlCommand("insertarUnidadMedida", cn);
                
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreUnidad", this.TextBox1.Text);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unidad agregada correctamente" + "');", true);
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
                cmd = new MySqlCommand("deleteUnidadMedida", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUnidad", SID);
                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                  
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unidad borrada correctamente" + "');", true);
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

                cmd = new MySqlCommand("actualizarUnidadMedida", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombreUnidad", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@idUnidad", SID);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unidad actualizada correctamente" + "');", true);
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