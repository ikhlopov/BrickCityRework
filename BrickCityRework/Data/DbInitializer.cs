using BrickCityRework.Models.EF;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BrickCityRework.Data
{
    public class DbInitializer
    {
        // Принимает на вход модель файла в которой задана строка содержащая текст из загруженного файла
        public static void AddConsumersFromFileToContext(FileModel file, BrickCityContext context)
        {
            City consumersDS = City.GetCityFromJSON(file.Content);
            foreach (var consumerDS in consumersDS.houses)
            {
                var consumer_to_add = new Consumer
                {
                    Name = consumerDS.Name,
                    TypeOfConsumer = Consumer_Type.House,
                    File = file
                };
                context.Consumers.Add(consumer_to_add);
                foreach (var consumptionDS in consumerDS.consumptions)
                {
                    var consumption_to_add = new Consumption { Consumer = consumer_to_add, Date = consumptionDS.Date, Value = consumptionDS.Consumption };
                    var weather_to_add = new Weather { Value = consumptionDS.Weather };
                    var msrmnt_to_add = new Msrmnt { Weather = weather_to_add, Consumption = consumption_to_add, Msrmnt_Type = Msrmnt_Type.Weather };
                    context.Consumptions.Add(consumption_to_add);
                    context.Weathers.Add(weather_to_add);
                    context.Msrmnts.Add(msrmnt_to_add);
                }
                context.SaveChanges();
            }

            foreach (var consumerDS in consumersDS.plants)
            {
                var consumer_to_add = new Consumer
                {
                    Name = consumerDS.Name,
                    TypeOfConsumer = Consumer_Type.Plant,
                    File = file
                };
                context.Consumers.Add(consumer_to_add);
                foreach (var consumptionDS in consumerDS.consumptions)
                {
                    var consumption_to_add = new Consumption { Consumer = consumer_to_add, Date = consumptionDS.Date, Value = consumptionDS.Consumption };
                    var price_to_add = new Price { Value = consumptionDS.Price, Consumer=consumer_to_add};
                    var msrmnt_to_add = new Msrmnt { Price = price_to_add, Consumption = consumption_to_add, Msrmnt_Type = Msrmnt_Type.Price };
                    context.Consumptions.Add(consumption_to_add);
                    context.Prices.Add(price_to_add);
                    context.Msrmnts.Add(msrmnt_to_add);
                }
                context.SaveChanges();
            }

            
        }
    }
}
