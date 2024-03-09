using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDB
    {
        public static void PropPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>());
            }
        }

        private static void SeedData(AppDBContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data...");
                context.Platforms.AddRange(
                    new Platform() { Name= "Youtube", Publisher="Les Jackson", Cost="Free"},
                    new Platform() { Name = "Meta", Publisher = "Mark", Cost = "Free" },
                    new Platform() { Name = "Twitter", Publisher = "Erimus", Cost = "Free" }
                    );

                context.SaveChanges();
                Console.WriteLine("Database seeded successfully");
            }
            else
            {
                Console.WriteLine("There's already data on the database");
            }
        }
    }
}
