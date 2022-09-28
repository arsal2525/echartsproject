using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using EchartsWebForms.Charts;
using Newtonsoft.Json;

namespace EchartsWebForms.Api
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public partial class ChartApi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static EChartsRoot LoadChartData(string freqopt,
            string dataContent,
            DateTime startDate,
            DateTime endDate,
            string chartId,
            string chartTitle,
            bool showXAxis)
        {
            var model = new EChartsRoot();
            model.Build(freqopt, dataContent, startDate, endDate, chartId, chartTitle, showXAxis);
            
            return model;
        }
    }
}