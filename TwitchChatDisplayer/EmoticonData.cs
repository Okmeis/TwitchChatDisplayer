using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatDisplayer {
    class EmoticonData {

        private int end;
        private String urlSnippet;

        public EmoticonData(int end, String urlSnippet) {
            this.end = end;
            this.urlSnippet = urlSnippet;
        }

        public int getEnd() {
            return end;
        }

        public String getUrl() {
            return "https://static-cdn.jtvnw.net/emoticons/v2/" + urlSnippet + "/default/dark/1.0";
        }

        public String getUrlSnippet() {
            return urlSnippet;
        }

    }
}
