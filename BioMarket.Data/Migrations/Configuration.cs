namespace BioMarket.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BioMarketDBContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "BioMarket.Data.BioMarketDBContext";
        }

        protected override void Seed(BioMarketDBContext context)
        {
        }
    }
}