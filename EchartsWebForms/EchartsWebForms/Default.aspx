<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EchartsWebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" for="Interval" name="lblInterval" ID="lblInterval"></asp:Label>
    <asp:DropDownList runat="server" name="Interval" id="Interval"/>
    <br />
    <asp:Label runat="server" for="Purpose" name="lblPurpose" ID="lblPurpose"></asp:Label>
    <asp:DropDownList runat="server" name="Purpose" id="Purpose"/>
    <br />
    <asp:TextBox runat="server" name="StartDate" id="StartDate" data-mask="99/99/9999"></asp:TextBox>
    <br />
    <asp:TextBox runat="server" name="EndDate" id="EndDate" data-mask="99/99/9999"></asp:TextBox>

    <div id="partialchart">

        <div class="charts-parent">
        
            <div id="Graph1" style="background:red;">

            </div>
            <div id="Graph2">

            </div>
            <div id="Graph3">

            </div>

            <div id="Graph4">
            </div>

            <div id="Graph5">

            </div>
            <div id="Graph6">

            </div>
        </div>


        <div class="charts-parent-lse">
            <div id="Graph7">

            </div>
            <div id="Graph8">


            </div>
            <div id="Graph9">

            </div>
            <div id="Graph10">


            </div>
            <div id="Graph11">
            </div>
            <div id="Graph12">
            </div>
        </div>
    </div>
    
<script>
    $(function () {

        $("#interval").on("change", function () {
            // ajax call
            console.log("changetrigger")
            // load smoothly without refresh
            // vanilla js ajax call is very wordy

            // specify url first
            loadGrid();
        });
        loadGrid();

        $("#interval").on("change", function () {
            var interval = $(this).val();


            switch (interval) {
                case "5MIN":

                    // so in 5 min for the interval case

                    // for loop to popualte start min and end min by 5 min s i %5 == 0
                    // use jqery to find how to change options in dropdown
                    // build html stirng of options and ue %startmin.html
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
                    // for loop to popualte start min and end min by 15 min s i %15 == 0

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

        // triggering change event on load



        const startDate = document.querySelector('#startDate');
        const endDate = document.querySelector('#endDate');
        const purpose = document.querySelector('#purpose');


        purpose.addEventListener('change', (event) => {

            loadGrid();
        });

        startDate.addEventListener('change', (event) => {

            console.log("inside startdatechange");
            loadGrid();

        });
        endDate.addEventListener('change', (event) => {

            console.log("inside enddatechange");
            loadGrid();
        });



        // get varialbes tlike totalminutes or datediff from controller to here somehow

    });

    function loadGrid() {
        // wrapper fucniton . fi want diffreent title need to treat grids as component
        var purpose = $("#purpose").val();
        console.log({ purpose: purpose });
        switch (purpose) {
            case "Price":


                $(".charts-parent").show();
                $(".charts-parent-lse").hide();


                getData("URL HERE", "container", "#chart-container1", "#option1-container", true);
                getData("URL HERE", "carData", "#chart-container", "#Graph1", false);
                getData("URL HERE", "chartTitle2", "#chart-container2", "#Graph2", false);
                getData("URL HERE", "chartTitle3", "#chart-container3", "#Graph3", false);
                getData("URL HERE", "chartTitle4", "#chart-container4", "#Graph4", false);
                getData("URL HERE", "chartTitle5", "#chart-container5", "#Graph5", false);
                getData("URL HERE", "chartTitle6", "#chart-container6", "#Graph6", true);
                //getdata passes all the stuff we want for our charts into hc and loadchart action

                break;
            case "LSELoad":

                $(".charts-parent").hide();
                $(".charts-parent-lse").show();



                getData("URL HERE", "chartTitle7", "#chart-container7", "#Graph7", false);
                getData("URL HERE", "chartTitle8", "#chart-container8", "#Graph8", false);
                getData("URL HERE", "chartTitle9", "#chart-container9", "#Graph9", false);
                getData("URL HERE", "chartTitle10", "#chart-container10", "#Graph10", false);
                getData("URL HERE", "chartTitle11", "#chart-container11", "#Graph11", false);
                getData("URL HERE", "chartTitle12", "#chart-container12", "#Graph12", true);


                break;
        }

    }
    // made get data more generic ge data from diffreernt endpoints
    // getting data andl oadign chart at once pass in chart id create unique cahrt id everytime get data is called
    // if same chartid could be issues pass in id as chart id thats why added to grpah mdoel

    function getData(url, chartTitle, chartId, chartContainerId, showXAxis) {
        $.ajax({
            // this fucnion gets the data from any action

            url: url,

            // define the url in a better way

            contentType: "application/html; charset=utf-8",
            //passing back entire partial view usually will be application/json
            type: "GET",
            dataType: "html",
            data: {
                //sends all of this

                freqopt: $("#interval").val(),
                dataContent: $("#purpose").val(),
                startDate: $("#startDate").val(),
                endDate: $("#endDate").val(),
                chartId: chartId,
                //created here set at bottom of loadchart action in hc from
                //GrahModel class object and the variable values it gets set from
                //the partial view html + index.cshtml id logic and hc logic

                chartTitle: chartTitle,
                showXAxis: showXAxis
                //last 2 set in getdata call

            }


        }).done(function (data) {
            $(chartContainerId).html(data);

            //post has a body
            // puts the partial in this div

        })
    }
    </script>
</asp:Content>


