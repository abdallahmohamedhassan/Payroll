$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/experienceincentive/getall' },
        "scrollX": true, // enable horizontal scroll for better layout control
        "columns": [
            { data: 'greaterThanYear', title: "Greater Than Year", width: "200px" },
            { data: 'incentivePercentage', title: "Incentive Percentage", width: "200px" },

            {
                data: 'id',
                title: "Actions",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/experienceincentive/upsert?id=${data}" class="btn btn-primary btn-sm mx-1">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                  
                    </div>`;
                },
                width: "220px"
            }
        ]
    });
}
