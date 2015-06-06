#region Copyright
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
#endregion // Copyright

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Added 
using System.Net;  // for HttpWebRequest
using System.IO;   // StreamWriter
using System.Diagnostics;    // for Debug writing 
using System.Configuration;  // for Configuration. Add reference to System.Configuration. 
// For HttpUtility. 
// Tip: If your application is set as .NET Framework 4 Client Profile, 
// it won't show System.Web. Use .NET Framework 4 instead.  
using System.Web;
using System.Web.Script.Serialization; // for JavaScriptSerializer. add reference to System.Web.Extensions. 
// Added for RestSharp. 
using RestSharp;
using RestSharp.Deserializers; 

///===================================================================
/// Welcome to the Glue REST API.  
/// 
/// "Glue" class in this page defines/constructs REST API calls 
/// to various Glue web services. 
/// 
/// You can find the API documentation at the following site. 
/// Doc 
/// http://b4.autodesk.com/api/doc/index.shtml
///
/// As of Oct 2014, display component page is not up to date.
/// Please refer to the comments in View method below. 
///===================================================================
/// 10/13/2014 - We use RestSharp in this Intro for simplicity and 
/// to focus on Glue specific API.
/// 
///===================================================================
namespace GlueAPIIntro
{
   class Glue
   {
      // Set values that are specific to your environments.
      // Hard-coding for simplicity for this exercise. 
      // companyId is the name of the host. 

     public const string baseApiUrl = @"https://b4.autodesk.com/api/";
     public const string baseViewerUrl = @"https://b2.autodesk.com?";

     // To Do: set your own configuration in app.config file.
     private static string apiKey = ConfigurationManager.AppSettings["publicKey"];
     private static string apiSecret = ConfigurationManager.AppSettings["privateKey"];
     private static string companyId = ConfigurationManager.AppSettings["company"];


     // Save the last response. This is for our learning purpose to seek into what we got.  
     public IRestResponse m_lastResponse = null;

     ///===============================================================
     /// Security service: Login
     /// URL 
     /// https://b4.autodesk.com/api/security/v1/login.{format}
     /// Methods: POST
     /// Doc
     /// http://b4.autodesk.com/api/security/v1/login/doc
     ///
     /// Sample Response (JSON)  
     /// {
     ///   "auth_token":"The authentication token returned by BIM 360",
     ///   "user_id":"The BIM 360 Glue user identifier for this user"
     /// }
     ///===============================================================

     public string Login(string login_name, string password)
     {
        string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
        string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);
        
        // (1) Build request 
        var client = new RestClient();
        client.BaseUrl = baseApiUrl; 
        client.Authenticator = new SimpleAuthenticator("login_name", login_name, "password", password); 

        // Set resource/end point
        var request = new RestRequest();
        request.Resource = "security/v1/login.json";
        request.Method = Method.POST;

        // Alternatively, you can set as param. 
        //request.AddParameter("login_name", login_name);
        //request.AddParameter("password", password);

        request.AddParameter("company_id", companyId);
        request.AddParameter("api_key", apiKey);
        request.AddParameter("timestamp", timeStamp);
        request.AddParameter("sig", signature); 

        // (2) Execute request and get response
        IRestResponse response = client.Execute(request);

        // Save response. This is to see the response for our learning.
        m_lastResponse = response; 

        // Get the auth token. 
        string authToken = "Undefined";
        if (response.StatusCode == HttpStatusCode.OK)
        {
           JsonDeserializer deserial = new JsonDeserializer();
           LoginResponse loginResponse = deserial.Deserialize<LoginResponse>(response);
           authToken = loginResponse.auth_token;
        }

        return authToken;
     }

     ///===================================================
     /// Security service: Logout 
     ///
     /// URL 
     /// https://b4.autodesk.com/api/security/v1/logout.{format}
     /// Methods: POST
     /// Doc
     /// http://b4.autodesk.com/api/security/v1/logout/doc
     ///
     /// Sample Response (JSON)  
     /// {
     ///   "auth_token_age": "number of minutes since login"
     /// }
     ///===================================================

     public string Logout(string authToken)
     {
        string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
        string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);

        // (1) Build request 

        // set base url and authentication info. 
        var client = new RestClient();
        client.BaseUrl = baseApiUrl;

        // Set resource or end point 
        var request = new RestRequest();
        request.Resource = "security/v1/logout.json";
        request.Method = Method.POST;

        // Add parameters 
        request.AddParameter("company_id", companyId);
        request.AddParameter("api_key", apiKey);
        request.AddParameter("timestamp", timeStamp);
        request.AddParameter("sig", signature);
        request.AddParameter("auth_token", authToken);

        // (2) Execute request and get response
        IRestResponse response = client.Execute(request);

        // Save response. This is to see the response for our learning.
        m_lastResponse = response;

        return response.Content;
     }

     // Version that uses AddSingingParams(). 
     public string Logout2(string authToken)
     {
        // (1) Build request 

        // set base url and authentication info. 
        var client = new RestClient();
        client.BaseUrl = baseApiUrl;

        // Set resource or end point 
        var request = new RestRequest();
        request.Resource = "security/v1/logout.json";
        request.Method = Method.POST;

        // Add parameters
        AddSingingParams(request, authToken); 

        // (2) Execute request and get response
        IRestResponse response = client.Execute(request);

        // Save response. This is to see the response for our learning.
        m_lastResponse = response;

        return response.Content;
     }
     
     // Once you get the idea what we need, you can writing a method for setting signing parameters.  
     private void AddSingingParams(RestRequest request, string authToken)
     {
        string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
        string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);

        request.AddParameter("company_id", companyId);
        request.AddParameter("api_key", apiKey);
        request.AddParameter("timestamp", timeStamp);
        request.AddParameter("sig", signature);
        request.AddParameter("auth_token", authToken);
     }

    ///===============================================================
    /// Project service: List
    /// Get a list of projects.  
    /// URL
    /// https://b4.autodesk.com/api/project/v1/list.{format}?
    /// Methods: GET
    /// Doc
    /// http://b4.autodesk.com/api/project/v1/list/doc
    ///===============================================================

    public List<Project> ProjectList(string authToken)
    {
       string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
       string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);

       // (1) Build request 
       // set base url and authenticatopm info. 
       var client = new RestClient();
       client.BaseUrl = baseApiUrl;

       // Set resource or end point 
       var request = new RestRequest();
       request.Resource = "project/v1/list.json";
       request.Method = Method.GET;

       // Add parameters 
       request.AddParameter("company_id", companyId);
       request.AddParameter("api_key", apiKey);
       request.AddParameter("timestamp", timeStamp);
       request.AddParameter("sig", signature);
       request.AddParameter("auth_token", authToken);

       // (2) Execute request and get response
       IRestResponse response = client.Execute(request);

       // Save response. This is to see the response for our learning.
       m_lastResponse = response;

       if (response.StatusCode != HttpStatusCode.OK)
       {
          return null; 
       }

       // Get a list of projects.
       JsonDeserializer deserial = new JsonDeserializer();
       ProjectListResponse projListResponse = deserial.Deserialize<ProjectListResponse>(response);
       List<Project> proj_list = projListResponse.project_list;

       return proj_list;
    }


    ///===============================================================
    /// Model service: List
    /// Get a list of models for a given project. 
    /// URL
    /// https://b4.autodesk.com/api/model/v1/list.{format}?
    /// Methods: GET
    /// Doc
    /// http://b4.autodesk.com/api/model/v1/list/doc
    ///===============================================================

    public List<ModelInfo> ModelList(string authToken, string projectId)
    {
       string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
       string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);

       // (1) Build request 
       // set base url and authenticatopm info. 
       var client = new RestClient();
       client.BaseUrl = baseApiUrl;

       // Set resource or end point 
       var request = new RestRequest();
       request.Resource = "model/v1/list.json";
       request.Method = Method.GET;

       // Add parameters 
       request.AddParameter("company_id", companyId);
       request.AddParameter("api_key", apiKey);
       request.AddParameter("timestamp", timeStamp);
       request.AddParameter("sig", signature);
       request.AddParameter("auth_token", authToken);

       request.AddParameter("project_id", projectId);

       // (2) Execute request and get response
       IRestResponse response = client.Execute(request);

       // Save response. This is to see the response for our learning.
       m_lastResponse = response;

       if (response.StatusCode != HttpStatusCode.OK)
       {
          return null;
       }

       // Get a list of models.
       JsonDeserializer deserial = new JsonDeserializer();
       ModelListResponse modelListResponse = deserial.Deserialize<ModelListResponse>(response);
       List<ModelInfo> model_list = modelListResponse.model_list;

       return model_list;
    }

    ///===============================================================
    /// Viewer service: 
    /// URL
    /// https://b2.autodesk.com?
    /// 
    /// Two ways to pass parameters. Normal 5 args +:
    /// (1) "&runner=embedded/#” + company_id + ”/”+ project_id + ”/” + model_id
    /// (2) "&runner=embedded/#" + company_id + "/action" + "/" + action_id 
    /// 
    /// Doc 
    /// http://b4.autodesk.com/api/doc/doc_disp_comp.shtml
    /// Note: the doc is not updated at this point. but hopefully will
    /// be later. 
    /// 
    /// This function returns the url to display model in an embeded viewer. 
    ///===============================================================

    public string View(string authToken, string projectId, string modelId)
    {
      string timeStamp = Utils.GetUNIXEpochTimestamp().ToString();
      string signature = Utils.ComputeMD5Hash(apiKey + apiSecret + timeStamp);

      string callArgs = "";
      // We need these 5 arguments for every subsequest requests. 
      // Auth token is returned when you login. 
      callArgs += "&company_id=" + HttpUtility.UrlEncode(companyId);
      callArgs += "&api_key=" + HttpUtility.UrlEncode(apiKey);
      callArgs += "&timestamp=" + HttpUtility.UrlEncode(timeStamp);
      callArgs += "&sig=" + HttpUtility.UrlEncode(signature);
      callArgs += "&auth_token=" + HttpUtility.UrlEncode(authToken);
      // 

      // Two ways to pass parameters. 
      // (1) "&runner=embedded/#” + company_id + ”/”+ project_id + ”/” + model_id
      // (2) "&runner=embedded/#" + company_id + "/action" + "/" + action_id 
      // We use #1 here with the saved project and model ids. 

      //callArgs += "&runner=embedded/#" + company_id + "/action" + "/" + action_id;

      callArgs += "&runner=embedded/#" + HttpUtility.UrlEncode(companyId)
        + "/" + projectId + "/" + modelId;

      // Note: &modules=properties will make a properties window display. 
      // Not suited with this simplistic example, however. 
      //callArgs += "&runner=embedded&modules=properties/#" + HttpUtility.UrlEncode(companyId)
      //  + "/" + projectId + "/" + modelId;

      // URL that we are going to embed a web browser. 
      string url = baseViewerUrl + callArgs; 

      return url;
    }

  }
}
