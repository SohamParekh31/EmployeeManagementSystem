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