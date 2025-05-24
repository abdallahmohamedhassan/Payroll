$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/department/getall' },
        "scrollX": true, // enable horizontal scroll for better layout control
        "columns": [
            { data: 'departmentName', title: "Department Name", width: "200px" },
          
            {
                data: 'id',
                title: "Actions",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/department/upsert?id=${data}" class="btn btn-primary btn-sm mx-1"> 
                        <i class="bi bi-pencil-square"></i> Edit</a>
                     <a onClick="Delete('/department/delete/${data}')" class="btn btn-danger btn-sm mx-1"> 
                        <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`;
                },
                width: "220px"
            }
        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}