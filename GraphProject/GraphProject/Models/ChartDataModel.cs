using System;
using System.Collections.Generic;

namespace GraphProject.Models
{
    public class AxisPointer
    {
        public string type { get; set; }
        public Label label { get; set; }
    }

    
    public class DataZoom
    {
        public string yAxisIndex { get; set; }
        public string type { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public Boolean show { get; set; }
        public string filterMode { get; set; }
        public string id { get; set; }
        public bool zoomLock { get; set; }
        public string zoomOnMouseWheel { get; set; }
    }

    public class Emphasis
    {
        public string focus { get; set; }
    }

    public class Feature
    {
        public DataZoom dataZoom { get; set; }
        public Restore restore { get; set; }
        //public DataView dataView { get; set; }
        public MagicType magicType { get; set; }
        public SaveAsImage saveAsImage { get; set; }
    }

    public class Grid
    {
        public string left { get; set; }
        public string right { get; set; }
        public string bottom { get; set; }
        public string top { get; set; }
        public bool containLabel { get; set; }
    }

    public class Label
    {
        public string backgroundColor { get; set; }
    }

    public class MagicType
    {
        public List<string> type { get; set; }
    }

    public class Restore
    {

    }

    public class EChartsRoot
    {
        public List<string> color { get; set; }
        public Title title { get; set; }
        public Tooltip tooltip { get; set; }
        //public Legend legend { get; set; }
        public Toolbox toolbox { get; set; }
        public Grid grid { get; set; }
        public List<XAxis> xAxis { get; set; }
        public List<YAxis> yAxis { get; set; }
        public List<Series> series { get; set; }
        public List<DataZoom> dataZoom { get; set; }
    
    }

    public class SaveAsImage
    {
    }

    public class Series
    {
        public string name { get; set; }
        public string type { get; set; }
        public string stack { get; set; }
        public List<int> data { get; set; }
        public Emphasis emphasis { get; set; }
        public string symbol { get; set; }
        public int symbolSize { get; set; }
    }

    public class Title
    {
        public string text { get; set; }
    }

    public class Toolbox
    {
        public Feature feature { get; set; }
        public int itemSize { get; set; }
    }

    public class Tooltip
    {
        public string trigger { get; set; }
        public AxisPointer axisPointer { get; set; }
    }

    public class XAxis
    {
        public string type { get; set; }
        public bool boundaryGap { get; set; }
        public List<string> data { get; set; }
        public bool show { get; set; }
    }

    public class YAxis
    {
        public string type { get; set; }
    }
}

