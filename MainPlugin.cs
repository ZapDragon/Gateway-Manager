using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreadBot;

namespace GatewayManager
{
    public class MainPlugin : IDreadBotPlugin 
    {

        public string PluginID { get { return "GatewayManager"; } }

        public void Init()
        {
            Events.events.CallbackEvent += EventHandler.PlaceHoldermethod;
            Events.events.ForwardEvent += EventHandler.PlaceHoldermethod;
            Events.events.TextEvent += EventHandler.TextEvent;
            Events.events.JoinEvent += EventHandler.JoinEvent;
            Events.events.PartEvent += EventHandler.PlaceHoldermethod;
            Events.events.PassportDataEvent += EventHandler.PlaceHoldermethod;

            Cron.events.CronFireEvent += CronEvents.CronFire;
            Database.dbEvent.DatabaseFireEvent += DatabaseHandler.DatabaseEvent;

        }

    }
}
