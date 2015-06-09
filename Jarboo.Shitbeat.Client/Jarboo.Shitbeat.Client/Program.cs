using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shipbeat.API;
using Shipbeat.API.Constants;
using Shipbeat.API.Items.Requests;
using System.Configuration;
using Shipbeat.API.Items;
using Newtonsoft.Json;
using Shipbeat.API.Addresses.Requests;
using Shipbeat.API.Areas;
using Shipbeat.API.Areas.Requests;
using Shipbeat.API.Carriers.Requests;
using Shipbeat.API.DeliveryPoints.Requests;
using Shipbeat.API.Quotes.Requests;

namespace Jarboo.Shitbeat.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string command = "";
            Console.WriteLine("Please select api which you want to consume:");
            Console.WriteLine("(1) Items");
            Console.WriteLine("(2) Areas");
            Console.WriteLine("(3) Addresses");
            Console.WriteLine("(4) Delivery Points");
            Console.WriteLine("(5) Carriers");
            Console.WriteLine("(6) Quotes");
            Console.WriteLine("(exit) Exit");
            Console.Write("Enter the number (1-6):");
            while ((command = Console.ReadLine()) != "exit")
            {
                int commandInt = 0;
                try
                {
                    commandInt = Int32.Parse(command);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please select next command (1-6):");
                    continue;
                }

                try
                {
                    switch (commandInt)
                    {
                        case 1:
                            RunItemsApi();
                            break;
                        case 2:
                            RunAreasApi();
                            break;
                        case 3:
                            RunAddressesApi();
                            break;
                        case 4:
                            RunDeliveryPointApi();
                            break;
                        case 5:
                            RunCarrierApi();
                            break;
                        case 6:
                            RunQuoteApi();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write("Please select next command (1-6):");
            }
        }

        private static void RunItemsApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to create new item and 2 to get item: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var item = new ItemCreateRequest
                {
                    Currency = Currency.USD,
                    LengthUnit = LengthUnit.Centimeter,
                    MassUnit = MassUnit.Gram,
                    Packaging = Packaging.Bag,
                    Oriented = false
                };

                Console.Write("Enter the height:");
                item.Height = Decimal.Parse(Console.ReadLine());
                Console.Write("Enter the length:");
                item.Length = Decimal.Parse(Console.ReadLine());
                Console.Write("Enter the value:");
                item.Value = Decimal.Parse(Console.ReadLine());
                Console.Write("Enter the weight:");
                item.Weight = Decimal.Parse(Console.ReadLine());
                Console.Write("Enter the Width:");
                item.Width = Decimal.Parse(Console.ReadLine());

                var result = client.Items.Post(item);
                Console.WriteLine("Item is created:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter item id: ");
                var itemId = Console.ReadLine();
                var result = client.Items.Get(itemId);
                Console.WriteLine("Item is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }

        private static void RunAreasApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to search areas and 2 to get area: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var area = new AreaGetRequest
                {
                };

                Console.Write("Enter the query:");
                area.Q = Console.ReadLine();
                Console.Write("Enter the limit:");
                area.Limit = Int32.Parse(Console.ReadLine());

                var results = client.Areas.Get(area);
                Console.WriteLine("Areas are retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter area id: ");
                var areaId = Console.ReadLine();
                var result = client.Areas.Get(areaId);
                Console.WriteLine("Area is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }

        private static void RunAddressesApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to create new address and 2 to get address: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var address = new AddressCreateRequest
                {
                    Type = AddressType.Business,
                    CountryCode = "DK"
                };

                Console.Write("Enter the name1:");
                address.Name1 = Console.ReadLine();
                Console.Write("Enter the name2:");
                address.Name2 = Console.ReadLine();
                Console.Write("Enter the street1:");
                address.Street1 = Console.ReadLine();
                Console.Write("Enter the postal code:");
                address.PostalCode = Console.ReadLine();
                Console.Write("Enter the city:");
                address.City = Console.ReadLine();
                Console.Write("Enter the state:");
                address.State = Console.ReadLine();
                Console.Write("Enter the email:");
                address.Email = Console.ReadLine();
                Console.Write("Enter the phone:");
                address.Phone = Console.ReadLine();
                Console.Write("Enter the sms:");
                address.Sms = Console.ReadLine();

                var result = client.Addresses.Post(address);
                Console.WriteLine("Address is created:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter address id: ");
                var itemId = Console.ReadLine();
                var result = client.Addresses.Get(itemId);
                Console.WriteLine("Address is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }

        private static void RunDeliveryPointApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to search delivery points and 2 to get delivery point: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var dPoint = new DeliveryPointGetRequest
                {
                };

                Console.Write("Enter the limit:");
                dPoint.Limit = Int32.Parse(Console.ReadLine());

                var results = client.DeliveryPoints.Get(dPoint);
                Console.WriteLine("Delivery points are retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter delivery point id: ");
                var areaId = Console.ReadLine();
                var result = client.DeliveryPoints.Get(areaId);
                Console.WriteLine("Delivery point is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }

        private static void RunCarrierApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to search carriers and 2 to get carrier: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var carrier = new CarrierGetRequest
                {
                };

                Console.Write("Enter the query:");
                carrier.Q = Console.ReadLine();
                Console.Write("Enter the limit:");
                carrier.Limit = Int32.Parse(Console.ReadLine());

                var results = client.Carriers.Get(carrier);
                Console.WriteLine("Carriers are retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter carrier id: ");
                var areaId = Console.ReadLine();
                var result = client.Carriers.Get(areaId);
                Console.WriteLine("Carrier is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }

        private static void RunQuoteApi()
        {
            var client = new ShipbeatClient(ConfigurationManager.AppSettings["ShipbeatApi.Key"]);

            Console.Write("Type 1 to create new quote and 2 to get quote: ");
            var command = Console.ReadLine();
            if (command == "1")
            {
                var item = new QuoteCreateByTemplateRequest
                {
                    From = "WSF4MVnHaa6LhA4KarweJ3", //Area or Address
                    To = "WSF4MVnHaa6LhA4KarweJ3", //Area or Address
                    Items = new List<string>(),
                    DeliveryOptions = new List<string>()
                };

                Console.Write("Enter the item id:");
                item.Items.Add(Console.ReadLine());
                Console.Write("Enter the delivery aption (return_dropoff, home_evening, home_letter, home, business, delivery_point):");
                item.DeliveryOptions.Add(Console.ReadLine());

                var result = client.Quotes.PostReturnMultiple(item);
                Console.WriteLine("Quotes is created:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            else if (command == "2")
            {
                Console.Write("Enter quote id: ");
                var itemId = Console.ReadLine();
                var result = client.Quotes.Get(itemId);
                Console.WriteLine("Quote is retrieved:");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
        }
    }
}
