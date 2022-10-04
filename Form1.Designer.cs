
namespace NowPlaying
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.nowPlayingLabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fMLaPazToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirdRockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.nowArtwork = new System.Windows.Forms.PictureBox();
            this.nowArtist = new System.Windows.Forms.Label();
            this.nowPlayingAlbum = new System.Windows.Forms.Label();
            this.nowTime = new System.Windows.Forms.Label();
            this.playBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nowArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // nowPlayingLabel
            // 
            this.nowPlayingLabel.AutoSize = true;
            this.nowPlayingLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nowPlayingLabel.Location = new System.Drawing.Point(240, 19);
            this.nowPlayingLabel.Name = "nowPlayingLabel";
            this.nowPlayingLabel.Size = new System.Drawing.Size(172, 48);
            this.nowPlayingLabel.TabIndex = 0;
            this.nowPlayingLabel.Text = "Now Title";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.stationToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 180);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(182, 44);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 44);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(182, 44);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // stationToolStripMenuItem
            // 
            this.stationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fMLaPazToolStripMenuItem,
            this.thirdRockToolStripMenuItem});
            this.stationToolStripMenuItem.Name = "stationToolStripMenuItem";
            this.stationToolStripMenuItem.Size = new System.Drawing.Size(182, 44);
            this.stationToolStripMenuItem.Text = "Station";
            // 
            // fMLaPazToolStripMenuItem
            // 
            this.fMLaPazToolStripMenuItem.Name = "fMLaPazToolStripMenuItem";
            this.fMLaPazToolStripMenuItem.Size = new System.Drawing.Size(291, 48);
            this.fMLaPazToolStripMenuItem.Text = "FM La Paz";
            this.fMLaPazToolStripMenuItem.Click += new System.EventHandler(this.fMLaPazToolStripMenuItem_Click);
            // 
            // thirdRockToolStripMenuItem
            // 
            this.thirdRockToolStripMenuItem.Name = "thirdRockToolStripMenuItem";
            this.thirdRockToolStripMenuItem.Size = new System.Drawing.Size(291, 48);
            this.thirdRockToolStripMenuItem.Text = "Third Rock";
            this.thirdRockToolStripMenuItem.Click += new System.EventHandler(this.thirdRockToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Now Playing";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // nowArtwork
            // 
            this.nowArtwork.Location = new System.Drawing.Point(0, 0);
            this.nowArtwork.Margin = new System.Windows.Forms.Padding(4);
            this.nowArtwork.Name = "nowArtwork";
            this.nowArtwork.Size = new System.Drawing.Size(175, 175);
            this.nowArtwork.TabIndex = 1;
            this.nowArtwork.TabStop = false;
            // 
            // nowArtist
            // 
            this.nowArtist.AutoSize = true;
            this.nowArtist.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nowArtist.Location = new System.Drawing.Point(240, 100);
            this.nowArtist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nowArtist.Name = "nowArtist";
            this.nowArtist.Size = new System.Drawing.Size(157, 41);
            this.nowArtist.TabIndex = 2;
            this.nowArtist.Text = "Now Artist";
            // 
            // nowPlayingAlbum
            // 
            this.nowPlayingAlbum.AutoSize = true;
            this.nowPlayingAlbum.Location = new System.Drawing.Point(380, 170);
            this.nowPlayingAlbum.Name = "nowPlayingAlbum";
            this.nowPlayingAlbum.Size = new System.Drawing.Size(90, 37);
            this.nowPlayingAlbum.TabIndex = 3;
            this.nowPlayingAlbum.Text = "label1";
            // 
            // nowTime
            // 
            this.nowTime.AutoSize = true;
            this.nowTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nowTime.Location = new System.Drawing.Point(240, 170);
            this.nowTime.Name = "nowTime";
            this.nowTime.Size = new System.Drawing.Size(97, 41);
            this.nowTime.TabIndex = 4;
            this.nowTime.Text = "label1";
            // 
            // playBtn
            // 
            this.playBtn.Location = new System.Drawing.Point(691, 177);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(56, 56);
            this.playBtn.TabIndex = 5;
            this.playBtn.Text = "P";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(786, 177);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(56, 56);
            this.stopBtn.TabIndex = 6;
            this.stopBtn.Text = "S";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 241);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.nowTime);
            this.Controls.Add(this.nowPlayingAlbum);
            this.Controls.Add(this.nowArtist);
            this.Controls.Add(this.nowArtwork);
            this.Controls.Add(this.nowPlayingLabel);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Now Playing on ThirdRock";
            this.Move += new System.EventHandler(this.Form1_Move);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nowArtwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nowPlayingLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.PictureBox nowArtwork;
        private System.Windows.Forms.Label nowArtist;
        private System.Windows.Forms.Label nowPlayingAlbum;
        private System.Windows.Forms.ToolStripMenuItem stationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fMLaPazToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirdRockToolStripMenuItem;
        private System.Windows.Forms.Label nowTime;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button stopBtn;
    }
}

