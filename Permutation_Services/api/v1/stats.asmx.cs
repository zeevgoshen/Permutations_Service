using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Permutation_Services.api.v1
{
    [WebService]
    public class stats : WebService
    {
        [WebMethod]
        [SoapDocumentMethod(ParameterStyle = SoapParameterStyle.Bare)]
        public void showStats()
        {

        }


    }
}
