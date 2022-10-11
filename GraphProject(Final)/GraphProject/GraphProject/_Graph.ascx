<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_Graph.ascx.cs" Inherits="GraphProject._Graph" %>

<div id="@Model.ChartId" class="container" style="width: 100%; height: 100%; overflow-x: scroll"></div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script src="https://fastly.jsdelivr.net/npm/echarts@5.3.3/dist/echarts.min.js"></script>


    <script>
        function createChart(divId) {
    console.log({ echarts: echarts });
            var myChart = echarts.init(document.getElementById(divId));
            var app = {};

    var option = @Model.GraphOptions;


    console.log({ option:option })


            if (option && typeof option === "object") {
                myChart.setOption(option);
            }
        }

        createChart("@Model.ChartId");

          
    </script>