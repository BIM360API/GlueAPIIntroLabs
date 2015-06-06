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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Reuse the Glue web services calls
using GlueAPIIntro;
// Added for RestSharp. 
using RestSharp;
using RestSharp.Deserializers;

///===================================================================
///     Welcome to Glue API Intro labs. 
/// 
/// This is the minimum web application using Glue API. 
/// No error checking, fancy style sheet for code redability.  
/// This simply does the following: 
/// login >> get a list of projects. choose an arbitrary one 
/// >> get a list of models. choose an arbitrary model. >> display 
/// 
///===================================================================

namespace GlueAPIWebIntro
{
   public partial class WebForm1 : System.Web.UI.Page
   {

      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void ButtonLogin_Click(object sender, EventArgs e)
      {
         // Glue Login call here. 
         string authToken = Glue.Login(TextBoxUserName.Text, TextBoxPassword.Text);

         bool authenticated = !string.IsNullOrEmpty(authToken);
         if (authenticated)
         {
            // Save authToken for this session 
            Session["authToken"] = authToken;
            TextBoxResponse.Text = authToken;
            ButtonLogin.Enabled = false;

            // Initialize indices of project and model index
            Session["projectIndex"] = 0; 
            Session["projectId"] = ""; 
            Session["modelIndex"] = 0;
            Session["modelId"] = ""; 
         }

            // Show the request and response in the form. 
            // This is for learning purpose. 
            IRestResponse response = Glue.m_lastResponse;
            TextBoxRequest.Text = response.ResponseUri.AbsoluteUri;
            TextBoxResponse.Text = response.Content;
      }

      protected void ButtonProject_Click(object sender, EventArgs e)
      {
         string authToken = HttpContext.Current.Session["authToken"] as string; 
         List<Project> proj_list = Glue.ProjectList(authToken);

         // Show the request and response in the form. 
         // This is for learning purpose. 
         IRestResponse response = Glue.m_lastResponse;
         TextBoxRequest.Text = response.ResponseUri.AbsoluteUri;
         TextBoxResponse.Text = response.Content; 

         // We want to get hold of one project. 
         // For simplicity, just pick up arbitrary one.

         int proj_index = Convert.ToInt32(HttpContext.Current.Session["projectIndex"]);
         proj_index %= proj_list.Count;
         Project proj = proj_list[proj_index++];
         string project_id = proj.project_id;
         string project_name = proj.project_name;
         Session["projectIndex"] = proj_index;
         Session["projectId"] = project_id; 

         TextBoxProjectName.Text = project_name + " (" + proj_index.ToString() + "/" + proj_list.Count.ToString() + ")";

         // No model in a viewer, yet. 
         iframeGlue.Src = ""; 
      }

      protected void ButtonModel_Click(object sender, EventArgs e)
      {
         string authToken = HttpContext.Current.Session["authToken"] as string;
         string project_id = HttpContext.Current.Session["projectId"] as string;

         List<ModelInfo> model_list = Glue.ModelList(authToken, project_id);

         // Show the request and response in the form. 
         // This is for learning purpose. 
         IRestResponse response = Glue.m_lastResponse;
         TextBoxRequest.Text = response.ResponseUri.AbsoluteUri;
         TextBoxResponse.Text = response.Content; 

         // We want to get hold of one model. 
         // For simplicity, just pick up arbitrary one.

         int model_index = Convert.ToInt32(HttpContext.Current.Session["modelIndex"]);
         model_index %= model_list.Count;
         ModelInfo model = model_list[model_index++];
         string model_id = model.model_id;
         string model_name = model.model_name;
         Session["modelIndex"] = model_index;
         Session["modelId"] = model_id; 

         TextBoxModelName.Text = model_name + " (" + model_index.ToString() + "/" + model_list.Count.ToString() + ")";

         // No model in a viewer, yet. 
         iframeGlue.Src = ""; 
      }

      protected void ButtonView_Click(object sender, EventArgs e)
      {
         string authToken = HttpContext.Current.Session["authToken"] as string;
         string project_id = HttpContext.Current.Session["projectId"] as string;
         string model_id = HttpContext.Current.Session["modelId"] as string;

         string url = Glue.View(authToken, project_id, model_id);

         // Show the request and response in the form. 
         TextBoxRequest.Text = url;
         TextBoxResponse.Text = "displaying model..."; 

         // embed a viewer in iframe 
         iframeGlue.Src = url; 
      }

   }
}