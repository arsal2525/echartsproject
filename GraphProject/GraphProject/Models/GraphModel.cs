using System;
using System.Web;

namespace GraphProject.Models
{
    public class GraphModel
    {
        public string Purpose { get; set; }
        //stores the option that the user sleects?
        public string GraphOptions { get; set; }
        //variable that shows the two different types of charts
        //if purpose chagnes uses these charts other wise use price cahrts
        public string ChartId { get; set; }
    }
}

