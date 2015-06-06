namespace HelloGlueWorld
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.labelUserName = new System.Windows.Forms.Label();
         this.labelPassword = new System.Windows.Forms.Label();
         this.textBoxUserName = new System.Windows.Forms.TextBox();
         this.textBoxPassword = new System.Windows.Forms.TextBox();
         this.buttonLogin = new System.Windows.Forms.Button();
         this.labelRequest = new System.Windows.Forms.Label();
         this.labelResponse = new System.Windows.Forms.Label();
         this.textBoxRequest = new System.Windows.Forms.TextBox();
         this.textBoxResponse = new System.Windows.Forms.TextBox();
         this.SuspendLayout();
         // 
         // labelUserName
         // 
         this.labelUserName.AutoSize = true;
         this.labelUserName.Location = new System.Drawing.Point(20, 13);
         this.labelUserName.Name = "labelUserName";
         this.labelUserName.Size = new System.Drawing.Size(60, 13);
         this.labelUserName.TabIndex = 0;
         this.labelUserName.Text = "User Name";
         // 
         // labelPassword
         // 
         this.labelPassword.AutoSize = true;
         this.labelPassword.Location = new System.Drawing.Point(20, 43);
         this.labelPassword.Name = "labelPassword";
         this.labelPassword.Size = new System.Drawing.Size(53, 13);
         this.labelPassword.TabIndex = 1;
         this.labelPassword.Text = "Password";
         // 
         // textBoxUserName
         // 
         this.textBoxUserName.Location = new System.Drawing.Point(90, 13);
         this.textBoxUserName.Name = "textBoxUserName";
         this.textBoxUserName.Size = new System.Drawing.Size(193, 20);
         this.textBoxUserName.TabIndex = 2;
         // 
         // textBoxPassword
         // 
         this.textBoxPassword.Location = new System.Drawing.Point(90, 43);
         this.textBoxPassword.Name = "textBoxPassword";
         this.textBoxPassword.PasswordChar = '*';
         this.textBoxPassword.Size = new System.Drawing.Size(193, 20);
         this.textBoxPassword.TabIndex = 3;
         // 
         // buttonLogin
         // 
         this.buttonLogin.Location = new System.Drawing.Point(138, 78);
         this.buttonLogin.Name = "buttonLogin";
         this.buttonLogin.Size = new System.Drawing.Size(86, 23);
         this.buttonLogin.TabIndex = 4;
         this.buttonLogin.Text = "Login";
         this.buttonLogin.UseVisualStyleBackColor = true;
         this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
         // 
         // labelRequest
         // 
         this.labelRequest.AutoSize = true;
         this.labelRequest.Location = new System.Drawing.Point(20, 117);
         this.labelRequest.Name = "labelRequest";
         this.labelRequest.Size = new System.Drawing.Size(47, 13);
         this.labelRequest.TabIndex = 5;
         this.labelRequest.Text = "Request";
         // 
         // labelResponse
         // 
         this.labelResponse.AutoSize = true;
         this.labelResponse.Location = new System.Drawing.Point(20, 194);
         this.labelResponse.Name = "labelResponse";
         this.labelResponse.Size = new System.Drawing.Size(55, 13);
         this.labelResponse.TabIndex = 6;
         this.labelResponse.Text = "Response";
         // 
         // textBoxRequest
         // 
         this.textBoxRequest.Location = new System.Drawing.Point(20, 133);
         this.textBoxRequest.Multiline = true;
         this.textBoxRequest.Name = "textBoxRequest";
         this.textBoxRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxRequest.Size = new System.Drawing.Size(320, 58);
         this.textBoxRequest.TabIndex = 7;
         // 
         // textBoxResponse
         // 
         this.textBoxResponse.Location = new System.Drawing.Point(20, 210);
         this.textBoxResponse.Multiline = true;
         this.textBoxResponse.Name = "textBoxResponse";
         this.textBoxResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
         this.textBoxResponse.Size = new System.Drawing.Size(320, 180);
         this.textBoxResponse.TabIndex = 8;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(362, 412);
         this.Controls.Add(this.textBoxResponse);
         this.Controls.Add(this.textBoxRequest);
         this.Controls.Add(this.labelResponse);
         this.Controls.Add(this.labelRequest);
         this.Controls.Add(this.buttonLogin);
         this.Controls.Add(this.textBoxPassword);
         this.Controls.Add(this.textBoxUserName);
         this.Controls.Add(this.labelPassword);
         this.Controls.Add(this.labelUserName);
         this.Name = "Form1";
         this.Text = "Glue API Intro";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label labelUserName;
      private System.Windows.Forms.Label labelPassword;
      private System.Windows.Forms.TextBox textBoxUserName;
      private System.Windows.Forms.TextBox textBoxPassword;
      private System.Windows.Forms.Button buttonLogin;
      private System.Windows.Forms.Label labelRequest;
      private System.Windows.Forms.Label labelResponse;
      private System.Windows.Forms.TextBox textBoxRequest;
      private System.Windows.Forms.TextBox textBoxResponse;
   }
}

