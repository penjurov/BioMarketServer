namespace BioMarket.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using BioMarket.Data;
    using BioMarket.Web.Models;
    using BioMarket.Models;

    public class OffersController : ApiController
    {
        private readonly IBioMarketData data;

        public OffersController() : this(new BioMarketData())
        {
        }

        public OffersController(IBioMarketData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var offers = this.data
            .Offers
                             .All()
                             .Where(a => a.Deleted == false)
                             .Select(OfferModel.FromOffer);

            return this.Ok(offers);
        }

        [HttpGet]
        public IHttpActionResult AllDetails()
        {
            var offers = this.data
            .Offers
                             .All()
                             .Where(a => a.Deleted == false)
                             .Select(OfferModel.FromOfferWithProductAndBoughtBuy);

            return this.Ok(offers);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var offers = this.data
            .Offers
                             .All()
                             .Where(a => a.Id == id && a.Deleted == false)
                             .Select(OfferModel.FromOffer)
                             .FirstOrDefault();

            if (offers == null)
            {
                return this.BadRequest("Offer does not exist - invalid id");
            }

            return this.Ok(offers);
        }

        [HttpGet]
        public IHttpActionResult ByPostDate(DateTime id)
        {
            var offer = this.data
            .Offers
                            .All()
                            .Where(a => a.PostDate == id && a.Deleted == false)
                            .Select(OfferModel.FromOffer)
                            .FirstOrDefault();

            if (offer == null)
            {
                return this.BadRequest("Client does not exist - invalid account name");
            }

            return this.Ok(offer);
        }
        [Authorize]
        [HttpPost]
        public IHttpActionResult Add(OfferModel offer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var isFarmer= this.User.IsInRole("Farmer");

            if (!isFarmer)
            {
                return this.BadRequest("You are not farmer!");
            }

            var userName = this.User.Identity.Name;

            var farmer = this.data.Farms.All().Where(c => c.Account == userName).FirstOrDefault();


            var newOffer = new Offer
            {
                Quantity = offer.Quantity,
                ProductPhoto = offer.ProductPhoto,
                PostDate = offer.PostDate,
                ProductId = offer.Product.Id
            };

            this.data.Offers.Add(newOffer);
            this.data.SaveChanges();

            offer.Id = newOffer.Id;

            return this.Ok(newOffer);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Buy(int id, OfferModel offer)
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

            var client = this.data.Clients.All().Where(c => c.Account == userName).FirstOrDefault();

            var existingOffer = this.data
            .Offers
                                    .All()
                                    .Where(a => a.Id == id && a.Deleted == false && a.BoughtBy == null)
                                    .FirstOrDefault();

            if (existingOffer == null)
            {
                return this.BadRequest("Such offer does not exists or it's already bought!");
            }

            existingOffer.BoughtBy = client;
            existingOffer.BoughtDate = DateTime.Now;

            this.data.SaveChanges();

            offer.Id = id;

            var newOffer = new
            {
                Id = client.Id,
                Quantity = offer.Quantity,
                ProductPhoto = offer.ProductPhoto,
                BoughtBy = offer.BoughtBy,
                PostDate = offer.PostDate,
                BoughtDate = offer.BoughtDate,
                Product = offer.Product
            , };

            return this.Ok(newOffer);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Delete(int id)
        {
            var isFarmer = this.User.IsInRole("Farmer");

            if (!isFarmer)
            {
                return this.BadRequest("You are not farmer!");
            }

            var userName = this.User.Identity.Name;

            var existingOffer = this.data
            .Offers
                                    .All()
                                    .Where(a => a.Id == id && a.Deleted == false && a.Product.Farm.Account == userName)
                                    .FirstOrDefault();

            if (existingOffer == null)
            {
                return this.BadRequest("Such offer does not exists or you have no authority to delete it!");
            }

            existingOffer.Deleted = true;

            this.data.SaveChanges();

            return this.Ok();
        }
    }
}