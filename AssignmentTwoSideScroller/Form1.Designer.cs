namespace AssignmentTwoSideScroller
{
    partial class SideScroller
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
            this.components = new System.ComponentModel.Container();
            this.tmrProjectile = new System.Windows.Forms.Timer(this.components);
            this.txtEnemy = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrMovements = new System.Windows.Forms.Timer(this.components);
            this.tmrProjectileSpawn = new System.Windows.Forms.Timer(this.components);
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblNumOfEnemies = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserError = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrProjectile
            // 
            this.tmrProjectile.Interval = 50;
            this.tmrProjectile.Tick += new System.EventHandler(this.tmrProjectile_Tick);
            // 
            // txtEnemy
            // 
            this.txtEnemy.Enabled = false;
            this.txtEnemy.Location = new System.Drawing.Point(335, 251);
            this.txtEnemy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEnemy.Name = "txtEnemy";
            this.txtEnemy.Size = new System.Drawing.Size(429, 22);
            this.txtEnemy.TabIndex = 1;
            this.txtEnemy.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(771, 273);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Visible = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrMovements
            // 
            this.tmrMovements.Interval = 25;
            this.tmrMovements.Tick += new System.EventHandler(this.tmrMovements_Tick);
            // 
            // tmrProjectileSpawn
            // 
            this.tmrProjectileSpawn.Interval = 2000;
            this.tmrProjectileSpawn.Tick += new System.EventHandler(this.tmrProjectileSpawn_Tick);
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(335, 290);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(429, 22);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.Visible = false;
            // 
            // lblNumOfEnemies
            // 
            this.lblNumOfEnemies.AutoSize = true;
            this.lblNumOfEnemies.BackColor = System.Drawing.Color.Transparent;
            this.lblNumOfEnemies.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNumOfEnemies.Location = new System.Drawing.Point(193, 239);
            this.lblNumOfEnemies.Name = "lblNumOfEnemies";
            this.lblNumOfEnemies.Size = new System.Drawing.Size(139, 34);
            this.lblNumOfEnemies.TabIndex = 4;
            this.lblNumOfEnemies.Text = "Number Of Enemies:\r\n(3-5)\r\n";
            this.lblNumOfEnemies.Visible = false;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblUserName.Location = new System.Drawing.Point(283, 293);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(49, 17);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "Name:";
            this.lblUserName.Visible = false;
            // 
            // lblUserError
            // 
            this.lblUserError.AutoSize = true;
            this.lblUserError.BackColor = System.Drawing.Color.Transparent;
            this.lblUserError.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserError.Location = new System.Drawing.Point(331, 198);
            this.lblUserError.Name = "lblUserError";
            this.lblUserError.Size = new System.Drawing.Size(140, 29);
            this.lblUserError.TabIndex = 6;
            this.lblUserError.Text = "Invalid Input";
            this.lblUserError.Visible = false;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.BackColor = System.Drawing.Color.Transparent;
            this.lblEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnd.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblEnd.Location = new System.Drawing.Point(487, 46);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(97, 29);
            this.lblEnd.TabIndex = 7;
            this.lblEnd.Text = "end text";
            this.lblEnd.Visible = false;
            // 
            // SideScroller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 484);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblUserError);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblNumOfEnemies);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtEnemy);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1198, 531);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1198, 531);
            this.Name = "SideScroller";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SideScroller";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SideScroller_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SideScroller_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrProjectile;
        private System.Windows.Forms.TextBox txtEnemy;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrMovements;
        private System.Windows.Forms.Timer tmrProjectileSpawn;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblNumOfEnemies;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserError;
        private System.Windows.Forms.Label lblEnd;
    }
}

