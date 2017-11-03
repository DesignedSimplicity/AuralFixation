namespace AuralFixation.Win
{
    partial class Picker
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
			this.list = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// list
			// 
			this.list.BackColor = System.Drawing.SystemColors.Control;
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.ForeColor = System.Drawing.SystemColors.ControlText;
			this.list.Location = new System.Drawing.Point(0, 0);
			this.list.MultiSelect = false;
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(1582, 959);
			this.list.TabIndex = 0;
			this.list.UseCompatibleStateImageBehavior = false;
			// 
			// Picker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1582, 959);
			this.Controls.Add(this.list);
			this.Name = "Picker";
			this.Text = "Picker";
			this.Load += new System.EventHandler(this.Picker_Load);
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.ListView list;
	}
}

