using System.ComponentModel;

namespace LabMPP;

partial class User
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
        dataGridView1 = new System.Windows.Forms.DataGridView();
        dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
        searchButton = new System.Windows.Forms.Button();
        exitButton = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        comboBox1 = new System.Windows.Forms.ComboBox();
        dataGridView2 = new System.Windows.Forms.DataGridView();
        label3 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        seatsTextBox = new System.Windows.Forms.TextBox();
        nameTextBox = new System.Windows.Forms.TextBox();
        reservationButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
        SuspendLayout();
        // 
        // dataGridView1
        // 
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
        dataGridView1.BackgroundColor = System.Drawing.Color.SteelBlue;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.GridColor = System.Drawing.SystemColors.InactiveCaption;
        dataGridView1.Location = new System.Drawing.Point(21, 170);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.Size = new System.Drawing.Size(369, 269);
        dataGridView1.TabIndex = 0;
        dataGridView1.Text = "dataGridView1";
        // 
        // dateTimePicker1
        // 
        dateTimePicker1.Location = new System.Drawing.Point(21, 127);
        dateTimePicker1.Name = "dateTimePicker1";
        dateTimePicker1.Size = new System.Drawing.Size(334, 27);
        dateTimePicker1.TabIndex = 2;
        // 
        // searchButton
        // 
        searchButton.BackColor = System.Drawing.Color.DarkBlue;
        searchButton.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        searchButton.ForeColor = System.Drawing.Color.Lavender;
        searchButton.Location = new System.Drawing.Point(251, 78);
        searchButton.Name = "searchButton";
        searchButton.Size = new System.Drawing.Size(139, 34);
        searchButton.TabIndex = 3;
        searchButton.Text = "Search";
        searchButton.UseVisualStyleBackColor = false;
        searchButton.Click += new EventHandler(searchButton_Click);
        // 
        // exitButton
        // 
        exitButton.BackColor = System.Drawing.Color.DarkBlue;
        exitButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        exitButton.ForeColor = System.Drawing.Color.Lavender;
        exitButton.Location = new System.Drawing.Point(646, 16);
        exitButton.Name = "exitButton";
        exitButton.Size = new System.Drawing.Size(139, 37);
        exitButton.TabIndex = 4;
        exitButton.Text = "Log Out";
        exitButton.UseVisualStyleBackColor = false;
        exitButton.Click += new EventHandler(exitButton_Click);
        // 
        // label1
        // 
        label1.Font = new System.Drawing.Font("Trebuchet MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label1.ForeColor = System.Drawing.Color.Navy;
        label1.Location = new System.Drawing.Point(314, 16);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(257, 49);
        label1.TabIndex = 5;
        label1.Text = "Welcome!";
        // 
        // comboBox1
        // 
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(21, 84);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(207, 28);
        comboBox1.TabIndex = 7;
        comboBox1.Text = "Destination";
        // 
        // dataGridView2
        // 
        dataGridView2.BackgroundColor = System.Drawing.Color.SteelBlue;
        dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView2.Location = new System.Drawing.Point(439, 278);
        dataGridView2.Name = "dataGridView2";
        dataGridView2.RowHeadersWidth = 51;
        dataGridView2.Size = new System.Drawing.Size(336, 160);
        dataGridView2.TabIndex = 8;
        dataGridView2.Text = "dataGridView2";
        // 
        // label3
        // 
        label3.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label3.Location = new System.Drawing.Point(440, 84);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(234, 27);
        label3.TabIndex = 9;
        label3.Text = "Number of seats:";
        // 
        // label2
        // 
        label2.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        label2.Location = new System.Drawing.Point(439, 155);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(194, 31);
        label2.TabIndex = 10;
        label2.Text = "Client\'s name:";
        // 
        // seatsTextBox
        // 
        seatsTextBox.Location = new System.Drawing.Point(439, 114);
        seatsTextBox.Name = "seatsTextBox";
        seatsTextBox.Size = new System.Drawing.Size(270, 27);
        seatsTextBox.TabIndex = 11;
        // 
        // nameTextBox
        // 
        nameTextBox.Location = new System.Drawing.Point(437, 189);
        nameTextBox.Name = "nameTextBox";
        nameTextBox.Size = new System.Drawing.Size(272, 27);
        nameTextBox.TabIndex = 12;
        // 
        // reservationButton
        // 
        reservationButton.BackColor = System.Drawing.Color.DarkBlue;
        reservationButton.Font = new System.Drawing.Font("Trebuchet MS", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
        reservationButton.ForeColor = System.Drawing.Color.Lavender;
        reservationButton.Location = new System.Drawing.Point(628, 229);
        reservationButton.Name = "reservationButton";
        reservationButton.Size = new System.Drawing.Size(147, 43);
        reservationButton.TabIndex = 13;
        reservationButton.Text = "Reserve";
        reservationButton.UseVisualStyleBackColor = false;
        reservationButton.Click += new EventHandler(reservationButton_Click);
        // 
        // User
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.InactiveCaption;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(reservationButton);
        Controls.Add(nameTextBox);
        Controls.Add(seatsTextBox);
        Controls.Add(label2);
        Controls.Add(label3);
        Controls.Add(dataGridView2);
        Controls.Add(comboBox1);
        Controls.Add(label1);
        Controls.Add(exitButton);
        Controls.Add(searchButton);
        Controls.Add(dateTimePicker1);
        Controls.Add(dataGridView1);
        Text = "User";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox seatsTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Button reservationButton;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.DataGridView dataGridView2;

    private System.Windows.Forms.ComboBox comboBox1;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DateTimePicker dateTimePicker1;
    private System.Windows.Forms.Button searchButton;
    private System.Windows.Forms.Button exitButton;

    #endregion
}