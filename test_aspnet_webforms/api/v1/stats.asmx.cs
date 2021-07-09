using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace test_aspnet_webforms.api.v1
{
    [WebService]
    public class stats : System.Web.Services.WebService
    {
        [WebMethod]
        [SoapDocumentMethod(ParameterStyle = SoapParameterStyle.Bare)]
        public void showStats()
        {

        }


    }
}
