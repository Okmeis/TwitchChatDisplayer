using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace TwitchChatDisplayer {
    class EmoticonFactory {

        private Dictionary<String, Image> downloadedEmotes;

        public EmoticonFactory() {
            this.downloadedEmotes = new Dictionary<string, Image>();
        }

        private void  downloadAndPrepareEmote(EmoticonData emoticonData) {
            String urlSnippet = emoticonData.getUrlSnippet();
            if ( downloadedEmotes.ContainsKey(urlSnippet) ) {
                return;
            }
            WebClient client = new WebClient();
            String emoteAddress = emoticonData.getUrl();
            byte[] imageData = client.DownloadData(emoteAddress);
            Image emoticon = Image.FromStream(new MemoryStream(imageData), true);
            downloadedEmotes.Add(urlSnippet, emoticon);
        }

        public void downloadAndSaveEmotes(MessageSet content) {
            foreach (int startIndex in content.getEmoticonDataDict().Keys.OrderByDescending(key => key)) {
                EmoticonData emoticonData = content.getEmoticonDataDict()[startIndex];
                downloadAndPrepareEmote(emoticonData);
            }
        }

        public void InsertIntoChat(RichTextBox chatDisplay, MessageSet content, int prevlength) {
            foreach (int startIndex in content.getEmoticonDataDict().Keys.OrderByDescending(key => key)) {
                EmoticonData emoticonData = content.getEmoticonDataDict()[startIndex];

                Image emoticon = downloadedEmotes[emoticonData.getUrlSnippet()];
                
                chatDisplay.Select(startIndex + prevlength + 3 + content.getName().Length, emoticonData.getEnd() - startIndex + 1);
                IDataObject dataTemp = Clipboard.GetDataObject();
                chatDisplay.ReadOnly = false;
                Clipboard.SetImage(emoticon);
                chatDisplay.Paste();
                chatDisplay.ReadOnly = true;
                Clipboard.SetDataObject(dataTemp);
            }
        }

    }
}
