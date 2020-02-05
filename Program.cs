using System;
using System.IO;

namespace MiniBillingServer
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "MiniBillingServer by Syinea V1.0";

            Console.WriteLine("Visit: https://facebook.com/syinea");
            Console.WriteLine("for more information and updates");
            Console.WriteLine("");
            Console.WriteLine("Visit: Official Coder ");
            Console.WriteLine("https://github.com/florian0");

            try
            {
                Http.HttpServer server = new Http.HttpServer();

                Model.BindingConfiguration bindcfg = new Model.BindingConfiguration("Settings/config.ini");

                Console.WriteLine("");
                Console.WriteLine("You should set billing server address to this one: ");
                Console.WriteLine("");
                Console.WriteLine("http://" + bindcfg.Address + ":" + bindcfg.Port + @"/");
                Console.WriteLine("");
                Console.WriteLine("Mini Billing Server modified by Syinea");
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------------------------------------");

                server.Prefixes.Add("http://" + bindcfg.Address + ":" + bindcfg.Port + "/");

                server.Handlers.Add(new Handlers.ServerStateHandler());
                server.Handlers.Add(new Handlers.SilkDataCallHandler());

                Model.SilkDB.Instance.Init();

                server.Start();

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("[Exception] {0}", ex.Message);
                Console.WriteLine(CheckConfigIssue());
                Console.Read();
                return -1;
            }

            Console.Read();

            return 0;
        }

        static string CheckConfigIssue()
        {
            if (!Directory.Exists("Settings"))
            {
                return "Settings folder doesn't exist, you should have it to make your config, please, download this tool again.";
            }
            else if (!File.Exists("Settings/config.ini"))
            {
                if (File.Exists("Settings/config.ini.dist"))
                {
                    return "You have to set your config in [Settings/config.ini.dist] and then rename it to [config.ini]";
                }
                else
                {
                    return "Couldn't find [Settings/config.ini.dist], you should have it to make your config, please, download this tool again or ask any Admins";
                }
            }
            else
            {
                return null;
            }
        }

    }
}
