//
// Copyright (C) 2013-2014 by Autodesk, Inc.
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
// Written by M.Harada

//==============================================================
// Welcome to the Glue API intro.  
// This is a minimum sample that demonstrates Glue viewer API. 
//==============================================================

$(document).ready(function () {

    // initialize the embedded viewer passing in the target iframe window
    GlueEmbedded.init(window.frames[0]);

    // ---------------------------------------------------------
    // Setting event handlers to get a messages from the viewer. 
    // ---------------------------------------------------------
    // specify an event handler on the document for the "selectionchanged" from the viewer. 
    // The event's eventData property will be set to the array of selected objects
    $(document).on('selectionchanged', function (e) {
        $("#messages").val("object selected: " + JSON.stringify(e.eventData));
    });

    // specify an event handler on the document for the "gotproperties" event from the viewr.  
    // The event's eventData property will be set to the collection of object properties
    $(document).on('gotproperties', function (e) {
        $("#messages").val("got properties: " + JSON.stringify(e.eventData));
    });

    // ---------------------------------
    // Posting a message to the viewer.
    //----------------------------------
    // When the user click get_properties button, this is called. 
    $('#get_properties').on('click', function () {
        GlueEmbedded.getSelectedProperties();
    });

    // This is for zooming in a currently selected element. 
    $('#zoom_selection').on('click', function () {
        GlueEmbedded.zoomSelection();
    });

}); 