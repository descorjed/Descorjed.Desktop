using Descorjed.Client.Http.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Descorjed.Client.Http
{
    class ClientDataContext
    {
        public DescorjedClient Client { get; set; }

        public List<Server> Servers { get; set; }
        public ClientDataContext()
        {
            this.Client = new DescorjedClient();
            this.init();
            

        }

        public async void init()
        {
            this.Servers = await this.Client.GetServers();


        }
    }
}
