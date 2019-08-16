using System;
using DreadBot;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace GatewayManager
{
    public class EventHandler
    {
        public static void PlaceHoldermethod(object o, EventArgs e)
        {

        }

        public static void JoinEvent(object o, EventArgs e)
        {
            Console.WriteLine("Join event");
            JoinEventArgs joinEvent = e as JoinEventArgs;
            Message msg = null;

            if (joinEvent != null) { msg = joinEvent.msg; }
            else { Console.WriteLine("Shit broke."); return; }

            foreach (User user in msg.new_chat_members)
            {
                if (user.id == Configs.Me.id)
                {
                    InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup();
                    keyboard.SetRowCount(1);
                    keyboard.addButton(new InlineKeyboardButton() { text = "Configure", callback_data = "groupcfg" }, 0);

                    Methods.sendMessage(msg.chat.id, "Hello!\n\nI am Gateway Manager. I help automate the process of screening new users before they are permited into your group(s).\n\nTo configure me click the \"Configure\" button below.", "markdown", keyboard);
                    continue;
                }

                lock (DatabaseHandler.Gateways)
                {
                    Gateway gateway;
                    if (DatabaseHandler.Gateways.ContainsKey(msg.chat.id))
                    {
                        gateway = DatabaseHandler.Gateways[msg.chat.id];
                    }
                    else if (DatabaseHandler.MainChats.ContainsKey(msg.chat.id))
                    {
                        long MainChatId = DatabaseHandler.MainChats[msg.chat.id];
                        gateway = DatabaseHandler.Gateways[MainChatId];

                        if (gateway.AutoBanNoAuth)
                        {

                        }

                    }
                    else { return; }

                    

                    
                }

            }


        }

        public static void TextEvent(object o, EventArgs e)
        {
            Console.WriteLine("Text event");
            TextEventArgs textEvent = e as TextEventArgs;

            Message msg = null;
            if (textEvent != null) { msg = textEvent.msg; }
            else { Console.WriteLine("Shit broke."); return; }

            switch (Utilities.isCommand(msg.text))
            {
                case "captcha":
                    {

                        Captcha cap = new Captcha(224, 100, 12, 8);
                        StreamContent sc = new StreamContent(cap.GenerateImage());
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup();
                        keyboard.SetRowCount(1);
                        keyboard.addButton(new InlineKeyboardButton() { text = "Reload Image (2 Reloads)", callback_data = "dreadbot tuneables" }, 0);

                        Methods.sendPhotoFile(msg.chat.id, sc, "captcha", cap.GetCaptchaCode(), keyboard: keyboard);
                        break;
                    }
                default: return;
            }


        }
    }
}
