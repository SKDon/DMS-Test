# Test.Sujith_Krishantha
Adding “CRUDContext” class next we are going to inherit this class with “DbContext” class
```ruby
public class CRUDContext: DbContext
    {
        public CRUDContext()
            : base("name=CRUDContext")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
```
#### Connection string in Web.config file
```ruby
<connectionStrings>
    <add name="CRUDContext" connectionString="Data Source=DESKTOP-EF73MB4;Initial Catalog=Test;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>
```
#### Employee Section UI
Install Microsoft.jQuery.Unobtrusive.Ajax by Nuget packages
```ruby
@section Scripts{
    @Scripts.Render("~/bundles/jquery")
    <script src="~/Scripts/jquery.unobtrusive-ajax.min.js"></script>
 
    <link href="https://cdn.datatables.net/1.10.15/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.datatables.net/responsive/2.1.1/css/responsive.bootstrap.min.css" rel="stylesheet" />

    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap4.min.js "></script>
    <script>

        $(document).ready(function () {
            $("#tbl").DataTable({
                "processing": true, // for show progress bar
                "serverSide": true, // for process server side
                "filter": true, // this is for disable filter (search box)
                "orderMulti": false, // for disable multiple column at once
                "pageLength": 5,

                "ajax": {
                    "url": '@Url.Action("GetAllEmployees", "Employees")',
                    "type": "POST",
                    "datatype": "json"
                },
                "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "Email", "name": "Email", "autoWidth": true },
                { "data": "Contact", "name": "Contact", "autoWidth": true },
                { "data": null, "name": "Action", "render": function (data, type, row) { return "<a href='#' class='btn btn-info' onclick=EditData('" + row.Id + "'); >Edit</a> <a href='#' class='btn btn-danger' onclick=DeleteData('" + row.Id + "'); >Delete</a>";}, "autoWidth": true }
              
            ]

            })
        });

        function Save() {
            var t = {
                Name: $("#name").val(),
                Email: $("#email").val(),
                Contact: $("#contact").val()
            };
            $.ajax({
                type: "POST",
                url: '@Url.Action("save", "Employees")',
                datatype: "Json",
                data: JSON.stringify(t),
                contentType: "application/json",
                success: function (data) {
                    oTable = $('#tbl').DataTable();
                    oTable.draw();
                }
            })
        }

        function Clear() {
            $("#id").val("");
            $("#name").val("");
            $("#email").val("");
            $("#contact").val("");
        }

        function Update() {
            var t = {
                id: $("#id").val(),
                Name: $("#name").val(),
                Email: $("#email").val(),
                Contact: $("#contact").val()
            };
            $.ajax({
                type: "POST",
                url: '@Url.Action("update", "Employees")',
                datatype: "Json",
                data: JSON.stringify(t),
                contentType: "application/json",
                success: function (data) {
                    oTable = $('#tbl').DataTable();
                    oTable.draw();
                }
            })
        }

        function EditData(Id) {
            $.ajax({
                type: "GET",
                url: "/Employees/edit/" + Id,
                datatype: "Json",
                success: function (data) {
                    $("#id").val(data[0].Id);
                    $("#name").val(data[0].Name);
                    $("#email").val(data[0].Email);
                    $("#contact").val(data[0].Contact);
                }
            })
        }

        function DeleteData(Id) {
            $.ajax({
                type: "GET",
                url: "/Employees/delete/" + Id,
                datatype: "Json",
                success: function (data) {
                    oTable = $('#tbl').DataTable();
                    oTable.draw();
                }
            })
        }

    </script>
}

```
#### Adding LoadData Action Method to Demo Controller
adding System.Linq.Dynamic package from NuGet packages 
![Screenshot](https://csharpcorner-mindcrackerinc.netdna-ssl.com/article/using-datatables-grid-with-asp-net-mvc/Images/image020.jpg)
#### front UI
```ruby
<div class="panel panel-primary">
    <div class="top-buffer"></div>
    <div class="panel-heading panel-head">Employees</div>
    <div class="panel-body">
        <div class="top-buffer"></div>
        <table class="table table-bordered table-striped table-condensed" id="tbl">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Contact</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </div>
</div>
<br />

<form class="form-horizontal" role="form">

    <div class="form-group">
        <label for="ID" class="col-sm-3 control-label">ID</label>
        <div class="col-sm-9">
            <input type="text" id="id" name="ID" class="form-control" readonly />
        </div>
    </div>

    <div class="form-group">
        <label for="name" class="col-sm-3 control-label">Name</label>
        <div class="col-sm-9">
            <input type="text" id="name" name="name" class="form-control" />
        </div>
    </div>

    <div class="form-group">
        <label for="email" class="col-sm-3 control-label">Email</label>
        <div class="col-sm-9">
            <input type="text" id="email" name="email" class="form-control" />
        </div>
    </div>

    <div class="form-group">
        <label for="contact" class="col-sm-3 control-label">Contact</label>
        <div class="col-sm-9">
            <input type="text" id="contact" name="contact" class="form-control" />
        </div>
    </div>

    <div class="col-sm-offset-3 col-sm-9">
        <button type="button" class="btn btn-primary" onclick="return Save();">Add</button>
        <button type="button" class="btn btn-primary" onclick="return Update();">Update</button>
        <button type="button" class="btn btn-primary" onclick="return Clear();">Clear</button>

    </div>

</form>

```

