﻿/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 4/30/2013
 * Time: 1:57 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace tcplistener
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            // TODO: Implement Functionality Here
            
            //
            //
            System.IO.StreamWriter writer = null;
            //
            //
            
            TcpListener server=null;   
            try
            {
              // Set the TcpListener on port 13000.
              //Int32 port = 13000;
              Int32 port = 4001;
              IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        
              // TcpListener server = new TcpListener(port);
              server = new TcpListener(localAddr, port);
              
              server.ExclusiveAddressUse = false;
              
              //
              //
              writer =
                  new StreamWriter(@"C:\Projects\probe\tcplistener\tcplistener\bin\Debug\receiver.log");
              //
              //
        
              // Start listening for client requests.
              server.Start();
        
              // Buffer for reading data
              Byte[] bytes = new Byte[256];
              String data = null;
        
              // Enter the listening loop. 
              while(true) 
              {
                Console.Write("Waiting for a connection... ");
        
                // Perform a blocking call to accept requests. 
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();            
                Console.WriteLine("Connected!");
        
                data = null;
        
                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
        
                int i;
        
                // Loop to receive all the data sent by the client. 
                while((i = stream.Read(bytes, 0, bytes.Length))!=0) 
                {   
                  // Translate data bytes to a ASCII string.
                  data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                  Console.WriteLine("Received: {0}", data);
                  try {
                  writer.WriteLine("Received: {0}", data);
                  }
                  catch (Exception ee) {
                      Console.WriteLine(ee.Message);
                  }
        
                  // Process the data sent by the client.
                  data = data.ToUpper();
        
                  byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
        
                  // Send back a response.
                  stream.Write(msg, 0, msg.Length);
                  Console.WriteLine("Sent: {0}", data);            
                }
        
                // Shutdown and end connection
                client.Close();
              }
            }
            catch(SocketException e)
            {
              Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
               // Stop listening for new clients.
               server.Stop();
               
               //
               //
               writer.Close();
               //
               //
            }
            
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}