using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sistema_de_presupuestos
{
    public partial class Presupuestos : System.Web.UI.Page
    {

        public string Usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario = Request.QueryString["Usuario"];
        }
    }
}