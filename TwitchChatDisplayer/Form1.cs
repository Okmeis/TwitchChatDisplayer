using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TwitchChatDisplayer {
    public partial class TwitchChatDisplay : Form {

        IrcClient irc;
        
        String message;

        private Point preLocation;
        private Boolean holding;


        public TwitchChatDisplay() {
            InitializeComponent();
            this.TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void startChat() {
            Random rnd = new Random();
            // 3 times Next(10) to always get 3 digits
            int suffixA = rnd.Next(10);
            int suffixB = rnd.Next(10);
            int suffixC = rnd.Next(10);
            irc = new IrcClient("irc.twitch.tv", 6667, "justinfan" + suffixA + suffixB + suffixC, "randomUwUStringerino");
            irc.joinRoom(tbChannelname.Text.ToLower());
        }

        private void switchToChat() {
            // Hide title bar and switch buttons
            btnClose.Visible = true;
            btnClose.TabStop = false;
            btnMove.Visible = true;
            btnMove.TabStop = false;
            lbCredits.Visible = false;
            // Switch from input to chat view
            lbChannelname.Enabled = false;
            lbChannelname.Visible = false;
            tbChannelname.Enabled = false;
            tbChannelname.Visible = false;
            chatDisplay.Enabled = true;
            chatDisplay.ReadOnly = true;
            chatDisplay.Visible = true;
            this.Height = 415;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Color transparentColor = ColorTranslator.FromHtml("#d3d3d3");
            this.BackColor = transparentColor;
            chatDisplay.BackColor = transparentColor;
            this.TransparencyKey = transparentColor;
        }

        private void tbChannelname_KeyUp(object sender, KeyEventArgs e) {
            // If textbox is empty Then show label
            if (tbChannelname.Text.Length > 0)
                lbChannelname.Visible = false;
            else
                lbChannelname.Visible = true;
            // If user presses Enter in textbox Then try connecting to chat
            if (e.KeyCode == Keys.Enter) {
                tbChannelname.Text = tbChannelname.Text.ToLower();
                try {
                    startChat();
                    lbConnectError.Visible = false;
                }
                catch {
                    lbConnectError.Visible = true;
                    return;
                }
                switchToChat();
                irc.askUserStates(tbChannelname.Text);
                chatDisplay.ReadOnly = false;
                chatDisplay.AppendText(irc.readMessage());
                chatDisplay.ReadOnly = true;
                backgroundWorker.RunWorkerAsync();
            }
        }

        // Chat movement functions
        private void btnMove_MouseDown(object sender, MouseEventArgs e) {
            holding = true;
            preLocation = e.Location;
        }

        private void btnMove_MouseMove(object sender, MouseEventArgs e) {
            if (holding) {
                Location = new Point((Location.X - preLocation.X) + e.X, (Location.Y - preLocation.Y) + e.Y);
                Update();
            }
        }

        private void btnMove_MouseUp(object sender, MouseEventArgs e) {
           holding = false;
        }

        // BackgroundWorker for irc communication and chat message management
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            try {
                message = irc.readMessage();
            }
            catch (Exception ex) {
                irc.leaveRoom(tbChannelname.Text);
                startChat();
            }
        }

        private void AddText(RichTextBox rtb, string txt, Color col) {
            int pos = rtb.TextLength;
            rtb.AppendText(txt);
            rtb.Select(pos, txt.Length);
            rtb.SelectionColor = col;
            rtb.Select();
        }

        private void AddMessage(MessageSet ms) {
            AddText(chatDisplay, ms.getName(), ms.getColor());
            AddText(chatDisplay, ": ", Color.White);
            AddText(chatDisplay, ms.getMsg() + "\n", Color.White);
            chatDisplay.ScrollToCaret();
            chatDisplay.DeselectAll();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            int linesLimit = 14;
            
            this.BringToFront();
            if (message.Equals(null)) {
                message = "";
            }
            else {
                if (message == "PING :tmi.twitch.tv") {
                    irc.sendIrcMessage("PONG :tmi.twitch.tv");
                }
                else if (message.StartsWith("@badge-info=") && message.Contains(".tmi.twitch.tv PRIVMSG #")) {
                    try {
                        MessageSet content = new MessageSet(message, tbChannelname.Text);

                        if (chatDisplay.Lines.Length > linesLimit) {
                            chatDisplay.ReadOnly = false;
                            chatDisplay.Select(0, chatDisplay.GetFirstCharIndexFromLine(chatDisplay.Lines.Length - linesLimit));
                            chatDisplay.SelectedText = "";
                            chatDisplay.ReadOnly = true;
                        }
                        EmoticonFactory emoticonFactory = new EmoticonFactory();
                        int prevlength = chatDisplay.Text.Length;
                        emoticonFactory.downloadAndSaveEmotes(content);
                        AddMessage(content);
                        emoticonFactory.InsertIntoChat(chatDisplay, content, prevlength);
                    }
                    catch (Exception ex) {
                        //Use "ex" for an error logo popup which does not interrupt the program.
                    }
                }
            }
            backgroundWorker.RunWorkerAsync();
        }

        private void closeButton_Click(object sender, EventArgs e) {
            backgroundWorker.Dispose();
            irc.leaveRoom(tbChannelname.Text);
            this.Close();
        }

        private void lbChannelname_MouseClick(object sender, MouseEventArgs e) {
            tbChannelname.Select();
        }

    }
}
