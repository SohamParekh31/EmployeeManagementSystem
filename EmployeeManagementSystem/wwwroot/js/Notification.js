const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();
var count = 0;
var badgeCounter = document.getElementById("badge-counter");

badgeCounter.style.display = "none";

connection.start().then(function () {
    NotificationModule();
})
var noti = document.getElementById("NotificationMenu");
function NotificationModule() {
    connection.on("departmentAdded", function (message) {
        noti.appendChild(CreateMenuItem(message));
        noti.classList.add("nav-item");
        noti.appendChild(CreateLine());
        count += 1;
        badgeCounter.textContent = count;
        badgeCounter.style.display = "inline";
    })
    connection.on("departmentDelete", function (message) {
        noti.appendChild(CreateMenuItem(message));
        noti.classList.add("m-2")
        noti.appendChild(CreateLine());
        count += 1;
        badgeCounter.textContent = count;
        badgeCounter.style.display = "inline";
    })
    connection.on("employeeUpdate", function (message) {
        noti.appendChild(CreateMenuItem(message));
        noti.appendChild(CreateLine());
        count += 1;
        badgeCounter.textContent = count;
        badgeCounter.style.display = "inline";
    })
    connection.on("addEmployee", function (message) {
        noti.appendChild(CreateMenuItem(message));
        noti.appendChild(CreateLine());
        count += 1;
        badgeCounter.textContent = count;
        badgeCounter.style.display = "inline";
    })
}
function CreateMenuItem(message) {
    let li = document.createElement("li");
    li.textContent = message;
    return li;
}
function CreateLine() {
    let hr = document.createElement("hr");
    return hr;
}
var t = document.getElementById("tabletoappend");
connection.on("Refresh", () => {
    t.innerHTML = "";
    loaddataforDepartment();
});

loaddataforDepartment();
function loaddataforDepartment() {
    $.ajax({
        url: 'Departments/GetDepart',
        method: 'GET',
        success: (result) => {
            var rows = [];
            $.each(result, (k, v) => {
                console.log(v.name);
                loadDepartment(v);
            })
        },
        error: (error) => {
            console.log(error)
        }
    })
}

function loadDepartment(Department) {
    t.innerHTML += `<tbody><tr><td>${Department.name}</td>
                    <td><a href="Departments/Edit/${Department.departmentId}">Edit</a> |
                <a href="Departments/Delete/${Department.departmentId}" >Delete</a></td></tr></tbody>`
}
var t1 = document.getElementById("tabletoappendEmp");
connection.on("RefreshEmployee", () => {
    t1.innerHTML = "";
    loaddataforEmployee();
});

loaddataforEmployee();
function loaddataforEmployee() {
    $.ajax({
        url: 'Employees/getEmployee',
        method: 'GET',
        success: (result) => {
            var rows = [];
            $.each(result, (k, v) => {
                console.log(v.name);
                loadEmployee(v);
            })
        },
        error: (error) => {
            console.log(error)
        }
    })
}

function loadEmployee(employee) {
    t1.innerHTML += `<tbody><tr><td>${employee.name}</td>
                       <td>${employee.surname}</td>
                        <td>${employee.address}</td>
                        <td>${employee.qualification}</td>
                        <td>${employee.contact_Number}</td>
                        <td>${employee.department.name}</td>
                    <td><a href="Employees/Edit/${employee.id}">Edit</a> |
                <a href="Employees/Delete/${employee.id}" >Delete</a></td></tr></tbody>`
}