using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Wireless Headphones",
                Category = new List<string> { "Electronics", "Audio" },
                Description = "High quality wireless headphones with noise cancellation.",
                ImageFile = "wireless_headphones.png",
                Price = 129.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Gaming Mouse",
                Category = new List<string> { "Electronics", "Gaming" },
                Description = "Ergonomic gaming mouse with customizable DPI and RGB lighting.",
                ImageFile = "gaming_mouse.jpg",
                Price = 49.99m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Coffee Maker",
                Category = new List<string> { "Home Appliances", "Kitchen" },
                Description = "Brew your perfect cup with this programmable coffee maker.",
                ImageFile = "coffee_maker.jpg",
                Price = 89.50m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Yoga Mat",
                Category = new List<string> { "Fitness", "Wellness" },
                Description = "Eco-friendly non-slip yoga mat for all levels.",
                ImageFile = "yoga_mat.jpg",
                Price = 25.00m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Smart Watch",
                Category = new List<string> { "Electronics", "Wearables" },
                Description = "Track your health and stay connected with this stylish smart watch.",
                ImageFile = "smart_watch.jpg",
                Price = 199.99m
            }
        };
    }
}
