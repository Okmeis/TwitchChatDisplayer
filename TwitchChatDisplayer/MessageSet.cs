using System;
using System.Drawing;

namespace TwitchChatDisplayer {
    class MessageSet {

        private Color color;
        private String username, message;

        public MessageSet(Color color, String username, String message) {
            this.color = color;
            this.username = username;
            this.message = message;
        }

        public Color getColor() { return color; }
        public String getName() { return username; }
        public String getMsg() { return message; }



    }
}
