$(document).ready(function () {
    $.ajax({
        url: urlContent + 'Home/GetStateList',
        dataType: 'json',
        async: false,
        success: function onSuccess(viewModel) {
            ko.applyBindings(viewModel, $('[id="State"]')[0]);
        }
    });

    $('#State').change(function (e) {
        $.ajax({
            url: urlContent + 'Home/GetCityList',
            type: 'POST',
            data: { 'Stateid': function () {
             return $('#State').val() } },
            dataType: 'json',
            async: false,
            success: function onSuccess(viewModel) {
                ko.applyBindings(viewModel, $('[id="City"]')[0]);
            }
        });
    });

    $('#SearchBtn').click(function(e){
     jQuery('#PlannedGrid').jqGrid('setGridParam',
            {
                datatype: 'json',
            }).trigger('reloadGrid');
    });


    jQuery("#PlannedGrid").jqGrid({
        url: urlContent + "Home/SearchPlanned/",
        datatype: 'json',
        mtype: 'POST',
        postData:
            {
                'City': function () {
                var city=$('#City').val();
                if($('#City :selected').text() !='--Select--')
                city = $('#City :selected').text();
                    return city;
                }, 'State': function () {
                 var state=$('#State').val();
                if($('#State :selected').text() !='--Select--')
                state = $('#State :selected').text();
                    return state
                }
            },
        colNames: ['State', 'City', 'Place', 'Created by', 'Created date', 'Peoples Joined', 'To Join','',
            ],

        colModel: [
       
         { name: 'State', index: 'State', align: 'left', search: false },
        { name: 'City', index: 'City', align: 'left', search: false },
        { name: 'Place', index: 'Place', align: 'left', search: false },
        { name: 'CreatedBy', index: 'CreatedBy', align: 'left', search: false },
        { name: 'CreatedOn', index: 'CreatedOn', align: 'left', search: false },
        { name: 'PeoplesJoined', index: 'PeoplesJoined', align: 'left', search: false },
        {
            name: 'Join',
            index: 'Join',
            align: 'center',
            formatter: function linkFormatterDocument(cellvalue, options, rowObject) {
                return '<a href=# onclick=dataPass('+cellvalue+')>Join</a>';
            },
            search: false,
            sortable: false
        },
             { name: 'PlanId', index: 'PlanId',hidden:true, align: 'left', search: false },             ],
        pager: jQuery('#PlannedGridpager'),
        rowNum: 5,

        loadonce: true,

        loadComplete: function (data) {
            var msg = data.ErrorMessage;
            if (msg != null && msg != "") {
                $('[id="errorMessages"]').html("<li><b>Server Error: </b>" + msg + "</li>");
            }
        },

        caption: "",
        rowList: [5, 10, 20, 50],
        width: 1000,
        height: 230,
        shrinkToFit: true,
        loadError: function RFPDetailsLoadError(xhr, st, err) {
            if (st == "error")
                $('[id="errorMessages"]').html("<li>" + "Type: " + st + "; Response: " + xhr.status + " " + xhr.statusText + "</li>");
            else
                $('[id="errorMessages"]').html("<li> <b>Server Error: </b>The Grid URL could not be located. Contact Site Administrator</li>");
            return false;
        }
    }).navGrid('#PlannedGridpager', {
        edit: false,
        add: false,
        del: false,
        refresh: true,
        search: true
    }
    		                										);

    function PlannedGridReload(result) {
       
        jQuery('#PlannedGrid').jqGrid('setGridParam',
            {
                datatype: 'json'
            }).trigger('reloadGrid');
    }




   
});

function dataPass(data){
$('#PlanId').val(data)
$('#JoinModal').modal('show')
}