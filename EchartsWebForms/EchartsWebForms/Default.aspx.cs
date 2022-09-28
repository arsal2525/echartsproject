using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EchartsWebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.StartDate.Text = DateTime.Now.ToShortDateString();
            this.EndDate.Text = DateTime.Now.AddDays(5).ToShortDateString();

            this.Interval.Items.Add(new ListItem("5 - min", "5MIN"));
            this.Interval.Items.Add(new ListItem("15 - min", "15MIN"));
            this.Interval.Items.Add(new ListItem("Daily", "DAILY"));

            this.Purpose.Items.Add(new ListItem("Price", "Price"));
            this.Purpose.Items.Add(new ListItem("LSE Load", "LSELoad"));

            this.lblInterval.Text = "Choose an interval:";
            this.lblPurpose.Text = "Choose an option:";
        }
    }
}