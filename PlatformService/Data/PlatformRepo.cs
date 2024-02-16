using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDBContext dBContext;
        public PlatformRepo(AppDBContext dBContext)
        {
             this.dBContext = dBContext;
        }
        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                dBContext.Platforms.Add(platform);
                SaveChanges();
            }
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return dBContext.Platforms.ToList();
        }

        public Platform GetPlatformById(int id)
        {
            return dBContext.Platforms.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return(dBContext.SaveChanges() >= 0);
        }
    }
}
