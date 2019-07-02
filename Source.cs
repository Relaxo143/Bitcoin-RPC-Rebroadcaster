using System;
using System.IO;
using System.Reflection;
using System.Threading;
using BitcoinLib.Services.Coins.Bitcoin;

namespace Bitcoin_Tx_Rebroadcaster
{
    class Program
    {
        static void Main()
        {
           
            int bufSize = 1048576; // increase max string size to allow the broadcasting of big transactions
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

            string rawTx = "";
            string connectionTest = "";
            string TXID = "";
            string IP = "";
            string rpcUser = "";
            string rpcPassword = "";
            int delayChoice = -1;
            int customDelay;
            bool isFirstTry = true;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bitcoin transaction rebroadcaster by Relaxo143");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("!!! Please note that exposing your node's RPC interface outside of your local network is dangerous, unsafe and not recommended."
               + "You should not use this program outside your local network where your bitcoin node is hosted !!!");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter the IP of your bitcoin node. If you are running this program on the same machine where the node is hosted you may just enter '1' w/o the apostrophes: ");
            IP = Console.ReadLine();

            if (IP == "1")
            {
                IP = "http://localhost:8332";
            }
            else
            {
                IP = "http://" + IP + ":8332";
            }

            Console.Write("Please enter the RPC username for your node: ");
            rpcUser = Console.ReadLine();
            Console.Write("Please enter the RPC password for your node: ");
            rpcPassword = Console.ReadLine();

            IBitcoinService btc = new BitcoinService(IP, rpcUser, rpcPassword, "", 15);
            try
            {
                connectionTest = btc.GetNetworkInfo().Subversion.ToString();
            }
            catch (BitcoinLib.ExceptionHandling.Rpc.RpcException)
            {
                Console.Clear();
                Console.WriteLine("There was an error with the connection. The provided credentials are probably wrong. Press any key to try again.");
                Console.ReadKey();
                Console.Clear();
                Main();
            }

        ConnectionEstablished:
            Console.Clear();
            Console.WriteLine("Successfully connected to node: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(connectionTest);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Now please enter the raw transaction you wish to keep broadcasting in hex: ");
            rawTx = Console.ReadLine();

            try
            {
                Console.Clear();
                Console.WriteLine("Your transaction was successfully decoded. TXID: " + btc.DecodeRawTransaction(rawTx).TxId);
            }
            catch (BitcoinLib.ExceptionHandling.Rpc.RpcInternalServerErrorException)
            {
                Console.Clear();
                Console.WriteLine("Error decoding transaction. Press any key to try again...");
                Console.ReadKey();
                Console.Clear();
                goto ConnectionEstablished;
            }

            while (!(delayChoice >= 0 && delayChoice <= 4))
            {
                if (isFirstTry == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The number must be between 0 and 4!");
                    Console.ForegroundColor = ConsoleColor.White;
                }

            DelayChoice:

                Console.WriteLine("Now please choose how frequently you would like to re-broadcast it. Type the preferred number and hit enter.");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("1 = every 30 mins");
                Console.WriteLine("2 = every 2 hours");
                Console.WriteLine("3 = every 6 hours (Recommended)");
                Console.WriteLine("4 = every 24 hours");
                Console.WriteLine("0 = Custom delay in SECONDS.");
                Console.ForegroundColor = ConsoleColor.White;
                try
                {
                    delayChoice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("That is not a number!");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto DelayChoice;
                }
                Console.Clear();
                isFirstTry = false;
            }

            switch (delayChoice)
            {
                case 0:
                    Console.Write("Enter your custom delay in SECONDS: ");
                    try
                    {
                        customDelay = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine("That is not a number!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        goto case 0;
                    }

                    Console.Clear();

                    while (true)
                    {          
                        TXID = btc.SendRawTransaction(rawTx);
                        DateTime currentTime = DateTime.Now;
                        Console.WriteLine("[" + currentTime + "] Transaction broadcasted successfully! TXID: " + TXID);
                        Thread.Sleep(customDelay * 1000); // custom
                    }
                    break;

                case 1:
                    Console.Clear();

                    while (true)
                    {
                       
                        TXID = btc.SendRawTransaction(rawTx);
                        DateTime currentTime = DateTime.Now;
                        Console.WriteLine("[" + currentTime + "] Transaction broadcasted successfully! TXID: " + TXID);
                        Thread.Sleep(30 * 60 * 1000); // 30 mins
                    }
                    break;

                case 2:

                    Console.Clear();

                    while (true)
                    {                     
                        TXID = btc.SendRawTransaction(rawTx);
                        DateTime currentTime = DateTime.Now;
                        Console.WriteLine("[" + currentTime + "] Transaction broadcasted successfully! TXID: " + TXID);
                        Thread.Sleep(2 * 60 * 60 * 1000); // 2h
                    }
                    break;

                case 3:

                    Console.Clear();

                    while (true)
                    {
                       
                        TXID = btc.SendRawTransaction(rawTx);
                        DateTime currentTime = DateTime.Now;
                        Console.WriteLine("[" + currentTime + "] Transaction broadcasted successfully! TXID: " + TXID);
                        Thread.Sleep(6 * 60 * 60 * 1000); // 6h
                    }
                    break;

                case 4:

                    Console.Clear();

                    while (true)
                    {
                        Console.Clear();
                        TXID = btc.SendRawTransaction(rawTx);
                        DateTime currentTime = DateTime.Now;
                        Console.WriteLine("[" + currentTime + "] Transaction broadcasted successfully! TXID: " + TXID);
                        Thread.Sleep(24 * 60 * 60 * 1000); // 24h
                    }
                    break;
            }
        }
    }
}
  

