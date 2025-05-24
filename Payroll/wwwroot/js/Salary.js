$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/salary/getall' },
        "scrollX": true, // enable horizontal scroll for better layout control
        "columns": [
            { data: 'gradeName', title: "Grade Name", width: "200px" },
            { data: 'baseSalary', title: "Base Salary", width: "200px" },

            {
                data: 'id',
                title: "Actions",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/salary/upsert?id=${data}" class="btn btn-primary btn-sm mx-1"> 
                        <i class="bi bi-pencil-square"></i> Edit</a>
                  
                    </div>`;
                },
                width: "220px"
            }
        ]
    });
}
