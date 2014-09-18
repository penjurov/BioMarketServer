namespace BioMarket.Web.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using BioMarket.Data;
    using BioMarket.Web.Models;


    public class ProductController : ApiController
    {

        private readonly IBioMarketData data;

        public ProductController()
            : this(new BioMarketData())
        {
        }

        public ProductController(IBioMarketData data)
        {
            this.data = data;
        }

        [HttpGet]
        public IHttpActionResult All()
        {
            var products = this.data
            .Products
                               .All()
                               .Where(p => p.Deleted == false)
                               .Select(ProductModel.FromProduct);

            return this.Ok(products);
        }

        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            var product = this.data
            .Products
                              .All()
                              .Where(p => p.Id == id && p.Deleted == false)
                              .Select(ProductModel.FromProduct);

            if (product == null)
            {
                return this.BadRequest("Product does not exists - invalid id");
            }

            return this.Ok(product);
        }

        [HttpGet]
        public IHttpActionResult ByName(string id)
        {
            var product = this.data
            .Products
                              .All()
                              .Where(p => p.Name == id && p.Deleted == false)
                              .Select(ProductModel.FromProduct);

            if (product == null)
            {
                return this.BadRequest("Product does not exists - invalid name");
            }

            return this.Ok(product);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Update(int id, ProductModel product)
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

            var existingProduct = this.data
            .Products
                              .All()
                              .Where(p => p.Id == id && p.Deleted == false)
                              .FirstOrDefault();

            if (existingProduct == null)
            {
                return this.BadRequest("Such product does not exists!");
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Farm = product.Farm;

            this.data.SaveChanges();

            product.Id = id;

            var newProduct = new
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Farm = product.Farm
            };

            return this.Ok(newProduct);
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

            var product = this.data
            .Products.All()
                              .Where(p => p.Id == id && p.Deleted == false)
                              .FirstOrDefault();

            if (product == null)
            {
                return this.BadRequest("Such product does not exists!");
            }

            product.Deleted = true;

            this.data.SaveChanges();

            return this.Ok(product);
        }
    }
}