namespace TwitchChatDisplayer
{
    partial class TwitchChatDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up used resources.
        /// </summary>
        /// <param name="disposing">True if managed resources are to be deleted; otherwise, False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Required method for designer support.
        /// The content of the method must not be changed using the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwitchChatDisplay));
            this.tbChannelname = new System.Windows.Forms.TextBox();
            this.lbChannelname = new System.Windows.Forms.Label();
            this.chatDisplay = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbCredits = new System.Windows.Forms.Label();
            this.lbConnectError = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbChannelname
            // 
            this.tbChannelname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.tbChannelname.Location = new System.Drawing.Point(26, 25);
            this.tbChannelname.Name = "tbChannelname";
            this.tbChannelname.Size = new System.Drawing.Size(284, 30);
            this.tbChannelname.TabIndex = 1;
            this.tbChannelname.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbChannelname_KeyUp);
            // 
            // lbChannelname
            // 
            this.lbChannelname.AutoSize = true;
            this.lbChannelname.BackColor = System.Drawing.SystemColors.Window;
            this.lbChannelname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbChannelname.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbChannelname.Location = new System.Drawing.Point(30, 27);
            this.lbChannelname.Name = "lbChannelname";
            this.lbChannelname.Size = new System.Drawing.Size(194, 25);
            this.lbChannelname.TabIndex = 2;
            this.lbChannelname.Text = "Enter Channel Name";
            this.lbChannelname.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbChannelname_MouseClick);
            // 
            // chatDisplay
            // 
            this.chatDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.chatDisplay.Enabled = false;
            this.chatDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chatDisplay.Location = new System.Drawing.Point(-1, -21);
            this.chatDisplay.Name = "chatDisplay";
            this.chatDisplay.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.chatDisplay.Size = new System.Drawing.Size(313, 414);
            this.chatDisplay.TabIndex = 3;
            this.chatDisplay.Text = "";
            this.chatDisplay.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkRed;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnClose.Location = new System.Drawing.Point(309, -1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(27, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "𐌗";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // lbCredits
            // 
            this.lbCredits.AutoSize = true;
            this.lbCredits.Location = new System.Drawing.Point(271, 369);
            this.lbCredits.Name = "lbCredits";
            this.lbCredits.Size = new System.Drawing.Size(56, 13);
            this.lbCredits.TabIndex = 5;
            this.lbCredits.Text = "by Okmeis";
            // 
            // lbConnectError
            // 
            this.lbConnectError.AutoSize = true;
            this.lbConnectError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConnectError.ForeColor = System.Drawing.Color.Red;
            this.lbConnectError.Location = new System.Drawing.Point(6, 3);
            this.lbConnectError.Name = "lbConnectError";
            this.lbConnectError.Size = new System.Drawing.Size(325, 20);
            this.lbConnectError.TabIndex = 6;
            this.lbConnectError.Text = "Please check your connection to twitch.";
            this.lbConnectError.Visible = false;
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.DarkGray;
            this.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.btnMove.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnMove.Location = new System.Drawing.Point(309, 29);
            this.btnMove.Margin = new System.Windows.Forms.Padding(0);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(27, 30);
            this.btnMove.TabIndex = 8;
            this.btnMove.Text = "📌";
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.Visible = false;
            this.btnMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseDown);
            this.btnMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseMove);
            this.btnMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMove_MouseUp);
            // 
            // TwitchChatDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TwitchChatDisplayer.Properties.Resources.tcdIcon;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(336, 391);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.lbConnectError);
            this.Controls.Add(this.lbCredits);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chatDisplay);
            this.Controls.Add(this.lbChannelname);
            this.Controls.Add(this.tbChannelname);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TwitchChatDisplay";
            this.Text = "TwitchChatDisplay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbChannelname;
        private System.Windows.Forms.Label lbChannelname;
        private System.Windows.Forms.RichTextBox chatDisplay;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbCredits;
        private System.Windows.Forms.Label lbConnectError;
        private System.Windows.Forms.Button btnMove;
    }
}

