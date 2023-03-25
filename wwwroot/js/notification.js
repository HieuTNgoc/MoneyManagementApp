"use strict";
var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();


connectionNotification.on("LoadNotification", function (message, counter) {
    console.log(message);
    console.log(counter);
    consolelog("Noti...");
    $("#messageList").html("");
    $("#notificationCounter").html("<span>(" + counter + ")</span>");
    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        $("#messageList").append(li);
    }
});

connectionNotification.start().then(function () {
    $(document).ready(function () {
        console.log("ahihih");
        var userId = $("#UserId").val();
        console.log(userId);
        connectionNotification.invoke('SendNotification', userId).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("done");
    });
    //connectionNotification.send("LoadMessages");
});


