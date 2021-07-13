using System;
using System.Web;
using System.Web.UI;

namespace Permutation_Services
{

    public partial class Default : System.Web.UI.Page
    {
        public string test = string.Empty;

        private void Page_Load(object sender, EventArgs e)
        {
            test = "123";
            HttpRequest httpRequest = HttpContext.Current.Request;

            string url = httpRequest.QueryString.ToString();

        }
    }
}
