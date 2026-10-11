using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Kui andmed on juba olemas, siis uusi ei lisa
            if (context.Classes.Any() || context.Customers.Any() || context.Products.Any()
                || context.Orders.Any() || context.OrderLines.Any())
            {
                return;
            }

            var random = new Random(12345); // fikseeritud seeme -> sama andmestik igal käivitusel

            var classes = GenerateClasses(context);
            var customers = GenerateCustomers(context);
            var products = GenerateProducts(context, classes);
            var orders = GenerateOrders(context, customers, random);
            GenerateOrderLines(context, orders, products, random);
        }

        private static List<Class> GenerateClasses(ApplicationDbContext context)
        {
            var names = new[]
            {
                "Nutitelefonid", "Sülearvutid", "Lauaarvutid", "Tahvelarvutid", "Monitorid",
                "Klaviatuurid", "Hiired", "Kõrvaklapid", "Kõlarid", "Teleriid",
                "Nutikellad", "Printerid", "Ruuterid", "Välkmälud ja kettad", "Mälukaardid",
                "Veebikaamerad", "Mikrofonid", "Mängukonsoolid", "Mängupuldid", "Laadijad",
                "Akupangad", "Kaablid ja adapterid", "Telefoniümbrised", "Nutikodu", "Fotokaamerad",
                "Droonid", "Robottolmuimejad", "Kohvimasinad", "Blenderid", "Õhufriteerijad",
                "Veekeetjad", "Triikrauad", "Juuksefööni", "Elektrilised hambaharjad", "Laua- ja töölambid"
            };

            var list = names.Select(n => new Class { Name = n }).ToList();
            context.Classes.AddRange(list);
            context.SaveChanges();
            return list;
        }

        private static List<Customer> GenerateCustomers(ApplicationDbContext context)
        {
            var names = new[]
            {
                "Mart Tamm", "Liis Saar", "Andres Kask", "Kadri Mägi", "Jaan Sepp",
                "Maria Kukk", "Toomas Rebane", "Piret Ilves", "Rein Oja", "Karin Kivi",
                "Margus Kuusk", "Anneli Lepik", "Tiit Põder", "Heli Karu", "Priit Pärn",
                "Merle Lill", "Siim Koppel", "Epp Vaher", "Raivo Jõgi", "Triin Nurk",
                "Indrek Org", "Katrin Mets", "Hannes Luik", "Kristiina Rand", "Urmas Peterson",
                "Evelin Raud", "Madis Aavik", "Jaanika Kallas", "Taavi Sild", "Helen Teder",
                "Oliver Pukk", "Mari-Liis Lõhmus", "Kalev Ots", "Annika Vares", "Ragnar Eller",
                "Tiina Hein", "Gert Kuusik", "Birgit Orav", "Lauri Toom", "Sandra Küttim"
            };

            var list = names.Select(n => new Customer
            {
                Name = n,
                Email = ToEmail(n)
            }).ToList();

            context.Customers.AddRange(list);
            context.SaveChanges();
            return list;
        }

        private static List<Product> GenerateProducts(ApplicationDbContext context, List<Class> classes)
        {
            // (kategooria indeks, nimi, kirjeldus, hind)
            var data = new (int Class, string Name, string Description, decimal Price)[]
            {
                (0,  "Samsung Galaxy S24",        "6,2\" AMOLED ekraan, 128 GB mälu, 50 MP kaamera.", 799.00m),
                (0,  "Apple iPhone 15",           "6,1\" Super Retina XDR ekraan, 128 GB, USB-C.", 899.00m),
                (1,  "Lenovo ThinkPad E14",       "14\" FHD, Intel Core i5, 16 GB RAM, 512 GB SSD.", 749.00m),
                (2,  "HP Pavilion TP01",          "Lauaarvuti AMD Ryzen 5, 16 GB RAM, 512 GB SSD.", 649.00m),
                (3,  "Apple iPad 10.9\"",         "10,9\" Liquid Retina ekraan, 64 GB, Wi-Fi.", 479.00m),
                (4,  "Dell P2422H",               "24\" Full HD IPS monitor, kõrguse reguleerimine.", 189.00m),
                (5,  "Logitech K380",             "Kompaktne Bluetooth klaviatuur mitmele seadmele.", 39.90m),
                (6,  "Logitech MX Master 3S",     "Juhtmeta ergonoomiline hiir, 8000 DPI.", 99.00m),
                (7,  "Sony WH-1000XM5",           "Mürasummutavad juhtmeta kõrvaklapid, 30 h akukestus.", 349.00m),
                (8,  "JBL Flip 6",                "Veekindel kaasaskantav Bluetooth kõlar.", 119.00m),
                (9,  "LG OLED C3 55\"",           "55\" 4K OLED teler, webOS, 120 Hz.", 1399.00m),
                (10, "Garmin Forerunner 255",     "GPS-spordikell pulsimõõtjaga.", 299.00m),
                (11, "Canon PIXMA G3410",         "Värviline tindipaagiga multifunktsionaalne printer.", 229.00m),
                (12, "TP-Link Archer AX55",       "Wi-Fi 6 ruuter, kahe sagedusalaga, 3000 Mbps.", 89.00m),
                (13, "SanDisk Ultra 128 GB",      "USB 3.0 välkmälu, kiirus kuni 130 MB/s.", 16.90m),
                (13, "Samsung T7 1 TB",           "Väline SSD, USB 3.2, kiirus kuni 1050 MB/s.", 99.00m),
                (14, "SanDisk Extreme microSD 64 GB", "microSDXC kaart, A2, V30.", 14.50m),
                (15, "Logitech C920 HD Pro",      "Full HD veebikaamera sisseehitatud mikrofoniga.", 79.00m),
                (16, "Blue Yeti USB",             "USB-mikrofon podcastide ja striimimise jaoks.", 129.00m),
                (17, "Sony PlayStation 5",        "Mängukonsool 825 GB SSD-ga, 4K mängud.", 549.00m),
                (18, "Xbox Wireless Controller",  "Juhtmeta puldi Xboxi ja arvutiga kasutamiseks.", 64.90m),
                (19, "Anker 65W GaN laadija",     "Kiirlaadija 3 pordiga, sobib sülearvutile.", 49.90m),
                (20, "Xiaomi Power Bank 20000",   "20000 mAh akupank 22,5 W kiirlaadimisega.", 32.00m),
                (21, "Baseus USB-C kaabel 2 m",   "Punutud USB-C laadimis- ja andmekaabel.", 9.90m),
                (22, "Spigen Liquid Air ümbris",  "Kaitsev silikoonümbris nutitelefonile.", 14.90m),
                (23, "Philips Hue Starter Kit",   "Nutivalgustuse komplekt: 3 pirni ja sild.", 169.00m),
                (24, "Canon EOS R10",             "Peegelkaamera ilma peegelita, 24,2 MP.", 979.00m),
                (25, "DJI Mini 3",                "Kokkupandav droon alla 249 g, 4K video.", 489.00m),
                (26, "Roborock Q Revo",           "Robottolmuimeja ja põrandapesija isetühjenduva jaamaga.", 599.00m),
                (27, "DeLonghi Magnifica S",      "Automaatne espressomasin kohviveskiga.", 399.00m),
                (28, "Bosch VitaPower MMB6382M",  "Võimas lauablender 1200 W, 2,3 L kann.", 129.00m),
                (29, "Philips Airfryer XL",       "Kuumaõhufritüür 6,2 L, 2000 W.", 169.00m),
                (30, "Russell Hobbs Honeycomb",   "Roostevabast terasest veekeetja 1,7 L.", 39.90m),
                (31, "Tefal Ultragliss Plus",     "Auruga triikraud 2800 W, keraamiline tald.", 49.00m),
                (32, "Dyson Supersonic HD07",     "Juuksefööni intelligentse temperatuurikontrolliga.", 399.00m),
                (33, "Oral-B iO Series 5",        "Elektriline hambahari 5 puhastusrežiimiga.", 149.00m),
                (34, "BenQ ScreenBar Plus",       "Monitorile kinnitatav LED-lamp silmi säästva valgusega.", 119.00m),
                (3,  "Samsung Galaxy Tab S9 FE",  "10,9\" ekraan, 128 GB, S Pen kaasas.", 449.00m),
                (1,  "Apple MacBook Air 13\" M2", "13,6\" Liquid Retina, Apple M2, 8 GB, 256 GB SSD.", 1199.00m),
                (4,  "Samsung Odyssey G5 27\"",   "27\" QHD kumer mängumonitor, 144 Hz.", 279.00m),
                (7,  "Apple AirPods Pro 2",       "Juhtmeta kõrvaklapid aktiivse mürasummutusega.", 279.00m),
                (9,  "Samsung Crystal UHD 50\"",  "50\" 4K UHD teler, Smart TV, HDR.", 449.00m)
            };

            var list = data.Select(d => new Product
            {
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Photo = "/images/products/" + Slug(d.Name) + ".jpg",
                ClassId = classes[d.Class].Id
            }).ToList();

            context.Products.AddRange(list);
            context.SaveChanges();
            return list;
        }

        private static List<Order> GenerateOrders(ApplicationDbContext context, List<Customer> customers, Random random)
        {
            var statuses = new[] { "Uus", "Makstud", "Pakendamisel", "Teel", "Kohale toimetatud", "Tühistatud" };
            var list = new List<Order>();
            var start = new DateTime(2026, 1, 5);

            for (var i = 0; i < 45; i++)
            {
                list.Add(new Order
                {
                    OrderDate = start.AddDays(i * 6 + random.Next(0, 5)).AddHours(random.Next(8, 21)).AddMinutes(random.Next(0, 60)),
                    Status = statuses[random.Next(statuses.Length)],
                    CustomerId = customers[i % customers.Count].Id
                });
            }

            context.Orders.AddRange(list);
            context.SaveChanges();
            return list;
        }

        private static void GenerateOrderLines(ApplicationDbContext context, List<Order> orders, List<Product> products, Random random)
        {
            var list = new List<OrderLine>();

            foreach (var order in orders)
            {
                var lineCount = random.Next(1, 5); // 1-4 rida tellimuse kohta
                var used = new HashSet<int>();

                for (var j = 0; j < lineCount; j++)
                {
                    Product product;
                    do
                    {
                        product = products[random.Next(products.Count)];
                    } while (!used.Add(product.Id));

                    list.Add(new OrderLine
                    {
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = random.Next(1, 4),
                        UnitPrice = product.Price
                    });
                }
            }

            // Tagame, et ridu on vähemalt 35
            while (list.Count < 35)
            {
                var order = orders[random.Next(orders.Count)];
                var product = products[random.Next(products.Count)];
                list.Add(new OrderLine
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.Price
                });
            }

            context.OrderLines.AddRange(list);
            context.SaveChanges();
        }

        private static string Transliterate(string text)
        {
            var map = new Dictionary<char, string>
            {
                ['õ'] = "o", ['ä'] = "a", ['ö'] = "o", ['ü'] = "u",
                ['š'] = "s", ['ž'] = "z", ['"'] = "", ['.'] = ""
            };

            var sb = new StringBuilder();
            foreach (var c in text.ToLowerInvariant())
            {
                if (map.TryGetValue(c, out var replacement))
                {
                    sb.Append(replacement);
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        private static string ToEmail(string name)
        {
            return Transliterate(name).Replace(' ', '.').Replace("-", "") + "@example.ee";
        }

        private static string Slug(string name)
        {
            var text = Transliterate(name);
            var sb = new StringBuilder();
            foreach (var c in text)
            {
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
                else if (sb.Length > 0 && sb[sb.Length - 1] != '-')
                {
                    sb.Append('-');
                }
            }

            return sb.ToString().Trim('-');
        }
    }
}
