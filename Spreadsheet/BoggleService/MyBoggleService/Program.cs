using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    class Program
    {
        static void Main(string[] args)
        {
            //why???
            //todo: ask about below, fix the dictionary, ask about 6.4, and the boggleGame file


            HttpStatusCode status;
            UserInfo name = new UserInfo { Nickname = "Joe" };
            BoggleService service = new BoggleService();
            UserToke user = service.Register(name, out status);
            Console.WriteLine(user.UserToken);
            Console.WriteLine(status.ToString());

            // This is our way of preventing the main thread from
            // exiting while the server is in use
            Console.ReadLine();
        }
    }
}
