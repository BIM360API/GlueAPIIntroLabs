<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GlueAPIWebIntro.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Glue API Web Intro</title>
    <style type="text/css">
        #form1
        {
            height: 171px;
            width: 577px;
        }
        body
        {
            background-color:#e6e0f1; 
        }
        h1
        {
            color:#2a2e74; 
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>My First Glue API</h1>
    </div>
        User Name:&nbsp;&nbsp;
        <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonProject" runat="server" OnClick="ButtonProject_Click" Text="Project" Width="80px" />
        <br />
        <br />
        Password:&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonLogin" runat="server" OnClick="ButtonLogin_Click" style="text-align: left" Text="Login" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonModel" runat="server" OnClick="ButtonModel_Click" Text="Model" Width="80px" />
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Height="196px" Width="575px">
            <asp:Label ID="LabelRequest" runat="server" Text="Request"></asp:Label>
            <br />
            <asp:TextBox ID="TextBoxRequest" runat="server" Height="45px" Width="450px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            Response
            <br />
            <asp:TextBox ID="TextBoxResponse" runat="server" Height="85px" Width="450px" TextMode="MultiLine"></asp:TextBox>

        </asp:Panel>
        <br />
        Project name:&nbsp;&nbsp;<asp:TextBox ID="TextBoxProjectName" runat="server" Width="180px"></asp:TextBox>
&nbsp;
        <br />
        Model name:&nbsp; &nbsp;<asp:TextBox ID="TextBoxModelName" runat="server" Width="180px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ButtonView" runat="server" OnClick="ButtonView_Click" Text="View" Width="75px" />
        <br />
        <br />
        <iframe id="iframeGlue" title="BIM 360 Glue Display Component" width="800" height="600" src="" runat="server"></iframe>

    </form>
</body>
</html>
 