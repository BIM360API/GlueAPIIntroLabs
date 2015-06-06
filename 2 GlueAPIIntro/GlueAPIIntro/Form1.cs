#region Copyright
//
// Copyright (C) 2013 by Autodesk, Inc.
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
// Written by M.Harada 
#endregion // Copyright

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics; // for Debug.
// Added for RestSharp. 
using RestSharp;
using RestSharp.Deserializers;


namespace GlueAPIIntro
{
  public partial class Form1 : Form
  {
    // Glue defines the actual Glue specic call 
    Glue glueCall = new Glue();

     // Save auth token. 
    private static string m_authToken = "";
    private static string m_project_id = ""; 
    private static int m_proj_index = 0;    
    private static string m_model_id = ""; 
    private static int m_model_index = 0;

    public Form1()
    {
      InitializeComponent();
    }

    private void buttonLogin_Click(object sender, EventArgs e)
    {
       // Get the user name and password from the user. 
       string userName = textBoxUserName.Text;
       string password = textBoxPassword.Text;

      textBoxRequest.Text = "Request comes here"; 
      textBoxResponse.Text = "Response comes here. This may take secones. Please wait...";
      this.Update();
      
      // Here is the main part that we call Glue login 
      m_authToken = glueCall.Login(userName, password);

      // Show the request and response in the form. 
      // This is for learning purpose. 
      IRestResponse response = glueCall.m_lastResponse; 
      textBoxRequest.Text = response.ResponseUri.AbsoluteUri; 
      textBoxResponse.Text = response.Content;
    }

    private void buttonLogout_Click(object sender, EventArgs e)
    {
       textBoxRequest.Text = "Request comes here";
       textBoxResponse.Text = "Response comes here. This may take secones. Please wait...";
       this.Update();

       // Here is the main part that we call Glue login 
       string logoutResponse = glueCall.Logout(m_authToken);

       // Show the request and response in the form. 
       // This is for learning purpose. 
       IRestResponse response = glueCall.m_lastResponse;
       textBoxRequest.Text = response.ResponseUri.AbsoluteUri;
       textBoxResponse.Text = response.Content;
    }

    private void buttonProjects_Click(object sender, EventArgs e)
    {
      textBoxRequest.Text = "Request comes here";
      textBoxResponse.Text = "Response comes here";

      List<Project> proj_list = glueCall.ProjectList(m_authToken);

      // Show the request and response in the form. 
      // This is for learning purpose. 
      IRestResponse response = glueCall.m_lastResponse; 

      textBoxRequest.Text = response.ResponseUri.AbsoluteUri;
      textBoxResponse.Text = response.Content; 

      // We want to get hold of one project. 
      // For simplicity, just pick up arbitrary one.
      m_proj_index %= proj_list.Count; 
      Project proj = proj_list[m_proj_index++];
      m_project_id = proj.project_id;
      string project_name = proj.project_name;

      labelCurProj.Text = project_name + " (" + m_proj_index.ToString() + "/" + proj_list.Count.ToString() + ")"; 
    }

    private void buttonModels_Click(object sender, EventArgs e)
    {
      textBoxRequest.Text = "Request comes here";
      textBoxResponse.Text = "Response comes here";

      List<ModelInfo> model_list = glueCall.ModelList(m_authToken, m_project_id);

      // Show the request and response in the form. 
      // This is for learning purpose. 
      IRestResponse response = glueCall.m_lastResponse;

      textBoxRequest.Text = response.ResponseUri.AbsoluteUri;
      textBoxResponse.Text = response.Content;

      // We want to get hold of one model. 
      // For simplicity, just pick up arbitrary one.
      m_model_index %= model_list.Count;
      ModelInfo model = model_list[m_model_index++];
      m_model_id = model.model_id;
      string model_name = model.model_name;

      labelCurModel.Text = model_name + " (" + m_model_index.ToString() + "/" + model_list.Count.ToString() + ")"; 

    }

    private void buttonView_Click(object sender, EventArgs e)
    {
      textBoxRequest.Text = "Request comes here";
      textBoxResponse.Text = "Response comes here";

      // The response we get here is URL that we embed in iframe. 
      // Let's see what we got by showing in the response text box.
      // If you take this string and copy&paste in a simple html 
      // file with an ifarme, you will see a model.
      // Note: viewer is not supported in windows control.
      // ScriptErrorsSuppressed is set to true to suppress script errors. 
      // We are using it here to learn web services API. 

      string url = glueCall.View(m_authToken, m_project_id, m_model_id);

      textBoxRequest.Text = url; 
      textBoxResponse.Text = "displaying model...";
      this.Update(); 

      // a view embedded form's web browser by URL.
      webBrowser1.Url = new System.Uri(url); 

    }

  }
}
