namespace RdpNotifier
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            textBox1 = new System.Windows.Forms.TextBox();
            bindingSource1 = new System.Windows.Forms.BindingSource(components);
            label1 = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", bindingSource1, "Address", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            textBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            textBox1.Location = new System.Drawing.Point(30, 40);
            textBox1.Margin = new System.Windows.Forms.Padding(0);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(422, 32);
            textBox1.TabIndex = 1;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(MainViewModel);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            label1.Location = new System.Drawing.Point(30, 20);
            label1.Margin = new System.Windows.Forms.Padding(0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(65, 20);
            label1.TabIndex = 0;
            label1.Text = "Address:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", bindingSource1, "PlaySound", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            checkBox1.Location = new System.Drawing.Point(30, 72);
            checkBox1.Margin = new System.Windows.Forms.Padding(0);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(306, 25);
            checkBox1.TabIndex = 2;
            checkBox1.Text = "Play sound when connectivity is restored";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label2.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", bindingSource1, "StatusColor", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            label2.DataBindings.Add(new System.Windows.Forms.Binding("Text", bindingSource1, "Status", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            label2.Font = new System.Drawing.Font("Segoe UI Semilight", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(30, 97);
            label2.Margin = new System.Windows.Forms.Padding(0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(422, 41);
            label2.TabIndex = 3;
            label2.Text = "✔ remote.contoso.com is online";
            label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            label2.UseMnemonic = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(482, 158);
            Controls.Add(textBox1);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            ShowIcon = false;
            Text = "RDP Notifier";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}