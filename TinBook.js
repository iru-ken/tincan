/*
=============COPYRIGHT============ 
Tin Book - An I-Read-This prototype for Tin Can API
Copyright (C) 2012  Andrew Downes

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.
<http://www.gnu.org/licenses/>.
*/



//Create an instance of the Tin Can Library
var myTinCan = new TinCan();

myTinCan.enableDebug = 1;

//Create an LRS and add to the list of record stores https://cloud.scorm.com/tc/BVRODUFK9Z/sandbox/   https://cloud.scorm.com/ScormEngineInterface/TCAPI/public/
var myLRS = new TinCan.LRS({
	endpoint: "https://cloud.scorm.com/tc/BVRODUFK9Z/", 
	version: "1.0.0",
	auth: 'Basic ' + Base64.encode("iru_learning_technology@irco.com" + ':' + "Scorm2014")
});
myTinCan.recordStores[0] = myLRS;

//Set the default actor
var myActor = new TinCan.Agent({
	name : "William Bell",
	mbox : "mailto:iru_learning_technology@irco.com"
});

myTinCan.actor = myActor;


function postActivity(thisActivity){
	var item = thisActivityObject;
		
	/*============SEND STATEMENT==============*/
	//Create the verb
	var myVerb = new TinCan.Verb({
		id : "http://www.tincanapi.co.uk/wiki/verbs:read",
		display : {
			"en-US":"read", 
			"en-GB":"read"
		}
	});
	
	//Create the activity definition
	var myActivityDefinition = new TinCan.ActivityDefinition({
		name : {
			"en-US":item.volumeInfo.title, 
			"en-GB":item.volumeInfo.title
		},
		description : {
			"en-US":item.volumeInfo.description, 
			"en-GB":item.volumeInfo.description
		}
	});
	
	//Create the activity
	var myActivity = new TinCan.Activity({
		id : item.selfLink,
		definition : myActivityDefinition
	});
	
	//create the statement
	var stmt = new TinCan.Statement({
		actor : myActor,
		verb : myVerb,
		target : myActivity
	},true);
	
    //tin-can post with callback function
	myTinCan.sendStatement(stmt, function() {
		/*============DISPLAY SENT DATA==============*/
		alert("Statement reported to LRS. Refresh this page to start again!");
	});
	/*============END DISPLAY SENT DATA==============*/
  }
  /*============END SEND STATEMENT==============*/