using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreadBot;

namespace GatewayManager
{
    class GatewayManager
    {
        public static void Action(Gateway gateway, User user, Task task)
        {
            switch (task)
            {
                case Task.WelcomeMsg:
                    {


                        Methods.sendMessage(gateway.id, gateway.WelcomeMsg, "Markdown", )
                        return;
                    }


            }
        }
    }
}
