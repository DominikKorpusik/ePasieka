using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace ePasieka.Models
{
    public class BeeGardenRepository : IBeeGardenRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeeGardenRepository(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._appDbContext = appDbContext;
            this._httpContextAccessor = httpContextAccessor;
        }


        public void AddBeeGarden(BeeGarden beeGarden)
        {
            _appDbContext.Add(beeGarden);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<BeeGarden> AllBeeGardens()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.beeGardens.Where(b => b.userID == user).OrderBy(b => b.name);
        }

        public IEnumerable<string> AllBeeGardensByName()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.beeGardens.Where(b => b.userID == user).OrderBy(b => b.name).Select(b => b.name);
        }

        public BeeGarden BeeGardenByID(int beeYardID)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.beeGardens.Where(b => b.userID == user).FirstOrDefault(b => b.beeGardenID == beeYardID);

        }

        public void DeleteBeeGarden(int id)
        {
            var beeYard = _appDbContext.beeGardens.Find(id);
            _appDbContext.beeGardens.Remove(beeYard);
            _appDbContext.SaveChanges();
        }

        public void UpdateBeeGarden(BeeGarden beeYard)
        {
            //optymistyczna współbieżność
            var entry = _appDbContext.Entry(beeYard);
            entry.State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
