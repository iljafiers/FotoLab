namespace FotoProducent
{
    partial class FormFotoProducent
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Retrieve = new System.Windows.Forms.Button();
            this.textBoxKlantID = new System.Windows.Forms.TextBox();
            this.KlantKey = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KlantNaam = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSelecteer = new System.Windows.Forms.Button();
            this.listBoxFotos = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonVerwijder = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Klant Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Retrieve);
            this.groupBox1.Controls.Add(this.textBoxKlantID);
            this.groupBox1.Controls.Add(this.KlantKey);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.KlantNaam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 149);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Klant informatie";
            // 
            // Retrieve
            // 
            this.Retrieve.Location = new System.Drawing.Point(389, 31);
            this.Retrieve.Name = "Retrieve";
            this.Retrieve.Size = new System.Drawing.Size(75, 23);
            this.Retrieve.TabIndex = 7;
            this.Retrieve.Text = "Ophalen";
            this.Retrieve.UseVisualStyleBackColor = true;
            this.Retrieve.Click += new System.EventHandler(this.OnClickOphalenKlant);
            // 
            // textBoxKlantID
            // 
            this.textBoxKlantID.Location = new System.Drawing.Point(150, 31);
            this.textBoxKlantID.Name = "textBoxKlantID";
            this.textBoxKlantID.Size = new System.Drawing.Size(73, 20);
            this.textBoxKlantID.TabIndex = 6;
            // 
            // KlantKey
            // 
            this.KlantKey.AutoSize = true;
            this.KlantKey.Location = new System.Drawing.Point(147, 92);
            this.KlantKey.Name = "KlantKey";
            this.KlantKey.Size = new System.Drawing.Size(13, 13);
            this.KlantKey.TabIndex = 5;
            this.KlantKey.Text = "--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Klant key:";
            // 
            // KlantNaam
            // 
            this.KlantNaam.AutoSize = true;
            this.KlantNaam.Location = new System.Drawing.Point(147, 63);
            this.KlantNaam.Name = "KlantNaam";
            this.KlantNaam.Size = new System.Drawing.Size(13, 13);
            this.KlantNaam.TabIndex = 3;
            this.KlantNaam.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Naam:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonVerwijder);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.buttonSelecteer);
            this.groupBox2.Controls.Add(this.listBoxFotos);
            this.groupBox2.Location = new System.Drawing.Point(13, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(485, 268);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fotos";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(150, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Naam van fotoserie:";
            // 
            // buttonSelecteer
            // 
            this.buttonSelecteer.Location = new System.Drawing.Point(389, 65);
            this.buttonSelecteer.Name = "buttonSelecteer";
            this.buttonSelecteer.Size = new System.Drawing.Size(75, 23);
            this.buttonSelecteer.TabIndex = 1;
            this.buttonSelecteer.Text = "Selecteer...";
            this.buttonSelecteer.UseVisualStyleBackColor = true;
            this.buttonSelecteer.Click += new System.EventHandler(this.OnClickBrowse);
            // 
            // listBoxFotos
            // 
            this.listBoxFotos.FormattingEnabled = true;
            this.listBoxFotos.Location = new System.Drawing.Point(22, 65);
            this.listBoxFotos.Name = "listBoxFotos";
            this.listBoxFotos.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFotos.Size = new System.Drawing.Size(340, 173);
            this.listBoxFotos.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 471);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Upload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClickUpload);
            // 
            // buttonVerwijder
            // 
            this.buttonVerwijder.Location = new System.Drawing.Point(389, 94);
            this.buttonVerwijder.Name = "buttonVerwijder";
            this.buttonVerwijder.Size = new System.Drawing.Size(75, 23);
            this.buttonVerwijder.TabIndex = 10;
            this.buttonVerwijder.Text = "Verwijder...";
            this.buttonVerwijder.UseVisualStyleBackColor = true;
            this.buttonVerwijder.Click += new System.EventHandler(this.buttonVerwijderClicked);
            // 
            // FormFotoProducent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 522);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormFotoProducent";
            this.Text = "Foto producent - Client app";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label KlantNaam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Retrieve;
        private System.Windows.Forms.TextBox textBoxKlantID;
        private System.Windows.Forms.Label KlantKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSelecteer;
        private System.Windows.Forms.ListBox listBoxFotos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonVerwijder;
    }
}

