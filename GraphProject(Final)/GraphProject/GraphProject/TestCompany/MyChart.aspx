<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyChart.aspx.cs" Inherits="GraphProject.TestCompany.MyChart" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row" style="margin-top: 10px;">
            <div class="col-md-3">
                <div class="form-group">
                    <label for="interval">Choose an interval:</label>
                    <asp:DropDownList ID="interval" runat="server">
                        <asp:ListItem Value="5min">5-min</asp:ListItem>
                        <asp:ListItem Value="15min">15-min</asp:ListItem>
                        <asp:ListItem Value="Daily">Daily</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <label for="purpose">Choose an option:</label>
                    <asp:DropDownList ID="purpose" runat="server">
                        <asp:ListItem Value="Price">Price</asp:ListItem>
                        <asp:ListItem Value="LSELoad">LSELoad</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-3">
                <%--<input runat="server" type="text" name="startDate" id="startDate" data-mask="99/99/9999" value="@DateTime.Now.ToShortDateString()" />--%>
                <input runat="server" type="date" name="startDate" id="startDate" value="2022-10-06" min="2022-01-01" max="2022-12-31" />
            </div>
            <div class="col-md-3">
                <%--<input runat="server" type="text" name="endDate" id="endDate" data-mask="99/99/9999" value="@DateTime.Now.AddDays(5).ToShortDateString()" />--%>
                <input runat="server" type="date" name="endDate" id="endDate" value="2022-10-07" min="2022-01-01" max="2022-12-31" />
            </div>
        </div>
    </div>


            <div class="charts-parent" id="charts-parent">
                <div id="Graph1" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph2" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph3" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>

                <div id="Graph4" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>

                <div id="Graph5" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph6" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
            </div>


            <div class="charts-parent-lse" id="charts-parent-lse">
                <div id="Graph7" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph8" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph9" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph10" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph11" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
                <div id="Graph12" class="container" style="width: 100%;height:200px;" overflow-x: scroll">
                </div>
            </div>

     <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4=" crossorigin="anonymous"></script>
    <script src="https://fastly.jsdelivr.net/npm/echarts@5.3.3/dist/echarts.min.js"></script>

    <script>

        $(function () {

            $("#MainContent_interval").on("change", async function () {

                await safeLoadGrid();

            });
            loadGrid();

            $("#MainContent_interval").on("change", function () {
                
                var interval = $(this).val();
                switch (interval) {
                    case "5MIN":

                        console.log(interval);

                        $("#lbl-end-mins").show();
                        $("#lbl-end-hours").show();
                        $("#end-mins").show();
                        $("#end-hours").show();

                        $("#lbl-start-mins").show();
                        $("#lbl-start-hours").show();
                        $("#start-mins").show();
                        $("#start-hours").show();
                        break;
                    case "15MIN":

                        $("#lbl-end-mins").show();
                        $("#lbl-end-hours").show();
                        $("#end-mins").show();
                        $("#end-hours").show();

                        $("#lbl-start-mins").show();
                        $("#lbl-start-hours").show();
                        $("#start-mins").show();
                        $("#start-hours").show();
                        break;

                    case "Daily":
                        $("#lbl-start-mins").hide();
                        $("#lbl-start-hours").hide();
                        $("#start-mins").hide();
                        $("#start-hours").hide();

                        $("#lbl-end-mins").hide();
                        $("#lbl-end-hours").hide();
                        $("#end-mins").hide();
                        $("#end-hours").hide();
                        break;

                }



            });

            const mcInterval = document.querySelector('#MainContent_interval');
            const startDate = document.querySelector('#MainContent_startDate');
            const endDate = document.querySelector('#MainContent_endDate');
            const purpose = document.querySelector('#MainContent_purpose');


            purpose.addEventListener('change', async function() {

                await safeLoadGrid();

            });

            startDate.addEventListener('change', async function() {
                await safeLoadGrid();

            });

            async function safeLoadGrid() {
                console.log("Disable StartDate");

                startDate.disabled = true;
                endDate.disabled = true;
                purpose.disabled = true;
                mcInterval.disabled = true;

                //https://barker.codes/blog/how-to-wrap-a-function-in-a-promise-in-vanilla-js/
                await Sleep();
                await loadGridAsync();

                console.log("Enable StartDate");

                startDate.disabled = false;
                endDate.disabled = false;
                purpose.disabled = false;
                mcInterval.disabled = false;
            }

            //Used to replicate latency in system, can delete/not use
            function timeoutPromise() {
                return new Promise(r => setTimeout(r, 1000));
            }

            //Used to replicate latency in system, can delete/not use
            async function Sleep() {
                await timeoutPromise();
                return function() { console.log("end sleep"); }
            }

            endDate.addEventListener('change', async function() {
                console.log("inside enddatechange");
                var startDt = $("#MainContent_startDate").val();
                var endDt = $("#MainContent_endDate").val();
                if (startDt <= endDt) {
                    await safeLoadGrid();
                }
                else {
                    $("#MainContent_endDate").val(startDt);
                    alert("EndDate always greater than StartDate!");
                }
            });

            async function loadGridAsync() {
                console.log("Initiate load grid promise");
                await new Promise(loadGridPromiseExecutor);
                console.log("Done with load grid promise");
            }
            
            function loadGridPromiseExecutor(resolutionFunc, rejectionFunc) {
                resolutionFunc(loadGrid());
            }

            function loadGrid() {
                var purpose = $("#MainContent_purpose").val();
                switch (purpose) {
                    case "Price":

                        $("#charts-parent").show();
                        $("#charts-parent-lse").hide();


                        console.log("Begin getdata function calls v1");
                        //getData("container", "#MainContent_chart-container1", "#MainContent_option1-container", true);
                        getData("carData", "#MainContent_chart-container", "Graph1", false);
                        getData("chartTitle2", "#MainContent_chart-container2", "Graph2", false);
                        getData("chartTitle3", "#MainContent_chart-container3", "Graph3", false);
                        getData("chartTitle4", "#MainContent_chart-container4", "Graph4", false);
                        getData("chartTitle5", "#MainContent_chart-container5", "Graph5", false);
                        getData("chartTitle6", "#MainContent_chart-container6", "Graph6", true);
                        console.log("End getdata function calls v1");
                        break;

                    case "LSELoad":

                        $("#charts-parent").hide();
                        $("#charts-parent-lse").show();

                        console.log("Begin getdata function calls v2");
                        getData("chartTitle7", "#MainContent_chart-container7", "Graph7", false);
                        getData("chartTitle8", "#MainContent_chart-container8", "Graph8", false);
                        getData("chartTitle9", "#MainContent_chart-container9", "Graph9", false);
                        getData("chartTitle10", "#MainContent_chart-container10", "Graph10", false);
                        getData("chartTitle11", "#MainContent_chart-container11", "Graph11", false);
                        getData("chartTitle12", "#MainContent_chart-container12", "Graph12", true);
                        console.log("End getdata function calls v2");

                        break;
                }
              
            }

            function getData(chartTitle, chartId, chartContainerId, showXAxis) {
                /*debugger;*/
                $.ajax({
                    type: "Post", //GET
                    url: "MyChart.aspx/LoadChart",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{ freqopt: '" + $("#MainContent_interval").val() + "', dataContent: '" + $("#MainContent_purpose").val() + "', startDate: '" + $("#MainContent_startDate").val() + "' , endDate: '" + $("#MainContent_endDate").val() + "', chartId: '" + chartId + "', chartTitle: '" + chartTitle + "', showXAxis: '" + showXAxis + "', ChartContainerId : '" + chartContainerId + "' }",
                    success: function (data) {
                        createChart(data);
                    },
                    failure: function (response) {
                    //    alert(response.d);
                    },
                    error: function (response) {
                    //    alert(response.d);
                    }
                });
            }
        });

        function createChart(data) {
            /*debugger*/
            //console.log({ echarts: echarts });
            var myChart = echarts.init(document.getElementById(data.d.ChartId));
            var app = {};

            var option = JSON.parse(data.d.GraphOptions);


            //console.log({ option: option })

            if (option && typeof option === "object") {
                myChart.setOption(option);
            }
        }
    </script>
</asp:Content>

