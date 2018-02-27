
(function ($) {

    'use strict';


    // Initialize a dataTable with collapsible rows to show more details
    var initDetailedViewTable = function () {

        var _format = function (d) {
            // `d` is the original data object for the row
            return '<table class="table table-inline">' +
                '<tr>' +
                '<td>' + d[0] + ' <span class="label label-important">ALERT!</span></td>' +
                '<td>' + d[1] + '</td>' +
                '</tr>' +
                '<tr>' +
                '<td>' + d[2] + '</td>' +
                '<td>' + d[3] + '</td>' +
                '</tr>' +
                '</table>';
        }


        var table = $('#detailedTable');

        table.DataTable({
            "sDom": "t",
            "scrollCollapse": true,
            "paging": true,
            "bSort": false
        });

        // Add event listener for opening and closing details
        $('#detailedTable tbody').on('click', 'tr', function () {
            //var row = $(this).parent()
            if ($(this).hasClass('shown') && $(this).next().hasClass('row-details')) {
                $(this).removeClass('shown');
                $(this).next().remove();
                return;
            }
            var tr = $(this).closest('tr');
            var row = table.DataTable().row(tr);

            $(this).parents('tbody').find('.shown').removeClass('shown');
            $(this).parents('tbody').find('.row-details').remove();

            row.child(_format(row.data())).show();
            tr.addClass('shown');
            tr.next().addClass('row-details');
        });

    }

    // Initialize a condensed table which will truncate the content 
    // if they exceed the cell width



    initDetailedViewTable();


})(window.jQuery);