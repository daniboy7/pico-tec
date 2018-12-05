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
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MySqlConnection cn;
            MySqlCommand cmd;
            MySqlCommand cmd2;
            try
            {
                cn = new MySqlConnection("server=localhost;database=pico-tec;userid=root;password=jugranados20140465226978");

                cmd = new MySqlCommand("UsuarioValido", cn);
                cmd2 = new MySqlCommand("Login", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@correo", this.TextBox1.Text);
                cmd.Parameters.AddWithValue("@pass", this.TextBox2.Text);
                cmd2.Parameters.AddWithValue("@correo", this.TextBox1.Text);
                cmd2.Parameters.AddWithValue("@pass", this.TextBox2.Text);

                cn.Open();

                int tipoUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                if (tipoUsuario == 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Usuario o contraseña incorrectos" + "');", true);
                }
                else if (tipoUsuario == 1 || tipoUsuario == 2) { 
                    int Usuario = Convert.ToInt32(cmd2.ExecuteScalar());
                    Response.Redirect("Administrador.aspx?Usuario=" + Usuario);
                }
                else
                {
                    int Usuario = Convert.ToInt32(cmd2.ExecuteScalar());
                    Response.Redirect("Presupuestos.aspx?Usuario=" + Usuario);
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