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
    public partial class Profesores : System.Web.UI.Page
    {

        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario = Request.QueryString["Usuario"];
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;

            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("RegistrarUsuario", cn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tipoUsuario", 2);
                cmd.Parameters.AddWithValue("@nombre", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@apellido1", this.TextBox2.Text);
                cmd.Parameters.AddWithValue("@apellido2", this.TextBox3.Text);
                cmd.Parameters.AddWithValue("@email", this.TextBox4.Text);
                cmd.Parameters.AddWithValue("@carne", null);
                cmd.Parameters.AddWithValue("@pass", this.TextBox5.Text);

                cn.Open();

                int res = cmd.ExecuteNonQuery();

                if (res == 1)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Registrado Correctamente" + "');", true);
                }

                cn.Close();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Ha ocurrido un error:" + "');", true);
            }
        }
    }
}