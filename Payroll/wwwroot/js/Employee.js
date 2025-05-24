$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/employee/getall' },
        "scrollX": true, // enable horizontal scroll for better layout control
        "columns": [
            { data: 'fullName', title: "Full Name", width: "200px" },
            { data: 'email', title: "Email", width: "180px" },
            { data: 'phone', title: "Phone", width: "120px" },
            { data: 'address', title: "Address", width: "200px" },
            { data: 'dateOfBirth', title: "Date of Birth", width: "130px" },
            { data: 'dateOfHire', title: "Date of Hire", width: "130px" },
            { data: 'department.departmentName', title: "Department", width: "150px" },

            {
                data: 'id',
                title: "Actions",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                     <a href="/employee/upsert?id=${data}" class="btn btn-primary btn-sm mx-1"> 
                        <i class="bi bi-pencil-square"></i> Edit</a>
                     <a onClick="Delete('/employee/delete/${data}')" class="btn btn-danger btn-sm mx-1"> 
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