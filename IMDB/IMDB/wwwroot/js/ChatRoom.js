document.addEventListener('DOMContentLoaded', ready);
var name = '';
var connection = new signalR.HubConnectionBuilder().withUrl('/ChatHub').build();

connection.on('ReciveMessage', renderMessage);
connection.onclose(function () {
    disConnect();
    setTimeout(startConnection,10000)
});

function renderMessage(message) {
   
    var li = document.createElement('li');
    li.setAttribute('id', 'sender');
    li.textContent = message.name+'  ';
    var time = document.createElement('small');
    time.textContent = message.date+' :';
    li.appendChild(time);
    var text = document.createElement('span');
    text.textContent = '    ' + message.text;
    text.setAttribute('style','margin:20px')
    li.appendChild(document.createElement('br'));

    li.appendChild(document.createElement('br'));
    li.appendChild(text);
    document.getElementById('income').appendChild(li);
  
    var objDiv = document.getElementById("sendmes");
    objDiv.scrollTop = objDiv.scrollHeight;
}

function startConnection() {

    connection.start().then(onConnect()).catch(function (err) {
        console.log(err);
    });
}

function onConnect() {
    console.log('Connected');
    document.getElementById('try').style.display = 'none';
    document.getElementById('messageform').style.display = 'block';
}

function disConnect() {

    document.getElementById('messageform').style.display = 'none';
    document.getElementById('try').style.display = 'block';
}

function sendMessage(text) {
    if(text && text.length)
        connection.invoke('SendMessage', name, text);
}

function ready() {
    var chat = document.getElementById('chatform');
    chat.addEventListener('submit', function (e) {
        e.preventDefault(); 
        var text = e.target[0].value;
        e.target[0].value = '';
        sendMessage(text);
    });
    var startpanel = document.getElementById('name');
    var form = document.getElementById('messageform');
    startpanel.addEventListener('submit', function (e) {
        e.preventDefault();
        var username = e.target[0].value;
        
        if (username && username.length) {
            e.target[0].value = '';
            name = username;
            startpanel.style.display = 'none';
            form.style.display = 'block';
            startConnection();
        }

    });

}

