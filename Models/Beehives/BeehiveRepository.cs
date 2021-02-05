using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ePasieka.Models
{
    public class BeehiveRepository : IBeehiveRepository
    {

        private readonly AppDbContext _appDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeehiveRepository(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._appDbContext = appDbContext;
            this._httpContextAccessor = httpContextAccessor;
        }

        public void AddBeehive(Beehive beehive, int howMany)
        {
            var beehives = new List<Beehive>();

            for (int i = 1; i <= howMany; i++)
            {
                var newBeehive = new Beehive
                {
                    name = beehive.name + $" {i}",
                    framesAmount = beehive.framesAmount,
                    scale = beehive.scale,
                    inspectionDate = beehive.inspectionDate,
                    queenExchangeDate = beehive.queenExchangeDate,
                    botttomBoard = beehive.botttomBoard,
                    breed = beehive.breed,
                    annotation = beehive.annotation,
                    beehiveType = beehive.beehiveType,
                    honeyAmount = beehive.honeyAmount,
                    beeGardenID = beehive.beeGardenID
                };
                beehives.Add(newBeehive);
            }
            _appDbContext.beehives.AddRange(beehives);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Beehive> AllBeehives()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _appDbContext.beehives.Where(b => b.beeGarden.userID == user).OrderBy(b => b.beeGardenID);
        }

        public Beehive BeehiveByID(int beehiveID)
        {
            return _appDbContext.beehives.FirstOrDefault(b => b.beeHiveID == beehiveID);
        }

        public IEnumerable<Beehive> BeehivesByBeeGarden(int id)
        {
            return _appDbContext.beehives.Where(b => b.beeGarden.beeGardenID == id);
        }

        public void DeleteBeehive(int id)
        {
            var beehive = _appDbContext.beehives.Find(id);
            _appDbContext.beehives.Remove(beehive);
            _appDbContext.SaveChanges();
        }

        public void UpdateBeehive(Beehive beehive)
        {
            //optymistyczna współbieżność
            var entry = _appDbContext.Entry(beehive);
            entry.State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
