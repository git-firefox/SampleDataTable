$(function () {
    fnInitClientSideDataTable();
    fnInitServerSideDataTable();
});

function fnInitClientSideDataTable() {
    $("#tableClient").DataTable();
    $('tbody', '#tableClient').on('click', '.btn-edit', function () {
        var row = $(this).closest('tr');
        var data = table.row(row).data();
    });

    $('tbody', '#tableClient').on('click', '.btn-delete', function () {
        var row = $(this).closest('tr');
        var data = table.row(row).data();
    });
}

function fnInitServerSideDataTable() {
    let table = $("#tableServer").DataTable({
        ajax: {
            url: "/ServerHome/GetPersons",
            type: "POST",
        },
        stateSave: true,
        processing: true,
        serverSide: true,
        filter: true,
        columns: [
            {
                data: "id",
                name: "Id"
            },
            {
                data: "name",
                name: "Name"
            },
            {
                data: "age",
                name: "Age"
            },
            {
                data: "birthDate",
                name: "BirthDate",
                render: function (data) {
                    const date = new Date(data);
                    const formattedDate = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
                    return formattedDate;
                    //const formattedDate = date.toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
                    //return formattedDate;
                }
            },
            {
                data: "height",
                name: "Height",
                render: function (data) {
                    return data.toFixed(2);
                }
            },
            {
                data: "height",
                name: "Height",
                render: function (data) {
                    return data.toFixed(2);
                }
            },
            {
                data: "gender",
                name: "Gender",
                render: function (data) {
                    switch (data) {
                        case 0:
                            return "Male";
                        case 1:
                            return "Female";
                        case 2:
                            return "Other";
                        default:
                            return "";
                    }
                }
            },
            {
                data: "address",
                name: "Address"
            },
            {
                data: "email",
                name: "Email"
            },
            {
                data: null, title: "Actions",
                render: function (data, type, row) {
                    return fnGetActionButtons();
                }
            }
        ],
        columnDefs: [
            { targets: [0], searchable: false, className: "dt-body-left dt-head-left" },
            { targets: [-1, 7], orderable: false, searchable: false },
        ],
    });

    $('tbody', '#tableServer').on('click', '.btn-edit', function () {
        var row = $(this).closest('tr');
        var data = table.row(row).data();

        $.ajax({
            url: '/ServerHome/UpdatePerson',
            data: data,
            method: 'POST',
            success: function (result) {
                if (result.success) {
                    var currentPage = table.page();
                    table.ajax.reload(function () {
                        table.page(currentPage).draw('page');
                    });

                    alert(result.message)
                }
            },
            error: function () {
                alert('Something went wrong', 'error');
            }

        })
    });

    $('tbody', '#tableServer').on('click', '.btn-delete', function () {
        if (confirm("Are you sure you want to delete this record?")) {

            var row = $(this).closest('tr');
            var data = table.row(row).data();

            $.ajax({
                url: '/ServerHome/DeletePerson',
                data: { id: data.id },
                method: 'DELETE',
                success: function (result) {
                    if (result.success) {

                        var currentPage = table.page();
                        table.ajax.reload(function () {
                            table.page(currentPage).draw('page');
                        });

                        //table.ajax.reload();
                        //$('#tableServer').DataTable().ajax.reload();
                        alert(result.message)
                    }
                },
                error: function () {
                    alert('Something went wrong', 'error');
                }
            })
        } else {
            console.log("Deletion cancelled.");
        }
    });
}

function fnGetActionButtons() {
    btnEdit = `<button class="btn btn-sm btn-primary btn-edit">Edit</button>`;
    btnDelete = `<button class="btn btn-sm btn-danger btn-delete">Delete</button>`;
    return btnEdit + ' ' + btnDelete;
}