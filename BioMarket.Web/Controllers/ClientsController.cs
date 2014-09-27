namespace BioMarket.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using BioMarket.Data;
    using BioMarket.Web.Models;

    public class ClientsController : ApiController
    {
        private readonly IBioMarketData data;

        public ClientsController()
            : this(new BioMarketData())
        {
        }

        public ClientsController(IBioMarketData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var clients = this.data
            .Clients
                              .All()
                              .Where(a => a.Deleted == false)
                              .Select(ClientModel.FromClient);

            return this.Ok(clients);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var client = this.data
            .Clients
                             .All()
                             .Where(a => a.Id == id && a.Deleted == false)
                             .Select(ClientModel.FromClient)
                             .FirstOrDefault();

            if (client == null)
            {
                return this.BadRequest("Client does not exist - invalid id");
            }

            return this.Ok(client);
        }

        [HttpGet]
        public IHttpActionResult ByAccount(string id)
        {
            var client = this.data
            .Clients
                             .All()
                             .Where(a => a.Account == id && a.Deleted == false)
                             .Select(ClientModel.FromClient)
                             .FirstOrDefault();

            if (client == null)
            {
                return this.BadRequest("Client does not exist - invalid account name");
            }

            return this.Ok(client);
        }

        [HttpPut]
        public IHttpActionResult Update(string name, ClientModel client)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Invalid data");
            }

            var isClient = this.User.IsInRole("Client");

            if (!isClient)
            {
                return this.BadRequest("You are not client!");
            }

            var userName = this.User.Identity.Name;

            var existingClient = this.data
            .Clients
                                    .All()
                                    .Where(a => a.Account == name && a.Deleted == false)
                                    .FirstOrDefault();

            if (existingClient == null)
            {
                return this.BadRequest("Such client does not exists or you have no authority to edit it!");
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Phone = client.Phone;

            this.data.SaveChanges();

            client.Id = existingClient.Id;

            var newClient = new
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone
            };

            return this.Ok(newClient);
        }

        [HttpPut]
        public IHttpActionResult Delete(string name)
        {
            var isFarmer = this.User.IsInRole("Client");

            if (!isFarmer)
            {
                return this.BadRequest("You are not client!");
            }

            var userName = this.User.Identity.Name;

            var existingClient = this.data
            .Clients
                                    .All()
                                    .Where(a => a.Account == name && a.Deleted == false)
                                    .FirstOrDefault();

            if (existingClient == null)
            {
                return this.BadRequest("Such client does not exists or you have no authority to edit it!");
            }

            this.data.Clients.Delete(existingClient);

            var existingAccount = this.data.Accounts.All().Where(a => a.UserName == name)
                                    .FirstOrDefault();

            this.data.Accounts.Delete(existingAccount);

            this.data.SaveChanges();

            return this.Ok();
        }
    }
}