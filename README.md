# GlueAPIIntroLabs
Set of Samples for Getting Started with BIM 360 Glue APIs. See more at: http://the360view.typepad.com/blog/2015/01/glue-api-intro-labs-overview.html


================================
  Glue API Intro Training Labs 
================================

This folder contains introductory materials for BIM 360 Glue API.

The labs consist of four modules. Starting from Lab1, it incrementally 
adds code or reuse the code you have written, and implement a simple web service application  
 

Lab1: HelloGlueWorld 
--------------------  

The minimum project that demonstrates how to make Glue REST API call. This lab, you learn what REST API is, how to make a request and obtain response, and login to Glue web services through API. 


Lab2: GlueAPIIntro 
------------------

In this lab, you will learn how to access to a project list and a model list in the Glue,  and display it in a Glue display component or a viewer.  


Lab3: GlueAPIWebIntro
--------------------- 

In the previous two labs, we looked at Glue API as desktop client application. In this lab, we write a minimum, single page web service in APS.NET. We will reuse most of Glue API call itself, and add UI layers as a web page.   


Lab4: GlueAPIWebIntroJS
-----------------------

In this lab, we build on top of Lab3 and add JavaScript layer to add selection functionality in the viewer. you will learn how to get properties of a selected object and zoom into a selected object. 



* How to run the sample project

In order to use Glue API, you will need API key and secret assigned to you. 
You will also need an account to Glue host. 

Samples are written in C#, using Microsoft Visual Studio 2012. 

Before you build, please set your own keys and host name in the code: 

In Labs1, 3, 4, 

in Glue.cs >> class Glue >> 
 - apiKey
 - apiSecret
 - compnayId

 
in Lab 2, 

in App.config
 - publicKey
 - privateKey
 - company 
 
 

