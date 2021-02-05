using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ePasieka.Models
{
    public class NucRepository : INucRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NucRepository(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._appDbContext = appDbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public void AddNuc(Nuc nuc)
        {
            _appDbContext.Add(nuc);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Nuc> AllNucs()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.nucs.Where(n => n.beeGarden.userID == user).OrderBy(n => n.nucID);

        }

        public void DeleteNuc(int id)
        {
            var nuc = _appDbContext.nucs.Find(id);
            _appDbContext.nucs.Remove(nuc);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Nuc> NucsByBeeGarden(int id)
        {
            return _appDbContext.nucs.Where(n => n.beeGardenID == id);
        }

        public Nuc NucsByID(int id)
        {
           return _appDbContext.nucs.FirstOrDefault(n => n.nucID == id);
        }

        public void UpdateNuc(Nuc nuc)
        {
            //optymistyczna współbieżność
            var entry = _appDbContext.Entry(nuc);
            entry.State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
