"use strict";
// Creating a connection to SignalR Hub
var connection = new signalR.HubConnectionBuilder().withUrl("/signalr-hub").build();

// Starting the connection with server
connection.start().then(function () { }).catch(function (err) {
    return console.error(err.toString());
});

//Ajax for Message Sending
function SendMessageByAjax(messageVM) {
    $.post("/Message/Send", messageVM);
}

// Sending the message from Client
$("#btnSend").click("click", function (event) {
    var sendername = $("#Sender").val();
    var messageToSend = $("#Text").val();
    var message = { ReceiverId: $("#ReceiverId").val(), Text: messageToSend,SenderName:"",When:"",SenderId:"" };
    SendMessageByAjax(message);
    var sentLi = $(`<li class="list-group-item bg-secondary">
                            <div class="bg-secondary">You : ${messageToSend}</div>
                        </li>`);
    $("#messsages").append(sentLi);
    connection.invoke("SendMessageToUser", message.ReceiverId, sendername, messageToSend).catch(function (err) {
        return console.error(err.toString());
    });
    $("#Text").val("");
    event.preventDefault();
});

connection.on("SendUserMessage", function (user, message) {
    console.log(message);
    var received = $(`<li class="list-group-item"><div class="bg-info">Received : ${message}</div></li>`);
    $("#messsages").append(received);
});