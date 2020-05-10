var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "api/Contact",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "firstName", "width": "25%" },
            { "data": "middleName", "width": "25%" },
            { "data": "lastName", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center"><a class="btn btn-success text-white" style="width: 55px;" href="/ContactList/Upsert?id=${data}">Edit</a>&nbsp;<a class="btn btn-danger text-white" style="width: 70px;" onclick=Delete('/api/Contact?id='+${data})>Delete</a></div>`;
                }
            }
        ],
        "language": {
            "emptyTable": "No data available."
        },
        "width": "70%"
    })
}

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this imaginary file!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (result) {
                        if (result.success) {
                            // Do something with the result
                            dataTable.ajax.reload();
                            swal(result.message, {
                                icon: "success",
                            });
                        }
                        else
                        {
                            swal(result.message, {
                                icon: "danger",
                            });
                        }
                    }
                });
            } else {
                swal("Your imaginary file is safe!");
            }
        });
}