using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioMarket.Data;

namespace BioMarket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new BioMarketData();

            data.Accounts.All().FirstOrDefault();
        }
    }
}
