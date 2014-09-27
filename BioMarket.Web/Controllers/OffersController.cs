namespace BioMarket.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using BioMarket.Data;
    using BioMarket.Models;
    using BioMarket.Web.Models;

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
        public IHttpActionResult AllDetails(string farmName = "", string postDate = "", string productName = "")
        {
            try
            {
                var offers = this.data
                .Offers
                                 .All()
                                 .Where(o => o.Deleted == false);

                if (farmName != string.Empty && farmName != null)
                {
                    offers = offers.Where(o => o.Product.Farm.Name == farmName);
                }

                if (postDate != string.Empty)
                {
                    try
                    {
                        var date = DateTime.Parse(postDate);
                        offers = offers.Where(o => o.PostDate.Equals(date));
                    }
                    catch (Exception)
                    {
                        return this.BadRequest("Invalid data format! Please follow YYYY-MM-DD format!");
                    }
                }

                if (productName != string.Empty)
                {
                    offers = offers.Where(o => o.Product.Name == productName);
                }

                var returnOffers = offers.Select(OfferModel.FromOfferWithProductAndBoughtBuy);

                return this.Ok(returnOffers);
            }
            catch (Exception)
            {
                return this.BadRequest("Invalid data");
            }
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
       
        [HttpPost]
        public IHttpActionResult Add(OfferModel offer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Invalid data");
            }

            var isFarmer = this.User.IsInRole("Farmer");

            if (!isFarmer)
            {
                return this.BadRequest("You are not farmer!");
            }

            var userName = this.User.Identity.Name;

            var product = this.data.Products.All()
                              .FirstOrDefault(p => p.Id == offer.ProductId);

            var newOffer = new Offer
            {
                Quantity = offer.Quantity,
                ProductPhoto = offer.ProductPhoto,
                PostDate = DateTime.Now,
                ProductId = offer.ProductId,
                Product = product
            };

            this.data.Offers.Add(newOffer);
            this.data.SaveChanges();

            var returnOffer = new
            {
                Id = newOffer.Id,
                Quantity = newOffer.Quantity,
                ProductPhoto = newOffer.ProductPhoto,
                PostDate = newOffer.PostDate,
                ProductId = newOffer.ProductId,
            };

            return this.Ok(returnOffer);
        }

        [HttpPut]
        public IHttpActionResult Buy(int id, OfferModel offer)
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
                ProductId = offer.ProductId
            , };

            return this.Ok(newOffer);
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