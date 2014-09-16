namespace BioMarket.Controllers
{   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using BioMarket.Data;

    public class FarmController : ApiController
    {
        private readonly IBioMarketData data;

        public FarmController() : this(new BioMarketData())
        {

        }

        public FarmController(IBioMarketData data)
        {
            this.data = data;
        }
    }
}