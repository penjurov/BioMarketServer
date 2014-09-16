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

        public ClientsController() : this(new BioMarketData())
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

        [Authorize]
        [HttpPut]
        public IHttpActionResult Update(int id, ClientModel client)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
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
                                     .Where(a => a.Id == id && a.Deleted == false && a.Account == userName)
                                     .FirstOrDefault();

            if (existingClient == null)
            {
                return this.BadRequest("Such client does not exists or you have no authority to edit it!");
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Phone = client.Phone;

            this.data.SaveChanges();

            client.Id = id;

            var newClient = new
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone
            };

            return this.Ok(newClient);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Delete(int id)
        {
            var isFarmer = this.User.IsInRole("Client");

            if (!isFarmer)
            {
                return this.BadRequest("You are not client!");
            }

            var userName = this.User.Identity.Name;

            var existingClient = this.data.Farms
                                     .All()
                                     .Where(a => a.Id == id && a.Deleted == false && a.Account == userName)
                                     .FirstOrDefault();

            if (existingClient == null)
            {
                return this.BadRequest("Such client does not exists or you have no authority to edit it!");
            }

            existingClient.Deleted = true;

            this.data.SaveChanges();

            return this.Ok();
        }
    }
}