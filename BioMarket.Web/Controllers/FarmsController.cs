namespace BioMarket.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using BioMarket.Data;
    using BioMarket.Web.Models;
    
    public class FarmsController : ApiController
    {
        private readonly IBioMarketData data;

        public FarmsController() : this(new BioMarketData())
        {
        }

        public FarmsController(IBioMarketData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var farms = this.data
            .Farms
                            .All()
                            .Where(a => a.Deleted == false)
                            .Select(FarmModel.FromFarm);

            return this.Ok(farms);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var farm = this.data
            .Farms
                           .All()
                           .Where(a => a.Id == id && a.Deleted == false)
                           .Select(FarmModel.FromFarm)
                           .FirstOrDefault();

            if (farm == null)
            {
                return this.BadRequest("Farm does not exist - invalid id");
            }

            return this.Ok(farm);
        }

        [HttpGet]
        public IHttpActionResult ByName(string id)
        {
            var farm = this.data
            .Farms
                           .All()
                           .Where(a => a.Account == id && a.Deleted == false)
                           .Select(FarmModel.FromFarm)
                           .FirstOrDefault();

            if (farm == null)
            {
                return this.BadRequest("Farm does not exist - invalid name");
            }

            return this.Ok(farm);
        }

        [HttpPut]
        public IHttpActionResult Update(string name, FarmModel farm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var isFarmer = this.User.IsInRole("Farmer");

            if (!isFarmer)
            {
                return this.BadRequest("You are not farmer!");
            }

            var userName = this.User.Identity.Name;

            var existingFarm = this.data
            .Farms
                                   .All()
                                   .Where(a => a.Account == name && a.Deleted == false)
                                   .FirstOrDefault();

            if (existingFarm == null)
            {
                return this.BadRequest("Such farm does not exists or you have no authority to edit it!");
            }

            existingFarm.Name = farm.Name;
            existingFarm.Address = farm.Address;

            if (farm.Phones != null)
            {
                existingFarm.Phones = farm.Phones;
            }

            if (farm.Owner != null)
            {
                existingFarm.Owner = farm.Owner;
            }

            if (farm.Latitude != null)
            {
                existingFarm.Latitude = farm.Latitude;
            }

            if (farm.Longitude != null)
            {
                existingFarm.Longitude = farm.Longitude;
            }

            this.data.SaveChanges();

            farm.Id = existingFarm.Id;

            var newFarm = new
            {
                Id = farm.Id,
                Name = farm.Name,
                Address = farm.Address,
                Owner = farm.Owner,
                Phones = farm.Phones,
                Latitude = farm.Latitude,
                Longitude = farm.Longitude
            };

            return this.Ok(newFarm);
        }

        [HttpPut]
        public IHttpActionResult Delete(int id)
        {
            var isFarmer = this.User.IsInRole("Farmer");

            if (!isFarmer)
            {
                return this.BadRequest("You are not farmer!");
            }

            var userName = this.User.Identity.Name;

            var existingFarm = this.data.Farms
                                   .All()
                                   .Where(a => a.Id == id && a.Deleted == false && a.Account == userName)
                                   .FirstOrDefault();

            if (existingFarm == null)
            {
                return this.BadRequest("Such farm does not exists or you have no authority to edit it!");
            }

            existingFarm.Deleted = true;

            this.data.SaveChanges();

            return this.Ok();
        }
    }
}