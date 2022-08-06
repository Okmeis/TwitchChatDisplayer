using System;
using System.Net.Sockets;
using System.IO;

namespace TwitchChatDisplayer {

    class IrcClient {
        private String username;
        private String channel;
        //Var. Dec.
        private TcpClient tcpClient;
        private StreamReader input;
        private StreamWriter output;

        //Constructor
        public IrcClient(string ip, int port, string userName, string password) {
            this.username = userName;
            tcpClient = new TcpClient(ip, port);
            input = new StreamReader(tcpClient.GetStream());
            output = new StreamWriter(tcpClient.GetStream());

            output.WriteLine("PASS " + password);
            output.WriteLine("NICK " + userName);
            output.WriteLine("USER " + userName + " 8 * :" + userName);
            output.Flush();
        }

        public void joinRoom(string channel) {
            this.channel = channel;
            output.WriteLine("JOIN #" + channel);
            output.Flush();
        }

        public void leaveRoom(string channel) {
            this.channel = null;
            output.WriteLine("PART #" + channel);
            output.Flush();

        }

        //Only necessary for pinging in case of TwitchChatDisplayer, if needed for another chat bot: override, else you risk connection "issues=R.I.P."
        public void sendIrcMessage(String message) {
            output.WriteLine(message);
            try {
                output.Flush();
            }
            catch (Exception e) {
            }
            output.Flush();
        }


        public void sendChatMessage(String message) {
            sendIrcMessage(":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
            output.Flush();
        }

        //> :tmi.twitch.tv USERSTATE #<channel>
        public void askUserStates(String chan) {
            sendIrcMessage("CAP REQ :twitch.tv/tags");
            output.Flush();
        }
        public String readMessage() {
            string message;
            message = input.ReadLine();
            return message;
        }

    }
}
