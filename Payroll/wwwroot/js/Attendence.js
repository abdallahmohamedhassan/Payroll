$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/attendence/getall' },
        "scrollX": true, // enable horizontal scroll for better layout control
        "columns": [
            { data: 'greaterThanDays', title: "Greater Than Days", width: "200px" },
            { data: 'percentage', title: "Percentage", width: "200px" },

            {
                data: 'id',
                title: "Actions",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/attendence/upsert?id=${data}" class="btn btn-primary btn-sm mx-1">
                        <i class="bi bi-pencil-square"></i> Edit</a>
                  
                    </div>`;
                },
                width: "220px"
            }
        ]
    });
}
