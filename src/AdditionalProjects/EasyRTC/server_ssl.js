// Load required modules
var http    = require("http");
var request = require('request');
var _      =  require('underscore');
var https   = require("https");     // https server core module
var fs      = require("fs");        // file system core module
var express = require("express");   // web framework external module
var io      = require("socket.io"); // web socket external module

// This sample is using the easyrtc from parent folder.
// To use this server_example folder only without parent folder:
// 1. you need to replace this "require("../");" by "require("easyrtc");"
// 2. install easyrtc (npm i easyrtc --save) in server_example/package.json

var easyrtc = require("easyrtc"); // EasyRTC internal module

// Setup and configure Express http server. Expect a subfolder called "static" to be the web root.
var httpApp = express();
httpApp.use(express.static(__dirname + "/static/"));

// Start Express https server on port 8443
var webServer = https.createServer({
    key:  fs.readFileSync(__dirname + "/certs/localhost.key"),
    cert: fs.readFileSync(__dirname + "/certs/localhost.crt")
}, httpApp);

// Start Socket.io so it attaches itself to Express server
var socketServer = io.listen(webServer, {"log level":1});

// Start EasyRTC server
var rtc = easyrtc.listen(httpApp, socketServer);

// Listen on port 8443
webServer.listen(8443, function () {
    console.log('listening on https://localhost:8443');
});

// my additional code
// store necessary default events (`cause easyrtc substitue previous implementation)
var connectDefault = easyrtc.events.defaultListeners.connection;
var disconnectDefault = easyrtc.events.defaultListeners.disconnect;
var roomJoinDefault = easyrtc.events.defaultListeners.roomJoin;
var msgTypeRoomJoinDefault = easyrtc.events.defaultListeners.msgTypeRoomJoin;

console.log(easyrtc.events.defaultListeners);

// array to store user information
var userInfoArray = [];

easyrtc.events.on('connection', function(socket, easyrtcid, next) {		
	var userInfoObj = { easyrtcid: easyrtcid, authToken: socket.handshake.query.authToken };	
	// console.log(userInfoObj.authToken);
	
	var options = {
		headers: {
			'Authorization': 'Bearer ' + userInfoObj.authToken,
			'Content-Type': 'application/json'
		},
		uri: 'http://localhost:5000/api/Account/whoAmI',		
		method: 'GET'
	};
	
	request(options, function (err, res) {
			if (err) return console.error(err.message);
			
			userInfoObj.userName = res.body;
			userInfoArray.push(userInfoObj);
			console.log(userInfoObj);
			
			connectDefault(socket, easyrtcid, next);
		});
		
});

/*easyrtc.events.on('roomJoin', function(connectionObj, roomName, roomParameter, callback) {
      console.log('------>>>>> ROMM JOIN for ', connectionObj.getEasyrtcid());
	  console.log('------ username ', connectionObj.getUsername());
	  //console.log(connectionObj);
	  console.log(roomName);
	  console.log(roomParameter);
      //return roomJoin(connectionObj, roomName, roomParameter, callback);
	  roomJoinDefault(connectionObj, roomName, roomParameter, callback);
    });*/
	
easyrtc.events.on('msgTypeRoomJoin', function(connectionObj, roomName, roomParameter, callback) {
      
	  var currentEasyRtcId = connectionObj.getEasyrtcid();
	  console.log('------>>>>> msgTypeRoomJoin ', currentEasyRtcId);
	  
	  //console.log('------ username ', connectionObj.getUsername());
	  //connectionObj.generateRoomClientList("join", null, callback);
	  var userInfo = _.find(userInfoArray, function(e) {
		  return e.easyrtcid == currentEasyRtcId;		  
	  });
	  
	  console.log(userInfo.userName);
	  
	  connectionObj.setField('barada', { val: userInfo.userName });
	  //connectionObj.setField(userInfo.userName);
	  //console.log(roomName);
	  //console.log(roomParameter);
	  
	  //if (userInfoArray.length > 0)
		//roomName.clients = userInfoArray;
	  console.log(roomName);
		
      //return roomJoin(connectionObj, roomName, roomParameter, callback);
	  msgTypeRoomJoinDefault(connectionObj, roomName, roomParameter, callback);
    });


easyrtc.events.on('disconnect', function(connectionObj, next) {	
	console.log('disconnect info object ' + connectionObj);
	disconnectDefault(connectionObj, next);
});