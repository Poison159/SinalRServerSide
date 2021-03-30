"use strict"

const signalRConnection = new signalR.HubConnectionBuilder()
    .withUrl("/messagebroker")
    .build();

signalRConnection.start().then(function () {
    console.log("SignalR Connected");
}).catch(function (err) {
    return console.error(err.toString());
});


signalRConnection.on("onMessageReceived", function (eventMessage) {
    console.log(eventMessage);
});

let messageCount = 0;

signalRConnection.on("onMessageReceived", function (eventMessage) {
    messageCount++;
    const msgCountH4 = document.getElementById("messageCount");
    msgCountH4.innerText = "Messages: " + messageCount.toString();
    const ul = document.getElementById("messages");
    const li = document.createElement("li");
    li.innerText = messageCount.toString();

    for (const prop in eventMessage) {
        const newDiv = document.createElement("div");
        const newContent = document.createTextNode(`${prop}: ${eventMessage[prop]}`);
        newDiv.appendChild(newContent);
        li.append(newDiv);
    }
    ul.appendChild(li);
    window.scrollTo(0, document.body.scrollHeight);
});