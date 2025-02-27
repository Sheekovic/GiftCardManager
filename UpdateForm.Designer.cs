namespace Card_Manager
{
    partial class UpdateForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ProgressBar progressBar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblStatus = new System.Windows.Forms.Label();
            btnUpdate = new System.Windows.Forms.Button();
            progressBar = new System.Windows.Forms.ProgressBar();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatus.Font = new System.Drawing.Font("Lucida Handwriting", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lblStatus.ForeColor = System.Drawing.Color.Gold;
            lblStatus.Location = new System.Drawing.Point(16, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(425, 88);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Checking for updates...";
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
            btnUpdate.BackColor = System.Drawing.Color.Black;
            btnUpdate.Font = new System.Drawing.Font("Lucida Handwriting", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnUpdate.ForeColor = System.Drawing.Color.Gold;
            btnUpdate.Location = new System.Drawing.Point(142, 91);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new System.Drawing.Size(173, 82);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Visible = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // progressBar
            // 
            progressBar.BackColor = System.Drawing.Color.Black;
            progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            progressBar.ForeColor = System.Drawing.Color.Gold;
            progressBar.Location = new System.Drawing.Point(16, 179);
            progressBar.Name = "progressBar";
            progressBar.Size = new System.Drawing.Size(425, 24);
            progressBar.TabIndex = 2;
            progressBar.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.030303F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.969696F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            tableLayoutPanel1.Controls.Add(btnUpdate, 1, 1);
            tableLayoutPanel1.Controls.Add(progressBar, 1, 2);
            tableLayoutPanel1.Controls.Add(lblStatus, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            tableLayoutPanel1.Size = new System.Drawing.Size(453, 206);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // UpdateForm
            // 
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(453, 206);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Lucida Handwriting", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "UpdateForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "GCM UPDATER";
            Load += UpdateForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
