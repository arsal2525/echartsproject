using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using GraphProject.Models;
using Newtonsoft.Json;

namespace GraphProject.TestCompany
{
    public partial class MyChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static GraphModel LoadChart(string freqopt, string dataContent, DateTime startDate, DateTime endDate, string chartId, string chartTitle, bool showXAxis, string ChartContainerId)
        {
            try
            {
                // remeberm to instanitaie each object 
                var model = new EChartsRoot();
                //our model variable is our object for the echartroot class in chartdatamodel
                model.series = new List<Series>();
                model.xAxis = new List<XAxis>();
                model.yAxis = new List<YAxis>();
                model.dataZoom = new List<DataZoom>();
                //model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 5, zoomOnMouseWheel = "ctrl" });

                //model.dataZoom.Add(new DataZoom { start = 0, end = 5 });

                // desginer pattern builder pattern creating an object
                model.grid = new Grid();

                model.color = new List<string>();
                model.color.Add("#80FFA5");
                model.color.Add("#00DDFF");
                model.color.Add("#37A2FF");
                model.color.Add("#FF0087");
                model.color.Add("#80FFA5");
                model.color.Add("#FFBF00");

                model.toolbox = new Toolbox();
                //model.legend = new Legend();
                //model.legend.data = new List<string>();
                //model.legend.data.Add("Union Ads");
                //model.legend.data.Add("Email");
                //model.legend.data.Add("Video Ads 1");
                //model.legend.data.Add("Video Ads 2");
                //model.legend.data.Add("Line 5");
                //model.legend.show = true;
                //model.legend.z = 4;
                //model.legend.orient = "horizontal";
                //model.legend.left = "center";
                //model.legend.align = "auto";

                model.tooltip = new Tooltip();
                model.tooltip.trigger = "none";
                model.tooltip.axisPointer = new AxisPointer();
                model.tooltip.axisPointer.type = "cross";
                model.tooltip.axisPointer.label = new Label();
                model.tooltip.axisPointer.label.backgroundColor = "#6a7985";

                model.title = new Title();
                model.title.text = chartTitle;
                // made it felxible when calling loadchart till pass the title in

                model.toolbox.itemSize = 10;
                model.toolbox.feature = new Feature();
                model.toolbox.feature.dataZoom = new DataZoom();
                model.toolbox.feature.restore = new Restore();
                //model.toolbox.feature.dataView = new DataView();
                model.toolbox.feature.magicType = new MagicType();
                model.toolbox.feature.saveAsImage = new SaveAsImage();

                model.toolbox.feature.dataZoom.yAxisIndex = "none";
                model.toolbox.feature.dataZoom.show = false;
                model.toolbox.feature.dataZoom.filterMode = "filter";
                //model.toolbox.feature.dataView.readOnly = false;
                model.toolbox.feature.magicType.type = new List<string>();

                model.toolbox.feature.magicType.type.Add("line");
                model.toolbox.feature.magicType.type.Add("bar");

                //{ } in the echarts lib meant its an object so would instiate a new thing for that
                //[] meant it was an array so would just set the value of the property

                model.grid.left = "3%";
                model.grid.right = "4%";
                model.grid.bottom = "3%";
                model.grid.top = "25%";
                model.grid.containLabel = true;

                var xAxisDaily = new List<string>();
                var xAxisMonthly = new List<string>();
                var xAxisData = new List<string>();

                var dateDiff = endDate - startDate;
                var totalMinutes = dateDiff.TotalMinutes == 0 ? 1440 : dateDiff.TotalMinutes;
                // if totalmins is 0 make it 1440 otherwise make it what it is
                var dataPointCount = 0;
                // total mins passed in from the value of the start end date


                //switch (dataContent.ToUpper())
                //{
                //    case "Price":
                //        break;
                //    case "LSELoad":
                //        break;
                //}
                switch (freqopt.ToUpper())
                {
                    case "5MIN":

                        model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 5, zoomOnMouseWheel = "ctrl" });

                        model.dataZoom.Add(new DataZoom { start = 0, end = 5 });

                        //increments of each
                        var hourcounter = 1;
                        var daycounter = 0;
                        var mincounter = 5;

                        for (int i = 0; i <= totalMinutes; i++)
                        {
                            // to group it logically do remianders
                            var minrem = i % 5;
                            var hourrem = i % 60;
                            var dayrem = i % 1440;

                            if (dayrem == 0)
                            {
                                //if ( daycounter == 1)
                                //{
                                //    Trace.WriteLine($"{i} oneday");
                                //} 
                                xAxisData.Add($"{startDate.ToString("MM/dd")}");
                                daycounter++;
                                dataPointCount++;
                                startDate = startDate.AddDays(1);
                            }
                            else if (hourrem == 0)
                            {
                                //if (hourcounter == 23)
                                //{
                                //    Trace.WriteLine($"{i} 23h");
                                //}
                                xAxisData.Add($"{hourcounter} h");
                                hourcounter = hourcounter == 23 ? 1 : hourcounter + 1;
                                // dont go to hour 25 roll back over
                                dataPointCount++;

                            }
                            else if (minrem == 0)
                            {
                                xAxisData.Add(mincounter.ToString());
                                mincounter = mincounter == 55 ? 5 : mincounter + 5;
                                dataPointCount++;
                            }
                        }

                        // if date is changed then reload the page with the new totalminutes
                        // and range for the amount of total mins
                        //var myObj = { }

                        //myObj.watch('p', function(id, oldVal, newVal) {
                        //    alert('the property myObj::' + id + ' changed from ' + oldVal + ' to ' + newVal);
                        //});
                        //model.title.text = "5MIN";
                        break;

                    case "15MIN":
                        model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 5, zoomOnMouseWheel = "ctrl" });
                        model.dataZoom.Add(new DataZoom { start = 0, end = 5 });
                        hourcounter = 1;
                        daycounter = 0;
                        mincounter = 15;

                        for (int i = 0; i <= totalMinutes; i++)
                        {
                            // to group it logically do remianders
                            var minrem = i % 15;
                            var hourrem = i % 60;
                            var dayrem = i % 1440;

                            if (dayrem == 0)
                            {
                                //if ( daycounter == 1)
                                //{
                                //    Trace.WriteLine($"{i} oneday");
                                //} 
                                xAxisData.Add($"{startDate.ToString("MM/dd")}");
                                daycounter++;
                                dataPointCount++;
                                startDate = startDate.AddDays(1);
                            }
                            else if (hourrem == 0)
                            {
                                //if (hourcounter == 23)
                                //{
                                //    Trace.WriteLine($"{i} 23h");
                                //}
                                xAxisData.Add($"{hourcounter} h");
                                hourcounter = hourcounter == 23 ? 1 : hourcounter + 1;
                                // dont go to hour 25 roll back over
                                dataPointCount++;

                            }
                            else if (minrem == 0)
                            {
                                xAxisData.Add(mincounter.ToString());
                                mincounter = mincounter == 45 ? 15 : mincounter + 15;
                                dataPointCount++;
                            }
                        }

                        // if date is changed then reload the page with the new totalminutes
                        // and range for the amount of total mins
                        //var myObj = { }

                        //myObj.watch('p', function(id, oldVal, newVal) {
                        //    alert('the property myObj::' + id + ' changed from ' + oldVal + ' to ' + newVal);
                        //});
                        break;

                    //case "MONTHLY":
                    //    xAxisMonthly.Add("Aug");
                    //    xAxisMonthly.Add("Sept");
                    //    xAxisMonthly.Add("Oct");
                    //    xAxisMonthly.Add("Nov");
                    //    xAxisMonthly.Add("Dec");
                    //    xAxisMonthly.Add("Jan");
                    //    xAxisMonthly.Add("Feb");
                    //    xAxisMonthly.Add("March");
                    //    xAxisMonthly.Add("April");
                    //    xAxisMonthly.Add("May");
                    //    xAxisMonthly.Add("June");
                    //    xAxisMonthly.Add("July");
                    //    xAxisData.AddRange(xAxisMonthly);
                    //    model.title.text = "Monthly";
                    //    break;
                    case "DAILY":
                        model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 100, zoomOnMouseWheel = "ctrl" });
                        model.dataZoom.Add(new DataZoom { start = 0, end = 100 });

                        daycounter = 0;

                        for (int i = 0; i <= totalMinutes; i++)
                        {
                            // to group it logically do remianders

                            var dayrem = i % 1440;

                            if (dayrem == 0)
                            {
                                //if ( daycounter == 1)
                                //{
                                //    Trace.WriteLine($"{i} oneday");
                                //} 
                                xAxisData.Add($"{startDate.ToString("MM/dd")}");
                                daycounter++;
                                dataPointCount++;
                                startDate = startDate.AddDays(1);
                            }

                        }

                        // if date is changed then reload the page with the new totalminutes
                        // and range for the amount of total mins
                        //var myObj = { }

                        //myObj.watch('p', function(id, oldVal, newVal) {
                        //    alert('the property myObj::' + id + ' changed from ' + oldVal + ' to ' + newVal);
                        //});
                        break;
                    //xAxisDaily.Add("07/01");
                    //xAxisDaily.Add("07/02");
                    //xAxisData.AddRange(xAxisDaily);
                    case "YEARLY":
                        xAxisDaily.Add("07/01");
                        xAxisDaily.Add("07/02");
                        xAxisData.AddRange(xAxisDaily);
                        break;
                    case "HOURLY":
                        xAxisDaily.Add("01");
                        xAxisDaily.Add("02");
                        xAxisDaily.Add("03");
                        xAxisDaily.Add("04");
                        xAxisDaily.Add("05");
                        xAxisDaily.Add("06");
                        xAxisDaily.Add("07");
                        xAxisDaily.Add("08");
                        xAxisDaily.Add("09");
                        xAxisDaily.Add("10");
                        xAxisDaily.Add("11");
                        xAxisDaily.Add("12");
                        xAxisDaily.Add("13");
                        xAxisDaily.Add("14");
                        xAxisDaily.Add("15");
                        xAxisDaily.Add("16");
                        xAxisDaily.Add("17");
                        xAxisDaily.Add("18");
                        xAxisDaily.Add("19");
                        xAxisDaily.Add("20");
                        xAxisDaily.Add("21");
                        xAxisDaily.Add("22");
                        xAxisDaily.Add("23");
                        xAxisDaily.Add("24");
                        xAxisData.AddRange(xAxisDaily);
                        break;
                }

                model.xAxis.Add(new XAxis
                {
                    type = "category",
                    boundaryGap = true,
                    data = xAxisData,
                    show = showXAxis,
                    axisLabel = new axisLabel
                    {
                        showMinLabel = true,
                        showMaxLabel = true
                    }
                });

                model.yAxis.Add(new YAxis
                {
                    type = "value"
                });

                var emailData = new List<int>();
                //var unionAddData = new List<int>();
                var random = new Random();
                for (int i = 0; i < dataPointCount; i++)
                {

                    emailData.Add(random.Next(1, 1000));
                    //unionAddData.Add(random.Next(1, 1500));
                }

                model.series.Add(new Series
                {
                    name = "Email",
                    type = "line",
                    stack = "Total",
                    symbol = "none",
                    data = emailData
                });
                //model.series.Add(new Series
                //{
                //    name = "Union Ads",
                //    type = "line",
                //    stack = "Total",
                //    emphasis = new Emphasis
                //    {
                //        focus = "series"
                //    },
                //    data = unionAddData
                //}) ;

                //model.series.Add(new Series
                //{
                //    name = "Video Ads 1",
                //    type = "line",
                //    stack = "Total",
                //    emphasis = new Emphasis
                //    {
                //        focus = "series"
                //    },
                //    data = new List<int>() { 220, 232, 201, 234, 290, 330, 310 }
                //});

                //model.series.Add(new Series
                //{
                //    name = "Video Ads 2",
                //    type = "line",
                //    stack = "Total",
                //    emphasis = new Emphasis
                //    {
                //        focus = "series"
                //    },
                //    data = new List<int>() { 320, 332, 301, 334, 390, 430, 410 }
                //});

                var json = JsonConvert.SerializeObject(model);
                // return Json(new { data = new HtmlString(json) });

                // new variable gmodel to create instance of grpahmodel class so can use its
                // data members and functions
                var gModel = new GraphModel();

                gModel.Purpose = dataContent;

                gModel.GraphOptions = json;
                //serialzied object from the json we have in visual studio code into the c# object
                // we have created here 

                gModel.ChartId = ChartContainerId;
                //set in _graph.cshtml atModel.ChartId cuz chartid is variable
                //of graphmodel object gmodel

                return gModel;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}