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
        loadGrid();

        $("#Interval").on("change", function () {
            loadGrid();

            var interval = $(this).val();

            switch (interval) {
                case "5MIN":
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
        
        const startDate = document.querySelector('#StartDate');
        const endDate = document.querySelector('#EndDate');
        const purpose = document.querySelector('#Purpose');
        
        purpose.addEventListener('change', (event) => {
            loadGrid();
        });

        startDate.addEventListener('change', (event) => {
            loadGrid();

        });
        endDate.addEventListener('change', (event) => {
            loadGrid();
        });
    });

    function loadGrid() {
        var purpose = $("#Purpose").val();
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
  
    function getData(url, chartTitle, chartId, chartContainerId, showXAxis) {
        $.ajax({
            url: url,
            contentType: "application/html; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: {
                freqopt: $("#interval").val(),
                dataContent: $("#purpose").val(),
                startDate: $("#startDate").val(),
                endDate: $("#endDate").val(),
                chartId: chartId,
                chartTitle: chartTitle,
                showXAxis: showXAxis
            }
        }).done(function(data) {
            $(chartContainerId).html(data);
        });
    }
</script>
</asp:Content>


