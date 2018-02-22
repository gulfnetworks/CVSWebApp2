  let aLastVisit = ["NEVER", "1 MONTH", "3 MONTHS", "6 MONTHS","> 6 MONTHS "]


$("#prgSaving").show();
$("#prgSaved").hide();
$("#prgFailed").hide();


       

var currentPopupViewIndex = false;

$('select').select2({
    dropdownParent: $('#modalSlideUp')
});


function SaveSurvey() {

    var jsonSurvey = {
        'SurveyId': 1,
        'QualityRate': 0,
        'QualityComment': '',
        'ValueRate': 0,
        'ValueComment': '',
        'ServiceRate': 0,
        'ServiceComment': '',
        'AmbienceRate': 0,
        'AmbienceComment': '',
        'RecommendRate': 0,
        'RecommendPoorArea': '',
        'RecommendImprovements': '',
        'RecommendSuggestions': '',
        'LastVisit': 0,
        'LastVisitComment': '',
        'Action': '',
        'Status': '',
        //'DateTime' : '',
        'Customer': '',
        'MobileNo': '',
        'Email': '',
        'CheckNo': '',
        'TableNo': '',
        'OutletId': 0,
        'ManagerId': 0,
        'StaffId': 0,
    }

    jsonSurvey.Action = '1';
    jsonSurvey.Status = 'Unresolved';
    //jsonSurvey.DateTime = '';
    jsonSurvey.Customer = document.getElementById("txtFirstName").value + " " + document.getElementById("txtLastName").value;
    jsonSurvey.MobileNo = document.getElementById("txtMobile").value;
    jsonSurvey.Email = document.getElementById("txtEmail").value;
    jsonSurvey.CheckNo = document.getElementById("txtCheckNo").value;
    jsonSurvey.TableNo = document.getElementById("txtTableNo").value;
    jsonSurvey.OutletId = $("#OutletId").val();
    jsonSurvey.ManagerId = $("#ManagerId").val();
    jsonSurvey.StaffId = $("#StaffId").val();


    jsonSurvey.Action = '';
    jsonSurvey.SurveyId = 0;
    jsonSurvey.QualityRate = parseInt($($(".field")[1]).find(".btn.btn-default.active").text());
    jsonSurvey.QualityComment = $("#QualityComment").val();
    jsonSurvey.ValueRate = parseInt($($(".field")[0]).find(".btn.btn-default.active").text());
    jsonSurvey.ValueComment = $("#ValueComment").val();
    jsonSurvey.ServiceRate = parseInt($($(".field")[2]).find(".btn.btn-default.active").text());
    jsonSurvey.ServiceComment = $("#ServiceComment").val();
    jsonSurvey.AmbienceRate = parseInt($($(".field")[3]).find(".btn.btn-default.active").text());
    jsonSurvey.AmbienceComment = $("#AmbienceComment").val();
    jsonSurvey.RecommendRate = parseInt($($(".field")[5]).find(".btn.btn-default.active").text());
    jsonSurvey.RecommendPoorArea = $("#RecommendPoorArea").val();
    jsonSurvey.RecommendImprovements = $("#RecommendImprovements").val();
    jsonSurvey.RecommendSuggestions = $("#RecommendSuggestions").val();
    jsonSurvey.LastVisit = $.inArray($($("#btnLastVisit").find(".active").find("span")[1]).text(), aLastVisit);
    jsonSurvey.LastVisitComment = $("#LastVisitComment").val();





    $.ajax({
        url: '/Surveys/Send',
        type: "POST",
        dataType: 'json',
        contentType: "application/json",
        async: true,

        data: JSON.stringify(jsonSurvey),

        success: function (data) {
            $("#prgSaving").hide();
            $("#prgSaved").show();
            $("#prgFailed").hide();

            setTimeout($('#modalLoader').modal('hide'), 1000);
        },
        error: function (xhr, textStatus , errorThrown) {
            $("#prgSaving").hide();
            $("#prgSaved").hide();
            $("#prgFailed").show();
            setTimeout($('#modalLoader').modal('hide'), 1000);
        }
              

    });

}




        //$('#CountryId').on('select2:select', function () {


        //    var id = $(this).val();

        //    $.ajax({
        //        url: "/Surveys/OutletByCountry?id=" + id,
        //        type: "GET",
        //        applicationType: "application/json",
        //        success: function (data) {


        //            $("#divOutletContainer").html(data);

        //            $("#OutletId").select2({
        //                dropdownParent: $('#modalSlideUp')
        //            });
                    
        //            $("#OutletId").on('select2:select', function () { 
        //                var mId = $(this).val(); 
        //                alert(1);
        //                //Load Manager Div
        //                $.ajax({
        //                    url: "/Surveys/ManagersByOutlet?id=" + mId ,
        //                    type: "GET",
        //                    applicationType: "application/json",
        //                    success: function (data2) {
        //                        alert(1);
        //                        $("#divManagerContainer").html(data2);
        //                        $("#ManagerId").select2({
        //                            dropdownParent: $("#modalSlideUp")
        //                        })
        //                    }
        //                });



        //            });

                   
        //        }
        //    });

        //});
        
$("#btnToggleSlideUpSize").on("click", function (e) {



       
    $(".modal").each(function () {


        console.log($(this).attr("id"));
        console.log($(this).prop("id"));


        if ($(this).attr("id") != "btnToggleSlideUpSize") {
            $(this).modal("hide");

        }
    });

        
    $("#modalSlideUpSmall").modal("show");

});
        
$('#CountryId').on('select2:select', function () {
     
         
    var id = $(this).val();

    $.ajax({
        url: "/Surveys/OutletByCountry?id=" + id,
        type: "GET",
        applicationType: "application/json",
        success: function (data) {
          
            $("#divOutletContainer").html(data);
            
            $("#OutletId").select2({
                dropdownParent: $('#modalSlideUp')

            }).val(null).trigger('change');

            $("#OutletId").on('select2:select', function () {
                var mId = $(this).val();
             
                //Load Manager Div
                $.ajax({
                    url: "/Surveys/UsersByOutlet?id=" + mId,
                    type: "GET",
                    applicationType: "application/json",
                    success: function (data2) {
                            
                        $("#divUsersByOutletContainer").html(data2);


                        $("#ManagerId").select2({
                            dropdownParent: $("#modalSlideUp")
                        }).val(null).trigger('change');


                        $("#StaffId").select2({
                            dropdownParent: $("#modalSlideUp")
                        }).val(null).trigger('change');


                              
                    }
                });



            });


        }
    });

});





$(document).ready(() => {

    currentPopupViewIndex = false;
    $('#divCustomerInfo').hide();
    $('#divVisitDetail').show();
    $("#lblModalHeader").text("Visit");
    $("#lblModalHeaderBold").text("Details");
    $("#btnNext").on("click", function (e) {
        if ($("#btnNext").text() == "Submit") {
                 
            $('#modalSlideUp').modal('hide');
            $('#modalLoader').modal('show');

            SaveSurvey();
        }
    });


    //$("#lblModalHeaderSubtext").text("We welcome your feedback! Please fill in the following. (* indicates required response)");
    $("#btnNext, #btnCancel").on("click", function (e) {
        e.preventDefault();

        if ($(this).text() == "Cancel") return false;
        if ($(this).text() == "Back") return false;

        if ($(this).text() == "Submit") {
            return false;
        }


        if (!currentPopupViewIndex) {
            currentPopupViewIndex = true;
            $('#divVisitDetail').fadeOut(300, function () {
                $("#lblModalHeader").text("Customer");
                $("#lblModalHeaderBold").text("Information");
                $("#lblModalHeaderSubtext").text("Please fill in the following. (* indicates required response)");
                $('#divCustomerInfo').show();
                $("#btnNext").text("Submit");
                $("#btnCancel").text("Back");
            });             
        } else {
            currentPopupViewIndex = false;
        

            $('#divCustomerInfo').fadeOut(300, function () {
                $("#lblModalHeader").text("Visit");
                $("#lblModalHeaderBold").text("Details");
                $("#lblModalHeaderSubtext").text("We welcome your feedback! Please fill in the following. (* indicates required response)");
                $('#divVisitDetail').show();
                $("#btnNext").text("Next");

                $("#btnCancel").text("Cancel");
            });
                 
        }
                

              
             

    });


});