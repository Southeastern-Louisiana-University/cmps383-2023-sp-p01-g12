using Microsoft.EntityFrameworkCore;
using SP23.P01.Web.Data;
using SP23.P01.Web.Entities;

namespace SP23.P01.Web.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DataContext>>()))
            {
                //look for TrainStations
                if (context.TrainStations.Any())
                {
                    return; // seeding the DB
                }

                context.TrainStations.AddRange(
                    new TrainStation
                    {
                        Id = 1,
                        Name = "Station 1",
                        Address = "1234 Street"
                    },
                    new TrainStation
                    {
                        Id = 2,
                        Name = "Station 2",
                        Address = "5678 Street"
                    },
                    new TrainStation
                    {
                        Id = 3,
                        Name = "Station 3",
                        Address = "9245 Street",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
