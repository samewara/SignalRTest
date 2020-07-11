﻿using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using System;

namespace SignalRServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://localhost:8080/";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server running at {url}");
                Console.WriteLine("Wait for clients message requests for server to respond OR " +
                    "Type any message - it will be broadcast to all clients.");

                // For server broadcast test
                // Get hub context 
                IHubContext ctx = GlobalHost.ConnectionManager.GetHubContext<TestHub>();

                string line = null;
                while ((line = System.Console.ReadLine()) != null)
                {
                    string newMessage = $"<Server sent:> {line}";
                    ctx.Clients.All.MessageFromServer(newMessage);
                }

                // pause to allow clients to receive
                Console.ReadLine();
            }
        }
    }
}