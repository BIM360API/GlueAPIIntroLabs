//
// Copyright (C) 2014 by Autodesk, Inc.
// 
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
// 

// GlueEmbeeded provides a thin JavaScript layer to interface with
// the Glue viewer. 
// 

var GlueEmbedded = {

    // init needs to be called in order to use the embedded viewer.
    // This method sets the target iframe window for the viewer 
    // and initializes viewer event handlers. 

    init: function (iframe) {
        this.target = iframe;
        this.initEventHandler();
    },

    /* APIs */

    // setSelection sets the selected object in the model currently loaded in the embedded viewer 
    // to the specified objectPaths (can be singular)

    setSelection: function (objectPaths) {
        this.postAPI("setSelection", objectPaths);
    },

    // getProperties sends a request to the viewer to retrieve the properties associated with 
    // the specified objectPath (singular only)
    // In order to get the return value, you need to set up an event handler bound to 
    // the "gotproperties" event

    getProperties: function (objectPath) {
        this.postAPI("getProperties", objectPath);
    },

    // getProperties sends a request to the viewer to retrieve the properties associated 
    // with the object currently selected in the viewer (singular only)
    // In order to get the return value, you need to set up an event handler bound 
    // to the "gotproperties" event

    getSelectedProperties: function () {
        this.postAPI("getSelectedProperties", "");
    },

    // zoomSelection triggers the viewer to zoom in on the currently selected object(s)
    zoomSelection: function () {
        this.postAPI("zoomSelection", "");
    },


    /* generic post */
    // this can be used to generically call any exposed API on the embedded viewer

    postAPI: function (api, data) {
        var json = JSON.stringify(data);
        var sdata = '{ "api":"' + api + '", "data":' + json + '}';
        var target = this.target || window.frames[0];
        var targetOrigin = "https://b2.autodesk.com/";
        if (target)
            target.postMessage(sdata, targetOrigin);

    },


    /* events */
    // call this method to register your own callback method to be executed when a viewer event is 
    // received.
    // a JSON object containing the event name ("event" property) and the event data 
    //("data" property) will be passed as the argument to the callback

    registerMessageCallback: function (callback) {
        if (typeof callback == "function")
            this.messageCallback = callback;
    },

    // function called internally to bind to "message" events coming from the embedded viewer.
    // If jquery is loaded, a jquery event (name determined from the window message event data) 
    // will be triggered
    // and the event data will be passed as the "eventData" property on the jQuery event.
    // If a callback has been reigistered via "registerMessageCallback" it will be executed with a 
    // a JSON object containing the event name ("event" property) and the event data 
    //("data" property) will be passed as the argument to the callback
    // Currently, the exposed viewer events are:
    //      - selectionchanged: includes the new selected object(s) path
    //      - gotproperties: includes the JSON formatted property data requested by the getProperties 
    //        or getSelectedProperties APIs
    
    initEventHandler: function () {
        var self = this;
        window.addEventListener("message", function (event) {
            var obj = JSON.parse(event.data);
            if (obj && window.jQuery) {
                var eventName = obj.event.split(':')[1];
                window.jQuery.event.trigger({ type: eventName, eventData: obj.data });
            }
            if (typeof self.messageCallback == "function")
                self.messageCallback(obj);
        });
    },

    messageCallback: function () { }

};