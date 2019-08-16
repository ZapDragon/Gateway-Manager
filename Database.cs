using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using DreadBot;

namespace GatewayManager
{
    class DatabaseHandler
    {
        public static Dictionary<long, Gateway> Gateways = new Dictionary<long, Gateway>();
        public static Dictionary<long, long> MainChats = new Dictionary<long, long>(); //<MainChatID, GatewayID>
        //public static Dictionary<long, long> UserWatch = new Dictionary<long, long>(); //<GatewayID, UserID>

        public static LiteCollection<Gateway> col = DreadBot.Database.db.GetCollection<Gateway>("gateways");

        public static void Init()
        {
            Gateway[] gateways = col.FindAll().ToArray();

            foreach (Gateway gateway in gateways)
            {
                Gateways.Add(gateway.id, gateway);
            }
        }



        public static void DatabaseEvent(object o, EventArgs e)
        {
            Dictionary<long, Gateway> TempGateways;
            lock (Gateways)
            {
                TempGateways = Gateways;
            }

            lock (col)
            {

                foreach (Gateway gateway in TempGateways.Values)
                {
                    col.Update(gateway);
                }
            }

        }
    }

    public class Gateway : Chat
    {

        public Dictionary<long, Chat> MainChats = new Dictionary<long, Chat>();

        public Dictionary<long, GatewayUsers> Users = new Dictionary<long, GatewayUsers>();

        public bool AutoBanNoAuth { get; set; } = false;

        public bool Captcha { get; set; } = false;

        public bool AgeAgreement { get; set; } = false;

        public string WelcomeMsg { get; set; }

        public string Instructions { get; set; }


    }

    public class GatewayUsers
    {
        long id { get; set; }

        long gatewayID { get; set; }

        int joinTime { get; set; }

        bool hasUsername { get; set; } = false;

        bool hasIcon { get; set; } = false;

        bool AgeConfirmed { get; set; } = false;

        bool PassedCapctha { get; set; } = false;

        bool RulesAgreement { get; set; } = false;

        bool InvitePending { get; set; } = false;




    }

    public enum Task : int
    {
        WelcomeMsg = 0,
        Instructions = 1,
        Capctha = 2,
        GroupSelection = 3,
        AgeAgreement = 4,
        RulesAgreement = 5,
        Invite = 6
    }
}
