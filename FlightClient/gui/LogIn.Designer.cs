using System.ComponentModel;

namespace LabMPP;

partial class LogIn
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        UsernameTextBox = new System.Windows.Forms.TextBox();
        PasswordTextBox = new System.Windows.Forms.TextBox();
        LogInButton = new System.Windows.Forms.Button();
        ExitButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Trebuchet MS", 19.800001F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.ForeColor = System.Drawing.Color.Navy;
        label1.Location = new System.Drawing.Point(276, 34);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(227, 62);
        label1.TabIndex = 0;
        label1.Text = "Login";
        label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Trebuchet MS", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label2.ForeColor = System.Drawing.Color.Navy;
        label2.Location = new System.Drawing.Point(163, 126);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(200, 44);
        label2.TabIndex = 1;
        label2.Text = "Name:";
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Trebuchet MS", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label3.ForeColor = System.Drawing.Color.Navy;
        label3.Location = new System.Drawing.Point(118, 223);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(160, 72);
        label3.TabIndex = 2;
        label3.Text = "Password:";
        // 
        // UsernameTextBox
        // 
        UsernameTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        UsernameTextBox.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        UsernameTextBox.Location = new System.Drawing.Point(259, 126);
        UsernameTextBox.Name = "UsernameTextBox";
        UsernameTextBox.Size = new System.Drawing.Size(288, 39);
        UsernameTextBox.TabIndex = 3;
        // 
        // PasswordTextBox
        // 
        PasswordTextBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        PasswordTextBox.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        PasswordTextBox.Location = new System.Drawing.Point(259, 223);
        PasswordTextBox.Name = "PasswordTextBox";
        PasswordTextBox.Size = new System.Drawing.Size(291, 39);
        PasswordTextBox.TabIndex = 4;
        PasswordTextBox.UseSystemPasswordChar = true;
        // 
        // LogInButton
        // 
        LogInButton.BackColor = System.Drawing.Color.DarkBlue;
        LogInButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        LogInButton.ForeColor = System.Drawing.Color.Lavender;
        LogInButton.Location = new System.Drawing.Point(163, 320);
        LogInButton.Name = "LogInButton";
        LogInButton.Size = new System.Drawing.Size(153, 46);
        LogInButton.TabIndex = 5;
        LogInButton.Text = "Login";
        LogInButton.UseVisualStyleBackColor = false;
        LogInButton.Click += new EventHandler(LogIn_Click);
        // 
        // ExitButton
        // 
        ExitButton.BackColor = System.Drawing.Color.DarkBlue;
        ExitButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        ExitButton.ForeColor = System.Drawing.Color.Lavender;
        ExitButton.Location = new System.Drawing.Point(473, 323);
        ExitButton.Name = "ExitButton";
        ExitButton.Size = new System.Drawing.Size(133, 43);
        ExitButton.TabIndex = 6;
        ExitButton.Text = "Exit";
        ExitButton.UseVisualStyleBackColor = false;
        ExitButton.Click += new EventHandler(ExitButton_Click);
        // 
        // LogIn
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.GradientActiveCaption;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(ExitButton);
        Controls.Add(LogInButton);
        Controls.Add(PasswordTextBox);
        Controls.Add(UsernameTextBox);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Text = "LogIn";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button ExitButton;

    private System.Windows.Forms.TextBox UsernameTextBox;
    private System.Windows.Forms.TextBox PasswordTextBox;
    private System.Windows.Forms.Button LogInButton;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;

    #endregion
}