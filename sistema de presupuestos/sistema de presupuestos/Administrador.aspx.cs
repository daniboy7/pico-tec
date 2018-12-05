using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace sistema_de_presupuestos
{
    public partial class Administrador : System.Web.UI.Page
    {

        public string Usuario;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario = Request.QueryString["Usuario"];
        }


    }
}