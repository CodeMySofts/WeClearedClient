namespace WeCleared
{
    partial class frmWeCleared
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeCleared));
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblPath = new System.Windows.Forms.Label();
            this.btnFindFolder = new System.Windows.Forms.Button();
            this.dataGridAddons = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMinimise = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbSelectAddon = new System.Windows.Forms.ComboBox();
            this.btnActivateAddon = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pgbUpdating = new System.Windows.Forms.ProgressBar();
            this.tmrProgress = new System.Windows.Forms.Timer(this.components);
            this.lblUpdateClient = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAddons)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 500000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // lblPath
            // 
            this.lblPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPath.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.ForeColor = System.Drawing.Color.White;
            this.lblPath.Location = new System.Drawing.Point(147, 335);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(469, 31);
            this.lblPath.TabIndex = 0;
            this.lblPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            // 
            // btnFindFolder
            // 
            this.btnFindFolder.BackColor = System.Drawing.Color.Gray;
            this.btnFindFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindFolder.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindFolder.Location = new System.Drawing.Point(12, 335);
            this.btnFindFolder.Name = "btnFindFolder";
            this.btnFindFolder.Size = new System.Drawing.Size(129, 31);
            this.btnFindFolder.TabIndex = 1;
            this.btnFindFolder.Text = "Dossier WoW";
            this.btnFindFolder.UseVisualStyleBackColor = false;
            this.btnFindFolder.Click += new System.EventHandler(this.btnFindFolder_Click);
            // 
            // dataGridAddons
            // 
            this.dataGridAddons.AllowUserToAddRows = false;
            this.dataGridAddons.AllowUserToDeleteRows = false;
            this.dataGridAddons.AllowUserToResizeColumns = false;
            this.dataGridAddons.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridAddons.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridAddons.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridAddons.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridAddons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridAddons.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridAddons.GridColor = System.Drawing.Color.Black;
            this.dataGridAddons.Location = new System.Drawing.Point(12, 47);
            this.dataGridAddons.Name = "dataGridAddons";
            this.dataGridAddons.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridAddons.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridAddons.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridAddons.Size = new System.Drawing.Size(1000, 281);
            this.dataGridAddons.TabIndex = 2;
            this.dataGridAddons.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGridAddons_ColumnAdded);
            this.dataGridAddons.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(194, 31);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "We Cleared Client";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(981, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 31);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMinimise
            // 
            this.btnMinimise.BackColor = System.Drawing.Color.Gray;
            this.btnMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimise.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimise.Location = new System.Drawing.Point(945, 9);
            this.btnMinimise.Name = "btnMinimise";
            this.btnMinimise.Size = new System.Drawing.Size(30, 31);
            this.btnMinimise.TabIndex = 5;
            this.btnMinimise.Text = "_";
            this.btnMinimise.UseVisualStyleBackColor = false;
            this.btnMinimise.Click += new System.EventHandler(this.btnMinimise_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Gray;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(622, 335);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(129, 31);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUpdate_MouseDown);
            // 
            // cmbSelectAddon
            // 
            this.cmbSelectAddon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmbSelectAddon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSelectAddon.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSelectAddon.ForeColor = System.Drawing.Color.White;
            this.cmbSelectAddon.FormattingEnabled = true;
            this.cmbSelectAddon.Location = new System.Drawing.Point(757, 336);
            this.cmbSelectAddon.Name = "cmbSelectAddon";
            this.cmbSelectAddon.Size = new System.Drawing.Size(217, 29);
            this.cmbSelectAddon.TabIndex = 7;
            this.cmbSelectAddon.SelectedIndexChanged += new System.EventHandler(this.cmbSelectAddon_SelectedIndexChanged);
            // 
            // btnActivateAddon
            // 
            this.btnActivateAddon.BackColor = System.Drawing.Color.DarkRed;
            this.btnActivateAddon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivateAddon.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivateAddon.Location = new System.Drawing.Point(981, 335);
            this.btnActivateAddon.Name = "btnActivateAddon";
            this.btnActivateAddon.Size = new System.Drawing.Size(31, 31);
            this.btnActivateAddon.TabIndex = 8;
            this.btnActivateAddon.UseVisualStyleBackColor = false;
            this.btnActivateAddon.Click += new System.EventHandler(this.btnActivateAddon_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInfo.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(315, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(624, 31);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            // 
            // pgbUpdating
            // 
            this.pgbUpdating.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pgbUpdating.Location = new System.Drawing.Point(709, 9);
            this.pgbUpdating.Name = "pgbUpdating";
            this.pgbUpdating.Size = new System.Drawing.Size(230, 31);
            this.pgbUpdating.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbUpdating.TabIndex = 10;
            this.pgbUpdating.Visible = false;
            // 
            // tmrProgress
            // 
            this.tmrProgress.Interval = 50;
            this.tmrProgress.Tick += new System.EventHandler(this.tmrProgress_Tick);
            // 
            // lblUpdateClient
            // 
            this.lblUpdateClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.lblUpdateClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUpdateClient.Font = new System.Drawing.Font("Franklin Gothic Medium", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblUpdateClient.ForeColor = System.Drawing.Color.White;
            this.lblUpdateClient.Location = new System.Drawing.Point(204, 9);
            this.lblUpdateClient.Name = "lblUpdateClient";
            this.lblUpdateClient.Size = new System.Drawing.Size(105, 31);
            this.lblUpdateClient.TabIndex = 12;
            this.lblUpdateClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmWeCleared
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1023, 374);
            this.Controls.Add(this.lblUpdateClient);
            this.Controls.Add(this.pgbUpdating);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnActivateAddon);
            this.Controls.Add(this.cmbSelectAddon);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnMinimise);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridAddons);
            this.Controls.Add(this.btnFindFolder);
            this.Controls.Add(this.lblPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWeCleared";
            this.Text = "We Cleared Client";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Interface_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAddons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnFindFolder;
        private System.Windows.Forms.DataGridView dataGridAddons;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMinimise;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox cmbSelectAddon;
        private System.Windows.Forms.Button btnActivateAddon;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ProgressBar pgbUpdating;
        private System.Windows.Forms.Timer tmrProgress;
        private System.Windows.Forms.Label lblUpdateClient;
    }
}

