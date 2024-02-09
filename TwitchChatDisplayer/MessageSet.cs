using System;
using System.Collections.Generic;
using System.Drawing;

namespace TwitchChatDisplayer {
    
    /* Saves the message information as an object given by twitch by filtering them into
     * instance variables and offers getters for relevant information. */
    class MessageSet {

        private Color color;
        private string username, message;
        private Dictionary<int, EmoticonData> emoticonDataDict;

        public MessageSet(string messageInformation, string channelName) {
            string[] pieces = filterForMessageAttribute(messageInformation, ".tmi.twitch.tv PRIVMSG #" + channelName + " :");
            string colorInfo = filterForMessageAttribute(pieces[0], ";color=")[1];
            string[] emoticonsInfo = filterForMessageAttribute(filterForMessageAttribute(pieces[0], ";emotes=")[1], ";")[0].Split('/');

            Dictionary<int, EmoticonData> emoticonData = new Dictionary<int, EmoticonData>();

            foreach (String emoticonInfo in emoticonsInfo) {
                string[] infos = emoticonInfo.Split(new string[] { ":" }, 2, StringSplitOptions.None);
                if (infos.Length < 2) {
                    continue;
                }
                string[] intervals = infos[1].Split(',');
                String urlSnippet = infos[0];
                foreach (String interval in intervals) {
                    string[] intervalData = filterForMessageAttribute(interval, "-");
                    int start, end;
                    if (Int32.TryParse(intervalData[0], out start) && Int32.TryParse(intervalData[1], out end)) {
                        emoticonData.Add(start, new EmoticonData(end, urlSnippet));
                    }
                }
            }

            this.color            = assignColor(colorInfo);
            this.username         = filterName(pieces[0]);
            this.message          = pieces[1];
            this.emoticonDataDict = emoticonData;
        }

        private string[] filterForMessageAttribute(string messageInformation, string byAttribute) {
            string[] mAttr = messageInformation.Split(new string[] { byAttribute }, 2, StringSplitOptions.None);
            if (mAttr.Length != 2)  {
                throw new InvalidOperationException("Unexcepted format for messageInformation! The lenght of messageAttribute " + byAttribute + " is" + mAttr.Length);
            }
            return mAttr;
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

        private int getLastIndexOfString(String text, char c) {
            for (int i = 1; i < text.Length - 1; i++) {
                if (text[text.Length - i - 1] == c) {
                    return text.Length - i;
                }
            }
            return -1;
        }

        private Color assignColor(String data) {
            Color c = new Color();
            if (data[0] == ';') {
                Color[] Colorlist = {Color.Blue, Color.BlueViolet, Color.CadetBlue, Color.Chocolate, Color.Coral,
                                     Color.DodgerBlue, Color.Firebrick, Color.Goldenrod, Color.Green, Color.HotPink,
                                     Color.OrangeRed, Color.Red, Color.SeaGreen, Color.SpringGreen, Color.YellowGreen};
                c = Colorlist[new Random().Next(0, Colorlist.Length)];
                return c;
            }
            return ColorTranslator.FromHtml(data.Substring(0, 7));
        }

        public Color getColor() { return color; }
        public string getName() { return username; }
        public string getMsg() { return message; }

        public Dictionary<int, EmoticonData> getEmoticonDataDict() {
            return emoticonDataDict;
        }

    }
}
