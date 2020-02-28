$(document).ready(function () {

    //data
    var host = 'http://' + window.location.host;
    var token = null;
    var headers = {};

    var employeesEndPoint = "/api/employees";
    var companiesEndPoint = "/api/companies";
    var employeesUrl = host + employeesEndPoint;
    var companiesUrl = host + companiesEndPoint;
    var formAction = "Update";

    var editingId;
    var companyId = 1;

    //*********************************************************************************

    //sign up
    $("#signUp").submit(function (e) {

        //prevent default form action
        e.preventDefault();

        var username = $("#signUpEmail").val();
        var password1 = $("#signUpPassword").val();
        var password2 = $("#signUpConfirmPassword").val();

        //prepare object
        var sendData = {
            "Email": username,
            "Password": password1,
            "ConfirmPassword": password2
        };

        //sending request
        $.ajax({

            type: "POST",
            url: host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {

            alert("User '" + username + "' is added successfully. Please go back to Log In");

            $("#signUpEmail").val('');
            $("#signUpPassword").val('');
            $("#signUpConfirmPassword").val('');

        }).fail(function (data) {

            alert("Registration failed! Check input \r\n "+
               "example - \r\n       username: marc@mail.com \r\n       password: Sifra.123");
        });


    });

    //log in
    $("#logIn").submit(function (e) {

        //prevent default form aciton
        e.preventDefault();

        var username = $("#logInEmail").val();
        var password = $("#logInPassword").val();

        //prepare object
        var sendData = {
            "grant_type": "password",
            "username": username,
            "password": password
        };

        //sending request
        $.ajax({

            "type": "POST",
            "url": host + "/Token",
            "data": sendData
        }).done(function (data) {

            token = data.access_token;

            console.log("Data: " + data);
            console.log("Token: " + token);

            $("#logInEmail").val('');
            $("#logInPassword").val('');

            //get JSON data using AJAX http get request
            loadDataTable();

            $("#info").empty().append(data.userName);
            $("#infoLogOut").css("display", "block");
            $("#logIn").css("display", "none");
            $("#search").css("display", "block");

        }).fail(function (data) {

            alert("Log In failed! Check input \r\n " +
                "example - \r\n       username: marc@mail.com \r\n       password: Sifra.123");
        });

    });

    //log out
    $("#btnLogOut").click(function () {

        token = null;
        headers = {};

        $("#choice").css("display", "block");
        $("#infoLogOut").css("display", "none");
        $("#search").css("display", "none");
        $("#edit").css("display", "none");

        loadDataTable();
    });



    //**********************************************************************************

    //load table
    function loadDataTable() {

        console.log("Request URL: " + employeesUrl);

        //get JSON data using AJAX http get request

        $.getJSON(employeesUrl, setEmployees)
            .fail(function () {
                alert("Error loading data from server (employees)");
            });

        $.getJSON(companiesUrl, loadCompanies)
            .fail(function () {
                alert("Error loading data from server (companies)");
            });

    }

    // prepare delete event
    $("body").on("click", "#btnDelete", deleteEmployee);

    //prepare edit event
    $("body").on("click", "#btnEdit1", editEmployee);
 

    //load employees
    console.log("loading employees..");
    loadDataTable();

    $("#btnSignUp1").click(function () {
        $("#signUp").css("display", "block");
        $("#choice").css("display", "none");
    });

    $("#btnLogIn1").click(function () {
        $("#logIn").css("display", "block");
        $("#choice").css("display", "none");
    });

    $("#btnBack1").click(function () {
        $("#choice").css("display", "block");
        $("#logIn").css("display", "none");
    });

    $("#btnBack2").click(function () {
        $("#choice").css("display", "block");
        $("#signUp").css("display", "none");
    });

    $("#btnBack3").click(function () {
        refreshForm();
        $("#edit").css("display", "none");
       
    });

    $("#btnRefresh").click(function () {
        loadDataTable();
        $("#edit").css("display", "none");
    });



    function setEmployees(data, status) {

        console.log("Status: " + status);

        var $container = $("#dataa");
        $container.empty();

        if (status === "success") {

            console.log(data);

            //title

            var h1 = $("<h1 align='center'>Employees</h1>");
            $container.append(h1);

            //table
            var table = $("<table class='table table-bordered table-hover'></table>");
            var header;

            if (token) {
                header = $("<thead><th>Name</th><th>Birth Year</th><th>Year of Employment</th><th>Company</th><th>Salary</th><th>Delete</th><th>Edit</th></thead>");
            } else {
                header = $("<thead><th>Name</th><th>Birth Year</th><th>Year of Employment</th><th>Company</th></thead>");
            }

            table.append(header);

            var i;
            for (i = 0; i < data.length; i++) {

                //showing new row in table
                var row = "<tbody><tr>";

                var displayData;

                if (token) {

                    displayData = "<td>" + data[i].Name + "</td><td>" + data[i].BirthYear + "</td><td>" + data[i].YearOfEmployment + "</td><td>" + data[i].CompanyName + "</td><td>" + data[i].Salary + "</td>";
                    var stringId = data[i].Id.toString();
                    var displayDelete = "<td><button class='btn btn-danger'  id=btnDelete name=" + stringId + ">Delete</button></td>";
                    var displayEdit = "<td><button class='btn btn-warning' id=btnEdit1 name=" + stringId + ">Edit</button></td>";

                    row += displayData + displayDelete  + displayEdit + "</tr></tbody>";

                } else {

                    displayData = "<td>" + data[i].Name + "</td><td>" + data[i].BirthYear + "</td><td>" + data[i].YearOfEmployment + "</td><td>" + data[i].CompanyName + "</td>";
                    row += displayData + "</tr></tbody>";
                }

                table.append(row);

            }

            $container.append(table);

        } else {

            var h11 = $("<h1>Error loading employees</h1>");
            $container.append(h11);
        }

    }

    function loadCompanies() {

        var idCompany = $("#companyId"); //id for drop down list

        var html = '';
        $.getJSON(companiesUrl, function (data) {

            $.each(data, function (key, value) {
                html += "<option value='" + value.Id + "'>" + value.Name + "</option>";
            });
            $(idCompany).html(html);

        });
    }


    $("#editForm").submit(function (e) {

        //sprecavanje default akcije forme
        e.preventDefault();

        var companyIdId = companyId;
        var name = $("#nameEmployee").val();
        var birthYear = $("#birthYearEmployee").val();
        var yearOfEmployment = $("#yearOfEmploymentEmployee").val();
        var salary = $("#salaryEmployee").val();

        var httpAction;
        var sendData;
        var url;

        //prepare object
        if (formAction === "Update") {

            httpAction = "PUT";
            url = employeesUrl + '/' + editingId.toString();
            sendData = {
                "Id": editingId,
                "Name": name,
                "BirthYear": birthYear,
                "YearOfEmployment": yearOfEmployment,
                "Salary": salary,
                "CompanyId": companyId
            };
        }

        console.log("Object to send: ");
        console.log(sendData);

        //user should be logged in
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        //sending request
        $.ajax({

            url: url,
            type: httpAction,
            data: sendData,
            "headers": headers

        }).done(function (data, status) {
            formAction = "Update";
            refreshForm();
            $("#edit").css("display", "none");
            loadDataTable();
        }).fail(function (data, status) {
            alert("Error editting employee");
        });


    });

    //edit employee
    function editEmployee() {

        //get id
        var editId = this.name;
        console.log("editId: " + editId);

        var url = employeesUrl + '/' + editId.toString();
        var httpAction = "GET";

        //send request, get employee
        $.ajax({

            url: url,
            type: httpAction
           
        }).done(function (data, status) {

            console.log(data);
            editingId = data.Id;
            formAction = "Update";

            $("#edit").css("display", "block");
            $("#companyId").val(data.CompanyId);
            $("#nameEmployee").val(data.Name);
            $("#birthYearEmployee").val(data.BirthYear);
            $("#yearOfEmploymentEmployee").val(data.YearOfEmployment);
            $("#salaryEmployee").val(data.Salary);

        }).fail(function (data, status) {
            alert("Error getting employee");
        });


    }

    //search employee
    $("#searchForm").submit(function (e) {

        //prevent default form aciton
        e.preventDefault();

        var start = $("#yearStart").val();
        var end = $("#yearEnd").val();

        if (start !== '' && end !== '') {

            var httpAction;
            var url;
            var sendData;

            //prepare object
            httpAction = "POST";
            url = host + "/api/employment";
            sendData = {
                "StartYear": start,
                "EndYear": end
            };

            console.log("Object to send: ");
            console.log(sendData);

            //user should be logged in
            if (token) {
                headers.Authorization = 'Bearer ' + token;
            }

            //sending request
            $.ajax({

                url: url,
                type: httpAction,
                data: sendData,
                "headers": headers
            }).done(function (data, status) {

                console.log(data);
                $("#yearStart").val('');
                $("#yearEnd").val('');
                setEmployees(data, status);
                $("#edit").css("display", "none");
            }).fail(function (data, status) {
                alert("Error searching employees!");
            });
        } else {
            alert("Fill in form");
        }

    });


    //delete employee
    function deleteEmployee() {

        //get id
        var deleteId = this.name;
        console.log("deleteId: " + deleteId);

        var url = employeesUrl + '/' + deleteId.toString();
        var httpAction = "DELETE";

        //user should be logged in
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        //send request
        $.ajax({

            url: url,
            type: httpAction,
            "headers": headers
        }).done(function (data, status) {
            loadDataTable();
        }).fail(function (data, status) {
            alert("Error deleting employee");
        });

    }


    //get selected company from drop down
    $(document).on("change", "#companyId", function () {

        companyId = $(this).val();
        console.log("company: " + companyId);
    });


    function refreshForm() {

        companyId = 1;
        $("#companyId").val(companyId);

        $("#nameEmployee").val('');
        $("#birthYearEmployee").val('');
        $("#yearOfEmploymentEmployee").val('');
        $("#salaryEmployee").val('');
        
    }

});