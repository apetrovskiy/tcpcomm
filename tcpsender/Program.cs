/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 4/30/2013
 * Time: 1:59 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace tcpsender
{
    using System;
    using NLog;
    using NLog.Targets;
    using NLog.Config;
    
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            // TODO: Implement Functionality Here
            
            LoggingConfiguration config = new LoggingConfiguration();

            //FileTarget fileTarget = new FileTarget();
            NetworkTarget networkTarget = new NetworkTarget();
            //config.AddTarget("file", fileTarget);
            config.AddTarget("network", networkTarget);

            //fileTarget.FileName =
            //    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) +
            //    @"\TMX.log";
            networkTarget.Address = "tcp://127.0.0.1:4001";
            //fileTarget.Layout = "${date:format=HH\\:MM\\:ss}: ${message}";
            networkTarget.Layout = "${date:format=HH\\:MM\\:ss}: ${message}";

            //LoggingRule rule = new LoggingRule("*", LogLevel.Info, fileTarget);
            LoggingRule rule = new LoggingRule("*", LogLevel.Info, networkTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            Logger logger = LogManager.GetLogger("TMXN");
            
            while (true) {
                
                logger.Info("beep");
                System.Threading.Thread.Sleep(20);
                //System.Threading.Thread.Sleep(2000);
                //System.Threading.Thread.Sleep(200);
                //System.Threading.Thread.Sleep(20);
                
            }
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}