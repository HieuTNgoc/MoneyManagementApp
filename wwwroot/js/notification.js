"use strict";
var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();


connectionNotification.on("LoadNotification", function (message, counter) {
    console.log(message);
    console.log(counter);
    console.log("Noti...");
    var counter_html = "<span>(" + counter + ")</span>";
    var noti_html = "";
    for (let i = message.length - 1; i >= 0; i--) {
        noti_html += "<li>Notification - " + message[i] + "</li>";
    }
    console.log(counter_html);
    console.log(noti_html);
    $("#messageList").html(noti_html);
    $("#notificationCounter").html(counter_html);
    console.log("Noti...done");
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


