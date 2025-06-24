namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("7f1a9e56-3fce-4b9c-a909-29a2b912df64")), "mehmet", "mehmet@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("d0c4fa89-6b70-4b96-9f1e-7d1d15b420d7")), "john", "john@gmail.com"),
            };

        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("3ac0fcd1-0a99-44dc-b2a5-8c7f86f3a512")), "IPhone X", 500),
                Product.Create(ProductId.Of(new Guid("90f1b55e-41ff-4a61-a083-f64a4ad97d2c")), "Samsung 10", 400),
                Product.Create(ProductId.Of(new Guid("2e2e4f92-bb3c-4ce5-9c44-e07cc4f6378c")), "Huawei Plus", 650),
                Product.Create(ProductId.Of(new Guid("f2f204e6-3e46-4d1f-9182-1e126c477728")), "Xiaomi Mi", 450)
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("mehmet", "ozkaya", "mehmet@gmail.com", "Bahcelievler No:4", "Turkey", "Istanbul", "38050");
                var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

                var payment1 = Payment.Of("mehmet", "5555555555554444", "12/28", "355", 1);
                var payment2 = Payment.Of("john", "8885555555554444", "06/30", "222", 2);

                var order1 = Order.Create(
                                OrderId.Of(Guid.NewGuid()),
                                CustomerId.Of(new Guid("7f1a9e56-3fce-4b9c-a909-29a2b912df64")),
                                OrderName.Of("ORD_1"),
                                shippingAddress: address1,
                                billingAddress: address1,
                                payment1);
                order1.Add(ProductId.Of(new Guid("3ac0fcd1-0a99-44dc-b2a5-8c7f86f3a512")), 2, 500);
                order1.Add(ProductId.Of(new Guid("90f1b55e-41ff-4a61-a083-f64a4ad97d2c")), 1, 400);

                var order2 = Order.Create(
                                OrderId.Of(Guid.NewGuid()),
                                CustomerId.Of(new Guid("d0c4fa89-6b70-4b96-9f1e-7d1d15b420d7")),
                                OrderName.Of("ORD_2"),
                                shippingAddress: address2,
                                billingAddress: address2,
                                payment2);
                order2.Add(ProductId.Of(new Guid("2e2e4f92-bb3c-4ce5-9c44-e07cc4f6378c")), 1, 650);
                order2.Add(ProductId.Of(new Guid("f2f204e6-3e46-4d1f-9182-1e126c477728")), 2, 450);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
