using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TwitchChatDisplayer {
    public partial class TwitchChatDisplay : Form {

        IrcClient irc;
        
        String message;

        private Point preLocation;
        private Boolean holding;
        private int formerLocationX, formerLocationY;
        private List<MessageSet> lastMessages = new List<MessageSet>();


        public TwitchChatDisplay() {
            InitializeComponent();
            this.TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        

        private void tbChannelname_KeyUp(object sender, KeyEventArgs e) {
            if (tbChannelname.Text.Length > 0)
                lbChannelname.Visible = false;
            else
                lbChannelname.Visible = true;
            if (e.KeyCode == Keys.Enter) {
                tbChannelname.Text = tbChannelname.Text.ToLower();
                btnClose.Visible = true;
                btnClose.TabStop = false;
                lbCredits.Visible = false;
                switchToChat();
                startChat();
                irc.askUserStates(tbChannelname.Text);
                chatDisplay.ReadOnly = false;
                chatDisplay.AppendText(irc.readMessage());
                chatDisplay.ReadOnly = true;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void switchToChat() {
            lbChannelname.Enabled = false;
            lbChannelname.Visible = false;
            tbChannelname.Enabled = false;
            tbChannelname.Visible = false;
            chatDisplay.Enabled = true;
            chatDisplay.ReadOnly = true;
            chatDisplay.Visible = true;
            this.Width = 317;
            this.Height = 415;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = ColorTranslator.FromHtml("#768593");
            chatDisplay.BackColor = ColorTranslator.FromHtml("#768593");
            this.TransparencyKey = ColorTranslator.FromHtml("#768593");
        }

        private void startChat() {
            Random rnd = new Random();
            //3 times Next(10) to always get 3 digits
            int suffixA = rnd.Next(10);
            int suffixB = rnd.Next(10);
            int suffixC = rnd.Next(10);
            irc = new IrcClient("irc.twitch.tv", 6667, "justinfan"+ suffixA + suffixB + suffixC, "randomUwUStringerino");
            irc.joinRoom(tbChannelname.Text.ToLower());
        }

        private void chatDisplay_MouseDown(object sender, MouseEventArgs e) {
            formerLocationX = this.Location.X;
            formerLocationY = this.Location.Y;
            holding = true;
            preLocation = e.Location;
        }

        private void chatDisplay_MouseMove(object sender, MouseEventArgs e) {
            if (holding) {
                Location = new Point( (Location.X - preLocation.X) + e.X, (Location.Y - preLocation.Y) + e.Y);
                Update();
            }
        }

        private void chatDisplay_MouseUp(object sender, MouseEventArgs e) {
            holding = false;
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            try {
                message = irc.readMessage();
            }
            catch (Exception ex) {
                irc.leaveRoom(tbChannelname.Text);
                startChat();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.BringToFront();
            if (message.Equals(null)) {
                message = "";
            }
            else {
                if (message == "PING :tmi.twitch.tv") {
                    irc.sendIrcMessage("PONG :tmi.twitch.tv");
                }
                else if (message.StartsWith("@badge-info=") && message.Contains(".tmi.twitch.tv PRIVMSG #")) {
                    MessageSet content = getMsgContent(message);
                    if (content.getName().Length > 0 && chatDisplay.Lines.Length <=15) {
                        AddMessage(content);
                    }
                    lastMessages.Add(content);
                    if (chatDisplay.Lines.Length > 15) {
                        chatDisplay.SuspendLayout();
                        chatDisplay.Text = "";
                        foreach ( MessageSet lm in lastMessages){
                            AddMessage(lm);
                        }
                        chatDisplay.ResumeLayout();
                        lastMessages.RemoveAt(0);
                    }
                }
            }
            backgroundWorker1.RunWorkerAsync();
        }


        private void AddMessage(MessageSet ms) {
            AddText(chatDisplay, "\n" + ms.getName(), ms.getColor());
            AddText(chatDisplay, ": ", Color.White);
            AddText(chatDisplay, ms.getMsg(), Color.White);
            chatDisplay.ScrollToCaret();
            chatDisplay.DeselectAll();
        }


        private void AddText(RichTextBox rtb, string txt, Color col) {
            int pos = rtb.TextLength;
            rtb.AppendText(txt);
            rtb.Select(pos, txt.Length);
            rtb.SelectionColor = col;
            rtb.Select();
        }


        private MessageSet getMsgContent(String message) {

            String[] pieces = message.Split(new string[] { ".tmi.twitch.tv PRIVMSG #" + tbChannelname.Text + " :" }, 2, StringSplitOptions.None);
            String colorTemp = pieces[0].Split(new string[] { ";color=" }, 2, StringSplitOptions.None)[1];
            String name = filterName(pieces[0]);

            if (name == "") { new MessageSet(new Color(),"",""); }
            
            return new MessageSet(filterColor(colorTemp), name, pieces[1]);
        }

        private String filterName(String data) {
            String nameTmp = data.Split(new string[] { ";display-name=" }, 2, StringSplitOptions.None)[1];
            if (nameTmp[0] == ';') { 
                int namePos = getLastIndexOfString(data, '@');
                if (namePos == -1) {
                    return "";
                }
                return data.Substring(namePos);
            }
            return nameTmp.Split(new string[] { ";" }, 2, StringSplitOptions.None)[0];
        }

        private Color filterColor(String data) {
            Color c = new Color();
            if (data[0] == ';') {
                Color[] Colorlist = {Color.Blue, Color.BlueViolet, Color.CadetBlue, Color.Chocolate, Color.Coral,
                                     Color.DodgerBlue, Color.Firebrick, Color.Goldenrod, Color.Green, Color.HotPink,
                                     Color.OrangeRed, Color.Red, Color.SeaGreen, Color.SpringGreen, Color.YellowGreen};
                c = Colorlist[new Random().Next(0, Colorlist.Length)];
                return c;
            }
            return ColorTranslator.FromHtml(data.Substring(0,7));
        }

        private void closeButton_Click(object sender, EventArgs e) {
            backgroundWorker1.Dispose();
            irc.leaveRoom(tbChannelname.Text);
            this.Close();
        }

        private void lbChannelname_MouseClick(object sender, MouseEventArgs e) {
            tbChannelname.Select();
        }

        private int getLastIndexOfString(String text, char c) {
            for (int i = 1; i < text.Length-1; i++) {
                if (text[text.Length - i-1] == c) {
                    return text.Length - i;
                }
            }
             return -1;
        }

    }
}
