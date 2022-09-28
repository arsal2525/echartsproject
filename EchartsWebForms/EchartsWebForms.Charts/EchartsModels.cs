using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EchartsWebForms.Charts
{
    public partial class EChartsRoot
    {
        public void Build(string freqopt,
            string dataContent,
            DateTime startDate,
            DateTime endDate,
            string chartId,
            string chartTitle,
            bool showXAxis)
        {
            // remeberm to instanitaie each object 
            var model = new EChartsRoot();
            //our model variable is our object for the echartroot class in chartdatamodel
            model.series = new List<Series>();
            model.xAxis = new List<XAxis>();
            model.yAxis = new List<YAxis>();
            model.dataZoom = new List<DataZoom>();

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
            
            switch (freqopt.ToUpper())
            {
                case "5MIN":

                    model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 5, zoomOnMouseWheel = "ctrl" });

                    model.dataZoom.Add(new DataZoom { start = 0, end = 5 });

                    //increments of each
                    var hourcounter = 1;
                    var daycounter = 0;
                    var mincounter = 5;

                    for (int i = 0; i < totalMinutes; i++)
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
                    
                    break;

                case "15MIN":
                    model.dataZoom.Add(new DataZoom { type = "inside", start = 0, end = 5, zoomOnMouseWheel = "ctrl" });
                    model.dataZoom.Add(new DataZoom { start = 0, end = 5 });
                    hourcounter = 1;
                    daycounter = 0;
                    mincounter = 15;

                    for (int i = 0; i < totalMinutes; i++)
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
                    
                    break;
                case "DAILY":
                    model.dataZoom.Add(
                        new DataZoom { type = "inside", start = 0, end = 100, zoomOnMouseWheel = "ctrl" });
                    model.dataZoom.Add(new DataZoom { start = 0, end = 100 });

                    daycounter = 0;

                    for (int i = 0; i < totalMinutes; i++)
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
                    
                    break;
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
                boundaryGap = false,
                data = xAxisData,
                show = showXAxis
            });

            model.yAxis.Add(new YAxis
            {
                type = "value"
            });

            var emailData = new List<int>();
            //var unionAddData = new List<int>();

            for (int i = 0; i < dataPointCount; i++)
            {
                var random = new Random();
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

        }
    }
}
